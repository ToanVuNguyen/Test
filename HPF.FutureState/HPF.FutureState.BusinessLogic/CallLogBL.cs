using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;


using Microsoft.Practices.EnterpriseLibrary.Validation;
using HPF.FutureState.Common.Utils.DataValidator;

using System;

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
        /// <returns>return a calllog id after inserting</returns>
        public int InsertCallLog(CallLogDTO aCallLog)
        {
            aCallLog.SetInsertTrackingInformation(aCallLog.CreateUserId);

            ExceptionMessageCollection exceptionMessages = new ExceptionMessageCollection();
            ValidationResults validationResults = HPFValidator.Validate<CallLogDTO>(aCallLog);
            if (!validationResults.IsValid)
            {
                int i = 0;
                DataValidationException dataValidationException = new DataValidationException();
                foreach (ValidationResult result in validationResults)
                {
                    dataValidationException.ExceptionMessages.AddExceptionMessage(++i, result.Message);
                }
                throw dataValidationException;

            }


            return CallLogDAO.Instance.InsertCallLog(aCallLog);

            
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
