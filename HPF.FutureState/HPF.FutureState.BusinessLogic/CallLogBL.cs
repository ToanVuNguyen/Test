using HPF.FutureState.Common.DataTransferObjects.WebServices;
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
            aCallLog.SetInsertTrackingInformation(aCallLog.CcAgentIdKey);

            ExceptionMessageCollection exceptionMessages = new ExceptionMessageCollection();
            ValidationResults validationResults = HPFValidator.Validate<CallLogDTO>(aCallLog);
            DataValidationException dataValidationException = new DataValidationException();
            if (!validationResults.IsValid)
            {

                foreach (ValidationResult result in validationResults)
                {
                    string errorCode = string.IsNullOrEmpty(result.Tag) ? "ERROR" : result.Tag;
                    string errorMess = string.IsNullOrEmpty(result.Tag) ? result.Message : ErrorMessages.GetExceptionMessageCombined(result.Tag);
                    dataValidationException.ExceptionMessages.AddExceptionMessage(errorCode, errorMess );
                }
                //throw dataValidationException;
            }

            dataValidationException.ExceptionMessages.Add(CheckValidCodeForCallLog(aCallLog));
            //List<string> errorList = CheckValidCodeForCallLog(aCallLog);
            //if (errorList != null && errorList.Count > 0)
            //    ThrowDataValidationException(errorList);

            dataValidationException.ExceptionMessages.Add(CheckForeignKey(aCallLog));
            //errorList = CheckForeignKey(aCallLog);
            //if (errorList != null && errorList.Count > 0)
            //    ThrowDataValidationException(errorList);

            
            if (aCallLog.StartDate > aCallLog.EndDate)
                dataValidationException.ExceptionMessages.AddExceptionMessage("Start date must < End date");

            dataValidationException.ExceptionMessages.Add(CheckDependingCallCenter(aCallLog));
            //errorList = CheckDependingCallCenter(aCallLog);
            //if (errorList != null && errorList.Count > 0)
            //    ThrowDataValidationException(errorList);

            dataValidationException.ExceptionMessages.Add(CheckDependingServicer(aCallLog));
            //errorList = CheckDependingServicer(aCallLog);
            //if (errorList != null && errorList.Count > 0)
            //    ThrowDataValidationException(errorList);

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

        private ExceptionMessageCollection CheckValidCodeForCallLog(CallLogDTO aCallLog)
        {
            ReferenceCodeValidatorBL referenceCode = new ReferenceCodeValidatorBL();
            ExceptionMessageCollection errorList = new ExceptionMessageCollection();            
                      
            if (!referenceCode.Validate(ReferenceCode.CALL_SOURCE_CODE, aCallLog.CallSourceCd))
                errorList.Add(new ExceptionMessage(){ErrorCode="ERROR", Message = "Call source code is not valid"});

            if (!referenceCode.Validate(ReferenceCode.FINAL_DISPO_CD, aCallLog.FinalDispoCd))
                errorList.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "FinalDispoCd code is not valid" });

            if (!referenceCode.Validate(ReferenceCode.LOAN_DELINQUENCY_STATUS_CODE, aCallLog.LoanDelinqStatusCd))
                errorList.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Loan Delinq status code is not valid" });
            
            
            return errorList;
        }

        private ExceptionMessageCollection CheckForeignKey(CallLogDTO aCallLog)
        {
            Dictionary<string, int> idList = CallLogDAO.Instance.GetForeignKey(aCallLog);
            int callCenterID = idList["CallCenterID"];
            //int isValidCCAgentIdKey = 1;
            int prevAgencyID = idList["PrevAgencyID"];
            //int isValidSelectedAgencyId = 1;
            int servicerID = idList["ServicerID"];
            ExceptionMessageCollection errorList = new ExceptionMessageCollection();
            if (callCenterID == 0)
                errorList.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "CallCenterID does not exist"});
            if (aCallLog.PrevAgencyId.HasValue && prevAgencyID == 0)
                errorList.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "prevAgencyID does not exist"});
            if (aCallLog.ServicerId.HasValue && servicerID == 0)
                errorList.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "ServicerId does not exist" });
            return errorList;
        }

        private ExceptionMessageCollection CheckDependingCallCenter(CallLogDTO aCallLog)
        {
            CallCenterDTO callCenter = CallLogDAO.Instance.GetCallCenter(aCallLog);
            ExceptionMessageCollection errorList = new ExceptionMessageCollection();

            if (callCenter != null)
            {


                if (!callCenter.CallCenterName.ToUpper().Equals(Constant.CALL_CENTER_OTHER.ToUpper()))
                {
                    aCallLog.CallCenter = callCenter.CallCenterName;
                    return errorList;
                }

                if ((aCallLog.CallCenter == null) || (aCallLog.CallCenter.Trim() == string.Empty))
                {
                    errorList.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Call center is required" });
                    return errorList;
                }

                if (aCallLog.CallCenter.Trim().Length <= 4)
                    return errorList;

                errorList.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Call center max length is 4" });
            }
            return errorList;
            
        }

        private ExceptionMessageCollection CheckDependingServicer(CallLogDTO aCallLog)
        {
            ServicerDTO servicer = CallLogDAO.Instance.GetServicer(aCallLog);
            ExceptionMessageCollection errorList = new ExceptionMessageCollection();


            if (servicer != null)
            {
                if (!servicer.ServicerName.ToUpper().Equals(Constant.SERVICER_OTHER.ToUpper()))
                    return errorList;

                if ((aCallLog.OtherServicerName == null)
                    || (aCallLog.OtherServicerName.Trim() == string.Empty))
                {
                    errorList.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Other servicer name is required" });
                    return errorList;
                }

                if (aCallLog.OtherServicerName.Trim().Length <= 50)
                    return errorList;

                errorList.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Other servicer name max length is 50" });
            }
            return errorList;
        }

        //private void ThrowDataValidationException(List<string> errorList)
        //{
        //    DataValidationException pe = new DataValidationException();
        //    foreach (string obj in errorList)
        //    {
        //        ExceptionMessage em = new ExceptionMessage();
        //        em.Message = obj;
        //        pe.ExceptionMessages.Add(em);
        //    }
        //    throw pe;
        //}

    }
}
