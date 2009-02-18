using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;

using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
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
        ForeclosureCaseDTO _dbFcCase = null;
        private string _workingUserID;

        private bool IsCaseCompleted;

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

        #region Implementation of IForclosureCaseBL        

        /// <summary>
        /// Save a ForeclosureCase
        /// </summary>
        /// <param name="foreclosureCaseSet">ForeclosureCaseSetDTO</param>
        public int? SaveForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
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
            SendCompletedCaseToQueueIfAny(fcId);
            //
            return fcId;         
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
        
        #endregion

        #region Function to serve SearchForeclosureCase
        private void ValidateFcCaseSearchCriteriaNotNull(ForeclosureCaseSearchCriteriaDTO searchCriteria, DataValidationException dataValidationException)
        {
            if (string.IsNullOrEmpty(searchCriteria.AgencyCaseNumber) &&
                string.IsNullOrEmpty(searchCriteria.FirstName) &&
                string.IsNullOrEmpty(searchCriteria.Last4_SSN) &&
                string.IsNullOrEmpty(searchCriteria.LastName) &&
                string.IsNullOrEmpty(searchCriteria.LoanNumber) &&
                string.IsNullOrEmpty(searchCriteria.PropertyZip))
            {
                string errorCode = "ERROR";// string.IsNullOrEmpty(result.Tag) ? "ERROR" : result.Tag;
                string errorMess = "At least one search criteria option is required"; // string.IsNullOrEmpty(result.Tag) ? result.Message : ErrorMessages.GetExceptionMessageCombined(result.Tag);
                dataValidationException.ExceptionMessages.AddExceptionMessage(errorCode, errorMess);
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

        #endregion

        #region Functions to serve SaveForeclosureCaseSet

        private int? ProcessUpdateForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {            
            var exceptionList = MiscErrorException(foreclosureCaseSet);
            if (exceptionList.Count > 0)
                ThrowDataValidationException(exceptionList);
            //
            DuplicatedCaseLoanDTOCollection collection = GetDuplicateCases(foreclosureCaseSet);

            ForeclosureCaseDTO fcCase = foreclosureCaseSet.ForeclosureCase;
            ForeclosureCaseDTO dbFcCase = GetForeclosureCase(fcCase.FcId);
            fcCase.NeverPayReasonCd = dbFcCase.NeverPayReasonCd;
            fcCase.NeverBillReasonCd = dbFcCase.NeverBillReasonCd;
            if (collection.Count > 0)
            {
                fcCase.DuplicateInd = Constant.DUPLICATE_YES;
                fcCase.NeverBillReasonCd = Constant.NEVER_BILL_REASON_CODE_DUPE;
                fcCase.NeverPayReasonCd = Constant.NEVER_PAY_REASON_CODE_DUPE;
                WarningMessage.Add(CreateDuplicateCaseWarning(collection));
            }
            else
            {               
                fcCase.DuplicateInd = Constant.DUPLICATE_NO;
                if (dbFcCase.NeverBillReasonCd.ToUpper().Equals(Constant.NEVER_BILL_REASON_CODE_DUPE))
                    fcCase.NeverBillReasonCd = null;
                if (dbFcCase.NeverPayReasonCd.ToUpper().Equals(Constant.NEVER_PAY_REASON_CODE_DUPE))
                    fcCase.NeverPayReasonCd = null;                
            }
            
            return UpdateForeclosureCaseSet(foreclosureCaseSet);
        }

        private int? ProcessInsertForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            DuplicatedCaseLoanDTOCollection collection = GetDuplicateCases(foreclosureCaseSet);
            if (collection.Count > 0)
            {
                ThrowDuplicateCaseException(collection);
            }
            foreclosureCaseSet.ForeclosureCase.DuplicateInd = Constant.DUPLICATE_NO;
            ExceptionMessageCollection exceptionList = MiscErrorException(foreclosureCaseSet);
            if (exceptionList.Count > 0)
                ThrowDataValidationException(exceptionList);
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
            ForeclosureCaseDTO dbFcCase = GetForeclosureCase(fc.FcId);
            if (dbFcCase == null)
                ThrowDataValidationException(ErrorMessages.ERR0251);

            //check valid fcCase for Agency
            if (dbFcCase.AgencyId != fc.AgencyId)
                ThrowDataValidationException(ErrorMessages.ERR0252);

            if (CheckInactiveCase(foreclosureCaseSet.ForeclosureCase.FcId))                
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
            if (ruleSet == Constant.RULESET_MIN_REQUIRE_FIELD)
            {
                msgFcCaseSet.Add(CheckOtherFieldFCaseForPartial(foreclosureCase));                                
            }                      
            return msgFcCaseSet;
        }

        private ExceptionMessageCollection CheckOtherFieldFCaseForPartial(ForeclosureCaseDTO foreclosureCase)
        {
            var msgFcCaseSet = new ExceptionMessageCollection();
            if (foreclosureCase != null)
            {                
                //-----BankruptcyInd, BankruptcyAttorney, BankruptcyPmtCurrentInd
                if (ConvertStringEmptyToNull(foreclosureCase.BankruptcyAttorney) != null &&
                    ConvertStringEmptyToNull(ConvertStringToUpper(foreclosureCase.BankruptcyInd)) !=
                    Constant.BANKRUPTCY_IND_YES)
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "BankruptcyInd value should be Y.");
                if (ConvertStringEmptyToNull(ConvertStringToUpper(foreclosureCase.BankruptcyInd)) ==
                    Constant.BANKRUPTCY_IND_YES && ConvertStringEmptyToNull(foreclosureCase.BankruptcyAttorney) == null)
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN",
                                                     "A BankruptcyAttorney is required to save a foreclosure case.");
                if (ConvertStringEmptyToNull(ConvertStringToUpper(foreclosureCase.BankruptcyInd)) ==
                    Constant.BANKRUPTCY_IND_YES &&
                    ConvertStringEmptyToNull(foreclosureCase.BankruptcyPmtCurrentInd) == null)
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN",
                                                     "A BankruptcyPmtCurrentInd is required to save a foreclosure case.");
                //-----SummarySentOtherCd, SummarySentOtherDt
                if (ConvertStringEmptyToNull(foreclosureCase.SummarySentOtherCd) == null && foreclosureCase.SummarySentOtherDt != null)
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN",
                                                     "A SummarySentOtherCd is required to save a foreclosure case.");
                else if (ConvertStringEmptyToNull(foreclosureCase.SummarySentOtherCd) != null && foreclosureCase.SummarySentOtherDt == null)
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN",
                                                     "A SummarySentOtherDt is required to save a foreclosure case.");
                //-----SrvcrWorkoutPlanCurrentInd
                if (ConvertStringEmptyToNull(foreclosureCase.SrvcrWorkoutPlanCurrentInd) == null &&
                    ConvertStringToUpper(foreclosureCase.HasWorkoutPlanInd) != Constant.HAS_WORKOUT_PLAN_IND_YES)
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN",
                                                     "A SrvcrWorkoutPlanCurrentInd is required to save a foreclosure case.");
                //-----HomeSalePrice
                if (ConvertStringToUpper(foreclosureCase.ForSaleInd) == Constant.FORSALE_IND_YES && 
                    foreclosureCase.HomeSalePrice == null)
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN",
                                                     "A HomeSalePrice is required to save a foreclosure case.");
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
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "Missing Outcome item. At least one Outcome item is required to save a foreclosure case.");
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
                        msgFcCaseSet.AddExceptionMessage(exItem.ErrorCode, ErrorMessages.GetExceptionMessageCombined(exItem.ErrorCode) + " working on outcome item index " + (i + 1));
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
            if (item.OutcomeTypeId == outComeTypeId && outComeTypeId != null && (ConvertStringEmptyToNull(item.NonprofitreferralKeyNum) == null && ConvertStringEmptyToNull(item.ExtRefOtherName) == null))
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
            if ((caseLoanDTOCollection == null || caseLoanDTOCollection.Count < 1) && ruleSet == Constant.RULESET_MIN_REQUIRE_FIELD)
            {
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0126, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0126));
                return msgFcCaseSet;
            }
            int? servicerId = FindServicerIDWithNameIsOther();            
            foreach (CaseLoanDTO item in caseLoanDTOCollection)
            {
                ExceptionMessageCollection ex = HPFValidator.ValidateToGetExceptionMessage(item, ruleSet);
                if (ex.Count > 0)
                {
                    msgFcCaseSet.Add(ex);
                    break;
                }
            }

            if (ruleSet == Constant.RULESET_MIN_REQUIRE_FIELD)
            {
                for (int i = 0; i < caseLoanDTOCollection.Count; i++)
                { 
                    var item = caseLoanDTOCollection[i];
                    ExceptionMessageCollection msgOther = CheckOtherFieldCaseLoanForPartial(servicerId, item, i);
                    if (msgOther.Count > 0)
                    {
                        msgFcCaseSet.Add(msgOther);
                        break;
                    }
                }                
            }            
            return msgFcCaseSet;
        }

        private ExceptionMessageCollection CheckOtherFieldCaseLoanForPartial(int? servicerId, CaseLoanDTO item, int i)
        {
            var msgFcCaseSet = new ExceptionMessageCollection();
            if (item.ServicerId == servicerId && ConvertStringEmptyToNull(item.OtherServicerName) == null)
            {                
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0266, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0266));
            }            
            if((ConvertStringToUpper(item.MortgageTypeCd) == Constant.MORTGATE_TYPE_CODE_ARM
               || ConvertStringToUpper(item.MortgageTypeCd) == Constant.MORTGATE_TYPE_CODE_HYBARM
               || ConvertStringToUpper(item.MortgageTypeCd) == Constant.MORTGATE_TYPE_CODE_POA
               || ConvertStringToUpper(item.MortgageTypeCd) == Constant.MORTGATE_TYPE_CODE_INTONLY) 
               && ConvertStringEmptyToNull(item.ArmResetInd) == null)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "A ArmResetInd is required to save a foreclosure case working on case loan index " + (i + 1));
            return msgFcCaseSet;
        }

        private int? FindServicerIDWithNameIsOther()
        {
            var serviers = foreclosureCaseSetDAO.GetServicer();
            foreach(var item in serviers)
            {
                if (ConvertStringToUpper(item.ServicerName) == Constant.SERVICER_OTHER)
                    return item.ServicerID;
            }
            return 0;
        }

        private int? FindOutcomeTypeIdWithNameIsExternalReferral()
        {
            OutcomeTypeDTOCollection outcomeType = foreclosureCaseSetDAO.GetOutcomeType();
            foreach (OutcomeTypeDTO item in outcomeType)
            {                
                if (ConvertStringToUpper(item.OutcomeTypeName) == Constant.OUTCOME_TYPE_NAME_EXTERNAL_REFERAL)
                    return item.OutcomeTypeID;
            }
            return 0;
        }

        private int FindSubCatWithNameIsMortgage()
        {
            BudgetSubcategoryDTOCollection budgetSubCat = foreclosureCaseSetDAO.GetBudgetSubcategory();
            foreach (BudgetSubcategoryDTO item in budgetSubCat)
            {                
                if (ConvertStringToUpper(item.BudgetSubcategoryName) == Constant.SUB_CATEGORY_NAME_MORTGAGE)
                    return item.BudgetSubcategoryID;
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
        private bool CheckInactiveCase(int? fcId)
        {
            DateTime currentDate = DateTime.Now;
            DateTime backOneYear = DateTime.MinValue;            
            ForeclosureCaseDTO foreclosureCase = GetForeclosureCase(fcId);            
            DateTime? completeDate = foreclosureCase.CompletedDt;
            if (completeDate == null || completeDate == DateTime.MinValue)
            {
                return false;
            }
            //Check leap year
            if (currentDate.Year % 400 == 0 || (currentDate.Year % 100 != 0 && currentDate.Year % 4 == 0))
            {
                backOneYear = currentDate.AddDays(-367);
            }
            else
            {
                backOneYear = currentDate.AddDays(-366);
            }
            //
            if (backOneYear < completeDate)
            {
                return false;
            }                
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
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid format is provided for CallID");            
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
            string callID = callId.Trim();
            if (callID == null || callID == string.Empty)
                return true;
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
            int? fcId = foreclosureCaseSet.ForeclosureCase.FcId;
            bool caseComplete = CheckForeclosureCaseComplete(fcId);            
            //Cannot Un-complete a Previously Completed Case
            msgFcCaseSet.Add(CheckUnCompleteCaseComplete(foreclosureCaseSet, caseComplete));
            //Two First Mortgages Not Allowed in a Case AND Case Loan must have atleast 1st (If case completed)
            msgFcCaseSet.Add(CheckFirstMortgages(foreclosureCaseSet, caseComplete));
            //Cannot resubmit the case complete without billable outcome
            msgFcCaseSet.Add(CheckBillableOutCome(foreclosureCaseSet, caseComplete));
            //Budget Item must have atleast 1 budget_item = 'Mortgage Amount'.(If case completed)
            msgFcCaseSet.Add(CheckMortgageBudgetItem(foreclosureCaseSet, caseComplete));
            //Budget Item valid or not
            msgFcCaseSet.Add(CheckBugetItemIsValid(foreclosureCaseSet));   
            //
            if (caseComplete && msgFcCaseSet.Count != 0)
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
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "BudgetItemAmt can not be null working on budget item index " + (i + 1));
                if (item.BudgetSubcategoryId == null)
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "BudgetSubCategory can not be null working on budget item index " + (i + 1));
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
            ExceptionMessageCollection msgRequire = ValidationFieldByRuleSet(foreclosureCaseSetInput, Constant.RULESET_MIN_REQUIRE_FIELD);
            if (caseComplete)
                msgFcCaseSet.Add(msgRequire);
            //
            ExceptionMessageCollection msgComplete = ValidationFieldByRuleSet(foreclosureCaseSetInput, Constant.RULESET_COMPLETE);
            if (caseComplete)            
                msgFcCaseSet.Add(msgComplete);            
            WarningMessage.Add(msgComplete);            
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check Case in Database is NULL or Complete Or Not Complete
        /// Null = Not Complete => return false
        /// CompleteDt != null => complete => return true
        /// </summary>     
        private bool CheckForeclosureCaseComplete(int? fcId)
        {
            ForeclosureCaseDTO fcCase = GetForeclosureCase(fcId);            
            bool caseComplete = false;
            if (fcCase != null && fcCase.CompletedDt != null && !CheckInactiveCase(fcId))
                caseComplete = true;
            return caseComplete;
        }

        /// <summary>
        /// Check Misc Error Exception
        /// Case 2: Two First Mortgages Not Allowed in a Case         
        /// return TRUE if have Error
        /// </summary>
        private ExceptionMessageCollection CheckFirstMortgages(ForeclosureCaseSetDTO foreclosureCaseSetInput, bool caseComplete)
        {
            int count = 0;
            CaseLoanDTOCollection caseLoan = foreclosureCaseSetInput.CaseLoans;
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            foreach (CaseLoanDTO item in caseLoan)
            {
                if (ConvertStringToUpper(item.Loan1st2nd) == Constant.LOAN_1ST)                
                    count = count + 1;                
            }
            if(count == 0)
                WarningMessage.AddExceptionMessage("UNKNOWN", "To be complete, atleast 1 mortgage with loan_1st_2nd_cd = '1st' is required.");
            if(count > 1)
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0256, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0256));
            if (caseComplete && count == 0)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "Must have Loan_1st_2nd with 1st value");
            
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
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "Must have OutcomeItem with billable value");
            if (!isBillable)
                WarningMessage.AddExceptionMessage(ErrorMessages.WARN0326, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0326));
            
            return msgFcCaseSet;
        }


        private bool CheckBillableOutcome(OutcomeItemDTO outcome)
        {
            OutcomeTypeDTOCollection outcomeType = foreclosureCaseSetDAO.GetOutcomeType();            
            foreach (OutcomeTypeDTO item in outcomeType)
            {
                if (item.OutcomeTypeID == outcome.OutcomeTypeId && ConvertStringToUpper(item.PayableInd) == Constant.PAYABLE_IND)
                    return true;
            }
            return false;
        }

        private ExceptionMessageCollection CheckMortgageBudgetItem(ForeclosureCaseSetDTO foreclosureCaseSetInput, bool caseComplete)
        {            
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            bool isbudgetHaveMortgate = CheckBudgetItemHaveMortgage(foreclosureCaseSetInput);
            if (!isbudgetHaveMortgate && caseComplete)
            {
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.WARN0327, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0327));
                return msgFcCaseSet;
            }
            if (foreclosureCaseSetInput.BudgetItems == null || foreclosureCaseSetInput.BudgetItems.Count < 1)
                WarningMessage.AddExceptionMessage(ErrorMessages.WARN0327, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0327));
            else if (!isbudgetHaveMortgate)
                WarningMessage.AddExceptionMessage(ErrorMessages.WARN0327, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0327));            
            return msgFcCaseSet;            
        }

        private bool CheckBudgetItemHaveMortgage(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {
            BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSetInput.BudgetItems;
            if (budgetItemCollection == null || budgetItemCollection.Count < 1)
                return false;
            int subCatId = FindSubCatWithNameIsMortgage();
            foreach (BudgetItemDTO item in budgetItemCollection)
            {
                if (item.BudgetSubcategoryId == subCatId && item.BudgetItemAmt != null)
                    return true;
            }
            return false;
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
            BudgetSetDTO budgetSet = AssignBudgetSetHPFAuto(foreclosureCaseSetDAO, foreclosureCaseSet);
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

                //check outcome item Input with outcome item DB
                //if not exist, insert new
                OutcomeItemDTOCollection outcomeCollecionNew = null;
                outcomeCollecionNew = CheckOutcomeItemInputwithDB(foreclosureCaseSetDAO, outcomeItemCollection, fcId);
                InsertOutcomeItem(foreclosureCaseSetDAO, outcomeCollecionNew, fcId);

                //check outcome item DB with outcome item input
                //if not exit, update outcome_deleted_dt = today()
                outcomeCollecionNew = CheckOutcomeItemDBwithInput(foreclosureCaseSetDAO, outcomeItemCollection, fcId);
                UpdateOutcome(foreclosureCaseSetDAO, outcomeCollecionNew);

                //Check for Delete Case Loan
                CaseLoanDTOCollection caseLoanCollecionNew = null;
                caseLoanCollecionNew = CheckCaseLoanForDelete(foreclosureCaseSetDAO, caseLoanCollection, fcId);
                DeleteCaseLoan(foreclosureCaseSetDAO, caseLoanCollecionNew);

                //Check for Update Case Loan
                caseLoanCollecionNew = null;
                caseLoanCollecionNew = CheckCaseLoanForUpdate(foreclosureCaseSetDAO, caseLoanCollection, fcId);
                UpdateCaseLoan(foreclosureCaseSetDAO, caseLoanCollecionNew);

                //Check for Insert Case Loan
                caseLoanCollecionNew = null;
                caseLoanCollecionNew = CheckCaseLoanForInsert(foreclosureCaseSetDAO, caseLoanCollection, fcId);
                InsertCaseLoan(foreclosureCaseSetDAO, caseLoanCollecionNew, fcId);
                //                               

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
            BudgetSetDTO budgetSet = AssignBudgetSetHPFAuto(foreclosureCaseSetDAO, foreclosureCaseSet);
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
                //                
                CompleteTransaction();
            }
            catch(Exception ex)
            {
                RollbackTransaction();
                throw ex;
            }
            return fcId;
        }

        private void SendCompletedCaseToQueueIfAny(int? fcId)
        {
            if (!IsCaseCompleted)            
                return;
            if(!ShouldSendSummary(fcId))
                return;
            //
            try
            {
                var queue = new HPFSummaryQueue();
                queue.SendACompletedCaseToQueue(fcId);
            }
            catch
            {
                
                var QUEUE_ERROR_MESSAGE = "Fail to push completed case to Queue : " + fcId;
                //Log
                Logger.Write(QUEUE_ERROR_MESSAGE, Constant.DB_LOG_CATEGORY);
                //Send E-mail to support
                var hpfSupportEmail = ConfigurationManager.AppSettings["HPF_SUPPORT_EMAIL"];
                var mail = new HPFSendMail
                               {
                                   To = hpfSupportEmail,
                                   Subject = QUEUE_ERROR_MESSAGE
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
        private bool ShouldSendSummary(int? fcId)
        { 
            var summaryBL = SummaryReportBL.Instance;
            var servicers = summaryBL.GetServicerbyFcId(fcId);
            foreach (ServicerDTO item in servicers)
            {
                if (ConvertStringToUpper(item.SecureDeliveryMethodCd) != Constant.SECURE_DELIVERY_METHOD_NOSEND)
                    return true;
            }
            return false;
        }
        
        #endregion

        #region Functions for Insert and Update tables
        /// <summary>
        /// Insert Budget
        /// </summary>
        private void InsertBudget(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetSetDTO budgetSet, BudgetItemDTOCollection budgetItemCollection, BudgetAssetDTOCollection budgetAssetCollection, int? fcId)
        {
            bool isInsertBudget = IsInsertBudgetSet(foreClosureCaseSetDAO , budgetItemCollection, budgetAssetCollection, fcId);
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
        private bool IsBudgetItemsDifference(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetItemDTOCollection budgetCollectionInput, int? fcId)
        {
            BudgetItemDTOCollection budgetCollectionDB = foreClosureCaseSetDAO.GetBudgetItemSet(fcId);
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
                    && ConvertStringToUpper(budgetItemInput.BudgetNote) == ConvertStringToUpper(budgetItemDB.BudgetNote))
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
        private bool IsBudgetAssetDifference(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetAssetDTOCollection budgetCollectionInput, int? fcId)
        {
            BudgetAssetDTOCollection budgetCollectionDB = foreClosureCaseSetDAO.GetBudgetAssetSet(fcId);            
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
                if (ConvertStringToUpper(budgetAssetInput.AssetName) == ConvertStringToUpper(budgetAssetDB.AssetName)
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
        private bool IsInsertBudgetSet(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetItemDTOCollection budgetItemCollection, BudgetAssetDTOCollection budgetAssetCollection, int? fcId)
        {
            bool budgetItem = IsBudgetItemsDifference(foreClosureCaseSetDAO, budgetItemCollection, fcId);
            bool budgetAsset = IsBudgetAssetDifference(foreClosureCaseSetDAO, budgetAssetCollection, fcId);
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
        private OutcomeItemDTOCollection CheckOutcomeItemInputwithDB(ForeclosureCaseSetDAO foreClosureCaseSetDAO, OutcomeItemDTOCollection outcomeItemCollectionInput, int? fcId)
        {
            OutcomeItemDTOCollection outcomeItemNew = new OutcomeItemDTOCollection();
            OutcomeItemDTOCollection outcomeItemCollectionDB = foreClosureCaseSetDAO.GetOutcomeItemCollection(fcId);
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
        private OutcomeItemDTOCollection CheckOutcomeItemDBwithInput(ForeclosureCaseSetDAO foreClosureCaseSetDAO, OutcomeItemDTOCollection outcomeItemCollectionInput, int? fcId)
        {
            OutcomeItemDTOCollection outcomeItemNew = new OutcomeItemDTOCollection();
            OutcomeItemDTOCollection outcomeItemCollectionDB = foreClosureCaseSetDAO.GetOutcomeItemCollection(fcId);            
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
                        || ConvertStringToUpper(caseLoan.InvestorLoanNum) != ConvertStringToUpper(item.InvestorLoanNum)
                        || ConvertStringToUpper(caseLoan.InvestorNum) != ConvertStringToUpper(item.InvestorNum)
                        || ConvertStringToUpper(caseLoan.InvestorName) != ConvertStringToUpper(item.InvestorName)
                        )
                        return false;
                }                
            }
            return true;
        }

        /// <summary>
        /// Check data input with database
        /// If input not exist in database (use CheckCaseLoan() to check)
        /// add caseLoan into caseLoanCollection to insert
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        private CaseLoanDTOCollection CheckCaseLoanForInsert(ForeclosureCaseSetDAO foreClosureCaseSetDAO, CaseLoanDTOCollection caseLoanCollection, int? fcId)
        {
            CaseLoanDTOCollection caseLoanNew = new CaseLoanDTOCollection();
            CaseLoanDTOCollection caseLoanCollectionDB = foreClosureCaseSetDAO.GetCaseLoanCollection(fcId);            
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
        private CaseLoanDTOCollection CheckCaseLoanForDelete(ForeclosureCaseSetDAO foreClosureCaseSetDAO, CaseLoanDTOCollection caseLoanCollection, int? fcId)
        {
            CaseLoanDTOCollection caseLoanNew = new CaseLoanDTOCollection();
            CaseLoanDTOCollection caseLoanCollectionDB = foreClosureCaseSetDAO.GetCaseLoanCollection(fcId);
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
        private CaseLoanDTOCollection CheckCaseLoanForUpdate(ForeclosureCaseSetDAO foreClosureCaseSetDAO,CaseLoanDTOCollection caseLoanCollection, int? fcId)
        {
            CaseLoanDTOCollection caseLoanNew = new CaseLoanDTOCollection();
            CaseLoanDTOCollection caseLoanCollectionDB = foreClosureCaseSetDAO.GetCaseLoanCollection(fcId);
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

            ExceptionMessageCollection msgOutcomeTypeId = CheckValidOutcomeTypeId(outcomeItemCollection);
            if (msgOutcomeTypeId != null && msgOutcomeTypeId.Count != 0)
                msgFcCaseSet.Add(msgOutcomeTypeId);

            ExceptionMessageCollection msgBudgetSubId = CheckValidBudgetSubcategoryId(budgetItemCollection);
            if (msgBudgetSubId != null && msgBudgetSubId.Count != 0)
                msgFcCaseSet.Add(msgBudgetSubId);

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
            if (!referenceCode.Validate(ReferenceCode.CASE_RESOURCE_CODE, forclosureCase.CaseSourceCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0201, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0201));
            if (!referenceCode.Validate(ReferenceCode.RACE_CODE, forclosureCase.RaceCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0202, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0202));
            if (!referenceCode.Validate(ReferenceCode.HOUSEHOLD_CODE, forclosureCase.HouseholdCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0203, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0203));
            //if (!referenceCode.Validate(ReferenceCode.NEVER_BILL_REASON_CODE, forclosureCase.NeverBillReasonCd))
            //    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for NeverBillReasonCd.");
            //if (!referenceCode.Validate(ReferenceCode.NEVER_PAY_REASON_CODE, forclosureCase.NeverPayReasonCd))
            //    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for NeverPayReasonCd.");
            if (!referenceCode.Validate(ReferenceCode.DEFAULT_REASON_CODE, forclosureCase.DfltReason1stCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0204, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0204));
            if (!referenceCode.Validate(ReferenceCode.DEFAULT_REASON_CODE, forclosureCase.DfltReason2ndCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0205, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0205));
            if (!referenceCode.Validate(ReferenceCode.HUD_TERMINATION_REASON_CODE, forclosureCase.HudTerminationReasonCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0206, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0206));
            if (!referenceCode.Validate(ReferenceCode.HUD_OUTCOME_CODE, forclosureCase.HudOutcomeCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0207, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0207));
            if (!referenceCode.Validate(ReferenceCode.COUNSELING_DURARION_CODE, forclosureCase.CounselingDurationCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0208, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0208));
            if (!referenceCode.Validate(ReferenceCode.GENDER_CODE, forclosureCase.GenderCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0209, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0209));
            if (!referenceCode.Validate(ReferenceCode.STATE, forclosureCase.ContactStateCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0210, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0210));
            if (!referenceCode.Validate(ReferenceCode.STATE, forclosureCase.PropStateCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0211, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0211));
            if (!referenceCode.Validate(ReferenceCode.EDUCATION_LEVEL_COMPLETED_CODE, forclosureCase.BorrowerEducLevelCompletedCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0212, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0212));
            if (!referenceCode.Validate(ReferenceCode.MARITAL_STATUS_CODE, forclosureCase.BorrowerMaritalStatusCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0213, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0213));
            if (!referenceCode.Validate(ReferenceCode.LANGUAGE_CODE, forclosureCase.BorrowerPreferredLangCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0214, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0214));            
            if (!referenceCode.Validate(ReferenceCode.SUMMARY_SENT_OTHER_CODE, forclosureCase.SummarySentOtherCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0215, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0215));
            if (!referenceCode.Validate(ReferenceCode.PROPERTY_CODE, forclosureCase.PropertyCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0216, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0216));
            if (!referenceCode.Validate(ReferenceCode.MILITARY_SERVICE_CODE, forclosureCase.MilitaryServiceCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0217, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0217));
            if (!referenceCode.Validate(ReferenceCode.CREDIT_BURREAU_CODE, forclosureCase.IntakeCreditBureauCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0218, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0218));
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
            for (int i = 0; i < caseLoanCollection.Count; i++)
            {
                CaseLoanDTO caseLoan = caseLoanCollection[i];
                if (!referenceCode.Validate(ReferenceCode.LOAN_1ST_2ND, caseLoan.Loan1st2nd))
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0219, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0219) + " working on case loan index " + (i + 1));
                if (!referenceCode.Validate(ReferenceCode.MORTGAGE_TYPE_CODE, caseLoan.MortgageTypeCd))
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0220, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0220) + " working on case loan index " + (i + 1));
                if (!referenceCode.Validate(ReferenceCode.TERM_LENGTH_CODE, caseLoan.TermLengthCd))
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0221, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0221) + " working on case loan index " + (i + 1));
                if (!referenceCode.Validate(ReferenceCode.LOAN_DELINQUENCY_STATUS_CODE, caseLoan.LoanDelinqStatusCd))
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0222, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0222) + " working on case loan index " + (i + 1));
                if (!referenceCode.Validate(ReferenceCode.MORTGAGE_PROGRAM_CODE, caseLoan.MortgageProgramCd))
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for MortgateProgramCode" + " working on case loan index " + (i + 1));
                if(!CheckValidServicerId(caseLoan.ServicerId))
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid ID was provided for ServicerId" + " working on case loan index " + (i + 1));
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
            if (!string.IsNullOrEmpty(forclosureCase.ContactZip) && forclosureCase.ContactZip.Length != 5)
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0257, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0257));
            if (!string.IsNullOrEmpty(forclosureCase.PropZip) && forclosureCase.PropZip.Length != 5)
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
            AgencyDTOCollection agencyCollection = foreclosureCaseSetDAO.GetAgency();
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
            bool isCall = foreclosureCaseSetDAO.GetCall(forclosureCase.CallId);
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
            ServicerDTOCollection servicerCollection = foreclosureCaseSetDAO.GetServicer();
            if (servicerId == null)
                return true;
            else if (servicerCollection == null || servicerCollection.Count < 1)
                return false;
            foreach (ServicerDTO item in servicerCollection)
            {
                if (item.ServicerID == servicerId)
                    return true;
            }
            return false;
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
            BudgetSubcategoryDTOCollection budgetSubcategoryCollection = foreclosureCaseSetDAO.GetBudgetSubcategory();
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
        private ExceptionMessageCollection CheckValidOutcomeTypeId(OutcomeItemDTOCollection outcomeItem)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (outcomeItem == null || outcomeItem.Count < 1)
                return null;
            for (int i = 0; i < outcomeItem.Count; i++)
            {
                OutcomeItemDTO item = outcomeItem[i];
                bool isValid = CheckOutcomeType(item);
                if (!isValid)
                    msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0263, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0263) + " working on outcome item index " + (i + 1));   
            }
            return msgFcCaseSet;
        }

        private bool CheckOutcomeType(OutcomeItemDTO outcomeItem)
        {
            OutcomeTypeDTOCollection outcomeTypeCollection = foreclosureCaseSetDAO.GetOutcomeType();
            if (outcomeTypeCollection == null || outcomeTypeCollection.Count < 1)
                return true;
            int? outcomeTypeId = outcomeItem.OutcomeTypeId;
            if (outcomeTypeId == null)
                return true;
            foreach (OutcomeTypeDTO item in outcomeTypeCollection)
            {
                if (item.OutcomeTypeID == outcomeTypeId)
                    return true;
            }
            return false;
        }
        #endregion

        #region CheckCaseIsComplete
        // <summary>
        /// Check Data Input is Complete or not base on warning message   
        /// </summary>        
        private bool CheckComplete(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {
            if (foreclosureCaseSetInput == null)
                return false;
            if (WarningMessage.Count == 0)
                return true;
            else
                return false;          
        }        
        #endregion

        #region Funcrions Set HP-Auto
        /// <summary>
        /// Add value HPF-Auto for ForclosureCase        
        /// </summary>
        private ForeclosureCaseDTO AssignForeclosureCaseHPFAuto(ForeclosureCaseSetDTO foreclosureCaseSet)
        {                                            
            ForeclosureCaseDTO foreclosureCase = foreclosureCaseSet.ForeclosureCase;            
            int? fcId = foreclosureCase.FcId;
            IsCaseCompleted = CheckComplete(foreclosureCaseSet);            
            if (IsCaseCompleted)
            {                
                foreclosureCase.CompletedDt = GetCompleteDate(fcId);                                
            }
            return foreclosureCase;
        }        

        /// <summary>
        /// Set value for Complete Date
        /// </summary>
        private DateTime? GetCompleteDate(int? fcId)
        {
            ForeclosureCaseDTO foreclosureCase;
            if (fcId != int.MinValue && fcId > 0)
            {
                foreclosureCase = GetForeclosureCase(fcId);
                if (foreclosureCase.CompletedDt == null)
                    return DateTime.Now;
                if(CheckInactiveCase(fcId))
                    return DateTime.Now;
                return foreclosureCase.CompletedDt;
            }
            return DateTime.Now;
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
        private BudgetSetDTO AssignBudgetSetHPFAuto(ForeclosureCaseSetDAO foreClosureCaseSetDAO, ForeclosureCaseSetDTO foreclosureCaseSet)
        {            
            BudgetSetDTO budgetSet = new BudgetSetDTO();

            BudgetAssetDTOCollection budgetAssetCollection = foreclosureCaseSet.BudgetAssets;
            BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSet.BudgetItems;
            double? totalIncome = 0;
            double? totalExpenses = 0;
            double? totalAssest = 0;

            if ((budgetAssetCollection == null || budgetAssetCollection.Count < 1) && (budgetItemCollection == null || budgetItemCollection.Count < 1))
                return null;

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
            BudgetDTOCollection budgetCollection = foreClosureCaseSetDAO.GetBudget();
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
        private ForeclosureCaseDTO GetForeclosureCase(int? fcId)
        {
            if (_dbFcCase == null || (_dbFcCase != null && _dbFcCase.FcId != fcId))
                _dbFcCase = foreclosureCaseSetDAO.GetForeclosureCase(fcId);

            return _dbFcCase;
        }

        #region Throw Detail Exception

        private void ThrowDataValidationException(string errorCode)
        {
            DataValidationException ex = new DataValidationException();
            ex.ExceptionMessages.AddExceptionMessage(errorCode, ErrorMessages.GetExceptionMessage(errorCode));
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
                em.ErrorCode = "WARNING";
                em.Message = string.Format("The duplicated Case Loan is Loan Number: {0}, Servicer Name: {1}, Borrower First Name: {2}, Borrower Last Name: {3}, Agency Name: {4}, Agency Case Number: {5}, Counselor Full Name: {6} {7},Counselor Phone {8} - Ext: {9}, Counselor Email: {10} "
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
                return s;
            s = s.ToUpper().Trim();
            return s;
        }

        private string ConvertStringEmptyToNull(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;            
            return s;
        }
        #endregion

        #endregion             
    }
}
