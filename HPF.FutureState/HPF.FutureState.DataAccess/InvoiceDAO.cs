using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.Utils;

namespace HPF.FutureState.DataAccess
{
    public class InvoiceDAO: BaseDAO
    {
        # region Private variables
        private SqlConnection dbConnection;
        /// <summary>
        /// Share transaction for InvoiceCase and Invoice
        /// </summary>
        private SqlTransaction trans;
        #endregion

        #region Share functions
        public static InvoiceDAO CreateInstance()
        {
            return new InvoiceDAO();
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

        #region Insert
        public bool InsertInvoiceCase(InvoiceCaseDTO invoiceCase)
        {
            throw new NotImplementedException();
        }
        public bool InserInvoice(InvoiceDTO invoice)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Searching Create Draft
        public InvoiceSearchResultDTOCollection SearchInvoice(InvoiceSearchCriteriaDTO searchCriteria)
        {
            InvoiceSearchResultDTOCollection invoices = null;
           
            try
            {
                dbConnection = base.CreateConnection();
                SqlCommand command = base.CreateCommand("hpf_invoice_search", dbConnection);
                //<Parameter>
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@pi_funding_source_id", searchCriteria.FundingSourceId);
                sqlParam[1] = new SqlParameter("@pi_start_dt", searchCriteria.PeriodStart);
                sqlParam[2] = new SqlParameter("@pi_end_dt", searchCriteria.PeriodEnd);

                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;

                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    invoices = new InvoiceSearchResultDTOCollection();
                    while (reader.Read())
                    {
                        InvoiceSearchResultDTO invoice = new InvoiceSearchResultDTO();
                        invoice.FundingSourceId = ConvertToInt(reader["funding_source_id"]);
                        invoice.FundingSourceName = ConvertToString(reader["funding_source_name"]);
                        invoice.InvoiceBillAmt = ConvertToDecimal(reader["invoice_bill_amt"]);                                                
                        invoice.InvoiceComment = ConvertToString(reader["invoice_comment"]);                        
                        invoice.InvoiceId = ConvertToInt(reader["Invoice_id"]);
                        invoice.InvoicePeriod = ConvertToString(reader["invoice_period"]);
                        invoice.InvoicePmtAmt = ConvertToDecimal(reader["invoice_pmt_amt"]);
                        invoice.StatusCd = ConvertToString(reader["status_cd"]);
                        invoice.InvoiceDate = ConvertToDateTime(reader["invoice_dt"]).Date;
                        invoices.Add(invoice);                           
                    }
                    reader.Close();
                }                
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return invoices;
        }

        public InvoiceDraftDTOCollection CreateInvoiceDraft(InvoiceSearchCriteriaDTO searchCriterial)
        {
            throw new NotImplementedException();
        }
        #endregion
        /// <summary>
        /// Get Funding Source to bind on DDLB
        /// </summary>
        /// <returns></returns>
        public FundingSourceDTOCollection AppGetFundingSource()
        {
            FundingSourceDTOCollection result = HPFCacheManager.Instance.GetData<FundingSourceDTOCollection>("fundingSource");
            if (result == null)
            {
                result = new FundingSourceDTOCollection();
                var dbConnection = CreateConnection();
                var command = new SqlCommand("hpf_funding_source_get", dbConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = dbConnection;
                try
                {
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var item = new FundingSourceDTO();
                            item.FundingSourceID = ConvertToInt(reader["funding_source_id"]);
                            item.FundingSourceName = ConvertToString(reader["funding_source_name"]);
                            result.Add(item);
                        }
                        reader.Close();
                    }
                    HPFCacheManager.Instance.Add("fundingSource", result);
                }
                catch (Exception ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(ex);
                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return result;
        }
        /// <summary>
        /// Get Servicer by FundingSourceId 
        /// </summary>
        /// <param name="fundingSourceId"></param>
        /// <returns>SErvicerDTOCollection</returns>
        public ServicerDTOCollection AppGetServicerByFundingSourceId(int fundingSourceId)
        {

            ServicerDTOCollection result = new ServicerDTOCollection();
            var dbConnection = CreateConnection();
            var command = new SqlCommand("hpf_servicer_get", dbConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = dbConnection;
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_funding_source_id", fundingSourceId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new ServicerDTO();
                        item.ServicerID = ConvertToInt(reader["servicer_id"]);
                        item.ServicerName = ConvertToString(reader["servicer_name"]);
                        result.Add(item);
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
        /// Search Invoice Draft
        /// </summary>
        /// <param name="searchCriteria">InvoiceCaseSearchCriteriaDTO</param>
        /// <returns></returns>
        public ForeclosureCaseDraftDTOCollection InvoiceCaseSearch(InvoiceCaseSearchCriteriaDTO searchCriteria)
        {
            ForeclosureCaseDraftDTOCollection invoices = null;
            try
            {
                dbConnection = base.CreateConnection();
                SqlCommand command = base.CreateCommand("hpf_invoice_case_search_draft", dbConnection);
                //<Parameter>   
                SqlParameter[] sqlParam = new SqlParameter[21];
                sqlParam[0] = new SqlParameter("@pi_funding_source_id",searchCriteria.FundingSourceId);
                sqlParam[1] = new SqlParameter("@pi_program_id", (searchCriteria.ProgramId==-1)?null:searchCriteria.ProgramId.ToString());
                sqlParam[2] = new SqlParameter("@pi_end_dt", searchCriteria.PeriodEnd);
                sqlParam[3] = new SqlParameter("@pi_start_dt", searchCriteria.PeriodStart);
                sqlParam[4] = new SqlParameter("@pi_duplicate_ind", searchCriteria.Duplicate== CustomBoolean.None?null:searchCriteria.ToString());
                sqlParam[5] = new SqlParameter("@pi_case_completed_ind", searchCriteria.CompleteCase==CustomBoolean.None?null:searchCriteria.CompleteCase.ToString());
                sqlParam[6] = new SqlParameter("@pi_is_billed_ind", searchCriteria.AlreadyBill==CustomBoolean.None?null:searchCriteria.AlreadyBill.ToString());
                sqlParam[7] = new SqlParameter("@pi_servicer_consent_ind", searchCriteria.ServicerConsent==CustomBoolean.None?null:searchCriteria.ServicerConsent.ToString());
                sqlParam[8] = new SqlParameter("@pi_funding_consent_ind", searchCriteria.FundingConsent==CustomBoolean.None?null:searchCriteria.FundingConsent.ToString());
                sqlParam[9] = new SqlParameter("@pi_loan_1st_2nd_cd", searchCriteria.LoanIndicator=="-1"?null:searchCriteria.LoanIndicator);
                sqlParam[10] = new SqlParameter("@pi_max_number_cases", searchCriteria.MaxNumOfCases==int.MinValue?null:searchCriteria.MaxNumOfCases.ToString());
                sqlParam[11] = new SqlParameter("@pi_gender_cd", searchCriteria.Gender=="-1"?null:searchCriteria.Gender);
                sqlParam[12] = new SqlParameter("@pi_race_cd", searchCriteria.Race=="-1"?null:searchCriteria.Race);
                sqlParam[13] = new SqlParameter("@pi_ethnicity_cd", searchCriteria.Hispanic==CustomBoolean.None?null:searchCriteria.Hispanic.ToString());
                sqlParam[14] = new SqlParameter("@pi_household_cd", searchCriteria.HouseholdCode=="-1"? null : searchCriteria.HouseholdCode);
                sqlParam[15] = new SqlParameter("@pi_city", searchCriteria.City== string.Empty ? null : searchCriteria.City);
                sqlParam[16] = new SqlParameter("@pi_state_cd", searchCriteria.State == "-1" ? null : searchCriteria.State);
                sqlParam[17] = new SqlParameter("@pi_min_age", searchCriteria.Age.Min == int.MinValue ? null : searchCriteria.Age.Min.ToString());
                sqlParam[18] = new SqlParameter("@pi_max_age", searchCriteria.Age.Max == int.MinValue ? null : searchCriteria.Age.Max.ToString());
                sqlParam[19] = new SqlParameter("@pi_min_gross_annual_income", searchCriteria.HouseholdGrossAnnualIncome.Min== double.MinValue ? null : searchCriteria.HouseholdGrossAnnualIncome.Min.ToString());
                sqlParam[20] = new SqlParameter("@pi_max_gross_annual_income", searchCriteria.HouseholdGrossAnnualIncome.Max== double.MinValue? null : searchCriteria.HouseholdGrossAnnualIncome.Max.ToString());
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;

                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    invoices = new ForeclosureCaseDraftDTOCollection();
                    while (reader.Read())
                    {
                        ForeclosureCaseDraftDTO caseDraft = new ForeclosureCaseDraftDTO();
                        caseDraft.ForeclosureCaseId= ConvertToInt(reader["fc_id"]);
                        caseDraft.AgencyCaseId = ConvertToString(reader["agency_case_num"]);
                        caseDraft.CompletedDate = ConvertToDateTime(reader["completed_dt"]);
                        caseDraft.Amount = ConvertToInt(reader["bill_rate"]);
                        caseDraft.AccountLoanNumber = ConvertToString(reader["acct_num"]);
                        caseDraft.ServicerName= ConvertToString(reader["servicer_name"]);
                        caseDraft.BorrowerName = ConvertToString(reader["borrower_name"]);
                        invoices.Add(caseDraft);
                    }
                    reader.Close();
                }
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return invoices;
        }
    }
}
