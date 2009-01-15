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
                AddDataValidationException(dataValidationException, errorList);

            errorList = CheckForeignKey(aCallLog);
            if (errorList != null && errorList.Count > 0)
                AddDataValidationException(dataValidationException, errorList);

            if (aCallLog.StartDate > aCallLog.EndDate)
                dataValidationException.ExceptionMessages.AddExceptionMessage("Start date must < End date");

            errorList = CheckDependingCallCenter(aCallLog);
            if (errorList != null && errorList.Count > 0)
                AddDataValidationException(dataValidationException, errorList);

            errorList = CheckDependingServicer(aCallLog);
            if (errorList != null && errorList.Count > 0)
                AddDataValidationException(dataValidationException, errorList);

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

            //not implemented            
            //if (!referenceCode.Validate(ReferenceCode.CallSourceCode, aCallLog.CallSourceCd))
            //    errorList.Add("Call source code is not valid");

            //not implemented
            //if (!referenceCode.Validate(ReferenceCode.FinalDispoCd, aCallLog.FinalDispoCd))
            //    errorList.Add("FinalDispoCd code is not valid");

            if (!referenceCode.Validate(ReferenceCode.LoanDelinquencyStatusCode, aCallLog.LoanDelinqStatusCd))
                errorList.Add("Loan Delinq status code is not valid");
            
            
            return errorList;
        }

        private List<string> CheckForeignKey(CallLogDTO aCallLog)
        {
            return CallLogDAO.Instance.CheckValidForeignKey(aCallLog);
        }

        private List<string> CheckDependingCallCenter(CallLogDTO aCallLog)
        {
            CallCenterDTO callCenter = CallLogDAO.Instance.GetCallCenter(aCallLog);
            if (!callCenter.CallCenterName.ToUpper().Equals(Constant.CALL_CENTER_OTHER.ToUpper()))
            {
                aCallLog.CallCenter = callCenter.CallCenterName;
                return null;
            }
            
            if ((aCallLog.CallCenter == null) || (aCallLog.CallCenter.Trim() == string.Empty) || (aCallLog.CallCenter.Trim().Length <= 4))
            {               
                return null;
            }

            List<string> errorList = new List<string>();
            errorList.Add("Call center max length is 4");
            return errorList;
            
        }

        private List<string> CheckDependingServicer(CallLogDTO aCallLog)
        {
            ServicerDTO servicer = CallLogDAO.Instance.GetServicer(aCallLog);
            if (!servicer.ServicerName.ToUpper().Equals(Constant.SERVICER_OTHER.ToUpper()))            
                return null;

            if ((aCallLog.OtherServicerName == null) 
                || (aCallLog.OtherServicerName.Trim() == string.Empty) 
                || (aCallLog.OtherServicerName.Trim().Length <= 50))
                return null;

            List<string> errorList = new List<string>();
            errorList.Add("Other servicer name max length is 50");
            return errorList;
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
