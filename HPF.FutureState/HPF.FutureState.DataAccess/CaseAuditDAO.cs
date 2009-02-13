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
            var command = CreateSPCommand("", dbConnection);
            if (isUpdated)
            {
                command = CreateSPCommand("hpf_case_audit_update", dbConnection);
                command.Parameters.Add(new SqlParameter("@pi_case_audit_id", caseAudit.CaseAuditId));
            }
            else
            {
                command = CreateSPCommand("hpf_case_audit_insert", dbConnection);
                command.Parameters.Add(new SqlParameter("@po_case_audit_id", SqlDbType.Int) { Direction = ParameterDirection.Output });                
                command.Parameters.Add(new SqlParameter("@pi_create_dt", caseAudit.CreateDate));
                command.Parameters.Add(new SqlParameter("@pi_create_user_id", caseAudit.CreateUserId));
                command.Parameters.Add(new SqlParameter("@pi_create_app_name", caseAudit.CreateAppName));
            }
            command.Parameters.Add(new SqlParameter("@pi_fc_id", caseAudit.FcId));
            command.Parameters.Add(new SqlParameter("@pi_audit_type_cd", caseAudit.AuditTypeCode));
            command.Parameters.Add(new SqlParameter("@pi_appropriate_outcome_ind", caseAudit.AppropriateOutcomeInd));
            command.Parameters.Add(new SqlParameter("@pi_appropriate_reason_for_dflt_ind", caseAudit.ReasonForDefaultInd));
            command.Parameters.Add(new SqlParameter("@pi_complete_budget_ind", caseAudit.BudgetCompletedInd));
            command.Parameters.Add(new SqlParameter("@pi_audit_dt", caseAudit.AuditDt));
            command.Parameters.Add(new SqlParameter("@pi_audit_comment", caseAudit.AuditComments));
            command.Parameters.Add(new SqlParameter("@pi_reviewed_by", caseAudit.ReviewedBy));
            command.Parameters.Add(new SqlParameter("@pi_client_action_plan_ind", caseAudit.ClientActionPlanInd));
            command.Parameters.Add(new SqlParameter("@pi_verbal_privacy_consent_ind", caseAudit.VerbalPrivacyConsentInd));
            command.Parameters.Add(new SqlParameter("@pi_written_privacy_consent_ind", caseAudit.WrittenActionConsentInd));
            command.Parameters.Add(new SqlParameter("@pi_compliant_ind", caseAudit.CompliantInd));
            command.Parameters.Add(new SqlParameter("@pi_audit_failure_reason_cd", caseAudit.AuditFailureReasonCode));
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
                    CaseAuditDTO caseAudit = new CaseAuditDTO();
                    
                    caseAudit.FcId = ConvertToInt(reader["fc_id"]);
                    caseAudit.CaseAuditId = ConvertToInt(reader["case_audit_id"]);
                    caseAudit.AppropriateOutcomeInd = ConvertToString(reader["appropriate_outcome_ind"]);
                    caseAudit.AuditComments = ConvertToString(reader["audit_comment"]);
                    caseAudit.AuditDt = ConvertToDateTime(reader["audit_dt"]);
                    caseAudit.AuditFailureReasonCode = ConvertToString(reader["audit_failure_reason_cd"]);
                    caseAudit.AuditTypeCode = ConvertToString(reader["audit_type_cd"]);
                    caseAudit.BudgetCompletedInd = ConvertToString(reader["complete_budget_ind"]);
                    caseAudit.ClientActionPlanInd = ConvertToString(reader["client_action_plan_ind"]);
                    caseAudit.CompliantInd = ConvertToString(reader["compliant_ind"]);
                    caseAudit.ReasonForDefaultInd = ConvertToString(reader["appropriate_reason_for_dflt_ind"]);
                    caseAudit.ReviewedBy = ConvertToString(reader["reviewed_by"]);
                    caseAudit.VerbalPrivacyConsentInd = ConvertToString(reader["verbal_privacy_consent_ind"]);
                    caseAudit.WrittenActionConsentInd = ConvertToString(reader["written_privacy_consent_ind"]);                   

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
