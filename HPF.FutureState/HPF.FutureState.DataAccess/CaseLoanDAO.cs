using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    public class CaseLoanDAO:BaseDAO
    {
        private static readonly CaseLoanDAO instance = new CaseLoanDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CaseLoanDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected CaseLoanDAO()
        {
            
        }

        public CaseLoanDTOCollection ReadCaseLoan(int fcId)
        {
            CaseLoanDTOCollection results = null;
            var dbConnection = CreateConnection();
            var command = new SqlCommand("hpf_case_loan_get", dbConnection);
            //<Parameter>            
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
            //</Parameter>   
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;            
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new CaseLoanDTOCollection();
                    while (reader.Read())
                    {
                        #region setValue
                        CaseLoanDTO item = new CaseLoanDTO();
                        item.CaseLoanId = ConvertToInt(reader["case_loan_id"]);
                        item.FcId = ConvertToInt(reader["fc_id"]);
                        item.ServicerId = ConvertToInt(reader["servicer_id"]);
                        item.ServicerName = ConvertToString(reader["servicer_name"]);
                        item.OtherServicerName = ConvertToString(reader["other_servicer_name"]);
                        item.AcctNum = ConvertToString(reader["acct_num"]);
                        item.Loan1st2nd = ConvertToString(reader["loan_1st_2nd_cd"]);
                        item.MortgageTypeCd = ConvertToString(reader["mortgage_type_cd"]);
                        item.ArmResetInd = ConvertToString(reader["arm_reset_ind"]);
                        item.LoanDelinqStatusCd = ConvertToString(reader["loan_delinq_status_cd"]);
                        item.CurrentLoanBalanceAmt = ConvertToDouble(reader["current_loan_balance_amt"]);
                        item.OrigLoanAmt = ConvertToDouble(reader["orig_loan_amt"]);
                        item.InterestRate = ConvertToDouble(reader["interest_rate"]);
                        item.OriginatingLenderName = ConvertToString(reader["originating_lender_name"]);
                        item.OrigMortgageCoFdicNcusNum = ConvertToString(reader["orig_mortgage_co_FDIC_NCUA_num"]);
                        item.OrigMortgageCoName = ConvertToString(reader["orig_mortgage_co_name"]);
                        item.OrginalLoanNum = ConvertToString(reader["orginal_loan_num"]);
                        item.FdicNcusNumCurrentServicerTbd = ConvertToString(reader["current_servicer_FDIC_NCUA_num"]);                        
                        item.InvestorLoanNum = ConvertToString(reader["investor_loan_num"]);
                        item.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                        item.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                        item.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                        results.Add(item);
                        #endregion
                    }
                }
                reader.Close();                    
            }
            catch (Exception Ex)
            {
                dbConnection.Close();
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
