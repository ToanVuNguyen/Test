using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.DataAccess
{
    public class AgencyPayableDAO: BaseDAO
    {
        # region Private variables
        private SqlConnection dbConnection;
        /// <summary>
        /// Share transaction for AgencyPayableCase and AgencyPayable
        /// </summary>
        private SqlTransaction trans;
        #endregion

        protected AgencyPayableDAO()
        { 
        }

        #region Share functions
        public static AgencyPayableDAO CreateInstance()
        {
            return new AgencyPayableDAO();
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
        public void InsertAgencyPayableCase(AgencyPayableCaseDTO agencyPayableCase)
        {
            var command = CreateCommand("hpf_agency_payable_case_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@pi_fc_id", agencyPayableCase.ForeclosureCaseId);
                sqlParam[1] = new SqlParameter("@pi_agency_payable_id", agencyPayableCase.AgencyPayableId);
                sqlParam[2] = new SqlParameter("@pi_pmt_dt", NullableDateTime(agencyPayableCase.PaymentDate));
                sqlParam[3] = new SqlParameter("@pi_pmt_amt", agencyPayableCase.PaymentAmount);
                sqlParam[4] = new SqlParameter("@pi_create_dt", NullableDateTime(agencyPayableCase.CreateDate));
                sqlParam[5] = new SqlParameter("@pi_create_user_id", agencyPayableCase.CreateUserId);
                sqlParam[6] = new SqlParameter("@pi_create_app_name", agencyPayableCase.CreateAppName);
                sqlParam[7] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(agencyPayableCase.ChangeLastDate));
                sqlParam[8] = new SqlParameter("@pi_chg_lst_user_id", agencyPayableCase.ChangeLastUserId);
                sqlParam[9] = new SqlParameter("@pi_chg_lst_app_name", agencyPayableCase.ChangeLastAppName);
                sqlParam[9] = new SqlParameter("@pi_NFMC_difference_paid_ind", agencyPayableCase.NFMCDiffererencePaidIndicator);
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }         
        }

        /// <summary>
        /// Insert AgencyPayable with agencyPayable provided
        /// </summary>
        /// <param name="agencyPayable"></param>
        /// <returns></returns>
        public int InsertAgencyPayable(AgencyPayableDTO agencyPayable)
        {
            var command = new SqlCommand("hpf_agency_payable_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[16];
                sqlParam[0] = new SqlParameter("@pi_agency_id", agencyPayable.AgencyId);
                sqlParam[1] = new SqlParameter("@pi_pmt_dt", NullableDateTime(agencyPayable.PaymentDate));
                sqlParam[2] = new SqlParameter("@pi_pmt_cd", agencyPayable.PayamentCode);
                sqlParam[3] = new SqlParameter("@pi_status_cd", agencyPayable.StatusCode);
                sqlParam[4] = new SqlParameter("@pi_period_start_dt", NullableDateTime(agencyPayable.PeriodStartDate));
                sqlParam[5] = new SqlParameter("@pi_period_end_dt", NullableDateTime(agencyPayable.PeriodEndDate));
                sqlParam[6] = new SqlParameter("@pi_pmt_comment", agencyPayable.PaymentComment);
                sqlParam[7] = new SqlParameter("@pi_accounting_link_TBD", agencyPayable.AccountLinkTBD);
                sqlParam[8] = new SqlParameter("@pi_create_dt", NullableDateTime(agencyPayable.CreateDate));
                sqlParam[9] = new SqlParameter("@pi_create_user_id", agencyPayable.CreateUserId);
                sqlParam[10] = new SqlParameter("@pi_create_app_name", agencyPayable.CreateAppName);
                sqlParam[11] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(agencyPayable.ChangeLastDate));
                sqlParam[12] = new SqlParameter("@pi_chg_lst_user_id", agencyPayable.ChangeLastUserId);
                sqlParam[13] = new SqlParameter("@pi_chg_lst_app_name", agencyPayable.ChangeLastAppName);
                sqlParam[14] = new SqlParameter("@pi_agency_payable_pmt_amt", agencyPayable.AgencyPayablePaymentAmount);
                sqlParam[15] = new SqlParameter("@po_agency_payable_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                command.ExecuteNonQuery();
                agencyPayable.AgencyPayableId = ConvertToInt(sqlParam[15].Value);
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            return agencyPayable.AgencyPayableId;
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
        public AgencyPayableDraftDTO CreateDraftAgencyPayable(AgencyPayableSearchCriteriaDTO agencyPayableCriteria)
        {
            AgencyPayableDraftDTO results = new AgencyPayableDraftDTO();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_agency_payable_search_draft", dbConnection);            
            var sqlParam = new SqlParameter[8];
            try
            {
                sqlParam[0] = new SqlParameter("@pi_agency_id", agencyPayableCriteria.AgencyId);
                sqlParam[1] = new SqlParameter("@pi_start_dt", agencyPayableCriteria.PeriodStartDate);
                sqlParam[2] = new SqlParameter("@pi_end_dt", agencyPayableCriteria.PeriodEndDate);
                sqlParam[3] = new SqlParameter("@pi_case_completed_ind", agencyPayableCriteria.CaseComplete);
                sqlParam[4] = new SqlParameter("@pi_servicer_consent_ind", agencyPayableCriteria.ServicerConsent);
                sqlParam[5] = new SqlParameter("@pi_funding_consent_ind", agencyPayableCriteria.FundingConsent);
                sqlParam[6] = new SqlParameter("@pi_loan_1st_2nd_cd", agencyPayableCriteria.LoanIndicator);
                sqlParam[7] = new SqlParameter("@pi_max_number_cases", agencyPayableCriteria.MaxNumberOfCase);                
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;           
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    ForeclosureCaseDraftDTOCollection fCaseDraftCollection = new ForeclosureCaseDraftDTOCollection();
                    results.AgencyId = agencyPayableCriteria.AgencyId;
                    results.PeriodStartDate = agencyPayableCriteria.PeriodStartDate;
                    results.PeriodEndDate = agencyPayableCriteria.PeriodEndDate;
                    while (reader.Read())
                    {
                        ForeclosureCaseDraftDTO item = new ForeclosureCaseDraftDTO();                        
                        item.ForeclosureCaseId = ConvertToInt(reader["fc_id"]);
                        item.AgencyCaseId = ConvertToString(reader["agency_id"]);
                        item.CompletedDate = ConvertToDateTime(reader["completed_dt"]);
                        item.Amount = ConvertToDecimal(reader["pmt_rate"]);
                        item.AccountLoanNumber = ConvertToString(reader["acct_num"]);
                        item.ServicerName = ConvertToString(reader["servicer_name"]);
                        item.BorrowerName = ConvertToString(reader["borrower_name"]);                            
                        fCaseDraftCollection.Add(item);                        
                    }                    
                    reader.Close();
                    results.ForclosureCaseDrafts = fCaseDraftCollection;                    
                }                
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return results;
        }
        #endregion
    }
}
