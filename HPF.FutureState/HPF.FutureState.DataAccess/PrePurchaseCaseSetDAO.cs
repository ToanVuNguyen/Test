using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using System.Data.SqlClient;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common;
using System.Data;

namespace HPF.FutureState.DataAccess
{
    public class PrePurchaseCaseSetDAO : BaseDAO
    {
        public SqlConnection dbConnection;
        public SqlTransaction trans;

        protected PrePurchaseCaseSetDAO() { }
        public static PrePurchaseCaseSetDAO CreateInstance()
        {
            return new PrePurchaseCaseSetDAO();
        }
        #region Manage Transaction
        /// <summary>
        /// Begin Transaction
        /// </summary>
        public void Begin()
        {
            dbConnection = CreateConnection();
            dbConnection.Open();
            trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }
        /// <summary>
        /// Commit Working
        /// </summary>
        public void Commit()
        {
            try
            {
                trans.Commit();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        /// <summary>
        /// Rollback working
        /// </summary>
        public void Rollback()
        {
            try
            {
                trans.Rollback();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        public void CloseConnection()
        {
            try
            {
                dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        #endregion

        public int? InsertPrePurchaseCase(PrePurchaseCaseDTO prePurchaseCase)
        {
            var command = CreateSPCommand("hpf_pre_purchase_case_insert", this.dbConnection);
            try
            {
                var sqlParam = new SqlParameter[50];

                sqlParam[0] = new SqlParameter("@pi_pp_borrower_id",prePurchaseCase.PPBorrowerId);
                sqlParam[1] = new SqlParameter("@pi_program_id",prePurchaseCase.ProgramId);
                sqlParam[2] = new SqlParameter("@pi_group_cd",NullableString(prePurchaseCase.GroupCd));
                sqlParam[3] = new SqlParameter("@pi_assigned_to_group_dt",NullableDateTime(prePurchaseCase.AssignedToGroupDt));
                sqlParam[4] = new SqlParameter("@pi_agency_id",prePurchaseCase.AgencyId);
                sqlParam[5] = new SqlParameter("@pi_surveyor_id",prePurchaseCase.SurveyorId);
                sqlParam[6] = new SqlParameter("@pi_sent_to_surveyor_dt",NullableDateTime(prePurchaseCase.SentToSurveyorDt));
                sqlParam[7] = new SqlParameter("@pi_right_party_contact_ind",NullableString(prePurchaseCase.RightPartyContactInd));
                sqlParam[8] = new SqlParameter("@pi_rpc_most_recent_dt",NullableDateTime(prePurchaseCase.RpcMostRecentDt));
                sqlParam[9] = new SqlParameter("@pi_no_rpc_reason",NullableString(prePurchaseCase.NoRpcReason));
                sqlParam[10] = new SqlParameter("@pi_counseling_accepted_dt",NullableDateTime(prePurchaseCase.CounselingAcceptedDt));
                sqlParam[11] = new SqlParameter("@pi_counseling_schedule_dt",NullableDateTime(prePurchaseCase.CounselingScheduledDt));
                sqlParam[12] = new SqlParameter("@pi_counseling_completed_dt",NullableDateTime(prePurchaseCase.CounselingCompletedDt));
                sqlParam[13] = new SqlParameter("@pi_counseling_refused_dt",NullableDateTime(prePurchaseCase.CounselingRefusedDt));
                sqlParam[14] = new SqlParameter("@pi_first_counseled_dt",NullableDateTime(prePurchaseCase.FirstCounseledDt));
                sqlParam[15] = new SqlParameter("@pi_second_counseled_dt",NullableDateTime(prePurchaseCase.SecondCounseledDt));
                sqlParam[16] = new SqlParameter("@pi_inbound_call_to_num_dt",NullableDateTime(prePurchaseCase.InboundCallToNumDt));
                sqlParam[17] = new SqlParameter("@pi_inbound_call_to_num_reason",NullableString(prePurchaseCase.InboundCallToNumReason));
                sqlParam[18] = new SqlParameter("@pi_agency_case_num",NullableString(prePurchaseCase.AgencyCaseNum));
                sqlParam[19] = new SqlParameter("@pi_new_email_addr1",NullableString(prePurchaseCase.NewMailAddr1));
                sqlParam[20] = new SqlParameter("@pi_new_email_addr2",NullableString(prePurchaseCase.NewMailAddr2));
                sqlParam[21] = new SqlParameter("@pi_new_mail_city",NullableString(prePurchaseCase.NewMailCity));
                sqlParam[22] = new SqlParameter("@pi_new_mail_state_cd",NullableString(prePurchaseCase.NewMailStateCd));
                sqlParam[23] = new SqlParameter("@pi_new_mail_zip",NullableString(prePurchaseCase.NewMailZip));
                sqlParam[24] = new SqlParameter("@pi_borrower_authorization_ind",NullableString(prePurchaseCase.BorrowerAuthorizationInd));
                sqlParam[25] = new SqlParameter("@pi_mother_maiden_lname",NullableString(prePurchaseCase.MotherMaidenLName));
                sqlParam[26] = new SqlParameter("@pi_primary_contact_no",NullableString(prePurchaseCase.PrimaryContactNo));
                sqlParam[27] = new SqlParameter("@pi_secondary_contact_no",NullableString(prePurchaseCase.SecondaryContactNo));
                sqlParam[28] = new SqlParameter("@pi_borrower_employer_name",NullableString(prePurchaseCase.BorrowerEmployerName));
                sqlParam[29] = new SqlParameter("@pi_borrower_job_title",NullableString(prePurchaseCase.BorrowerJobTitle));
                sqlParam[30] = new SqlParameter("@pi_borrower_self_employed_ind",NullableString(prePurchaseCase.BorrowerSelfEmployedInd));
                sqlParam[31] = new SqlParameter("@pi_borrower_years_employed",prePurchaseCase.BorrowerYearsEmployed);
                sqlParam[32] = new SqlParameter("@pi_co_borrower_employer_name",NullableString(prePurchaseCase.CoBorrowerEmployerName));
                sqlParam[33] = new SqlParameter("@pi_co_borrower_job_title",NullableString(prePurchaseCase.CoBorrowerJobTitle));
                sqlParam[34] = new SqlParameter("@pi_co_borrower_self_employed_ind",NullableString(prePurchaseCase.CoBorrowerSelfEmployedInd));
                sqlParam[35] = new SqlParameter("@pi_co_borrower_years_employed",prePurchaseCase.CoBorrowerYearsEmployed);
                sqlParam[36] = new SqlParameter("@pi_counselor_id_ref",NullableString(prePurchaseCase.CounselorIdRef));
                sqlParam[37] = new SqlParameter("@pi_counselor_fname",NullableString(prePurchaseCase.CounselorFName));
                sqlParam[38] = new SqlParameter("@pi_counselor_lname",NullableString(prePurchaseCase.CounselorLName));
                sqlParam[39] = new SqlParameter("@pi_counselor_email",NullableString(prePurchaseCase.CounselorEmail));
                sqlParam[40] = new SqlParameter("@pi_counselor_phone",NullableString(prePurchaseCase.CounselorPhone));
                sqlParam[41] = new SqlParameter("@pi_counselor_ext",NullableString(prePurchaseCase.CounselorExt));
                sqlParam[42] = new SqlParameter("@pi_counseling_duration_mins",prePurchaseCase.CounselingDurationMins);
                sqlParam[43] = new SqlParameter("@pi_create_dt",prePurchaseCase.CreateDate);
                sqlParam[44] = new SqlParameter("@pi_create_user_id",prePurchaseCase.CreateUserId);
                sqlParam[45] = new SqlParameter("@pi_create_app_name",prePurchaseCase.CreateAppName);
                sqlParam[46] = new SqlParameter("@pi_chg_lst_dt",prePurchaseCase.ChangeLastDate);
                sqlParam[47] = new SqlParameter("@pi_chg_lst_user_id",prePurchaseCase.ChangeLastUserId);
                sqlParam[48] = new SqlParameter("@pi_chg_lst_app_name",prePurchaseCase.ChangeLastAppName);

                sqlParam[49] = new SqlParameter("@po_pp_case_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
                //</Parameter> 
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                command.ExecuteNonQuery();
                prePurchaseCase.PPCaseId = ConvertToInt(sqlParam[118].Value);
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                command.Dispose();
            }
            return prePurchaseCase.PPCaseId;
        }

    }
}
