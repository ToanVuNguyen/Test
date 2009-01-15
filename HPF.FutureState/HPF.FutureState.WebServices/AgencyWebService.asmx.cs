﻿using System;
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
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation;

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
                    
                    response.FcId = ForeclosureCaseSetBL.Instance.SaveForeclosureCaseSet(request.ForeclosureCaseSet);
                    if (ForeclosureCaseSetBL.Instance.WarningMessage != null)
                    {
                        response.Status = ResponseStatus.Warning;
                        response.Messages = ForeclosureCaseSetBL.Instance.WarningMessage.ExceptionMessages;
                    }
                    else
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
                    bool validCallLodId = ValidateCallLogID(request);
                    if (validCallLodId)
                    {
                        int callLogId = GetCallLogID(request);
                        if (callLogId != 0)
                        {
                            callLogDTO = CallLogBL.Instance.RetrieveCallLog(callLogId);
                        }
                    }
                    if (callLogDTO != null)
                    {
                        CallLogWSReturnDTO callLogWSDTO = ConvertToCallLogWSDTO(callLogDTO);
                        response.CallLog = callLogWSDTO;
                        response.Status = ResponseStatus.Success;
                    }
                    else
                    {
                        response.Status = ResponseStatus.Warning;
                        response.Messages.AddExceptionMessage("Call Log Id does not exist");
                    }
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
                response.Messages = Ex.ExceptionMessages;
                ExceptionProcessor.HandleException(Ex);
            }
            catch (DataAccessException Ex)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.AddExceptionMessage("Data access error.");
                ExceptionProcessor.HandleException(Ex);
            }
            catch (Exception Ex)
            {
                response.Messages.AddExceptionMessage(Ex.Message);
                ExceptionProcessor.HandleException(Ex);
            }
            return response;
        }

        #region private
        private bool ValidateCallLogID(CallLogRetrieveRequest request)
        {
            DataValidationException dataValidationException = new DataValidationException();
            request.callLogId = request.callLogId.Trim();
            if (request.callLogId == null || request.callLogId == string.Empty)
            {
                dataValidationException.ExceptionMessages.AddExceptionMessage("Call Log Id is required");
                throw dataValidationException;
            }
            ValidationResults validationResults = HPFValidator.Validate<CallLogRetrieveRequest>(request);
            if (!validationResults.IsValid)
            {
                dataValidationException.ExceptionMessages.AddExceptionMessage("Call Log Id is invalid");
                throw dataValidationException;
            }
            return true;
        }

        private int GetCallLogID(CallLogRetrieveRequest request)
        {
            int callLogId = 0;
            if (request.callLogId != string.Empty)
            {
                string sCallLogId = request.callLogId.Replace("HPF", "");
                try
                {
                    callLogId = Convert.ToInt32(sCallLogId);
                }
                catch
                {
                    DataValidationException dataValidationException = new DataValidationException();
                    dataValidationException.ExceptionMessages.AddExceptionMessage("Call Log Id is invalid");
                    throw dataValidationException;
                }
            }
            return callLogId;
        }

        

        private CallLogWSReturnDTO ConvertToCallLogWSDTO(CallLogDTO sourceObject)
        {
            CallLogWSReturnDTO destObject = new CallLogWSReturnDTO();
            if (sourceObject.CallId != 0)
                destObject.CallId = "HPF" + Convert.ToString(sourceObject.CallId);

            destObject.AuthorizedInd = sourceObject.AuthorizedInd;
            //destObject.CallCenter = sourceObject.CallCenter;
            destObject.CallSourceCd = sourceObject.CallSourceCd;
            //destObject.CallCenterID = sourceObject.CallCenterID;
            //destObject.CcAgentIdKey = sourceObject.CcAgentIdKey;
            //destObject.CcCallKey = sourceObject.CcCallKey;
            //destObject.DNIS = sourceObject.DNIS;
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
            destObject.ReasonToCall = sourceObject.ReasonToCall;
            destObject.StartDate = sourceObject.StartDate;
            destObject.ServicerId = sourceObject.ServicerId;
            destObject.SelectedAgencyId = sourceObject.SelectedAgencyId;
            destObject.SelectedCounselor = sourceObject.SelectedCounselor;
            //destObject.ScreenRout = sourceObject.ScreenRout;
            //destObject.TransNumber = sourceObject.TransNumber;
            return destObject;
        }
        #endregion
                 
    }
}
