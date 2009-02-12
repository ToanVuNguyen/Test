using System;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Services.Protocols;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.WebServices
{
    /// <summary>
    /// Summary description for AgencyWebService
    /// </summary>
    [WebService(Namespace = "https://www.homeownershopenetwork.org")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]    
    public class AgencyWebService : BaseAgencyWebService
    {
        [WebMethod]
        [SoapHeader("Authentication", Direction = SoapHeaderDirection.In)]
        public ForeclosureCaseSaveResponse SaveForeclosureCase(ForeclosureCaseSaveRequest request)
        {
            var response = new ForeclosureCaseSaveResponse();
            try
            {
                if (IsAuthenticated())//Authentication checking                
                {
                    var workingInstance = ForeclosureCaseSetBL.Instance;
                    response.FcId = workingInstance.SaveForeclosureCaseSet(request.ForeclosureCaseSet);
                    if (workingInstance.WarningMessage != null && workingInstance.WarningMessage.Count != 0)
                    {
                        response.Status = ResponseStatus.Warning;
                        response.Messages = workingInstance.WarningMessage;
                    }                    
                    else
                        response.Status = ResponseStatus.Success;
                }
            }
            catch (AuthenticationException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex);
            }
            catch (DataValidationException Ex)
            {
                response.Status = ResponseStatus.Fail;                
                if (Ex.ExceptionMessages != null && Ex.ExceptionMessages.Count > 0)
                    response.Messages = Ex.ExceptionMessages;
                else
                    response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex);
            }
            catch (DataAccessException Ex)
            {
                response.Status = ResponseStatus.Fail;
                //response.Messages.AddExceptionMessage("Data access error.");
                response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex);
            }            
            catch (DuplicateException Ex)
            {
                response.Status = ResponseStatus.Fail;
                if (Ex.ExceptionMessages != null && Ex.ExceptionMessages.Count > 0)
                    response.Messages = Ex.ExceptionMessages;
                else
                    response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex);
            }
            catch (Exception Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex);
            }
            return response;
        }

        [WebMethod]
        [SoapHeader("Authentication", Direction = SoapHeaderDirection.In)]
        public CallLogRetrieveResponse RetrieveCallLog(CallLogRetrieveRequest request)
        {
            var response = new CallLogRetrieveResponse();
            //            
            try
            {
                if (IsAuthenticated())//Authentication checking
                {
                    CallLogDTO callLogDTO = null;
                    var validCallLodId = ValidateCallLogID(request);
                    if (validCallLodId)
                    {
                        var callLogId = GetCallLogID(request);
                        if (callLogId != 0)
                        {
                            callLogDTO = CallLogBL.Instance.RetrieveCallLog(callLogId);
                        }
                    }
                    if (callLogDTO != null)
                    {
                        var callLogWSDTO = ConvertToCallLogWSDTO(callLogDTO);
                        response.CallLog = callLogWSDTO;
                        response.Status = ResponseStatus.Success;
                    }
                    else
                    {
                        response.Status = ResponseStatus.Warning;
                        response.Messages.AddExceptionMessage("Call Log Id does not exist");
                    }
                }
            }
            catch (AuthenticationException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex);
            }
            catch (DataValidationException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages = Ex.ExceptionMessages;
                HandleException(Ex);
            }
            catch (DataAccessException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage("Data access error.");
                HandleException(Ex);
            }
            catch (Exception Ex)
            {
                response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex);
            }
            return response;
        }
        
        private static bool ValidateCallLogID(CallLogRetrieveRequest request)
        {
            var dataValidationException = new DataValidationException();
            request.callLogId = request.callLogId.Trim();
            if (string.IsNullOrEmpty(request.callLogId))
            {
                dataValidationException.ExceptionMessages.AddExceptionMessage("Call Log Id is required");
                throw dataValidationException;
            }
            var validationResults = HPFValidator.Validate(request);
            if (!validationResults.IsValid)
            {
                dataValidationException.ExceptionMessages.AddExceptionMessage("Call Log Id is invalid");
                throw dataValidationException;
            }
            return true;
        }

        private static int GetCallLogID(CallLogRetrieveRequest request)
        {
            var callLogId = 0;
            if (request.callLogId != string.Empty)
            {
                var sCallLogId = request.callLogId.Replace("HPF", "");
                try
                {
                    callLogId = Convert.ToInt32(sCallLogId);
                }
                catch
                {
                    var dataValidationException = new DataValidationException();
                    dataValidationException.ExceptionMessages.AddExceptionMessage("Call Log Id is invalid");
                    throw dataValidationException;
                }
            }
            return callLogId;
        }        

        private static CallLogWSReturnDTO ConvertToCallLogWSDTO(CallLogDTO sourceObject)
        {
            var destObject = new CallLogWSReturnDTO();
            if (sourceObject.CallId.HasValue)
                destObject.CallId = "HPF" + Convert.ToString(sourceObject.CallId);

            destObject.AuthorizedInd = sourceObject.AuthorizedInd;
            destObject.CallSourceCd = sourceObject.CallSourceCd;            
            destObject.EndDate = sourceObject.EndDate;
            destObject.FinalDispoCd = sourceObject.FinalDispoCd;
            destObject.FirstName = sourceObject.FirstName;
            destObject.HomeownerInd = sourceObject.HomeownerInd;
            destObject.LoanAccountNumber = sourceObject.LoanAccountNumber;
            destObject.LastName = sourceObject.LastName;
            destObject.LoanDelinqStatusCd = sourceObject.LoanDelinqStatusCd;
            destObject.OtherServicerName = sourceObject.OtherServicerName;
            destObject.PowerOfAttorneyInd = sourceObject.PowerOfAttorneyInd;
            destObject.PropZipFull9 = sourceObject.PropZipFull9;
            destObject.PrevAgencyId = sourceObject.PrevAgencyId;
            destObject.ReasonToCall = sourceObject.ReasonToCall;
            destObject.StartDate = sourceObject.StartDate;
            destObject.ServicerId = sourceObject.ServicerId;
            destObject.SelectedAgencyId = sourceObject.SelectedAgencyId;
            destObject.SelectedCounselor = sourceObject.SelectedCounselor;
            destObject.City = sourceObject.City;
            destObject.State = sourceObject.State;
            destObject.NonprofitReferralKeyNum1 = sourceObject.NonprofitReferralKeyNum1;
            destObject.NonprofitReferralKeyNum2 = sourceObject.NonprofitReferralKeyNum2;
            destObject.NonprofitReferralKeyNum3 = sourceObject.NonprofitReferralKeyNum3;
            return destObject;
        }                         
    }
}
