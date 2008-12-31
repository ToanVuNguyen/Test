﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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

namespace HPF.FutureState.BusinessLogic
{
    public class ForeclosureCaseSetBL : BaseBusinessLogic
    {
        const int NUMBER_OF_ERROR_APP_SEARCH_CRITERIA = 11;
        const string LOAN_1ST_2ND = "1ST";
        const string INCOME = "1";
        const string EXPENSES = "2";
        const string CASE_COMPLETE_IND_YES = "Y";
        const string CASE_COMPLETE_IND_NO = "N";
        private static readonly ForeclosureCaseSetBL instance = new ForeclosureCaseSetBL();
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
        public void SaveForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            if (foreclosureCaseSet == null || foreclosureCaseSet.ForeclosureCase == null)
                throw new ProcessingException(ErrorMessages.PROCESSING_EXCEPTION_NULL_FORECLOSURE_CASE_SET);
           
            Collection<string> exDetailCollection = RequireFieldsValidation(foreclosureCaseSet);
            if (exDetailCollection != null && exDetailCollection.Count > 0)
            {
                ThrowMissingRequiredFieldsException(exDetailCollection);
            }
            
            ForeclosureCaseDTO fcCase = foreclosureCaseSet.ForeclosureCase;

            if (fcCase.FcId != 0)
            {                
                exDetailCollection = CheckValidCode(foreclosureCaseSet);
                if (exDetailCollection != null && exDetailCollection.Count > 0)
                {
                    ThrowInvalidCodeException(exDetailCollection);
                }
                ProcessInsertUpdateWithForeclosureCaseId(foreclosureCaseSet);
            }
            else
                ProcessInsertUpdateWithoutForeclosureCaseId(foreclosureCaseSet);
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
                int i =0;
                DataValidationException dataValidationException = new DataValidationException();
                foreach (ValidationResult result in validationResults)
                {
                    dataValidationException.ExceptionMessages.AddExceptionMessage(++i, result.Message);
                }
                throw dataValidationException;

            }
            
