using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.Utils.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.ObjectModel;
using Microsoft.Practices.EnterpriseLibrary.Logging;
//using HPF.FutureState.Web.Security;

namespace HPF.FutureState.BusinessLogic
{
    public class ForeclosureCaseBL : BaseBusinessLogic
    {
        const int NUMBER_OF_ERROR_APP_SEARCH_CRITERIA = 12;
        private static readonly ForeclosureCaseBL instance = new ForeclosureCaseBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static ForeclosureCaseBL Instance
        {
            get
            {
                return instance;
            }
        }
        protected ForeclosureCaseBL()
        {
        }
       
        /// <summary>
        /// Check if the user has input any info to the search criteria?
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>true for doing search and false for exception</returns>
        private bool ValidateSearchCriteria(AppForeclosureCaseSearchCriteriaDTO searchCriteria)
        {
            ValidationResults validationResults = HPFValidator.Validate<AppForeclosureCaseSearchCriteriaDTO>(searchCriteria, Constant.RULESET_APPSEARCH);
            if (validationResults.Count == NUMBER_OF_ERROR_APP_SEARCH_CRITERIA)
                return false;// user doesnt change anything in search criteria
            return true;
        }
        /// <summary>
        /// Search Foreclosure Case
        /// </summary>
        /// <param name="searchCriteria">Search criteria</param>
        /// <returns>Collection of AppForeclosureCaseSearchResult</returns>
        public AppForeclosureCaseSearchResultDTOCollection AppSearchforeClosureCase(AppForeclosureCaseSearchCriteriaDTO searchCriteria)
        {            
            //ExceptionMessageCollection exCol = new ExceptionMessageCollection();
            DataValidationException dataVaidEx = new DataValidationException();
            ValidationResults validationResults = HPFValidator.Validate<AppForeclosureCaseSearchCriteriaDTO>(searchCriteria, Constant.RULESET_CRITERIAVALID);
            
            if (!validationResults.IsValid)
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    string errorCode = string.IsNullOrEmpty(validationResult.Tag) ? "ERROR" : validationResult.Tag;
                    string errorMess = string.IsNullOrEmpty(validationResult.Tag) ? validationResult.Message : ErrorMessages.GetExceptionMessage(validationResult.Tag);
                    dataVaidEx.ExceptionMessages.AddExceptionMessage(errorCode, errorMess);
                }
            }
            else if (!ValidateSearchCriteria(searchCriteria))
                dataVaidEx.ExceptionMessages.AddExceptionMessage(ErrorMessages.ERR0378, ErrorMessages.GetExceptionMessage(ErrorMessages.ERR0378));
          
            if (dataVaidEx.ExceptionMessages.Count > 0)
                throw dataVaidEx;

