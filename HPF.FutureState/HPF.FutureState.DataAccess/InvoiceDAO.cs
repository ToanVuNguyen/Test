using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;

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
        public List<InvoiceSearchResultDTO> SearchInvoice(InvoiceSearchCriteriaDTO searchCriteria)
        {
            List<InvoiceSearchResultDTO> invoices = null;
           
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
                    invoices = new List<InvoiceSearchResultDTO>();
                    while (reader.Read())
                    {
                        InvoiceSearchResultDTO invoice = new InvoiceSearchResultDTO();
                        
                        invoice.FundingSourceId = ConvertToInt(reader["funding_source_id"]);
                        invoice.FundingSourceName = ConvertToString(reader["funding_source_name"]);
                        invoice.InvoiceBillAmt = ConvertToDecimal(reader["invoice_bill_amt"]);                                                
                        invoice.InvoiceComment = ConvertToString(reader["invoice_comment"]);                        
                        invoice.InvoiceId = ConvertToInt(reader["Invoice_id"]);
                        invoice.InvoicePeriod = ConvertToString(reader["invoice_dt"]);
                        invoice.InvoicePmtAmt = ConvertToDecimal(reader["invoice_pmt_amt"]);
                        invoice.StatusCd = ConvertToString(reader["status_cd"]);

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
    }
}
