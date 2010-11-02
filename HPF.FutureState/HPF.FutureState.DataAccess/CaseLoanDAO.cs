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

        /// <summary>
        /// Select all CAse Loan from database by Fc_ID. 
        /// </summary>
        /// <param name=""></param>
        /// <returns>OutcomeItemDTOCollection</returns>
        public CaseLoanDTOCollection GetCaseLoanCollection(int? fcId)
        {
            CaseLoanDTOCollection results = null; // HPFCacheManager.Instance.GetData<CaseLoanDTOCollection>(Constant.HPF_CACHE_CASE_LOAN);            
            if (results == null)
            {
                var dbConnection = CreateConnection();
                var command = new SqlCommand("hpf_case_loan_get", dbConnection);
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
                        results = new CaseLoanDTOCollection();
                        while (reader.Read())
                        {
                            CaseLoanDTO item = new CaseLoanDTO();
                            item.CaseLoanId = ConvertToInt(reader["case_loan_id"]);
                            item.FcId = ConvertToInt(reader["fc_id"]);
                            item.ServicerId = ConvertToInt(reader["servicer_id"]);
                            item.ServicerName = ConvertToString(reader["servicer_name"]);
                            item.OtherServicerName = ConvertToString(reader["other_servicer_name"]);
                            item.AcctNum = ConvertToString(reader["acct_num"]);
                            item.Loan1st2nd = ConvertToString(reader["loan_1st_2nd_cd"]);
                            item.MortgageTypeCd = ConvertToString(reader["mortgage_type_cd"]);
                            item.MortgageTypeCdDesc = ConvertToString(reader["mortgage_type_code_desc"]);
                            item.MortgageProgramCdDesc = ConvertToString(reader["mortgage_program_code_desc"]);
                            item.ArmResetInd = ConvertToString(reader["arm_reset_ind"]);
                            item.LoanDelinqStatusCd = ConvertToString(reader["loan_delinq_status_cd"]);
                            item.LoanDelinquencyDesc = ConvertToString(reader["delinq_code_desc"]);
                            item.TermLengthDesc = ConvertToString(reader["term_code_desc"]);
                            item.CurrentLoanBalanceAmt = ConvertToDouble(reader["current_loan_balance_amt"]);
                            item.OrigLoanAmt = ConvertToDouble(reader["orig_loan_amt"]);
                            item.InterestRate = ConvertToDouble(reader["interest_rate"]);
                            item.OriginatingLenderName = ConvertToString(reader["originating_lender_name"]);
                            item.OrigMortgageCoFdicNcusNum = ConvertToString(reader["orig_mortgage_co_FDIC_NCUA_num"]);
                            item.OrigMortgageCoName = ConvertToString(reader["orig_mortgage_co_name"]);
                            item.OrginalLoanNum = ConvertToString(reader["orginal_loan_num"]);
                            item.CurrentServicerFdicNcuaNum = ConvertToString(reader["current_servicer_FDIC_NCUA_num"]);
                            item.InvestorLoanNum = ConvertToString(reader["investor_loan_num"]);
                            item.InvestorNum = ConvertToString(reader["investor_num"]);
                            item.InvestorName = ConvertToString(reader["investor_name"]);
                            item.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                            item.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                            item.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);

                            item.ArmRateAdjustDt =  ConvertToDateTime(reader["arm_rate_adjust_dt"]);
			                item.ArmLockDuration = ConvertToInt(reader["arm_lock_duration"]);
			                item.LoanLookupCd = ConvertToString(reader["loan_lookup_cd"]);
			                item.ThirtyDaysLatePastYrInd = ConvertToString(reader["thirty_days_late_past_yr_ind"]);
			                item.PmtMissLessOneYrLoanInd = ConvertToString(reader["pmt_miss_less_one_yr_loan_ind"]);
			                item.SufficientIncomeInd = ConvertToString(reader["sufficient_income_ind"]);
			                item.LongTermAffordInd = ConvertToString(reader["long_term_afford_ind"]);
			                item.HarpEligibleInd = ConvertToString(reader["harp_eligible_ind"]);
			                item.OrigPriorTo2009Ind = ConvertToString(reader["orig_prior_to_2009_ind"]);
			                item.PriorHampInd = ConvertToString(reader["prior_hamp_ind"]);
			                item.PrinBalWithinLimitInd = ConvertToString(reader["prin_bal_within_limit_ind"]);
                            item.HampEligibleInd = ConvertToString(reader["hamp_eligible_ind"]);
                            item.LossMitStatusCd = ConvertToString(reader["loss_mit_status_cd"]);
                            results.Add(item);
                        }
                    }
                    reader.Close();
                    //HPFCacheManager.Instance.Add(Constant.HPF_CACHE_CASE_LOAN, results);
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
            return results;
        }
                      
    }
}
