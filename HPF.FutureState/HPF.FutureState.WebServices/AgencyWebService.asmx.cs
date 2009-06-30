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
                    response.FcId = workingInstance.SaveForeclosureCaseSet(request.ForeclosureCaseSet);
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
                HandleException(Ex);
            }

            if (workingInstance != null && workingInstance.WarningMessage != null && workingInstance.WarningMessage.Count != 0)
                    response.Messages.Add(workingInstance.WarningMessage);

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
                    CallLogDTOCollection callLogDTOs = CallLogBL.Instance.RetrieveICTCallLog(request.ICTCallId);

                    if (callLogDTOs.Count > 0)
                    {
                        CallLogWSReturnDTOCollection results = new CallLogWSReturnDTOCollection();
                        int Count = callLogDTOs.Count;
                        if (Count > 100) Count = 100;
                        for (int i = 0; i < Count; i++)
                        {                            
                            results.Add(ConvertToCallLogWSDTO(callLogDTOs[i]));
                        }
                        response.CallLogs = results;
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
                    string reportFormat = request.OutputFormat.ToUpper();
                    if (reportFormat == null || reportFormat == string.Empty)
                        response.ForeclosureCaseSet = ForeclosureCaseSetBL.Instance.GetForeclosureCaseDetail(request.FCId.Value);
                    else
                        response.ReportSummary = SummaryReportBL.Instance.GenerateSummaryReport(request.FCId);
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
                    CallLogDTOCollection callLogDTOs = CallLogBL.Instance.ICTSearchCall(request.SearchCriteria);

                    CallLogWSReturnDTOCollection results = new CallLogWSReturnDTOCollection();
                    int Count = callLogDTOs.Count;
                    if (Count > 100) Count = 100;
                    for (int i = 0; i < Count; i++)
                    {
                        results.Add(ConvertToCallLogWSDTO(callLogDTOs[i]));
                    }
                    response.CallLogs = results;
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

        private static CallLogWSReturnDTO ConvertToCallLogWSDTO(CallLogDTO sourceObject)
        {
            var destObject = new CallLogWSReturnDTO();
            if (sourceObject.CallId.HasValue)
                destObject.HopeNetCallId = Convert.ToString(sourceObject.CallId);

            destObject.AuthorizedInd = sourceObject.AuthorizedInd;
            destObject.CallSourceCd = sourceObject.CallSourceCd;
            destObject.EndDate = sourceObject.EndDate;
            destObject.FinalDispoCd = sourceObject.FinalDispoCd;
            destObject.FirstName = sourceObject.FirstName;
            destObject.HomeownerInd = sourceObject.HomeownerInd;
            destObject.LoanAccountNumber = sourceObject.LoanAccountNumber;
            destObject.LastName = sourceObject.LastName;
            destObject.LoanDelinqStatusCd = sourceObject.LoanDelinqStatusCd;
            destObject.OtherServicerName = sourceObject.OtherServicerName;
            destObject.PowerOfAttorneyInd = sourceObject.PowerOfAttorneyInd;
            destObject.PropZipFull9 = sourceObject.PropZipFull9;
            destObject.ReasonForCall = sourceObject.ReasonForCall;
            destObject.StartDate = sourceObject.StartDate;
            destObject.ServicerId = sourceObject.ServicerId;
            destObject.City = sourceObject.City;
            destObject.State = sourceObject.State;
            destObject.NonprofitReferralKeyNum1 = sourceObject.NonprofitReferralKeyNum1;
            destObject.NonprofitReferralKeyNum2 = sourceObject.NonprofitReferralKeyNum2;
            destObject.NonprofitReferralKeyNum3 = sourceObject.NonprofitReferralKeyNum3;
            destObject.DelinqInd = sourceObject.DelinqInd;
            destObject.PropStreetAddress = sourceObject.PropStreetAddress;
            destObject.PrimaryResidenceInd = sourceObject.PrimaryResidenceInd;
            destObject.MaxLoanAmountInd = sourceObject.MaxLoanAmountInd;
            destObject.CustomerPhone = sourceObject.CustomerPhone;
            destObject.LoanLookupCd = sourceObject.LoanLookupCd;
            destObject.OriginatedPrior2009Ind = sourceObject.OriginatedPrior2009Ind;
            destObject.PaymentAmount = sourceObject.PaymentAmount;
            destObject.GrossIncomeAmount = sourceObject.GrossIncomeAmount;
            destObject.DTIInd = sourceObject.DTIInd;
            destObject.ServicerCANumber = sourceObject.ServicerCANumber;
            destObject.ServicerCALastContactDate = sourceObject.ServicerCALastContactDate;
            destObject.ServicerCAId = sourceObject.ServicerCAId;
            destObject.ServicerCAOtherName = sourceObject.ServicerCAOtherName;
            destObject.MHAInfoShareInd = sourceObject.MHAInfoShareInd;
            destObject.ICTCallId = sourceObject.ICTCallId;
            destObject.MHAEligibilityCd = sourceObject.MHAEligibilityCd;
            destObject.ServicerName = sourceObject.ServicerName;
            destObject.FinalDispoDesc = sourceObject.FinalDispoDesc;
            destObject.NonprofitReferralDesc1 = sourceObject.NonprofitReferralDesc1;
            destObject.NonprofitReferralDesc2 = sourceObject.NonprofitReferralDesc2;
            destObject.NonprofitReferralDesc3 = sourceObject.NonprofitReferralDesc3;
            destObject.ServicerCAName = sourceObject.ServicerCAName;
            destObject.MHAEligibilityDesc = sourceObject.MHAEligibilityDesc;
            destObject.MHAIneligibilityReasonDesc = sourceObject.MHAIneligibilityReasonDesc;
            destObject.MHAIneligibilityReasonCd = sourceObject.MHAIneligibilityReasonCd;

            return destObject;
        }

        private void GetCallWarningMessage(int callId, CallLogRetrieveResponse response)
        {           
            string msgFormat = ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0903);
            ForeclosureCaseCallSearchResultDTOCollection results = ForeclosureCaseSetBL.Instance.GetForclosureCaseFromCall(callId);
            
            if (results.Count > 0)
                response.Status = ResponseStatus.Warning;

            foreach (ForeclosureCaseCallSearchResultDTO fc in results)
            {                
                string buffer = string.Format(msgFormat, fc.ServicerName, fc.AccountNum, fc.PropZip, fc.BorrowFName, fc.BorrowLName, fc.CounselorFName, fc.CountselorLName, fc.AgencyName, fc.CounselorPhone, fc.CounselorExt, fc.CounselorEmail, fc.OutcomeDate, fc.OutcomeTypeName);
                response.Messages.AddExceptionMessage(ErrorMessages.WARN0903, buffer);
            }
        }

        private ExceptionMessageCollection ValidateRetrieveSummary(SummaryRetrieveRequest request)
        {
            string reportFormat = request.OutputFormat;
            if(reportFormat != null)
                reportFormat = reportFormat.Trim().ToUpper();

            ExceptionMessageCollection ex = new ExceptionMessageCollection();
            if (request.FCId == null)
                ex.AddExceptionMessage("Unable to retrieve summary. A FCId is required to retrieve a summary.");
            else
            {
                ForeclosureCaseDTO fc = ForeclosureCaseBL.Instance.GetForeclosureCase(request.FCId);
                if (fc == null)
                    ex.AddExceptionMessage("Unable to retrieve case summary. The FCId is not a valid foreclosure case ID.");
                else if (fc.AgencyId != CurrentAgencyID.Value)
                    ex.AddExceptionMessage("Unable to retrieve case summary. The FCId does not belong to your agency.");
            }
            if (reportFormat != null && reportFormat!= string.Empty && reportFormat != "PDF")
                ex.AddExceptionMessage("Unable to retrieve case summary. The specified summary format is not supported.");            

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
