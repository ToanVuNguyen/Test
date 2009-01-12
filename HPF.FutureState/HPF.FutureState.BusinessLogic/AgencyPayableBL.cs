﻿using System;
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
    public class AgencyPayableBL: BaseBusinessLogic
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

        public bool InsertAgencyPayable(AgencyPayableDraftDTO agencyPayableDraft)
        {
            if (agencyPayableDraft == null)
                return true;
            AgencyPayableDAO agencyPayableDAO = AgencyPayableDAO.CreateInstance();            
            try
            {                
                agencyPayableDAO.Begin();
                AgencyPayableDTO agencyPayable = new AgencyPayableDTO();
                //-----------
                agencyPayable.AgencyId = agencyPayableDraft.AgencyId;
                agencyPayable.PeriodStartDate = agencyPayableDraft.PeriodStartDate;
                agencyPayable.PeriodEndDate = agencyPayableDraft.PeriodEndDate;
                // don't know much about that fields
                agencyPayable.PayamentCode = "";
                agencyPayable.AccountLinkTBD = "";
                agencyPayable.AgencyPayablePaymentAmount = 0;
                //----------- agency_payable (base)
                agencyPayable.ChangeLastAppName = "";
                agencyPayable.ChangeLastDate = DateTime.Now;
                agencyPayable.ChangeLastUserId = "";
                agencyPayable.CreateAppName = "";
                agencyPayable.CreateDate = DateTime.Now;
                agencyPayable.CreateUserId = "";
                //-----------
                //Insert Agency Payable
                int agencyPayableId = 0;
                agencyPayableId = agencyPayableDAO.InsertAgencyPayable(agencyPayable);
                //Insert Acency Payable Case
                ForeclosureCaseDraftDTOCollection fCaseDrafColection = agencyPayableDraft.ForclosureCaseDrafts;
                foreach (ForeclosureCaseDraftDTO fCaseDraf in fCaseDrafColection)
                {
                    AgencyPayableCaseDTO agencyPayableCase = new AgencyPayableCaseDTO();
                    agencyPayableCase.ForeclosureCaseId = fCaseDraf.ForeclosureCaseId;
                    agencyPayableCase.AgencyPayableId = agencyPayableId;
                    agencyPayableCase.PaymentDate = DateTime.Now;
                    agencyPayableCase.PaymentAmount = fCaseDraf.Amount;
                    agencyPayableCase.NFMCDiffererencePaidIndicator = "";
                    //-------------
                    agencyPayableCase.AgencyName = "";
                    agencyPayableCase.PaymentDate = DateTime.Now;
                    agencyPayableCase.StatusCode = "";
                    agencyPayableCase.PaymentComment = "";
                    //----------- agency_payable (base)
                    agencyPayableCase.ChangeLastAppName = "";
                    agencyPayableCase.ChangeLastDate = DateTime.Now;
                    agencyPayableCase.ChangeLastUserId = "";
                    agencyPayableCase.CreateAppName = "";
                    agencyPayableCase.CreateDate = DateTime.Now;
                    agencyPayableCase.CreateUserId = "";
                    agencyPayableDAO.InsertAgencyPayableCase(agencyPayableCase);
      
                }
                agencyPayableDAO.Commit();
            }
            catch (Exception)
            {
                agencyPayableDAO.Cancel();
                throw;
            }
            return true;
        }
        ///<summary>
        ///
        /// </summary>
        protected void NewPayableThrowMissingRequiredFieldsException(Collection<string> collection)
        {
            DataValidationException exception = new DataValidationException();
            foreach (string item in collection)
            {
                ExceptionMessage ex = new ExceptionMessage();
                ex.Message=item;
                exception.ExceptionMessages.Add(ex);
            }
            throw exception;
        }

        /// <summary>
        /// Search and get put the list for AgencyPayable
        /// </summary>
        /// <param name="agencyPayableCriteria"></param>
        /// <returns></returns>
        public AgencyPayableDTOCollection SearchAgencyPayable(AgencyPayableSearchCriteriaDTO agencyPayableCriteria)
        {

            AgencyPayableDTOCollection result = new AgencyPayableDTOCollection();
            Collection<string> ErrorMess = NewPayabeCriteriaRequireFieldValidation(agencyPayableCriteria);
            if (ErrorMess != null)
            {
                NewPayableThrowMissingRequiredFieldsException(ErrorMess);
            }
            result = AgencyPayableDAO.CreateInstance().SearchAgencyPayable(agencyPayableCriteria);
            return result;
        }

        /// <summary>
        /// Create a draft AgencyPayable with criteria provided
        /// </summary>
        /// <param name="agencyPayableCriteria"></param>
        /// <returns></returns>

       
        public AgencyPayableDraftDTOCollection CreateDraftAgencyPayable(AgencyPayableSearchCriteriaDTO agencyPayableCriteria)
        {
            AgencyPayableDraftDTOCollection result = new AgencyPayableDraftDTOCollection();
            Collection<string> ErrorMess = NewPayabeCriteriaRequireFieldValidation(agencyPayableCriteria);
            if (ErrorMess != null)
            {
                NewPayableThrowMissingRequiredFieldsException(ErrorMess);
            }
            result = AgencyPayableDAO.CreateInstance().CreateDraftAgencyPayable(agencyPayableCriteria);
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


    }
}
