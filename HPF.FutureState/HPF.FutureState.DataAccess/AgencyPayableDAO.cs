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
    public class AgencyPayableDAO : BaseDAO
    {
        public SqlTransaction trans;
        public SqlConnection dbConnection;
        protected AgencyPayableDAO()
        {
        }
        public static AgencyPayableDAO CreateInstance()
        {
            return new AgencyPayableDAO();
        }
        ///<summary>
        ///begin working
        /// </summary>
        /// 
        public void BeginTran()
        {
            dbConnection = CreateConnection();
            dbConnection.Open();
            trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);

        }
        ///<summary>
        ///commit working
        ///</summary>
        public void CommitTran()
        {
            try
            {
                trans.Commit();
                dbConnection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        ///<summary>
        ///rollback working
        ///</summary>
        public void RollbackTran()
        {
            try
            {
                trans.Rollback();
                dbConnection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        # region Insert

        /// <summary>
        /// Insert AgencyPayableCase with agencyPayableCase provided
        /// </summary>
        /// <param name="agencyPayableCase"></param>
        /// <returns></returns>
        public void InsertAgencyPayableCase(AgencyPayableCaseDTO agencyPayableCase)
        {
            var command = CreateSPCommand("hpf_agency_payable_case_insert", this.dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[14];
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
            sqlParam[10] = new SqlParameter("@pi_NFMC_difference_eligible_ind ", agencyPayableCase.NFMCDifferenceEligibleInd);
            sqlParam[11] = new SqlParameter("@pi_takeback_pmt_identified_dt",DateTime.Now);
            sqlParam[12] = new SqlParameter("@pi_takeback_pmt_reason_cd", null);
            sqlParam[13] = new SqlParameter("@pi_NFMC_difference_paid_amt", null);
            
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            try
            {
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
        public int? InsertAgencyPayable(AgencyPayableDTO agencyPayable)
        {
            var command = CreateSPCommand("hpf_agency_payable_insert", this.dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[15];
            sqlParam[0] = new SqlParameter("@pi_agency_id", agencyPayable.AgencyId);
            sqlParam[1] = new SqlParameter("@pi_pmt_dt", NullableDateTime(agencyPayable.PaymentDate));
            sqlParam[2] = new SqlParameter("@pi_status_cd", agencyPayable.StatusCode);
            sqlParam[3] = new SqlParameter("@pi_period_start_dt", NullableDateTime(agencyPayable.PeriodStartDate));
            sqlParam[4] = new SqlParameter("@pi_period_end_dt", NullableDateTime(agencyPayable.PeriodEndDate));
            sqlParam[5] = new SqlParameter("@pi_pmt_comment", agencyPayable.PaymentComment);
            sqlParam[6] = new SqlParameter("@pi_accounting_link_TBD", agencyPayable.AccountLinkTBD);
            sqlParam[7] = new SqlParameter("@pi_create_dt", NullableDateTime(agencyPayable.CreateDate));
            sqlParam[8] = new SqlParameter("@pi_create_user_id", agencyPayable.CreateUserId);
            sqlParam[9] = new SqlParameter("@pi_create_app_name", agencyPayable.CreateAppName);
            sqlParam[10] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(agencyPayable.ChangeLastDate));
            sqlParam[11] = new SqlParameter("@pi_chg_lst_user_id", agencyPayable.ChangeLastUserId);
            sqlParam[12] = new SqlParameter("@pi_chg_lst_app_name", agencyPayable.ChangeLastAppName);
            sqlParam[13] = new SqlParameter("@pi_agency_payable_pmt_amt", agencyPayable.AgencyPayablePaymentAmount);
            sqlParam[14] = new SqlParameter("@po_agency_payable_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            try
            {
                command.Transaction = this.trans;
                command.ExecuteNonQuery();
                agencyPayable.AgencyPayableId = ConvertToInt(sqlParam[14].Value);

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

        ///<summary>
        ///Update Status, Comments when you click Cancel Payments
        /// </summary>
        public void CancelAgencyPayable(AgencyPayableDTO agencyPayable)
        {
            var dbConnection = CreateConnection();
            var command =CreateSPCommand("hpf_agency_payable_update", dbConnection);
            var sqlParam = new SqlParameter[12];
            sqlParam[0] = new SqlParameter("@pi_agency_payable_id", agencyPayable.AgencyPayableId);
            sqlParam[1] = new SqlParameter("@pi_status_cd", agencyPayable.StatusCode);
            sqlParam[2] = new SqlParameter("@pi_pmt_comment", null);
            //
            sqlParam[3] = new SqlParameter("@pi_agency_id", null);
            sqlParam[4] = new SqlParameter("@pi_pmt_dt", null);
            sqlParam[5] = new SqlParameter("@pi_period_start_dt", null);
            sqlParam[6] = new SqlParameter("@pi_period_end_dt", null);
            sqlParam[7] = new SqlParameter("@pi_accounting_link_TBD", null);
            sqlParam[8] = new SqlParameter("@pi_chg_lst_dt", null);
            sqlParam[9] = new SqlParameter("@pi_chg_lst_user_id", null);
            sqlParam[10] = new SqlParameter("@pi_chg_lst_app_name", null);
            sqlParam[11] = new SqlParameter("@pi_agency_payable_pmt_amt", null);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
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
            AgencyPayableDTOCollection result = new AgencyPayableDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_agency_payable_search", dbConnection);
            var sqlParam = new SqlParameter[3];
            try
            {

                sqlParam[0] = new SqlParameter("@pi_agency_id", agencyPayableCriteria.AgencyId);
                sqlParam[1] = new SqlParameter("@pi_start_dt", agencyPayableCriteria.PeriodStartDate);
                sqlParam[2] = new SqlParameter("@pi_end_dt", agencyPayableCriteria.PeriodEndDate);
                command.Parameters.AddRange(sqlParam);
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    AgencyPayableCaseDTOCollection agencyPayableCaseDTOCollection = new AgencyPayableCaseDTOCollection();

                    while (reader.Read())
                    {
                        AgencyPayableDTO results = new AgencyPayableDTO();
                        results.AgencyName = ConvertToString(reader["agency_name"]);
                        results.AgencyPayableId = ConvertToInt(reader["agency_payable_id"]);
                        results.PaymentDate = ConvertToDateTime(reader["pmt_dt"]);
                        results.PeriodStartDate = ConvertToDateTime(reader["period_start_dt"]);
                        results.PeriodEndDate = ConvertToDateTime(reader["period_end_dt"]);
                        results.TotalAmount = ConvertToDouble(reader["agency_payable_pmt_amt"]);
                        results.PaymentComment = ConvertToString(reader["pmt_comment"]);
                        results.StatusCode = ConvertToString(reader["status_cd"]);
                        result.Add(results);
                    }
                    reader.Close();
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
            return result;
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
            var sqlParam = new SqlParameter[5];
            ForeclosureCaseDraftDTOCollection fCaseDraftCollection = new ForeclosureCaseDraftDTOCollection();
            try
            {
                sqlParam[0] = new SqlParameter("@pi_agency_id", agencyPayableCriteria.AgencyId);
                sqlParam[1] = new SqlParameter("@pi_start_dt", agencyPayableCriteria.PeriodStartDate);
                sqlParam[2] = new SqlParameter("@pi_end_dt", agencyPayableCriteria.PeriodEndDate);
                sqlParam[3] = new SqlParameter("@pi_case_completed_ind", (agencyPayableCriteria.CaseComplete == CustomBoolean.None) ? null : agencyPayableCriteria.CaseComplete.ToString());
                sqlParam[4] = new SqlParameter("@pi_consent_flag", (agencyPayableCriteria.Indicator ));
                command.Parameters.AddRange(sqlParam);
                dbConnection.Open();
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    results.AgencyId = agencyPayableCriteria.AgencyId;
                    results.PeriodStartDate = agencyPayableCriteria.PeriodStartDate;
                    results.PeriodEndDate = agencyPayableCriteria.PeriodEndDate;
                    while (reader.Read())
                    {
                        ForeclosureCaseDraftDTO item = new ForeclosureCaseDraftDTO();
                        item.ForeclosureCaseId = ConvertToInt(reader["fc_id"]);
                        item.AgencyCaseId = ConvertToString(reader["agency_case_num"]);
                        item.CompletedDate = ConvertToDateTime(reader["completed_dt"]);
                        item.CreateDate = ConvertToDateTime(reader["create_dt"]);
                        item.Amount = ConvertToDouble(reader["pmt_rate"]);
                        item.AccountLoanNumber = ConvertToString(reader["acct_num"]);
                        item.ServicerName = ConvertToString(reader["servicer_name"]);
                        item.BorrowerName = ConvertToString(reader["borrower_name"]);
                        item.Srvcr = ConvertToString(reader["servicer_consent_ind"]);
                        item.Fund = ConvertToString(reader["funding_consent_ind"]);

                        fCaseDraftCollection.Add(item);
                    }
                    reader.Close();
                }
                results.ForclosureCaseDrafts = fCaseDraftCollection;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public AgencyPayableSetDTO AgencyPayableSetGet(int? agencypayableId)
        {
            AgencyPayableSetDTO result = new AgencyPayableSetDTO();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_agency_payable_get", dbConnection);
            command.Connection = dbConnection;
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_agency_payable_id", agencypayableId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    //read Invoice
                    reader.Read();
                    AgencyPayableDTO payable = new AgencyPayableDTO();
                    payable.AgencyId = ConvertToInt(reader["agency_id"]);
                    payable.AgencyName = ConvertToString(reader["agency_name"]);
                    payable.PeriodStartDate = ConvertToDateTime(reader["period_start_dt"]) == null ? DateTime.MinValue : ConvertToDateTime(reader["period_start_dt"]).Value;
                    payable.PeriodEndDate = ConvertToDateTime(reader["period_end_dt"]) == null ? DateTime.MinValue : ConvertToDateTime(reader["period_end_dt"]).Value;
                    payable.PaymentComment = ConvertToString(reader["pmt_comment"]);
                    payable.PayableNum = ConvertToInt(reader["agency_payable_id"]);
                    result.Payable=payable;

                    reader.NextResult();
                    // read payable Cases

                    while (reader.Read())
                    {
                        var payableCase = new AgencyPayableCaseDTO();

                        payableCase.ForeclosureCaseId = ConvertToInt(reader["fc_id"]);
                        payableCase.AgencyPayableId = ConvertToInt(reader["agency_payable_case_id"]);
                        payableCase.AgencyCaseID = ConvertToInt(reader["agency_case_num"]);
                        payableCase.CreateDt = ConvertToDateTime(reader["create_dt"])==null? DateTime.MinValue : ConvertToDateTime(reader["create_dt"]).Value;
                        payableCase.CompleteDt = ConvertToDateTime(reader["completed_dt"]) == null ? DateTime.MinValue : ConvertToDateTime(reader["completed_dt"]).Value;
                        payableCase.PaymentAmount = ConvertToDouble(reader["pmt_amt"])==null?0:ConvertToDouble(reader["pmt_amt"]).Value;
                        payableCase.LoanNum = ConvertToString(reader["acct_num"]);
                        payableCase.ServicerName= ConvertToString(reader["servicer_name"]);
                        payableCase.BorrowerName = ConvertToString(reader["Borrower"]);
                        payableCase.NFMCDifferenceEligibleInd = ConvertToString(reader["NFMC_difference_eligible_ind"]);
                        payableCase.NFMCDifferencePaidAmt = ConvertToDouble(reader["NFMC_difference_paid_amt"]);
                        payableCase.TakebackReason = ConvertToString(reader["takeback_pmt_reason_cd"]);
                        payableCase.TakebackDate = ConvertToDateTime(reader["takeback_pmt_identified_dt"]) == null ? DateTime.MinValue : ConvertToDateTime(reader["takeback_pmt_identified_dt"]).Value;

                        result.PayableCases.Add(payableCase);
                    }
                    reader.Close();
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
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoicePayment"></param>
        public void TakebackMarkCase(AgencyPayableSetDTO agencyPayableSet,string takebackReason,string agencyPayableIDCol )
        {
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_agency_payable_case_update", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[6];
            sqlParam[0] = new SqlParameter("@pi_update_flag", 0);
            sqlParam[1] = new SqlParameter("@pi_str_agency_payable_case_id", agencyPayableIDCol);// payableid collection
            sqlParam[2] = new SqlParameter("@pi_takeback_pmt_reason_cd", takebackReason);
            sqlParam[3] = new SqlParameter("@pi_chg_lst_dt", agencyPayableSet.ChangeLastDate);
            sqlParam[4] = new SqlParameter("@pi_chg_lst_user_id", agencyPayableSet.ChangeLastUserId);
            sqlParam[5] = new SqlParameter("@pi_chg_lst_app_name",agencyPayableSet.ChangeLastAppName);
            
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally {
                dbConnection.Close();
            }
        }
        public void PayUnPayMarkCase(AgencyPayableSetDTO agencyPayableSet, string agencyPayableIDCol)
        {
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_agency_payable_case_update", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[6];
            sqlParam[0] = new SqlParameter("@pi_update_flag", 1);
            sqlParam[1] = new SqlParameter("@pi_str_agency_payable_case_id", agencyPayableIDCol);// payableid collection
            sqlParam[2] = new SqlParameter("@pi_takeback_pmt_reason_cd", null);
            sqlParam[3] = new SqlParameter("@pi_chg_lst_dt", agencyPayableSet.ChangeLastDate);
            sqlParam[4] = new SqlParameter("@pi_chg_lst_user_id", agencyPayableSet.ChangeLastUserId);
            sqlParam[5] = new SqlParameter("@pi_chg_lst_app_name", agencyPayableSet.ChangeLastAppName);

            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }
       
        #endregion
    }
}
