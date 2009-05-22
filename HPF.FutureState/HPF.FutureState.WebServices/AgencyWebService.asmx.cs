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
            try
            {
                if (IsAuthenticated())//Authentication checking                
                {
                    var workingInstance = ForeclosureCaseSetBL.Instance;
                    response.FcId = workingInstance.SaveForeclosureCaseSet(request.ForeclosureCaseSet);
                    if (workingInstance.WarningMessage != null && workingInstance.WarningMessage.Count != 0)
                    {
                        response.Status = ResponseStatus.Warning;
                        response.Messages = workingInstance.WarningMessage;
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
                    CallLogDTO callLogDTO = null;
                    var validCallLodId = ValidateCallLogID(request);
                    if (validCallLodId)
                    {
                        var callLogId = GetCallLogID(request);
                        if (callLogId != 0)
                        {
                            callLogDTO = CallLogBL.Instance.RetrieveCallLog(callLogId);
                        }
                    }
                    if (callLogDTO != null)
                    {
                        var callLogWSDTO = ConvertToCallLogWSDTO(callLogDTO);
                        response.CallLog = callLogWSDTO;
                        response.Status = ResponseStatus.Success;
                        GetCallWarningMessage(callLogDTO.CallId.Value, response);                                                    
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
                    string reportFormat = request.ReportOutput.ToUpper();
                    if (reportFormat == null || reportFormat == string.Empty)
                        response.ForeclosureCaseSet = ForeclosureCaseSetBL.Instance.GetForeclosureCaseDetail(request.ForeclosureId.Value);
                    else
                        response.ReportSummary = SummaryReportBL.Instance.GenerateSummaryReport(request.ForeclosureId);
                    response.Status = ResponseStatus.Success;
                }
            }
            #region Exception Handling
            catch (AuthenticationException Ex)
            {
                response.Status = ResponseStatus.AuthenticationFail;
                response.Messages.AddExceptionMessage(ErrorMessages.ERR0451, Ex.Message);
                HandleException(Ex, request.ForeclosureId.HasValue?request.ForeclosureId.Value.ToString():"");
            }
            catch (DataValidationException Ex)
            {
                response.Messages = Ex.ExceptionMessages;
                HandleException(Ex, request.ForeclosureId.HasValue ? request.ForeclosureId.Value.ToString() : "");
            }
            catch (DataAccessException Ex)
            {
                response.Messages.AddExceptionMessage("Data access error.");
                HandleException(Ex, request.ForeclosureId.HasValue ? request.ForeclosureId.Value.ToString() : "");
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
        
        #region Privates Methods
        private static bool ValidateCallLogID(CallLogRetrieveRequest request)
        {
            var dataValidationException = new DataValidationException();
            request.callLogId = request.callLogId.Trim();
            if (string.IsNullOrEmpty(request.callLogId))
            {
                dataValidationException.ExceptionMessages.AddExceptionMessage(ErrorMessages.ERR0900, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0900));
                throw dataValidationException;
            }
            var validationResults = HPFValidator.Validate(request);
            if (!validationResults.IsValid)
            {
                dataValidationException.ExceptionMessages.AddExceptionMessage(ErrorMessages.ERR0902, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0902));
                throw dataValidationException;
            }
            return true;
        }

        private static int GetCallLogID(CallLogRetrieveRequest request)
        {
            var callLogId = 0;
            if (request.callLogId != string.Empty)
            {
                var sCallLogId = request.callLogId.Replace("HPF", "");
                try
                {
                    callLogId = Convert.ToInt32(sCallLogId);
                }
                catch
                {
                    var dataValidationException = new DataValidationException();
                    dataValidationException.ExceptionMessages.AddExceptionMessage(ErrorMessages.ERR0902, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0902));
                    throw dataValidationException;
                }
            }
            return callLogId;
        }        

        private static CallLogWSReturnDTO ConvertToCallLogWSDTO(CallLogDTO sourceObject)
        {
            var destObject = new CallLogWSReturnDTO();
            if (sourceObject.CallId.HasValue)
                destObject.CallId = "HPF" + Convert.ToString(sourceObject.CallId);

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
            destObject.PrevAgencyId = sourceObject.PrevAgencyId;
            destObject.ReasonForCall = sourceObject.ReasonForCall;
            destObject.StartDate = sourceObject.StartDate;
            destObject.ServicerId = sourceObject.ServicerId;
            destObject.SelectedAgencyId = sourceObject.SelectedAgencyId;
            destObject.SelectedCounselor = sourceObject.SelectedCounselor;
            destObject.City = sourceObject.City;
            destObject.State = sourceObject.State;
            destObject.NonprofitReferralKeyNum1 = sourceObject.NonprofitReferralKeyNum1;
            destObject.NonprofitReferralKeyNum2 = sourceObject.NonprofitReferralKeyNum2;
            destObject.NonprofitReferralKeyNum3 = sourceObject.NonprofitReferralKeyNum3;
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
            string reportFormat = request.ReportOutput;
            if(reportFormat != null)
                reportFormat = reportFormat.Trim().ToUpper();

            ExceptionMessageCollection ex = new ExceptionMessageCollection();
            if (request.ForeclosureId == null)
                ex.AddExceptionMessage("Unable to retrieve summary. A FCId is required to retrieve a summary.");
            else
            {
                ForeclosureCaseDTO fc = ForeclosureCaseBL.Instance.GetForeclosureCase(request.ForeclosureId);
                if (fc == null)
                    ex.AddExceptionMessage("Unable to retrieve summary. The FCId is not a valid foreclosure case ID.");
                else if (fc.AgencyId != CurrentAgencyID.Value)
                    ex.AddExceptionMessage("Unable to retrieve summary. The FCId does not belong to your agency.");
            }
            if (reportFormat != null && reportFormat!= string.Empty && reportFormat != "PDF")
                ex.AddExceptionMessage("Unable to retrieve summary. The specified report format is not supported.");            

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
