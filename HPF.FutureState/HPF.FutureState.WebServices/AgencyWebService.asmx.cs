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
        public ForeclosureCaseInsertResponse SaveForeclosureCase(ForeclosureCaseInsertRequest request)
        {
            var response = new ForeclosureCaseInsertResponse();
            try
            {
                if (IsAuthenticated())//Authentication checking
                //if (true)
                {
                    SetDefaultValues(request.ForeclosureCaseSet);
                    ForeclosureCaseSetBL.Instance.SaveForeclosureCaseSet(request.ForeclosureCaseSet);
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
            catch (ProcessingException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages = Ex.ExceptionMessages;
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

        private void SetDefaultValues(ForeclosureCaseSetDTO fcCaseSet)
        {
            base.SetDefaultValues(fcCaseSet.ForeclosureCase);
            foreach (BudgetAssetDTO obj in fcCaseSet.BudgetAssets)
                base.SetDefaultValues(obj);
            foreach (BudgetItemDTO obj in fcCaseSet.BudgetItems)
                base.SetDefaultValues(obj);
            foreach (OutcomeItemDTO obj in fcCaseSet.Outcome)
                base.SetDefaultValues(obj);
            foreach (ActivityLogDTO obj in fcCaseSet.ActivityLog)
                base.SetDefaultValues(obj);
            foreach (CaseLoanDTO obj in fcCaseSet.CaseLoans)
                base.SetDefaultValues(obj);
        }                
    }
}
