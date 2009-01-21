using System;
using System.Collections.Generic;
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
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace HPF.FutureState.BusinessLogic
{
    public class ForeclosureCaseSetBL : BaseBusinessLogic
    {
        private static readonly ForeclosureCaseSetBL instance = new ForeclosureCaseSetBL();
        ForeclosureCaseSetDAO foreclosureCaseSetDAO;
        private string _workingUserID;

        //public DuplicateException WarningMessage { get; set; }

        public ExceptionMessageCollection WarningMessage { get; set; } 
    
        /// <summary>
        /// Singleton
        /// </summary>
        public static ForeclosureCaseSetBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected ForeclosureCaseSetBL()
        {
            

        }

        #region Implementation of IForclosureCaseBL

        /// <summary>
        /// Save a ForeclosureCase
        /// </summary>
        /// <param name="foreClosureCaseSet">ForeclosureCaseSetDTO</param>
        public int SaveForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            int fcid = 0;
            try
            {
                InitiateTransaction();
                WarningMessage = new ExceptionMessageCollection();
                if (foreclosureCaseSet == null || foreclosureCaseSet.ForeclosureCase == null)
                    throw new DataValidationException(ErrorMessages.PROCESSING_EXCEPTION_NULL_FORECLOSURE_CASE_SET);

                ExceptionMessageCollection exceptionList = CheckRequireForPartial(foreclosureCaseSet);
                if (exceptionList != null && exceptionList.Count > 0)
                    ThrowDataValidationException(exceptionList);

                _workingUserID = foreclosureCaseSet.WorkingUserID;

                exceptionList = CheckInvalidFormatData(foreclosureCaseSet);
                if (exceptionList != null && exceptionList.Count > 0)
                    ThrowDataValidationException(exceptionList);

                ForeclosureCaseDTO fcCase = foreclosureCaseSet.ForeclosureCase;

                exceptionList = CheckValidCode(foreclosureCaseSet);
                if (exceptionList != null && exceptionList.Count > 0)
                    ThrowDataValidationException(exceptionList);
                
                if (fcCase.FcId > 0)
                    fcid = ProcessInsertUpdateWithForeclosureCaseId(foreclosureCaseSet);
                else
                    fcid = ProcessInsertUpdateWithoutForeclosureCaseId(foreclosureCaseSet);                
                CompleteTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw ex;
            }
            return fcid;         
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
            foreclosureCaseSetDAO = ForeclosureCaseSetDAO.CreateInstance();
            foreclosureCaseSetDAO.Begin();
        }

        /// <summary>
        /// return ForeclosureCase search result 
        /// </summary>
        /// <param name="searchCriteria">search criteria</param>
        /// <returns>collection of ForeclosureCaseWSDTO and collection of exception messages if there are any</returns>
        public ForeclosureCaseSearchResult SearchForeclosureCase(ForeclosureCaseSearchCriteriaDTO searchCriteria, int pageSize)
        {
            ExceptionMessageCollection exceptionMessages = new ExceptionMessageCollection();
            ValidationResults validationResults = HPFValidator.Validate<ForeclosureCaseSearchCriteriaDTO>(searchCriteria);
            if (!validationResults.IsValid)
            {                
                DataValidationException dataValidationException = new DataValidationException();
                foreach (ValidationResult result in validationResults)
                {
                    dataValidationException.ExceptionMessages.AddExceptionMessage(result.Message);
                }
                throw dataValidationException;

            }
            
            return ForeclosureCaseDAO.CreateInstance().SearchForeclosureCase(searchCriteria, pageSize);
        }
        #endregion

        #region Functions to serve SaveForeclosureCaseSet

        private int ProcessUpdateForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {            
            ExceptionMessageCollection exceptionList = MiscErrorException(foreclosureCaseSet);
            if (exceptionList != null && exceptionList.Count > 0)
                ThrowDataValidationException(exceptionList);

            DuplicatedCaseLoanDTOCollection collection = CheckDuplicateCase(foreclosureCaseSet);
            ForeclosureCaseDTO fcCase = foreclosureCaseSet.ForeclosureCase;
            ForeclosureCaseDTO dbFcCase = GetForeclosureCase(fcCase.FcId);
            fcCase.NeverPayReasonCd = dbFcCase.NeverPayReasonCd;
            fcCase.NeverBillReasonCd = dbFcCase.NeverBillReasonCd;
            if (collection != null && collection.Count > 0)
            {
                fcCase.DuplicateInd = Constant.DUPLICATE_YES;
                WarningMessage.Add(CreateDuplicateCaseWarning(collection));
            }
            else
            {
                if (dbFcCase.DuplicateInd.ToUpper().Equals(Constant.DUPLICATE_YES.ToUpper()))
                    fcCase.DuplicateInd = Constant.DUPLICATE_NO;
                if (dbFcCase.NeverBillReasonCd.ToUpper().Equals(Constant.NEVER_BILL_REASON_CODE_DUPE))
                    fcCase.NeverBillReasonCd = null;

                if (dbFcCase.NeverPayReasonCd.ToUpper().Equals(Constant.NEVER_PAY_REASON_CODE_DUPE))
                    fcCase.NeverPayReasonCd = null;
                WarningMessage = null;
            }
            
            return UpdateForeclosureCaseSet(foreclosureCaseSet);
        }

        private int ProcessInsertForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            DuplicatedCaseLoanDTOCollection collection = CheckDuplicateCase(foreclosureCaseSet);
            if (collection != null && collection.Count > 0)
            {
                ThrowDuplicateCaseException(collection);
            }
            foreclosureCaseSet.ForeclosureCase.DuplicateInd = Constant.DUPLICATE_NO;
            ExceptionMessageCollection exceptionList = MiscErrorException(foreclosureCaseSet);
            if (exceptionList != null && exceptionList.Count > 0)
                ThrowDataValidationException(exceptionList);
            return InsertForeclosureCaseSet(foreclosureCaseSet);
        }

        private int ProcessInsertUpdateWithoutForeclosureCaseId(ForeclosureCaseSetDTO foreclosureCaseSet)
        {                        
            ForeclosureCaseDTO fcCase = foreclosureCaseSet.ForeclosureCase;

            if (fcCase.AgencyCaseNum == null || fcCase.AgencyCaseNum == string.Empty || fcCase.AgencyId == 0)
                ThrowDataValidationException(ErrorMessages.ERR0250);
            
            if (CheckExistingAgencyIdAndCaseNumber(fcCase.AgencyId, fcCase.AgencyCaseNum))
                ThrowDataValidationException(ErrorMessages.ERR0254);                            
            
            return ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
        }

        private int ProcessInsertUpdateWithForeclosureCaseId(ForeclosureCaseSetDTO foreclosureCaseSet)
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
            
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            ExceptionMessageCollection msgFcCase = RequireFieldsForeclosureCase(foreclosureCase, ruleSet);
            if (msgFcCase != null && msgFcCase.Count != 0)
                msgFcCaseSet.Add(msgFcCase);

            ExceptionMessageCollection msgCaseSet = RequireFieldsForeclosureCaseSet(foreclosureCaseSet, ruleSet);
            if (msgCaseSet != null && msgCaseSet.Count != 0)
                msgFcCaseSet.Add(msgCaseSet);

            ExceptionMessageCollection msgBudgetItem = RequireFieldsBudgetItem(budgetItem, ruleSet);
            if (msgBudgetItem != null && msgBudgetItem.Count != 0)
                msgFcCaseSet.Add(msgBudgetItem);

            ExceptionMessageCollection msgOutcomeItem = RequireFieldsOutcomeItem(outcomeItem, ruleSet);
            if (msgOutcomeItem != null && msgOutcomeItem.Count != 0)
                msgFcCaseSet.Add(msgOutcomeItem);

            ExceptionMessageCollection msgCaseLoanItem = RequireFieldsCaseLoanItem(caseLoanItem, ruleSet);
            if (msgCaseLoanItem != null && msgCaseLoanItem.Count != 0)
                msgFcCaseSet.Add(msgCaseLoanItem);         

            if (msgFcCaseSet.Count == 0)
                return null;
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
        /// 0: Check Create UserID and Change Last User ID
        /// <return>Collection Message Error</return>
        /// </summary>
        private ExceptionMessageCollection RequireFieldsForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCase, string ruleSet)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            ExceptionMessageCollection ex = HPFValidator.ValidateToGetExceptionMessage<ForeclosureCaseSetDTO>(foreclosureCase, ruleSet);
            if (ex != null && ex.Count != 0)
            {
                msgFcCaseSet.Add(ex);
            }           
            return msgFcCaseSet;
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 1: Min request validate of Fore Closure Case
        /// <return>Collection Message Error</return>
        /// </summary>
        private ExceptionMessageCollection RequireFieldsForeclosureCase(ForeclosureCaseDTO foreclosureCase, string ruleSet)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            ExceptionMessageCollection ex = HPFValidator.ValidateToGetExceptionMessage<ForeclosureCaseDTO>(foreclosureCase, ruleSet);
            if (ex != null && ex.Count != 0)
            {
                msgFcCaseSet.Add(ex);
            }
            if (ruleSet == Constant.RULESET_MIN_REQUIRE_FIELD)
            {
                ExceptionMessageCollection msgOther = CheckOtherFieldFCaseForPartial(foreclosureCase);
                if (msgOther != null)
                    msgFcCaseSet.Add(msgOther);
            }
            return msgFcCaseSet;
        }

        private ExceptionMessageCollection CheckOtherFieldFCaseForPartial(ForeclosureCaseDTO foreclosureCase)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (foreclosureCase == null)
                return null;
            //-----CoBorrowerFname, CoBorrowerLname
            if (ConvertStringEmptyToNull(foreclosureCase.CoBorrowerFname) == null && ConvertStringEmptyToNull(foreclosureCase.CoBorrowerLname) != null)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "A CoBorrowerFname is required to save a foreclosure case.");
            else if (ConvertStringEmptyToNull(foreclosureCase.CoBorrowerFname) != null && ConvertStringEmptyToNull(foreclosureCase.CoBorrowerLname) == null)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "A CoBorrowerLname is required to save a foreclosure case.");
            //-----BankruptcyInd, BankruptcyAttorney, BankruptcyPmtCurrentInd
            if (ConvertStringEmptyToNull(foreclosureCase.BankruptcyAttorney) != null && ConvertStringEmptyToNull(ConvertStringToUpper(foreclosureCase.BankruptcyInd)) != Constant.BANKRUPTCY_IND_YES)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "BankruptcyInd value should be Y.");
            if (ConvertStringEmptyToNull(ConvertStringToUpper(foreclosureCase.BankruptcyInd)) == Constant.BANKRUPTCY_IND_YES && ConvertStringEmptyToNull(foreclosureCase.BankruptcyAttorney) == null)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "A BankruptcyAttorney is required to save a foreclosure case.");
            if (ConvertStringEmptyToNull(ConvertStringToUpper(foreclosureCase.BankruptcyInd)) == Constant.BANKRUPTCY_IND_YES && ConvertStringEmptyToNull(foreclosureCase.BankruptcyPmtCurrentInd) == null)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "A BankruptcyPmtCurrentInd is required to save a foreclosure case.");
            //-----SummarySentOtherCd, SummarySentOtherDt
            if (ConvertStringEmptyToNull(foreclosureCase.SummarySentOtherCd) == null && foreclosureCase.SummarySentOtherDt != DateTime.MinValue)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "A SummarySentOtherCd is required to save a foreclosure case.");
            else if (ConvertStringEmptyToNull(foreclosureCase.SummarySentOtherCd) != null && foreclosureCase.SummarySentOtherDt == DateTime.MinValue)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "A SummarySentOtherDt is required to save a foreclosure case.");
            //-----SrvcrWorkoutPlanCurrentInd
            if (ConvertStringEmptyToNull(foreclosureCase.SrvcrWorkoutPlanCurrentInd) == null && ConvertStringEmptyToNull(foreclosureCase.HasWorkoutPlanInd) != null)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "A SrvcrWorkoutPlanCurrentInd is required to save a foreclosure case.");
            //-----HomeSalePrice
            if (ConvertStringEmptyToNull(ConvertStringToUpper(foreclosureCase.ForSaleInd)) == Constant.FORSALE_IND_YES && foreclosureCase.HomeSalePrice == 0)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "A HomeSalePrice is required to save a foreclosure case.");
            if (msgFcCaseSet.Count == 0)
                return null;
            return msgFcCaseSet;
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 2:  Min request validate of Budget Item Collection
        /// <return>Collection Message Error</return>
        /// </summary>
        private ExceptionMessageCollection RequireFieldsBudgetItem(BudgetItemDTOCollection budgetItemDTOCollection, string ruleSet)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (budgetItemDTOCollection == null || budgetItemDTOCollection.Count < 1)
                return null;
            foreach (BudgetItemDTO item in budgetItemDTOCollection)
            {
                ExceptionMessageCollection ex = HPFValidator.ValidateToGetExceptionMessage<BudgetItemDTO>(item, ruleSet);
                if (ex != null && ex.Count != 0)
                    return ex;
            }           
            return msgFcCaseSet;
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 3:  Min request validate of Outcome Item Collection
        /// <return>Collection Message Error</return>
        /// </summary>
        private ExceptionMessageCollection RequireFieldsOutcomeItem(OutcomeItemDTOCollection outcomeItemDTOCollection, string ruleSet)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (outcomeItemDTOCollection == null || outcomeItemDTOCollection.Count < 1)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "Missing Outcome item. At least one Outcome item is required to save a foreclosure case.");            
            int outComeTypeId = FindOutcomeTypeIdWithNameIsExternalReferral();
            foreach (OutcomeItemDTO item in outcomeItemDTOCollection)
            {
                ExceptionMessageCollection ex = HPFValidator.ValidateToGetExceptionMessage<OutcomeItemDTO>(item, ruleSet);
                if (ex != null && ex.Count != 0)
                {
                    msgFcCaseSet.Add(ex);
                    break;
                }
            }            
            if (ruleSet == Constant.RULESET_MIN_REQUIRE_FIELD)
            {
                foreach (OutcomeItemDTO item in outcomeItemDTOCollection)
                {
                    ExceptionMessageCollection msgOthers = CheckOtherFieldOutcomeItemForPartial(outComeTypeId, item);
                    if (msgOthers != null)
                    {
                        msgFcCaseSet.Add(msgOthers);
                        break;
                    }
                }
            }
            return msgFcCaseSet;
        }

        private ExceptionMessageCollection CheckOtherFieldOutcomeItemForPartial(int outComeTypeId, OutcomeItemDTO item)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (item.OutcomeTypeId == outComeTypeId && outComeTypeId != 0 && (ConvertStringEmptyToNull(item.NonprofitreferralKeyNum) == null && ConvertStringEmptyToNull(item.ExtRefOtherName) == null))
            {
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An NonprofitreferralKeyNum or ExtRefOtherName is required");
            }            
            if(msgFcCaseSet.Count == 0)
                return null;
            return msgFcCaseSet;
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 4:  Min request validate of Case Loan Collection
        /// <return>Collection Message Error</return>
        /// </summary>       
        private ExceptionMessageCollection RequireFieldsCaseLoanItem(CaseLoanDTOCollection caseLoanDTOCollection,string ruleSet)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (caseLoanDTOCollection == null || caseLoanDTOCollection.Count < 1)
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0126, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0126));
            int servicerId = FindServicerIDWithNameIsOther();            
            foreach (CaseLoanDTO item in caseLoanDTOCollection)
            {
                ExceptionMessageCollection ex = HPFValidator.ValidateToGetExceptionMessage<CaseLoanDTO>(item, ruleSet);
                if (ex != null && ex.Count != 0)
                {
                    msgFcCaseSet.Add(ex);
                    break;
                }
            }

            if (ruleSet == Constant.RULESET_MIN_REQUIRE_FIELD)
            {
                foreach (CaseLoanDTO item in caseLoanDTOCollection)
                {                    
                    ExceptionMessageCollection msgOther = CheckOtherFieldCaseLoanForPartial(servicerId,item);
                    if (msgOther != null)
                    {
                        msgFcCaseSet.Add(msgOther);
                        break;
                    }
                }
            }            
            return msgFcCaseSet;
        }

        private ExceptionMessageCollection CheckOtherFieldCaseLoanForPartial(int servicerId, CaseLoanDTO item)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (item.ServicerId == servicerId && ConvertStringEmptyToNull(item.OtherServicerName) == null)
            {
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An OtherServicerName is required");
            }
            if (msgFcCaseSet.Count == 0)
                return null;
            return msgFcCaseSet;
        }

        private int FindServicerIDWithNameIsOther()
        {
            ServicerDTOCollection serviers = foreclosureCaseSetDAO.GetServicer();
            foreach(ServicerDTO item in serviers)
            {
                string servicerName = ConvertStringToUpper(item.ServicerName);
                if (servicerName == Constant.SERVICER_OTHER)
                    return item.ServicerID;
            }
            return 0;
        }

        private int FindOutcomeTypeIdWithNameIsExternalReferral()
        {
            OutcomeTypeDTOCollection outcomeType = foreclosureCaseSetDAO.GetOutcomeType();
            foreach (OutcomeTypeDTO item in outcomeType)
            {
                string outcomeTypeName = ConvertStringToUpper(item.OutcomeTypeName);
                if (outcomeTypeName == Constant.OUTCOME_TYPE_NAME_EXTERNAL_REFERAL)
                    return item.OutcomeTypeID;
            }
            return 0;
        }

        private int FindSubCatWithNameIsMortgage()
        {
            BudgetSubcategoryDTOCollection budgetSubCat = foreclosureCaseSetDAO.GetBudgetSubcategory();
            foreach (BudgetSubcategoryDTO item in budgetSubCat)
            {
                string budgetSubName = ConvertStringToUpper(item.BudgetSubcategoryName);
                if (budgetSubName == Constant.SUB_CATEGORY_NAME_MORTGAGE)
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
        private bool CheckInactiveCase(int fcId)
        {
            DateTime currentDate = DateTime.Now;
            DateTime backOneYear = DateTime.MinValue;            
            ForeclosureCaseDTO foreclosureCase = GetForeclosureCase(fcId);            
            DateTime completeDate = foreclosureCase.CompletedDt;
            if (completeDate == null || completeDate == DateTime.MinValue)
            {
                return false;
            }
            else
            {
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
        }      

        /// <summary>
        /// Check Duplicated Fore Closure Case
        /// </summary>
        private DuplicatedCaseLoanDTOCollection CheckDuplicateCase(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            return foreclosureCaseSetDAO.GetDuplicatedCases(foreclosureCaseSet);
            //if (foreclosureCase.FcId > 0)
            //    return foreclosureCaseSetDAO.CheckDuplicate(foreclosureCase.FcId);
            //else
            //    return foreclosureCaseSetDAO.CheckDuplicate(foreclosureCase.AgencyId, foreclosureCase.AgencyCaseNum);
        }

        /// <summary>
        /// Check existed AgencyId and Case number
        /// </summary>
        bool CheckExistingAgencyIdAndCaseNumber(int agencyId, string caseNumner)
        {
            return foreclosureCaseSetDAO.CheckExistingAgencyIdAndCaseNumber(agencyId, caseNumner);
        }

        #region Check Invalid Format Data
        private ExceptionMessageCollection CheckInvalidFormatData(ForeclosureCaseSetDTO fCaseSet)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            //Special character
            ExceptionMessageCollection ex = ValidationFieldByRuleSet(fCaseSet, Constant.RULESET_LENGTH);
            ExceptionMessageCollection msgBudgetAsset = RequireFieldsBudgetAsset(fCaseSet.BudgetAssets, Constant.RULESET_LENGTH);
            if (ex != null)
                msgFcCaseSet.Add(ex);
            if (msgBudgetAsset != null && msgBudgetAsset.Count > 0)
                msgFcCaseSet.Add(msgBudgetAsset);
            if (!CheckDateOfBirth(fCaseSet.ForeclosureCase.BorrowerDob))
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "Age of the Borrower must be >=12 and <=110");
            if (!CheckDateOfBirth(fCaseSet.ForeclosureCase.CoBorrowerDob))
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "Age of the Co_borrower must be >=12 and <=110");
            if (!CheckSpecialCharacrer(fCaseSet.ForeclosureCase.BorrowerFname))
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "BorrowerFname must not include the following characters:!@#$%^*(){}|:;?><567890");
            if (!CheckSpecialCharacrer(fCaseSet.ForeclosureCase.BorrowerLname))
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "BorrowerLname must not include the following characters:!@#$%^*(){}|:;?><567890");
            if (!CheckSpecialCharacrer(fCaseSet.ForeclosureCase.CoBorrowerFname))
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "CoBorrowerFname must not include the following characters:!@#$%^*(){}|:;?><567890");
            if (!CheckSpecialCharacrer(fCaseSet.ForeclosureCase.CoBorrowerLname))
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "CoBorrowerLname must not include the following characters:!@#$%^*(){}|:;?><567890");
            if (msgFcCaseSet.Count == 0)
                return null;
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check Special characrer in Name
        /// </summary>
        private bool CheckSpecialCharacrer(string s)
        {
            if (s == null || s == string.Empty)
                return true;
            Regex regex = new Regex(@"[!@#$%^*(){}|:;?><567890]");
            if (regex.IsMatch(s))
                return false;
            return true;
        }

        /// <summary>
        /// Check MaxLength for budgetAsset
        /// </summary>
        private ExceptionMessageCollection RequireFieldsBudgetAsset(BudgetAssetDTOCollection budgetAssetDTOCollection, string ruleSet)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (budgetAssetDTOCollection == null || budgetAssetDTOCollection.Count < 1)
                return null;
            foreach (BudgetAssetDTO item in budgetAssetDTOCollection)
            {
                ExceptionMessageCollection ex = HPFValidator.ValidateToGetExceptionMessage<BudgetAssetDTO>(item, ruleSet);
                if (ex != null && ex.Count != 0)
                    return ex;
            }
            return msgFcCaseSet;
        }

        
        /// <summary>
        /// Check Old between 12 and 110
        /// </summary>
        private bool CheckDateOfBirth(DateTime dateOfBirth)
        {
            int systemYear = DateTime.Now.Year;
            if (dateOfBirth != null && dateOfBirth != DateTime.MinValue)
            {
                int yearOfBirth = dateOfBirth.Year;
                int calculateYear = systemYear - yearOfBirth;
                if (calculateYear >= 12 && calculateYear <= 110)
                    return true;
                return false;
            }
            return true;
        }        
        #endregion       

        #region Functions Check MiscError
        /// <summary>
        /// Check Misc Error Exception
        /// Case 1: Cannot Un-complete a Previously Completed Case
        /// Case 2: Two First Mortgages Not Allowed in a Case AND Case Loan must have atleast 1st (If case completed)
        /// Case 3: Cannot resubmit the case complete without billable outcome
        /// Case 4: Budget Item must have atleast 1 budget_item = 'Mortgage Amount'.(If case completed)
        /// return TRUE if have Error
        /// </summary>
        private ExceptionMessageCollection MiscErrorException(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();

            int fcId = foreclosureCaseSet.ForeclosureCase.FcId;
            bool caseComplete = CheckForeclosureCaseComplete(fcId);

            ExceptionMessageCollection msgCase1 = CheckUnCompleteCaseComplete(foreclosureCaseSet, caseComplete);
            if (msgCase1 != null && msgCase1.Count != 0) 
                msgFcCaseSet.Add(msgCase1);

            ExceptionMessageCollection msgCase2 = CheckFirstMortgages(foreclosureCaseSet, caseComplete);
            if (msgCase2 != null && msgCase2.Count != 0)
                msgFcCaseSet.Add(msgCase2);

            ExceptionMessageCollection msgCase3 = CheckBillableOutCome(foreclosureCaseSet, caseComplete);
            if (msgCase3 != null && msgCase3.Count != 0)
                msgFcCaseSet.Add(msgCase3);

            ExceptionMessageCollection msgCase4 = CheckMortgageBudgetItem(foreclosureCaseSet, caseComplete);
            if (msgCase4 != null && msgCase4.Count != 0)
                msgFcCaseSet.Add(msgCase4);

            if (msgFcCaseSet.Count == 0)
                return null;
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
            if (msgRequire != null && msgRequire.Count > 0 && caseComplete)
                msgFcCaseSet.Add(msgRequire);

            ExceptionMessageCollection msgComplete = ValidationFieldByRuleSet(foreclosureCaseSetInput, Constant.RULESET_COMPLETE);
            if (msgComplete != null && msgComplete.Count > 0 && caseComplete)            
                msgFcCaseSet.Add(msgComplete);
            if (msgComplete != null && msgComplete.Count > 0)                            
                WarningMessage.Add(msgComplete);            
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check Case in Database is NULL or Complete Or Not Complete
        /// Null = Not Complete => return false
        /// CompleteDt != null => complete => return true
        /// </summary>     
        private bool CheckForeclosureCaseComplete(int fcId)
        {
            ForeclosureCaseDTO fcCase = GetForeclosureCase(fcId);            
            bool caseComplete = false;
            if (fcCase != null && fcCase.CompletedDt != DateTime.MinValue)
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
            if (!CheckBudgetItemHaveMortgage(foreclosureCaseSetInput) && caseComplete)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "BudgetItem must exist and it must have atleast 1 budget_item = 'Mortgage Amount'");
            if(!CheckBudgetItemHaveMortgage(foreclosureCaseSetInput))
                WarningMessage.AddExceptionMessage("UNKNOWN", "To be complete, a budget_item must exist and it must have atleast 1 budget_item = 'Mortgage Amount'.");
            if (foreclosureCaseSetInput.BudgetItems == null || foreclosureCaseSetInput.BudgetItems.Count < 1)
                WarningMessage.AddExceptionMessage(ErrorMessages.WARN0327, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0327));
            return msgFcCaseSet;            
        }
        #endregion

        #region Function Update Fore Closure Case Set
        /// <summary>
        /// Update the Fore Closure Case
        /// </summary>
        private int UpdateForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            ForeclosureCaseDTO foreclosureCase = ForeclosureCaseHPAuto(foreclosureCaseSet);
            CaseLoanDTOCollection caseLoanCollection = foreclosureCaseSet.CaseLoans;
            OutcomeItemDTOCollection outcomeItemCollection = foreclosureCaseSet.Outcome;
            BudgetSetDTO budgetSet = BudgetSetHPAuto(foreclosureCaseSetDAO, foreclosureCaseSet);
            BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSet.BudgetItems;
            BudgetAssetDTOCollection budgetAssetCollection = foreclosureCaseSet.BudgetAssets;            
            //Insert table Foreclosure_Case
            //Return Fc_id
            int fcId = UpdateForeClosureCase(foreclosureCaseSetDAO, foreclosureCase);

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

            return fcId;
        }
        
        /// <summary>
        /// Update foreclosureCase
        /// </summary>
        private int UpdateForeClosureCase(ForeclosureCaseSetDAO foreClosureCaseSetDAO, ForeclosureCaseDTO foreclosureCase)
        {
            foreclosureCase.SetUpdateTrackingInformation(_workingUserID);
            int fcId = foreClosureCaseSetDAO.UpdateForeclosureCase(foreclosureCase);
            return fcId;
        }
        #endregion

        #region Function Insert Fore Closure Case Set
        /// <summary>
        /// Insert the Fore Closure Case
        /// </summary>
        private int InsertForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            ForeclosureCaseDTO foreclosureCase = ForeclosureCaseHPAuto(foreclosureCaseSet);
            CaseLoanDTOCollection caseLoanCollection = foreclosureCaseSet.CaseLoans;
            OutcomeItemDTOCollection outcomeItemCollection = OutcomeHPAuto(foreclosureCaseSet);
            BudgetSetDTO budgetSet = BudgetSetHPAuto(foreclosureCaseSetDAO, foreclosureCaseSet);
            BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSet.BudgetItems;
            BudgetAssetDTOCollection budgetAssetCollection = foreclosureCaseSet.BudgetAssets;            
            //Insert table Foreclosure_Case
            //Return Fc_id
            int fcId = InsertForeclosureCase(foreclosureCaseSetDAO, foreclosureCase);

            //Insert table Case Loan
            InsertCaseLoan(foreclosureCaseSetDAO, caseLoanCollection, fcId);

            //Insert Table Outcome Item
            InsertOutcomeItem(foreclosureCaseSetDAO, outcomeItemCollection, fcId);

            //Insert Table Budget Set
            //Return Budget Set Id
            int budgetSetId = InsertBudgetSet(foreclosureCaseSetDAO, budgetSet, fcId);

            //Insert Table Budget Item
            InsertBudgetItem(foreclosureCaseSetDAO, budgetItemCollection, budgetSetId);

            //Insert table Budget Asset
            InsertBudgetAsset(foreclosureCaseSetDAO, budgetAssetCollection, budgetSetId);

            return fcId;
        }
        #endregion

        #region Functions for Insert and Update tables
        /// <summary>
        /// Insert Budget
        /// </summary>
        private void InsertBudget(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetSetDTO budgetSet, BudgetItemDTOCollection budgetItemCollection, BudgetAssetDTOCollection budgetAssetCollection, int fcId)
        {
            bool isInsertBudget = IsInsertBudgetSet(foreClosureCaseSetDAO , budgetItemCollection, budgetAssetCollection, fcId);
            if (isInsertBudget)
            {
                //Insert Table Budget Set
                //Return Budget Set Id
                int budget_set_id = InsertBudgetSet(foreClosureCaseSetDAO, budgetSet, fcId);
                //Insert Table Budget Item
                InsertBudgetItem(foreClosureCaseSetDAO, budgetItemCollection, budget_set_id);
                //Insert table Budget Asset
                InsertBudgetAsset(foreClosureCaseSetDAO, budgetAssetCollection, budget_set_id);
            }
        }
        /// <summary>
        /// Insert Budget Asset
        /// </summary>
        private void InsertBudgetAsset(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetAssetDTOCollection budgetAssetCollection, int budget_set_id)
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
        private void InsertBudgetItem(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetItemDTOCollection budgetItemCollection, int budget_set_id)
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
        private int InsertBudgetSet(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetSetDTO budgetSet, int fcId)
        {
            int budget_set_id = int.MinValue;
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
        private int InsertForeclosureCase(ForeclosureCaseSetDAO foreClosureCaseSetDAO, ForeclosureCaseDTO foreclosureCase)
        {
            int fcId = int.MinValue;
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
        private void InsertOutcomeItem(ForeclosureCaseSetDAO foreClosureCaseSetDAO, OutcomeItemDTOCollection outcomeItemCollection, int fcId)
        {
            if (outcomeItemCollection != null)
            {
                foreach (OutcomeItemDTO items in outcomeItemCollection)
                {
                    items.SetInsertTrackingInformation(_workingUserID);
                    foreClosureCaseSetDAO.InsertOutcomeItem(items, fcId);
                }
            }
        }

        /// <summary>
        /// Insert CaseLoan
        /// </summary>
        private void InsertCaseLoan(ForeclosureCaseSetDAO foreClosureCaseSetDAO, CaseLoanDTOCollection caseLoanCollection, int fcId)
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
        private bool IsBudgetItemsDifference(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetItemDTOCollection budgetCollectionInput, int fcId)
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
        private bool IsBudgetAssetDifference(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetAssetDTOCollection budgetCollectionInput, int fcId)
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
                if (budgetAssetInput.AssetName == budgetAssetDB.AssetName
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
        private bool IsInsertBudgetSet(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetItemDTOCollection budgetItemCollection, BudgetAssetDTOCollection budgetAssetCollection, int fcId)
        {
            bool budgetItem = IsBudgetItemsDifference(foreClosureCaseSetDAO, budgetItemCollection, fcId);
            bool budgetAsset = IsBudgetAssetDifference(foreClosureCaseSetDAO, budgetAssetCollection, fcId);
            return (budgetItem || budgetItem);                
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
        private OutcomeItemDTOCollection CheckOutcomeItemInputwithDB(ForeclosureCaseSetDAO foreClosureCaseSetDAO, OutcomeItemDTOCollection outcomeItemCollectionInput, int fcId)
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
        private OutcomeItemDTOCollection CheckOutcomeItemDBwithInput(ForeclosureCaseSetDAO foreClosureCaseSetDAO, OutcomeItemDTOCollection outcomeItemCollectionInput, int fcId)
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
                    && outcomeItem.OutcomeDt.Date == item.OutcomeDt.Date
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
                if (caseLoan.FcId == item.FcId
                    && ConvertStringToUpper(caseLoan.AcctNum) == ConvertStringToUpper(item.AcctNum))
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
                if (caseLoan.FcId == item.FcId && caseLoan.AcctNum == item.AcctNum)
                {
                    if (ConvertStringToUpper(caseLoan.OtherServicerName) != ConvertStringToUpper(item.OtherServicerName)
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
                        || ConvertStringToUpper(caseLoan.FdicNcusNumCurrentServicerTbd) != ConvertStringToUpper(item.FdicNcusNumCurrentServicerTbd)
                        || ConvertStringToUpper(caseLoan.CurrentServicerNameTbd) != ConvertStringToUpper(item.CurrentServicerNameTbd)
                        || ConvertStringToUpper(caseLoan.InvestorLoanNum) != item.InvestorLoanNum
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
        private CaseLoanDTOCollection CheckCaseLoanForInsert(ForeclosureCaseSetDAO foreClosureCaseSetDAO, CaseLoanDTOCollection caseLoanCollection, int fcId)
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
        private CaseLoanDTOCollection CheckCaseLoanForDelete(ForeclosureCaseSetDAO foreClosureCaseSetDAO, CaseLoanDTOCollection caseLoanCollection, int fcId)
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
        private CaseLoanDTOCollection CheckCaseLoanForUpdate(ForeclosureCaseSetDAO foreClosureCaseSetDAO,CaseLoanDTOCollection caseLoanCollection, int fcId)
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

            if(msgFcCaseSet.Count == 0)
                return null;
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
            if (!referenceCode.Validate(ReferenceCode.IncomeEarnersCode, forclosureCase.IncomeEarnersCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0200, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0200));
            if (!referenceCode.Validate(ReferenceCode.CaseResourceCode, forclosureCase.CaseSourceCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0201, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0201));
            if (!referenceCode.Validate(ReferenceCode.RaceCode, forclosureCase.RaceCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0202, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0202));
            if (!referenceCode.Validate(ReferenceCode.HouseholdCode, forclosureCase.HouseholdCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0203, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0203));
            if (!referenceCode.Validate(ReferenceCode.NeverBillReasonCode, forclosureCase.NeverBillReasonCd))
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for NeverBillReasonCd.");
            if (!referenceCode.Validate(ReferenceCode.NeverPayReasonCode, forclosureCase.NeverPayReasonCd))
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for NeverPayReasonCd.");
            if (!referenceCode.Validate(ReferenceCode.DefaultReasonCode, forclosureCase.DfltReason1stCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0204, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0204));
            if (!referenceCode.Validate(ReferenceCode.DefaultReasonCode, forclosureCase.DfltReason2ndCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0205, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0205));
            if (!referenceCode.Validate(ReferenceCode.HUDTerminationReasonCode, forclosureCase.HudTerminationReasonCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0206, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0206));
            if (!referenceCode.Validate(ReferenceCode.HUDOutcomeCode, forclosureCase.HudOutcomeCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0207, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0207));
            if (!referenceCode.Validate(ReferenceCode.CounselingDurarionCode, forclosureCase.CounselingDurationCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0208, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0208));
            if (!referenceCode.Validate(ReferenceCode.GenderCode, forclosureCase.GenderCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0209, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0209));
            if (!referenceCode.Validate(ReferenceCode.State, forclosureCase.ContactStateCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0210, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0210));
            if (!referenceCode.Validate(ReferenceCode.State, forclosureCase.PropStateCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0211, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0211));
            if (!referenceCode.Validate(ReferenceCode.EducationLevelCompletedCode, forclosureCase.BorrowerEducLevelCompletedCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0212, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0212));
            if (!referenceCode.Validate(ReferenceCode.MaritalStatusCode, forclosureCase.BorrowerMaritalStatusCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0213, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0213));
            if (!referenceCode.Validate(ReferenceCode.LanguageCode, forclosureCase.BorrowerPreferredLangCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0214, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0214));
            if (!referenceCode.Validate(ReferenceCode.OccupationCode, forclosureCase.BorrowerOccupationCd))
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for BorrowerOccupationCd.");
            if (!referenceCode.Validate(ReferenceCode.OccupationCode, forclosureCase.CoBorrowerOccupationCd))
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for CoBorrowerOccupationCd.");
            if (!referenceCode.Validate(ReferenceCode.SummarySentOtherCode, forclosureCase.SummarySentOtherCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0215, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0215));
            if (!referenceCode.Validate(ReferenceCode.PropertyCode, forclosureCase.PropertyCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0216, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0216));
            if (!referenceCode.Validate(ReferenceCode.MilitaryServiceCode, forclosureCase.MilitaryServiceCd))
                msgFcCaseSet.AddExceptionMessage(ErrorMessages.ERR0217, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0217));
            if (!referenceCode.Validate(ReferenceCode.CreditBurreauCode, forclosureCase.IntakeCreditBureauCd))
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for IntakeCreditBureauCd.");
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
                if (!referenceCode.Validate(ReferenceCode.Loan1st2nd, caseLoan.Loan1st2nd))
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for Loan1st2nd " + (i + 1));
                if (!referenceCode.Validate(ReferenceCode.MortgageTypeCode, caseLoan.MortgageTypeCd))
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for MortgageTypeCode " + (i + 1));
                if (!referenceCode.Validate(ReferenceCode.TermLengthCode, caseLoan.TermLengthCd))
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for TermLengthCode " + (i + 1));
                if (!referenceCode.Validate(ReferenceCode.LoanDelinquencyStatusCode, caseLoan.LoanDelinqStatusCd))
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for LoanDelinqStatusCode " + (i + 1));
                if(!CheckValidServicerId(caseLoan.ServicerId))
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid ID was provided for ServicerId " + (i + 1));
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
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "Combination ContactStateCode and ContactZipCode is invalid");
            if(propertyValid == false)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "Combination PropertyStateCode and PropertyZipcode is invalid");
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check valid combination contact state_code and contact zip code
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private bool CombinationContactValid(ForeclosureCaseDTO forclosureCase, GeoCodeRefDTO item)
        {
            return (forclosureCase.ContactZip == item.ZipCode && forclosureCase.ContactStateCd == item.StateAbbr);
        }

        /// <summary>
        /// Check valid combination property state_code and property zip code
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private bool CombinationPropertyValid(ForeclosureCaseDTO forclosureCase, GeoCodeRefDTO item)
        {
            return (forclosureCase.PropZip == item.ZipCode && forclosureCase.PropStateCd == item.StateAbbr);            
        }

        /// <summary>
        /// Check valid zipcode
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidZipCode(ForeclosureCaseDTO forclosureCase)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (forclosureCase.ContactZip.Length != 5)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for ContactZip");
            if (forclosureCase.PropZip.Length != 5)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid code was provided for PropZip");
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check valid AgencyId
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidAgencyId(ForeclosureCaseDTO forclosureCase)
        {
            string agencyName = foreclosureCaseSetDAO.GetAgencyName(forclosureCase.AgencyId);
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (agencyName == null || agencyName == string.Empty)
                msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid ID was provided for AgencyId");            
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check valid Call Id
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidCallId(ForeclosureCaseDTO forclosureCase)
        {
            if (forclosureCase.CallId == 0)
                return null;
            bool isCall = foreclosureCaseSetDAO.GetCall(forclosureCase.CallId);
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (!isCall)
                msgFcCaseSet.AddExceptionMessage("An invalid ID was provided for CallId");
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check valid AgencyId
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private bool CheckValidServicerId(int servicerId)
        {
            string servicerName = foreclosureCaseSetDAO.GetServicerName(servicerId);
            if (servicerName == null || servicerName == string.Empty)
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
            if (programCollection == null || programCollection.Count < 1)
                return null;
            int programId = forclosureCase.ProgramId;
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            foreach (ProgramDTO item in programCollection)
            {
                if (item.ProgramID == programId.ToString())
                    return null;
            }
            msgFcCaseSet.AddExceptionMessage("UNKNOWN", "An invalid ID was provided for ProgramId");
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
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "Budget item " + (i + 1) + " was provided invalid BudgetSubcategoryId");
            }
            return msgFcCaseSet;
        }

        private bool CheckBudgetSubcategory(BudgetItemDTO budgetItem)
        {
            BudgetSubcategoryDTOCollection budgetSubcategoryCollection = foreclosureCaseSetDAO.GetBudgetSubcategory();
            if (budgetSubcategoryCollection == null || budgetSubcategoryCollection.Count < 1)
                return true;
            int budgetSubId = budgetItem.BudgetSubcategoryId;
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
                    msgFcCaseSet.AddExceptionMessage("UNKNOWN", "Outcome item " + (i + 1) + " was provided invalid OutcomeTypeId");
            }
            return msgFcCaseSet;
        }

        private bool CheckOutcomeType(OutcomeItemDTO outcomeItem)
        {
            OutcomeTypeDTOCollection outcomeTypeCollection = foreclosureCaseSetDAO.GetOutcomeType();
            if (outcomeTypeCollection == null || outcomeTypeCollection.Count < 1)
                return true;
            int outcomeTypeId = outcomeItem.OutcomeTypeId;
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
        /// Check Data Input is Complete
        /// 1-Perform partial caes required fields check.
        /// 2-Perform complete case required fields check.
        /// 3-Perform check case loan have 1st
        /// 4-Perform check Outcome have Payable
        /// 5-Perform check Budget Item have Amount
        /// </summary>        
        private bool CheckComplete(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {
            if (foreclosureCaseSetInput == null)
                return false;
            bool isRequireField = CheckRequireFieldPartial(foreclosureCaseSetInput);
            bool isCompleteField = CheckRequireFieldForComplete(foreclosureCaseSetInput);
            bool isCaseLoanHaveOne1st = CheckCaseLoanHaveOne1st(foreclosureCaseSetInput);
            bool isOutcomeHavePayable = CheckOutcomeHavePayable(foreclosureCaseSetInput);
            bool isBudgetItemHaveAmount = CheckBudgetItemHaveMortgage(foreclosureCaseSetInput);
            return (isRequireField && isCompleteField && isCaseLoanHaveOne1st && isOutcomeHavePayable && isBudgetItemHaveAmount);
        }

        /// <summary>
        /// 1-Perform partial caes required fields check.
        /// </summary>
        private bool CheckRequireFieldPartial(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {
            ExceptionMessageCollection msgError = ValidationFieldByRuleSet(foreclosureCaseSetInput, Constant.RULESET_MIN_REQUIRE_FIELD);
            if (msgError == null || msgError.Count < 1)
                return true;
            return false;
        }

        /// <summary>
        /// 2-Perform complete case required fields check.
        /// </summary>
        private bool CheckRequireFieldForComplete(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {
            ExceptionMessageCollection msgError = ValidationFieldByRuleSet(foreclosureCaseSetInput, Constant.RULESET_COMPLETE);
            if (msgError == null || msgError.Count < 0)
                return true;
            return false;
        }

        /// <summary>
        /// 3-Perform check case loan have 1st
        /// </summary>
        private bool CheckCaseLoanHaveOne1st(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {
            CaseLoanDTOCollection caseLoanCollection = foreclosureCaseSetInput.CaseLoans;
            if (caseLoanCollection == null || caseLoanCollection.Count < 1)
                return false;
            foreach (CaseLoanDTO item in caseLoanCollection)
            {
                if (ConvertStringToUpper(item.Loan1st2nd) == Constant.LOAN_1ST)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 4-Perform check outcome have payable
        /// </summary>
        private bool CheckOutcomeHavePayable(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {
            OutcomeItemDTOCollection outcomeCollection = foreclosureCaseSetInput.Outcome;
            if (outcomeCollection == null || outcomeCollection.Count < 1)
                return false;
            foreach (OutcomeItemDTO item in outcomeCollection)
            {
                if (CheckBillableOutcome(item))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 5-Perform check Budget Item have Amount
        /// </summary>
        private bool CheckBudgetItemHaveMortgage(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {
            BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSetInput.BudgetItems;
            if (budgetItemCollection == null || budgetItemCollection.Count < 1)
                return false;
            int subCatId = FindSubCatWithNameIsMortgage();
            foreach (BudgetItemDTO item in budgetItemCollection)
            {
                if (item.BudgetSubcategoryId == subCatId && item.BudgetItemAmt != 0)
                    return true;
            }
            return false;
        }
        #endregion

        #region Funcrions Set HP-Auto
        /// <summary>
        /// Add value HPF-Auto for ForclosureCase        
        /// </summary>
        private ForeclosureCaseDTO ForeclosureCaseHPAuto(ForeclosureCaseSetDTO foreclosureCaseSet)
        {                                            
            ForeclosureCaseDTO foreclosureCase = foreclosureCaseSet.ForeclosureCase;            
            int fcId = foreclosureCase.FcId;
            bool isComplete = CheckComplete(foreclosureCaseSet);
            foreclosureCase.AmiPercentage = CalculateAmiPercentage();
            foreclosureCase.SummarySentDt = DateTime.Now;            
            if (isComplete == true)
            {
                foreclosureCase.CompletedDt = GetCompleteDate(fcId);
            }
            return foreclosureCase;
        }        

        /// <summary>
        /// Set value for Complete Date
        /// </summary>
        private DateTime GetCompleteDate(int fcId)
        {
            ForeclosureCaseDTO foreclosureCase = null;
            if (fcId != int.MinValue && fcId > 0)
            {
                foreclosureCase = GetForeclosureCase(fcId);
                if (foreclosureCase.CompletedDt == DateTime.MinValue)
                    return DateTime.Now;
                else
                    return foreclosureCase.CompletedDt;
            }
            return DateTime.Now;
        }      

        /// <summary>
        /// Set value for AmiPercentage
        /// </summary>
        private int CalculateAmiPercentage()
        {
            return 0;
        }

        /// <summary>
        /// Add value HPF-Auto for Outcome
        /// </summary>
        private OutcomeItemDTOCollection OutcomeHPAuto(ForeclosureCaseSetDTO foreclosureCaseSet)
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
        private BudgetSetDTO BudgetSetHPAuto(ForeclosureCaseSetDAO foreClosureCaseSetDAO, ForeclosureCaseSetDTO foreclosureCaseSet)
        {            
            BudgetSetDTO budgetSet = new BudgetSetDTO();

            BudgetAssetDTOCollection budgetAssetCollection = foreclosureCaseSet.BudgetAssets;
            BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSet.BudgetItems;
            double totalIncome = 0;
            double totalExpenses = 0;
            double totalAssest = 0;

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

            budgetSet.CreateUserId = foreclosureCaseSet.ForeclosureCase.CreateUserId;
            
            return budgetSet;
        }

        /// <summary>
        /// Calculate Total Assets
        /// </summary>
        private double CalculateTotalAssets(BudgetAssetDTOCollection budgetAssetCollection, double totalAssest)
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
        private void CalculateTotalExpenseAndIncome(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetItemDTOCollection budgetItemCollection, ref double totalIncome, ref double totalExpenses)
        {
            //Calculate totalExpenses, totalIncome
            if (budgetItemCollection == null || budgetItemCollection.Count < 1)
            {
                totalIncome = 0;
                totalExpenses = 0;
            }
            totalIncome = 0;
            totalExpenses = 0;
            foreach (BudgetItemDTO item in budgetItemCollection)
            {
                string budgetCode = BuggetCategoryCode(foreClosureCaseSetDAO, item.BudgetSubcategoryId);
                if (budgetCode != null)
                {
                    if (budgetCode == Constant.INCOME)
                        totalIncome += (double)item.BudgetItemAmt;
                    else if (budgetCode == Constant.EXPENSES)
                        totalExpenses += (double)item.BudgetItemAmt;
                }
            }
            
        }

        private string BuggetCategoryCode(ForeclosureCaseSetDAO foreClosureCaseSetDAO, int subCategoryId)
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
        /// <param name="fc_id">id for a ForeclosureCase</param>
        /// <returns>object of ForeclosureCase </returns>
        private ForeclosureCaseDTO GetForeclosureCase(int fcId)
        {
            return ForeclosureCaseDAO.CreateInstance().GetForeclosureCase(fcId);
        }

        private string GetAgencyName(int agencyID)
        {
            return AgencyDAO.Instance.GetAgencyName(agencyID);
        }

        private void UpdateFcCase_DuplicateIndicator(int fcid, string ind)
        {
            foreclosureCaseSetDAO.UpdateFcCase_DuplicateIndicator(fcid, ind);
        }

        #region Throw Detail Exception

        private void ThrowDataValidationException(string errorCode)
        {
            DataValidationException ex = new DataValidationException();
            ex.ExceptionMessages.AddExceptionMessage(errorCode, ErrorMessages.GetExceptionMessage(errorCode));
            throw ex;
        }

        private void ThrowDataValidationException(ExceptionMessageCollection exDetailCollection)
        {
            DataValidationException ex = new DataValidationException(exDetailCollection);
            throw ex;
        }

        private void ThrowInvalidFCIdForAgencyException(int fcId)
        {
            DataValidationException pe = new DataValidationException();
            ForeclosureCaseDTO fcCase = GetForeclosureCase(fcId);
            ExceptionMessage em = new ExceptionMessage();
            em.Message = string.Format("The case belongs to Agency: {0}, Counsellor: {1}, Contact number: {2}, email: {3}", GetAgencyName(fcCase.AgencyId), fcCase.CounselorFname + ", " + fcCase.CounselorLname, fcCase.CounselorPhone, fcCase.CounselorEmail);
            pe.ExceptionMessages.Add(em);

            throw pe;
        }

        private ExceptionMessageCollection CreateDuplicateCaseWarning(DuplicatedCaseLoanDTOCollection collection)
        {
            ExceptionMessageCollection de = new ExceptionMessageCollection();
            foreach (DuplicatedCaseLoanDTO obj in collection)
            {
                ExceptionMessage em = new ExceptionMessage();
                em.ErrorCode = "WARN0ING";
                em.Message = string.Format("The duplicated Case Loan is Loan Number: {0}, Servicer Name: {1}, Borrower First Name: {2}, Borrower Last Name: {3}, Agency Name: {4}, Agency Case Number: {5}, Counselor Full Name: {6} {7},Counselor Phone {8} - Ext: {9}, Counselor Email: {10} "
                            , obj.ServicerName, obj.LoanNumber, obj.PropertyZip, obj.BorrowerFirstName, obj.BorrowerLastName
                            , obj.CounselorFName, obj.CounselorLName, obj.AgencyName, obj.CounselorPhone, obj.CounselorEmail, obj.CounselorEmail);
                de.Add(em);
            }
            return de;
            
        }

        private void ThrowDuplicateCaseException(DuplicatedCaseLoanDTOCollection collection)
        {
            DuplicateException pe = new DuplicateException();
            foreach(DuplicatedCaseLoanDTO obj in collection)
            {
                ExceptionMessage em = new ExceptionMessage();
                em.ErrorCode = ErrorMessages.ERR0253;
                //string s = "Duplicate Case Found for Servicer: {0}, Account Number: {1}, Zip Code: {2}. "
                //                              + "Borrower Name: {3} {4} The case is currently being worked on by: {5} {6} of {7}. "
                //                              + "Counselor Phone: {8} {9} Counselor Email: {10} Last Outcome Date: {11} "
                //                              + "Last Outcome: {12} ";
                //string s = ;
                em.Message = string.Format(ErrorMessages.GetExceptionMessage(em.ErrorCode)
                            , obj.ServicerName, obj.LoanNumber, obj.PropertyZip, obj.BorrowerFirstName, obj.BorrowerLastName
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
            if (s == null || s == string.Empty)
                return s;
            else            
                s = s.ToUpper().Trim();
            return s;
        }

        private string ConvertStringEmptyToNull(string s)
        {
            if (s == null || s == string.Empty)
                return null;            
            return s;
        }
        #endregion

        #endregion             
    }
}
