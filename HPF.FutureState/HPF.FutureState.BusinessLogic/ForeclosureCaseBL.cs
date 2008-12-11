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

        public ForeClosureCaseSearchResult SearchForeClosureCase(ForeClosureCaseSearchCriteriaDTO searchCriteria)
        {
            ForeClosureCaseSearchResult searchResult = new ForeClosureCaseSearchResult();
            
            ExceptionMessageCollection exCollection = new ExceptionMessageCollection();
            Validator<ForeClosureCaseSearchCriteriaDTO> validator = ValidationFactory.CreateValidator<ForeClosureCaseSearchCriteriaDTO>();
            ValidationResults results = validator.Validate(searchCriteria);
            if (!results.IsValid)
            {
                searchResult.Messages = new DataValidationException();

                foreach (ValidationResult result in results)
                {
                    searchResult.Messages.ExceptionMessages.AddExceptionMessage(0, result.Message);
                }
            }
            else
            {
                searchResult = ForeClosureCaseSetDAO.CreateInstance().SearchForeClosureCase(searchCriteria);
            }
//            if (exCollection == null || exCollection.Count == 0)
//            {
//                searchResult = ForeClosureCaseSetDAO.CreateInstance().SearchForeClosureCase(searchCriteria);                
//            }
//            else
//            {
//                searchResult.Messages.ExceptionMessages = exCollection;
//            }
            return searchResult;
        }
        #endregion
    }
}
