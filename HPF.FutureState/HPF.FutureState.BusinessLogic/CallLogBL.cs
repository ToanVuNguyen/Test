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
        public CallLogWSReturnDTOCollection RetrieveICTCallLog(string ICTcallLogId)
        {
            if (string.IsNullOrEmpty(ICTcallLogId) || ICTcallLogId.Length > 40)
            {
                ExceptionMessageCollection errorList = new ExceptionMessageCollection();                
                errorList.AddExceptionMessage(ErrorMessages.ERR0900, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0900));
                DataValidationException ex = new DataValidationException(errorList);
                
                throw ex;
            }
            return CallLogDAO.Instance.ICTReadCallLog(ICTcallLogId);
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
                    string errorMess = string.IsNullOrEmpty(result.Tag) ? result.Message : ErrorMessages.GetExceptionMessageCombined(result.Tag);
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
                errorList.AddExceptionMessage(ErrorMessages.ERR0358, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0358));

            if (!referenceCode.Validate(ReferenceCode.FINAL_DISPO_CD, aCallLog.FinalDispoCd))
                errorList.AddExceptionMessage(ErrorMessages.ERR0355, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0355));

            if (!referenceCode.Validate(ReferenceCode.LOAN_DELINQUENCY_STATUS_CODE, aCallLog.LoanDelinqStatusCd))
                errorList.AddExceptionMessage(ErrorMessages.ERR0359, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0359));

            if (!referenceCode.Validate(ReferenceCode.MHA_LOAN_LOOKUP_CODE, aCallLog.LoanLookupCd))
                errorList.AddExceptionMessage("ERROR","ERROR--An invalid code was provided for LoanLookupCd.");
            if (!referenceCode.Validate(ReferenceCode.SERVICER_COMPLAINT_CODE, aCallLog.ServicerComplaintCd))
                errorList.AddExceptionMessage("ERROR", "ERROR--An invalid code was provided for ServicerComplaintCd.");            
            
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
                errorList.AddExceptionMessage(ErrorMessages.ERR0901, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0901));
            if (aCallLog.PrevAgencyId.HasValue && prevAgencyID == 0)
                errorList.AddExceptionMessage(ErrorMessages.ERR0360, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0360));
            if (aCallLog.ServicerId.HasValue && servicerID == 0)
            {
                string format = ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0361);
                string message = string.Format(format, aCallLog.LoanAccountNumber);
                errorList.AddExceptionMessage(ErrorMessages.ERR0361, message);
            }
            if (aCallLog.SelectedAgencyId.HasValue && selectedAgencyId == 0)
                errorList.AddExceptionMessage(ErrorMessages.ERR0362, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0362));

            //NonProfitReferralDTOCollection nonProfitReferrals = LookupDataBL.Instance.GetNonProfitReffals();

            //if (!string.IsNullOrEmpty(aCallLog.NonprofitReferralKeyNum1) && nonProfitReferrals.GetNonProfitReferral(aCallLog.NonprofitReferralKeyNum1) == null)            
              //  errorList.AddExceptionMessage("An invalid NonprofitReferralKeyNum1 was provided for.");
            //if (!string.IsNullOrEmpty(aCallLog.NonprofitReferralKeyNum2) && nonProfitReferrals.GetNonProfitReferral(aCallLog.NonprofitReferralKeyNum2) == null)
              //  errorList.AddExceptionMessage("An invalid NonprofitReferralKeyNum2 was provided.");
            //if (!string.IsNullOrEmpty(aCallLog.NonprofitReferralKeyNum3) && nonProfitReferrals.GetNonProfitReferral(aCallLog.NonprofitReferralKeyNum3) == null)
              //  errorList.AddExceptionMessage("An invalid NonprofitReferralKeyNum3 was provided.");            

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
                    errorList.AddExceptionMessage(ErrorMessages.ERR0354, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0354));
            }            
            return errorList;
        }

        private ExceptionMessageCollection CheckDependingServicer(CallLogDTO aCallLog)
        {
            ExceptionMessageCollection errorList = new ExceptionMessageCollection();
            ServicerDTO servicer = null;

            if (aCallLog.ServicerId.HasValue)
            {
                servicer = ServicerBL.Instance.GetServicer(aCallLog.ServicerId.Value);

                if (servicer != null && servicer.ServicerID == Constant.SERVICER_OTHER_ID)            
                {                
                    if (string.IsNullOrEmpty(aCallLog.OtherServicerName))                
                        errorList.AddExceptionMessage(ErrorMessages.ERR0357, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0357));                                       
                }
            }

            if (aCallLog.ServicerCAId.HasValue)
            {
                servicer = ServicerBL.Instance.GetServicer(aCallLog.ServicerCAId.Value);

                if (servicer != null && servicer.ServicerID == Constant.SERVICER_OTHER_ID)
                {
                    if (string.IsNullOrEmpty(aCallLog.ServicerCAOtherName))
                        errorList.AddExceptionMessage(ErrorMessages.ERR0400, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0400));
                }
            }

            return errorList;
        }

        public bool GetCall(string callID)
        {
            return CallLogDAO.Instance.GetCall(callID);
        }

        #region WSSearchCall
        private ExceptionMessageCollection ICTCheckDataSearchValidation(CallLogSearchCriteriaDTO searchCriteria)
        {
            ExceptionMessageCollection errorList = new ExceptionMessageCollection();
            ValidationResults checkRequireFields = HPFValidator.Validate<CallLogSearchCriteriaDTO>(searchCriteria, "Default");

            if (checkRequireFields.Count == 3) //check atleast one field is entered
            {
                errorList.AddExceptionMessage(ErrorMessages.ERR0378, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0378));
            }
            else //check the data length
            {
                ValidationResults validationResults = HPFValidator.Validate<CallLogSearchCriteriaDTO>(searchCriteria, Constant.RULESET_LENGTH);
                if (!validationResults.IsValid)
                {

                    foreach (ValidationResult result in validationResults)
                    {
                        string errorCode = string.IsNullOrEmpty(result.Tag) ? "ERROR" : result.Tag;
                        string errorMess = string.IsNullOrEmpty(result.Tag) ? result.Message : ErrorMessages.GetExceptionMessageCombined(result.Tag);
                        errorList.AddExceptionMessage(errorCode, errorMess);
                    }
                }
            }

            return errorList;
        }

        public CallLogWSReturnDTOCollection ICTSearchCall(CallLogSearchCriteriaDTO searchCriteria)
        {
            ExceptionMessageCollection errors = ICTCheckDataSearchValidation(searchCriteria);
            if (errors.Count > 0)
            {
                DataValidationException ex = new DataValidationException(errors);
                throw ex;
            }

            return CallLogDAO.Instance.ICTSearchCallLog(searchCriteria);
        }
        #endregion
    }
}
