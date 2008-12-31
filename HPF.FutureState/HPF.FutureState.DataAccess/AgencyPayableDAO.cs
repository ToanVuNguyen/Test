using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.DataAccess
{
    public class AgencyPayable: BaseDAO
    {
        # region Private variables
        private SqlConnection dbConnection;
        /// <summary>
        /// Share transaction for AgencyPayableCase and AgencyPayable
        /// </summary>
        private SqlTransaction trans;
        #endregion

        protected AgencyPayable()
        { 
        }

        #region Share functions
        public static AgencyPayable CreateInstance()
        {
            return new AgencyPayable();
        }

        /// <summary>
        /// Begin working
        /// </summary>
        public void Begin()
        {
            dbConnection = CreateConnection();
            dbConnection.Open();
            trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Commit work.
        /// </summary>
        public void Commit()
        {
            trans.Commit();
            dbConnection.Close();
        }
        /// <summary>
        /// Cancel work
        /// </summary>
        public void Cancel()
        {
            trans.Rollback();
            dbConnection.Close();
        }
        #endregion

        # region Insert
                
        /// <summary>
        /// Insert AgencyPayableCase with agencyPayableCase provided
        /// </summary>
        /// <param name="agencyPayableCase"></param>
        /// <returns></returns>
        public bool InsertAgencyPayableCase(AgencyPayableCaseDTO agencyPayableCase)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert AgencyPayable with agencyPayable provided
        /// </summary>
        /// <param name="agencyPayable"></param>
        /// <returns></returns>
        public bool InserttAgencyPayable(AgencyPayableDTO agencyPayable)
        {
            throw new NotImplementedException();
        }
        #endregion

        # region Update

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
        /// Update AgencyPayable with agencyPayable provided
        /// </summary>
        /// <param name="agencyPayable"></param>
        /// <returns></returns>
        public bool UpdateAgencyPayable(AgencyPayableDTO agencyPayable)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Searching and Create Draft

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
        public AgencyPayableDraftDTOCollection CreateDraftAgencyPayable(AgencyPayableSearchCriteriaDTO agencyPayableCriteria)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
