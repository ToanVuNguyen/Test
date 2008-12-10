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
        public ForeClosureCaseSearchResponse SearchForeClosureCase(ForeClosureCaseSearchRequest request)
        {
            var response = new ForeClosureCaseSearchResponse();
            //response.SearchResultCount = 100;         
            try
            {
                //if (IsAuthenticated())
                if (true)
                {
                    //Business Call
                    int maxRow = 0;
                    int.TryParse(ConfigurationManager.AppSettings["SearchResult_MaxRow"].ToString(), out maxRow);

                    ForeClosureCaseSearchResult results = ForeclosureCaseBL.Instance.SearchForeClosureCase(request.SearchCriteria);
                    response = new ForeClosureCaseSearchResponse();
                    response.Results = new ForeClosureCaseSearchResult();

                    int count = 1;
                    if (maxRow > 0 && maxRow < results.Count)
                    {
                        //var subResults = results.Take<ForeClosureCaseWSDTO>(maxRow);                        
                        foreach (ForeClosureCaseWSDTO result in results)
                            if (count <= maxRow)
                            {
                                response.Results.Add(result);
                                count++;
                            }
                            else break;
                    }

                    else
                        response.Results = results;//(ForeClosureCaseSearchResult)results.Take(results.Count);

                    //response.Results = results;
                    response.SearchResultCount = results.Count;
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
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage(0, Ex.Message);
            }
            catch (DataAccessException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage(0, Ex.Message);
            }
            catch (Exception Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage(0, Ex.Message);
            }
            return response;
        }        
    }
}
