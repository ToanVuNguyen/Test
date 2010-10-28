using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using HPF.FutureState.Common;
using System.Collections.Generic;
using System;
using HPF.FutureState.Common.Utils;
using System.Text;
using System.Data.SqlClient;
using System.IO;

namespace HPF.FutureState.BusinessLogic
{
    public class CaseEvaluationBL: BaseBusinessLogic
    {
        private static readonly CaseEvaluationBL instance = new CaseEvaluationBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CaseEvaluationBL Instance
        {
            get
            {
                return instance;
            }
        }
        protected CaseEvaluationBL()
        {

        }

        public CaseEvalSearchResultDTOCollection SearchCaseEval(CaseEvalSearchCriteriaDTO caseEvalCriteria)
        {
            return CaseEvalHeaderDAO.Instance.SearchCaseEval(caseEvalCriteria);
        }

        public CaseEvalSearchResultDTO SearchCaseEvalByFcId(int fcId)
        {
            return CaseEvalHeaderDAO.Instance.SearchCaseEvalByFcId(fcId);
        }

        public CaseEvalHeaderDTO GetCaseEvalHeaderByCaseId(int fc_ID)
        {
            return CaseEvalHeaderDAO.Instance.GetCaseEvalHeaderByCaseId(fc_ID);
        }
        public CaseEvalSetDTO GetCaseEvalLatest(int? caseEvalHeaderId, string hpfAuditInd)
        {
            CaseEvalSetDAO instance = CaseEvalSetDAO.CreateInstance();
            return instance.GetCaseEvalLatestSet(caseEvalHeaderId, hpfAuditInd);
        }
        public CaseEvalSetDTOCollection GetCaseEvalLatestAll(int fcId)
        {
            CaseEvalSetDAO instance = CaseEvalSetDAO.CreateInstance();
            return instance.GetCaseEvalLatestSetAll(fcId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caseEvalHeader"></param>
        public void SaveSelectForQCEvalHeader(CaseEvalHeaderDTO caseEvalHeader)
        {
            try
            {
                if (caseEvalHeader.EvalType == EvaluationType.DESKTOP)
                {
                    caseEvalHeader.EvaluationYearMonth = DateTime.Today.ToString("yyyy") + DateTime.Today.AddMonths(-1).ToString("MM");
                    caseEvalHeader.EvalType = EvaluationType.DESKTOP;
                    caseEvalHeader.EvalStatus = EvaluationStatus.AGENCY_INPUT_REQUIRED;
                }
                else
                {
                    caseEvalHeader.EvaluationYearMonth = DateTime.Today.ToString("yyyy") + DateTime.Today.ToString("MM");
                    caseEvalHeader.EvalType = EvaluationType.ONSITE;
                    caseEvalHeader.EvalStatus = EvaluationStatus.HPF_INPUT_REQUIRED;
                }

                CaseEvalHeaderDAO.Instance.InsertCaseEvalHeader(caseEvalHeader);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Insert case set and all questions to database
        /// Roll back when insert fail
        /// </summary>
        /// <param name="caseEvalSetDraft"></param>
        /// <param name="userId">User login</param>
        /// <param name="isHpfUser"></param>
        public void SaveCaseEvalSet(CaseEvalHeaderDTO caseEvalHeader, CaseEvalSetDTO caseEvalSetDraft, bool isHpfUser, string userId,bool isFullFill)
        {
            CaseEvalSetDAO caseEvalSetDAO = CaseEvalSetDAO.CreateInstance();
            string prevAuditor = string.Empty;
            bool isResultWithinRange = false;
            string evalStatus = string.Empty;
            try
            {
                caseEvalSetDAO.Begin();
                //Check if all questions be fullfilled
                if (isFullFill)
                {
                    isResultWithinRange = CompareScoreInRange(caseEvalSetDraft, caseEvalHeader, ref prevAuditor);
                    evalStatus = GetEvaluationStatus(isHpfUser, caseEvalHeader.EvalType, caseEvalHeader.EvalStatus, isResultWithinRange);
                }
                if (!string.IsNullOrEmpty(evalStatus) && evalStatus != caseEvalHeader.EvalStatus)
                {
                    caseEvalHeader.EvalStatus = evalStatus;
                }
                caseEvalHeader.SetUpdateTrackingInformation(userId);
                caseEvalSetDAO.UpdateCaseEvalHeader(caseEvalHeader);
                //Insert new case eval set
                caseEvalSetDraft.SetInsertTrackingInformation(userId);
                caseEvalSetDraft.HpfAuditInd = (isHpfUser ? Constant.INDICATOR_YES : Constant.INDICATOR_NO);
                int? caseEvalSetId = caseEvalSetDAO.InsertCaseEvalSet(caseEvalSetDraft);
                foreach (CaseEvalDetailDTO caseEvalDetailDraft in caseEvalSetDraft.CaseEvalDetails)
                {
                    caseEvalDetailDraft.CaseEvalSetId = caseEvalSetId;
                    caseEvalDetailDraft.SetInsertTrackingInformation(userId);
                    caseEvalSetDAO.InsertCaseEvalDetail(caseEvalDetailDraft);
                }
                caseEvalSetDAO.Commit();
                if (isFullFill && !isHpfUser)
                    SendNotifyEmail(prevAuditor, caseEvalHeader.FcId.Value, caseEvalHeader.AgencyId.Value, caseEvalHeader.EvalStatus, caseEvalHeader.EvalType);
            }
            catch (SqlException ex)
            {
                caseEvalSetDAO.Rollback();
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                caseEvalSetDAO.CloseConnection();
            }
        }
        /// <summary>
        /// Remove Case Eval from QC 
        /// </summary>
        /// <param name="evalHeader"></param>
        public void RemoveCaseEval(CaseEvalHeaderDTO evalHeader,string rootFolder)
        {
            CaseEvalSetDAO instance = CaseEvalSetDAO.CreateInstance();
            try
            {
                CaseEvalFileDTOCollection files = GetCaseEvalFileByEvalHeaderIdAll(evalHeader.CaseEvalHeaderId);
                instance.Begin();
                //Remove case_eval_header
                instance.RemoveCaseEvalHeader(evalHeader.CaseEvalHeaderId);
                //Remove all files belong to case eval
                StringBuilder fullPath;
                foreach (CaseEvalFileDTO file in files)
                {
                    fullPath = new StringBuilder();
                    fullPath.AppendFormat("{0}{1}{2}", rootFolder, file.FilePath, file.FileName);
                    if (File.Exists(fullPath.ToString()))
                    {
                        File.Delete(fullPath.ToString());
                    }
                }
                //Remove fcid directory
                if (files.Count > 0)
                {
                    fullPath = new StringBuilder();
                    fullPath.AppendFormat("{0}{1}", rootFolder, files[0].FilePath);
                    if (Directory.Exists(fullPath.ToString()) && Directory.GetFiles(fullPath.ToString()).Length==0)
                        Directory.Delete(fullPath.ToString());
                }
                //Commit all changes
                instance.Commit();
            }
            catch (Exception ex)
            {
                instance.Rollback();
                throw (ex);
            }
            finally
            {
                instance.CloseConnection();
            }
        }
        public void UpdateCaseEvalHeader(CaseEvalHeaderDTO caseEvalHeader)
        {
            CaseEvalSetDAO instance = CaseEvalSetDAO.CreateInstance();
            try
            {
                instance.Begin();
                instance.UpdateCaseEvalHeader(caseEvalHeader);
                instance.Commit();
            }
            catch (Exception ex)
            {
                instance.Rollback();
                throw ex;
            }
            finally
            {
                instance.CloseConnection();
            }
        }
        public void InsertCaseEvalFile(CaseEvalFileDTO caseEvalFile, CaseEvalSearchResultDTO caseEval, string loginName)
        {
            CaseEvalSetDAO instance = CaseEvalSetDAO.CreateInstance();
            try
            {
                instance.Begin();
                instance.InsertCaseEvalFile(caseEvalFile);
                instance.Commit();
            }
            catch (Exception ex)
            {
                instance.Rollback();
                throw ex;
            }
            finally
            {
                instance.CloseConnection();
            }
        }
        public CaseEvalFileDTOCollection GetCaseEvalFileByEvalHeaderIdAll(int? caseEvalHeaderId)
        {
            return CaseEvalHeaderDAO.Instance.GetCaseEvalFileByEvalHeaderIdAll(caseEvalHeaderId);
        }
        /// <summary>
        /// Get next evaluation status of case 
        /// </summary>
        /// <param name="hpfUserInd"></param>
        /// <param name="evalType"></param>
        /// <param name="prevEvalStatus"></param>
        /// <param name="isResultWithinRange">Two types of score in range +-5%</param>
        /// <returns></returns>
        private string GetEvaluationStatus(bool isHpfUser,string evalType, string prevEvalStatus,bool isResultWithinRange)
        {
            string result = prevEvalStatus;
            if (evalType == EvaluationType.DESKTOP)
            {
                if (isResultWithinRange)
                    result = EvaluationStatus.RESULT_WITHIN_RANGE;
                else
                    switch (prevEvalStatus)
                    {
                        case EvaluationStatus.AGENCY_INPUT_REQUIRED:
                            result = EvaluationStatus.AGENCY_UPLOAD_REQUIRED;
                            break;
                        case EvaluationStatus.AGENCY_UPLOAD_REQUIRED:
                            result = EvaluationStatus.HPF_INPUT_REQUIRED;
                            break;
                        case EvaluationStatus.HPF_INPUT_REQUIRED:
                            if (isHpfUser)
                                result = EvaluationStatus.RECON_REQUIRED_AGENCY_INPUT;
                            break;
                        case EvaluationStatus.RECON_REQUIRED_AGENCY_INPUT:
                            if (!isHpfUser)
                                result = EvaluationStatus.RECON_REQUIRED_HPF_INPUT;
                            break;
                        case EvaluationStatus.RECON_REQUIRED_HPF_INPUT:
                            if (isHpfUser)
                                result = EvaluationStatus.RECON_REQUIRED_AGENCY_INPUT;
                            break;
                        case EvaluationStatus.RESULT_WITHIN_RANGE:
                            if (isHpfUser)
                                result = EvaluationStatus.RECON_REQUIRED_AGENCY_INPUT;
                            else
                                result = EvaluationStatus.RECON_REQUIRED_HPF_INPUT;
                            break;
                    }
            }
            return result;
        }
        /// <summary>
        /// Compare new set score with the score latest of the another user type
        /// </summary>
        /// <param name="evalSetNew"></param>
        /// <returns>True if in 5% range,false if out of this range</returns>
        private bool CompareScoreInRange(CaseEvalSetDTO evalSetNew, CaseEvalHeaderDTO caseEvalHeader, ref string prevAuditorId)
        {
            //HPF Auditor have not audited yet, does not need compare score
            if (string.Compare(caseEvalHeader.EvalStatus, EvaluationStatus.AGENCY_INPUT_REQUIRED) == 0
                || string.Compare(caseEvalHeader.EvalStatus, EvaluationStatus.AGENCY_UPLOAD_REQUIRED) == 0)
            return false;
            
            string hpfAuditIndLatest = (evalSetNew.HpfAuditInd==Constant.INDICATOR_YES?Constant.INDICATOR_NO:Constant.INDICATOR_YES);
            CaseEvalSetDTO evalSetLatest = GetCaseEvalLatest(evalSetNew.CaseEvalHeaderId, hpfAuditIndLatest);
            if (evalSetLatest == null) return false;
            prevAuditorId = evalSetLatest.ChangeLastUserId;
            decimal percentNew = Math.Round((decimal)((decimal)evalSetNew.TotalAuditScore/ (decimal)evalSetNew.TotalPossibleScore), 4);
            decimal percentLatest = Math.Round((decimal)((decimal)evalSetLatest.TotalAuditScore / (decimal)evalSetLatest.TotalPossibleScore), 4);
            if (((percentNew - percentLatest <= (decimal)0.05) && (percentNew - percentLatest>=0))
                || ((percentLatest - percentNew <= (decimal)0.05) && (percentLatest - percentNew>=0)))
                return true;
            else
                return false;
        }
        public void SendNotifyEmail(string prevAuditorId, int fcId, int agencyId, string evalStatus, string evalType)
        {
            //Does not send mail when status of evaluation case is OnSite
            if (evalType == EvaluationType.ONSITE) return;
            if (string.Compare(evalStatus, EvaluationStatus.RECON_REQUIRED_AGENCY_INPUT) == 0
                    || string.Compare(evalStatus, EvaluationStatus.RECON_REQUIRED_HPF_INPUT) == 0
                    || string.Compare(evalStatus,EvaluationStatus.AGENCY_INPUT_REQUIRED)==0)
            {
                StringBuilder subject = new StringBuilder();
                subject.Append("HPF Quality Control");

                //Get agency info
                CaseEvalSearchResultDTO caseEval = SearchCaseEvalByFcId(fcId);
                string agencyCaseNum = caseEval.AgencyCaseNum;
                string agencyName = caseEval.AgencyName;
                if (evalStatus == EvaluationStatus.AGENCY_INPUT_REQUIRED)
                {
                    HPFUserDTOCollection hpfUsers = HPFUserBL.Instance.RetriveHpfUsersByAgencyId(agencyId);
                    foreach (HPFUserDTO hpfUser in hpfUsers)
                    {
                        if (!string.IsNullOrEmpty(hpfUser.Email))
                        {
                            StringBuilder content = new StringBuilder();
                            content.AppendFormat("Dear {0},\n \n"
                                   + "HPF has selected case id {1}, agency number of {2}  for this month's audit.  Please use the following URL to input your evaluation result and upload the supporting files.\n \n"
                                   + "https://www.hopenetadmin.org/QCSelectionCaseInfo.aspx?caseId={3}\n\n"
                                   + "Sincerely Yours,\n\n"
                                   + "HPF Auditor", hpfUser.FullName, fcId.ToString(), agencyCaseNum, fcId.ToString());
                            var email = new HPFSendMail
                            {
                                To = hpfUser.Email,
                                Body = content.ToString(),
                                Subject = subject.ToString()
                            };
                            email.Send();
                        }
                    }
                }
                else
                {
                    UserDTO user = SecurityBL.Instance.GetWebUser(prevAuditorId);
                    string toEmail = user.Email;
                    string personName = user.FirstName + " " + user.LastName;
                    StringBuilder content = new StringBuilder();
                    HPFSendMail email;
                    switch (evalStatus)
                    {
                        case EvaluationStatus.RECON_REQUIRED_AGENCY_INPUT:
                            content.AppendFormat("Dear {0},\n \n"
                                   + "HPF has evaluated case id {1}, agency number of {2}  and the result is not within +/-5% range.  Please use the following URL to review the evaluation result and calibrate the scoring to fall within range.\n \n"
                                   + "https://www.hopenetadmin.org/QCSelectionCaseInfo.aspx?caseId={3}\n\n"
                                   + "Sincerely Yours,\n"
                                   + "HPF Auditor", personName, fcId.ToString(), agencyCaseNum, fcId.ToString());
                            email = new HPFSendMail
                            {
                                To = toEmail,
                                Body = content.ToString(),
                                Subject = subject.ToString()
                            };
                            email.Send();
                            break;
                        case EvaluationStatus.RECON_REQUIRED_HPF_INPUT:
                            content.AppendFormat("Dear HPF QC Auditor,\n \n"
                                   + "Agency {0} has updated case id {1} and still results in reconciliation stage.  Please use the following URL for reviewing the case status and calibrate the scoring to fall within range.\n \n"
                                   + "https://www.hopenetadmin.org/QCSelectionCaseInfo.aspx?caseId={2}\n\n"
                                   + "Sincerely Yours,\n\n"
                                   + "HPF HopeNet Admin", agencyName, fcId.ToString(), fcId.ToString());
                            email = new HPFSendMail
                            {
                                To = toEmail,
                                Body = content.ToString(),
                                Subject = subject.ToString()
                            };
                            email.Send();
                            break;
                    }
                }
            }
        }
        public CaseEvalDetailDTOCollection AssignAllQuestionScores(CaseEvalDetailDTOCollection caseEvalDetails,ref int totalYesAnswer,ref int totalNoAnswer,ref int totalNAAnswer)
        {
            foreach (CaseEvalDetailDTO evalDetail in caseEvalDetails)
            {
                switch (evalDetail.EvalAnswer)
                {
                    case CaseEvaluationBL.EvaluationYesNoAnswer.YES:
                        evalDetail.AuditScore = (int)evalDetail.QuestionScore;
                        totalYesAnswer++;
                        break;
                    case CaseEvaluationBL.EvaluationYesNoAnswer.NO:
                        evalDetail.AuditScore = 0;
                        totalNoAnswer++;
                        break;
                    case CaseEvaluationBL.EvaluationYesNoAnswer.NA:
                        evalDetail.AuditScore = 0;
                        totalNAAnswer++;
                        break;
                    case null:
                        break;
                }
            }

            return caseEvalDetails;
        }

        public CaseEvalSetDTO CalculateCaseTotalScore(CaseEvalSetDTO caseEvalSet,bool isHpfUser, ref int totalNoScore,ref int totalNAScore,ref bool warningMessage)
        {
            int totalYesScore = 0;
            int totalPossibleScore = 0;
            totalNoScore = 0;
            totalNAScore = 0;
            foreach (CaseEvalDetailDTO evalDetail in caseEvalSet.CaseEvalDetails)
            {
                switch (evalDetail.EvalAnswer)
                {
                    case CaseEvaluationBL.EvaluationYesNoAnswer.YES:
                        totalYesScore = totalYesScore + (int)evalDetail.AuditScore;
                        totalPossibleScore = totalPossibleScore + (int)evalDetail.QuestionScore;
                        break;
                    case CaseEvaluationBL.EvaluationYesNoAnswer.NO:
                        totalNoScore = totalNoScore + (int)evalDetail.AuditScore;
                        totalPossibleScore = totalPossibleScore + (int)evalDetail.QuestionScore; 
                        break;
                    case CaseEvaluationBL.EvaluationYesNoAnswer.NA:
                        totalNAScore = totalNAScore + (int)evalDetail.AuditScore;
                        break;
                    case null:
                        if (!isHpfUser)
                            throw new Exception("All questions required answer");
                        else
                            warningMessage = true;
                        break;
                }
            }
            if (!warningMessage)
            {
                decimal percent = Math.Round((decimal)((decimal)totalYesScore / (decimal)totalPossibleScore), 2);

                //Set all value back to set dto
                caseEvalSet.ResultLevel = GetLevelNameFromPercent((double)percent);
                caseEvalSet.TotalAuditScore = totalYesScore;
                caseEvalSet.TotalPossibleScore = totalPossibleScore;
                //Check FatalErrorInd
                if ((!string.IsNullOrEmpty(caseEvalSet.FatalErrorInd)) && (caseEvalSet.FatalErrorInd == Constant.INDICATOR_YES))
                {
                    caseEvalSet.ResultLevel = CaseEvaluationBL.ResultLevel.REMEDIATION;
                }
            }
            return caseEvalSet;
        }

        public string GetLevelNameFromPercent(double percent)
        {
            if (percent >= 0.95)
                return ResultLevel.MASTERY;
            if ((percent < 0.95) && (percent >= 0.85))
                return ResultLevel.PROFICIENCY;
            if ((percent < 0.85) && (percent >= 0.70))
                return ResultLevel.NOVICE;
            return ResultLevel.REMEDIATION;
        }

        public class EvaluationYesNoAnswer
        {
            public const string YES = Constant.INDICATOR_YES;
            public const string NO = Constant.INDICATOR_NO;
            public const string NA = "NA";
        }

        public class EvaluationType
        {
            public const string DESKTOP = "Desktop";
            public const string ONSITE = "Onsite";
        }

        public class EvaluationStatus
        {
            public const string AGENCY_INPUT_REQUIRED = "Agency Input Required";
            public const string AGENCY_UPLOAD_REQUIRED = "Agency Upload Required";
            public const string HPF_INPUT_REQUIRED = "HPF Input Required";
            public const string RECON_REQUIRED_AGENCY_INPUT = "Recon Required/Agency Input";
            public const string RECON_REQUIRED_HPF_INPUT = "Recon Required/HPF Input";
            public const string RESULT_WITHIN_RANGE = "Result within Range";
            public const string CLOSED = "Closed";
        }

        public class ResultLevel
        {
            public const string MASTERY = "Mastery";
            public const string PROFICIENCY = "Proficiency";
            public const string NOVICE = "Novice";
            public const string REMEDIATION = "Remediation";
        }

        public class EvaluationAnswer
        {
            public const string YES = "Yes";
            public const string NO = "No";
            public const string NA = "NA";
        }

    }
}
