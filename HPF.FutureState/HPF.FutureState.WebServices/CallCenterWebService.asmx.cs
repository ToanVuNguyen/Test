using System;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Services.Protocols;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.WebServices
{
    /// <summary>
    /// Summary description for CallCenterService
    /// </summary>
    [WebService(Namespace = "https://www.homeownershopenetwork.org")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class CallCenterService : BaseCallCenterWebService
    {
        [WebMethod]
        [SoapHeader("Authentication", Direction = SoapHeaderDirection.In)]
        public CallLogInsertResponse SaveCallLog(CallLogInsertRequest request)
        {
            var response = new CallLogInsertResponse();
            try
            {
                if (IsAuthenticated())//Authentication checking
                {
                    var callLogDTO = ConvertToCallLogDTO(request.CallLog);
                    callLogDTO.CallCenterID = CurrentCallCenterID;
                    response.CallLogID = "HPF" + CallLogBL.Instance.InsertCallLog(callLogDTO);                    
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
                response.Messages.AddExceptionMessage("Data access error.");
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

        private CallLogDTO ConvertToCallLogDTO(CallLogWSDTO sourceObject)
        {
            var destObject = new CallLogDTO();
            if (sourceObject.CallId != null)
            {
                sourceObject.CallId = sourceObject.CallId.Replace("HPF", "");
                int id;
                int.TryParse(sourceObject.CallId, out id);
                destObject.CallId = id;
            }
            //
            destObject.AuthorizedInd = sourceObject.AuthorizedInd;
            destObject.CallCenter = sourceObject.CallCenter;
            destObject.CallSourceCd = sourceObject.CallSourceCd;
            destObject.CallCenterID = sourceObject.CallCenterID;
            destObject.CcAgentIdKey = sourceObject.CcAgentIdKey;
            destObject.CcCallKey = sourceObject.CcCallKey;
            destObject.DNIS = sourceObject.DNIS;
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
            destObject.ScreenRout = sourceObject.ScreenRout;
            destObject.TransNumber = sourceObject.TransNumber;
            destObject.City = sourceObject.City;
            destObject.State = sourceObject.State;
            destObject.NonprofitReferralKeyNum1 = sourceObject.NonprofitReferralKeyNum1;
            destObject.NonprofitReferralKeyNum2 = sourceObject.NonprofitReferralKeyNum2;
            destObject.NonprofitReferralKeyNum3 = sourceObject.NonprofitReferralKeyNum3;
            return destObject;
        }
    }
}
