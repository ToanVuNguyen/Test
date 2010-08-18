﻿using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using HPF.FutureState.Common;
using System.Collections.Generic;
using System;

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
        public void SaveCaseEvalSet(CaseEvalHeaderDTO caseEvalHeader, CaseEvalSetDTO caseEvalSetDraft,bool isHpfUser, string userId)
        {
            CaseEvalSetDAO caseEvalSetDAO = CaseEvalSetDAO.CreateInstance();
            try
            {
                caseEvalSetDAO.Begin();
                //Need to modify this value
                bool isResultWithinRange = false;
                string evalStatus = GetEvaluationStatus(isHpfUser, caseEvalHeader.EvalType, caseEvalHeader.EvalStatus, isResultWithinRange);
                if (evalStatus != caseEvalHeader.EvalStatus)
                {
                    caseEvalHeader.EvalStatus = evalStatus;
                    caseEvalSetDAO.UpdateCaseEvalHeader(caseEvalHeader);
                }
                //Insert new case eval set
                caseEvalSetDraft.SetInsertTrackingInformation(userId);
                caseEvalSetDraft.HpfAuditInd = (isHpfUser ? "Y" : "N");
                int? caseEvalSetId = caseEvalSetDAO.InsertCaseEvalSet(caseEvalSetDraft);
                foreach (CaseEvalDetailDTO caseEvalDetailDraft in caseEvalSetDraft.CaseEvalDetails)
                {
                    caseEvalDetailDraft.CaseEvalSetId = caseEvalSetId;
                    caseEvalDetailDraft.SetInsertTrackingInformation(userId);
                    caseEvalSetDAO.InsertCaseEvalDetail(caseEvalDetailDraft);
                }
            }
            catch (Exception ex)
            {
                caseEvalSetDAO.Cancel();
                throw ex;
            }
            finally
            {
                caseEvalSetDAO.Commit();
            }
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
                    }
            }
            return result;
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
            public const string YES = "Y";
            public const string NO = "N";
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
    }
}
