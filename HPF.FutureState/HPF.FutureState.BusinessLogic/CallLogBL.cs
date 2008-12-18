using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;


using Microsoft.Practices.EnterpriseLibrary.Validation;
using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.BusinessLogic
{
    public class CallLogBL : BaseBusinessLogic, ICallLogBL
    {
        private static readonly CallLogBL instance = new CallLogBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CallLogBL Instance
        {
            get
            {
                return instance;
            }
        }
       
        protected CallLogBL()
        {
            
        }

        #region Implementation of ICallLogBL

        /// <summary>
        /// Insert a CallLog
        /// </summary>
        /// <param name="aCallLog"></param>
        /// <returns>A result object after inserting a call log</returns>
        public CallLogInsertResult InsertCallLog(CallLogDTO aCallLog)
        {
            ////HPFValidator.ValidateToExceptionMessage<CallLogDTO>(aCallLog);
            //ExceptionMessageCollection exceptionMessages = new ExceptionMessageCollection();
            //exceptionMessages = HPFValidator.ValidateToExceptionMessage<ForeclosureCaseSearchCriteriaDTO>(searchCriteria);

            ////HPFValidator is not complete yet, it does not get the content of the message
            ////so use the system validator just for testing
            Validator<CallLogDTO> validator = ValidationFactory.CreateValidator<CallLogDTO>("Default");
            ValidationResults validationResults = validator.Validate(aCallLog);
            CallLogInsertResult insertResult = new CallLogInsertResult();
            if (!validationResults.IsValid)
            {
                insertResult.Messages = new DataValidationException();
            //    //searchResult.Messages.ExceptionMessages = exceptionMessages;
                foreach (ValidationResult result in validationResults)
                {
                    insertResult.Messages.ExceptionMessages.AddExceptionMessage(0, result.Message);
                }
            }
            else
            {
                insertResult.CallLogID = "HPF_" + CallLogDAO.Instance.InsertCallLog(aCallLog);
            }

            //return searchResult;
            return insertResult;
            
        }       

        /// <summary>
        /// Get a CallLog by CallLogId
        /// </summary>
        /// <param name="callLogId">CallLogId</param>
        /// <returns></returns>
        public CallLogDTO RetrieveCallLog(int callLogId)
        {
            return CallLogDAO.Instance.ReadCallLog(callLogId);
        }
        #endregion        
    }
}
