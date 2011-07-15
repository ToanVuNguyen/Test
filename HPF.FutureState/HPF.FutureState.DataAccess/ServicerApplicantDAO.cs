using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using HPF.FutureState.Common.DataTransferObjects;
using System.Data;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    public class ServicerApplicantDAO:BaseDAO
    {
        protected ServicerApplicantDAO() { }
        public static ServicerApplicantDAO CreateInstance()
        {
            return new ServicerApplicantDAO();
        }
        public SqlConnection dbConnection;
        public SqlTransaction trans;
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
            try
            {
                trans.Commit();
                dbConnection.Close();
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// Cancel work
        /// </summary>
        public void Cancel()
        {
            try
            {
                trans.Rollback();
                dbConnection.Close();
            }
            catch (Exception)
            {

            }
        }

        public void InsertServicerApplicant(ServicerApplicantDTO record)
        {
            var command = CreateSPCommand("hpf_servicer_applicant_insert", dbConnection);
            var sqlParam = new SqlParameter[27];
            try
            {

                sqlParam[0] = new SqlParameter("@pi_servicer_id", record.ServicerId);
                sqlParam[1] = new SqlParameter("@pi_acct_num", record.AcctNum);
                sqlParam[2] = new SqlParameter("@pi_borrower_fname", NullableString(record.BorrowerFName));
                sqlParam[3] = new SqlParameter("@pi_borrower_lname", NullableString(record.BorrowerLName));
                sqlParam[4] = new SqlParameter("@pi_co_borrower_fname",NullableString(record.CoBorrowerFName));
                sqlParam[5] = new SqlParameter("@pi_co_borrower_lname", NullableString(record.CoBorrowerLName));
                sqlParam[6] = new SqlParameter("@pi_prop_addr1", NullableString(record.PropAddr1));
                sqlParam[7] = new SqlParameter("@pi_prop_addr2", NullableString(record.PropAddr2));
                sqlParam[8] = new SqlParameter("@pi_prop_city", NullableString(record.PropCity));
                sqlParam[9] = new SqlParameter("@pi_prop_state", NullableString(record.PropStateCd));
                sqlParam[10] = new SqlParameter("@pi_prop_zip", NullableString(record.PropZip));
                sqlParam[11] = new SqlParameter("@pi_mail_addr1", NullableString(record.MailAddr1));
                sqlParam[12] = new SqlParameter("@pi_mail_addr2", NullableString(record.MailAddr2));
                sqlParam[13] = new SqlParameter("@pi_mail_city", NullableString(record.MailCity));
                sqlParam[14] = new SqlParameter("@pi_mail_state", NullableString(record.MailStateCd));
                sqlParam[15] = new SqlParameter("@pi_mail_zip", NullableString(record.MailZip));
                sqlParam[16] = new SqlParameter("@pi_home_phone", NullableString(record.HomePhone));
                sqlParam[17] = new SqlParameter("@pi_work_phone", NullableString(record.WorkPhone));
                sqlParam[18] = new SqlParameter("@pi_email_addr", NullableString(record.EmailAddr));
                sqlParam[19] = new SqlParameter("@pi_mortgage_program_cd", NullableString(record.MortgageProgramCd));
                sqlParam[20] = new SqlParameter("@pi_scheduled_close_dt", NullableDateTime(record.ScheduledCloseDt));
                sqlParam[21] = new SqlParameter("@pi_acceptance_method_cd", NullableString(record.AcceptanceMethodCd));
                sqlParam[22] = new SqlParameter("@pi_comments", NullableString(record.Comments));
                sqlParam[23] = new SqlParameter("@pi_servicer_file_name", record.ServicerFileName);
                
                
                sqlParam[24] = new SqlParameter("@pi_create_user_id", record.CreateUserId);
                sqlParam[25] = new SqlParameter("@pi_create_app_name", record.CreateAppName);
                sqlParam[26] = new SqlParameter("@po_servicer_applicant_id", SqlDbType.Int) { Direction = ParameterDirection.Output }; 
                command.Parameters.AddRange(sqlParam);
                command.Transaction = trans;
                command.ExecuteNonQuery();
                record.ServicerApplicantId = ConvertToInt(sqlParam[26].Value);
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }

        public void InsertApplicant(ApplicantDTO record)
        {
            var command = CreateSPCommand("hpf_applicant_insert", dbConnection);
            var sqlParam = new SqlParameter[23];
            try
            {

                sqlParam[0] = new SqlParameter("@pi_servicer_applicant_id", record.ServicerApplicantId);
                sqlParam[1] = new SqlParameter("@pi_program_id", record.ProgramId);
                sqlParam[2] = new SqlParameter("@pi_group_cd", record.GroupCd);
                sqlParam[3] = new SqlParameter("@pi_assigned_to_group_dt", record.AssignedToGroupDt);
                sqlParam[4] = new SqlParameter("@pi_sent_to_agency_id", record.SentToAgencyId);
                sqlParam[5] = new SqlParameter("@pi_sent_to_agency_dt", NullableDateTime(record.SentToAgencyDt));
                sqlParam[6] = new SqlParameter("@pi_sent_to_surveyor_dt", NullableDateTime(record.SentToSurveyorDt));
                sqlParam[7] = new SqlParameter("@pi_right_party_contact_ind", record.RightPartyContactInd);
                sqlParam[8] = new SqlParameter("@pi_rpc_most_recent_dt", NullableDateTime(record.RpcMostRecentDt));
                sqlParam[9] = new SqlParameter("@pi_no_rpc_reason", record.NoRpcReason);
                sqlParam[10] = new SqlParameter("@pi_counseling_accepted_dt", NullableDateTime(record.CounselingAcceptedDt));
                sqlParam[11] = new SqlParameter("@pi_counseling_scheduled_dt", NullableDateTime(record.CounselingScheduledDt));
                sqlParam[12] = new SqlParameter("@pi_counseling_completed_dt", NullableDateTime(record.CounselingCompletedDt));
                sqlParam[13] = new SqlParameter("@pi_counseling_refused_dt", NullableDateTime(record.CounselingRefusedDt));
                sqlParam[14] = new SqlParameter("@pi_first_counseled_dt", NullableDateTime(record.FirstCounseledDt));
                sqlParam[15] = new SqlParameter("@pi_second_counseled_dt", NullableDateTime(record.SecondCounseledDt));
                sqlParam[16] = new SqlParameter("@pi_ed_module_completed_dt", NullableDateTime(record.EdModuleCompletedDt));
                sqlParam[17] = new SqlParameter("@pi_inbound_call_to_num_dt", NullableDateTime(record.InboundCallToNumDt));
                sqlParam[18] = new SqlParameter("@pi_inbound_call_to_num_reason", record.InboundCallToNumReason);
                sqlParam[19] = new SqlParameter("@pi_actual_close_dt", NullableDateTime(record.ActualCloseDt));
                
                sqlParam[20] = new SqlParameter("@pi_create_user_id", record.CreateUserId);
                sqlParam[21] = new SqlParameter("@pi_create_app_name", record.CreateAppName);
                sqlParam[22] = new SqlParameter("@po_applicant_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
                command.Parameters.AddRange(sqlParam);
                command.Transaction = trans;
                command.ExecuteNonQuery();
                record.ApplicantId = ConvertToInt(sqlParam[22].Value);
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
    }
}
