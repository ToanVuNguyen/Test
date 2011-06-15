using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using System.Data;
using System.Data.SqlClient;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    public class PostModInclusionDAO:BaseDAO
    {
        protected PostModInclusionDAO() { }
        public static PostModInclusionDAO CreateInstance()
        {
            return new PostModInclusionDAO();
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

        public void InsertPostModInclusion(PostModInclusionDTO record)
        {
            var command = CreateSPCommand("hpf_post_mod_inclusion_insert", dbConnection);
            var sqlParam = new SqlParameter[49];
            try
            {

                sqlParam[0] = new SqlParameter("@pi_fannie_mae_loan_num", record.FannieMaeLoanNum);
                sqlParam[1] = new SqlParameter("@pi_referral_dt", record.ReferallDt);
                sqlParam[2] = new SqlParameter("@pi_servicer_name", record.ServicerName);
                sqlParam[3] = new SqlParameter("@pi_servicer_id", record.ServicerId);
                sqlParam[4] = new SqlParameter("@pi_fannie_mae_agency", record.FannieMaeAgency);
                sqlParam[5] = new SqlParameter("@pi_backlog_ind", record.BackLogInd);
                sqlParam[6] = new SqlParameter("@pi_trial_start_dt", NullableDateTime(record.TrialStartDt));
                sqlParam[7] = new SqlParameter("@pi_mod_conversion_dt", NullableDateTime(record.ModConversionDt));
                sqlParam[8] = new SqlParameter("@pi_acct_num", record.AcctNum);
                sqlParam[9] = new SqlParameter("@pi_ach_flag", record.AchFlag);
                sqlParam[10] = new SqlParameter("@pi_trial_mod_pmt_amt", record.TrialModPmtAmt);
                sqlParam[11] = new SqlParameter("@pi_next_pmt_due_dt", NullableDateTime(record.NextPmtDueDt));
                sqlParam[12] = new SqlParameter("@pi_last_pmt_applied_dt", NullableDateTime(record.LastPmtAppliedDt));
                sqlParam[13] = new SqlParameter("@pi_unpaid_principal_bal_amt", record.UnpaidPrincipalBalAmt);
                sqlParam[14] = new SqlParameter("@pi_dflt_reason", record.DefaultReason);
                sqlParam[15] = new SqlParameter("@pi_spanish_ind", record.SpanishInd);
                sqlParam[16] = new SqlParameter("@pi_borrower_fname", record.BorrowerFName);
                sqlParam[17] = new SqlParameter("@pi_borrower_lname", record.BorrowerLName);
                sqlParam[18] = new SqlParameter("@pi_co_borrower_fname", record.CoBorrowerFName);
                sqlParam[19] = new SqlParameter("@pi_co_borrower_lname", record.CoBorrowerLName);
                sqlParam[20] = new SqlParameter("@pi_prop_addr1", record.PropAddr1);
                sqlParam[21] = new SqlParameter("@pi_prop_addr2", record.PropAddr2);
                sqlParam[22] = new SqlParameter("@pi_prop_city", record.PropCity);
                sqlParam[23] = new SqlParameter("@pi_prop_state_cd", record.PropStateCd);
                sqlParam[24] = new SqlParameter("@pi_prop_zip", record.PropZip);
                sqlParam[25] = new SqlParameter("@pi_contact_addr1", record.ContactAddr1);
                sqlParam[26] = new SqlParameter("@pi_contact_addr2", record.ContactAddr2);
                sqlParam[27] = new SqlParameter("@pi_contact_city", record.ContactCity);
                sqlParam[28] = new SqlParameter("@pi_contact_zip", record.ContactZip);
                sqlParam[29] = new SqlParameter("@pi_borrower_home_contact_no", record.BorrowerHomeContactNo);
                sqlParam[30] = new SqlParameter("@pi_borrower_office1_contact_no", record.BorrowerOffice1ContactNo);
                sqlParam[31] = new SqlParameter("@pi_borrower_office2_contact_no", record.BorrowerOffice2ContactNo);
                sqlParam[32] = new SqlParameter("@pi_borrower_other_contact_no", record.BorrowerOtherContactNo);
                sqlParam[33] = new SqlParameter("@pi_borrower_cell1_contact_no", record.BorrowerOffice1ContactNo);
                sqlParam[34] = new SqlParameter("@pi_borrower_cell2_contact_no", record.BorrowerOffice2ContactNo);
                sqlParam[35] = new SqlParameter("@pi_borrower_email", record.BorrowerEmail);
                sqlParam[36] = new SqlParameter("@pi_co_borrower_home_contact_no", record.CoBorrowerHomeContactNo);
                sqlParam[37] = new SqlParameter("@pi_co_borrower_office1_contact_no", record.CoBorrowerOffice1ContactNo);
                sqlParam[38] = new SqlParameter("@pi_co_borrower_office2_contact_no", record.CoBorrowerOffice2ContactNo);
                sqlParam[39] = new SqlParameter("@pi_co_borrower_other_contact_no", record.CoBorrowerOtherContactNo);
                sqlParam[40] = new SqlParameter("@pi_co_borrower_cell1_contact_no", record.CoBorrowerCell1ContactNo);
                sqlParam[41] = new SqlParameter("@pi_co_borrower_cell2_contact_no", record.CoBorrowerCell2ContactNo);
                sqlParam[42] = new SqlParameter("@pi_co_borrower_email", record.CoBorrowerEmail);
                sqlParam[43] = new SqlParameter("@pi_servicer_file_name", record.ServicerFileName);
                sqlParam[44] = new SqlParameter("@pi_agency_id", record.AgencyId);
                sqlParam[45] = new SqlParameter("@pi_agency_file_name", record.AgencyFileName);
                sqlParam[46] = new SqlParameter("@pi_agency_file_dt", NullableDateTime(record.AgencyFileDt));
                
                sqlParam[47] = new SqlParameter("@pi_create_user_id", record.CreateUserId);
                sqlParam[48] = new SqlParameter("@pi_create_app_name", record.CreateAppName);

                command.Parameters.AddRange(sqlParam);
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
    }
}
