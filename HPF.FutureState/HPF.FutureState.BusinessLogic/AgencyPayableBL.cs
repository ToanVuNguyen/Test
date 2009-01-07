using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;

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
                agencyPayable.AgencyId = agencyPayableDraft.AgencyId;
                agencyPayable.PaymentDate = DateTime.Now;
                agencyPayable.PayamentCode = "";
                agencyPayable.StatusCode = "";
                agencyPayable.PeriodStartDate = agencyPayableDraft.PeriodStartDate;
                agencyPayable.PeriodEndDate = agencyPayableDraft.PeriodEndDate;
                agencyPayable.PaymentComment = "";
                agencyPayable.AccountLinkTBD = "";
                agencyPayable.AgencyPayablePaymentAmount = 0;
                //Insert Agency Payable
                int agencyPayableId = 0;
                agencyPayableId = InsertAgencyPayable(agencyPayableDAO, agencyPayable);
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
                    InserAgencyPayableCase(agencyPayableDAO, agencyPayableCase);
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

        public int InsertAgencyPayable(AgencyPayableDAO agencyPayableDAO, AgencyPayableDTO agencyPayable)
        {
            int agencyPayableId = int.MinValue;
            if (agencyPayable != null)                
                agencyPayableId= agencyPayableDAO.InsertAgencyPayable(agencyPayable);
            return agencyPayableId;
        }

        /// <summary>
        /// Insert AgencyPayableCase with agencyPayableDraft provided
        /// </summary>
        /// <param name="agencyPayableDraft"></param>
        /// <returns></returns>
        public void InserAgencyPayableCase(AgencyPayableDAO agencyPayableDAO, AgencyPayableCaseDTO agencyPayableCase)
        {
            agencyPayableDAO.InsertAgencyPayableCase(agencyPayableCase);
        }

        public bool UpdateAgencyPayable(AgencyPayableDTO agencyPayable)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update AgencyPayableCase with agencyPayableCase provided
        /// </summary>
        /// <param name="agencyPayableCase"></param>
        /// <returns></returns>
        public bool UpdateAgencyPayableCase(AgencyPayableCaseDTO agencyPayableCase)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Search and get put the list for AgencyPayable
        /// </summary>
        /// <param name="agencyPayableCriteria"></param>
        /// <returns></returns>
        public AgencyPayableDTOCollection SearchAgencyPayable(AgencyPayableSearchCriteriaDTO agencyPayableCriteria)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a draft AgencyPayable with criteria provided
        /// </summary>
        /// <param name="agencyPayableCriteria"></param>
        /// <returns></returns>
        public AgencyPayableDraftDTO CreateDraftAgencyPayable(AgencyPayableSearchCriteriaDTO agencyPayableCriteria)
        {
            AgencyPayableDAO agencyPayableDAO = AgencyPayableDAO.CreateInstance();
            return agencyPayableDAO.CreateDraftAgencyPayable(agencyPayableCriteria);
        }
    }
}
