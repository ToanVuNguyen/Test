using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using System.Data;

namespace HPF.FutureState.DataAccess
{
    public class CaseFollowUpDAO : BaseDAO
    {
        private static readonly CaseFollowUpDAO instance = new CaseFollowUpDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CaseFollowUpDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected CaseFollowUpDAO()
        {
        }

        public bool SaveCaseFollowUp(CaseFollowUpDTO caseFollowUp, bool isUpdated)
        {
            bool bReturn = false;
            var dbConnection=CreateConnection();
            var command = CreateSPCommand("hpf_case_post_counseling_status_update", dbConnection);
            if (!isUpdated)
            {
                command = CreateSPCommand("hpf_case_post_counseling_status_insert", dbConnection);
                command.Parameters.Add(new SqlParameter("@po_case_post_counseling_status_id", SqlDbType.Int) { Direction = ParameterDirection.Output });
                command.Parameters.Add(new SqlParameter("@pi_create_dt", caseFollowUp.CreateDate));
                command.Parameters.Add(new SqlParameter("@pi_create_user_id", caseFollowUp.CreateUserId));
                command.Parameters.Add(new SqlParameter("@pi_create_app_name", caseFollowUp.CreateAppName));
            }
            if (isUpdated)
            {
                command.Parameters.Add(new SqlParameter("@pi_case_post_counseling_status_id", caseFollowUp.CasePostCounselingStatusId));
            }
            command.Parameters.Add(new SqlParameter("@pi_fc_id", caseFollowUp.FcId));
            command.Parameters.Add(new SqlParameter("@pi_followup_dt", caseFollowUp.FollowUpDt));
            command.Parameters.Add(new SqlParameter("@pi_followup_comment", caseFollowUp.FollowUpComment));
            command.Parameters.Add(new SqlParameter("@pi_followup_source_cd", caseFollowUp.FollowUpSourceCd));
            command.Parameters.Add(new SqlParameter("@pi_loan_delinq_status_cd", caseFollowUp.LoanDelinqStatusCd));
            command.Parameters.Add(new SqlParameter("@pi_still_in_house_ind", caseFollowUp.StillInHouseInd));
            command.Parameters.Add(new SqlParameter("@pi_credit_score", caseFollowUp.CreditScore));
            command.Parameters.Add(new SqlParameter("@pi_credit_bureau_cd", caseFollowUp.CreditBureauCd));
            command.Parameters.Add(new SqlParameter("@pi_credit_report_dt", caseFollowUp.CreditReportDt));
            command.Parameters.Add(new SqlParameter("@pi_outcome_type_id", caseFollowUp.OutcomeTypeId));
            command.Parameters.Add(new SqlParameter("@pi_chg_lst_dt", caseFollowUp.ChangeLastDate));
            command.Parameters.Add(new SqlParameter("@pi_chg_lst_user_id", caseFollowUp.ChangeLastUserId));
            command.Parameters.Add(new SqlParameter("@pi_chg_lst_app_name", caseFollowUp.ChangeLastAppName));
            
            try
            {
                dbConnection.Open();
                command.ExecuteNonQuery();
                bReturn = true;
            }
            catch (Exception Ex)
            {
                bReturn = false;
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);                
            }
            finally
            {
                dbConnection.Close();
            }
            return bReturn;
        }

        public CaseFollowUpDTOCollection GetCaseFollowUp(int fcId)
        {
            CaseFollowUpDTOCollection result = new CaseFollowUpDTOCollection();
            SqlConnection dbConnection = CreateConnection();
            SqlCommand command = CreateSPCommand("hpf_case_post_counseling_status_get", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);            
            command.Parameters.AddRange(sqlParam);
            //</Parameter>
            try
            {
                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CaseFollowUpDTO caseFollowUp = new CaseFollowUpDTO();

                    caseFollowUp.FcId = ConvertToInt(reader["fc_id"]);
                    caseFollowUp.CasePostCounselingStatusId = ConvertToInt(reader["case_post_counseling_status_id"]);
                    caseFollowUp.FollowUpDt = ConvertToDateTime(reader["followup_dt"]);
                    caseFollowUp.FollowUpComment = ConvertToString(reader["followup_comment"]);
                    caseFollowUp.FollowUpSourceCd = ConvertToString(reader["followup_source_cd"]);
                    caseFollowUp.FollowUpSourceCdDesc = ConvertToString(reader["followup_source_cd_desc"]);
                    caseFollowUp.LoanDelinqStatusCd = ConvertToString(reader["loan_delinq_status_cd"]);
                    caseFollowUp.LoanDelinqStatusCdDesc = ConvertToString(reader["loan_delinq_status_cd_desc"]);
                    caseFollowUp.StillInHouseInd = ConvertToString(reader["still_in_house_ind"]);
                    caseFollowUp.CreditScore = ConvertToString(reader["credit_score"]);
                    caseFollowUp.CreditBureauCd = ConvertToString(reader["credit_bureau_cd"]);
                    caseFollowUp.CreditBureauCdDesc = ConvertToString(reader["credit_bureau_cd_desc"]);
                    caseFollowUp.CreditReportDt = ConvertToDateTime(reader["credit_report_dt"]);
                    caseFollowUp.OutcomeTypeId = ConvertToInt(reader["outcome_type_id"]);
                    caseFollowUp.OutcomeTypeName = ConvertToString(reader["outcome_type_name"]);

                    result.Add(caseFollowUp);

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

            return result;
        }

    }
}
