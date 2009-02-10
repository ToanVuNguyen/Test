using System;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

namespace HPF.FutureState.WebServices
{
    public class BaseWebService : WebService
    {
        public AuthenticationInfo Authentication;

        protected int? CurrentAgencyID { get; set; }

        protected int? CurrentCallCenterID { get; set; }

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
                {                    
                    int pageSize;
                    int.TryParse(ConfigurationManager.AppSettings["SearchResult_MaxRow"], out pageSize);

                    var results = ForeclosureCaseSetBL.Instance.SearchForeclosureCase(request.SearchCriteria, pageSize);
                    response = new ForeclosureCaseSearchResponse
                                   {
                                       Results = results,
                                       SearchResultCount = results.SearchResultCount,
                                       Status = ResponseStatus.Success
                                   };
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


        protected void HandleException(Exception Ex)
        {
            ExceptionProcessor.HandleException(Ex, Authentication.UserName, CurrentAgencyID.ToString(),
                                               CurrentCallCenterID.ToString());
        }
    }
}
