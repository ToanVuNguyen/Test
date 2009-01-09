using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;


using Microsoft.Practices.EnterpriseLibrary.Validation;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common;

using System.Collections.Generic;
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
            DataValidationException dataValidationException = new DataValidationException();
            if (!validationResults.IsValid)
            {                
                
                foreach (ValidationResult result in validationResults)
                {
                    dataValidationException.ExceptionMessages.AddExceptionMessage(result.Message);
                }                
            }

            List<string> errorList = CheckValidCodeForCallLog(aCallLog);
            if (errorList != null && errorList.Count > 0)
            {
                AddDataValidationException(dataValidationException, errorList);
            }

            errorList = CheckForeignKey(aCallLog);
            if (errorList != null && errorList.Count > 0)
            {
                AddDataValidationException(dataValidationException, errorList);
            }

            if (aCallLog.StartDate > aCallLog.EndDate)
                dataValidationException.ExceptionMessages.AddExceptionMessage("Start date must < End date");

            if (dataValidationException.ExceptionMessages.Count > 0)
                throw dataValidationException;

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

        private List<string> CheckValidCodeForCallLog(CallLogDTO aCallLog)
        {
            ReferenceCodeValidatorBL referenceCode = new ReferenceCodeValidatorBL();
            List<string> errorList = new List<string>();

            if (!referenceCode.Validate(ReferenceCode.CallCenterCode, aCallLog.CallSourceCd))
                errorList.Add("Call source code is not valid");

            if (!referenceCode.Validate(ReferenceCode.LoanDelinquencyStatusCode, aCallLog.LoanDelinqStatusCd))
                errorList.Add("Loan Delinq status code is not valid");
            
            
            return errorList;
        }

        private List<string> CheckForeignKey(CallLogDTO aCallLog)
        {
            return CallLogDAO.Instance.CheckValidForeignKey(aCallLog);
        }

        private void AddDataValidationException(DataValidationException pe, List<string> errorList)
        {             
            foreach (string obj in errorList)
            {
                ExceptionMessage em = new ExceptionMessage();
                em.Message = obj;
                pe.ExceptionMessages.Add(em);
            }            
        }

    }
}
