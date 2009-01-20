using System;
using System.ComponentModel;

using System.Configuration;

using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Validation;
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
    /// Summary description for CallCenterService
    /// </summary>
    [WebService(Namespace = "https://www.homeownershopenetwork.org")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CallCenterService : BaseCallCenterWebService
    {
        #region ICallCenterBL implementation
        [WebMethod]
        [SoapHeader("Authentication", Direction = SoapHeaderDirection.In)]
        public CallLogInsertResponse SaveCallLog(CallLogInsertRequest request)
        {
            var response = new CallLogInsertResponse();            
            try
            {
                if (IsAuthenticated())//Authentication checking
                {                    
                    CallLogDTO callLogDTO = ConvertToCallLogDTO(request.CallLog);
                    callLogDTO.CallCenterID = base.CallCenterID;
                    //callLogDTO.CallCenter = base.CallCenterName;
                    
                    string sCallLogID = "HPF" + CallLogBL.Instance.InsertCallLog(callLogDTO);
                    response.CallLogID = sCallLogID;
                    response.Status = ResponseStatus.Success;                                        
                }
            }
            catch (AuthenticationException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Messages.AddExceptionMessage(Ex.Message);
                ExceptionProcessor.HandleException(Ex);
            }
            catch (DataValidationException Ex)
            {
                response.Status = ResponseStatus.Fail;
                if (Ex.ExceptionMessages != null && Ex.ExceptionMessages.Count > 0)
                    response.Messages = Ex.ExceptionMessages;
                else
                    response.Messages.AddExceptionMessage(Ex.Message);
                ExceptionProcessor.HandleException(Ex);
            }
            catch (DataAccessException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage("Data access error.");
                ExceptionProcessor.HandleException(Ex);
            } 
            catch(Exception Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage(Ex.Message);
                ExceptionProcessor.HandleException(Ex);
            }
            return response;
        }

        
#endregion


        private CallLogDTO ConvertToCallLogDTO(CallLogWSDTO sourceObject)
        {
            CallLogDTO destObject = new CallLogDTO();
            if (sourceObject.CallId != null)
            {
                sourceObject.CallId = sourceObject.CallId.Replace("HPF", "");
                int id = 0;
                int.TryParse(sourceObject.CallId, out id);
                destObject.CallId = id;
            }

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

            destObject.CreateDate = sourceObject.CreateDate;
            destObject.CreateUserId = sourceObject.CreateUserId;
            destObject.CreateAppName = sourceObject.CreateAppName;
            destObject.ChangeLastDate = sourceObject.ChangeLastDate;
            destObject.ChangeLastUserId = sourceObject.ChangeLastUserId;
            destObject.ChangeLastAppName = sourceObject.ChangeLastAppName;
            destObject.WorkingUserId = sourceObject.WorkingUserId;
            return destObject;
        }
        

    }



}