            return ForeclosureCaseDAO.CreateInstance().SearchForeclosureCase(searchCriteria, pageSize);
        }
        #endregion

        #region functions to serve SaveForeclosureCaseSet

        void ProcessUpdateForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            if (foreclosureCaseSet == null || foreclosureCaseSet.ForeclosureCase == null)
                throw new ProcessingException(ErrorMessages.PROCESSING_EXCEPTION_NULL_FORECLOSURE_CASE_SET);

            if (MiscErrorException(foreclosureCaseSet))
                throw new MiscProcessingException(ErrorMessages.MISC_PROCESSING_EXCEPTION);
            
            UpdateForeclosureCaseSet(foreclosureCaseSet);

        }

        void ProcessInsertForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            if (foreclosureCaseSet == null || foreclosureCaseSet.ForeclosureCase == null)
                throw new ProcessingException(ErrorMessages.PROCESSING_EXCEPTION_NULL_FORECLOSURE_CASE_SET);
            DuplicatedCaseLoanDTOCollection collection = CheckDuplicateCase(foreclosureCaseSet.ForeclosureCase);
            if (collection != null)
            {
                ThrowDuplicateCaseException(collection);
            }
                
            
            if (MiscErrorException(foreclosureCaseSet))
                throw new MiscProcessingException(ErrorMessages.MISC_PROCESSING_EXCEPTION);
            
            InsertForeclosureCaseSet(foreclosureCaseSet);

            
        }

        void ProcessInsertUpdateWithoutForeclosureCaseId(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            
            if (foreclosureCaseSet == null || foreclosureCaseSet.ForeclosureCase == null)
                throw new ProcessingException(ErrorMessages.PROCESSING_EXCEPTION_NULL_FORECLOSURE_CASE_SET);

            ForeclosureCaseDTO fcCase = foreclosureCaseSet.ForeclosureCase;

            if (fcCase.AgencyCaseNum == null || fcCase.AgencyId == 0)
                throw new ProcessingException(ErrorMessages.PROCESSING_EXCEPTION_INVALID_AGENCY_CASE_NUM_OR_AGENCY_ID);

            if (CheckExistingAgencyIdAndCaseNumber(fcCase.AgencyId, fcCase.AgencyCaseNum))
                throw new ProcessingException(ErrorMessages.PROCESSING_EXCEPTION_EXISTING_AGENCY_CASE_NUM_AND_AGENCY_ID);
            
            ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
        }

        void ProcessInsertUpdateWithForeclosureCaseId(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            if (foreclosureCaseSet == null || foreclosureCaseSet.ForeclosureCase == null)
                throw new ProcessingException(ErrorMessages.PROCESSING_EXCEPTION_NULL_FORECLOSURE_CASE_SET);

            ForeclosureCaseDTO fc = foreclosureCaseSet.ForeclosureCase;

            if (fc.FcId <= 0) //CheckInvalidFCID
                throw new ProcessingException(ErrorMessages.PROCESSING_EXCEPTION_INVALID_FC_ID);

            if (!CheckValidFCIdForAgency(fc.FcId, fc.AgencyId))
            {
                ThrowInvalidFCIdForAgencyException(fc.FcId);
            }

            if (CheckInactiveCase(foreclosureCaseSet))                
                ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
            else
                ProcessUpdateForeclosureCaseSet(foreclosureCaseSet);


            
        }

        #region Functions check min request validate
        /// <summary>
        /// Min request validate the fore closure case set
        /// 1: Min request validate of Fore Closure Case
        /// 2: Min request validate of Budget Item Collection
        /// 3: Min request validate of Outcome Item Collection
        /// 4: Min request validate of Case Loan Collection
        /// 5: Min request validate of Activity Log
        /// </summary>
        Collection<string> RequireFieldsValidation(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            ForeclosureCaseDTO foreclosureCase = foreclosureCaseSet.ForeclosureCase;
            BudgetItemDTOCollection budgetItem = foreclosureCaseSet.BudgetItems;
            OutcomeItemDTOCollection outcomeItem = foreclosureCaseSet.Outcome;
            CaseLoanDTOCollection caseLoanItem = foreclosureCaseSet.CaseLoans;
            ActivityLogDTOCollection activityLogItem = foreclosureCaseSet.ActivityLog;
            Collection<string> msgFcCaseSet = new Collection<string>();
            RequireFieldsForeclosureCase(foreclosureCase, ref msgFcCaseSet, "Min Request Validate");
            RequireFieldsBudgetItem(budgetItem, ref msgFcCaseSet, "Min Request Validate");
            RequireFieldsOutcomeItem(outcomeItem, ref msgFcCaseSet, "Min Request Validate");
            RequireFieldsCaseLoanItem(caseLoanItem, ref msgFcCaseSet, "Min Request Validate");
            RequireFieldsActivityLogItem(activityLogItem, ref msgFcCaseSet, "Min Request Validate");
            if (msgFcCaseSet.Count == 0)
                return null;
            return msgFcCaseSet;                     
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 1: Min request validate of Fore Closure Case
        /// <return>Collection Message Error</return>
        /// </summary>
        void RequireFieldsForeclosureCase(ForeclosureCaseDTO foreclosureCase,ref Collection<string> fcCaseSetMsg, string ruleSet)
        {            
            ValidationResults validationResults = HPFValidator.Validate<ForeclosureCaseDTO>(foreclosureCase, ruleSet);
            foreach (ValidationResult result in validationResults)
            {
                fcCaseSetMsg.Add(result.Key + " is required");
            }            
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 2:  Min request validate of Budget Item Collection
        /// <return>Collection Message Error</return>
        /// </summary>
        void RequireFieldsBudgetItem(BudgetItemDTOCollection budgetItemDTOCollection, ref Collection<string> fcCaseSetMsg, string ruleSet)
        {
            if (budgetItemDTOCollection == null)            
                return ;            
            for (int i = 0; i < budgetItemDTOCollection.Count; i++)
            {
                BudgetItemDTO item = budgetItemDTOCollection[i];
                ValidationResults validationResults = HPFValidator.Validate<BudgetItemDTO>(item, ruleSet);
                if (!validationResults.IsValid)
                {
                    foreach (ValidationResult result in validationResults)
                    {
                        fcCaseSetMsg.Add(result.Key + " " + (i + 1) + " is required");
                    }
                }
            }            
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 3:  Min request validate of Outcome Item Collection
        /// <return>Collection Message Error</return>
        /// </summary>
        void RequireFieldsOutcomeItem(OutcomeItemDTOCollection outcomeItemDTOCollection,ref Collection<string> fcCaseSetMsg, string ruleSet)
        {
            if (outcomeItemDTOCollection == null)
                return;            
            for (int i = 0; i < outcomeItemDTOCollection.Count; i++)
            {
                OutcomeItemDTO item = outcomeItemDTOCollection[i];
                ValidationResults validationResults = HPFValidator.Validate<OutcomeItemDTO>(item, ruleSet);
                if (!validationResults.IsValid)
                {
                    foreach (ValidationResult result in validationResults)
                    {
                        fcCaseSetMsg.Add(result.Key + " " + (i + 1) + " is required");
                    }
                }
            }            
        }

        /// <summary>
        /// Min request validate the fore closure case set
        /// 4:  Min request validate of Case Loan Collection
        /// <return>Collection Message Error</return>
        /// </summary>       
        void RequireFieldsCaseLoanItem(CaseLoanDTOCollection caseLoanDTOCollection,ref Collection<string> fcCaseSetMsg, string ruleSet)
        {
            if (caseLoanDTOCollection == null)
                return;
            Collection<string> caseLoanMessage = new Collection<string>();
            for (int i = 0; i < caseLoanDTOCollection.Count; i++)
            {
                CaseLoanDTO item = caseLoanDTOCollection[i];
                ValidationResults validationResults = HPFValidator.Validate<CaseLoanDTO>(item, ruleSet);
                if (!validationResults.IsValid)
                {
                    foreach (ValidationResult result in validationResults)
                    {
                        fcCaseSetMsg.Add(result.Key + " " + (i + 1) + " is required");
                    }
                }
            }         
        }
        /// <summary>
        /// Min request validate the fore closure case set
        /// 5:  Min request validate of Activity Log Collection
        /// <return>Collection Message Error</return>
        /// </summary>
        void RequireFieldsActivityLogItem(ActivityLogDTOCollection activityLogDTOCollection,ref Collection<string> fcCaseSetMsg, string ruleSet)
        {
            if (activityLogDTOCollection == null)
                return;            
            for (int i = 0; i < activityLogDTOCollection.Count; i++)
            {
                ActivityLogDTO item = activityLogDTOCollection[i];
                ValidationResults validationResults = HPFValidator.Validate<ActivityLogDTO>(item, ruleSet);
                if (!validationResults.IsValid)
                {
                    foreach (ValidationResult result in validationResults)
                    {
                        fcCaseSetMsg.Add(result.Key + " " + (i + 1) + " is required");
                    }
                }
            }            
        }
        #endregion

        /// <summary>
        /// Check if ForeclosureCase with fcId and agencyId exists or not
        /// </summary>
        bool CheckValidFCIdForAgency(int fcId, int agencyId)
        {
            ForeclosureCaseDTO fcCase = GetForeclosureCase(fcId);

            return (fcCase != null && agencyId == fcCase.AgencyId);
        }

        /// <summary>
        /// Check Inactive(Over one year)
        /// <input>datetime</input>
        /// false: Active
        /// true: Inactive
        /// <return>bool<return>
        /// </summary>
        private bool CheckInactiveCase(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            DateTime currentDate = DateTime.Now;
            DateTime backOneYear = DateTime.MinValue;
            int fcId = foreclosureCaseSet.ForeclosureCase.FcId;
            ForeclosureCaseDTO foreclosureCase = GetForeclosureCase(fcId);
            if (foreclosureCase == null)
                return false;
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
        DuplicatedCaseLoanDTOCollection CheckDuplicateCase(ForeclosureCaseDTO foreclosureCase)
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
        /// return TRUE if have Error
        /// </summary>
        bool MiscErrorException(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            bool case1 = CheckUnComplete(foreclosureCaseSet);
            bool case2 = CheckFirstMortgages(foreclosureCaseSet);
            return (case1 || case2);
        }

        /// <summary>
        /// Check Misc Error Exception
        /// Case 1: Cannot Un-complete a Previously Completed Case  
        /// return TRUE if have Error
        /// </summary>        
        private bool CheckUnComplete(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {
            bool caseComplete = CheckForeclosureCaseComplete(foreclosureCaseSetInput);
            ForeclosureCaseDTO foreclosureCase = foreclosureCaseSetInput.ForeclosureCase;
            CaseLoanDTOCollection caseLoan = foreclosureCaseSetInput.CaseLoans;
            OutcomeItemDTOCollection outcome = foreclosureCaseSetInput.Outcome;
            BudgetItemDTOCollection budget = foreclosureCaseSetInput.BudgetItems;
            bool rfForeclosureCase = CompleteFieldsForeclosureCase(foreclosureCase, "Complete");
            bool rfCaseLoan = CompleteFieldsCaseLoanItem(caseLoan, "Complete");
            bool rfOutcome = CompleteFieldsOutcomeItem(outcome, "Complete");
            bool rfBudget = CompleteFieldsBudgetItem(budget, "Complete");
            if (!caseComplete || (rfForeclosureCase && rfCaseLoan && rfOutcome && rfBudget && caseComplete))
                return false;
            else
                return true;                        
        }

        /// <summary>
        /// Check Case in Database is NULL or Complete Or Not Complete
        /// Null = Not Complete => return false
        /// CompleteDt != null => complete => return true
        /// </summary>     
        private bool CheckForeclosureCaseComplete(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {
            ForeclosureCaseDTO fcCase = GetForeclosureCase(foreclosureCaseSetInput.ForeclosureCase.FcId);            
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
        private bool CheckFirstMortgages(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {
            int count = 0;
            CaseLoanDTOCollection caseLoan = foreclosureCaseSetInput.CaseLoans;
            foreach (CaseLoanDTO item in caseLoan)
            {
                if (item.Loan1st2nd.ToUpper() == LOAN_1ST_2ND)
                {
                    count = count + 1;
                }
            }
            return (count > 1);                
        }

        /// <summary>
        /// Complete validate the fore closure case        
        /// <return>bool</return>
        /// </summary>
        bool CompleteFieldsForeclosureCase(ForeclosureCaseDTO foreclosureCase, string ruleSet)
        {
            ValidationResults validationResults = HPFValidator.Validate<ForeclosureCaseDTO>(foreclosureCase, ruleSet);
            return (validationResults.IsValid);
        }

        /// <summary>
        /// Complete validate the Budget Item
        /// <return>bool</return>
        /// </summary>
        bool CompleteFieldsBudgetItem(BudgetItemDTOCollection budgetItemDTOCollection, string ruleSet)
        {
            if (budgetItemDTOCollection == null)
                return false;
            foreach (BudgetItemDTO item in budgetItemDTOCollection)
            {
                ValidationResults validationResults = HPFValidator.Validate<BudgetItemDTO>(item, ruleSet);
                if (!validationResults.IsValid)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Complete validate the Out Come      
        /// <return>bool</return>
        /// </summary>
        bool CompleteFieldsOutcomeItem(OutcomeItemDTOCollection outcomeItemDTOCollection, string ruleSet)
        {
            if (outcomeItemDTOCollection == null)
                return false;
            foreach (OutcomeItemDTO item in outcomeItemDTOCollection)
            {
                ValidationResults validationResults = HPFValidator.Validate<OutcomeItemDTO>(item, ruleSet);
                if (!validationResults.IsValid)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Complete validate the Case Loan
        /// <return>bool</return>
        /// </summary>
        bool CompleteFieldsCaseLoanItem(CaseLoanDTOCollection caseLoanDTOCollection, string ruleSet)
        {
            if (caseLoanDTOCollection == null)
                return false;
            foreach (CaseLoanDTO item in caseLoanDTOCollection)
            {
                ValidationResults validationResults = HPFValidator.Validate<CaseLoanDTO>(item, ruleSet);
                if (!validationResults.IsValid)
                    return false;
            }
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
                ForeclosureCaseDTO foreclosureCase = ForclosureCaseHPAuto(foreclosureCaseSet);
                CaseLoanDTOCollection caseLoanCollection = foreclosureCaseSet.CaseLoans;
                OutcomeItemDTOCollection outcomeItemCollection = foreclosureCaseSet.Outcome;
                BudgetSetDTO budgetSet = BudgetSetHPAuto(foreClosureCaseSetDAO, foreclosureCaseSet);
                BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSet.BudgetItems;
                BudgetAssetDTOCollection budgetAssetCollection = foreclosureCaseSet.BudgetAssets;
                ActivityLogDTOCollection activityLogCollection = foreclosureCaseSet.ActivityLog;
                //Insert table Foreclosure_Case
                //Return Fc_id
                int fcId = UpdateForeClosureCase(foreClosureCaseSetDAO, foreclosureCase);

                //check changed from budgetItem and budget asset
                //if they are changed, insert new budget set, budget Item and budget asset
                InsertBudget(foreClosureCaseSetDAO, budgetSet, budgetItemCollection, budgetAssetCollection, fcId);

                //check outcome item Input with outcome item DB
                //if not exist, insert new
                OutcomeItemDTOCollection outcomeCollecionNew = null;
                outcomeCollecionNew = CheckOutcomeItemInputwithDB(foreClosureCaseSetDAO, outcomeItemCollection, fcId);
                InsertOutcomeItem(foreClosureCaseSetDAO, outcomeItemCollection, fcId);

                //check outcome item DB with outcome item input
                //if not exit, update outcome_deleted_dt = today()
                outcomeCollecionNew = CheckOutcomeItemDBwithInput(foreClosureCaseSetDAO, outcomeItemCollection, fcId);                
                UpdateOutcome(foreClosureCaseSetDAO, outcomeCollecionNew);   
             
                //Check for Delete Case Loan
                CaseLoanDTOCollection caseLoanCollecionNew = CheckCaseLoanForDelete(foreClosureCaseSetDAO, caseLoanCollection, fcId);
                DeleteCaseLoan(foreClosureCaseSetDAO, caseLoanCollecionNew);  
              
                //Check for Update Case Loan
                caseLoanCollecionNew = CheckCaseLoanForUpdate(foreClosureCaseSetDAO, caseLoanCollection, fcId);                
                UpdateCaseLoan(foreClosureCaseSetDAO, caseLoanCollecionNew);  
              
                //Check for Insert Case Loan
                caseLoanCollecionNew = CheckCaseLoanForInsert(foreClosureCaseSetDAO, caseLoanCollection, fcId);                
                InsertCaseLoan(foreClosureCaseSetDAO, caseLoanCollection, fcId);

                //Insert table ActivityLog
                InsertActivityLog(foreClosureCaseSetDAO, activityLogCollection, fcId);

                foreClosureCaseSetDAO.Commit();
            }
            catch (Exception)
            {
                foreClosureCaseSetDAO.Cancel();
                throw;
            }
        }
        
        /// <summary>
        /// Update foreclosureCase
        /// </summary>
        private static int UpdateForeClosureCase(ForeclosureCaseSetDAO foreClosureCaseSetDAO, ForeclosureCaseDTO foreclosureCase)
        {
            int fcId = foreClosureCaseSetDAO.UpdateForeclosureCase(foreclosureCase);
            return fcId;
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
                ForeclosureCaseDTO foreclosureCase = ForclosureCaseHPAuto(foreclosureCaseSet);
                CaseLoanDTOCollection caseLoanCollection = foreclosureCaseSet.CaseLoans;
                OutcomeItemDTOCollection outcomeItemCollection = OutcomeHPAuto(foreclosureCaseSet);
                BudgetSetDTO budgetSet = BudgetSetHPAuto(foreClosureCaseSetDAO, foreclosureCaseSet);
                BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSet.BudgetItems;
                BudgetAssetDTOCollection budgetAssetCollection = foreclosureCaseSet.BudgetAssets;
                ActivityLogDTOCollection activityLogCollection = foreclosureCaseSet.ActivityLog;
                //Insert table Foreclosure_Case
                //Return Fc_id
                int fcId = InsertForeclosureCase(foreClosureCaseSetDAO, foreclosureCase);

                //Insert table Case Loan
                InsertCaseLoan(foreClosureCaseSetDAO, caseLoanCollection, fcId);

                //Insert Table Outcome Item
                InsertOutcomeItem(foreClosureCaseSetDAO, outcomeItemCollection, fcId);

                //Insert Table Budget Set
                //Return Budget Set Id
                int budgetSetId = InsertBudgetSet(foreClosureCaseSetDAO, budgetSet, fcId);

                //Insert Table Budget Item
                InsertbudgetItem(foreClosureCaseSetDAO, budgetItemCollection, budgetSetId);

                //Insert table Budget Asset
                InsertBudgetAsset(foreClosureCaseSetDAO, budgetAssetCollection, budgetSetId);

                //Insert table ActivityLog
                InsertActivityLog(foreClosureCaseSetDAO, activityLogCollection, fcId);
                foreClosureCaseSetDAO.Commit();
            }
            catch (Exception)
            {                
                foreClosureCaseSetDAO.Cancel();                
                throw;
            }
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
                InsertbudgetItem(foreClosureCaseSetDAO, budgetItemCollection, budget_set_id);
                //Insert table Budget Asset
                InsertBudgetAsset(foreClosureCaseSetDAO, budgetAssetCollection, budget_set_id);
            }
        }
        /// <summary>
        /// Insert Budget Asset
        /// </summary>
        private static void InsertBudgetAsset(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetAssetDTOCollection budgetAssetCollection, int budget_set_id)
        {
            if (budgetAssetCollection != null)
            {
                foreach (BudgetAssetDTO items in budgetAssetCollection)
                {
                    foreClosureCaseSetDAO.InsertBudgetAsset(items, budget_set_id);
                }
            }
        }

        /// <summary>
        /// Insert Budget Item
        /// </summary>
        private static void InsertbudgetItem(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetItemDTOCollection budgetItemCollection, int budget_set_id)
        {
            if (budgetItemCollection != null)
            {
                foreach (BudgetItemDTO items in budgetItemCollection)
                {
                    foreClosureCaseSetDAO.InsertBudgetItem(items, budget_set_id);
                }
            }
        }

        /// <summary>
        /// Insert Budget Set
        /// </summary>
        private static int InsertBudgetSet(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetSetDTO budgetSet, int fcId)
        {
            int budget_set_id = int.MinValue;
            if(budgetSet != null)
            {
                budget_set_id = foreClosureCaseSetDAO.InsertBudgetSet(budgetSet, fcId);
            }
            return budget_set_id;
        }

        /// <summary>
        /// Insert ForeclosureCase
        /// </summary>
        private static int InsertForeclosureCase(ForeclosureCaseSetDAO foreClosureCaseSetDAO, ForeclosureCaseDTO foreclosureCase)
        {
            int fcId = int.MinValue;
            if(foreclosureCase != null)
            {
                fcId  = foreClosureCaseSetDAO.InsertForeclosureCase(foreclosureCase);
            }
            return fcId;
        }

        /// <summary>
        /// Insert OutcomeItem
        /// </summary>
        private static void InsertOutcomeItem(ForeclosureCaseSetDAO foreClosureCaseSetDAO, OutcomeItemDTOCollection outcomeItemCollection, int fcId)
        {
            if (outcomeItemCollection != null)
            {
                foreach (OutcomeItemDTO items in outcomeItemCollection)
                {
                    foreClosureCaseSetDAO.InsertOutcomeItem(items, fcId);
                }
            }
        }

        /// <summary>
        /// Insert CaseLoan
        /// </summary>
        private static void InsertCaseLoan(ForeclosureCaseSetDAO foreClosureCaseSetDAO, CaseLoanDTOCollection caseLoanCollection, int fcId)
        {
            if (caseLoanCollection != null)
            {
                foreach (CaseLoanDTO items in caseLoanCollection)
                {
                    foreClosureCaseSetDAO.InsertCaseLoan(items, fcId);
                }
            }
        }

        /// <summary>
        /// Insert ActivityLog
        /// </summary>
        private static void InsertActivityLog(ForeclosureCaseSetDAO foreClosureCaseSetDAO, ActivityLogDTOCollection activityLogCollection, int fcId)
        {
            if (activityLogCollection != null)
            {
                foreach (ActivityLogDTO items in activityLogCollection)
                {
                    foreClosureCaseSetDAO.InsertActivityLog(items, fcId);
                }
            }
        }

        /// <summary>
        /// Update CaseLoan
        /// </summary>
        private static void UpdateCaseLoan(ForeclosureCaseSetDAO foreClosureCaseSetDAO, CaseLoanDTOCollection caseLoanCollecion)
        {
            if (caseLoanCollecion != null)
            {
                foreach (CaseLoanDTO items in caseLoanCollecion)
                {
                    foreClosureCaseSetDAO.UpdateCaseLoan(items);
                }
            }
        }

        /// <summary>
        /// Delete CaseLoan
        /// </summary>
        private static void DeleteCaseLoan(ForeclosureCaseSetDAO foreClosureCaseSetDAO, CaseLoanDTOCollection caseLoanCollecion)
        {
            if (caseLoanCollecion != null)
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
        private static void UpdateOutcome(ForeclosureCaseSetDAO foreClosureCaseSetDAO, OutcomeItemDTOCollection outcomeCollecion)
        {
            if (outcomeCollecion != null)
            {
                foreach (OutcomeItemDTO items in outcomeCollecion)
                {
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
            if (budgetCollectionDB != null && budgetCollectionDB.Count == budgetCollectionInput.Count)
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
        private bool IsBudgetAssetDifference(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetAssetDTOCollection budgetCollectionInput, int fcId)
        {
            BudgetAssetDTOCollection budgetCollectionDB = foreClosureCaseSetDAO.GetBudgetAssetSet(fcId);
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
                foreach (OutcomeItemDTO itemInput in outcomeItemCollectionInput)
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
        private OutcomeItemDTOCollection CheckOutcomeItemDBwithInput(ForeclosureCaseSetDAO foreClosureCaseSetDAO, OutcomeItemDTOCollection outcomeItemCollectionInput, int fcId)
        {
            OutcomeItemDTOCollection outcomeItemNew = new OutcomeItemDTOCollection();
            OutcomeItemDTOCollection outcomeItemCollectionDB = foreClosureCaseSetDAO.GetOutcomeItemCollection(fcId);            
            //Compare OutcomeItem input with OutcomeItem DB
            if (outcomeItemCollectionDB == null)
            {
                return null;
            }
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
                foreach (CaseLoanDTO itemInput in caseLoanCollection)
                {
                    caseLoanNew.Add(itemInput);
                }
            }
            return caseLoanNew;   
        }

        private CaseLoanDTOCollection CheckCaseLoanForDelete(ForeclosureCaseSetDAO foreClosureCaseSetDAO, CaseLoanDTOCollection caseLoanCollection, int fcId)
        {
            CaseLoanDTOCollection caseLoanNew = new CaseLoanDTOCollection();
            CaseLoanDTOCollection caseLoanCollectionDB = foreClosureCaseSetDAO.GetCaseLoanCollection(fcId);
            //Compare CAseLoanItem input with Case Loan DB
            if (caseLoanCollectionDB != null)
            {
                return null;
            }
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

        private CaseLoanDTOCollection CheckCaseLoanForUpdate(ForeclosureCaseSetDAO foreClosureCaseSetDAO,CaseLoanDTOCollection caseLoanCollection, int fcId)
        {
            CaseLoanDTOCollection caseLoanNew = new CaseLoanDTOCollection();
            CaseLoanDTOCollection caseLoanCollectionDB = foreClosureCaseSetDAO.GetCaseLoanCollection(fcId);
            if (caseLoanCollectionDB == null)
            {
                return null;
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

        #region Functions check valid code
        /// <summary>
        /// Check valid code
        /// <input>ForeclosureCaseSetDTO</input>
        /// <return>bool<return>
        /// </summary>
        private Collection<string> CheckValidCode(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            if (foreclosureCaseSet == null)
                return null;            
            ForeclosureCaseDTO foreclosureCase = foreclosureCaseSet.ForeclosureCase;
            CaseLoanDTOCollection caseLoanCollection = foreclosureCaseSet.CaseLoans;
            Collection<string> msgFcCaseSet = new Collection<string>();
            CheckValidCodeForForclosureCase(foreclosureCase, ref msgFcCaseSet);
            CheckValidCodeForCaseLoan(caseLoanCollection, ref msgFcCaseSet);
            CheckValidCombinationStateCdAndZip(foreclosureCase, ref msgFcCaseSet);
            CheckValidZipCode(foreclosureCase, ref msgFcCaseSet);
            if(msgFcCaseSet.Count == 0)
                return null;
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check valid code for forclosu Case
        /// <input>Foreclosurecase</input>
        /// <return>bool<return>
        /// </summary>
        private void CheckValidCodeForForclosureCase(ForeclosureCaseDTO forclosureCase, ref Collection<string> msgFcCaseSet)
        {
            ReferenceCodeValidatorBL referenceCode = new ReferenceCodeValidatorBL();
            if(forclosureCase == null)
                return;
            if(!referenceCode.Validate(ReferenceCode.IncomeEarnersCode, forclosureCase.IncomeEarnersCd))
                msgFcCaseSet.Add("IncomeEarnersCd is bad code data");
            if(!referenceCode.Validate(ReferenceCode.CaseResourceCode, forclosureCase.CaseSourceCd))
                msgFcCaseSet.Add("CaseResourceCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.RaceCode, forclosureCase.RaceCd))
                msgFcCaseSet.Add("RaceCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.HouseholdCode, forclosureCase.HouseholdCd))
                msgFcCaseSet.Add("HouseholdCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.NeverBillReasonCode, forclosureCase.NeverBillReasonCd))
                msgFcCaseSet.Add("NeverBillReasonCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.NeverPayReasonCode, forclosureCase.NeverPayReasonCd))
                msgFcCaseSet.Add("NeverPayReasonCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.DefaultReasonCode, forclosureCase.DfltReason1stCd))
                msgFcCaseSet.Add("DfltReason1stCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.DefaultReasonCode, forclosureCase.DfltReason2ndCd))
                msgFcCaseSet.Add("DfltReason2ndCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.HUDTerminationReasonCode, forclosureCase.HudTerminationReasonCd))
                msgFcCaseSet.Add("HUDTerminationReasonCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.HUDOutcomeCode, forclosureCase.HudOutcomeCd))
                msgFcCaseSet.Add("HUDOutcomeCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.CounselingDurarionCode, forclosureCase.CounselingDurationCd))
                msgFcCaseSet.Add("CounselingDurarionCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.GenderCode, forclosureCase.GenderCd))
                msgFcCaseSet.Add("GenderCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.State, forclosureCase.ContactStateCd))
                msgFcCaseSet.Add("ContactStateCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.State, forclosureCase.PropStateCd))
                msgFcCaseSet.Add("PropStateCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.EducationCode, forclosureCase.BorrowerEducLevelCompletedCd))
                msgFcCaseSet.Add("BorrowerEducLevelCompletedCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.MaritalStatusCode, forclosureCase.BorrowerMaritalStatusCd))
                msgFcCaseSet.Add("BorrowerMaritalStatusCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.LanguageCode, forclosureCase.BorrowerPreferredLangCd))
                msgFcCaseSet.Add("BorrowerPreferredLangCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.OccupationCode, forclosureCase.BorrowerOccupationCd))
                msgFcCaseSet.Add("BorrowerOccupationCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.OccupationCode, forclosureCase.CoBorrowerOccupationCd))
                msgFcCaseSet.Add("CoBorrowerOccupationCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.SummarySentOtherCode, forclosureCase.SummarySentOtherCd))
                msgFcCaseSet.Add("SummarySentOtherCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.PropertyCode, forclosureCase.PropertyCd))
                msgFcCaseSet.Add("PropertyCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.MilitaryServiceCode, forclosureCase.MilitaryServiceCd))
                msgFcCaseSet.Add("MilitaryServiceCode is bad code data");
            if(!referenceCode.Validate(ReferenceCode.CreditBurreauCode, forclosureCase.IntakeCreditBureauCd))
                msgFcCaseSet.Add("IntakeCreditBureauCode is bad code data");
        }

        /// <summary>
        /// Check valid code for case loan
        /// <input>CaseLoanDTOCollection</input>
        /// <return>bool<return>
        /// </summary>
        private void CheckValidCodeForCaseLoan(CaseLoanDTOCollection caseLoanCollection, ref Collection<string> msgFcCaseSet)
        {
            ReferenceCodeValidatorBL referenceCode = new ReferenceCodeValidatorBL();
            if (caseLoanCollection == null)
                return;
            for (int i = 0; i < caseLoanCollection.Count; i++)
            {
                CaseLoanDTO caseLoan = caseLoanCollection[i];
                if (!referenceCode.Validate(ReferenceCode.MortgageTypeCode, caseLoan.MortgageTypeCd))
                    msgFcCaseSet.Add("MortgageTypeCode " + i + " is bad code data");
                if (!referenceCode.Validate(ReferenceCode.TermLengthCode, caseLoan.TermLengthCd))
                    msgFcCaseSet.Add("TermLengthCode " + i + " is bad code data");
                if (!referenceCode.Validate(ReferenceCode.LoanDelinquencyStatusCode, caseLoan.LoanDelinqStatusCd))
                    msgFcCaseSet.Add("LoanDelinqStatusCode " + i + "  is bad code data");
            }                
        }

        /// <summary>
        /// Check valid combination state_code and zip code
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private void CheckValidCombinationStateCdAndZip(ForeclosureCaseDTO forclosureCase, ref Collection<string> msgFcCaseSet)
        {
            GeoCodeRefDTOCollection geoCodeRefCollection = GeoCodeRefDAO.Instance.GetGeoCodeRef();
            bool contactValid = false;
            bool propertyValid = false;
            if (geoCodeRefCollection == null)
                return;            
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
                msgFcCaseSet.Add("Combination StateCode and Zipcode is invalid");
            if(propertyValid == false)
                msgFcCaseSet.Add("Combination StateCode and Zipcode is invalid");
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
        private void CheckValidZipCode(ForeclosureCaseDTO forclosureCase,ref Collection<string> msgFcCaseSet)
        {
            if (forclosureCase.ContactZip.Length != 5)
                msgFcCaseSet.Add("ContactZip is invalid");
            if (forclosureCase.PropZip.Length != 5)
                msgFcCaseSet.Add("PropZip is invalid");            
        }
        #endregion

        #region Funcrions check to input HP-Auto        
        /// <summary>
        /// Add value HPF-Auto for ForclosureCase        
        /// </summary>
        private ForeclosureCaseDTO ForclosureCaseHPAuto(ForeclosureCaseSetDTO foreclosureCaseSet)
        {            
            if (foreclosureCaseSet == null || foreclosureCaseSet.ForeclosureCase == null)
                return null;                        
            ForeclosureCaseDTO foreclosureCase = foreclosureCaseSet.ForeclosureCase;            
            int fcId = foreclosureCase.FcId;
            bool isComplete = CheckComplete(foreclosureCaseSet);
            foreclosureCase.AmiPercentage = CalculateAmiPercentage();
            foreclosureCase.SummarySentDt = DateTime.Now;
            foreclosureCase.CaseCompleteInd = CASE_COMPLETE_IND_NO;
            if (isComplete == true)
            {
                foreclosureCase.CompletedDt = SetCompleteDate(fcId);
                foreclosureCase.CaseCompleteInd = SetCaseCompleteInd(fcId);
            }            
            return foreclosureCase;
        }

        /// <summary>
        /// Check Data Input is Complete
        /// </summary>        
        private bool CheckComplete(ForeclosureCaseSetDTO foreclosureCaseSetInput)
        {            
            ForeclosureCaseDTO foreclosureCase = foreclosureCaseSetInput.ForeclosureCase;
            CaseLoanDTOCollection caseLoan = foreclosureCaseSetInput.CaseLoans;
            OutcomeItemDTOCollection outcome = foreclosureCaseSetInput.Outcome;
            BudgetItemDTOCollection budget = foreclosureCaseSetInput.BudgetItems;
            bool rfForeclosureCase = CompleteFieldsForeclosureCase(foreclosureCase, "Complete");
            bool rfCaseLoan = CompleteFieldsCaseLoanItem(caseLoan, "Complete");
            bool rfOutcome = CompleteFieldsOutcomeItem(outcome, "Complete");
            bool rfBudget = CompleteFieldsBudgetItem(budget, "Complete");
            return (rfForeclosureCase && rfCaseLoan && rfOutcome && rfBudget);                
        }

        /// <summary>
        /// Set value for Complete Date
        /// </summary>
        private DateTime SetCompleteDate(int fcId)
        {
            ForeclosureCaseDTO foreclosureCase = null;
            if (fcId != int.MinValue && fcId != 0)
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
        /// Set value for Case Complete Indicator
        /// </summary>
        private string SetCaseCompleteInd(int fcId)
        {
            ForeclosureCaseDTO foreclosureCase = null;
            if (fcId != int.MinValue && fcId != 0)
            {
                foreclosureCase = GetForeclosureCase(fcId);
                if (foreclosureCase.CaseCompleteInd == CASE_COMPLETE_IND_NO)
                    return CASE_COMPLETE_IND_YES;
                else
                    return foreclosureCase.CaseCompleteInd;
            }
            return CASE_COMPLETE_IND_YES;
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
            if (foreclosureCaseSet == null || foreclosureCaseSet.Outcome == null)            
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

        private BudgetSetDTO BudgetSetHPAuto(ForeclosureCaseSetDAO foreClosureCaseSetDAO, ForeclosureCaseSetDTO foreclosureCaseSet)
        {            
            if (foreclosureCaseSet == null)
                return null;
            BudgetSetDTO budgetSet = foreclosureCaseSet.BudgetSet;
            BudgetAssetDTOCollection budgetAssetCollection = foreclosureCaseSet.BudgetAssets;
            BudgetItemDTOCollection budgetItemCollection = foreclosureCaseSet.BudgetItems;            
            decimal totalIncome = decimal.MinValue;
            decimal totalExpenses = decimal.MinValue;
            decimal totalAssest = decimal.MinValue;

            if (budgetAssetCollection != null)
                totalAssest = CalculateTotalAssets(budgetAssetCollection, totalAssest);
            if (budgetItemCollection!= null)
                CalculateTotalExpenseAndIncome(foreClosureCaseSetDAO, budgetItemCollection, ref totalIncome, ref totalExpenses);

            budgetSet.TotalAssets = totalAssest;
            budgetSet.TotalExpenses = totalExpenses;
            budgetSet.TotalIncome = totalIncome;
            budgetSet.BudgetSetDt = DateTime.Now;            
            return budgetSet;
        }

        private decimal CalculateTotalAssets(BudgetAssetDTOCollection budgetAssetCollection, decimal totalAssest)
        {
            //Calculate totalAssest
            if (budgetAssetCollection == null)
            {
                return decimal.MinValue;
            }
            totalAssest = 0;
            foreach (BudgetAssetDTO item in budgetAssetCollection)
            {
                totalAssest += item.AssetValue;
            }            
            return totalAssest;
        }

        private void CalculateTotalExpenseAndIncome(ForeclosureCaseSetDAO foreClosureCaseSetDAO, BudgetItemDTOCollection budgetItemCollection, ref decimal totalIncome, ref decimal totalExpenses)
        {
            //Calculate totalExpenses, totalIncome
            if (budgetItemCollection == null)
            {
                totalIncome = decimal.MinValue;
                totalExpenses = decimal.MinValue;
            }
            totalIncome = 0;
            totalExpenses = 0;
            foreach (BudgetItemDTO item in budgetItemCollection)
            {
                string budgetCode = BuggetCategoryCode(foreClosureCaseSetDAO, item.BudgetSubcategoryId);
                if (budgetCode != null)
                {
                    if (budgetCode == INCOME)
                        totalIncome += (decimal)item.BudgetItemAmt;
                    else if (budgetCode == EXPENSES)
                        totalExpenses += (decimal)item.BudgetItemAmt;
                }
            }
            
        }

        private string BuggetCategoryCode(ForeclosureCaseSetDAO foreClosureCaseSetDAO, int subCategoryId)
        {
            BudgetDTOCollection budgetCollection = foreClosureCaseSetDAO.GetBudget();
            if (budgetCollection == null)
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

        #region Throw Detail Exception
        private void ThrowMissingRequiredFieldsException(Collection<string> collection)
        {
            ProcessingException pe = new ProcessingException(ErrorMessages.PROCESSING_EXCEPTION_MISSING_REQUIRED_FIELD);            
            foreach (string obj in collection)
            {
                ExceptionMessage em = new ExceptionMessage();
                em.Message = obj;
                pe.ExceptionMessages.Add(em);
            }
            throw pe;
        }

        private void ThrowInvalidCodeException(Collection<string> collection)
        {
            ProcessingException pe = new ProcessingException(ErrorMessages.PROCESSING_EXCEPTION_INVALID_CODE);
            foreach (string obj in collection)
            {
                ExceptionMessage em = new ExceptionMessage();
                em.Message = obj;
                pe.ExceptionMessages.Add(em);
            }
            throw pe;
        }

        private void ThrowInvalidFCIdForAgencyException(int fcId)
        {
            ProcessingException pe = new ProcessingException(ErrorMessages.PROCESSING_EXCEPTION_INVALID_FC_ID_FOR_AGENCY_ID);
            ForeclosureCaseDTO fcCase = GetForeclosureCase(fcId);
            ExceptionMessage em = new ExceptionMessage();
            em.Message = string.Format("The case belongs to Agency: {0}, Counsellor: {1}, Contact number: {2}, email: {3}", GetAgencyName(fcCase.AgencyId), fcCase.CounselorFname + ", " + fcCase.CounselorLname, fcCase.CounselorPhone, fcCase.CounselorEmail);
            pe.ExceptionMessages.Add(em);

            throw pe;
        }

        private void ThrowDuplicateCaseException(DuplicatedCaseLoanDTOCollection collection)
        {
            ProcessingException pe = new ProcessingException(ErrorMessages.PROCESSING_EXCEPTION_DUPLICATE_FORECLOSURE_CASE);
            foreach(DuplicatedCaseLoanDTO obj in collection)
            {
                ExceptionMessage em = new ExceptionMessage();
                em.Message = string.Format("The duplicated Case Loan is Loan Number: {0}, Servicer Name: {1}, Borrower First Name: {2}, Borrower Last Name: {3}, Agency Name: {4}, Agency Case Number: {5}, Counselor Full Name: {6},Counselor Phone & Extension: {7}, Counselor Email: {8} "
                            , obj.LoanNumber, obj.ServicerName, obj.BorrowerFirstName, obj.BorrowerLastName
                            , obj.AgencyName, obj.AgencyCaseNumber, obj.CounselorFullName, obj.CounselorPhone, obj.CounselorEmail);
                pe.ExceptionMessages.Add(em);
            }
            throw pe;
        }
        #endregion

        #endregion

        #region AppForeclosureCaseSearch
        /// <summary>
        /// Check if the user has input any info to the search criteria?
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>true for doing search and false for exception</returns>
        private bool ValidateSearchCriteria(AppForeclosureCaseSearchCriteriaDTO searchCriteria)
        {
            ValidationResults validationResults = HPFValidator.Validate<AppForeclosureCaseSearchCriteriaDTO>(searchCriteria, "AppSearchRequireCriteria");
            if (validationResults.Count == NUMBER_OF_ERROR_APP_SEARCH_CRITERIA)
                return false;// user doesnt change anything in search criteria
            
            return true;
        }
        /// <summary>
        /// Search Foreclosure Case
        /// </summary>
        /// <param name="searchCriteria">Search criteria</param>
        /// <returns>Collection of AppForeclosureCaseSearchResult</returns>
        public AppForeclosureCaseSearchResultDTOCollection AppSearchforeClosureCase(AppForeclosureCaseSearchCriteriaDTO searchCriteria)
        {
            AppForeclosureCaseSearchResultDTOCollection result = new AppForeclosureCaseSearchResultDTOCollection();
            if (ValidateSearchCriteria(searchCriteria))
                result = ForeclosureCaseDAO.CreateInstance().AppSearchForeclosureCase(searchCriteria);
            else
            {
                throw new DataValidationException();
            }
            return result;
        }
        /// <summary>
        /// Get Program Name and Program ID to display in DDLB
        /// </summary>
        /// <returns>ProgramDTOCollection containts all Programs</returns>
        public ProgramDTOCollection GetProgram()
        {
            ProgramDTOCollection result = ForeclosureCaseDAO.CreateInstance().AppGetProgram();
            return result;
        }
        /// <summary>
        /// Get State Name and State ID to display in DDLB
        /// </summary>
        /// <returns>StateDTOCollection containts all State</returns>
        public StateDTOCollection GetState()
        {
            StateDTOCollection result = ForeclosureCaseDAO.CreateInstance().AppGetState();
            return result;
        }
        /// <summary>
        /// Get Agency Name and Agency ID to display in DDLB
        /// </summary>
        /// <returns>AgencyDTOCollection containts all Agency</returns>
        public AgencyDTOCollection GetAgency()
        {
            AgencyDTOCollection result = ForeclosureCaseDAO.CreateInstance().AppGetAgency();
            return result;
        }

        #endregion
    }
}
