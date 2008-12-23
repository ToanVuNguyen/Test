using System;
using System.Data;
using System.Data.SqlClient;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

namespace HPF.FutureState.DataAccess
{
    public class CaseLoanDAO : BaseDAO
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

        /// <summary>
        /// Insert a Case Loan to database.
        /// </summary>
        /// <param name="caseLoan">CaseLoanDTO</param>
        /// <returns></returns>
        public void InsertCaseLoan(CaseLoanDTO caseLoan)
        {   
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_case_loan_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[20];
            sqlParam[0] = new SqlParameter("@fc_id", caseLoan.FcId);
            sqlParam[1] = new SqlParameter("@servicer_id", caseLoan.ServicerId);
            sqlParam[2] = new SqlParameter("@other_servicer_name", caseLoan.OtherServicerName);
            sqlParam[3] = new SqlParameter("@acct_num", caseLoan.AcctNum);
            sqlParam[4] = new SqlParameter("@loan_1st_2nd", caseLoan.Loan1st2nd);
            sqlParam[5] = new SqlParameter("@mortgage_type_cd", caseLoan.MortgageTypeCd);
            sqlParam[6] = new SqlParameter("@arm_loan_ind", caseLoan.ArmLoanInd);
            sqlParam[7] = new SqlParameter("@arm_reset_ind", caseLoan.ArmResetInd);
            sqlParam[8] = new SqlParameter("@term_length_cd", caseLoan.TermLengthCd);
            sqlParam[9] = new SqlParameter("@loan_delinq_status_cd", caseLoan.LoanDelinqStatusCd);
            sqlParam[10] = new SqlParameter("@current_loan_balance_amt", caseLoan.CurrentLoanBalanceAmt);
            sqlParam[11] = new SqlParameter("@orig_loan_amt", caseLoan.OrigLoanAmt);
            sqlParam[12] = new SqlParameter("@interest_rate", caseLoan.InterestRate);
            sqlParam[13] = new SqlParameter("@Originating_Lender_Name", caseLoan.OriginatingLenderName);
            sqlParam[14] = new SqlParameter("@orig_mortgage_co_FDIC_NCUS_num", caseLoan.OrigMortgageCoFdicNcusNum);
            sqlParam[15] = new SqlParameter("@Orig_mortgage_co_name", caseLoan.OrigMortgageCoName);
            sqlParam[16] = new SqlParameter("@Orginal_Loan_Num", caseLoan.OrginalLoanNum);
            sqlParam[17] = new SqlParameter("@FDIC_NCUA_Num_current_servicer_TBD", caseLoan.FdicNcusNumCurrentServicerTbd);
            sqlParam[18] = new SqlParameter("@Current_Servicer_Name_TBD", caseLoan.CurrentServicerNameTbd);
            sqlParam[19] = new SqlParameter("@freddie_loan_num", caseLoan.FreddieLoanNum);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            dbConnection.Open();
            var trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = trans;
            try
            {
                command.ExecuteNonQuery();
                trans.Commit();
                dbConnection.Close();                
            }
            catch(Exception Ex)
            {
                trans.Rollback();
                dbConnection.Close();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }            
        }

