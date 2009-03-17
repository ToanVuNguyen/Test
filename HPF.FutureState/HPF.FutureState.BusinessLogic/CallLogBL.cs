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
        public int? InsertCallLog(CallLogDTO aCallLog)
        {
            aCallLog.SetInsertTrackingInformation(aCallLog.CcAgentIdKey);
            
            DataValidationException dataValidationException = new DataValidationException();

            dataValidationException.ExceptionMessages.Add(CheckDataValidation(aCallLog));

            dataValidationException.ExceptionMessages.Add(CheckTimePart(aCallLog));
            
            dataValidationException.ExceptionMessages.Add(CheckValidCodeForCallLog(aCallLog));

            dataValidationException.ExceptionMessages.Add(CheckForeignKey(aCallLog));
            
            //dataValidationException.ExceptionMessages.Add(CheckDependingCallCenter(aCallLog));

            dataValidationException.ExceptionMessages.Add(CheckDependingServicer(aCallLog));

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

        private ExceptionMessageCollection CheckDataValidation(CallLogDTO aCallLog)
        {
            ExceptionMessageCollection errorList = new ExceptionMessageCollection();
            ValidationResults validationResults = HPFValidator.Validate<CallLogDTO>(aCallLog);
            if (!validationResults.IsValid)
            {

                foreach (ValidationResult result in validationResults)
                {
                    string errorCode = string.IsNullOrEmpty(result.Tag) ? "ERROR" : result.Tag;
                    string errorMess = string.IsNullOrEmpty(result.Tag) ? result.Message : ErrorMessages.GetExceptionMessage(result.Tag);
                    errorList.AddExceptionMessage(errorCode, errorMess);
                }
            }
            return errorList;
        }

        private ExceptionMessageCollection CheckValidCodeForCallLog(CallLogDTO aCallLog)
        {
            ReferenceCodeValidatorBL referenceCode = new ReferenceCodeValidatorBL();
            ExceptionMessageCollection errorList = new ExceptionMessageCollection();            
                      
            if (!referenceCode.Validate(ReferenceCode.CALL_SOURCE_CODE, aCallLog.CallSourceCd))
                errorList.AddException(ErrorMessages.ERR0358);

            if (!referenceCode.Validate(ReferenceCode.FINAL_DISPO_CD, aCallLog.FinalDispoCd))
                errorList.AddException(ErrorMessages.ERR0355);

            if (!referenceCode.Validate(ReferenceCode.LOAN_DELINQUENCY_STATUS_CODE, aCallLog.LoanDelinqStatusCd))
                errorList.AddException(ErrorMessages.ERR0359);
            
            
            return errorList;
        }

        private ExceptionMessageCollection CheckForeignKey(CallLogDTO aCallLog)
        {
            Dictionary<string, int?> idList = new Dictionary<string, int?>();
            
            idList = CallLogDAO.Instance.GetForeignKey(aCallLog);
            int? callCenterID = (idList["CallCenterID"].HasValue) ? idList["CallCenterID"].Value : 0;
            
            int? prevAgencyID = (idList["PrevAgencyID"].HasValue) ? idList["PrevAgencyID"].Value : 0;
            
            int? servicerID = (idList["ServicerID"].HasValue) ? idList["ServicerID"].Value : 0;

            int? selectedAgencyId = (idList["SelectedAgencyID"].HasValue) ? idList["SelectedAgencyID"].Value : 0; 
            
            ExceptionMessageCollection errorList = new ExceptionMessageCollection();
            if (aCallLog.CallCenterID.HasValue && callCenterID == 0)
                errorList.AddException(ErrorMessages.ERR0901);
            if (aCallLog.PrevAgencyId.HasValue && prevAgencyID == 0)
                errorList.AddException(ErrorMessages.ERR0360);
            if (aCallLog.ServicerId.HasValue && servicerID == 0)
                errorList.AddException(ErrorMessages.ERR0361);
            if (aCallLog.SelectedAgencyId.HasValue && selectedAgencyId == 0)
                errorList.AddException(ErrorMessages.ERR0362);
            return errorList;
        }

        private ExceptionMessageCollection CheckDependingCallCenter(CallLogDTO aCallLog)
        {
            CallCenterDTO callCenter = CallLogDAO.Instance.GetCallCenter(aCallLog);
            ExceptionMessageCollection errorList = new ExceptionMessageCollection();

            if (callCenter != null)
            {
                if (string.IsNullOrEmpty(callCenter.CallCenterName))
                {
                    aCallLog.CallCenter = callCenter.CallCenterName;
                    return errorList;
                }

                if (!callCenter.CallCenterName.ToUpper().Equals(Constant.CALL_CENTER_OTHER.ToUpper()))
                {
                    aCallLog.CallCenter = callCenter.CallCenterName;
                    return errorList;
                }

                if (string.IsNullOrEmpty(aCallLog.CallCenter))
                {
                    errorList.AddException(ErrorMessages.ERR0900);
                    return errorList;
                }

                if (aCallLog.CallCenter.Trim().Length <= 4)
                    return errorList;

                errorList.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Call center max length is 4" });
            }
            return errorList;
            
        }

        private ExceptionMessageCollection CheckTimePart(CallLogDTO aCallLog)
        {
            ExceptionMessageCollection errorList = new ExceptionMessageCollection();
            if (aCallLog.StartDate.HasValue && aCallLog.EndDate.HasValue)
            {                                
                TimeSpan sp = new TimeSpan();
                if (aCallLog.StartDate.Value.TimeOfDay == sp && aCallLog.EndDate.Value.TimeOfDay == sp)
                    errorList.AddException(ErrorMessages.ERR0354);
            }            
            return errorList;
        }

        private ExceptionMessageCollection CheckDependingServicer(CallLogDTO aCallLog)
        {
            ServicerDTO servicer = CallLogDAO.Instance.GetServicer(aCallLog);
            ExceptionMessageCollection errorList = new ExceptionMessageCollection();


            if (servicer != null)
            {
                if (string.IsNullOrEmpty(servicer.ServicerName))
                    return errorList;

                if (!servicer.ServicerName.ToUpper().Equals(Constant.SERVICER_OTHER.ToUpper()))
                    return errorList;

                if (string.IsNullOrEmpty(aCallLog.OtherServicerName))
                {
                    errorList.AddException(ErrorMessages.ERR0357);
                    return errorList;
                }

                if (aCallLog.OtherServicerName.Trim().Length <= 50)
                    return errorList;

                errorList.Add(new ExceptionMessage() { ErrorCode = "ERROR", Message = "Other servicer name max length is 50" });
            }
            return errorList;
        }       

    }
}
