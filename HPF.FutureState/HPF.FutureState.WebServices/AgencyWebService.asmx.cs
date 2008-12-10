using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.WebServices
{
    /// <summary>
    /// Summary description for AgencyWebService
    /// </summary>
    [WebService(Namespace = "https://www.homeownershopenetwork.org")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AgencyWebService : BaseAgencyWebService
    {
        [WebMethod]
        [SoapHeader("Authentication", Direction = SoapHeaderDirection.In)]
        public ForeClosureCaseResponse SaveForeClosureCass(ForeClosureCaseRequest request)
        {
            var response = new ForeClosureCaseResponse();
            //            
            try
            {
                if (IsAuthenticated())
                {
                    //
                    response.Status = ResponseStatus.Success;
                }
            }
            catch (AuthenticationException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Messages.AddExceptionMessage(0, Ex.Message);
            }
            catch (DataValidationException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Messages = Ex.ExceptionMessages;
            } 
            catch (Exception Ex)
            {
                response.Messages.AddExceptionMessage(0, Ex.Message);
            }
            return response;
        }
        
    }
}
