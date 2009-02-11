using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    public class CaseAuditDAO : BaseDAO
    {
        private static readonly CaseAuditDAO instance = new CaseAuditDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CaseAuditDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected CaseAuditDAO()
        {
        }

        public bool SaveCaseAudit(CaseAuditDTO caseAudit, bool isUpdated)
        {
            bool bReturn = false;
            var dbConnection=CreateConnection();
            var command = CreateSPCommand("hpf_case_audit_insert", dbConnection);
            if (isUpdated)
                command = CreateSPCommand("hpf_case_audit_update", dbConnection);
            //<Parameter>
            command.Parameters.Add(new SqlParameter("@pi_fc_id", caseAudit.FcId));
            command.Parameters.Add(new SqlParameter("@pi_appropriate_outcome_cd", caseAudit.AppropriateOutcomeInd));
            command.Parameters.Add(new SqlParameter("@pi_reason_for_default_cd", caseAudit.ReasonForDefaultInd));
            command.Parameters.Add(new SqlParameter("@pi_complete_budget_cd", caseAudit.BudgetCompletedInd));
            command.Parameters.Add(new SqlParameter("@pi_audit_dt", caseAudit.AuditDt));
            command.Parameters.Add(new SqlParameter("@pi_audit_comment", caseAudit.AuditComments));
            command.Parameters.Add(new SqlParameter("@pi_reviewed_by", caseAudit.ReviewedBy));
            command.Parameters.Add(new SqlParameter("@pi_client_action_plan_cd", caseAudit.ClientActionPlanInd));
            command.Parameters.Add(new SqlParameter("@pi_verbal_privacy_cd", caseAudit.VerbalPrivacyConsentInd));
            command.Parameters.Add(new SqlParameter("@pi_written_privacy_cd", caseAudit.WrittenActionConsentInd));
            command.Parameters.Add(new SqlParameter("@pi_compliant_cd", caseAudit.CompliantInd));
            command.Parameters.Add(new SqlParameter("@pi_audit_failure_reason_cd", caseAudit.AuditFailureReasonCode));
            command.Parameters.Add(new SqlParameter("@pi_create_dt", caseAudit.CreateDate));
            command.Parameters.Add(new SqlParameter("@pi_create_user_id", caseAudit.CreateUserId));
            command.Parameters.Add(new SqlParameter("@pi_create_app_name", caseAudit.CreateAppName));
            command.Parameters.Add(new SqlParameter("@pi_chg_lst_dt", caseAudit.ChangeLastDate));
            command.Parameters.Add(new SqlParameter("@pi_chg_lst_user_id", caseAudit.ChangeLastUserId));
            command.Parameters.Add(new SqlParameter("@pi_chg_lst_app_name", caseAudit.ChangeLastAppName));
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

        public CaseAuditDTOCollection GetCaseAudits(int fcId)
        {
            CaseAuditDTOCollection result = new CaseAuditDTOCollection();
            SqlConnection dbConnection = CreateConnection();
            SqlCommand command = CreateSPCommand("hpf_case_audit_get", dbConnection);
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
                    CaseAuditDTO caseAudit = new CaseAuditDTO()
                    {
                        FcId = ConvertToInt(reader["fc_id"]),
                        AppropriateOutcomeInd = ConvertToString(reader[""]),
                        AuditComments = ConvertToString(reader[""]),
                        AuditDt = ConvertToDateTime(reader[""]),
                        AuditFailureReasonCode = ConvertToString(reader[""]),
                        AuditTypeCode = ConvertToString(reader[""]),
                        BudgetCompletedInd = ConvertToString(reader[""]),
                        ClientActionPlanInd = ConvertToString(reader[""]),
                        CompliantInd = ConvertToString(reader[""]),
                        ReasonForDefaultInd = ConvertToString(reader[""]),
                        ReviewedBy = ConvertToString(reader[""]),
                        VerbalPrivacyConsentInd = ConvertToString(reader[""]),
                        WrittenActionConsentInd = ConvertToString(reader[""])
                    };

                    result.Add(caseAudit);

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
