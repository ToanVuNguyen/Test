using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.DataTransferObjects;
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
        public ForeclosureCaseSaveResponse SaveForeclosureCase(ForeclosureCaseSaveRequest request)
        {
            var response = new ForeclosureCaseSaveResponse();
            try
            {
                if (IsAuthenticated())//Authentication checking                
                {
                    
                    ForeclosureCaseSetBL.Instance.SaveForeclosureCaseSet(request.ForeclosureCaseSet);
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
                //response.Messages = Ex.ExceptionMessages;
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
            catch (DuplicateException Ex)
            {
                response.Status = ResponseStatus.Fail;
                if (Ex.ExceptionMessages != null && Ex.ExceptionMessages.Count > 0)
                    response.Messages = Ex.ExceptionMessages;
                else
                    response.Messages.AddExceptionMessage(Ex.Message);
                ExceptionProcessor.HandleException(Ex);
            }
            catch (Exception Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage(Ex.Message);
                ExceptionProcessor.HandleException(Ex);
            }
            return response;
        }

                 
    }
}
