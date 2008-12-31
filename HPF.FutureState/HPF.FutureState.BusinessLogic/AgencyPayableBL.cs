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
            throw new NotImplementedException();
        }

        public bool InsertAgencyPayable(AgencyPayableDTO agencyPayable)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert AgencyPayableCase with agencyPayableDraft provided
        /// </summary>
        /// <param name="agencyPayableDraft"></param>
        /// <returns></returns>
        public bool InserAgencyPayableCase(AgencyPayableCaseDTO agencyPayableCase)
        {
            throw new NotImplementedException();            
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
        public AgencyPayableDTOCollection CreateDraftAgencyPayable(AgencyPayableSearchCriteriaDTO agencyPayableCriteria)
        {
            throw new NotImplementedException();
        }
    }
}
