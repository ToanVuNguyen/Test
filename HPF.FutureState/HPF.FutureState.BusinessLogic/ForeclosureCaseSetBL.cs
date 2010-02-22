using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;

using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.DataAccess;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.Utils.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace HPF.FutureState.BusinessLogic
{
    public class ForeclosureCaseSetBL : BaseBusinessLogic
    {
        ForeclosureCaseSetDAO foreclosureCaseSetDAO = ForeclosureCaseSetDAO.CreateInstance();        

        private string _workingUserID;
        
        private bool IsFirstTimeCaseCompleted;
        private bool IsForeclosureCaseInserted;
        //
        ForeclosureCaseSetDTO FCaseSetFromDB = new ForeclosureCaseSetDTO();
        DuplicatedCaseLoanDTOCollection dupeCaseLoans = new DuplicatedCaseLoanDTOCollection();

        public ExceptionMessageCollection WarningMessage { get; private set; } 
    
        /// <summary>
        /// Singleton
        /// </summary>
        public static ForeclosureCaseSetBL Instance
        {
            get
            {
                return new ForeclosureCaseSetBL();
            }
        }

        protected ForeclosureCaseSetBL()
        {

            WarningMessage = new ExceptionMessageCollection();
        }

        #region Send Complete Case to Queue
        private void SendCompletedCaseToQueueIfAny(ForeclosureCaseSetDTO fCaseSetFromAgency)
        {            
            var fcId = fCaseSetFromAgency.ForeclosureCase.FcId;
            //
            try
            {                
                if (WarningMessage.Count > 0) //not completed case
                    return;                
                if (!ShouldSendSummary(fCaseSetFromAgency))
                    return;                
                var queue = new HPFSummaryQueue();
                queue.SendACompletedCaseToQueue(fcId);
            }
            catch(Exception Ex)
            {
                var QUEUE_ERROR_MESSAGE = "Fail to push completed case into Queue : " + fcId;
                //Log
                Logger.Write(QUEUE_ERROR_MESSAGE, "General");
                Logger.Write(Ex.Message, "General");
                Logger.Write(Ex.StackTrace, "General");                
                Logger.Write(QUEUE_ERROR_MESSAGE + " with error message: " + Ex.Message, Constant.DB_LOG_CATEGORY);
                
                //Send E-mail to support
                var hpfSupportEmail = HPFConfigurationSettings.HPF_SUPPORT_EMAIL;
                var mail = new HPFSendMail
                {
                    To = hpfSupportEmail,
                    Subject = QUEUE_ERROR_MESSAGE,
                    Body = "Messsage: " + Ex.Message + "\nTrace: " + Ex.StackTrace
                };
                mail.Send();
                //
            }
        }

        /// <summary>
        /// Check sercure dilivery method of all servicer in CaseloanCollection
        /// if all of them is NOSEND return FALSE
        /// if one of them is not NOSEND return TRUE
        /// <return>bool<return>
        /// </summary>
        private bool ShouldSendSummary(ForeclosureCaseSetDTO fCaseSetFromAcency)
        {
            var fcId = fCaseSetFromAcency.ForeclosureCase.FcId;
            var caseLoan1st = fCaseSetFromAcency.CaseLoans.GetCaseLoan1st();
            var primaryServicer = ServicerBL.Instance.GetServicer(caseLoan1st.ServicerId.Value);
            //
            if (IsFirstTimeCaseCompleted)
                return IsNotNOSENDDeliveryMethod(primaryServicer) &&
                       (fCaseSetFromAcency.ForeclosureCase.ServicerConsentInd == "Y");
            //
            return (IsNotNOSENDDeliveryMethod(primaryServicer) && CheckFieldChange(fCaseSetFromAcency)) &&
                   (fCaseSetFromAcency.ForeclosureCase.ServicerConsentInd == "Y");

        }

        /// <summary>
        /// Track change to send summary to queue
        /// <return>bool<return>
        /// </summary>
        private bool CheckFieldChange(ForeclosureCaseSetDTO fCaseSetFromAcency)
        {
            var fCaseFromAcency = fCaseSetFromAcency.ForeclosureCase;
            var caseloanFromAcency = fCaseSetFromAcency.CaseLoans;
            var caseLoan = FindPrimaryCaseLoan(caseloanFromAcency);
            return (CompareForeClosureCase(fCaseFromAcency) || ComparePrimaryCaseLoan(caseLoan) || CompareBudgetSetTotal(FCaseSetFromDB.BudgetSet, fCaseSetFromAcency.BudgetSet));
        }

        private CaseLoanDTO FindPrimaryCaseLoan(CaseLoanDTOCollection caseloanCollection)
        {
            foreach (CaseLoanDTO item in caseloanCollection)
            {
                if (ConvertStringToUpper(item.Loan1st2nd) == Constant.LOAN_1ST)
                    return item;
            }
            return null;
        }

        /// <summary>
        /// Compare foreclosure case from DB and foreclosure case from Acgency
        /// <return>bool<return>
        /// </summary>
        private bool CompareForeClosureCase(ForeclosureCaseDTO fCaseFromAcency)
        {
            var fCaseFromDB = FCaseSetFromDB.ForeclosureCase;
            return (ConvertStringToUpper(fCaseFromAcency.BorrowerFname) != ConvertStringToUpper(fCaseFromDB.BorrowerFname)
                || ConvertStringToUpper(fCaseFromAcency.BorrowerLname) != ConvertStringToUpper(fCaseFromDB.BorrowerLname)
                || ConvertStringToUpper(fCaseFromAcency.ContactAddr1) != ConvertStringToUpper(fCaseFromDB.ContactAddr1)
                || ConvertStringToUpper(fCaseFromAcency.ContactAddr2) != ConvertStringToUpper(fCaseFromDB.ContactAddr2)
                || ConvertStringToUpper(fCaseFromAcency.ContactCity) != ConvertStringToUpper(fCaseFromDB.ContactCity)
                || ConvertStringToUpper(fCaseFromAcency.ContactStateCd) != ConvertStringToUpper(fCaseFromDB.ContactStateCd)
                || ConvertStringToUpper(fCaseFromAcency.ContactZip) != ConvertStringToUpper(fCaseFromDB.ContactZip)
                || ConvertStringToUpper(fCaseFromAcency.PropAddr1) != ConvertStringToUpper(fCaseFromDB.PropAddr1)
                || ConvertStringToUpper(fCaseFromAcency.PropAddr2) != ConvertStringToUpper(fCaseFromDB.PropAddr2)
                || ConvertStringToUpper(fCaseFromAcency.PropCity) != ConvertStringToUpper(fCaseFromDB.PropCity)
                || ConvertStringToUpper(fCaseFromAcency.PropStateCd) != ConvertStringToUpper(fCaseFromDB.PropStateCd)
                || ConvertStringToUpper(fCaseFromAcency.PropZip) != ConvertStringToUpper(fCaseFromDB.PropZip)
                || ConvertStringToUpper(fCaseFromAcency.PrimaryContactNo) != ConvertStringToUpper(fCaseFromDB.PrimaryContactNo)
                || ConvertStringToUpper(fCaseFromAcency.SecondContactNo) != ConvertStringToUpper(fCaseFromDB.SecondContactNo)
                || ConvertStringToUpper(fCaseFromAcency.Email1) != ConvertStringToUpper(fCaseFromDB.Email1)
                || ConvertStringToUpper(fCaseFromAcency.Email2) != ConvertStringToUpper(fCaseFromDB.Email2)
                );
        }

        /// <summary>
        /// Compare primary case Loan from DB and primary case Loan from Acgency
        /// <return>bool<return>
        /// </summary>
        private bool ComparePrimaryCaseLoan(CaseLoanDTO caseLoan)
        {
            var caseLoanDB = FindPrimaryCaseLoan(FCaseSetFromDB.CaseLoans);
            return (caseLoan.ServicerId != caseLoanDB.ServicerId
                    || ConvertStringToUpper(caseLoan.AcctNum) != ConvertStringToUpper(caseLoanDB.AcctNum)
                );
        }

        /// <summary>
        /// Compare total_income - total_expenses  from DB and total_income - total_expenses  from Acgency
        /// <return>bool<return>
        /// </summary>
        private bool CompareBudgetSetTotal(BudgetSetDTO oldBubgetSet, BudgetSetDTO newSubmitBudgetSet)
        {
            double? bugetSetMinus = oldBubgetSet.TotalIncome - oldBubgetSet.TotalExpenses;
            return (bugetSetMinus != (newSubmitBudgetSet.TotalIncome - newSubmitBudgetSet.TotalExpenses));
        }


        /// <summary>
        /// Check Delivery Method Code
        /// <return>bool<return>
        /// </summary>
        private bool IsNotNOSENDDeliveryMethod(ServicerDTO servicers)
        {
            return (servicers.SummaryDeliveryMethod.ToUpper() != Constant.SECURE_DELIVERY_METHOD_NOSEND);
        }


        #endregion

        #region Implementation of IForclosureCaseBL        

        /// <summary>
        /// Save a ForeclosureCase
        /// </summary>
        /// <param name="foreclosureCaseSet">ForeclosureCaseSetDTO</param>
        public ForeclosureCaseDTO SaveForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {            
            int? fcId;            
            if (foreclosureCaseSet == null || foreclosureCaseSet.ForeclosureCase == null)
                throw new DataValidationException(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0998));


            var exceptionList = CheckRequireForPartial(foreclosureCaseSet);               

            _workingUserID = foreclosureCaseSet.ForeclosureCase.ChgLstUserId;

            var formatDataException = CheckInvalidFormatData(foreclosureCaseSet);
            exceptionList.Add(formatDataException);
            //            
            foreclosureCaseSet = SplitHPFOfCallId(foreclosureCaseSet);

            ForeclosureCaseDTO fcCase = foreclosureCaseSet.ForeclosureCase;

            exceptionList.Add(CheckValidCode(foreclosureCaseSet));
            //
            if (exceptionList.Count > 0)
                ThrowDataValidationException(exceptionList);
            //                
            if (fcCase.FcId.HasValue)
                fcId = ProcessInsertUpdateWithForeclosureCaseId(foreclosureCaseSet);
            else
                fcId = ProcessInsertUpdateWithoutForeclosureCaseId(foreclosureCaseSet);            

            //
            foreclosureCaseSet.ForeclosureCase.FcId = fcId;
            SendCompletedCaseToQueueIfAny(foreclosureCaseSet);
            //
            return foreclosureCaseSet.ForeclosureCase;         
        }

       

        private void RollbackTransaction()
        {
            foreclosureCaseSetDAO.Cancel();
        }

        private void CompleteTransaction()
        {
            foreclosureCaseSetDAO.Commit();
        }

        private void InitiateTransaction()
        {            
            foreclosureCaseSetDAO.Begin();
        }

        /// <summary>
        /// return ForeclosureCase search result 
        /// </summary>
        /// <param name="searchCriteria">search criteria</param>
        /// <returns>collection of ForeclosureCaseWSDTO and collection of exception messages if there are any</returns>
        public ForeclosureCaseSearchResult SearchForeclosureCase(ForeclosureCaseSearchCriteriaDTO searchCriteria, int pageSize)
        {
            var dataValidationException = new DataValidationException();

            ValidateFcCaseSearchCriteriaNotNull(searchCriteria, dataValidationException);

            ValidateFcCaseSearchCriteriaData(searchCriteria, dataValidationException);
            
            if (dataValidationException.ExceptionMessages.Count > 0)
                throw dataValidationException;

            return ForeclosureCaseDAO.CreateInstance().SearchForeclosureCase(searchCriteria, pageSize);
        }


        // <summary>
        // return ForeclosureCase search result 
        // </summary>
        // <param name="searchCriteria">search criteria</param>
        // <returns>collection of ForeclosureCaseWSDTO and collection of exception messages if there are any</returns>
        public ForeclosureCaseSearchResult ICTSearchForeclosureCase(ICTForeclosureCaseSearchRequest searchCriteria, int pageSize)
        {
            ForeclosureCaseSearchResult searchResult = new ForeclosureCaseSearchResult();
            var dataValidationException = new DataValidationException();
            ForeclosureCaseSearchCriteriaDTO searchCriteriaCheck = new ForeclosureCaseSearchCriteriaDTO();

            searchCriteriaCheck.LoanNumber = searchCriteria.LoanNumber;
            searchCriteriaCheck.FirstName = searchCriteria.FirstName;
            searchCriteriaCheck.LastName = searchCriteria.LastName;
            searchCriteriaCheck.PropertyZip = searchCriteria.PropertyZip;

            ValidateFcCaseSearchCriteriaNotNull(searchCriteriaCheck, dataValidationException);

            ValidateFcCaseSearchCriteriaData(searchCriteriaCheck, dataValidationException);

            if (dataValidationException.ExceptionMessages.Count > 0)
                throw dataValidationException;
            
            //search by loan number
            if (!string.IsNullOrEmpty(searchCriteria.LoanNumber))
            {
                ForeclosureCaseSearchCriteriaDTO loanNumberSearch=new ForeclosureCaseSearchCriteriaDTO();
                loanNumberSearch.LoanNumber = searchCriteria.LoanNumber;

                searchResult = ForeclosureCaseDAO.CreateInstance().SearchForeclosureCase(loanNumberSearch, pageSize);

                if (searchResult.SearchResultCount>0)
                    return searchResult;
            }
            //search by zip code, first name, last name
            if (!string.IsNullOrEmpty(searchCriteria.PropertyZip) && !string.IsNullOrEmpty(searchCriteria.LastName)
                && !string.IsNullOrEmpty(searchCriteria.FirstName))
            {
                ForeclosureCaseSearchCriteriaDTO loanNumberFullNameSearch = new ForeclosureCaseSearchCriteriaDTO();
                loanNumberFullNameSearch.PropertyZip = searchCriteria.PropertyZip;
                loanNumberFullNameSearch.LastName = searchCriteria.LastName;
                loanNumberFullNameSearch.FirstName = searchCriteria.FirstName;

                searchResult = ForeclosureCaseDAO.CreateInstance().SearchForeclosureCase(loanNumberFullNameSearch, pageSize);

                if (searchResult.SearchResultCount > 0)
                    return searchResult;
            }

            //search by zip code, last name
            if (!string.IsNullOrEmpty(searchCriteria.PropertyZip) && !string.IsNullOrEmpty(searchCriteria.LastName))
            {
                ForeclosureCaseSearchCriteriaDTO loanNumberLastNameSearch = new ForeclosureCaseSearchCriteriaDTO();
                loanNumberLastNameSearch.PropertyZip = searchCriteria.PropertyZip;
                loanNumberLastNameSearch.LastName = searchCriteria.LastName;

                searchResult = ForeclosureCaseDAO.CreateInstance().SearchForeclosureCase(loanNumberLastNameSearch, pageSize);

                if (searchResult.SearchResultCount > 0)
                    return searchResult;
            }

            return ForeclosureCaseDAO.CreateInstance().SearchForeclosureCase(searchCriteriaCheck, pageSize);
        }

        #endregion

        #region Function to serve SearchForeclosureCase
        private void ValidateFcCaseSearchCriteriaNotNull(ForeclosureCaseSearchCriteriaDTO searchCriteria, DataValidationException dataValidationException)
        {
            if (string.IsNullOrEmpty(searchCriteria.AgencyCaseNumber) &&
                string.IsNullOrEmpty(searchCriteria.FirstName) &&
                string.IsNullOrEmpty(searchCriteria.Last4_SSN) &&
                string.IsNullOrEmpty(searchCriteria.LastName) &&
                string.IsNullOrEmpty(searchCriteria.LoanNumber) &&
                string.IsNullOrEmpty(searchCriteria.PropertyZip) &&
                searchCriteria.Servicer <=0 )
            {                
                dataValidationException.ExceptionMessages.AddExceptionMessage(ErrorMessages.ERR0378, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0378));
            }
        }

        private void ValidateFcCaseSearchCriteriaData(ForeclosureCaseSearchCriteriaDTO searchCriteria, DataValidationException dataValidationException)
        {
            var validationResults = HPFValidator.Validate(searchCriteria);
            if (!validationResults.IsValid)
            {
                foreach (var result in validationResults)
                {

                    string errorCode = string.IsNullOrEmpty(result.Tag) ? "ERROR" : result.Tag;
                    string errorMess = string.IsNullOrEmpty(result.Tag) ? result.Message : ErrorMessages.GetExceptionMessageCombined(result.Tag);
                    dataValidationException.ExceptionMessages.AddExceptionMessage(errorCode, errorMess);
                }
            }
        }

        public ForeclosureCaseCallSearchResultDTOCollection GetForclosureCaseFromCall(int callId)
        {
            return foreclosureCaseSetDAO.GetForclosureCaseFromCall(callId);
        }
        #endregion

        #region Functions to serve SaveForeclosureCaseSet

        private int? ProcessUpdateForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            IsForeclosureCaseInserted = false;
            var exceptionList = MiscErrorException(foreclosureCaseSet);
            if (exceptionList.Count > 0)
                ThrowDataValidationException(exceptionList);            
            dupeCaseLoans = GetDuplicateCases(foreclosureCaseSet);

            ForeclosureCaseDTO fcCase = foreclosureCaseSet.ForeclosureCase;
            ForeclosureCaseDTO dbFcCase = FCaseSetFromDB.ForeclosureCase;
            fcCase.DuplicateInd = dbFcCase.DuplicateInd;
            fcCase.NeverPayReasonCd = dbFcCase.NeverPayReasonCd;
            fcCase.NeverBillReasonCd = dbFcCase.NeverBillReasonCd;
            if (dupeCaseLoans.Count > 0 && fcCase.DuplicateInd == Constant.DUPLICATE_NO)
            {
                fcCase.DuplicateInd = Constant.DUPLICATE_YES;                
                fcCase.NeverBillReasonCd = Constant.NEVER_BILL_REASON_CODE_DUPE;
                fcCase.NeverPayReasonCd = Constant.NEVER_PAY_REASON_CODE_DUPE;
                if (fcCase.AgencyId == Constant.AGENCY_MMI_ID && fcCase.ProgramId == Constant.PROGRAM_ESCALATION_ID)
                {
                    bool existFCIsCompleted = false;
                    foreach (DuplicatedCaseLoanDTO dupcase in dupeCaseLoans)
                    {
                        if (dupcase.FcCompletedDt != null)
                        {
                            existFCIsCompleted = true;
                            break;
                        }
                    }

                    if (existFCIsCompleted)//Existed dupe case is Completed case
                    {//#BUG-432: CASE 2       
                        fcCase.DuplicateInd = Constant.DUPLICATE_YES;
                        fcCase.NeverBillReasonCd = Constant.ESCALATION_COMPLETE_CODE_DUPE;
                        fcCase.NeverPayReasonCd = Constant.ESCALATION_COMPLETE_CODE_DUPE;
                    }
                    else
                    {
                        fcCase.DuplicateInd = Constant.DUPLICATE_NO;
                        fcCase.NeverBillReasonCd = null;
                        fcCase.NeverPayReasonCd = null;
                    }
                }
                WarningMessage.Add(CreateDuplicateCaseWarning(dupeCaseLoans));
            }
            else if (dupeCaseLoans.Count == 0 && fcCase.DuplicateInd.Equals(Constant.DUPLICATE_YES))
            {
                if (string.IsNullOrEmpty(dbFcCase.NeverBillReasonCd) || !dbFcCase.NeverBillReasonCd.Equals(Constant.NEVER_BILL_REASON_CODE_DUPEMAN))
                {
                    fcCase.DuplicateInd = Constant.DUPLICATE_NO;                    
                    fcCase.NeverBillReasonCd = null;                    
                    fcCase.NeverPayReasonCd = null;
                }
            }
            return UpdateForeclosureCaseSet(foreclosureCaseSet);
        }

        private int? ProcessInsertForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            IsForeclosureCaseInserted = true;
            ExceptionMessageCollection exceptionList = MiscErrorException(foreclosureCaseSet);
            if (exceptionList.Count > 0)
                ThrowDataValidationException(exceptionList);            
            dupeCaseLoans = GetDuplicateCases(foreclosureCaseSet);
            foreclosureCaseSet.ForeclosureCase.DuplicateInd = Constant.DUPLICATE_NO;

            if (dupeCaseLoans.Count > 0)//duplicate cases found
            {                
                if (foreclosureCaseSet.ForeclosureCase.ProgramId == Constant.PROGRAM_ESCALATION_ID
                    && foreclosureCaseSet.ForeclosureCase.AgencyId == Constant.AGENCY_MMI_ID)
                { //If MMI agency submit then we do not raise duplicate errors
                    bool existFCIsCompleted = false;
                    foreach(DuplicatedCaseLoanDTO dupcase in dupeCaseLoans )
                    {
                        if (dupcase.FcCompletedDt != null)
                        {
                            existFCIsCompleted = true;
                            break;
                        }
                    }
                    
                    if (existFCIsCompleted)//Existed dupe case is Completed case
                    {//#BUG-432: CASE 2
                        foreclosureCaseSet.ForeclosureCase.DuplicateInd = Constant.DUPLICATE_YES;
                        foreclosureCaseSet.ForeclosureCase.NeverBillReasonCd = Constant.ESCALATION_COMPLETE_CODE_DUPE;
                        foreclosureCaseSet.ForeclosureCase.NeverPayReasonCd = Constant.ESCALATION_COMPLETE_CODE_DUPE;
                    }                    
                }                
                else
                    ThrowDuplicateCaseException(dupeCaseLoans);

            }
                       
            return InsertForeclosureCaseSet(foreclosureCaseSet);
        }

        private int? ProcessInsertUpdateWithoutForeclosureCaseId(ForeclosureCaseSetDTO foreclosureCaseSet)
        {                        
            ForeclosureCaseDTO fcCase = foreclosureCaseSet.ForeclosureCase;            
            //if (string.IsNullOrEmpty(fcCase.AgencyCaseNum) || fcCase.AgencyId == 0)
            //    ThrowDataValidationException(ErrorMessages.ERR0250);
            
            if (CheckExistingAgencyIdAndCaseNumber(fcCase.AgencyId, fcCase.AgencyCaseNum))
                ThrowDataValidationException(ErrorMessages.ERR0254);                            
            
            return ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
        }

        private int? ProcessInsertUpdateWithForeclosureCaseId(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            ForeclosureCaseDTO fc = foreclosureCaseSet.ForeclosureCase;            
            //check fcid in db or not
            FCaseSetFromDB.ForeclosureCase = GetForeclosureCase(fc.FcId);
            
            if (FCaseSetFromDB.ForeclosureCase == null)
                ThrowDataValidationException(ErrorMessages.ERR0251);
            else
            {   //User this data to check data changed or not in Update function and Send FC to queue
                FCaseSetFromDB.CaseLoans = CaseLoanBL.Instance.GetCaseLoanCollection(fc.FcId);
                FCaseSetFromDB.Outcome = OutcomeBL.Instance.GetOutcomeItemCollection(fc.FcId);
                FCaseSetFromDB.BudgetSet = BudgetBL.Instance.GetBudgetSet(fc.FcId);
                FCaseSetFromDB.ProposedBudgetSet = BudgetBL.Instance.GetProposedBudgetSet(fc.FcId);
                FCaseSetFromDB.BudgetAssets = BudgetBL.Instance.GetBudgetAssetSet(fc.FcId);
                FCaseSetFromDB.BudgetItems = BudgetBL.Instance.GetBudgetItemSet(fc.FcId);
                FCaseSetFromDB.ProposedBudgetItems = BudgetBL.Instance.GetProposedBudgetItemSet(fc.FcId);
            }

            //check valid fcCase for Agency
            if (FCaseSetFromDB.ForeclosureCase.AgencyId != fc.AgencyId)
                ThrowDataValidationException(ErrorMessages.ERR0252);

            if (CheckForeclosureCaseDBIsInactiveCase(FCaseSetFromDB.ForeclosureCase))                
                return ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
            else
                return ProcessUpdateForeclosureCaseSet(foreclosureCaseSet);
        }        
        
        #region Functions check min request validate
        private ExceptionMessageCollection ValidationFieldByRuleSet(ForeclosureCaseSetDTO foreclosureCaseSet, string ruleSet)
        {
            ForeclosureCaseDTO foreclosureCase = foreclosureCaseSet.ForeclosureCase;
            CaseLoanDTOCollection caseLoanItem = foreclosureCaseSet.CaseLoans;
            OutcomeItemDTOCollection outcomeItem = foreclosureCaseSet.Outcome;
            BudgetItemDTOCollection budgetItem = foreclosureCaseSet.BudgetItems;           
            
            var msgFcCaseSet = new ExceptionMessageCollection();
            //
            msgFcCaseSet.Add(ValidateFieldsForeclosureCase(foreclosureCase, ruleSet));                        
            //
            msgFcCaseSet.Add(ValidateFieldsCaseLoanItem(caseLoanItem, ruleSet));
            //
            msgFcCaseSet.Add(ValidateFieldsOutcomeItem(outcomeItem, ruleSet));
            //
            if (ruleSet != Constant.RULESET_MIN_REQUIRE_FIELD)
                msgFcCaseSet.Add(ValidateFieldsBudgetItem(budgetItem, ruleSet));
            //
            return msgFcCaseSet;
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 1: Min request validate of Fore Closure Case
        /// 2: Min request validate of Budget Item Collection
        /// 3: Min request validate of Outcome Item Collection
        /// 4: Min request validate of Case Loan Collection        
        /// </summary>
        private ExceptionMessageCollection CheckRequireForPartial(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            return ValidationFieldByRuleSet(foreclosureCaseSet, Constant.RULESET_MIN_REQUIRE_FIELD);
        }      

        /// <summary>
        /// Min request validate the fore closure case set
        /// 1: Min request validate of Fore Closure Case
        /// <return>Collection Message Error</return>
        /// </summary>
        private ExceptionMessageCollection ValidateFieldsForeclosureCase(ForeclosureCaseDTO foreclosureCase, string ruleSet)
        {            
            var  msgFcCaseSet = new ExceptionMessageCollection { HPFValidator.ValidateToGetExceptionMessage(foreclosureCase, ruleSet) };
            
            switch(ruleSet)
            {
                case Constant.RULESET_MIN_REQUIRE_FIELD:                    
                    break;
                case Constant.RULESET_COMPLETE: //BUG-439: only raise warning message completed case from 1/1/2010 when updating
                    if (string.IsNullOrEmpty(foreclosureCase.ErcpOutcomeCd))
                    {
                        bool warn331 = false;                        
                        if (IsForeclosureCaseInserted) //insert case --> always add warning
                            warn331 = true;
                        else if (FCaseSetFromDB.ForeclosureCase.CompletedDt != null && FCaseSetFromDB.ForeclosureCase.CompletedDt >= new DateTime(2010, 1, 1))
                            warn331 = true; //update case have completed date from 1/1/2010

                        if(warn331)
                            msgFcCaseSet.AddExceptionMessage(ErrorMessages.WARN0331, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0331));
                    }
                    break;
            }

            return msgFcCaseSet;
        }

        private ExceptionMessageCollection CheckCrossFields(ForeclosureCaseDTO foreclosureCase)
        {
            var msgFcCaseSet = new ExceptionMessageCollection();
            if (foreclosureCase != null)
            {                
                //-----BankruptcyInd, BankruptcyAttorney, BankruptcyPmtCurrentInd
                if(!CompareString(foreclosureCase.BankruptcyAttorney, null) 
                    && !CompareString(foreclosureCase.BankruptcyInd,Constant.BANKRUPTCY_IND_YES))                
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.WARN0274, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0274));
                if (CompareString(foreclosureCase.BankruptcyInd, Constant.BANKRUPTCY_IND_YES)
                    && CompareString(foreclosureCase.BankruptcyAttorney, null))                
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.WARN0275, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0275));
                if (CompareString(foreclosureCase.BankruptcyInd, Constant.BANKRUPTCY_IND_YES)
                    && CompareString(foreclosureCase.BankruptcyPmtCurrentInd,null))                
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.WARN0276, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0276));
                //-----SummarySentOtherCd, SummarySentOtherDt                
                if (CompareString(foreclosureCase.SummarySentOtherCd, null) && foreclosureCase.SummarySentOtherDt != null)
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.WARN0278, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0278));
                else if (!CompareString(foreclosureCase.SummarySentOtherCd, null) && foreclosureCase.SummarySentOtherDt == null)
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.WARN0279, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0279));
                //-----SrvcrWorkoutPlanCurrentInd
                if (CompareString(foreclosureCase.SrvcrWorkoutPlanCurrentInd, null)
                    && CompareString(foreclosureCase.HasWorkoutPlanInd, Constant.HAS_WORKOUT_PLAN_IND_YES))                
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.WARN0280, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0280));
                //-----HomeSalePrice
                if (CompareString(foreclosureCase.ForSaleInd, Constant.FORSALE_IND_YES) && 
                    foreclosureCase.HomeSalePrice == null)
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.WARN0281, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0281));
            }
            return msgFcCaseSet;
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 2:  Min request validate of Budget Item Collection
        /// <return>Collection Message Error</return>
        /// </summary>
        private ExceptionMessageCollection ValidateFieldsBudgetItem(BudgetItemDTOCollection budgetItemDTOCollection, string ruleSet)
        {
            var msgFcCaseSet = new ExceptionMessageCollection();
            if (budgetItemDTOCollection != null || budgetItemDTOCollection.Count > 0)
            {
                foreach (var item in budgetItemDTOCollection)
                {
                    var ex = HPFValidator.ValidateToGetExceptionMessage(item, ruleSet);
                    if (ex.Count > 0)
                        return ex;
                }
            }
            return msgFcCaseSet;
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 3:  Min request validate of Outcome Item Collection
        /// <return>Collection Message Error</return>
        /// </summary>
        private ExceptionMessageCollection ValidateFieldsOutcomeItem(OutcomeItemDTOCollection outcomeItemDTOCollection, string ruleSet)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if ((outcomeItemDTOCollection == null || outcomeItemDTOCollection.Count < 1) && ruleSet == Constant.RULESET_MIN_REQUIRE_FIELD)
            {
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0129, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0129));
                return msgFcCaseSet;
            }
            int? outComeTypeId = FindOutcomeTypeIdWithNameIsExternalReferral();
            for (int i = 0; i < outcomeItemDTOCollection.Count; i++)
            {
                var item = outcomeItemDTOCollection[i];
                var ex = HPFValidator.ValidateToGetExceptionMessage(item, ruleSet);
                if (ex.Count != 0)
                {
                    for (int j = 0; j < ex.Count; j++)
                    {
                        var exItem = ex[j];
                        msgFcCaseSet.AddExceptionMessage(exItem.ErrorCode, ErrorMessages.GetExceptionMessageCombined(exItem.ErrorCode) + " on outcome item index " + (i + 1));
                    }
                }

            }                  
            if (ruleSet == Constant.RULESET_MIN_REQUIRE_FIELD)
            {
                foreach (OutcomeItemDTO item in outcomeItemDTOCollection)
                {
                    ExceptionMessageCollection msgOthers = CheckOtherFieldOutcomeItemForPartial(outComeTypeId, item);
                    if (msgOthers.Count > 0)
                    {
                        msgFcCaseSet.Add(msgOthers);
                        break;
                    }
                }
            }
            return msgFcCaseSet;
        }

        private ExceptionMessageCollection CheckOtherFieldOutcomeItemForPartial(int? outComeTypeId, OutcomeItemDTO item)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (item.OutcomeTypeId == outComeTypeId && outComeTypeId != null && (CompareString(item.NonprofitreferralKeyNum, null) && CompareString(item.ExtRefOtherName, null)))
            {
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0265, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0265));
            }                        
            return msgFcCaseSet;
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 4:  Min request validate of Case Loan Collection
        /// <return>Collection Message Error</return>
        /// </summary>       
        private ExceptionMessageCollection ValidateFieldsCaseLoanItem(CaseLoanDTOCollection caseLoanDTOCollection,string ruleSet)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            //If caseLoan wasn't submitted by agency
            //Throw Error0126
            if ((caseLoanDTOCollection == null || caseLoanDTOCollection.Count < 1) && ruleSet == Constant.RULESET_MIN_REQUIRE_FIELD)
            {
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0126, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0126));
                return msgFcCaseSet;
            }
            //If caseLoan not null 
            //Check validate
            int? servicerId = Constant.SERVICER_OTHER_ID;//FindServicerIDWithNameIsOther();                        
            for (int i = 0; i < caseLoanDTOCollection.Count; i++)
            {
                var item = caseLoanDTOCollection[i];
                ExceptionMessageCollection ex = HPFValidator.ValidateToGetExceptionMessage(item, ruleSet);
                if (ex.Count != 0)
                {
                    for (int j = 0; j < ex.Count; j++)
                    {
                        var exItem = ex[j];
                        if (exItem.ErrorCode != null)
                            msgFcCaseSet.AddExceptionMessage(exItem.ErrorCode, ErrorMessages.GetExceptionMessageCombined(exItem.ErrorCode) + " on case loan index " + (i + 1));
                        else
                           msgFcCaseSet.AddExceptionMessage("UNKNOWN", "UNKNOWN--" + exItem.Message + " on case loan index " + (i + 1));
                    }
                }
                if (ruleSet == Constant.RULESET_MIN_REQUIRE_FIELD)
                {
                    if (item.ServicerId == null)
                        msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0127, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0127, item.AcctNum));
                    if (CompareString(item.Loan1st2nd, null))
                        msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0130, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0130, item.AcctNum));
                    if (i == 0)
                        msgFcCaseSet.Add(CheckAtLeastFirstMortgages(caseLoanDTOCollection));
                    msgFcCaseSet.Add(CheckOtherFieldCaseLoanForPartial(servicerId, item, i));
                }
                else if (ruleSet == Constant.RULESET_LENGTH && item.InterestRate.HasValue)
                {
                    double testValue = Math.Round(item.InterestRate.Value, 3);
                    if (testValue != item.InterestRate.Value && msgFcCaseSet.GetExceptionMessages(ErrorMessages.ERR0395).Count == 0)
                        msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0395, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0395));
                }
                else if (ruleSet == Constant.RULESET_COMPLETE)
                {
                    bool warning = false;                    
                    if (IsForeclosureCaseInserted) //insert case --> always add warning
                        warning = true;
                    else if (FCaseSetFromDB.ForeclosureCase.CompletedDt != null && FCaseSetFromDB.ForeclosureCase.CompletedDt >= new DateTime(2010, 1, 1))
                        warning = true; //update case have completed date from 1/1/2010

                    if (warning && item.Loan1st2nd == Constant.LOAN_1ST) //only check for 1st loan
                    {
                        if(string.IsNullOrEmpty(item.HarpEligibleInd))
                            msgFcCaseSet.AddExceptionMessage(ErrorMessages.WARN0332, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0332));
                        if (string.IsNullOrEmpty(item.HampEligibleInd))
                            msgFcCaseSet.AddExceptionMessage(ErrorMessages.WARN0333, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0333));
                    }
                }
            }            
            return msgFcCaseSet;
        }

        private ExceptionMessageCollection CheckOtherFieldCaseLoanForPartial(int? servicerId, CaseLoanDTO item, int i)
        {
            var msgFcCaseSet = new ExceptionMessageCollection();
            if (item.ServicerId == servicerId && CompareString(item.OtherServicerName, null) && WarningMessage.GetExceptionMessages(ErrorMessages.WARN0266).Count == 0)
            {
                WarningMessage.AddExceptionMessage(ErrorMessages.WARN0266, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0266));
            }
            else if (item.ServicerId != servicerId)
                item.OtherServicerName = null;
            if ((CompareString(item.MortgageTypeCd, Constant.MORTGATE_TYPE_CODE_ARM)
               || CompareString(item.MortgageTypeCd, Constant.MORTGATE_TYPE_CODE_HYBARM)
               || CompareString(item.MortgageTypeCd, Constant.MORTGATE_TYPE_CODE_POA)
               || CompareString(item.MortgageTypeCd, Constant.MORTGATE_TYPE_CODE_INTONLY))
               && CompareString(item.ArmResetInd, null))
            {
                bool warn282 = true;
                ExceptionMessageCollection exCol = WarningMessage.GetExceptionMessages(ErrorMessages.WARN0282);
                
                foreach (ExceptionMessage ex in exCol)
                {
                    if (ex.Message.IndexOf(item.AcctNum) > 0)
                    {
                        warn282 = false; break;
                    }
                }
                if(warn282)
                    WarningMessage.AddExceptionMessage(ErrorMessages.WARN0282, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0282, item.AcctNum));
            }
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check have least 1st in case loan collection
        /// </summary>
        /// <param name="caseLoan"></param>
        /// <returns></returns>
        private ExceptionMessageCollection CheckAtLeastFirstMortgages(CaseLoanDTOCollection caseLoan)
        {
            int count = 0;
            var msgFcCaseSet = new ExceptionMessageCollection();
            foreach (CaseLoanDTO item in caseLoan)
            {
                if (ConvertStringToUpper(item.Loan1st2nd) == Constant.LOAN_1ST)
                    count = count + 1;
            }
            if (count == 0)
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0273, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0273));
            
            return msgFcCaseSet;
        }

        //private int? FindServicerIDWithNameIsOther()
        //{
        //    var serviers = ServicerBL.Instance.GetServicers();
        //    foreach(var item in serviers)
        //    {
        //        if (ConvertStringToUpper(item.ServicerName) == Constant.SERVICER_OTHER)
        //            return item.ServicerID;
        //    }
        //    return 0;
        //}

        private int? FindOutcomeTypeIdWithNameIsExternalReferral()
        {
            OutcomeTypeDTOCollection outcomeType = OutcomeBL.Instance.GetOutcomeType();
            if (outcomeType != null)
            {
                foreach (OutcomeTypeDTO item in outcomeType)
                {
                    if (ConvertStringToUpper(item.OutcomeTypeName) == Constant.OUTCOME_TYPE_NAME_EXTERNAL_REFERAL)
                        return item.OutcomeTypeID;
                }
            }
            return 0;
        }

        private int FindSubCatWithNameIsMortgage()
        {
            BudgetSubcategoryDTOCollection budgetSubCat = BudgetBL.Instance.GetBudgetSubcategory();
            if (budgetSubCat != null)
            {
                foreach (BudgetSubcategoryDTO item in budgetSubCat)
                {
                    if (ConvertStringToUpper(item.BudgetSubcategoryName) == Constant.SUB_CATEGORY_NAME_MORTGAGE)
                        return item.BudgetSubcategoryID;
                }
            }
            return 0;
        }
        #endregion              

        /// <summary>
        /// Check Inactive(Over one year)
        /// <input>datetime</input>
        /// false: Active
        /// true: Inactive
        /// <return>bool<return>
        /// </summary>
        private bool CheckForeclosureCaseDBIsInactiveCase(ForeclosureCaseDTO foreclosureCase)
        {
            DateTime currentDate = DateTime.Now;
            DateTime backOneYear = DateTime.MinValue;                        
            DateTime? completeDate = foreclosureCase.CompletedDt;

            if (completeDate == null || completeDate == DateTime.MinValue)
                return false;

            //Check leap year
            if (currentDate.Year % 400 == 0 || (currentDate.Year % 100 != 0 && currentDate.Year % 4 == 0))            
                backOneYear = currentDate.AddDays(-367);            
            else            
                backOneYear = currentDate.AddDays(-366);            
            //
            if (backOneYear < completeDate)            
                return false;                            

            return true;
        }      

        /// <summary>
        /// Check Duplicated Fore Closure Case
        /// </summary>
        private DuplicatedCaseLoanDTOCollection GetDuplicateCases(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            return foreclosureCaseSetDAO.GetDuplicatedCases(foreclosureCaseSet);            
        }

        /// <summary>
        /// Check existed AgencyId and Case number
        /// </summary>
        bool CheckExistingAgencyIdAndCaseNumber(int? agencyId, string caseNumner)
        {
            return foreclosureCaseSetDAO.CheckExistingAgencyIdAndCaseNumber(agencyId, caseNumner);
        }

        #region Check Invalid Format Data
        private ExceptionMessageCollection CheckInvalidFormatData(ForeclosureCaseSetDTO fCaseSet)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            //Special character
            ExceptionMessageCollection ex = ValidationFieldByRuleSet(fCaseSet, Constant.RULESET_LENGTH);
            ExceptionMessageCollection msgBudgetAsset = ValidateFieldsBudgetAsset(fCaseSet.BudgetAssets, Constant.RULESET_LENGTH);            
            msgFcCaseSet.Add(ex);
            //            
            msgFcCaseSet.Add(msgBudgetAsset);
            //
            if (!CheckDateOfBirth(fCaseSet.ForeclosureCase.BorrowerDob))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0271, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0271));
            if (!CheckDateOfBirth(fCaseSet.ForeclosureCase.CoBorrowerDob))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0272, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0272));
            if (!CheckSpecialCharacrer(fCaseSet.ForeclosureCase.BorrowerFname))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0267, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0267));
            if (!CheckSpecialCharacrer(fCaseSet.ForeclosureCase.BorrowerLname))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0268, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0268));
            if (!CheckSpecialCharacrer(fCaseSet.ForeclosureCase.CoBorrowerFname))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0269, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0269));
            if (!CheckSpecialCharacrer(fCaseSet.ForeclosureCase.CoBorrowerLname))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0270, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0270));
            if (!CheckCallID(fCaseSet.ForeclosureCase.CallId))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0283, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0283));            
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check Special characrer in Name
        /// </summary>
        private bool CheckSpecialCharacrer(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;
            Regex regex = new Regex(@"[!@#$%^*(){}|:;?><567890]");
            if (regex.IsMatch(s))
                return false;
            return true;
        }

        /// <summary>
        /// Check MaxLength for budgetAsset
        /// </summary>
        private ExceptionMessageCollection ValidateFieldsBudgetAsset(BudgetAssetDTOCollection budgetAssetDTOCollection, string ruleSet)
        {
            var msgFcCaseSet = new ExceptionMessageCollection();           
            for (int i = 0; i < budgetAssetDTOCollection.Count; i++)
            {
                var item = budgetAssetDTOCollection[i];
                var ex = HPFValidator.ValidateToGetExceptionMessage(item, ruleSet);
                if (ex.Count != 0)
                {
                    for (int j = 0; j < ex.Count; j++)
                    {
                        var exItem = ex[j];
                        msgFcCaseSet.AddExceptionMessage(exItem.ErrorCode, ErrorMessages.GetExceptionMessageCombined(exItem.ErrorCode) + " working on budget asset index " + (i + 1));
                    }
                }
                    
            }
            return msgFcCaseSet;
        }


        /// <summary>
        /// Check Old between 12 and 110
        /// </summary>
        private bool CheckDateOfBirth(DateTime? dateOfBirth)
        {
            int systemYear = DateTime.Now.Year;
            if (dateOfBirth != null && dateOfBirth != DateTime.MinValue)
            {
                int yearOfBirth = dateOfBirth.Value.Year;
                int calculateYear = systemYear - yearOfBirth;
                if (calculateYear >= 12 && calculateYear <= 110)
                    return true;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check invalid Call id and convert Call ID
        /// </summary>
        private bool CheckCallID(string callId)
        {
            string callID = callId;
            if (callID == null || callID == string.Empty)
                return true;

            callID = callID.Trim();
            if (callID.Length >= 4 && callID.Substring(0, 3).ToUpper() == "HPF")
            {                
                try
                {
                    string temp = callID.Substring(3, callID.Length - 3);
                    int.Parse(temp);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        private ForeclosureCaseSetDTO SplitHPFOfCallId(ForeclosureCaseSetDTO fCaseSet)
        {
            string callID = fCaseSet.ForeclosureCase.CallId;
            if (string.IsNullOrEmpty(callID))
                return fCaseSet;
            else if(CheckCallID(callID))
                fCaseSet.ForeclosureCase.CallId = callID.Substring(3, callID.Length - 3);
            return fCaseSet;
        }
        #endregion       

        #region Functions Check MiscError
        /// <summary>
        /// Check Misc Error Exception
        /// </summary>
        /// <param name="foreclosureCaseSet"></param>
        /// <returns></returns>
        private ExceptionMessageCollection MiscErrorException(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            var msgFcCaseSet = new ExceptionMessageCollection();            
            bool caseComplete = CheckForeclosureCaseDBComplete(FCaseSetFromDB);            
            //Cannot Un-complete a Previously Completed Case
            WarningMessage.Add(CheckUnCompleteCaseComplete(foreclosureCaseSet, caseComplete));
            //Two First Mortgages Not Allowed in a Case AND Case Loan must have atleast 1st (If case completed)
            msgFcCaseSet.Add(CheckFirstMortgages(foreclosureCaseSet));
            //Cannot resubmit the case complete without billable outcome
            msgFcCaseSet.Add(CheckBillableOutCome(foreclosureCaseSet, caseComplete));
            //Budget Item must have atleast 1 budget_item = 'Mortgage Amount'.(If case completed)
            CheckMortgageBudgetItem(foreclosureCaseSet, caseComplete);
            //Budget Item valid or not
            msgFcCaseSet.Add(CheckBugetItemIsValid(foreclosureCaseSet));
            WarningMessage.Add(CheckCrossFields(foreclosureCaseSet.ForeclosureCase));
            if (caseComplete && WarningMessage.Count != 0 && msgFcCaseSet.GetExceptionMessages(ErrorMessages.ERR0255).Count == 0)
            {
                var msg = new ExceptionMessage
                              {
                                  ErrorCode = ErrorMessages.ERR0255,
                                  Message = ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0255)
                              };
                msgFcCaseSet.Insert(0,msg);
            }                    
            return msgFcCaseSet;
        }

        private ExceptionMessageCollection CheckBugetItemIsValid(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            var budgetCollection = foreclosureCaseSet.BudgetItems;
            var msgFcCaseSet = new ExceptionMessageCollection();
            if (budgetCollection == null || budgetCollection.Count < 1)
                return msgFcCaseSet;
            for (int i = 0; i < budgetCollection.Count; i++)
            {
                var item = budgetCollection[i];
                if (item.BudgetItemAmt == null)
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0079, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0079) +  " on budget item index " + (i + 1));
                if (item.BudgetSubcategoryId == null)
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0262, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0262) + " on budget item index " + (i + 1));
            }            
            return msgFcCaseSet;
        }
        /// <summary>
        /// Check Misc Error Exception
        /// Case 1: Cannot Un-complete a Previously Completed Case  
        /// return TRUE if have Error
        /// </summary>        
        private ExceptionMessageCollection CheckUnCompleteCaseComplete(ForeclosureCaseSetDTO foreclosureCaseSetInput, bool caseComplete)
        {   
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            /*ExceptionMessageCollection msgRequire = ValidationFieldByRuleSet(foreclosureCaseSetInput, Constant.RULESET_MIN_REQUIRE_FIELD);
            if (caseComplete)
                msgFcCaseSet.Add(msgRequire);*/
            //
            ExceptionMessageCollection msgComplete = ValidationFieldByRuleSet(foreclosureCaseSetInput, Constant.RULESET_COMPLETE);
            //if (caseComplete)            
            msgFcCaseSet.Add(msgComplete);            
            
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check Case in Database is NULL or Complete Or Not Complete
        /// Null = Not Complete => return false
        /// CompleteDt != null => complete => return true
        /// </summary>     
        private bool CheckForeclosureCaseDBComplete(ForeclosureCaseSetDTO fc)
        {            
            bool caseComplete = false;
            //use for send queue            
            IsFirstTimeCaseCompleted = true;
            if (fc.ForeclosureCase != null && fc.ForeclosureCase.CompletedDt != null && !CheckForeclosureCaseDBIsInactiveCase(fc.ForeclosureCase))
            {
                caseComplete = true;
                IsFirstTimeCaseCompleted = false;                
            }
            return caseComplete;
        }

        /// <summary>
        /// Check Misc Error Exception
        /// Case 2: Two First Mortgages Not Allowed in a Case         
        /// return TRUE if have Error
        /// </summary>
        private ExceptionMessageCollection CheckFirstMortgages(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {
            int count = 0;
            CaseLoanDTOCollection caseLoan = foreclosureCaseSetInput.CaseLoans;
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            foreach (CaseLoanDTO item in caseLoan)
            {
                if (ConvertStringToUpper(item.Loan1st2nd) == Constant.LOAN_1ST)                
                    count = count + 1;                
            }            
            if(count > 1)
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0256, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0256));                        
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check Misc Error Exception
        /// Case 3: Cannot resubmit the case complete without billable outcome        
        /// return TRUE if have Error
        /// </summary>
        private ExceptionMessageCollection CheckBillableOutCome(ForeclosureCaseSetDTO foreclosureCaseSetInput, bool caseComplete)
        {   
            OutcomeItemDTOCollection outcome = foreclosureCaseSetInput.Outcome;
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            bool isBillable = false;
            for (int i = 0; i < outcome.Count; i++)
            {
                OutcomeItemDTO item = outcome[i];
                isBillable = CheckBillableOutcome(item);
                if (isBillable) break;
            }
            if (!isBillable && caseComplete)
            {
                if (msgFcCaseSet.GetExceptionMessages(ErrorMessages.ERR0255).Count == 0)                    
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0255, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0255));
            }
            if (!isBillable)
                WarningMessage.AddExceptionMessage(ErrorMessages.WARN0326, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0326));
            
            return msgFcCaseSet;
        }


        private bool CheckBillableOutcome(OutcomeItemDTO outcome)
        {
            OutcomeTypeDTOCollection outcomeType = OutcomeBL.Instance.GetOutcomeType();            
            foreach (OutcomeTypeDTO item in outcomeType)
            {
                if (item.OutcomeTypeID == outcome.OutcomeTypeId && ConvertStringToUpper(item.PayableInd) == Constant.PAYABLE_IND)
                    return true;
            }
            return false;
        }

        private void CheckMortgageBudgetItem(ForeclosureCaseSetDTO foreclosureCaseSetInput, bool caseComplete)
        {                        
            CheckBudgetItemHaveMortgage(foreclosureCaseSetInput);             
        }

        /// <summary>
        /// Check BudgetItem have least  item Mortgate
        /// </summary>
        /// <param name="foreclosureCaseSetInput"></param>
        /// <returns></returns>
        private void CheckBudgetItemHaveMortgage(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {
            bool isInvalidAmount = false;
            bool isFoundMortgage = false;
            BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSetInput.BudgetItems;
            if (budgetItemCollection == null || budgetItemCollection.Count < 1)
            {
                WarningMessage.AddExceptionMessage(ErrorMessages.WARN0329, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0329));
                return;
            }

            int subCatId = FindSubCatWithNameIsMortgage();
            for(int i=0;i<budgetItemCollection.Count; i++)
            {
                BudgetItemDTO item = budgetItemCollection[i];
                if (item.BudgetSubcategoryId == subCatId)
                {
                    isFoundMortgage = true;
                    if (item.BudgetItemAmt <= 0)
                    {
                        isInvalidAmount = true;
                        break;
                    }
                }
            }

            if ((isFoundMortgage && isInvalidAmount) || !isFoundMortgage)
                WarningMessage.AddExceptionMessage(ErrorMessages.WARN0327, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0327));            
        }
       
        #endregion        

        #region Function Update Fore Closure Case Set
        /// <summary>
        /// Update the Fore Closure Case
        /// </summary>
        private int? UpdateForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            ForeclosureCaseDTO foreclosureCase = AssignForeclosureCaseHPFAuto(foreclosureCaseSet);
            CaseLoanDTOCollection caseLoanCollection = foreclosureCaseSet.CaseLoans;
            OutcomeItemDTOCollection outcomeItemCollection = foreclosureCaseSet.Outcome;
            BudgetSetDTO budgetSet = AssignBudgetSetHPFAuto(foreclosureCaseSetDAO, foreclosureCaseSet.BudgetAssets, foreclosureCaseSet.BudgetItems);
            foreclosureCaseSet.BudgetSet = budgetSet;
            //
            BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSet.BudgetItems;

            BudgetAssetDTOCollection budgetAssetCollection = foreclosureCaseSet.BudgetAssets;
            int? fcId = 0;
            try
            {
                InitiateTransaction();
                //Insert table Foreclosure_Case
                //Return Fc_id
                fcId = UpdateForeClosureCase(foreclosureCaseSetDAO, foreclosureCase);

                //check changed from budgetItem and budget asset
                //if they are changed, insert new budget set, budget Item and budget asset
                InsertBudget(foreclosureCaseSetDAO, budgetSet, budgetItemCollection, budgetAssetCollection, fcId);

                BudgetSetDTO proposedBudgetSet = AssignBudgetSetHPFAuto(foreclosureCaseSetDAO, null, foreclosureCaseSet.ProposedBudgetItems);
                InsertProposedBudget(foreclosureCaseSetDAO, budgetSet, foreclosureCaseSet.ProposedBudgetItems, fcId);
                //check outcome item Input with outcome item DB
                //if not exist, insert new
                OutcomeItemDTOCollection outcomeCollecionNew = null;                

                outcomeCollecionNew = CheckOutcomeItemInputwithDB(foreclosureCaseSetDAO, outcomeItemCollection, FCaseSetFromDB.Outcome, fcId);
                InsertOutcomeItem(foreclosureCaseSetDAO, outcomeCollecionNew, fcId);

                //check outcome item DB with outcome item input
                //if not exit, update outcome_deleted_dt = today()
                outcomeCollecionNew = CheckOutcomeItemDBwithInput(foreclosureCaseSetDAO, outcomeItemCollection, FCaseSetFromDB.Outcome, fcId);
                UpdateOutcome(foreclosureCaseSetDAO, outcomeCollecionNew);                

                //Get list case loan wil be deleted
                CaseLoanDTOCollection caseLoanDeleteCollecion = CheckCaseLoanForDelete(foreclosureCaseSetDAO, caseLoanCollection, FCaseSetFromDB.CaseLoans, fcId);

                //Get list case loan wil be updated
                CaseLoanDTOCollection caseLoanUpdateCollecion = CheckCaseLoanForUpdate(foreclosureCaseSetDAO, caseLoanCollection, FCaseSetFromDB.CaseLoans, fcId);

                //Get list case loan wil be inserted
                CaseLoanDTOCollection caseLoanInsertCollecion = CheckCaseLoanForInsert(foreclosureCaseSetDAO, caseLoanCollection, FCaseSetFromDB.CaseLoans, fcId);

                // Set Change Acc Num For CaseLoan Insertd
                caseLoanInsertCollecion = SetChangeAccNumForCaseLoan(caseLoanDeleteCollecion, caseLoanInsertCollecion);

                //Delete Case Loan
                DeleteCaseLoan(foreclosureCaseSetDAO, caseLoanDeleteCollecion);
                
                //Update Case Loan
                UpdateCaseLoan(foreclosureCaseSetDAO, caseLoanUpdateCollecion);
                
                //Insert Case Loan
                InsertCaseLoan(foreclosureCaseSetDAO, caseLoanInsertCollecion, fcId);
                //                               

                if (foreclosureCaseSet.ForeclosureCase.ProgramId == Constant.PROGRAM_ESCALATION_ID
                    && foreclosureCaseSet.ForeclosureCase.AgencyId == Constant.AGENCY_MMI_ID)
                { //#BUG-432: CASE 1: mark partial cases DUPEESCP
                    string fcIds = "";
                    foreach (DuplicatedCaseLoanDTO dupCaseLoan in dupeCaseLoans)
                    {
                        if (fcIds.Length > 0)
                            fcIds += "," + dupCaseLoan.FcID.Value;
                        else
                            fcIds += dupCaseLoan.FcID.Value;
                    }
                    if (fcIds.Length > 0)//Update the dupe cases if duplicated cases found
                        foreclosureCaseSetDAO.MarkDuplicateCases(fcIds, foreclosureCase.ChangeLastUserId, Constant.DUPLICATE_YES, Constant.ESCALATION_PARTIAL_CODE_DUPE, Constant.ESCALATION_PARTIAL_CODE_DUPE);
                }  
                CompleteTransaction();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            return fcId;
        }
        
        /// <summary>
        /// Update foreclosureCase
        /// </summary>
        private int? UpdateForeClosureCase(ForeclosureCaseSetDAO foreClosureCaseSetDAO, ForeclosureCaseDTO foreclosureCase)
        {
            foreclosureCase.SetUpdateTrackingInformation(_workingUserID);
            return foreClosureCaseSetDAO.UpdateForeclosureCase(foreclosureCase);
            
        }
        #endregion

        #region Function Insert Fore Closure Case Set
        /// <summary>
        /// Insert the Fore Closure Case
        /// </summary>
        private int? InsertForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            ForeclosureCaseDTO foreclosureCase = AssignForeclosureCaseHPFAuto(foreclosureCaseSet);
            CaseLoanDTOCollection caseLoanCollection = foreclosureCaseSet.CaseLoans;
            OutcomeItemDTOCollection outcomeItemCollection = AssignOutcomeHPFAuto(foreclosureCaseSet);
            BudgetSetDTO budgetSet = AssignBudgetSetHPFAuto(foreclosureCaseSetDAO, foreclosureCaseSet.BudgetAssets, foreclosureCaseSet.BudgetItems);
            foreclosureCaseSet.BudgetSet = budgetSet;
            //
            BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSet.BudgetItems;

            BudgetAssetDTOCollection budgetAssetCollection = foreclosureCaseSet.BudgetAssets;
            int? fcId = 0;
            try
            {
                InitiateTransaction();
                //Insert table Foreclosure_Case
                //Return Fc_id
                fcId = InsertForeclosureCase(foreclosureCaseSetDAO, foreclosureCase);

                //Insert table Case Loan
                InsertCaseLoan(foreclosureCaseSetDAO, caseLoanCollection, fcId);

                //Insert Table Outcome Item
                InsertOutcomeItem(foreclosureCaseSetDAO, outcomeItemCollection, fcId);

                //Insert Table Budget Set
                //Return Budget Set Id
                int? budgetSetId = InsertBudgetSet(foreclosureCaseSetDAO, budgetSet, fcId);
                //Insert Table Budget Item
                InsertBudgetItem(foreclosureCaseSetDAO, budgetItemCollection, budgetSetId);
                //Insert table Budget Asset
                InsertBudgetAsset(foreclosureCaseSetDAO, budgetAssetCollection, budgetSetId);

                BudgetSetDTO proposedBudgetSet = AssignBudgetSetHPFAuto(foreclosureCaseSetDAO, null, foreclosureCaseSet.ProposedBudgetItems);
                int? proposedBudgetSetId = InsertProposedBudgetSet(foreclosureCaseSetDAO, proposedBudgetSet, fcId);
                InsertProposedBudgetItem(foreclosureCaseSetDAO, foreclosureCaseSet.ProposedBudgetItems, proposedBudgetSetId);
                //            
                if (foreclosureCaseSet.ForeclosureCase.ProgramId == Constant.PROGRAM_ESCALATION_ID
                    && foreclosureCaseSet.ForeclosureCase.AgencyId == Constant.AGENCY_MMI_ID)
                { //#BUG-432: CASE 1: mark partial cases DUPEESCP
                    string fcIds = "";
                    foreach (DuplicatedCaseLoanDTO dupCaseLoan in dupeCaseLoans)
                    {
                        if (fcIds.Length > 0)
                            fcIds += "," + dupCaseLoan.FcID.Value;
                        else
                            fcIds += dupCaseLoan.FcID.Value;
                    }
                    if (fcIds.Length > 0)//Update the dupe cases if duplicated cases found
                        foreclosureCaseSetDAO.MarkDuplicateCases(fcIds, foreclosureCase.CreateUserId, Constant.DUPLICATE_YES, Constant.ESCALATION_PARTIAL_CODE_DUPE, Constant.ESCALATION_PARTIAL_CODE_DUPE);

                }  
                CompleteTransaction();
            }
            catch(Exception ex)
            {
                RollbackTransaction();
                throw ex;
            }
            return fcId;
        }       
        
        #endregion

        #region Functions for Insert and Update tables
        /// <summary>
        /// Insert Budget
        /// </summary>
        private void InsertBudget(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetSetDTO budgetSet, BudgetItemDTOCollection budgetItemCollection, BudgetAssetDTOCollection budgetAssetCollection, int? fcId)
        {
            bool isInsertBudget = IsInsertBudgetSet(FCaseSetFromDB.BudgetItems, budgetItemCollection, FCaseSetFromDB.BudgetAssets, budgetAssetCollection, fcId);
            if (isInsertBudget)
            {
                //Insert Table Budget Set
                //Return Budget Set Id
                int? budget_set_id = InsertBudgetSet(foreClosureCaseSetDAO, budgetSet, fcId);
                //Insert Table Budget Item
                InsertBudgetItem(foreClosureCaseSetDAO, budgetItemCollection, budget_set_id);
                //Insert table Budget Asset
                InsertBudgetAsset(foreClosureCaseSetDAO, budgetAssetCollection, budget_set_id);
            }
        }

        private void InsertProposedBudget(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetSetDTO budgetSet, BudgetItemDTOCollection budgetItemCollection, int? fcId)
        {
            bool isInsertBudget = IsInsertBudgetSet(FCaseSetFromDB.ProposedBudgetItems, budgetItemCollection, null, null, fcId);
            if (isInsertBudget)
            {
                //Insert Table Budget Set
                //Return Budget Set Id
                int? budget_set_id = InsertProposedBudgetSet(foreClosureCaseSetDAO, budgetSet, fcId);
                //Insert Table Budget Item
                InsertProposedBudgetItem(foreClosureCaseSetDAO, budgetItemCollection, budget_set_id);                
            }
        }
        /// <summary>
        /// Insert Budget Asset
        /// </summary>
        private void InsertBudgetAsset(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetAssetDTOCollection budgetAssetCollection, int? budget_set_id)
        {
            if (budgetAssetCollection != null)
            {
                foreach (BudgetAssetDTO items in budgetAssetCollection)
                {
                    items.SetInsertTrackingInformation(_workingUserID);
                    foreClosureCaseSetDAO.InsertBudgetAsset(items, budget_set_id);
                }
            }
        }

        /// <summary>
        /// Insert Budget Item
        /// </summary>
        private void InsertBudgetItem(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetItemDTOCollection budgetItemCollection, int? budget_set_id)
        {
            if (budgetItemCollection != null)
            {
                foreach (BudgetItemDTO items in budgetItemCollection)
                {
                    items.SetInsertTrackingInformation(_workingUserID);
                    foreClosureCaseSetDAO.InsertBudgetItem(items, budget_set_id);
                }
            }
        }

        /// <summary>
        /// Insert Budget Item
        /// </summary>
        private void InsertProposedBudgetItem(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetItemDTOCollection budgetItemCollection, int? budget_set_id)
        {
            if (budgetItemCollection != null)
            {
                foreach (BudgetItemDTO items in budgetItemCollection)
                {
                    items.SetInsertTrackingInformation(_workingUserID);
                    foreClosureCaseSetDAO.InsertProposedBudgetItem(items, budget_set_id);
                }
            }
        }

        /// <summary>
        /// Insert Budget Set
        /// </summary>
        private int? InsertBudgetSet(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetSetDTO budgetSet, int? fcId)
        {
            int? budget_set_id = null;
            if(budgetSet != null)
            {
                budgetSet.SetInsertTrackingInformation(_workingUserID);
                budget_set_id = foreClosureCaseSetDAO.InsertBudgetSet(budgetSet, fcId);
            }
            return budget_set_id;
        }

        /// <summary>
        /// Insert Budget Set
        /// </summary>
        private int? InsertProposedBudgetSet(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetSetDTO budgetSet, int? fcId)
        {
            int? budget_set_id = null;
            if (budgetSet != null)
            {
                budgetSet.SetInsertTrackingInformation(_workingUserID);
                budget_set_id = foreClosureCaseSetDAO.InsertProposedBudgetSet(budgetSet, fcId);
            }
            return budget_set_id;
        }

        /// <summary>
        /// Insert ForeclosureCase
        /// </summary>
        private int? InsertForeclosureCase(ForeclosureCaseSetDAO foreClosureCaseSetDAO, ForeclosureCaseDTO foreclosureCase)
        {
            int? fcId = null;
            if(foreclosureCase != null)
            {
                foreclosureCase.SetInsertTrackingInformation(_workingUserID);
                fcId  = foreClosureCaseSetDAO.InsertForeclosureCase(foreclosureCase);
            }
            return fcId;
        }

        /// <summary>
        /// Insert OutcomeItem
        /// </summary>
        private void InsertOutcomeItem(ForeclosureCaseSetDAO foreClosureCaseSetDAO, OutcomeItemDTOCollection outcomeItemCollection, int? fcId)
        {
            if (outcomeItemCollection != null)
            {
                foreach (OutcomeItemDTO items in outcomeItemCollection)
                {
                    items.SetInsertTrackingInformation(_workingUserID);
                    items.OutcomeDt = DateTime.Now;
                    foreClosureCaseSetDAO.InsertOutcomeItem(items, fcId);
                }
            }
        }

        /// <summary>
        /// Insert CaseLoan
        /// </summary>
        private void InsertCaseLoan(ForeclosureCaseSetDAO foreClosureCaseSetDAO, CaseLoanDTOCollection caseLoanCollection, int? fcId)
        {
            if (caseLoanCollection != null)
            {
                foreach (CaseLoanDTO items in caseLoanCollection)
                {
                    items.SetInsertTrackingInformation(_workingUserID);
                    foreClosureCaseSetDAO.InsertCaseLoan(items, fcId);
                }
            }
        }        

        /// <summary>
        /// Update CaseLoan
        /// </summary>
        private void UpdateCaseLoan(ForeclosureCaseSetDAO foreClosureCaseSetDAO, CaseLoanDTOCollection caseLoanCollecion)
        {
            if (caseLoanCollecion != null)
            {                
                foreach (CaseLoanDTO items in caseLoanCollecion)
                {
                    items.SetUpdateTrackingInformation(_workingUserID);
                    foreClosureCaseSetDAO.UpdateCaseLoan(items);
                }
            }
        }

        /// <summary>
        /// Delete CaseLoan
        /// </summary>
        private void DeleteCaseLoan(ForeclosureCaseSetDAO foreClosureCaseSetDAO, CaseLoanDTOCollection caseLoanCollecion)
        {
            if (caseLoanCollecion != null && caseLoanCollecion.Count != 0)
            {
                foreach (CaseLoanDTO items in caseLoanCollecion)
                {
                    foreClosureCaseSetDAO.DeleteCaseLoan(items);
                }
            }
        }

        /// <summary>
        /// Update Outcome
        /// </summary>
        private void UpdateOutcome(ForeclosureCaseSetDAO foreClosureCaseSetDAO, OutcomeItemDTOCollection outcomeCollecion)
        {
            if (outcomeCollecion != null)
            {
                foreach (OutcomeItemDTO items in outcomeCollecion)
                {
                    items.SetUpdateTrackingInformation(_workingUserID);
                    foreClosureCaseSetDAO.UpdateOutcomeItem(items);
                }
            }
        }      

        #endregion

        #region Functions check for insert Budget_*
        /// <summary>
        /// Check Budget Item input with Budget Item from DB
        /// If difference Insert New Budget Items
        /// VS: Do nothing
        /// </summary>
        /// <param name>BudgetItemDTOCollection</param>
        /// <returns>true: if have difference</returns>
        private bool IsBudgetItemsDifference(BudgetItemDTOCollection budgetCollectionDB, BudgetItemDTOCollection budgetCollectionInput, int? fcId)
        {            
            if ((budgetCollectionInput != null && budgetCollectionDB == null) || (budgetCollectionInput == null && budgetCollectionDB != null))
                return true;            
            if (budgetCollectionDB != null && budgetCollectionInput != null && budgetCollectionDB.Count != budgetCollectionInput.Count)
                return true;
            if (budgetCollectionDB != null && budgetCollectionInput != null && budgetCollectionDB.Count == budgetCollectionInput.Count)
            {
                foreach (BudgetItemDTO budgetItem in budgetCollectionInput)
                {
                    if (!CompareBudgetItem(budgetItem, budgetCollectionDB))                    
                        return true;                    
                }
            }
            return false;
        }
        
        /// <summary>
        /// Compare budgetItem input with BudgetItem from DB        
        /// <returns>false: if have difference</returns>
        private bool CompareBudgetItem(BudgetItemDTO budgetItemInput, BudgetItemDTOCollection budgetCollectionDB)
        {
            foreach (BudgetItemDTO budgetItemDB in budgetCollectionDB)
            {
                if (budgetItemInput.BudgetSubcategoryId == budgetItemDB.BudgetSubcategoryId
                    && budgetItemInput.BudgetItemAmt == budgetItemDB.BudgetItemAmt
                    && CompareString(budgetItemInput.BudgetNote,budgetItemDB.BudgetNote))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check Budget Asset input with Budget Asset from DB
        /// If difference Insert New Budget Assets
        /// VS: Do nothing
        /// </summary>
        /// <param name>BudgetAssetDTOCollection</param>
        /// <returns>true: if have difference</returns>
        private bool IsBudgetAssetDifference(BudgetAssetDTOCollection budgetCollectionDB, BudgetAssetDTOCollection budgetCollectionInput, int? fcId)
        {            
            if ((budgetCollectionInput != null && budgetCollectionDB == null) || (budgetCollectionInput == null && budgetCollectionDB != null))
                return true;
            if (budgetCollectionDB != null && budgetCollectionInput != null && budgetCollectionDB.Count != budgetCollectionInput.Count)
                return true;
            if (budgetCollectionDB != null && budgetCollectionInput != null && budgetCollectionDB.Count == budgetCollectionInput.Count)
            {
                foreach (BudgetAssetDTO budgetAssetInput in budgetCollectionInput)
                {
                    if (!CompareBudgetAsset(budgetAssetInput, budgetCollectionDB))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Compare budgetAsset input with BudgetAsset from DB        
        /// <returns>false: if have difference</returns>
        private bool CompareBudgetAsset(BudgetAssetDTO budgetAssetInput, BudgetAssetDTOCollection budgetCollectionDB)
        {
            foreach (BudgetAssetDTO budgetAssetDB in budgetCollectionDB)
            {
                if (CompareString(budgetAssetInput.AssetName, budgetAssetDB.AssetName)
                    && budgetAssetInput.AssetValue == budgetAssetDB.AssetValue)
                    return true;
            }
            return false;             
        }

        /// <summary>
        /// Check for Insert new budget Set
        /// If budgetItem changed or budgetAsset change insert new
        /// VS: do Nothing
        /// </summary>
        /// <param name>BudgetAssetDTOCollection,BudgetAssetDTOCollection, fc_id </param>
        /// <returns>true: if have difference</returns>
        private bool IsInsertBudgetSet(BudgetItemDTOCollection budgetCollectionDB, BudgetItemDTOCollection budgetItemCollection, BudgetAssetDTOCollection budgetAssetCollectionDB, BudgetAssetDTOCollection budgetAssetCollection, int? fcId)
        {
            bool budgetItem = IsBudgetItemsDifference(budgetCollectionDB, budgetItemCollection, fcId);
            bool budgetAsset = IsBudgetAssetDifference(budgetAssetCollectionDB, budgetAssetCollection, fcId);
            return (budgetItem || budgetAsset);                
        }
        #endregion

        #region Functions check for insert or update Outcome Item
        /// <summary>        
        /// Check exist of outcome item input with DB
        /// If not exist add OutcomeItemDTOCollection
        /// finally, return OutcomeItemDTOCollection
        /// </summary>
        /// <param name>OutcomeItemDTOCollection, fc_id </param>
        /// <returns>new OutcomeItemDTOCollection use for Insert</returns>
        private OutcomeItemDTOCollection CheckOutcomeItemInputwithDB(ForeclosureCaseSetDAO foreClosureCaseSetDAO, OutcomeItemDTOCollection outcomeItemCollectionInput, OutcomeItemDTOCollection outcomeItemCollectionDB, int? fcId)
        {
            OutcomeItemDTOCollection outcomeItemNew = new OutcomeItemDTOCollection();
            //OutcomeItemDTOCollection outcomeItemCollectionDB = foreClosureCaseSetDAO.GetOutcomeItemCollection(fcId);
            //Compare OutcomeItem input with OutcomeItem DB
            if (outcomeItemCollectionDB != null)
            {
                foreach (OutcomeItemDTO itemInput in outcomeItemCollectionInput)
                {
                    bool isChanged = CheckOutcomeItem(itemInput, outcomeItemCollectionDB);
                    if (!isChanged)
                        outcomeItemNew.Add(itemInput);
                }
            }
            else
            {
                outcomeItemNew = outcomeItemCollectionInput;
            }
            return outcomeItemNew;   
        }

        /// <summary>        
        /// Check exist of outcome item DB with input
        /// If not exist, Update outcome_deleted_dt
        /// after that add OutcomeItemDTOCollection
        /// finally, return OutcomeItemDTOCollection
        /// </summary>
        /// <param name>OutcomeItemDTOCollection, fc_id </param>
        /// <returns>new OutcomeItemDTOCollection use for Insert</returns>
        private OutcomeItemDTOCollection CheckOutcomeItemDBwithInput(ForeclosureCaseSetDAO foreClosureCaseSetDAO, OutcomeItemDTOCollection outcomeItemCollectionInput, OutcomeItemDTOCollection outcomeItemCollectionDB, int? fcId)
        {
            OutcomeItemDTOCollection outcomeItemNew = new OutcomeItemDTOCollection();
            //OutcomeItemDTOCollection outcomeItemCollectionDB = foreClosureCaseSetDAO.GetOutcomeItemCollection(fcId);            
            //Compare OutcomeItem input with OutcomeItem DB
            if (outcomeItemCollectionDB == null || outcomeItemCollectionDB.Count < 1)   
                return null;            
            foreach (OutcomeItemDTO itemDB in outcomeItemCollectionDB)
            {
                bool isDeleted = CheckOutcomeItem(itemDB, outcomeItemCollectionInput);
                if (!isDeleted)
                {
                    itemDB.OutcomeDeletedDt = DateTime.Now;
                    outcomeItemNew.Add(itemDB);
                }
            }            
            return outcomeItemNew;
        }

        /// <summary>                
        /// </summary>
        /// <param name>OutcomeItemDTOCollection, fc_id </param>
        /// <returns>new OutcomeItemDTOCollection use for Insert</returns>
        private bool CheckOutcomeItem(OutcomeItemDTO outcomeItem, OutcomeItemDTOCollection itemCollection)
        {            
            foreach (OutcomeItemDTO item in itemCollection)
            {
                if (outcomeItem.OutcomeTypeId == item.OutcomeTypeId                    
                    && ConvertStringToUpper(outcomeItem.NonprofitreferralKeyNum) == ConvertStringToUpper(item.NonprofitreferralKeyNum)
                    && ConvertStringToUpper(outcomeItem.ExtRefOtherName) == ConvertStringToUpper(item.ExtRefOtherName))
                    return true;
            }
            return false;
        }
       
        #endregion

        #region Functions check for update Case Loan
        /// <summary>      
        
        /// </summary>
        /// <param name>caseLoanDeleteCollection, caseLoanInsertCollection</param>
        /// <returns>caseLoanInsertCollection with changed_acct_num</returns>
        private CaseLoanDTOCollection SetChangeAccNumForCaseLoan(CaseLoanDTOCollection caseLoanDelete, CaseLoanDTOCollection caseLoanInsert)
        {
            var caseLoanNew = new CaseLoanDTOCollection();          
            int minCount = caseLoanDelete.Count < caseLoanInsert.Count ? caseLoanDelete.Count : caseLoanInsert.Count;
            for (int i = 0; i < minCount; i++)
            {
                var itemDelete = caseLoanDelete[i];
                var itemInsert = caseLoanInsert[i];
                itemInsert.ChangedAcctNum = itemDelete.AcctNum + "-" + itemDelete.Loan1st2nd + " changed to " + itemInsert.AcctNum + "-" + itemInsert.Loan1st2nd;
                caseLoanNew.Add(itemInsert);
            }
            //If collection Insert greater than collection delete
            //add more item Insert
            if (caseLoanDelete.Count < caseLoanInsert.Count)
            {
                var j = caseLoanInsert.Count - minCount;
                for (int i = minCount; i < (minCount + j); i++)
                {
                    var itemInsert = caseLoanInsert[i];
                    caseLoanNew.Add(itemInsert);
                }
            }
            return caseLoanNew;
        }

        /// <summary>      
        /// Check exist of a caseLoan
        /// base on fcId, Accnum
        /// If exist return true; vs return false
        /// </summary>
        /// <param name>CaseLoanDTO, caseLoanCollection</param>
        /// <returns>bool</returns>
        private bool CheckCaseLoan(CaseLoanDTO caseLoan, CaseLoanDTOCollection caseLoanCollection)
        {
            foreach (CaseLoanDTO item in caseLoanCollection)
            {
                if (ConvertStringToUpper(caseLoan.AcctNum) == ConvertStringToUpper(item.AcctNum))
                    return true;
            }
            return false;
        }

        /// <summary>                       
        /// <param name></param>
        /// <returns>bool</returns>
        /// </summary>
        private bool CheckCaseLoanUpdate(CaseLoanDTO caseLoan, CaseLoanDTOCollection caseLoanCollection)
        {
            foreach (CaseLoanDTO item in caseLoanCollection)
            {
                if (ConvertStringToUpper(caseLoan.AcctNum) == ConvertStringToUpper(item.AcctNum))
                {
                    if (caseLoan.ServicerId != item.ServicerId
                        || ConvertStringToUpper(caseLoan.OtherServicerName) != ConvertStringToUpper(item.OtherServicerName)
                        || ConvertStringToUpper(caseLoan.Loan1st2nd) != ConvertStringToUpper(item.Loan1st2nd)
                        || ConvertStringToUpper(caseLoan.MortgageTypeCd) != ConvertStringToUpper(item.MortgageTypeCd)                        
                        || ConvertStringToUpper(caseLoan.ArmResetInd) != ConvertStringToUpper(item.ArmResetInd)
                        || ConvertStringToUpper(caseLoan.TermLengthCd) != ConvertStringToUpper(item.TermLengthCd)
                        || ConvertStringToUpper(caseLoan.LoanDelinqStatusCd) != ConvertStringToUpper(item.LoanDelinqStatusCd)
                        || caseLoan.CurrentLoanBalanceAmt != item.CurrentLoanBalanceAmt
                        || caseLoan.OrigLoanAmt != item.OrigLoanAmt
                        || caseLoan.InterestRate != item.InterestRate
                        || ConvertStringToUpper(caseLoan.OriginatingLenderName) != ConvertStringToUpper(item.OriginatingLenderName)
                        || ConvertStringToUpper(caseLoan.OrigMortgageCoFdicNcusNum) != ConvertStringToUpper(item.OrigMortgageCoFdicNcusNum)
                        || ConvertStringToUpper(caseLoan.OrigMortgageCoName) != ConvertStringToUpper(item.OrigMortgageCoName)
                        || ConvertStringToUpper(caseLoan.OrginalLoanNum) != ConvertStringToUpper(item.OrginalLoanNum)
                        || ConvertStringToUpper(caseLoan.CurrentServicerFdicNcuaNum) != ConvertStringToUpper(item.CurrentServicerFdicNcuaNum)
//Comment these checking because these oness are hidden for WS
//                        || ConvertStringToUpper(caseLoan.InvestorLoanNum) != ConvertStringToUpper(item.InvestorLoanNum)
//                        || ConvertStringToUpper(caseLoan.InvestorNum) != ConvertStringToUpper(item.InvestorNum)
//                        || ConvertStringToUpper(caseLoan.InvestorName) != ConvertStringToUpper(item.InvestorName)
                        || ConvertStringToUpper(caseLoan.MortgageProgramCd) != ConvertStringToUpper(item.MortgageProgramCd)
                        || !CompareDate(caseLoan.ArmRateAdjustDt, item.ArmRateAdjustDt)
                        || caseLoan.ArmLockDuration != item.ArmLockDuration
                        || ConvertStringToUpper(caseLoan.LoanLookupCd) != ConvertStringToUpper(item.LoanLookupCd)
                        || ConvertStringToUpper(caseLoan.ThirtyDaysLatePastYrInd) != ConvertStringToUpper(item.ThirtyDaysLatePastYrInd)
                        || ConvertStringToUpper(caseLoan.PmtMissLessOneYrLoanInd) != ConvertStringToUpper(item.PmtMissLessOneYrLoanInd)
                        || ConvertStringToUpper(caseLoan.SufficientIncomeInd) != ConvertStringToUpper(item.SufficientIncomeInd)
                        || ConvertStringToUpper(caseLoan.LongTermAffordInd) != ConvertStringToUpper(item.LongTermAffordInd)
                        || ConvertStringToUpper(caseLoan.HarpEligibleInd) != ConvertStringToUpper(item.HarpEligibleInd)
                        || ConvertStringToUpper(caseLoan.OrigPriorTo2009Ind) != ConvertStringToUpper(item.OrigPriorTo2009Ind)
                        || ConvertStringToUpper(caseLoan.PriorHampInd) != ConvertStringToUpper(item.PriorHampInd)
                        || ConvertStringToUpper(caseLoan.PrinBalWithinLimitInd) != ConvertStringToUpper(item.PrinBalWithinLimitInd)
                        || ConvertStringToUpper(caseLoan.HampEligibleInd) != ConvertStringToUpper(item.HampEligibleInd)
                        )
                        return false;
                }                
            }
            return true;
        }
        private bool CompareDate(DateTime? source, DateTime? des)
        {
            if (!source.HasValue && !des.HasValue)
                return true;
            if (source.HasValue && !des.HasValue)
                return false;
            if (!source.HasValue && des.HasValue)
                return false;

            if (source.Value.Day != des.Value.Day || source.Value.Month != des.Value.Month || source.Value.Year != des.Value.Year)
                return false;

            return true;
        }
        /// <summary>
        /// Check data input with database
        /// If input not exist in database (use CheckCaseLoan() to check)
        /// add caseLoan into caseLoanCollection to insert
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        private CaseLoanDTOCollection CheckCaseLoanForInsert(ForeclosureCaseSetDAO foreClosureCaseSetDAO, CaseLoanDTOCollection caseLoanCollection, 
                                            CaseLoanDTOCollection caseLoanCollectionDB, int? fcId)
        {
            CaseLoanDTOCollection caseLoanNew = new CaseLoanDTOCollection();
            //CaseLoanDTOCollection caseLoanCollectionDB = foreClosureCaseSetDAO.GetCaseLoanCollection(fcId);            
            //Compare CAseLoanItem input with Case Loan DB
            if (caseLoanCollectionDB != null)
            {
                foreach (CaseLoanDTO itemInput in caseLoanCollection)
                {
                    bool isChanged = CheckCaseLoan(itemInput, caseLoanCollectionDB);
                    if (!isChanged)
                    {
                        caseLoanNew.Add(itemInput);
                    }
                }
            }
            else
            {
                caseLoanNew = caseLoanCollection;                
            }
            return caseLoanNew;   
        }

        /// <summary>
        /// Check data database with input
        /// If database not exist in data input (use CheckCaseLoan() to check)
        /// add caseLoan into caseLoanCollection to delete
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        private CaseLoanDTOCollection CheckCaseLoanForDelete(ForeclosureCaseSetDAO foreClosureCaseSetDAO, CaseLoanDTOCollection caseLoanCollection, 
                                                    CaseLoanDTOCollection caseLoanCollectionDB, int? fcId)
        {
            CaseLoanDTOCollection caseLoanNew = new CaseLoanDTOCollection();
            //CaseLoanDTOCollection caseLoanCollectionDB = foreClosureCaseSetDAO.GetCaseLoanCollection(fcId);
            //Compare CAseLoanItem input with Case Loan DB
            if (caseLoanCollectionDB == null || caseLoanCollectionDB.Count < 1) 
                return null;            
            foreach (CaseLoanDTO itemDB in caseLoanCollectionDB)
            {
                bool isChanged = CheckCaseLoan(itemDB, caseLoanCollection);
                if (!isChanged)
                {                    
                    caseLoanNew.Add(itemDB);
                }
            }            
            return caseLoanNew;   
        }

        /// <summary>
        /// With each data in CaseLoan input
        /// Compare it with caseLoan in database
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        private CaseLoanDTOCollection CheckCaseLoanForUpdate(ForeclosureCaseSetDAO foreClosureCaseSetDAO,CaseLoanDTOCollection caseLoanCollection, 
                                                        CaseLoanDTOCollection caseLoanCollectionDB, int? fcId)
        {
            CaseLoanDTOCollection caseLoanNew = new CaseLoanDTOCollection();
            //CaseLoanDTOCollection caseLoanCollectionDB = foreClosureCaseSetDAO.GetCaseLoanCollection(fcId);
            if (caseLoanCollectionDB == null || caseLoanCollectionDB.Count < 1)  
                return null;            
            foreach (CaseLoanDTO item in caseLoanCollection)
            {
                bool isChanged = CheckCaseLoanUpdate(item, caseLoanCollectionDB);
                if (!isChanged)
                {
                    item.FcId = fcId;
                    caseLoanNew.Add(item);
                }
            }            
            return caseLoanNew;
        }
        #endregion

        #region Functions check valid code
        /// <summary>
        /// Check valid code
        /// <input>ForeclosureCaseSetDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidCode(ForeclosureCaseSetDTO foreclosureCaseSet)
        {                     
            ForeclosureCaseDTO foreclosureCase = foreclosureCaseSet.ForeclosureCase;
            CaseLoanDTOCollection caseLoanCollection = foreclosureCaseSet.CaseLoans;
            BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSet.BudgetItems;
            OutcomeItemDTOCollection outcomeItemCollection = foreclosureCaseSet.Outcome;
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            ExceptionMessageCollection msgFcCase = CheckValidCodeForForclosureCase(foreclosureCase);
            if (msgFcCase != null && msgFcCase.Count != 0)
                msgFcCaseSet.Add(msgFcCase);

            ExceptionMessageCollection msgCaseLoan = CheckValidCodeForCaseLoan(caseLoanCollection);
            if (msgCaseLoan != null && msgCaseLoan.Count != 0)
                msgFcCaseSet.Add(msgCaseLoan);

            ExceptionMessageCollection msgStateCdAndZip = CheckValidCombinationStateCdAndZip(foreclosureCase);
            if (msgStateCdAndZip != null && msgStateCdAndZip.Count != 0)
                msgFcCaseSet.Add(msgStateCdAndZip);

            ExceptionMessageCollection msgZip = CheckValidZipCode(foreclosureCase);
            if (msgZip != null && msgZip.Count != 0)
                msgFcCaseSet.Add(msgZip);

            ExceptionMessageCollection msgAgencyId = CheckValidAgencyId(foreclosureCase);
            if (msgAgencyId != null && msgAgencyId.Count != 0)
                msgFcCaseSet.Add(msgAgencyId);

            ExceptionMessageCollection msgProgramId = CheckValidProgramId(foreclosureCase);
            if (msgProgramId != null && msgProgramId.Count != 0)
                msgFcCaseSet.Add(msgProgramId);

            ExceptionMessageCollection msgOutcomeTypeId = CheckValidOutcomeTypeId(outcomeItemCollection, foreclosureCase.IntakeDt);
            if (msgOutcomeTypeId != null && msgOutcomeTypeId.Count != 0)
                msgFcCaseSet.Add(msgOutcomeTypeId);

            ExceptionMessageCollection msgBudgetSubId = CheckValidBudgetSubcategoryId(budgetItemCollection);
            if (msgBudgetSubId != null && msgBudgetSubId.Count != 0)
                msgFcCaseSet.Add(msgBudgetSubId);

            ExceptionMessageCollection msgProposedBudgetSubId = CheckValidBudgetSubcategoryId(foreclosureCaseSet.ProposedBudgetItems);
            if (msgProposedBudgetSubId != null && msgProposedBudgetSubId.Count != 0)
                msgFcCaseSet.Add(msgProposedBudgetSubId);

            ExceptionMessageCollection msgCallId = CheckValidCallId(foreclosureCase);
            if (msgCallId != null && msgCallId.Count != 0)
                msgFcCaseSet.Add(msgCallId);            
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check valid code for forclosu Case
        /// <input>Foreclosurecase</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidCodeForForclosureCase(ForeclosureCaseDTO forclosureCase)
        {
            ReferenceCodeValidatorBL referenceCode = new ReferenceCodeValidatorBL();
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (forclosureCase == null)
                return null;

            if (!referenceCode.Validate(ReferenceCode.INCOME_EARNERS_CODE, forclosureCase.IncomeEarnersCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0200, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0200));
            if (!referenceCode.Validate(ReferenceCode.CASE_RESOURCE_CODE,  forclosureCase.CaseSourceCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0201, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0201));
            if (!referenceCode.Validate(ReferenceCode.RACE_CODE,  forclosureCase.RaceCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0202, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0202));
            if (!referenceCode.Validate(ReferenceCode.HOUSEHOLD_CODE,  forclosureCase.HouseholdCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0203, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0203));
            //if (!referenceCode.Validate(ReferenceCode.NEVER_BILL_REASON_CODE, forclosureCase.NeverBillReasonCd))
            //    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for NeverBillReasonCd.");
            //if (!referenceCode.Validate(ReferenceCode.NEVER_PAY_REASON_CODE, forclosureCase.NeverPayReasonCd))
            //    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for NeverPayReasonCd.");
            if (!referenceCode.Validate(ReferenceCode.DEFAULT_REASON_CODE,  forclosureCase.DfltReason1stCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0204, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0204));
            if (!referenceCode.Validate(ReferenceCode.DEFAULT_REASON_CODE,  forclosureCase.DfltReason2ndCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0205, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0205));
            if (!referenceCode.Validate(ReferenceCode.HUD_TERMINATION_REASON_CODE,  forclosureCase.HudTerminationReasonCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0206, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0206));
            if (!referenceCode.Validate(ReferenceCode.HUD_OUTCOME_CODE,  forclosureCase.HudOutcomeCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0207, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0207));
            if (!referenceCode.Validate(ReferenceCode.COUNSELING_DURARION_CODE,  forclosureCase.CounselingDurationCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0208, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0208));
            if (!referenceCode.Validate(ReferenceCode.GENDER_CODE,  forclosureCase.GenderCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0209, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0209));
            if (!referenceCode.Validate(ReferenceCode.STATE_CODE,  forclosureCase.ContactStateCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0210, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0210));
            if (!referenceCode.Validate(ReferenceCode.STATE_CODE,  forclosureCase.PropStateCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0211, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0211));
            if (!referenceCode.Validate(ReferenceCode.EDUCATION_LEVEL_COMPLETED_CODE,  forclosureCase.BorrowerEducLevelCompletedCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0212, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0212));
            if (!referenceCode.Validate(ReferenceCode.MARITAL_STATUS_CODE,  forclosureCase.BorrowerMaritalStatusCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0213, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0213));
            if (!referenceCode.Validate(ReferenceCode.LANGUAGE_CODE,  forclosureCase.BorrowerPreferredLangCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0214, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0214));            
            if (!referenceCode.Validate(ReferenceCode.SUMMARY_SENT_OTHER_CODE,  forclosureCase.SummarySentOtherCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0215, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0215));
            if (!referenceCode.Validate(ReferenceCode.PROPERTY_CODE,  forclosureCase.PropertyCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0216, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0216));
            if (!referenceCode.Validate(ReferenceCode.MILITARY_SERVICE_CODE,  forclosureCase.MilitaryServiceCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0217, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0217));
            if (!referenceCode.Validate(ReferenceCode.CREDIT_BURREAU_CODE,  forclosureCase.IntakeCreditBureauCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0218, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0218));
            if (!referenceCode.Validate(ReferenceCode.ERCP_OUTCOME_CODE, forclosureCase.ErcpOutcomeCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0225, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0225));
            if (!referenceCode.Validate(ReferenceCode.LANGUAGE_CODE, forclosureCase.CounseledLanguageCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0224, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0224));
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check valid code for case loan
        /// <input>CaseLoanDTOCollection</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidCodeForCaseLoan(CaseLoanDTOCollection caseLoanCollection)
        {
            ReferenceCodeValidatorBL referenceCode = new ReferenceCodeValidatorBL();
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (caseLoanCollection == null || caseLoanCollection.Count < 1)
                return null;
            bool error131 = false;

            for (int i = 0; i < caseLoanCollection.Count; i++)
            {
                CaseLoanDTO caseLoan = caseLoanCollection[i];
                if (!referenceCode.Validate(ReferenceCode.LOAN_1ST_2ND_CODE, caseLoan.Loan1st2nd))
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0219, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0219) + " working on case loan index " + (i + 1));
                if (!referenceCode.Validate(ReferenceCode.MORTGAGE_TYPE_CODE,  caseLoan.MortgageTypeCd))
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0220, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0220) + " working on case loan index " + (i + 1));
                if (!referenceCode.Validate(ReferenceCode.TERM_LENGTH_CODE,  caseLoan.TermLengthCd))
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0221, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0221) + " working on case loan index " + (i + 1));
                if (!referenceCode.Validate(ReferenceCode.LOAN_DELINQUENCY_STATUS_CODE,  caseLoan.LoanDelinqStatusCd))
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0222, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0222) + " working on case loan index " + (i + 1));
                if (!referenceCode.Validate(ReferenceCode.MORTGAGE_PROGRAM_CODE,  caseLoan.MortgageProgramCd))
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0223, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0223) + " working on case loan index " + (i + 1));
                if (!referenceCode.Validate(ReferenceCode.LOAN_LOOKUP_CODE, caseLoan.LoanLookupCd))
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0226, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0226) + " working on case loan index " + (i + 1));

                if (!CheckValidServicerId(caseLoan.ServicerId))
                {
                    string msg = string.Format(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0361), caseLoan.AcctNum);
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0361, msg);
                }

                if (!error131 && i < caseLoanCollection.Count - 1)
                {   
                    for (int j = i + 1; j < caseLoanCollection.Count; j++)
                    {
                        if (caseLoanCollection[i].AcctNum == null || caseLoanCollection[j].AcctNum == null
                            || caseLoanCollection[i].AcctNum == string.Empty || caseLoanCollection[j].AcctNum == string.Empty)
                            continue;
                        if (caseLoanCollection[i].AcctNum.ToUpper().CompareTo(caseLoanCollection[j].AcctNum.ToUpper()) == 0)
                        {
                            msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0131, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0131));
                            error131 = true;
                            break;
                        }
                    }
                }
            }

            return msgFcCaseSet;  
        }

        /// <summary>
        /// Check valid combination state_code and zip code
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidCombinationStateCdAndZip(ForeclosureCaseDTO forclosureCase)
        {
            GeoCodeRefDTOCollection geoCodeRefCollection = GeoCodeRefDAO.Instance.GetGeoCodeRef();
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            bool contactValid = false;
            bool propertyValid = false;
            if (geoCodeRefCollection == null || geoCodeRefCollection.Count < 1)
                return null;            
            foreach (GeoCodeRefDTO item in geoCodeRefCollection)
            {
                contactValid = CombinationContactValid(forclosureCase, item);
                if (contactValid == true)
                    break;
            }
            foreach (GeoCodeRefDTO item in geoCodeRefCollection)
            {
                propertyValid = CombinationPropertyValid(forclosureCase, item);
                if (propertyValid == true)
                    break;
            }            
            if(contactValid == false)
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0259, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0259));
            if(propertyValid == false)
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0260, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0260));
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check valid combination contact state_code and contact zip code
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private bool CombinationContactValid(ForeclosureCaseDTO forclosureCase, GeoCodeRefDTO item)
        {
            if (string.IsNullOrEmpty(forclosureCase.ContactZip) && string.IsNullOrEmpty(forclosureCase.ContactStateCd))
                return true;
            return (ConvertStringToUpper(forclosureCase.ContactZip) == ConvertStringToUpper(item.ZipCode) && ConvertStringToUpper(forclosureCase.ContactStateCd) == ConvertStringToUpper(item.StateAbbr));
        }

        /// <summary>
        /// Check valid combination property state_code and property zip code
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private bool CombinationPropertyValid(ForeclosureCaseDTO forclosureCase, GeoCodeRefDTO item)
        {
            if (string.IsNullOrEmpty(forclosureCase.PropZip) && string.IsNullOrEmpty(forclosureCase.PropStateCd))
                return true;
            return (ConvertStringToUpper(forclosureCase.PropZip) == ConvertStringToUpper(item.ZipCode) && ConvertStringToUpper(forclosureCase.PropStateCd) == ConvertStringToUpper(item.StateAbbr));            
        }

        /// <summary>
        /// Check valid zipcode
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidZipCode(ForeclosureCaseDTO forclosureCase)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if ((!string.IsNullOrEmpty(forclosureCase.ContactZip) && forclosureCase.ContactZip.Length != 5) || !ConvertStringtoInt(forclosureCase.ContactZip))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0257, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0257));
            if ((!string.IsNullOrEmpty(forclosureCase.PropZip) && forclosureCase.PropZip.Length != 5) || !ConvertStringtoInt(forclosureCase.PropZip))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0258, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0258));
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check valid AgencyId
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidAgencyId(ForeclosureCaseDTO forclosureCase)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            AgencyDTOCollection agencyCollection = AgencyDAO.Instance.GetAgency();
            int? agencyID = forclosureCase.AgencyId;
            if (agencyID == null)
                return null;            
            else if ((agencyCollection == null || agencyCollection.Count < 1) && agencyID != null)
            {
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0250, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0250));
                return msgFcCaseSet;
            }
            foreach (AgencyDTO item in agencyCollection)
            {
                if (item.AgencyID == agencyID.ToString())
                    return null;
            }
            msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0250, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0250));            
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check valid Call Id
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidCallId(ForeclosureCaseDTO forclosureCase)
        {
            if (forclosureCase.CallId == null || forclosureCase.CallId == string.Empty)
                return null;
            if(!CheckCallID("HPF" + forclosureCase.CallId))
                return null;
            bool isCall = CallLogBL.Instance.GetCall(forclosureCase.CallId);
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (!isCall)
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0264, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0264));
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check valid AgencyId
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private bool CheckValidServicerId(int? servicerId)
        {            
            ServicerDTOCollection servicerCollection = ServicerBL.Instance.GetServicers();
            if (servicerId == null)
                return true;
            else if (servicerCollection == null || servicerCollection.Count < 1)
                return false;

            ServicerDTO serv = servicerCollection.GetServicerById(servicerId);
            
            if(serv == null || serv.ActiveInd != "Y")
                return false;

            return true;
        }

        /// <summary>
        /// Check valid programID
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidProgramId(ForeclosureCaseDTO forclosureCase)
        {
            ProgramDTOCollection programCollection = foreclosureCaseSetDAO.GetProgram();
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            int? programId = forclosureCase.ProgramId;
            if (programId == null)
                return null;
            else if (programCollection == null || programCollection.Count < 1)
            {
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0261, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0261));
                return msgFcCaseSet;
            }            
            foreach (ProgramDTO item in programCollection)
            {
                if (item.ProgramID == programId.ToString())
                    return null;
            }
            msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0261, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0261)); 
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check valid budget subcategory id
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidBudgetSubcategoryId(BudgetItemDTOCollection budgetItem)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (budgetItem == null || budgetItem.Count < 1)
                return null;
            for (int i = 0; i < budgetItem.Count; i++)
            { 
                BudgetItemDTO item =  budgetItem[i];
                bool isValid = CheckBudgetSubcategory(item);
                if(!isValid)
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0262, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0262)); 
            }
            return msgFcCaseSet;
        }

        private bool CheckBudgetSubcategory(BudgetItemDTO budgetItem)
        {
            BudgetSubcategoryDTOCollection budgetSubcategoryCollection = BudgetBL.Instance.GetBudgetSubcategory();
            int? budgetSubId = budgetItem.BudgetSubcategoryId;
            if (budgetSubcategoryCollection == null || budgetSubcategoryCollection.Count < 1 || budgetSubId == null)
                return true;            
            foreach(BudgetSubcategoryDTO item in budgetSubcategoryCollection)
            {
                if (item.BudgetSubcategoryID == budgetSubId)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check valid Outcome Type Id
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidOutcomeTypeId(OutcomeItemDTOCollection outcomeItem, DateTime? fcIntakeDate)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (outcomeItem == null || outcomeItem.Count < 1)
                return null;
            for (int i = 0; i < outcomeItem.Count; i++)
            {
                OutcomeItemDTO item = outcomeItem[i];
                if (!CheckOutcomeType(item, fcIntakeDate))
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0263, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0263) + " on outcome item index " + (i + 1));   
            }
            return msgFcCaseSet;
        }

        private bool CheckOutcomeType(OutcomeItemDTO outcomeItem, DateTime? fcIntakeDate)
        {
            OutcomeTypeDTOCollection outcomeTypeCollection = OutcomeBL.Instance.GetOutcomeType();
            if (outcomeTypeCollection == null || outcomeTypeCollection.Count < 1)
                return true;
            int? outcomeTypeId = outcomeItem.OutcomeTypeId;
            if (outcomeTypeId == null)
                return true;
            foreach (OutcomeTypeDTO item in outcomeTypeCollection)
            {
                if (item.OutcomeTypeID == outcomeTypeId)
                {
                    if (item.InactiveDt == null || fcIntakeDate == null || item.InactiveDt > fcIntakeDate)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        #endregion        

        #region Funcrions Set HP-Auto
        /// <summary>
        /// Add value HPF-Auto for ForclosureCase        
        /// </summary>
        private ForeclosureCaseDTO AssignForeclosureCaseHPFAuto(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            if (CheckForeclosureCaseDBComplete(FCaseSetFromDB))            
                foreclosureCaseSet.ForeclosureCase.CompletedDt = FCaseSetFromDB.ForeclosureCase.CompletedDt;                
            
            else if (WarningMessage.Count == 0)
                foreclosureCaseSet.ForeclosureCase.CompletedDt = DateTime.Now;
            
            return foreclosureCaseSet.ForeclosureCase;
        }        
              
        /// <summary>
        /// Add value HPF-Auto for Outcome
        /// </summary>
        private OutcomeItemDTOCollection AssignOutcomeHPFAuto(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            if (foreclosureCaseSet.Outcome == null || foreclosureCaseSet.Outcome.Count < 1)  
                return null;            
            OutcomeItemDTOCollection outcomeItemNew = new OutcomeItemDTOCollection();
            OutcomeItemDTOCollection outcomeItemOld = foreclosureCaseSet.Outcome;            
            foreach(OutcomeItemDTO item in outcomeItemOld)
            {
                item.OutcomeDt = DateTime.Now;
                outcomeItemNew.Add(item);
            }                        
            return outcomeItemNew;
        }        

        /// <summary>
        /// Add value HPF-Auto for Budget Set
        /// </summary>
        private BudgetSetDTO AssignBudgetSetHPFAuto(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetAssetDTOCollection budgetAssetCollection, BudgetItemDTOCollection budgetItemCollection)
        {            
            BudgetSetDTO budgetSet = new BudgetSetDTO();

            //BudgetAssetDTOCollection budgetAssetCollection = foreclosureCaseSet.BudgetAssets;
            //BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSet.BudgetItems;
            double? totalIncome = 0;
            double? totalExpenses = 0;
            double? totalAssest = 0;

            //if ((budgetAssetCollection == null || budgetAssetCollection.Count < 1) && (budgetItemCollection == null || budgetItemCollection.Count < 1))
            //    return null;

            if (budgetAssetCollection != null)
                totalAssest = CalculateTotalAssets(budgetAssetCollection, totalAssest);
            if (budgetItemCollection!= null)
                CalculateTotalExpenseAndIncome(foreClosureCaseSetDAO, budgetItemCollection, ref totalIncome, ref totalExpenses);                        

            budgetSet.TotalAssets = totalAssest;
            budgetSet.TotalExpenses = totalExpenses;
            budgetSet.TotalIncome = totalIncome;
            budgetSet.BudgetSetDt = DateTime.Now;
            //
            budgetSet.SetInsertTrackingInformation(_workingUserID);
                        
            return budgetSet;
        }

        /// <summary>
        /// Calculate Total Assets
        /// </summary>
        private double? CalculateTotalAssets(BudgetAssetDTOCollection budgetAssetCollection, double? totalAssest)
        {
            //Calculate totalAssest
            if (budgetAssetCollection == null || budgetAssetCollection.Count < 1)
            {
                return 0;
            }
            totalAssest = 0;
            foreach (BudgetAssetDTO item in budgetAssetCollection)
            {
                totalAssest += item.AssetValue;
            }
            return totalAssest;
        }

        /// <summary>
        /// Calculate Total Expense And Income
        /// </summary>
        private void CalculateTotalExpenseAndIncome(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetItemDTOCollection budgetItemCollection, ref double? totalIncome, ref double? totalExpenses)
        {            
            totalIncome = 0;
            totalExpenses = 0;
            foreach (var item in budgetItemCollection)
            {
                string budgetCode = BuggetCategoryCode(foreClosureCaseSetDAO, item.BudgetSubcategoryId);
                if (budgetCode != null)
                {
                    if (budgetCode == Constant.INCOME)
                        totalIncome += item.BudgetItemAmt;
                    else if (budgetCode == Constant.EXPENSES)
                        totalExpenses += item.BudgetItemAmt;
                }
            }           
        }

        private string BuggetCategoryCode(ForeclosureCaseSetDAO foreClosureCaseSetDAO, int? subCategoryId)
        {
            BudgetDTOCollection budgetCollection = BudgetBL.Instance.GetBudget();
            if (budgetCollection == null || budgetCollection.Count < 1)
                return null;            
            foreach (BudgetDTO item in budgetCollection)
            {
                if (subCategoryId == item.BudgetSubcategoryId)
                    return item.BudgetCategoryCode;
            }
            return null;
        }
        #endregion

        /// <summary>
        /// Get Foreclosure case basing on its fc_id
        /// </summary>
        /// <param name="fcId">id for a ForeclosureCase</param>
        /// <returns>object of ForeclosureCase </returns>        

        #region Throw Detail Exception

        private void ThrowDataValidationException(string errorCode)
        {
            DataValidationException ex = new DataValidationException();
            ex.ExceptionMessages.AddExceptionMessage(errorCode, ErrorMessages.GetExceptionMessageCombined(errorCode));
            throw ex;
        }

        private static void ThrowDataValidationException(ExceptionMessageCollection exDetailCollection)
        {
            var ex = new DataValidationException(exDetailCollection);
            throw ex;
        }

        private static ExceptionMessageCollection CreateDuplicateCaseWarning(DuplicatedCaseLoanDTOCollection collection)
        {
            ExceptionMessageCollection de = new ExceptionMessageCollection();
            foreach (DuplicatedCaseLoanDTO obj in collection)
            {
                ExceptionMessage em = new ExceptionMessage();
                em.ErrorCode = "WAR0253";
                em.Message = string.Format("WAR0253--The duplicated Case Loan is Loan Number: {0}, Servicer Name: {1}, Borrower First Name: {2}, Borrower Last Name: {3}, Agency Name: {4}, Agency Case Number: {5}, Counselor Full Name: {6} {7},Counselor Phone {8} - Ext: {9}, Counselor Email: {10} "
                             , obj.LoanNumber, obj.ServicerName, obj.BorrowerFirstName, obj.BorrowerLastName, obj.AgencyName, obj.AgencyCaseNumber
                             , obj.CounselorFName, obj.CounselorLName, obj.CounselorPhone, obj.CounselorExt, obj.CounselorEmail);
                de.Add(em);
            }
            return de;
            
        }

        private static void ThrowDuplicateCaseException(DuplicatedCaseLoanDTOCollection collection)
        {
            DuplicateException pe = new DuplicateException();
            foreach(DuplicatedCaseLoanDTO obj in collection)
            {
                var em = new ExceptionMessage();
                em.ErrorCode = ErrorMessages.ERR0253;               
                em.Message = ErrorMessages.GetExceptionMessage(em.ErrorCode, obj.ServicerName, obj.LoanNumber, obj.PropertyZip, obj.BorrowerFirstName, obj.BorrowerLastName
                            , obj.CounselorFName, obj.CounselorLName, obj.AgencyName, obj.CounselorPhone, obj.CounselorExt
                            , obj.CounselorEmail, obj.OutcomeDt, obj.OutcomeTypeCode);
                pe.ExceptionMessages.Add(em);
            }
            throw pe;
        }
      
        #endregion

        #region Utility
        private string ConvertStringToUpper(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;
            s = s.ToUpper().Trim();
            return s;
        }

        private string ConvertStringEmptyToNull(string s)
        {
            if (s == null)
                return null;
            s = s.Trim();
            if (string.IsNullOrEmpty(s))
                return null;
            return s.Trim(); ;
        }

        private bool ConvertStringtoInt(string s)
        {
            if (s == null)
                return true;
            else
            {
                try
                {
                    int.Parse(s);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private bool CompareString(string str1, string str2)
        {
            var temp1 = ConvertStringEmptyToNull(str1);
            var temp2 = ConvertStringEmptyToNull(str2);
            if (temp1 == null && temp2 == null)
                return true;
            if ((temp1 == null && temp2 != null) || (temp1 != null && temp2 == null))
                return false;
            if (temp1.ToUpper() == temp2.ToUpper())
                return true;
            return false;
        }
        #endregion

        #endregion             
        
        public ForeclosureCaseSetDTO GetForeclosureCaseDetail(int fcId)
        {            
            ForeclosureCaseSetDTO fcs = new ForeclosureCaseSetDTO();

            fcs.ForeclosureCase = GetForeclosureCase(fcId);

            if (fcs.ForeclosureCase == null)
                return null;
            //fcs.ActivityLog = ActivityLogBL.Instance.GetActivityLog(fcId);
            fcs.BudgetAssets = BudgetBL.Instance.GetBudgetAssetSet(fcId);
            fcs.BudgetItems = BudgetBL.Instance.GetBudgetItemSet(fcId);
            fcs.ProposedBudgetItems = BudgetBL.Instance.GetProposedBudgetItemSet(fcId);
            fcs.BudgetSet = BudgetBL.Instance.GetBudgetSet(fcId);
            fcs.ProposedBudgetSet = BudgetBL.Instance.GetProposedBudgetSet(fcId);
            fcs.CaseLoans = CaseLoanBL.Instance.GetCaseLoanCollection(fcId);
            fcs.Outcome = OutcomeBL.Instance.GetOutcomeItemCollection(fcId);

            return fcs;
        }

        private ForeclosureCaseDTO GetForeclosureCase(int? fcId)
        {
            return foreclosureCaseSetDAO.GetForeclosureCase(fcId);
        }
    }
}
