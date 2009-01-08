using System;
using System.Reflection;
using System.Web.Security;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.Configuration;

using System.Linq;

using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

namespace HPF.FutureState.WebServices
{
    public class BaseWebService : WebService
    {
        public AuthenticationInfo Authentication;        

        /// <summary>
        /// Authenticate checking, this method can override for specific web service.
        /// </summary>
        /// <returns>True: Success, False : Fail</returns>
        protected virtual bool IsAuthenticated()
        {           
            return true;
        }

        [WebMethod]
        [SoapHeader("Authentication", Direction = SoapHeaderDirection.In)]
        public ForeclosureCaseSearchResponse SearchForeclosureCase(ForeclosureCaseSearchRequest request)
        {
            var response = new ForeclosureCaseSearchResponse();
            try
            {
                if (IsAuthenticated())
                //if (true)
                {
                    //Business Call
                    int pageSize = 0;
                    int.TryParse(ConfigurationManager.AppSettings["SearchResult_MaxRow"].ToString(), out pageSize);

                    ForeclosureCaseSearchResult results = ForeclosureCaseSetBL.Instance.SearchForeclosureCase(request.SearchCriteria, pageSize);
                    response = new ForeclosureCaseSearchResponse();
                    response.Results = results;
                    response.SearchResultCount = results.SearchResultCount;
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
                response.Messages.AddExceptionMessage(0, Ex.Message);
                ExceptionProcessor.HandleException(Ex);
            }
            catch (Exception Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage(0, Ex.Message);
                ExceptionProcessor.HandleException(Ex);
            }
            return response;
        }        

       
    }
}
