﻿using System;
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
        public void SaveForeClosureCaseSet(ForeClosureCaseSetDTO foreClosureCaseSet)
        {
            //Validation here     
       
            var foreClosureCaseSetDAO = ForeClosureCaseSetDAO.CreateInstance();
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
        public ForeClosureCaseSearchResult SearchForeClosureCase(ForeClosureCaseSearchCriteriaDTO searchCriteria)
        {
            ForeClosureCaseSearchResult searchResult = new ForeClosureCaseSearchResult();
            
            ExceptionMessageCollection exceptionMessages = new ExceptionMessageCollection();
            //exceptionMessages = HPFValidator.ValidateToExceptionMessage<ForeClosureCaseSearchCriteriaDTO>(searchCriteria);

            //HPFValidator is not complete yet, it does not get the content of the message
            //so use the system validator just for testing
            Validator<ForeClosureCaseSearchCriteriaDTO> validator = ValidationFactory.CreateValidator<ForeClosureCaseSearchCriteriaDTO>();
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
                searchResult = ForeClosureCaseSetDAO.CreateInstance().SearchForeClosureCase(searchCriteria);
            }

            return searchResult;
        }
        #endregion
    }
}
