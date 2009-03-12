using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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
            AppForeclosureCaseSearchResultDTOCollection result = new AppForeclosureCaseSearchResultDTOCollection();
            ExceptionMessageCollection exCol = new ExceptionMessageCollection();
            DataValidationException dataVaidEx = new DataValidationException();
            ValidationResults validationResults = HPFValidator.Validate<AppForeclosureCaseSearchCriteriaDTO>(searchCriteria, Constant.RULESET_CRITERIAVALID);
            if (!ValidateSearchCriteria(searchCriteria))
            {
                ExceptionMessage ex = new ExceptionMessage();
                ex.Message = ErrorMessages.GetExceptionMessageCombined("ERR0378");
                dataVaidEx.ExceptionMessages.Add(ex);
            }
            if (!validationResults.IsValid)
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    string errorCode = string.IsNullOrEmpty(validationResult.Tag) ? "ERROR" : validationResult.Tag;
                    string errorMess = string.IsNullOrEmpty(validationResult.Tag) ? validationResult.Message : ErrorMessages.GetExceptionMessageCombined(validationResult.Tag);
                    dataVaidEx.ExceptionMessages.AddExceptionMessage(errorCode, errorMess);
                }
            }
            if(dataVaidEx.ExceptionMessages.Count>0)
                throw dataVaidEx;
            result = ForeclosureCaseDAO.CreateInstance().AppSearchForeclosureCase(searchCriteria);
            return result;
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

                var QUEUE_ERROR_MESSAGE = "Fail to push completed case to Queue : " + fcId;
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


    }
}
