using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common;

namespace HPF.FutureState.DataAccess
{
    public class InvoicePaymentDAO : BaseDAO
    {
        private static readonly InvoicePaymentDAO instance = new InvoicePaymentDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static InvoicePaymentDAO Instance
        {
            get
            {
                return instance;
            }
        }
        //public static InvoicePaymentDAO CreateInstance()
        //{
        //    return new InvoicePaymentDAO();
        //}

        protected InvoicePaymentDAO()
        {

        }
        public InvoicePaymentDTOCollection InvoicePaymentSearch(InvoiceSearchCriteriaDTO searchCriteria)
        {
            InvoicePaymentDTOCollection invoicePayments = null;
            SqlConnection dbConnection =base.CreateConnection();
            //SqlCommand command = base.CreateCommand("hpf_invoice_payment_search", dbConnection);
            SqlCommand command = new SqlCommand("hpf_invoice_payment_search",dbConnection);

            //<Parameter>   
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@pi_funding_source_id", searchCriteria.FundingSourceId);
            sqlParam[1] = new SqlParameter("@pi_start_dt", searchCriteria.PeriodStart);
            sqlParam[2] = new SqlParameter("@pi_end_dt", searchCriteria.PeriodEnd);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    invoicePayments = new InvoicePaymentDTOCollection();
                    while (reader.Read())
                    {
                        InvoicePaymentDTO invoicePayment = new InvoicePaymentDTO();
                        invoicePayment.FundingSourceName = ConvertToString(reader["funding_source_name"]);
                        invoicePayment.FundingSourceID = ConvertToString(reader["funding_source_id"]);
                        invoicePayment.InvoicePaymentID = ConvertToInt(reader["invoice_payment_id"]);
                        invoicePayment.PaymentType = ConvertToString(reader["pmt_cd"]);
                        invoicePayment.PaymentDate = ConvertToDateTime(reader["pmt_dt"]);
                        invoicePayment.PaymentNum = ConvertToString(reader["pmt_num"]);
                        invoicePayment.PaymentAmount = ConvertToDouble(reader["pmt_amt"]);
                        invoicePayments.Add(invoicePayment);
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
            return invoicePayments;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public InvoicePaymentDTOCollection PaymentTypeGet()
        {
            InvoicePaymentDTOCollection result = HPFCacheManager.Instance.GetData<InvoicePaymentDTOCollection>(Constant.HPF_CACHE_PAYMENT_TYPE);
            if (result == null)
            {
                result = new InvoicePaymentDTOCollection();
                var dbConnection = CreateConnection();
                var command = new SqlCommand("hpf_payment_type_get", dbConnection);
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
                            var item = new InvoicePaymentDTO();
                            item.PaymentType = ConvertToString(reader["pmt_cd"]);
                            result.Add(item);
                        }
                        reader.Close();
                    }
                    HPFCacheManager.Instance.Add(Constant.HPF_CACHE_PAYMENT_TYPE, result);
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
        public InvoicePaymentDTO InvoicePaymentGet(int invoicePaymentId)
        {
            SqlConnection dbConnection = base.CreateConnection();
            //SqlCommand command = base.CreateCommand("hpf_invoice_payment_search", dbConnection);
            SqlCommand command = CreateSPCommand("hpf_invoice_payment_get", dbConnection);

            //<Parameter>   
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_invoice_payment_id", invoicePaymentId);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            InvoicePaymentDTO invoicePayment = null;
            try
            {
                dbConnection.Open();
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    invoicePayment = new InvoicePaymentDTO();
                    while (reader.Read())
                    {
                        invoicePayment.FundingSourceName = ConvertToString(reader["funding_source_name"]);
                        invoicePayment.FundingSourceID = ConvertToString(reader["funding_source_id"]);
                        invoicePayment.InvoicePaymentID = ConvertToInt(reader["invoice_payment_id"]);
                        invoicePayment.PaymentType = ConvertToString(reader["pmt_cd"]);
                        invoicePayment.PaymentDate = ConvertToDateTime(reader["pmt_dt"]);
                        invoicePayment.PaymentNum = ConvertToString(reader["pmt_num"]);
                        invoicePayment.PaymentAmount = ConvertToDouble(reader["pmt_amt"]);
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
            return invoicePayment;
        }
    }
}
