using System;
using System.ComponentModel;

using System.Configuration;

using System.Linq;

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
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
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
                    CallLogDTO callLogDTO = ConvertToCallLogDTO(request.CallLog);                                        
                    string sCallLogID = "HPF_" + CallLogBL.Instance.InsertCallLog(callLogDTO);
                    response.CallLogID = sCallLogID;
                    response.Status = ResponseStatus.Success;                                        
                }
            }
            catch (AuthenticationException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Messages.AddExceptionMessage(0, Ex.Message);
                ExceptionProcessor.HandleException(Ex);
            }
            catch (DataValidationException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages = Ex.ExceptionMessages;
                ExceptionProcessor.HandleException(Ex);
            }
            catch (DataAccessException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage(0, "Data access error.");
                ExceptionProcessor.HandleException(Ex);
            } 
            catch(Exception Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage(0, Ex.Message);
                ExceptionProcessor.HandleException(Ex);
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
                    CallLogWSDTO callLogWSDTO = null;
                    CallLogDTO callLogDTO = null;
                    var callLogId = ValidateCallLogID(request);
                    if (callLogId != int.MinValue)
                    {
                        callLogDTO = CallLogBL.Instance.RetrieveCallLog(callLogId);
                    }
                    else
                    {
                        response.Messages.AddExceptionMessage(0, "No data found");
                        response.Status = ResponseStatus.Warning;
                    }
                    //
                    if (callLogDTO != null)
                    {
                        callLogWSDTO = ConvertToCallLogWSDTO(callLogDTO);
                        response.CallLog = callLogWSDTO;
                        response.Status = ResponseStatus.Success;
                    }
                    else
                    {
                        response.Messages.AddExceptionMessage(0, "No data found");
                        response.Status = ResponseStatus.Warning;
                    }
                }
            }
            catch (AuthenticationException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Messages.AddExceptionMessage(0, Ex.Message);
                ExceptionProcessor.HandleException(Ex);
            }
            catch (DataValidationException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages = Ex.ExceptionMessages;
                ExceptionProcessor.HandleException(Ex);
            }
            catch (DataAccessException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage(0, "Data access error.");
                ExceptionProcessor.HandleException(Ex);
            }
            catch (Exception Ex)
            {
                response.Messages.AddExceptionMessage(0, Ex.Message);
                ExceptionProcessor.HandleException(Ex);
            }
            return response;
        }

        private static int ValidateCallLogID(CallLogRetrieveRequest request)
        {
            var callLogId = int.MinValue;
            if (request.callLogId != string.Empty)
            {
                callLogId = Convert.ToInt32(request.callLogId);
            }
            return callLogId;
        }

        private CallLogDTO ConvertToCallLogDTO(CallLogWSDTO sourceObject)
        {
            CallLogDTO destObject = new CallLogDTO();
            if (sourceObject.CallId != null)
            {
                sourceObject.CallId = sourceObject.CallId.Replace("HPF_", "");
                int id = 0;
                int.TryParse(sourceObject.CallId, out id);
                destObject.CallId = id;
            }
            destObject.CallCenterID = sourceObject.CallCenterID;
            destObject.CcAgentIdKey = sourceObject.CcAgentIdKey;
            destObject.StartDate = sourceObject.StartDate;
            destObject.EndDate = sourceObject.EndDate;
            destObject.DNIS = sourceObject.DNIS;
            destObject.CallCenter = sourceObject.CallCenter;
            destObject.CallSourceCd = sourceObject.CallSourceCd;
            destObject.ReasonToCall = sourceObject.ReasonToCall;
            destObject.LoanAccountNumber = sourceObject.LoanAccountNumber;
            destObject.FirstName = sourceObject.FirstName;
            destObject.LastName = sourceObject.LastName;
            destObject.ServicerId = sourceObject.ServicerId;
            destObject.OtherServicerName = sourceObject.OtherServicerName;
            destObject.PropZipFull9 = sourceObject.PropZipFull9;
            destObject.PrevAgencyId = sourceObject.PrevAgencyId;
            destObject.SelectedAgencyId = sourceObject.SelectedAgencyId;
            destObject.ScreenRout = sourceObject.ScreenRout;
            destObject.FinalDispoCd = sourceObject.FinalDispoCd;
            destObject.TransNumber = sourceObject.TransNumber;
            destObject.CreateDate = sourceObject.CreateDate;
            destObject.CreateUserId = sourceObject.CreateUserId;
            destObject.CreateAppName = sourceObject.CreateAppName;
            destObject.ChangeLastDate = sourceObject.ChangeLastDate;
            destObject.ChangeLastUserId = sourceObject.ChangeLastUserId;
            destObject.ChangeLastAppName = sourceObject.ChangeLastAppName;

            return destObject;
        }

        private CallLogWSDTO ConvertToCallLogWSDTO(CallLogDTO sourceObject )
        {
            CallLogWSDTO destObject = new CallLogWSDTO();
            if (sourceObject.CallId != 0)
                destObject.CallId = "HPF_" + Convert.ToString(sourceObject.CallId);
            
            destObject.CallCenterID = sourceObject.CallCenterID;
            destObject.CcAgentIdKey = sourceObject.CcAgentIdKey;
            destObject.StartDate = sourceObject.StartDate;
            destObject.EndDate = sourceObject.EndDate;
            destObject.DNIS = sourceObject.DNIS;
            destObject.CallCenter = sourceObject.CallCenter;
            destObject.CallSourceCd = sourceObject.CallSourceCd;
            destObject.ReasonToCall = sourceObject.ReasonToCall;
            destObject.LoanAccountNumber = sourceObject.LoanAccountNumber;
            destObject.FirstName = sourceObject.FirstName;
            destObject.LastName = sourceObject.LastName;
            destObject.ServicerId = sourceObject.ServicerId;
            destObject.OtherServicerName = sourceObject.OtherServicerName;
            destObject.PropZipFull9 = sourceObject.PropZipFull9;
            destObject.PrevAgencyId = sourceObject.PrevAgencyId;
            destObject.SelectedAgencyId = sourceObject.SelectedAgencyId;
            destObject.ScreenRout = sourceObject.ScreenRout;
            destObject.FinalDispoCd = sourceObject.FinalDispoCd;
            destObject.TransNumber = sourceObject.TransNumber;
            destObject.CreateDate = sourceObject.CreateDate;
            destObject.CreateUserId = sourceObject.CreateUserId;
            destObject.CreateAppName = sourceObject.CreateAppName;
            destObject.ChangeLastDate = sourceObject.ChangeLastDate;
            destObject.ChangeLastUserId = sourceObject.ChangeLastUserId;
            destObject.ChangeLastAppName = sourceObject.ChangeLastAppName;

            return destObject;
        }
    }



}
