using System;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Services.Protocols;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils;

namespace HPF.FutureState.WebServices
{
    /// <summary>
    /// Summary description for AgencyWebService
    /// </summary>
    [WebService(Namespace = "https://www.homeownershopenetwork.org")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]    
    public class AgencyWebService : BaseAgencyWebService
    {
        #region WebMethods
        [WebMethod]
        [SoapHeader("Authentication", Direction = SoapHeaderDirection.In)]
        public ForeclosureCaseSaveResponse SaveForeclosureCase(ForeclosureCaseSaveRequest request)
        {
            var response = new ForeclosureCaseSaveResponse();
            ForeclosureCaseSetBL workingInstance = null;
            try
            {
                if (IsAuthenticated())//Authentication checking                
                {
                    workingInstance = ForeclosureCaseSetBL.Instance;
                    ForeclosureCaseDTO fc = workingInstance.SaveForeclosureCaseSet(request.ForeclosureCaseSet);
                    response.FcId = fc.FcId;
                    response.CompletedDt = fc.CompletedDt;
                    response.AgencyId = (int)fc.AgencyId;
                    if (workingInstance.WarningMessage != null && workingInstance.WarningMessage.Count != 0)
                    {
                        response.Status = ResponseStatus.Warning;
                        //response.Messages = workingInstance.WarningMessage;
                    }                    
                    else
                        response.Status = ResponseStatus.Success;
                }
            }
            catch (AuthenticationException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Messages.AddExceptionMessage(ErrorMessages.ERR0451, Ex.Message);

                HandleException(Ex, GetFcId(request));
            }
            catch (DataValidationException Ex)
            {
                response.Status = ResponseStatus.Fail;                
                if (Ex.ExceptionMessages != null && Ex.ExceptionMessages.Count > 0)
                    response.Messages = Ex.ExceptionMessages;
                else
                    response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex, GetFcId(request));
            }
            catch (DataAccessException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage("Data access error.");
                //response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex, GetFcId(request));
            }            
            catch (DuplicateException Ex)
            {
                response.Status = ResponseStatus.Fail;
                if (Ex.ExceptionMessages != null && Ex.ExceptionMessages.Count > 0)
                    response.Messages = Ex.ExceptionMessages;
                else
                    response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex, GetFcId(request));
            }
            catch (Exception Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex, GetFcId(request));
            }

            if (workingInstance != null && workingInstance.WarningMessage != null && workingInstance.WarningMessage.Count != 0)
                    response.Messages.Add(workingInstance.WarningMessage);
            
            //Send WS debug info collector if any
            if ((response.Status!=ResponseStatus.AuthenticationFail) && (string.Compare(HPFConfigurationSettings.WS_DEBUG_MODE.ToUpper(), "ON") == 0))
            {
                try
                {
                    string[] agencyIds = HPFConfigurationSettings.WS_DEBUG_AGENCY_LIST.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < agencyIds.Length; i++)
                        if (string.Compare(CurrentAgencyID.ToString(), agencyIds[i]) == 0)
                        {
                            DebugInfoCollector debugInfo = new DebugInfoCollector() { FCaseSetRequest = request.ForeclosureCaseSet, Response = response, CurAgencyId = CurrentAgencyID, FcId = GetFcId(request) };
                            debugInfo.SaveForeclosureCaseWSDebugInfo();
                            break;
                        }
                }
                catch (Exception ex)
                {
                    HandleException(ex,GetFcId(request));
                }
            }
            return response;
        }

        [WebMethod]
        [SoapHeader("Authentication", Direction = SoapHeaderDirection.In)]
        public PrePurchaseCaseSaveResponse SavePrePurchaseCase(PrePurchaseCaseSaveRequest request)
        {
            PrePurchaseCaseSaveResponse response = new PrePurchaseCaseSaveResponse();
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
                    CallLogWSReturnDTOCollection callLogDTOs = CallLogBL.Instance.RetrieveICTCallLog(request.ICTCallId);

                    if (callLogDTOs.Count > 0)
                    {                       
                        response.CallLogs = callLogDTOs;
                        response.Status = ResponseStatus.Success;
                        //GetCallWarningMessage(callLogDTO.CallId.Value, response);
                    }
                    else
                    {
                        response.Status = ResponseStatus.Fail;
                        response.Messages.AddExceptionMessage(ErrorMessages.ERR0901, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0901));
                    }
                }
            }
            #region Exception Handling
            catch (AuthenticationException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Messages.AddExceptionMessage(ErrorMessages.ERR0451, Ex.Message);
                HandleException(Ex);
            }
            catch (DataValidationException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages = Ex.ExceptionMessages;
                HandleException(Ex);
            }
            catch (DataAccessException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage("Data access error.");
                HandleException(Ex);
            }
            catch (Exception Ex)
            {
                response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex);
            }
            #endregion
            return response;
        }
        
        [WebMethod]
        [SoapHeader("Authentication", Direction = SoapHeaderDirection.In)]
        public SendSummaryResponse SendSummary(SendSummaryRequest request)
        {
            var response = new SendSummaryResponse();
            try
            {
                if (IsAuthenticated())//Authentication checking                
                {
                    var workingInstance = EmailSummaryBL.Instance;
                    response = workingInstance.ProcessWebServiceSendSummary(request, (int)CurrentAgencyID);

                }
            }
            catch (AuthenticationException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Messages.AddExceptionMessage(ErrorMessages.ERR0451, Ex.Message);
                HandleException(Ex, request.FCId.HasValue?request.FCId.Value.ToString():"");
            }
            catch (DataValidationException Ex)
            {
                response.Status = ResponseStatus.Fail;                
                if (Ex.ExceptionMessages != null && Ex.ExceptionMessages.Count > 0)
                    response.Messages = Ex.ExceptionMessages;
                else
                    response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex, request.FCId.HasValue ? request.FCId.Value.ToString() : "");
            }
            catch (DataAccessException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex, request.FCId.HasValue ? request.FCId.Value.ToString() : "");
            }            
            catch (Exception Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex, request.FCId.HasValue ? request.FCId.Value.ToString() : "");
            }

            return response;
        }

        [WebMethod]
        [SoapHeader("Authentication", Direction = SoapHeaderDirection.In)]
        public ServicerListRetrieveResponse RetrieveServicerList()
        {
            ServicerListRetrieveResponse response = new ServicerListRetrieveResponse();
            response.Status = ResponseStatus.Fail;
            try
            {
                if (IsAuthenticated())//Authentication checking
                {
                    response.Servicers = LookupDataBL.Instance.GetServicers();
                    if (response.Servicers.Count > 0 && response.Servicers[0].ServicerID == -1)
                        response.Servicers.RemoveAt(0);
                    //if (response.Servicers.Count == 0)
                    //{
                    //    response.Status = ResponseStatus.Fail;
                    //    response.Messages.AddExceptionMessage("ERR0", "ERR0--There is no marched data found. Please contact administrator for more information");
                    //}
                    response.Status = ResponseStatus.Success;                    
                }
            }
            #region Exception Handling
            catch (AuthenticationException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Messages.AddExceptionMessage(ErrorMessages.ERR0451, Ex.Message);
                HandleException(Ex);
            }
            catch (DataValidationException Ex)
            {
                response.Messages = Ex.ExceptionMessages;
                HandleException(Ex);
            }
            catch (DataAccessException Ex)
            {
                response.Messages.AddExceptionMessage("Data access error.");
                HandleException(Ex);
            }
            catch (Exception Ex)
            {
                response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex);
            }
            #endregion
            return response;
        }

        [WebMethod]
        [SoapHeader("Authentication", Direction = SoapHeaderDirection.In)]
        public ReferenceCodeRetrieveResponse RetrieveReferenceCode(ReferenceCodeRetrieveRequest request)
        {
            ReferenceCodeRetrieveResponse response = new ReferenceCodeRetrieveResponse();
            response.Status = ResponseStatus.Fail;
            try
            {
                if (IsAuthenticated())//Authentication checking
                {                    
                    response.ReferenceCodes = RefCodeItemBL.Instance.GetRefCodeItemsForAgency(request.ReferenceCodeName);
                    //if (response.ReferenceCodes.Count == 0)
                    //{
                    //    response.Status = ResponseStatus.Fail;
                    //    response.Messages.AddExceptionMessage("ERR0", "ERR0--There is no marched data found. Please contact administrator for more information");
                    //}
                    response.Status = ResponseStatus.Success;                    
                }
            }
            #region Exception Handling
            catch (AuthenticationException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Messages.AddExceptionMessage(ErrorMessages.ERR0451, Ex.Message);
                HandleException(Ex);
            }
            catch (DataValidationException Ex)
            {             
                response.Messages = Ex.ExceptionMessages;
                HandleException(Ex);
            }
            catch (DataAccessException Ex)
            {                
                response.Messages.AddExceptionMessage("Data access error.");
                HandleException(Ex);
            }
            catch (Exception Ex)
            {
                response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex);
            }
            #endregion
            return response;
        }

        [WebMethod]
        [SoapHeader("Authentication", Direction = SoapHeaderDirection.In)]
        public SummaryRetrieveResponse RetrieveSummary(SummaryRetrieveRequest request)
        {
            SummaryRetrieveResponse response = new SummaryRetrieveResponse();
            response.Status = ResponseStatus.Fail;
            try
            {
                if (IsAuthenticated())//Authentication checking                
                {
                    ExceptionMessageCollection exCol = ValidateRetrieveSummary(request);
                    if (exCol.Count > 0)
                    {
                        response.Messages = exCol;
                        return response;
                    }

                    if (string.IsNullOrEmpty(request.OutputFormat))
                        response.ForeclosureCaseSet = ForeclosureCaseSetBL.Instance.GetForeclosureCaseDetail(request.FCId.Value);
                    else
                    {
                        ReportFormat rptFormat = (ReportFormat)Enum.Parse(typeof(ReportFormat), request.OutputFormat);
                        response.ReportSummary = SummaryReportBL.Instance.GenerateSummaryReport(request.FCId, rptFormat);
                    }

                    response.Status = ResponseStatus.Success;
                }
            }
            #region Exception Handling
            catch (AuthenticationException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Messages.AddExceptionMessage(ErrorMessages.ERR0451, Ex.Message);
                HandleException(Ex, request.FCId.HasValue ? request.FCId.Value.ToString() : "");
            }
            catch (DataValidationException Ex)
            {
                response.Messages = Ex.ExceptionMessages;
                HandleException(Ex, request.FCId.HasValue ? request.FCId.Value.ToString() : "");
            }
            catch (DataAccessException Ex)
            {
                response.Messages.AddExceptionMessage("Data access error.");
                HandleException(Ex, request.FCId.HasValue ? request.FCId.Value.ToString() : "");
            }
            catch (Exception Ex)
            {
                response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex);
            }
            #endregion
            return response;
        }
        #endregion

        [WebMethod]
        [SoapHeader("Authentication", Direction = SoapHeaderDirection.In)]
        public CallLogSearchResponse SearchCallLog(CallLogSearchRequest request)
        {
            var response = new CallLogSearchResponse();
            //            
            try
            {
                if (IsAuthenticated())//Authentication checking
                {
                    CallLogWSReturnDTOCollection callLogDTOs = CallLogBL.Instance.ICTSearchCall(request.SearchCriteria);                    
                    
                    response.CallLogs = callLogDTOs;
                    response.Status = ResponseStatus.Success;
                }
            }
            #region Exception Handling
            catch (AuthenticationException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Messages.AddExceptionMessage(ErrorMessages.ERR0451, Ex.Message);
                HandleException(Ex);
            }
            catch (DataValidationException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages = Ex.ExceptionMessages;
                HandleException(Ex);
            }
            catch (DataAccessException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage("Data access error.");
                HandleException(Ex);
            }
            catch (Exception Ex)
            {
                response.Messages.AddExceptionMessage(Ex.Message);
                HandleException(Ex);
            }
            #endregion
            return response;
        }
        #region Privates Methods
                    
        private ExceptionMessageCollection ValidateRetrieveSummary(SummaryRetrieveRequest request)
        {
            string reportFormat = request.OutputFormat;
            if(reportFormat != null)
                reportFormat = reportFormat.Trim().ToUpper();

            ExceptionMessageCollection ex = new ExceptionMessageCollection();
            if (request.FCId == null)
                ex.AddExceptionMessage(ErrorMessages.ERR1000, ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1000));
            else
            {
                ForeclosureCaseDTO fc = ForeclosureCaseBL.Instance.GetForeclosureCase(request.FCId);
                if (fc == null)
                    ex.AddExceptionMessage(ErrorMessages.ERR1001, ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1001, request.FCId));
                //else if ((fc.AgencyId == Constant.AGENCY_AURITON_ID && CurrentAgencyID.Value != Constant.AGENCY_NOVADEBT_ID)
                //            && (fc.AgencyId != CurrentAgencyID.Value && CurrentAgencyID.Value != Constant.AGENCY_MMI_ID))
                //    ex.AddExceptionMessage("Unable to retrieve case summary. The FCId does not belong to your agency.");
                else if (fc.AgencyId != CurrentAgencyID.Value)
                {
                    if (CurrentAgencyID.Value == Constant.AGENCY_NOVADEBT_ID)
                    {
                        if (fc.AgencyId != Constant.AGENCY_AURITON_ID && fc.AgencyId != Constant.AGENCY_NOVADEBT_ID)
                            ex.AddExceptionMessage("Unable to retrieve case summary. The FCId does not belong to your agency.");
                    }
                    else if (CurrentAgencyID.Value != Constant.AGENCY_MMI_ID)
                       ex.AddExceptionMessage("Unable to retrieve case summary. The FCId does not belong to your agency.");
                }
            }
            if (!string.IsNullOrEmpty(reportFormat))
            {
                try
                {
                    ReportFormat rptFormat = (ReportFormat)Enum.Parse(typeof(ReportFormat), reportFormat);
                }
                catch
                {
                    ex.AddExceptionMessage(ErrorMessages.ERR1002, ErrorMessages.GetExceptionMessage(ErrorMessages.ERR1002));
                }
            }

            return ex;
        }

        private string GetFcId(ForeclosureCaseSaveRequest request)
        {
            try
            {
                return request.ForeclosureCaseSet.ForeclosureCase.FcId.Value.ToString();
            }
            catch 
            {
                return "";
            }
        }
        #endregion
    }
}
