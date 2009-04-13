using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HPF.FutureState.Common;
using System.Collections.ObjectModel;


namespace HPF.FutureState.BusinessLogic
{
    public class AgencyPayableBL : BaseBusinessLogic
    {
        private static readonly AgencyPayableBL instance = new AgencyPayableBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static AgencyPayableBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected AgencyPayableBL()
        {

        }
        public int? InsertAgencyPayable(AgencyPayableDraftDTO agencyPayableDraft)
        {
            AgencyPayableDAO agencyPayableDAO = AgencyPayableDAO.CreateInstance();
            int? agencyPayableId = 0;
            try
            {
                agencyPayableDAO.BeginTran();
                AgencyPayableDTO agencyPayable = new AgencyPayableDTO();
                agencyPayable.SetInsertTrackingInformation(agencyPayableDraft.CreateUserId);
                agencyPayable.AgencyId = agencyPayableDraft.AgencyId;
                agencyPayable.PeriodStartDate = agencyPayableDraft.PeriodStartDate;
                agencyPayable.PeriodEndDate = agencyPayableDraft.PeriodEndDate;
                agencyPayable.AgencyPayablePaymentAmount = agencyPayableDraft.TotalAmount;
                agencyPayable.StatusCode = agencyPayableDraft.StatusCode;
                agencyPayable.PaymentDate = DateTime.Now;
                agencyPayable.PaymentComment = agencyPayableDraft.PaymentComment;

                agencyPayableId = agencyPayableDAO.InsertAgencyPayable(agencyPayable).Value;
                //Insert Acency Payable Case

                ForeclosureCaseDraftDTOCollection fCaseDrafColection = agencyPayableDraft.ForclosureCaseDrafts;
                foreach (ForeclosureCaseDraftDTO fCaseDraf in fCaseDrafColection)
                {
                    AgencyPayableCaseDTO agencyPayableCase = new AgencyPayableCaseDTO();
                    agencyPayableCase.SetInsertTrackingInformation(fCaseDraf.CreateUserId);
                    agencyPayableCase.ForeclosureCaseId = fCaseDraf.ForeclosureCaseId;
                    agencyPayableCase.AgencyPayableId = agencyPayableId;
                    agencyPayableCase.PaymentDate = DateTime.Now;
                    agencyPayableCase.PaymentAmount = fCaseDraf.Amount;
                    agencyPayableCase.AgencyName = "";
                    agencyPayableCase.PaymentDate = DateTime.Now;
                    agencyPayableCase.NFMCDifferenceEligibleInd = "N";
                    agencyPayableDAO.InsertAgencyPayableCase(agencyPayableCase);
                }

                agencyPayableDAO.CommitTran();
            }
            catch (Exception)
            {
                agencyPayableDAO.RollbackTran();
                throw;
            }
            return agencyPayableId;
        }
        ///<summary>
        ///get exception messages.
        /// </summary>
        protected void NewPayableThrowMissingRequiredFieldsException(Collection<string> collection)
        {
            DataValidationException exception = new DataValidationException();
            foreach (string item in collection)
            {
                ExceptionMessage ex = new ExceptionMessage();
                ex.Message = item;
                exception.ExceptionMessages.Add(ex);
            }
            throw exception;
        }
        ///<summary>
        ///
        /// </summary>
        public void CancelAgencyPayable(AgencyPayableDTO agencyPayable)
        {
            try
            {
                AgencyPayableDAO.CreateInstance().CancelAgencyPayable(agencyPayable);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Search and get put the list for AgencyPayable
        /// </summary>
        /// <param name="agencyPayableCriteria"></param>
        /// <returns></returns>
        public AgencyPayableDTOCollection SearchAgencyPayable(AgencyPayableSearchCriteriaDTO agencyPayableCriteria)
        {

            AgencyPayableDTOCollection result = new AgencyPayableDTOCollection();
            CheckSeachAgencyPayable(agencyPayableCriteria);
            return AgencyPayableDAO.CreateInstance().SearchAgencyPayable(agencyPayableCriteria);
        }
        public void CheckSeachAgencyPayable(AgencyPayableSearchCriteriaDTO agencyPayableCriteria)
        {
            DataValidationException dataEx = new DataValidationException();
            ValidationResults validResults = HPFValidator.Validate<AgencyPayableSearchCriteriaDTO>(agencyPayableCriteria, Constant.RULESET_AGENCY_PAYABLE_SEARCH);
            foreach(ValidationResult validResult in validResults)
            {
                string errorCode = string.IsNullOrEmpty(validResult.Tag) ? "ERROR" : validResult.Tag;
                string errorMessage = string.IsNullOrEmpty(validResult.Tag) ? validResult.Message : ErrorMessages.GetExceptionMessage(validResult.Tag);
                dataEx.ExceptionMessages.AddExceptionMessage(errorCode,errorMessage);
            }
            if (dataEx.ExceptionMessages.Count > 0)
                throw dataEx;
        }

        /// <summary>
        /// Create a draft AgencyPayable with criteria provided
        /// </summary>
        /// <param name="agencyPayableCriteria"></param>
        /// <returns></returns>


        public AgencyPayableDraftDTO CreateDraftAgencyPayable(AgencyPayableSearchCriteriaDTO agencyPayableCriteria)
        {
            AgencyPayableDraftDTO result = new AgencyPayableDraftDTO();
            Collection<string> ErrorMess = NewPayabeCriteriaRequireFieldValidation(agencyPayableCriteria);
            if (ErrorMess != null)
            {
                NewPayableThrowMissingRequiredFieldsException(ErrorMess);
            }
            AgencyPayableSearchCriteriaDTO searchCriteria = new AgencyPayableSearchCriteriaDTO();
            searchCriteria.AgencyId = agencyPayableCriteria.AgencyId;
            searchCriteria.CaseComplete = agencyPayableCriteria.CaseComplete;            
            searchCriteria.PeriodStartDate = SetToStartDay(agencyPayableCriteria.PeriodStartDate);
            searchCriteria.PeriodEndDate = SetToEndDay(agencyPayableCriteria.PeriodEndDate);
            result = AgencyPayableDAO.CreateInstance().CreateDraftAgencyPayable(searchCriteria);
            return result;
        }
        ///<summary>
        ///Check validator of Period Start :date
        ///Check validator of Period End :date
        ///Check validator of Max number of cases :number
        ///<param name="searchCriteria"></param>
        Collection<string> NewPayabeCriteriaRequireFieldValidation(AgencyPayableSearchCriteriaDTO searchCriteria)
        {

            Collection<string> msgAppForeclosureCaseSearch = new Collection<string>();
            RequireNewPayableCriteria(searchCriteria, ref msgAppForeclosureCaseSearch, "CriteriaValidation");
            if (msgAppForeclosureCaseSearch.Count == 0)
                return null;
            return msgAppForeclosureCaseSearch;
        }
        void RequireNewPayableCriteria(AgencyPayableSearchCriteriaDTO searchCriteria, ref Collection<string> msg, string ruleset)
        {
            ValidationResults validationResults = HPFValidator.Validate<AgencyPayableSearchCriteriaDTO>(searchCriteria, ruleset);
            foreach (ValidationResult result in validationResults)
            {
                msg.Add(result.Message);
            }
        }
        public AgencyPayableSetDTO AgencyPayableSetGet(int? agencyPayableID)
        {
            return (AgencyPayableDAO.CreateInstance().AgencyPayableSetGet(agencyPayableID));
        }
        public void TakebackMarkCase(AgencyPayableSetDTO agencyPayableSet, string takebackReason, string agencyPayableIDCol)
        {
            AgencyPayableDAO.CreateInstance().TakebackMarkCase(agencyPayableSet, takebackReason, agencyPayableIDCol);
        }
        public void PayUnPayMarkCase(AgencyPayableSetDTO agencyPayableSet, string agencyPayableIDCol, int flag)
        {
            AgencyPayableDAO.CreateInstance().PayUnPayMarkCase(agencyPayableSet, agencyPayableIDCol, flag);
        }

        DateTime SetToStartDay(DateTime t)
        {
            t = t.AddMonths(-6);
            t = t.AddHours(-t.Hour);
            t = t.AddMinutes(-t.Minute);
            t = t.AddSeconds(-t.Second);
            t = t.AddMilliseconds(-t.Millisecond);
            return t;
        }
        DateTime SetToEndDay(DateTime t)
        {
            t = t.AddHours(-t.Hour);
            t = t.AddMinutes(-t.Minute);
            t = t.AddSeconds(-t.Second);

            t = t.AddDays(1);
            t = t.AddMilliseconds(-1);
            return t;
        }
    }
}
