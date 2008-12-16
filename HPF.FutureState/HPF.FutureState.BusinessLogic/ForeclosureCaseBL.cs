using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;

using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace HPF.FutureState.BusinessLogic
{
    public class ForeclosureCaseBL : BaseBusinessLogic, IForclosureCaseBL
    {
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

        #region Implementation of IForclosureCaseBL

        /// <summary>
        /// Save a ForeClosureCase
        /// </summary>
        /// <param name="foreClosureCaseSet">ForeClosureCaseSetDTO</param>
        public void SaveForeClosureCaseSet(ForeclosureCaseSetDTO foreClosureCaseSet)
        {
            //Validation here     
       
            var foreClosureCaseSetDAO = ForeclosureCaseSetDAO.CreateInstance();
            //
            try
            {
                foreClosureCaseSetDAO.Begin();
                //
                //Business process here
                //
                foreClosureCaseSetDAO.Commit();
            }
            catch (Exception)
            {                
                foreClosureCaseSetDAO.Cancel();
                throw;
            }            
        }

        /// <summary>
        /// return ForeclosureCase search result 
        /// </summary>
        /// <param name="searchCriteria">search criteria</param>
        /// <returns>collection of ForeclosureCaseWSDTO and collection of exception messages if there are any</returns>
        public ForeclosureCaseSearchResult SearchForeClosureCase(ForeclosureCaseSearchCriteriaDTO searchCriteria)
        {
            ForeclosureCaseSearchResult searchResult = new ForeclosureCaseSearchResult();
            
            ExceptionMessageCollection exceptionMessages = new ExceptionMessageCollection();
            //exceptionMessages = HPFValidator.ValidateToExceptionMessage<ForeClosureCaseSearchCriteriaDTO>(searchCriteria);

            //HPFValidator is not complete yet, it does not get the content of the message
            //so use the system validator just for testing
            Validator<ForeclosureCaseSearchCriteriaDTO> validator = ValidationFactory.CreateValidator<ForeclosureCaseSearchCriteriaDTO>("Default");
            ValidationResults validationResults = validator.Validate(searchCriteria);
            //if (exceptionMessages != null || exceptionMessages.Count > 0)
            if (!validationResults.IsValid)
            {
                searchResult.Messages = new DataValidationException();
                //searchResult.Messages.ExceptionMessages = exceptionMessages;
                foreach (ValidationResult result in validationResults)
                {
                    searchResult.Messages.ExceptionMessages.AddExceptionMessage(0, result.Message);
                }
            }
            else
            {
                searchResult = ForeclosureCaseSetDAO.CreateInstance().SearchForeClosureCase(searchCriteria);
            }

            return searchResult;
        }
        #endregion

        #region functions to serve SaveForeClosureCaseSet
        /// <summary>
        /// Min request validate the fore closure case
        /// </summary>
        bool RequireFieldsValidation(ForeclosureCaseSetDTO  foreclosureCaseSet)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check ForeClosureCase Id is existed or not
        /// </summary>
        bool CheckValidFCIdForAgency(int fcId, int agencyId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check Fore Closure Case is over one year
        /// </summary>
        bool CheckInactiveCase(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check Duplicated Fore Closure Case
        /// </summary>
        bool CheckDuplicateCase(ForeclosureCaseDTO foreclosureCase)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check existed AgencyId and Case number
        /// </summary>
        bool CheckExistingAgencyIdAndCaseNumber(string agencyId, string caseNumner)
        {
            throw new NotImplementedException();
        }

        bool CheckMiscErrorException(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update the Fore Closure Case
        /// </summary>
        void UpdateForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update the Fore Closure Case
        /// </summary>
        void InsertForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Foreclosure case basing on its fc_id
        /// </summary>
        /// <param name="fc_id">id for a ForeclosureCase</param>
        /// <returns>object of ForeclosureCase </returns>
        ForeclosureCaseDTO GetForeclosureCase(int fc_id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
