using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;

using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace HPF.FutureState.BusinessLogic
{
    public class ForeclosureCaseBL : BaseBusinessLogic, IForclosureCaseBL
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
        /// Save a ForeClosureCase
        /// </summary>
        /// <param name="foreClosureCaseSet">ForeClosureCaseSetDTO</param>
        public void SaveForeClosureCaseSet(ForeclosureCaseSetDTO foreClosureCaseSet)
        {
            //Validation here     
       
            var foreClosureCaseSetDAO = ForeclosureCaseSetDAO.CreateInstance();
            //
            try
            {
                foreClosureCaseSetDAO.Begin();
                //
                //Business process here
                //
                foreClosureCaseSetDAO.Commit();
            }
            catch (Exception)
            {                
                foreClosureCaseSetDAO.Cancel();
                throw;
            }            
        }

        /// <summary>
        /// return ForeclosureCase search result 
        /// </summary>
        /// <param name="searchCriteria">search criteria</param>
        /// <returns>collection of ForeclosureCaseWSDTO and collection of exception messages if there are any</returns>
        public ForeclosureCaseSearchResult SearchForeClosureCase(ForeclosureCaseSearchCriteriaDTO searchCriteria)
        {
            ForeclosureCaseSearchResult searchResult = new ForeclosureCaseSearchResult();
            
            ExceptionMessageCollection exceptionMessages = new ExceptionMessageCollection();
            //exceptionMessages = HPFValidator.ValidateToExceptionMessage<ForeClosureCaseSearchCriteriaDTO>(searchCriteria);

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
                searchResult = ForeclosureCaseSetDAO.CreateInstance().SearchForeClosureCase(searchCriteria);
            }

            return searchResult;
        }
        #endregion

        #region functions to serve SaveForeClosureCaseSet
        /// <summary>
        /// Min request validate the fore closure case set
        /// 1: fore closure case
        /// 2: budget item collection
        /// 3: outcome item collection
        /// </summary>
        bool RequireFieldsValidation(ForeclosureCaseSetDTO  foreclosureCaseSet)
        {
            ForeclosureCaseDTO foreclosureCase = foreclosureCaseSet.ForeClosureCase;
            BudgetItemDTOCollection budgetItem = foreclosureCaseSet.BudgetItems;
            OutcomeItemDTOCollection outcomeItem = foreclosureCaseSet.Outcome;
            bool rfForeclosureCase = RequireFieldsForeClosureCase(foreclosureCase);
            bool rfbudgetItem = RequireFieldsBudgetItem(budgetItem);
            bool rfoutcomeItem = RequireFieldsOutcomeItem(outcomeItem);
            if (rfForeclosureCase && rfbudgetItem && rfoutcomeItem)
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
        /// 1: fore closure case
        /// <return>bool</return>
        /// </summary>
        bool RequireFieldsForeClosureCase(ForeclosureCaseDTO foreclosureCase)
        {
            Validator<ForeclosureCaseDTO> cValidator = ValidationFactory.CreateValidator<ForeclosureCaseDTO>("Default");
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
        /// 2: Budget Item Collection
        /// <return>bool</return>
        /// </summary>
        bool RequireFieldsBudgetItem(BudgetItemDTOCollection budgetItemDTOCollection)
        {
            if (budgetItemDTOCollection != null && budgetItemDTOCollection.Count > 0)
            {
                foreach (BudgetItemDTO item in budgetItemDTOCollection)
                {
                    Validator<BudgetItemDTO> cValidator = ValidationFactory.CreateValidator<BudgetItemDTO>("Default");
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
        /// 3: Outcome Item Collection
        /// <return>bool</return>
        /// </summary>
        bool RequireFieldsOutcomeItem(OutcomeItemDTOCollection outcomeItemDTOCollection)
        {
            if (outcomeItemDTOCollection != null && outcomeItemDTOCollection.Count > 0)
            {
                foreach (OutcomeItemDTO item in outcomeItemDTOCollection)
                {
                    Validator<OutcomeItemDTO> cValidator = ValidationFactory.CreateValidator<OutcomeItemDTO>("Default");
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
        /// Check ForeClosureCase Id is existed or not
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

        /// <summary>
        /// Check valid code
        /// </summary>
        private bool CheckValidCode(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            if (foreclosureCaseSet != null && foreclosureCaseSet.ForeClosureCase != null && foreclosureCaseSet.CaseLoans != null
                // Check valid code for ForeClosureCase
                && CheckCode(foreclosureCaseSet.ForeClosureCase.IncomeEarnersCd, CodeConstants.INCOME_EARNERS_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.CaseSourceCd, CodeConstants.CASE_RESOURCE_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.RaceCd, CodeConstants.RACE_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.HouseholdCd, CodeConstants.HOUSEHOLD_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.NeverBillReasonCd, CodeConstants.NEVER_BILL_REASON_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.NeverPayReasonCd, CodeConstants.NEVER_PAY_REASON_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.DfltReason1stCd, CodeConstants.DEFAULT_REASON_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.DfltReason2ndCd, CodeConstants.DEFAULT_REASON_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.HudTerminationReasonCd, CodeConstants.HUD_TERMINATION_REASON_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.HudOutcomeCd, CodeConstants.HUD_OUTCOME_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.CounselingDurationCd, CodeConstants.COUNSELING_DURARION_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.GenderCd, CodeConstants.GENDER_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.ContactStateCd, CodeConstants.CONTACT_STATE_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.CaseSourceCd, CodeConstants.PROP_STATE_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.BorrowerEducLevelCompletedCd, CodeConstants.BORROWER_EDUC_LEVEL_COMPLETED_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.BorrowerMaritalStatusCd, CodeConstants.BORROWER_MARITAL_STATUS_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.BorrowerPreferredLangCd, CodeConstants.BORROWER_PREFERRED_LANG_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.BorrowerOccupationCd, CodeConstants.BORROWER_OCCUPATION_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.CoBorrowerOccupationCd, CodeConstants.CO_BORROWER_OCCUPATION_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.SummarySentOtherCd, CodeConstants.SUMMARY_SENT_OTHER_CD)
                && CheckCode(foreclosureCaseSet.ForeClosureCase.MilitaryServiceCd, CodeConstants.MILITARY_SERVICE_CD)
                // Check valid code for Case Loan
                && CheckCodeCaseLoan(foreclosureCaseSet.CaseLoans)
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
        private bool CheckCodeCaseLoan(CaseLoanDTOCollection caseLoanCollection)
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
            RefCodeItemDTOCollection refCodeItemCollection = new RefCodeItemDTOCollection();
            refCodeItemCollection = RefCodeItem.Instance.GetRefCodeItem();
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

        bool CheckMiscErrorException(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update the Fore Closure Case
        /// </summary>
        void UpdateForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update the Fore Closure Case
        /// </summary>
        void InsertForeclosureCaseSet(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Foreclosure case basing on its fc_id
        /// </summary>
        /// <param name="fc_id">id for a ForeclosureCase</param>
        /// <returns>object of ForeclosureCase </returns>
        ForeclosureCaseDTO GetForeclosureCase(int fc_id)
        {

            return ForeclosureCaseSetDAO.CreateInstance().GetForeclosureCase(fc_id);

        }

        #endregion
    }
}
