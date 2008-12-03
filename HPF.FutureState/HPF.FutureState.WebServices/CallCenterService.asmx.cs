using System;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Services.Protocols;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

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
        public CallLogResponse SaveCallLog(CallLogRequest request)
        {
            var response = new CallLogResponse();            
            //            
            try
            {
                if (IsAuthenticated())//Authentication checking
                {
                    //Call Business Logic Here
                    response.Status = ResponseStatus.Success;
                }
            }
            catch (AuthenticationFailException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Message = Ex.Message;
            }            
            catch(Exception Ex)
            {
                response.Message = Ex.Message;                
            }
            return response;
        }
    }
}
