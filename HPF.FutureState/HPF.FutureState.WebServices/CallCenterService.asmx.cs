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
            //            
            try
            {
                //if (IsAuthenticated())//Authentication checking
                if (true)
                {
                    //Call Business Logic Here
                    CallLogDTO callLogDTO = new CallLogDTO(request.CallLog);
                    string callLogID = CallLogBL.Instance.InsertCallLog(callLogDTO);

                    response.CallLogID = callLogID;
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
                    CallLogWSDTO callLogWSDTO = new CallLogWSDTO();
                    CallLogDTO callLogDTO = new CallLogDTO();
                    var callLogId = int.MinValue;
                    if (request.callLogId != string.Empty)
                    {
                        callLogId = Convert.ToInt32(request.callLogId);                        
                    }
                    callLogDTO = CallLogBL.Instance.RetrieveCallLog(callLogId);
                    //
                    if (callLogDTO != null)
                    {
                        callLogWSDTO = new CallLogWSDTO(callLogDTO);
                        response.CallLog = callLogWSDTO;
                        response.Status = ResponseStatus.Success;
                    }
                    else
                    {
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

    }



}