        /// <summary>
        /// Update a Case Loan to database.
        /// </summary>
        /// <param name="caseLoan">CaseLoanDTO</param>
        /// <returns></returns>
        public void UpdateCaseLoan(CaseLoanDTO caseLoan)
        {            
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_case_loan_update", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[21];
            sqlParam[0] = new SqlParameter("@fc_id", caseLoan.FcId);
            sqlParam[1] = new SqlParameter("@servicer_id", caseLoan.ServicerId);
            sqlParam[2] = new SqlParameter("@other_servicer_name", caseLoan.OtherServicerName);
            sqlParam[3] = new SqlParameter("@acct_num", caseLoan.AcctNum);
            sqlParam[4] = new SqlParameter("@loan_1st_2nd", caseLoan.Loan1st2nd);
            sqlParam[5] = new SqlParameter("@mortgage_type_cd", caseLoan.MortgageTypeCd);
            sqlParam[6] = new SqlParameter("@arm_loan_ind", caseLoan.ArmLoanInd);
            sqlParam[7] = new SqlParameter("@arm_reset_ind", caseLoan.ArmResetInd);
            sqlParam[8] = new SqlParameter("@term_length_cd", caseLoan.TermLengthCd);
            sqlParam[9] = new SqlParameter("@loan_delinq_status_cd", caseLoan.LoanDelinqStatusCd);
            sqlParam[10] = new SqlParameter("@current_loan_balance_amt", caseLoan.CurrentLoanBalanceAmt);
            sqlParam[11] = new SqlParameter("@orig_loan_amt", caseLoan.OrigLoanAmt);
            sqlParam[12] = new SqlParameter("@interest_rate", caseLoan.InterestRate);
            sqlParam[13] = new SqlParameter("@Originating_Lender_Name", caseLoan.OriginatingLenderName);
            sqlParam[14] = new SqlParameter("@orig_mortgage_co_FDIC_NCUS_num", caseLoan.OrigMortgageCoFdicNcusNum);
            sqlParam[15] = new SqlParameter("@Orig_mortgage_co_name", caseLoan.OrigMortgageCoName);
            sqlParam[16] = new SqlParameter("@Orginal_Loan_Num", caseLoan.OrginalLoanNum);
            sqlParam[17] = new SqlParameter("@FDIC_NCUA_Num_current_servicer_TBD", caseLoan.FdicNcusNumCurrentServicerTbd);
            sqlParam[18] = new SqlParameter("@Current_Servicer_Name_TBD", caseLoan.CurrentServicerNameTbd);
            sqlParam[19] = new SqlParameter("@freddie_loan_num", caseLoan.FreddieLoanNum);
            sqlParam[20] = new SqlParameter("@case_loan_id", caseLoan.CaseLoanId);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            dbConnection.Open();
            var trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = trans;
            try
            {
                command.ExecuteNonQuery();
                trans.Commit();
                dbConnection.Close();
            }
            catch (Exception Ex)
            {
                trans.Rollback();
                dbConnection.Close();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        /// <summary>
        /// Select all CAse Loan from database by Fc_ID. 
        /// </summary>
        /// <param name=""></param>
        /// <returns>OutcomeItemDTOCollection</returns>
        public CaseLoanDTOCollection GetCaseLoanCollection(int fcId)
        {            
            CaseLoanDTOCollection results = HPFCacheManager.Instance.GetData<CaseLoanDTOCollection>("caseLoanItem");
            if (results == null)
            {
                var dbConnection = new SqlConnection(ConnectionString);
                var command = new SqlCommand("hpf_get_case_loan_list", dbConnection);
                //<Parameter>            
                var sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@fc_id", fcId);
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
                            CaseLoanDTO item = new CaseLoanDTO();
                            item.CaseLoanId = ConvertToInt(reader["case_loan_id"]);
                            item.FcId = ConvertToInt(reader["fc_id"]);
                            item.ServicerId = ConvertToInt(reader["servicer_id"]);
                            item.OtherServicerName = ConvertToString(reader["other_servicer_name"]);
                            item.AcctNum = ConvertToString(reader["acct_num"]);
                            item.Loan1st2nd = ConvertToString(reader["loan_1st_2nd_cd"]);
                            item.MortgageTypeCd = ConvertToString(reader["mortgage_type_cd"]);
                            item.ArmLoanInd = ConvertToString(reader["arm_loan_ind"]);
                            item.ArmResetInd = ConvertToString(reader["arm_reset_ind"]);
                            item.TermLengthCd = ConvertToString(reader["term_length_cd"]);
                            item.LoanDelinqStatusCd = ConvertToString(reader["loan_delinq_status_cd"]);
                            item.CurrentLoanBalanceAmt = ConvertToDecimal(reader["current_loan_balance_amt"]);
                            item.OrigLoanAmt = ConvertToDecimal(reader["orig_loan_amt"]);
                            item.InterestRate = ConvertToDecimal(reader["interest_rate"]);
                            item.OriginatingLenderName = ConvertToString(reader["originating_lender_name"]);
                            item.OrigMortgageCoFdicNcusNum = ConvertToString(reader["orig_mortgage_co_FDIC_NCUS_num"]);
                            item.OrigMortgageCoName = ConvertToString(reader["orig_mortgage_co_name"]);
                            item.OrginalLoanNum = ConvertToString(reader["orginal_loan_num"]);
                            item.FdicNcusNumCurrentServicerTbd = ConvertToString(reader["FDIC_NCUS_num_current_servicer_TBD"]);
                            item.CurrentServicerNameTbd = ConvertToString(reader["current_servicer_name_TBD"]);
                            item.FreddieLoanNum = ConvertToString(reader["freddie_loan_num"]);
                            results.Add(item);
                        }
                        reader.Close();
                        HPFCacheManager.Instance.Add("caseLoanItem", results);
                    }
                    dbConnection.Close();
                }
                catch (Exception Ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
                }
            }
            return results;
        }

        /// <summary>
        /// Update a Case Loan to database.
        /// </summary>
        /// <param name="caseLoan">CaseLoanDTO</param>
        /// <returns></returns>
        public void DeleteCaseLoan(CaseLoanDTO caseLoan)
        {
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_case_loan_delete", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@fc_id", caseLoan.FcId);
            sqlParam[1] = new SqlParameter("@servicer_id", caseLoan.ServicerId);            
            sqlParam[2] = new SqlParameter("@acct_num", caseLoan.AcctNum);            
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            dbConnection.Open();
            var trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = trans;
            try
            {
                command.ExecuteNonQuery();
                trans.Commit();
                dbConnection.Close();
            }
            catch (Exception Ex)
            {
                trans.Rollback();
                dbConnection.Close();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
