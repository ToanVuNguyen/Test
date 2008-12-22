using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.Utils.Exceptions;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation;


namespace HPF.FutureState.BusinessLogic
{
    public class ForeclosureCaseBL : BaseBusinessLogic
    {
        private static readonly ForeclosureCaseBL instance = new ForeclosureCaseBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static ForeclosureCaseBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected ForeclosureCaseBL()
        {
            
        }

        #region Implementation of IForclosureCaseBL

        /// <summary>
        /// Save a ForeclosureCase
        /// </summary>
        /// <param name="foreClosureCaseSet">ForeclosureCaseSetDTO</param>
        public void SaveForeclosureCaseSet(ForeclosureCaseSetDTO foreClosureCaseSet)
        {
            //Validation here     
            if (RequireFieldsValidation(foreClosureCaseSet))
            {
                ForeclosureCaseDTO fcCase = foreClosureCaseSet.ForeclosureCase;

                if (fcCase != null && fcCase.FcId != 0)
                {
                    var foreClosureCaseSetDAO = ForeclosureCaseSetDAO.CreateInstance();
                    //Biz call
                    try
                    {
                        ProcessInsertUpdateWithForeclosureCaseId(foreClosureCaseSet);   
                    }
                    catch (Exception ex)
                    {
                        
                        throw ex;
                    }
                }
                else
                {
                    if (fcCase == null)
                    {
                        throw new ProcessingException();
                    }
                    else if (fcCase.FcId == 0)
                    {
                        //Biz call
                        try
                        {
                            ProcessInsertUpdateWithoutForeclosureCaseId(foreClosureCaseSet);
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                    }
                }
            }
            else
                throw new ProcessingException();
        }

        /// <summary>
        /// return ForeclosureCase search result 
        /// </summary>
        /// <param name="searchCriteria">search criteria</param>
        /// <returns>collection of ForeclosureCaseWSDTO and collection of exception messages if there are any</returns>
        public ForeclosureCaseSearchResult SearchForeclosureCase(ForeclosureCaseSearchCriteriaDTO searchCriteria)
        {
            ForeclosureCaseSearchResult searchResult = new ForeclosureCaseSearchResult();
            
            ExceptionMessageCollection exceptionMessages = new ExceptionMessageCollection();
            //exceptionMessages = HPFValidator.ValidateToExceptionMessage<ForeclosureCaseSearchCriteriaDTO>(searchCriteria);

            //HPFValidator is not complete yet, it does not get the content of the message
            //so use the system validator just for testing
            Validator<ForeclosureCaseSearchCriteriaDTO> validator = ValidationFactory.CreateValidator<ForeclosureCaseSearchCriteriaDTO>("Default");
            ValidationResults validationResults = validator.Validate(searchCriteria);
            //if (exceptionMessages != null || exceptionMessages.Count > 0)
            if (!validationResults.IsValid)
            {
                searchResult.Messages = new DataValidationException();
                //searchResult.Messages.ExceptionMessages = exceptionMessages;
                foreach (ValidationResult result in validationResults)
                {
                    searchResult.Messages.ExceptionMessages.AddExceptionMessage(0, result.Message);
                }
            }
            else
            {
                searchResult = ForeclosureCaseSetDAO.CreateInstance().SearchForeclosureCase(searchCriteria);
            }

            return searchResult;
        }
        #endregion

        #region functions to serve SaveForeclosureCaseSet

        #region Functions check min request validate
        /// <summary>
        /// Min request validate the fore closure case set
        /// 1: Min request validate of Fore Closure Case
        /// 2: Min request validate of Budget Item Collection
        /// 3: Min request validate of Outcome Item Collection
        /// 4: Min request validate of Case Loan Collection
        /// </summary>
        bool RequireFieldsValidation(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            ForeclosureCaseDTO foreclosureCase = foreclosureCaseSet.ForeclosureCase;
            BudgetItemDTOCollection budgetItem = foreclosureCaseSet.BudgetItems;
            OutcomeItemDTOCollection outcomeItem = foreclosureCaseSet.Outcome;
            CaseLoanDTOCollection caseLoanItem = foreclosureCaseSet.CaseLoans;
            bool rfForeclosureCase = RequireFieldsForeclosureCase(foreclosureCase, "Default");
            bool rfbudgetItem = RequireFieldsBudgetItem(budgetItem, "Default");
            bool rfoutcomeItem = RequireFieldsOutcomeItem(outcomeItem, "Default");
            bool rfcaseLoanItem = RequireFieldsCaseLoanItem(caseLoanItem, "Default");
            if (rfForeclosureCase && rfbudgetItem && rfoutcomeItem && rfcaseLoanItem)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 1: Min request validate of Fore Closure Case
        /// <return>bool</return>
        /// </summary>
        bool RequireFieldsForeclosureCase(ForeclosureCaseDTO foreclosureCase, string ruleSet)
        {
            Validator<ForeclosureCaseDTO> cValidator = ValidationFactory.CreateValidator<ForeclosureCaseDTO>(ruleSet);
            ValidationResults validationResults = cValidator.Validate(foreclosureCase);
            if (validationResults.IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 2:  Min request validate of Budget Item Collection
        /// <return>bool</return>
        /// </summary>
        bool RequireFieldsBudgetItem(BudgetItemDTOCollection budgetItemDTOCollection, string ruleSet)
        {
            if (budgetItemDTOCollection != null && budgetItemDTOCollection.Count > 0)
            {
                foreach (BudgetItemDTO item in budgetItemDTOCollection)
                {
                    Validator<BudgetItemDTO> cValidator = ValidationFactory.CreateValidator<BudgetItemDTO>(ruleSet);
                    ValidationResults validationResults = cValidator.Validate(item);
                    if (!validationResults.IsValid)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 3:  Min request validate of Outcome Item Collection
        /// <return>bool</return>
        /// </summary>
        bool RequireFieldsOutcomeItem(OutcomeItemDTOCollection outcomeItemDTOCollection, string ruleSet)
        {
            if (outcomeItemDTOCollection != null && outcomeItemDTOCollection.Count > 0)
            {
                foreach (OutcomeItemDTO item in outcomeItemDTOCollection)
                {
                    Validator<OutcomeItemDTO> cValidator = ValidationFactory.CreateValidator<OutcomeItemDTO>(ruleSet);
                    ValidationResults validationResults = cValidator.Validate(item);
                    if (!validationResults.IsValid)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 4:  Min request validate of Case Loan Collection
        /// <return>bool</return>
        /// </summary>
        bool RequireFieldsCaseLoanItem(CaseLoanDTOCollection caseLoanDTOCollection, string ruleSet)
        {
            if (caseLoanDTOCollection != null && caseLoanDTOCollection.Count > 0)
            {
                foreach (CaseLoanDTO item in caseLoanDTOCollection)
                {
                    Validator<CaseLoanDTO> cValidator = ValidationFactory.CreateValidator<CaseLoanDTO>(ruleSet);
                    ValidationResults validationResults = cValidator.Validate(item);
                    if (!validationResults.IsValid)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// Check ForeclosureCase Id is existed or not
        /// </summary>
        bool CheckValidFCIdForAgency(int fcId, int agencyId)
        {
            ForeclosureCaseDTO fcCase = GetForeclosureCase(fcId);
            if (fcCase == null)
                return false;
            else
            {
                if (agencyId == fcCase.AgencyId) return true;
                else return false;
            }
        }

        /// <summary>
        /// Check Inactive(Over one year)
        /// <input>datetime</input>
        /// <return>bool<return>
        /// </summary>
        private bool CheckInactiveCase(DateTime completeDate)
        {
            DateTime currentDate = DateTime.Now;
            DateTime backOneYear = DateTime.MinValue;
            if (completeDate == null)
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
                else
                {
                    return true;
                }
            }
        }

        #region Functions check valid code
        /// <summary>
        /// Check valid code
        /// <input>ForeclosureCaseSetDTO</input>
        /// <return>bool<return>
        /// </summary>
        private bool CheckValidCode(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            if (foreclosureCaseSet != null && foreclosureCaseSet.ForeclosureCase != null && foreclosureCaseSet.CaseLoans != null
                // Check valid code for ForeclosureCase
                && CheckCode(foreclosureCaseSet.ForeclosureCase.IncomeEarnersCd, CodeConstants.INCOME_EARNERS_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.CaseSourceCd, CodeConstants.CASE_RESOURCE_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.RaceCd, CodeConstants.RACE_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.HouseholdCd, CodeConstants.HOUSEHOLD_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.NeverBillReasonCd, CodeConstants.NEVER_BILL_REASON_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.NeverPayReasonCd, CodeConstants.NEVER_PAY_REASON_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.DfltReason1stCd, CodeConstants.DEFAULT_REASON_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.DfltReason2ndCd, CodeConstants.DEFAULT_REASON_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.HudTerminationReasonCd, CodeConstants.HUD_TERMINATION_REASON_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.HudOutcomeCd, CodeConstants.HUD_OUTCOME_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.CounselingDurationCd, CodeConstants.COUNSELING_DURARION_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.GenderCd, CodeConstants.GENDER_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.ContactStateCd, CodeConstants.CONTACT_STATE_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.CaseSourceCd, CodeConstants.PROP_STATE_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.BorrowerEducLevelCompletedCd, CodeConstants.BORROWER_EDUC_LEVEL_COMPLETED_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.BorrowerMaritalStatusCd, CodeConstants.BORROWER_MARITAL_STATUS_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.BorrowerPreferredLangCd, CodeConstants.BORROWER_PREFERRED_LANG_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.BorrowerOccupationCd, CodeConstants.BORROWER_OCCUPATION_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.CoBorrowerOccupationCd, CodeConstants.CO_BORROWER_OCCUPATION_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.SummarySentOtherCd, CodeConstants.SUMMARY_SENT_OTHER_CD)
                && CheckCode(foreclosureCaseSet.ForeclosureCase.MilitaryServiceCd, CodeConstants.MILITARY_SERVICE_CD)
                // Check valid code for Case Loan
                && CheckValidCodeForCaseLoan(foreclosureCaseSet.CaseLoans)
            )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check valid code for case loan
        /// <input>CaseLoanDTOCollection</input>
        /// <return>bool<return>
        /// </summary>
        private bool CheckValidCodeForCaseLoan(CaseLoanDTOCollection caseLoanCollection)
        {
            foreach (CaseLoanDTO caseLoanDTO in caseLoanCollection)
            {
                if (!CheckCode(caseLoanDTO.MortgageTypeCd, CodeConstants.MORTGAGE_TYPE_CD)
                     || !CheckCode(caseLoanDTO.TermLengthCd, CodeConstants.TERM_LENGTH_CD)
                     || !CheckCode(caseLoanDTO.LoanDelinqStatusCd, CodeConstants.LOAN_DELINQUENCY_STATUS_CD)
                    )
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check valid code
        /// <input>string, string</input>
        /// <return>bool<return>
        /// </summary>
        private bool CheckCode(string codeValue, string codeName)
        {
            RefCodeItemDTOCollection refCodeItemCollection = HPFCacheManager.Instance.GetData<RefCodeItemDTOCollection>("refCodeItem");            
            if (refCodeItemCollection == null)
            {
                refCodeItemCollection = RefCodeItem.Instance.GetRefCodeItem();
                HPFCacheManager.Instance.Add("refCodeItem", refCodeItemCollection);
            } 
            if (codeValue == string.Empty || codeValue == null)
            {
                return true;
            }
            else
            {
                foreach (RefCodeItemDTO items in refCodeItemCollection)
                {
                    if (items.Code == codeValue && items.RefCodeSetName == codeName)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        #endregion

        /// <summary>
        /// Check Duplicated Fore Closure Case
        /// </summary>
        bool CheckDuplicateCase(ForeclosureCaseDTO foreclosureCase)
        {
            if (foreclosureCase.FcId != 0)
                return ForeclosureCaseSetDAO.CreateInstance().CheckDuplicate(foreclosureCase.FcId);
            else
                return ForeclosureCaseSetDAO.CreateInstance().CheckDuplicate(foreclosureCase.AgencyId, foreclosureCase.AgencyCaseNum);
        }

        /// <summary>
        /// Check existed AgencyId and Case number
        /// </summary>
        bool CheckExistingAgencyIdAndCaseNumber(int agencyId, string caseNumner)
        {
            return ForeclosureCaseSetDAO.CreateInstance().CheckExistingAgencyIdAndCaseNumber(agencyId, caseNumner);
        }

        #region Functions Check MiscError
        /// <summary>
        /// Check Misc Error Exception
        /// Case 1: Cannot Un-complete a Previously Completed Case
        /// Case 2: Two First Mortgages Not Allowed in a Case 
        /// </summary>
        bool MiscErrorException(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            bool case1 = CheckMiscErrorCase1(foreclosureCaseSet);
            bool case2 = CheckMiscErrorCase2(foreclosureCaseSet);
            if (!case1 && !case2)
                return false;
            else
                return true;

        }

        /// <summary>
        /// Check Misc Error Exception
        /// Case 1: Cannot Un-complete a Previously Completed Case        
        /// </summary>        
        private bool CheckMiscErrorCase1(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {
            ForeclosureCaseDTO fcCase = GetForeclosureCase(foreclosureCaseSetInput.ForeclosureCase.FcId);
            //Check case if is copleted( have Completed Date)
            if (fcCase != null && fcCase.CompletedDt != DateTime.MinValue)
            {
                ForeclosureCaseDTO foreclosureCase = foreclosureCaseSetInput.ForeclosureCase;
                CaseLoanDTOCollection caseLoan = foreclosureCaseSetInput.CaseLoans;
                bool rfForeclosureCase = RequireFieldsForeclosureCase(foreclosureCase, "Complete");
                bool rfCaseLoan = RequireFieldsCaseLoanItem(caseLoan, "Complete");
                if (rfForeclosureCase && rfCaseLoan)
                    return false;
                else
                    return true;            
            }
            return false;
        }

        /// <summary>
        /// Check Misc Error Exception
        /// Case 2: Two First Mortgages Not Allowed in a Case         
        /// </summary>
        private bool CheckMiscErrorCase2(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {
            int count = 0;
            CaseLoanDTOCollection caseLoan = foreclosureCaseSetInput.CaseLoans;
            foreach (CaseLoanDTO item in caseLoan)
            {
                if (item.Loan1st2nd == "1st")
                {
                    count = count + 1;
                }
            }
            if (count > 1)
                return false;
            else
                return true;
        }
        #endregion

        #region Function Update Fore Closure Case Set
        /// <summary>
        /// Update the Fore Closure Case
        /// </summary>
        void UpdateForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            var foreClosureCaseSetDAO = ForeclosureCaseSetDAO.CreateInstance();
            try
            {
                foreClosureCaseSetDAO.Begin();
                ForeclosureCaseDTO foreclosureCase = foreclosureCaseSet.ForeclosureCase;
                CaseLoanDTOCollection caseLoanCollection = foreclosureCaseSet.CaseLoans;
                OutcomeItemDTOCollection outcomeItemCollection = foreclosureCaseSet.Outcome;
                BudgetSetDTO budgetSet = foreclosureCaseSet.BudgetSet;
                BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSet.BudgetItems;
                BudgetAssetDTOCollection budgetAssetCollection = foreclosureCaseSet.BudgetAssets;
                //Insert table Foreclosure_Case
                //Return Fc_id
                int fcId = foreClosureCaseSetDAO.UpdateForeclosureCase(foreclosureCase);
                //check changed from budgetItem and budget asset
                //if they are changed, insert new budget set, budget Item and budget asset
                if (IsInsertBudgetSet(budgetItemCollection, budgetAssetCollection, fcId))
                {
                    //Insert Table Budget Set
                    //Return Budget Set Id
                    int budget_set_id = foreClosureCaseSetDAO.InsertBudgetSet(budgetSet, fcId);
                    //Insert Table Budget Item
                    foreach (BudgetItemDTO items in budgetItemCollection)
                    {
                        foreClosureCaseSetDAO.InsertBudgetItem(items, budget_set_id);
                    }
                    //Insert table Budget Asset
                    foreach (BudgetAssetDTO items in budgetAssetCollection)
                    {
                        foreClosureCaseSetDAO.InsertBudgetAsset(items, budget_set_id);
                    }
                }
                //check outcome item Input with outcome item DB
                //if not exist, insert new
                OutcomeItemDTOCollection outcomeCollecionNew = CheckOutcomeItemInputwithDB(outcomeItemCollection, fcId);
                if (outcomeCollecionNew != null)
                {
                    foreach (OutcomeItemDTO items in outcomeCollecionNew)
                    {
                        foreClosureCaseSetDAO.InsertOutcomeItem(items, fcId);
                    }
                }
                //check outcome item DB with outcome item input
                //if not exit, update outcome_deleted_dt = today()
                outcomeCollecionNew = CheckOutcomeItemDBwithInput(outcomeItemCollection, fcId);
                if (outcomeCollecionNew != null)
                {
                    foreach (OutcomeItemDTO items in outcomeCollecionNew)
                    {
                        foreClosureCaseSetDAO.UpdateOutcomeItem(items);
                    }
                }
                //Check for Delete Case Loan
                CaseLoanDTOCollection caseLoanCollecionNew = CheckCaseLoanForDelete(caseLoanCollection, fcId);
                if(caseLoanCollecionNew != null)
                {
                    foreach (CaseLoanDTO items in caseLoanCollecionNew)
                    {
                        foreClosureCaseSetDAO.DeleteCaseLoan(items);
                    }
                }
                //Check for Update Case Loan
                caseLoanCollecionNew = CheckCaseLoanForUpdate(caseLoanCollection, fcId);
                if (caseLoanCollecionNew != null)
                {
                    foreach (CaseLoanDTO items in caseLoanCollecionNew)
                    {
                        foreClosureCaseSetDAO.UpdateCaseLoan(items);
                    }
                }
                //Check for Insert Case Loan
                caseLoanCollecionNew = CheckCaseLoanForInsert(caseLoanCollection, fcId);
                if (caseLoanCollecionNew != null)
                {
                    foreach (CaseLoanDTO items in caseLoanCollecionNew)
                    {
                        foreClosureCaseSetDAO.InsertCaseLoan(items, fcId);
                    }
                }
                foreClosureCaseSetDAO.Commit();
            }
            catch (Exception)
            {
                foreClosureCaseSetDAO.Cancel();
                throw;
            }
        }
        #endregion

        #region Function Insert Fore Closure Case Set
        /// <summary>
        /// Insert the Fore Closure Case
        /// </summary>
        void InsertForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            var foreClosureCaseSetDAO = ForeclosureCaseSetDAO.CreateInstance();
            try
            {
                foreClosureCaseSetDAO.Begin();
                ForeclosureCaseDTO foreclosureCase = foreclosureCaseSet.ForeclosureCase;
                CaseLoanDTOCollection caseLoanCollection = foreclosureCaseSet.CaseLoans;
                OutcomeItemDTOCollection outcomeItemCollection = foreclosureCaseSet.Outcome;
                BudgetSetDTO budgetSet = foreclosureCaseSet.BudgetSet;
                BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSet.BudgetItems;
                BudgetAssetDTOCollection budgetAssetCollection = foreclosureCaseSet.BudgetAssets;
                //Insert table Foreclosure_Case
                //Return Fc_id
                int fcId = foreClosureCaseSetDAO.InsertForeclosureCase(foreclosureCase);
                //Insert table Case Loan
                foreach (CaseLoanDTO items in caseLoanCollection)
                {
                    foreClosureCaseSetDAO.InsertCaseLoan(items, fcId);
                }
                //Insert Table Outcome Item
                foreach (OutcomeItemDTO items in outcomeItemCollection)
                {
                    foreClosureCaseSetDAO.InsertOutcomeItem(items, fcId);
                }
                //Insert Table Budget Set
                //Return Budget Set Id
                int budget_set_id = foreClosureCaseSetDAO.InsertBudgetSet(budgetSet, fcId);
                //Insert Table Budget Item
                foreach (BudgetItemDTO items in budgetItemCollection)
                {
                    foreClosureCaseSetDAO.InsertBudgetItem(items, budget_set_id);
                }
                //Insert table Budget Asset
                foreach (BudgetAssetDTO items in budgetAssetCollection)
                {
                    foreClosureCaseSetDAO.InsertBudgetAsset(items, budget_set_id);
                }
                foreClosureCaseSetDAO.Commit();
            }
            catch (Exception)
            {                
                foreClosureCaseSetDAO.Cancel();                
                throw;
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
        private bool IsBudgetItemsDifference(BudgetItemDTOCollection budgetCollectionInput, int fc_id)
        {            
            BudgetItemDTOCollection budgetCollectionDB = BudgetItemDAO.Instance.GetBudgetSet(fc_id);
            if (budgetCollectionDB != null && budgetCollectionDB.Count == budgetCollectionInput.Count)
            {
                foreach (BudgetItemDTO budgetItem in budgetCollectionInput)
                {
                    if (!CompareBudgetItem(budgetItem, budgetCollectionDB))
                    {
                        return true;
                    }
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
                    && budgetItemInput.BudgetNote == budgetItemDB.BudgetNote)
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
        private bool IsBudgetAssetDifference(BudgetAssetDTOCollection budgetCollectionInput, int fc_id)
        {            
            BudgetAssetDTOCollection budgetCollectionDB = BudgetAssetDAO.Instance.GetBudgetSet(fc_id);
            if (budgetCollectionDB != null && budgetCollectionDB.Count == budgetCollectionInput.Count)
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
        private bool IsInsertBudgetSet(BudgetItemDTOCollection budgetItemCollection, BudgetAssetDTOCollection budgetAssetCollection, int fc_id)
        {
            bool budgetItem = IsBudgetItemsDifference(budgetItemCollection, fc_id);
            bool budgetAsset = IsBudgetAssetDifference( budgetAssetCollection, fc_id);
            if (budgetItem || budgetItem)
                return true;
            else
                return false;
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
        private OutcomeItemDTOCollection CheckOutcomeItemInputwithDB(OutcomeItemDTOCollection outcomeItemCollectionInput, int fc_id)
        {
            OutcomeItemDTOCollection outcomeItemNew = new OutcomeItemDTOCollection();
            OutcomeItemDTOCollection outcomeItemCollectionDB = HPFCacheManager.Instance.GetData<OutcomeItemDTOCollection>("outcomeItem");
            if (outcomeItemCollectionDB == null)
            {
                outcomeItemCollectionDB = OutcomeItemDAO.Instance.GetOutcomeItemCollection(fc_id);
                HPFCacheManager.Instance.Add("outcomeItem", outcomeItemCollectionDB);
            }    
            //Compare OutcomeItem input with OutcomeItem DB
            foreach(OutcomeItemDTO itemInput in outcomeItemCollectionInput)
            {
                bool isChanged = CheckOutcomeItem(itemInput, outcomeItemCollectionDB);
                if (!isChanged)
                {
                    outcomeItemNew.Add(itemInput);                 
                }
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
        private OutcomeItemDTOCollection CheckOutcomeItemDBwithInput(OutcomeItemDTOCollection outcomeItemCollectionInput, int fc_id)
        {
            OutcomeItemDTOCollection outcomeItemNew = new OutcomeItemDTOCollection();
            OutcomeItemDTOCollection outcomeItemCollectionDB = HPFCacheManager.Instance.GetData<OutcomeItemDTOCollection>("outcomeItem");
            if (outcomeItemCollectionDB == null)
            {
                outcomeItemCollectionDB = OutcomeItemDAO.Instance.GetOutcomeItemCollection(fc_id);
                HPFCacheManager.Instance.Add("outcomeItem", outcomeItemCollectionDB);
            }             
            //Compare OutcomeItem input with OutcomeItem DB
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
                    && outcomeItem.OutcomeDt == item.OutcomeDt
                    && outcomeItem.NonprofitreferralKeyNum == item.NonprofitreferralKeyNum
                    && outcomeItem.ExtRefOtherName == item.ExtRefOtherName)
                    return true;
            }
            return false;
        }
       
        #endregion

        #region Functions check for update Case Loan
        // <summary>                
        /// </summary>
        /// <param name>CaseLoanDTO, caseLoanCollection</param>
        /// <returns>bool</returns>
        private bool CheckCaseLoan(CaseLoanDTO caseLoan, CaseLoanDTOCollection caseLoanCollection)
        {
            foreach (CaseLoanDTO item in caseLoanCollection)
            {
                if (caseLoan.FcId == item.FcId
                    && caseLoan.ServicerId == item.ServicerId
                    && caseLoan.AcctNum == item.AcctNum)
                    return true;
            }
            return false;
        }

        private bool CheckCaseLoanUpdate(CaseLoanDTO caseLoan, CaseLoanDTOCollection caseLoanCollection)
        {
            foreach (CaseLoanDTO item in caseLoanCollection)
            {
                if (caseLoan.FcId == item.FcId && caseLoan.ServicerId == item.ServicerId && caseLoan.AcctNum == item.AcctNum)
                {
                    if (caseLoan.OtherServicerName != item.OtherServicerName
                        || caseLoan.Loan1st2nd != item.Loan1st2nd
                        || caseLoan.MortgageTypeCd != item.MortgageTypeCd
                        || caseLoan.ArmLoanInd != item.ArmLoanInd
                        || caseLoan.ArmResetInd != item.ArmResetInd
                        || caseLoan.TermLengthCd != item.TermLengthCd
                        || caseLoan.LoanDelinqStatusCd != item.LoanDelinqStatusCd
                        || caseLoan.CurrentLoanBalanceAmt != item.CurrentLoanBalanceAmt
                        || caseLoan.OrigLoanAmt != item.OrigLoanAmt
                        || caseLoan.InterestRate != item.InterestRate
                        || caseLoan.OriginatingLenderName != item.OriginatingLenderName
                        || caseLoan.OrigMortgageCoFdicNcusNum != item.OrigMortgageCoFdicNcusNum
                        || caseLoan.OrigMortgageCoName != item.OrigMortgageCoName
                        || caseLoan.OrginalLoanNum != item.OrginalLoanNum
                        || caseLoan.FdicNcusNumCurrentServicerTbd != item.FdicNcusNumCurrentServicerTbd
                        || caseLoan.CurrentServicerNameTbd != item.CurrentServicerNameTbd
                        || caseLoan.FreddieLoanNum != item.FreddieLoanNum
                        )
                        return false;
                }                
            }
            return true;
        }

        private CaseLoanDTOCollection CheckCaseLoanForInsert(CaseLoanDTOCollection caseLoanCollection, int fc_id)
        {
            CaseLoanDTOCollection caseLoanNew = new CaseLoanDTOCollection();
            CaseLoanDTOCollection caseLoanCollectionDB = HPFCacheManager.Instance.GetData<CaseLoanDTOCollection>("caseLoanItem");
            if (caseLoanCollectionDB == null)
            {
                caseLoanCollectionDB = CaseLoanDAO.Instance.GetCaseLoanCollection(fc_id);
                HPFCacheManager.Instance.Add("caseLoanItem", caseLoanCollectionDB);
            }
            //Compare CAseLoanItem input with Case Loan DB
            foreach (CaseLoanDTO itemInput in caseLoanCollection)
            {
                bool isChanged = CheckCaseLoan(itemInput, caseLoanCollectionDB);
                if (!isChanged)
                {
                    caseLoanNew.Add(itemInput);
                }
            }
            return caseLoanNew;   
        }

        private CaseLoanDTOCollection CheckCaseLoanForDelete(CaseLoanDTOCollection caseLoanCollection, int fc_id)
        {
            CaseLoanDTOCollection caseLoanNew = new CaseLoanDTOCollection();
            CaseLoanDTOCollection caseLoanCollectionDB = HPFCacheManager.Instance.GetData<CaseLoanDTOCollection>("caseLoanItem");
            if (caseLoanCollectionDB == null)
            {
                caseLoanCollectionDB = CaseLoanDAO.Instance.GetCaseLoanCollection(fc_id);
                HPFCacheManager.Instance.Add("caseLoanItem", caseLoanCollectionDB);
            }
            //Compare CAseLoanItem input with Case Loan DB
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

        private CaseLoanDTOCollection CheckCaseLoanForUpdate(CaseLoanDTOCollection caseLoanCollection, int fc_id)
        {
            CaseLoanDTOCollection caseLoanNew = new CaseLoanDTOCollection();
            CaseLoanDTOCollection caseLoanCollectionDB = HPFCacheManager.Instance.GetData<CaseLoanDTOCollection>("caseLoanItem");
            if (caseLoanCollectionDB == null)
            {
                caseLoanCollectionDB = CaseLoanDAO.Instance.GetCaseLoanCollection(fc_id);
                HPFCacheManager.Instance.Add("caseLoanItem", caseLoanCollectionDB);
            }
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

        /// <summary>
        /// Get Foreclosure case basing on its fc_id
        /// </summary>
        /// <param name="fc_id">id for a ForeclosureCase</param>
        /// <returns>object of ForeclosureCase </returns>
        ForeclosureCaseDTO GetForeclosureCase(int fc_id)
        {
            return ForeclosureCaseSetDAO.CreateInstance().GetForeclosureCase(fc_id);
        }


        void ProcessUpdateForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            if (foreclosureCaseSet != null)
            {
                if (MiscErrorException(foreclosureCaseSet))
                    throw new MiscProcessingException();
                else
                    try
                    {
                        UpdateForeclosureCaseSet(foreclosureCaseSet);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
            }
            else
                throw new ProcessingException();

        }

        void ProcessInsertForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            if (foreclosureCaseSet != null && foreclosureCaseSet.ForeclosureCase != null)
            {
                if (CheckDuplicateCase(foreclosureCaseSet.ForeclosureCase))
                    throw new ProcessingException();
                else
                {
                    if (MiscErrorException(foreclosureCaseSet))
                        throw new MiscProcessingException();
                    else
                        try
                        {
                            InsertForeclosureCaseSet(foreclosureCaseSet);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                }
            }
            else
                throw new ProcessingException();
        }

        void ProcessInsertUpdateWithoutForeclosureCaseId(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            if (foreclosureCaseSet != null && foreclosureCaseSet.ForeclosureCase !=null)
            {
                if (foreclosureCaseSet.ForeclosureCase.AgencyCaseNum != null && foreclosureCaseSet.ForeclosureCase.AgencyId != 0)
                {
                    if (CheckExistingAgencyIdAndCaseNumber(foreclosureCaseSet.ForeclosureCase.AgencyId, foreclosureCaseSet.ForeclosureCase.AgencyCaseNum))
                        throw new ProcessingException();
                    else
                        try
                        {
                            ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }            
                }
                else
                    throw new ProcessingException();
            }
            else
                throw new ProcessingException();
        }

        void ProcessInsertUpdateWithForeclosureCaseId(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            if (foreclosureCaseSet != null && foreclosureCaseSet.ForeclosureCase != null)
            {
                ForeclosureCaseDTO fc = foreclosureCaseSet.ForeclosureCase;
                if (fc.FcId > 0) //CheckInvalidFCID
                {
                    if (CheckValidFCIdForAgency(fc.FcId, fc.AgencyId))
                    {
                        if (CheckInactiveCase(fc.CompletedDt))
                            try
                            {
                                ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                        else
                            throw new ProcessingException();
                    }
                    else
                        throw new ProcessingException();
                }
                else
                    throw new ProcessingException();
            }
            else
                throw new ProcessingException();
        }
        #endregion
    }
}
