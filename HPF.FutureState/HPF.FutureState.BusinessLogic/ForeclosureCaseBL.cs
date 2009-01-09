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

namespace HPF.FutureState.BusinessLogic
{
    public class ForeclosureCaseBL : BaseBusinessLogic
    {
        const int NUMBER_OF_ERROR_APP_SEARCH_CRITERIA = 11;
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
            ValidationResults validationResults = HPFValidator.Validate<AppForeclosureCaseSearchCriteriaDTO>(searchCriteria, "AppSearchRequireCriteria");
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
            Collection<string> ErrorMess = AppRequireFieldValidation(searchCriteria);
            if (!ValidateSearchCriteria(searchCriteria))
            {
                throw new DataValidationException("Please choose argument(s) for at least one search option");
            }
            if (ErrorMess != null)
            {

                AppThrowMissingRequiredFieldsException(ErrorMess);
            }
            result = ForeclosureCaseDAO.CreateInstance().AppSearchForeclosureCase(searchCriteria);
            return result;
        }
        /// <summary>
        /// Get ForeclosureCase to display on the detail page
        /// </summary>
        /// <param name="fcId">Foreclosure Case ID</param>
        /// <returns>ForeclosureCaseDTO , null for not found</returns>
        public ForeclosureCaseDTO GetForeclosureCase(int fcId)
        {
            return ForeclosureCaseDAO.CreateInstance().GetForeclosureCase(fcId);
        }
        public int UpdateForeclosureCase(ForeclosureCaseDTO foreclosureCase)
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

        #region Functions check validate to app foreclosure case
        ///<summary>
        ///1.AgencyCaseID request alpha numberic 
        ///2.ForclosureCaseID request numberic
        ///3.LoanNumber request alpha numberic
        ///4.PropertyZip request 5 digit
        ///5.Last4SSN request 4 digit
        /// </summary>
        Collection<string> AppRequireFieldValidation(AppForeclosureCaseSearchCriteriaDTO searchCriteria)
        {

            Collection<string> msgAppForeclosureCaseSearch = new Collection<string>();
            RequireAppForeclosureCase(searchCriteria, ref msgAppForeclosureCaseSearch, "CriteriaValidation");
            if (msgAppForeclosureCaseSearch.Count == 0)
                return null;
            return msgAppForeclosureCaseSearch;
        }
        void RequireAppForeclosureCase(AppForeclosureCaseSearchCriteriaDTO searchCriteria, ref Collection<string> msg, string ruleset)
        {
            ValidationResults validationResults = HPFValidator.Validate<AppForeclosureCaseSearchCriteriaDTO>(searchCriteria, ruleset);
            foreach (ValidationResult result in validationResults)
            {
                msg.Add(result.Message);
            }
        }

        #endregion
      
       
    }
}
