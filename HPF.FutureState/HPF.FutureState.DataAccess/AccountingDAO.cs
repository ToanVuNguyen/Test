using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using System.Data.SqlClient;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    public class AccountingDAO : BaseDAO
    {
        protected AccountingDAO()
        { }
        public static AccountingDAO CreateInstance()
        {
            return new AccountingDAO();
        }
        #region display
        /// <summary>
        /// Payment info
        /// </summary>
        /// <param name="agencyPayableCriteria">case id</param>
        /// <returns></returns>
        public AgencyPayableCaseDTOCollection DisplayAgencyPayable(int CaseID)
        {
            AgencyPayableCaseDTOCollection results = new AgencyPayableCaseDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_agency_payable_detail_get", dbConnection);
            var sqlParam = new SqlParameter[1];
            try
            {

                sqlParam[0] = new SqlParameter("@pi_fc_id", CaseID);
                command.Parameters.AddRange(sqlParam);
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AgencyPayableCaseDTO result = new AgencyPayableCaseDTO();
                        result.AgencyName = ConvertToString(reader["agency_name"]);
                        result.AgencyPayableId = ConvertToInt(reader["agency_payable_id"]);
                        result.PaymentDate = ConvertToDateTime(reader["pmt_dt"]);
                        result.NFMCDifferenceEligibleInd = ConvertToString(reader["NFMC_difference_eligible_ind"]);
                        result.NFMCDiffererencePaidInd = ConvertToString(reader["NFMC_difference_paid_ind"]);
                        result.PaymentAmount = ConvertToDecimal(reader["agency_payable_pmt_amt"]);
                        results.Add(result);
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
            return results;
        }
        /// <summary>
        /// display billing information
        /// </summary>
        /// <param name="CaseID">case id</param>
        /// <returns></returns>
        public BillingInfoDTOCollection DisplayInvoiceCase(int CaseID)
        {
            BillingInfoDTOCollection results = new BillingInfoDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_invoice_detail_get", dbConnection);
            var sqlParam = new SqlParameter[1];
            try
            {

                sqlParam[0] = new SqlParameter("@pi_fc_id", CaseID);
                command.Parameters.AddRange(sqlParam);
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BillingInfoDTO result = new BillingInfoDTO();

                        result.InvoiceDate = ConvertToDateTime(reader["invoice_dt"]);
                        result.FundingSourceName = ConvertToString(reader["funding_source_name"]);
                        result.InvoiceId = ConvertToInt(reader["Invoice_id"]);
                        result.Loan = ConvertToString(reader["acct_num"]);
                        result.InDisputeIndicator = ConvertToString(reader["loan_1st_2nd_cd"]);
                        result.InvoiceCaseBillAmount = ConvertToDouble(reader["invoice_case_bill_amt"]);
                        result.InvoiceCasePaymentAmount = ConvertToDouble(reader["invoice_case_pmt_amt"]);
                        result.PaidDate = ConvertToDateTime(reader["pmt_dt"]);
                        result.PaymentRejectReasonCode = ConvertToString(reader["pmt_reject_reason_cd"]);
                        results.Add(result);
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
            return results;
        }
        public AccountingDTO DisplayAccounting(int Fc_ID)
        {
            AccountingDTO results = new AccountingDTO();
            SqlConnection dbConnecion = CreateConnection();
            SqlCommand command = CreateSPCommand("hpf_foreclosure_case_nbr_npr_get", dbConnecion);
            var sqlParam = new SqlParameter[1];
            try
            {

                sqlParam[0] = new SqlParameter("@pi_fc_id", Fc_ID);
                command.Parameters.AddRange(sqlParam);
                dbConnecion.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        results.NerverPayReason = ConvertToString(reader["never_pay_reason_cd"]);
                        results.NeverBillReason = ConvertToString(reader["never_bill_reason_cd"]);
                        AgencyPayableCaseDTOCollection agencypayable_result = DisplayAgencyPayable(Fc_ID);
                        BillingInfoDTOCollection invoice_result = DisplayInvoiceCase(Fc_ID);
                        results.AgencyPayableCase = agencypayable_result;
                        results.BillingInfo = invoice_result;
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
                dbConnecion.Close();
            }
            return results;
        }


        #endregion
        #region save
        public void UpdateForclosureCase(string NeverBillReason, string NeverPayReason, int Fc_ID)
        {
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_foreclosure_case_update_npr_nbr", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@pi_fc_id", Fc_ID);
            sqlParam[1] = new SqlParameter("@pi_never_pay_reason", NeverPayReason);
            sqlParam[2] = new SqlParameter("@pi_never_bill_reason", NeverBillReason);
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