            return ForeclosureCaseDAO.CreateInstance().AppSearchForeclosureCase(searchCriteria);            
        }
        /// <summary>
        /// Get ForeclosureCase to display on the detail page
        /// </summary>
        /// <param name="fcId">Foreclosure Case ID</param>
        /// <returns>ForeclosureCaseDTO , null for not found</returns>
        public ForeclosureCaseDTO GetForeclosureCase(int? fcId)
        {
            return ForeclosureCaseDAO.CreateInstance().GetForeclosureCase(fcId);
        }
        public int? UpdateForeclosureCase(ForeclosureCaseDTO foreclosureCase)
        {
            return ForeclosureCaseDAO.CreateInstance().UpdateAppForeclosureCase(foreclosureCase);
        }
        private void AppThrowMissingRequiredFieldsException(Collection<string> collection)
        {
            DataValidationException pe = new DataValidationException();
            foreach (string obj in collection)
            {
                ExceptionMessage em = new ExceptionMessage();
                em.Message = obj;
                pe.ExceptionMessages.Add(em);
            }
            throw pe;
        }
        public void ResendToServicer(ForeclosureCaseDTO foreclosureCase)
        {
            var fcId = foreclosureCase.FcId;
            try
            {
                var queue = new HPFSummaryQueue();
                queue.SendACompletedCaseToQueue(fcId);
            }
            catch
            {

                var QUEUE_ERROR_MESSAGE = "Fail to push completed case into Queue : " + fcId;
                //Log
                Logger.Write(QUEUE_ERROR_MESSAGE, Constant.DB_LOG_CATEGORY);
                //Send E-mail to support
                var hpfSupportEmail = HPFConfigurationSettings.HPF_SUPPORT_EMAIL;
                var mail = new HPFSendMail
                {
                    To = hpfSupportEmail,
                    Subject = QUEUE_ERROR_MESSAGE
                };
                mail.Send();
                //
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="critera"></param>
        /// <returns>Number of successful cases have been sent</returns>
        public int SendSummariesToServicer(AppSummariesToServicerCriteriaDTO critera, string createdUser)
        {
            string FcIdList = "";
            int count = 0;
            //TODO:Invalidate data
            DataValidationException dataVaidEx = new DataValidationException();
            ValidationResults validationResults = HPFValidator.Validate<AppSummariesToServicerCriteriaDTO>(critera);

            if (!validationResults.IsValid)
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    string errorCode = string.IsNullOrEmpty(validationResult.Tag) ? "ERROR" : validationResult.Tag;
                    string errorMess = string.IsNullOrEmpty(validationResult.Tag) ? validationResult.Message : ErrorMessages.GetExceptionMessage(validationResult.Tag);
                    dataVaidEx.ExceptionMessages.AddExceptionMessage(errorCode, errorMess);
                }
                throw dataVaidEx;
            }
            //TODO: call Search cases and proccess
            int[] fcIds = ForeclosureCaseDAO.CreateInstance().FCSearchToSendSummaries(critera);
            foreach (int fc in fcIds)
            {
                SummaryReportBL.Instance.SendCompletedCaseSummary(fc);
                if (FcIdList.Length == 0)
                    FcIdList += fc;
                else
                    FcIdList += ("," + fc);

                count++;
            }

            //TODDO: insert info into log table
            ServicerDTOCollection servicers = LookupDataBL.Instance.GetServicers();
            ServicerDTO servicer = servicers.GetServicerById(critera.ServicerId);
            AdminTaskLogDTO adminLog = new AdminTaskLogDTO();
            adminLog.SetInsertTrackingInformation(createdUser);
            adminLog.TaskName = Constant.ADMIN_TASK_SEND_SUMMARIES;
            adminLog.TaskNotes = "servicer name = " + servicer.ServicerName +
                                ", delivery method = " + servicer.SummaryDeliveryMethod +
                                ", start date = " + critera.StartDt.Value.ToShortDateString() +
                                " and end date = " + critera.EndDt.Value.ToShortDateString();            
            adminLog.RecordCount = count;
            adminLog.FcIdList = FcIdList;

            AdminTaskLogDAO.Instance.InsertAdminTaskLog(adminLog);

            return count;
        }

        public int MarkDuplicateCases(Stream excelStream, string createdUser)
        {
            string FcIdList = "";
            int count = 0;
            DataValidationException dataVaidEx = new DataValidationException();

            if (excelStream == null || excelStream.Length == 0)
            {
                dataVaidEx.ExceptionMessages.AddExceptionMessage("ERROR","Excel file content fc id required to process.");
                throw dataVaidEx;
            }
            //TODO: Mark Duplicate cases
            DataSet dataSet = ExcelFileReader.Read(excelStream, Constant.EXCEL_DUPLICATE_FC_TAB_NAME);
            DataTable fileContent = dataSet.Tables[0];
            foreach (DataRow row in fileContent.Rows)
            {
                int fcId;
                if (!int.TryParse(row[0].ToString(), out fcId))
                {
                    dataVaidEx.ExceptionMessages.AddExceptionMessage("ERROR", "--Invalid fc id in row " + (count + 1));
                    throw dataVaidEx;
                }

                if (FcIdList.Length == 0)
                    FcIdList += fcId.ToString();
                else
                    FcIdList += ("," + fcId.ToString());

                //count++;
            }
            
            count = ForeclosureCaseDAO.CreateInstance().MarkDuplicateCases(FcIdList);
            
            //TODDO: insert info into log table            
            AdminTaskLogDTO adminLog = new AdminTaskLogDTO();
            adminLog.SetInsertTrackingInformation(createdUser);
            adminLog.TaskName = Constant.ADMIN_TASK_MARK_DUPLICATES;
            
            adminLog.RecordCount = count;
            adminLog.FcIdList = FcIdList;

            AdminTaskLogDAO.Instance.InsertAdminTaskLog(adminLog);
            
            return count;
        }
    }
}
