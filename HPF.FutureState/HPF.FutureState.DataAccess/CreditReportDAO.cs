using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common;

namespace HPF.FutureState.DataAccess
{
    public class CreditReportDAO:BaseDAO
    {
        private static readonly CreditReportDAO instance = new CreditReportDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CreditReportDAO Instance
        {
            get { return instance; }
        }
        protected CreditReportDAO() { }

        /// <summary>
        /// Select all CreditReport from database by Fc_ID. 
        /// </summary>
        /// <param name=""></param>
        /// <returns>CreditReportDTOCollection</returns>
        public CreditReportDTOCollection GetCreditReportCollection(int? fcId)
        {
            CreditReportDTOCollection results = null;

            var dbConnection = CreateConnection();
            var command = new SqlCommand("hpf_credit_report_get", dbConnection);
            //<Parameter>            
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
            try
            {
                //</Parameter>                   
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new CreditReportDTOCollection();
                    while (reader.Read())
                    {
                        CreditReportDTO item = new CreditReportDTO();
                        item.CreditReportId = ConvertToInt(reader["credit_report_id"]);
                        item.FcId = ConvertToInt(reader["fc_id"]);
                        item.CreditPullDt = ConvertToDateTime(reader["credit_pull_dt"]);
                        item.CreditScore = ConvertToString(reader["credit_score"]);
                        item.CreditBureauCd = ConvertToString(reader["credit_bureau_cd"]);
                        item.RevolvingBal = ConvertToDouble(reader["revolving_bal"]);
                        item.RevolvingLimitAmt = ConvertToDouble(reader["revolving_limit_amt"]);
                        item.InstallmentBal = ConvertToDouble(reader["installment_bal"]);
                        item.InstallmentLimitAmt = ConvertToDouble(reader["installment_limit_amt"]);
                        results.Add(item);
                    }
                }
                reader.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return results;
        }
    }
}
