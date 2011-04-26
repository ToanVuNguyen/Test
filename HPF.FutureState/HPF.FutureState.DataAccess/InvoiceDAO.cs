using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common;

namespace HPF.FutureState.DataAccess
{
    public class InvoiceDAO : BaseDAO
    {
        # region Private variables
        private SqlConnection dbConnection;
        /// <summary>
        /// Share transaction for InvoiceCase and Invoice
        /// </summary>
        private SqlTransaction trans;
        #endregion

        public static InvoiceDAO CreateInstance()
        {
            return new InvoiceDAO();
        }

        public void BeginTransaction()
        {
            try
            {
                dbConnection = CreateConnection();
                dbConnection.Open();
                trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Commit work.
        /// </summary>
        public void CommitTransaction()
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
        /// <summary>
        /// Cancel work
        /// </summary>
        public void CancelTransaction()
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


        #region Insert
        /// <summary>
        /// Insert an Invoice-Case to the database
        /// </summary>
        /// <param name="invoiceCase">InvoiceCaseDTO</param>
        public void InsertInvoiceCase(InvoiceCaseDTO invoiceCase)
        {
            var command = CreateSPCommand("hpf_invoice_case_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[14];
            sqlParam[0] = new SqlParameter("@pi_fc_id", invoiceCase.ForeclosureCaseId);
            sqlParam[1] = new SqlParameter("@pi_Invoice_id", invoiceCase.InvoiceId);
            sqlParam[2] = new SqlParameter("@pi_pmt_reject_reason_cd", invoiceCase.PaymentRejectReasonCode);
            sqlParam[3] = new SqlParameter("@pi_invoice_case_pmt_amt", invoiceCase.InvoiceCasePaymentAmount);
            sqlParam[4] = new SqlParameter("@pi_invoice_case_bill_amt", invoiceCase.InvoiceCaseBillAmount);
            sqlParam[5] = new SqlParameter("@pi_in_dispute_ind", invoiceCase.InDisputeIndicator);
            sqlParam[6] = new SqlParameter("@pi_rebill_ind", invoiceCase.RebuildIndicator);
            sqlParam[7] = new SqlParameter("@pi_intent_to_pay_flg_TBD", invoiceCase.IntentToPayFlagBTD);
            sqlParam[8] = new SqlParameter("@pi_create_dt", NullableDateTime(invoiceCase.CreateDate));
            sqlParam[9] = new SqlParameter("@pi_create_user_id", invoiceCase.CreateUserId);
            sqlParam[10] = new SqlParameter("@pi_create_app_name", invoiceCase.CreateAppName);
            sqlParam[11] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(invoiceCase.ChangeLastDate));
            sqlParam[12] = new SqlParameter("@pi_chg_lst_user_id", invoiceCase.ChangeLastUserId);
            sqlParam[13] = new SqlParameter("@pi_chg_lst_app_name", invoiceCase.CreateAppName);
            command.Parameters.AddRange(sqlParam);
            //</Parameter>
            try
            {
                command.Transaction = trans;
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
        }
        /// <summary>
        /// Update Invoice and Invoice Cases when user want to Reject,Pay,UnPay InvoiceCases
        /// </summary>
        /// <param name="invoiceSet">Contains InvoiceDTO and InvoiceCaseDTOCollection</param>
        /// <param name="invoiceCaseIdCollection">a string that contains all invoiceCaseId to change</param>
        /// <param name="updateFlag">0:Reject, 1:Unpay, 2:Pay</param>
        /// <returns>true: Update Sucessfull;false:Payment_id does not exists</returns>
        public bool UpdateInvoiceCase(InvoiceSetDTO invoiceSet, List<string> invoiceCaseIdCollection, InvoiceCaseUpdateFlag updateFlag)
        {
            InvoiceDTO invoice = invoiceSet.Invoice;
            InvoiceCaseDTOCollection invoiceCases = invoiceSet.InvoiceCases;
            var command = CreateSPCommand("hpf_invoice_case_update_for_invoice", dbConnection);
            command.Transaction = trans;
            var sqlParam = new SqlParameter[11];
            //<Parameter>
            //</Parameter>
            try
            {
                foreach (var s in invoiceCaseIdCollection)
                {
                    sqlParam[0] = new SqlParameter("@pi_update_flag", (int)updateFlag);
                    sqlParam[1] = new SqlParameter("@pi_Invoice_id", invoice.InvoiceId);
                    sqlParam[3] = new SqlParameter("@pi_pmt_reject_reason_cd", invoiceSet.PaymentRejectReason);
                    sqlParam[4] = new SqlParameter("@pi_invoice_payment_id", invoiceSet.InvoicePaymentId);
                    sqlParam[5] = new SqlParameter("@pi_invoice_pmt_amt", invoiceSet.Invoice.InvoicePaymentAmount);
                    sqlParam[6] = new SqlParameter("@pi_chg_lst_dt", invoiceSet.ChangeLastDate);
                    sqlParam[7] = new SqlParameter("@pi_chg_lst_user_id", invoiceSet.ChangeLastUserId);
                    sqlParam[8] = new SqlParameter("@pi_chg_lst_app_name", invoiceSet.ChangeLastAppName);
                    sqlParam[9] = new SqlParameter("@po_valid_invoice_payment_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    sqlParam[10] = new SqlParameter("@pi_invoice_bill_amt", invoiceSet.Invoice.InvoiceBillAmount);
                    sqlParam[2] = new SqlParameter("@pi_str_invoice_case_id", s);
                    command.Parameters.Clear();
                    command.Parameters.AddRange(sqlParam);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            // @po_valid_invoice_payment_id: 
            // 0 : Not exitst in invoice payment
            // >0 : Exists in invoice payment
            return (ConvertToInt(sqlParam[9].Value) > 0);

        }
        /// <summary>
        /// InsertInvoice to the database
        /// </summary>
        /// <param name="invoice">InvoiceDTO</param>
        /// <returns>Invoice ID</returns>
        public int InsertInvoice(InvoiceDTO invoice)
        {
            var command = CreateSPCommand("hpf_invoice_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[16];
            sqlParam[0] = new SqlParameter("@pi_funding_source_id", invoice.FundingSourceId);
            sqlParam[1] = new SqlParameter("@pi_invoice_dt", NullableDateTime(invoice.InvoiceDate));
            sqlParam[2] = new SqlParameter("@pi_period_start_dt", NullableDateTime(invoice.PeriodStartDate));
            sqlParam[3] = new SqlParameter("@pi_period_end_dt", NullableDateTime(invoice.PeriodEndDate));
            sqlParam[4] = new SqlParameter("@pi_invoice_pmt_amt", invoice.InvoicePaymentAmount);
            sqlParam[5] = new SqlParameter("@pi_invoice_bill_amt", invoice.InvoiceBillAmount);
            sqlParam[6] = new SqlParameter("@pi_status_cd", invoice.StatusCode);
            sqlParam[7] = new SqlParameter("@pi_invoice_comment", invoice.InvoiceComment);
            sqlParam[8] = new SqlParameter("@pi_accounting_link_TBD", invoice.AccountingLinkBTD);
            sqlParam[9] = new SqlParameter("@pi_create_dt", NullableDateTime(invoice.CreateDate));
            sqlParam[10] = new SqlParameter("@pi_create_user_id", invoice.CreateUserId);
            sqlParam[11] = new SqlParameter("@pi_create_app_name", invoice.CreateAppName);
            sqlParam[12] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(invoice.ChangeLastDate));
            sqlParam[13] = new SqlParameter("@pi_chg_lst_user_id", invoice.ChangeLastUserId);
            sqlParam[14] = new SqlParameter("@pi_chg_lst_app_name", invoice.ChangeLastAppName);
            sqlParam[15] = new SqlParameter("@po_Invoice_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            try
            {
                command.Transaction = trans;
                command.ExecuteNonQuery();
                invoice.InvoiceId = ConvertToInt(sqlParam[15].Value);
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            return invoice.InvoiceId.Value;
        }

        public void UpdateInvoice(InvoiceDTO invoice)
        {
            var command = CreateSPCommand("hpf_invoice_update", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[13];
            sqlParam[0] = new SqlParameter("@pi_funding_source_id", invoice.FundingSourceId);
            sqlParam[1] = new SqlParameter("@pi_invoice_dt", NullableDateTime(invoice.InvoiceDate));
            sqlParam[2] = new SqlParameter("@pi_period_start_dt", NullableDateTime(invoice.PeriodStartDate));
            sqlParam[3] = new SqlParameter("@pi_period_end_dt", NullableDateTime(invoice.PeriodEndDate));
            sqlParam[4] = new SqlParameter("@pi_invoice_pmt_amt", invoice.InvoicePaymentAmount);
            sqlParam[5] = new SqlParameter("@pi_invoice_bill_amt", invoice.InvoiceBillAmount);
            sqlParam[6] = new SqlParameter("@pi_status_cd", invoice.StatusCode);
            sqlParam[7] = new SqlParameter("@pi_invoice_comment", invoice.InvoiceComment);
            sqlParam[8] = new SqlParameter("@pi_accounting_link_TBD", invoice.AccountingLinkBTD);
            sqlParam[9] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(invoice.ChangeLastDate));
            sqlParam[10] = new SqlParameter("@pi_chg_lst_user_id", invoice.ChangeLastUserId);
            sqlParam[11] = new SqlParameter("@pi_chg_lst_app_name", invoice.ChangeLastAppName);
            sqlParam[12] = new SqlParameter("@pi_Invoice_id", invoice.InvoiceId);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            try
            {
                command.Transaction = trans;
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
        }
        #endregion

        #region Searching Create Draft
        /// <summary>
        /// Search invoice 
        /// </summary>
        /// <param name="searchCriteria">Invoice searchCriteria</param>
        /// <returns>InvoiceDTOCollection</returns>
        public InvoiceDTOCollection SearchInvoice(InvoiceSearchCriteriaDTO searchCriteria)
        {
            InvoiceDTOCollection invoices = null;

            try
            {
                dbConnection = base.CreateConnection();
                SqlCommand command = base.CreateCommand("hpf_invoice_search", dbConnection);
                //<Parameter>
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@pi_funding_source_id", searchCriteria.FundingSourceId);
                sqlParam[1] = new SqlParameter("@pi_start_dt", searchCriteria.PeriodStart);
                sqlParam[2] = new SqlParameter("@pi_end_dt", searchCriteria.PeriodEnd);
                sqlParam[3] = new SqlParameter("@pi_includeCancel", searchCriteria.IncludeCancelledInvoice);

                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;

                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    invoices = new InvoiceDTOCollection();
                    while (reader.Read())
                    {
                        InvoiceDTO invoice = new InvoiceDTO();
                        invoice.FundingSourceId = ConvertToInt(reader["funding_source_id"]);
                        invoice.FundingSourceName = ConvertToString(reader["funding_source_name"]);
                        invoice.InvoiceBillAmount = ConvertToDouble(reader["invoice_bill_amt"]);
                        invoice.InvoiceComment = ConvertToString(reader["invoice_comment"]);
                        invoice.InvoiceId = ConvertToInt(reader["Invoice_id"]);
                        invoice.PeriodStartDate = ConvertToDateTime(reader["period_start_dt"]);
                        invoice.PeriodEndDate = ConvertToDateTime(reader["period_end_dt"]);
                        invoice.InvoicePaymentAmount = ConvertToDouble(reader["invoice_pmt_amt"]);
                        invoice.RejectedAmount = ConvertToDouble(reader["rejected_amt"]);
                        invoice.UnfundedAmount = ConvertToDouble(reader["unfunded_amt"]);
                        invoice.StatusCode = ConvertToString(reader["status_cd"]);
                        invoice.InvoiceDate = ConvertToDateTime(reader["invoice_dt"]);
                        if (invoice.PeriodStartDate != null && invoice.PeriodEndDate != null)
                            invoice.InvoicePeriod = invoice.PeriodStartDate.Value.ToShortDateString() + "-" + invoice.PeriodEndDate.Value.ToShortDateString();
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


        #endregion
        /// <summary>
        /// Get Funding Source to bind on DDLB
        /// </summary>
        /// <returns></returns>
        public FundingSourceDTOCollection AppGetFundingSource()
        {
            FundingSourceDTOCollection result = HPFCacheManager.Instance.GetData<FundingSourceDTOCollection>(Constant.HPF_CACHE_FUNDING_SOURCE);
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
                            item.FundingSourceID = ConvertToInt(reader["funding_source_id"]).Value;
                            item.FundingSourceName = ConvertToString(reader["funding_source_name"]);
                            item.BillingEmail = ConvertToString(reader["billing_email"]);
                            item.BillingDeliveryMethod = ConvertToString(reader["billing_delivery_method_cd"]);
                            item.ExportFormatCd = ConvertToString(reader["export_format_cd"]);
                            item.FundingSourceAbbrev = ConvertToString(reader["funding_source_abbrev"]);
                            item.SharePointFolder = ConvertToString(reader["sharepoint_foldername"]);
                            item.ActiveIndicator = ConvertToString(reader["active_ind"]);
                            item.EffectedDt = ConvertToDateTime(reader["eff_dt"]);
                            item.ExpiredDt = ConvertToDateTime(reader["exp_dt"]);
                            result.Add(item);
                        }
                        reader.Close();
                    }
                    HPFCacheManager.Instance.Add(Constant.HPF_CACHE_FUNDING_SOURCE, result);
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
        /// <returns>ForeclosureCaseDraftDTOCollection</returns>
        public ForeclosureCaseDraftDTOCollection InvoiceCaseSearch(InvoiceCaseSearchCriteriaDTO searchCriteria)
        {
            ForeclosureCaseDraftDTOCollection invoices = null;
            try
            {
                dbConnection = base.CreateConnection();
                SqlCommand command = base.CreateCommand("hpf_invoice_case_search_draft", dbConnection);
                //<Parameter>   
                SqlParameter[] sqlParam = new SqlParameter[29];
                sqlParam[0] = new SqlParameter("@pi_funding_source_id", searchCriteria.FundingSourceId);
                sqlParam[1] = new SqlParameter("@pi_program_id", (searchCriteria.ProgramId == "-1") ? null : searchCriteria.ProgramId.ToString());
                sqlParam[2] = new SqlParameter("@pi_end_dt", searchCriteria.PeriodEnd);
                sqlParam[3] = new SqlParameter("@pi_start_dt", searchCriteria.PeriodStart);
                sqlParam[4] = new SqlParameter("@pi_duplicate_ind", searchCriteria.Duplicate == CustomBoolean.None ? null : searchCriteria.Duplicate.ToString());
                sqlParam[5] = new SqlParameter("@pi_case_completed_ind", searchCriteria.Completed == CustomBoolean.None ? null : searchCriteria.Completed.ToString());
                sqlParam[6] = new SqlParameter("@pi_multiple_billing_ind", searchCriteria.AlreadyBill == CustomBoolean.None ? null : searchCriteria.AlreadyBill.ToString());
                sqlParam[7] = new SqlParameter("@pi_funding_consent_ind", searchCriteria.IgnoreFundingConsent == CustomBoolean.None ? null : searchCriteria.IgnoreFundingConsent.ToString());
                sqlParam[8] = new SqlParameter("@pi_max_number_cases", searchCriteria.MaxNumOfCases == -1 ? null : searchCriteria.MaxNumOfCases.ToString());
                sqlParam[9] = new SqlParameter("@pi_gender_cd", searchCriteria.Gender == "-1" ? null : searchCriteria.Gender);
                sqlParam[10] = new SqlParameter("@pi_race_cd", searchCriteria.Race == "-1" ? null : searchCriteria.Race);
                sqlParam[11] = new SqlParameter("@pi_hispanic_ind", searchCriteria.Hispanic == CustomBoolean.None ? null : searchCriteria.Hispanic.ToString());
                sqlParam[12] = new SqlParameter("@pi_household_cd", searchCriteria.HouseholdCode == "-1" ? null : searchCriteria.HouseholdCode);
                sqlParam[13] = new SqlParameter("@pi_city", searchCriteria.City == string.Empty ? null : searchCriteria.City);
                sqlParam[14] = new SqlParameter("@pi_state_cd", searchCriteria.State == "-1" ? null : searchCriteria.State);
                sqlParam[15] = new SqlParameter("@pi_min_age", searchCriteria.AgeMin == -1 ? null : searchCriteria.AgeMin.ToString());
                sqlParam[16] = new SqlParameter("@pi_max_age", searchCriteria.AgeMax == -1 ? null : searchCriteria.AgeMax.ToString());
                sqlParam[17] = new SqlParameter("@pi_min_gross_annual_income", searchCriteria.HouseholdGrossAnnualIncomeMin == -1 ? null : searchCriteria.HouseholdGrossAnnualIncomeMin.ToString());
                sqlParam[18] = new SqlParameter("@pi_max_gross_annual_income", searchCriteria.HouseholdGrossAnnualIncomeMax == -1 ? null : searchCriteria.HouseholdGrossAnnualIncomeMax.ToString());
                sqlParam[19] = new SqlParameter("@pi_selected_servicer_option_1",searchCriteria.ServicerRejected==false?null:"1" );
                sqlParam[20] = new SqlParameter("@pi_selected_servicer_option_2", searchCriteria.ServicerRejectedFreddie == false ? null : "1");
                sqlParam[21] = new SqlParameter("@pi_selected_servicer_option_3", searchCriteria.NeighborworkRejected == false ? null : "1");
                sqlParam[22] = new SqlParameter("@pi_selected_servicer_option_4", searchCriteria.SelectAllServicer == false ? null : "1");
                sqlParam[23] = new SqlParameter("@pi_selected_servicer_option_5", searchCriteria.SelectUnfunded == false ? null : "1");
                sqlParam[24] = new SqlParameter("@pi_servicer_consent_qty", searchCriteria.ServicerConsentQty);
                sqlParam[25] = new SqlParameter("@pi_UTL_ind", searchCriteria.UnableToLocateLoanInPortfolio);
                sqlParam[26] = new SqlParameter("@pi_DOCS_ind", searchCriteria.InvalidCounselingDocument);
                sqlParam[27] = new SqlParameter("@pi_agency_id", searchCriteria.AgencyId);
                sqlParam[28] = new SqlParameter("@pi_msa_code", searchCriteria.MSACode);


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
                        caseDraft.ForeclosureCaseId = ConvertToInt(reader["fc_id"]);
                        caseDraft.AgencyCaseId = ConvertToString(reader["agency_case_num"]);
                        caseDraft.CompletedDate = ConvertToDateTime(reader["completed_dt"]);
                        caseDraft.CreateDate= ConvertToDateTime(reader["create_dt"]);
                        caseDraft.Amount = ConvertToDouble(reader["bill_rate"]);
                        caseDraft.AccountLoanNumber = ConvertToString(reader["acct_num"]);
                        caseDraft.ServicerName = ConvertToString(reader["servicer_name"]);
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
        /// <summary>
        /// Get InvoiceSet to display in View/Edit Invoice Page
        /// </summary>
        /// <returns> InvoiceSetDTO containts info about the Invoice and InvoiceCases</returns>
        public InvoiceSetDTO InvoiceSetGet(int invoiceId)
        {
            InvoiceSetDTO result = new InvoiceSetDTO();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_invoice_get", dbConnection);
            command.Connection = dbConnection;
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_invoice_id", invoiceId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    //read Invoice
                    reader.Read();
                    InvoiceDTO invoice = new InvoiceDTO();
                    invoice.FundingSourceId = ConvertToInt(reader["funding_source_id"]);
                    invoice.FundingSourceName = ConvertToString(reader["funding_source_name"]);
                    invoice.InvoiceBillAmount = ConvertToDouble(reader["invoice_bill_amt"]);
                    invoice.InvoiceComment = ConvertToString(reader["invoice_comment"]);
                    invoice.InvoiceId = ConvertToInt(reader["Invoice_id"]);
                    invoice.PeriodStartDate = ConvertToDateTime(reader["period_start_dt"]);
                    invoice.PeriodEndDate = ConvertToDateTime(reader["period_end_dt"]);
                    invoice.InvoicePaymentAmount = ConvertToDouble(reader["invoice_pmt_amt"]);
                    invoice.StatusCode = ConvertToString(reader["status_cd"]);
                    invoice.InvoiceDate = ConvertToDateTime(reader["invoice_dt"]);

                    invoice.CreateDate = ConvertToDateTime(reader["create_dt"]);
                    invoice.CreateUserId = ConvertToString(reader["create_user_id"]);
                    invoice.CreateAppName = ConvertToString(reader["create_app_name"]);
                    invoice.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                    invoice.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                    invoice.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);


                    result.Invoice = invoice;

                    reader.NextResult();
                    // read InvoiceCases

                    while (reader.Read())
                    {
                        var invoiceCase = new InvoiceCaseDTO();

                        invoiceCase.InvoiceCaseId = ConvertToInt(reader["invoice_case_id"]);
                        invoiceCase.ForeclosureCaseId = ConvertToInt(reader["fc_id"]);
                        invoiceCase.AgencyCaseNum = ConvertToString(reader["agency_case_num"]);
                        invoiceCase.CaseCompleteDate = (ConvertToDateTime(reader["completed_dt"])) == null ? "" : (ConvertToDateTime(reader["completed_dt"])).Value.ToShortDateString();
                        invoiceCase.InvoiceCaseBillAmount = ConvertToDouble(reader["invoice_case_bill_amt"]);
                        invoiceCase.LoanNumber = ConvertToString(reader["acct_num"]);
                        invoiceCase.ServicerName = ConvertToString(reader["servicer_name"]);
                        invoiceCase.BorrowerName = ConvertToString(reader["borrower_name"]);
                        invoiceCase.PaidDate = (ConvertToDateTime(reader["pmt_dt"])) == null ? "" : (ConvertToDateTime(reader["pmt_dt"])).Value.ToShortDateString();
                        invoiceCase.InvoiceCasePaymentAmount = ConvertToDouble(reader["invoice_case_pmt_amt"]);
                        invoiceCase.PaymentRejectReasonCode = ConvertToString(reader["pmt_reject_reason_cd"]);
                        invoiceCase.InvenstorLoanId = ConvertToString(reader["investor_loan_num"]);
                        invoiceCase.CaseCreateDt = ConvertToDateTime(reader["foreclosure_case_create_dt"]);
                        invoiceCase.CreateDate = ConvertToDateTime(reader["create_dt"]);
                        invoiceCase.CreateUserId = ConvertToString(reader["create_user_id"]);
                        invoiceCase.CreateAppName = ConvertToString(reader["create_app_name"]);
                        invoiceCase.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                        invoiceCase.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                        invoiceCase.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);

                        result.InvoiceCases.Add(invoiceCase);
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
        /// Get InvoiceSet to display in View/Edit Invoice Page
        /// </summary>
        /// <returns> InvoiceSetDTO containts info about the Invoice and InvoiceCases</returns>
        public void BackEndPreProcessing(string xmlString,string fundingSourceId,int invoicePaymentId)
        {
            DataValidationException result = new DataValidationException();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_Invoice_case_validate", dbConnection);
            command.Connection = dbConnection;
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@pi_XMLDOC", xmlString);
            sqlParam[1] = new SqlParameter("@pi_funding_source_id", fundingSourceId);
            sqlParam[2] = new SqlParameter("@pi_invoice_payment_id", invoicePaymentId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        string errorCodes = ConvertToString(reader["error_code"]);
                        if (errorCodes == null)
                            continue;
                        int rowIndex = ConvertToInt(reader["row_index"]).Value;
                        //rowIndex = rowIndex + 1;(count from 1)
                        rowIndex++;
                        string[] errorList = errorCodes.Split(',');
                        foreach (string errorCode in errorList)
                        {
                            int firstIndex = errorCodes.IndexOf(errorCode);
                            int lastIndex = errorCodes.LastIndexOf(errorCode);
                            if (firstIndex != lastIndex)
                            {
                                errorCodes= errorCodes.Remove(firstIndex, errorCode.Length);
                                continue;
                            }
                            ExceptionMessage exMes = new ExceptionMessage();
                            exMes.Message = ErrorMessages.GetExceptionMessage(errorCode, rowIndex);
                            exMes.ErrorCode = errorCode;
                            result.ExceptionMessages.Add(exMes);
                        }

                    }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }
            if (result.ExceptionMessages.Count > 0)
                throw result;
        }

        public void InvoiceCaseUpdateForPayment(string xmlString,int invoicePaymentId, DateTime changeLastDt, string changeLastUserId, string changeLastAppName)
        {
            var command = CreateSPCommand("hpf_Invoice_case_update_for_payment", dbConnection);
            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@pi_XMLDOC", xmlString);
            sqlParam[1] = new SqlParameter("@pi_invoice_payment_id", invoicePaymentId);
            sqlParam[2] = new SqlParameter("@pi_chg_lst_dt", changeLastDt);
            sqlParam[3] = new SqlParameter("@pi_chg_lst_user_id", changeLastUserId);
            sqlParam[4] = new SqlParameter("@pi_chg_lst_app_name", changeLastAppName);
            command.Parameters.AddRange(sqlParam);
            try
            {
                command.Transaction = trans;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }

        public void UpdateInvoicePayment(InvoicePaymentDTO invoicePayment,bool withFile)
        {
            var command = CreateSPCommand("hpf_invoice_payment_update", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[12];
            sqlParam[0] = new SqlParameter("@pi_funding_source_id", invoicePayment.FundingSourceID);
            sqlParam[1] = new SqlParameter("@pi_pmt_num", invoicePayment.PaymentNum);
            sqlParam[2] = new SqlParameter("@pi_pmt_dt", NullableDateTime(invoicePayment.PaymentDate));
            sqlParam[3] = new SqlParameter("@pi_pmt_cd", invoicePayment.PaymentType);
            sqlParam[4] = new SqlParameter("@pi_pmt_amt", invoicePayment.PaymentAmount);
            sqlParam[5] = new SqlParameter("@pi_chg_lst_dt", invoicePayment.ChangeLastDate);
            sqlParam[6] = new SqlParameter("@pi_chg_lst_user_id", invoicePayment.ChangeLastUserId);
            sqlParam[7] = new SqlParameter("@pi_chg_lst_app_name", invoicePayment.ChangeLastAppName);
            sqlParam[8] = new SqlParameter("@pi_invoice_payment_id", invoicePayment.InvoicePaymentID);
            sqlParam[9] = new SqlParameter("@pi_invoice_payment_comment", invoicePayment.Comments);
            sqlParam[10] = new SqlParameter("@pi_payment_file", invoicePayment.PaymentFile);
            sqlParam[11] = new SqlParameter("@pi_with_file", withFile==true?1:0);

            
            
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            try
            {
                command.Transaction = trans;
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
        }
        public int InsertInvoicePayment(InvoicePaymentDTO invoicePayment)
        {
            var command = CreateSPCommand("hpf_invoice_payment_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[14];
            sqlParam[0] = new SqlParameter("@pi_funding_source_id", invoicePayment.FundingSourceID);
            sqlParam[1] = new SqlParameter("@pi_pmt_num", invoicePayment.PaymentNum);
            sqlParam[2] = new SqlParameter("@pi_pmt_dt", NullableDateTime(invoicePayment.PaymentDate));
            sqlParam[3] = new SqlParameter("@pi_pmt_cd", invoicePayment.PaymentType);
            sqlParam[4] = new SqlParameter("@pi_pmt_amt", invoicePayment.PaymentAmount);
            sqlParam[5] = new SqlParameter("@pi_chg_lst_dt", invoicePayment.ChangeLastDate);
            sqlParam[6] = new SqlParameter("@pi_chg_lst_user_id", invoicePayment.ChangeLastUserId);
            sqlParam[7] = new SqlParameter("@pi_chg_lst_app_name", invoicePayment.ChangeLastAppName);
            sqlParam[8] = new SqlParameter("@pi_create_dt", invoicePayment.CreateDate);
            sqlParam[9] = new SqlParameter("@pi_create_user_id", invoicePayment.CreateUserId);
            sqlParam[10] = new SqlParameter("@pi_create_app_name", invoicePayment.CreateAppName);
            sqlParam[11] = new SqlParameter("@po_invoice_payment_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
            sqlParam[12] = new SqlParameter("@pi_invoice_payment_comment", invoicePayment.Comments);
            sqlParam[13] = new SqlParameter("@pi_payment_file", invoicePayment.PaymentFile);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            try
            {
                command.Transaction = trans;
                command.ExecuteNonQuery();
                invoicePayment.InvoicePaymentID = ConvertToInt(sqlParam[11].Value);
                return invoicePayment.InvoicePaymentID.Value;
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
        }
        public int? GetInvoiceCaseIdRejected(int fcId)
        {
            int?  icId = -1;
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_invoice_case_rejected_get", dbConnection);
            command.Connection = dbConnection;
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    //read Invoice
                    reader.Read();
                    icId = ConvertToInt(reader["invoice_case_id"]);
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
            return icId;
        }
        public int MarkRebillableInvoceCases(string icIdList, string updateUser)
        {
            int rowCount = 0;
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_invoice_case_mark_rebillabe", dbConnection);
            var sqlParam = new SqlParameter[4];

            sqlParam[0] = new SqlParameter("@pi_ic_id_list", icIdList);
            sqlParam[1] = new SqlParameter("@pi_change_app_name", HPFConfigurationSettings.HPF_APPLICATION_NAME);
            sqlParam[2] = new SqlParameter("@pi_change_user_id", updateUser);
            sqlParam[3] = new SqlParameter("@po_row_count", SqlDbType.Int) { Direction = ParameterDirection.Output };

            try
            {
                command.Parameters.AddRange(sqlParam);
                dbConnection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                rowCount = ConvertToInt(sqlParam[3].Value).Value;
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return rowCount;
        }
    }
}
