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
    public class ForeClosureCaseSetDAO : BaseDAO
    {
        public SqlConnection dbConnection;

        public SqlTransaction trans;

        protected ForeClosureCaseSetDAO()
        {

        }

        public static ForeClosureCaseSetDAO CreateInstance()
        {
            return new ForeClosureCaseSetDAO();
        }

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
            trans.Commit();
            dbConnection.Close();
        }
        /// <summary>
        /// Cancel work
        /// </summary>
        public void Cancel()
        {
            trans.Rollback();
            dbConnection.Close();
        }

        /// <summary>
        /// Get ForeClosure
        /// </summary>
        /// <param name="foreClosureCase"></param>
        /// <returns>ForeClosureCaseDTO if it exists, oherwise: null</returns>
        public ForeClosureCaseDTO GetExistingForeClosureCase(ForeClosureCaseDTO foreClosureCase)
        {
            return null;
        }

        public void InsertForeClosureCase(ForeClosureCaseDTO foreClosureCase)
        {
            try
            {
                var command = CreateSPCommand("USPForeClosureCaseInsert", dbConnection, trans);
                //<Parameter>
                var sqlParam = new SqlParameter[109];
                //sqlParam[0] = new SqlParameter("@agency_id", int);
                //sqlParam[1] = new SqlParameter("@call_id", int);
                //sqlParam[2] = new SqlParameter("@program_id", int);
                //sqlParam[3] = new SqlParameter("@agency_case_num", string);
                //sqlParam[4] = new SqlParameter("@agency_client_num", string);
                //sqlParam[5] = new SqlParameter("@intake_dt", DateTime);
                //sqlParam[6] = new SqlParameter("@income_earners_cd", string);
                //sqlParam[7] = new SqlParameter("@race_cd", string);
                //sqlParam[8] = new SqlParameter("@household_cd", string);
                //sqlParam[9] = new SqlParameter("@never_bill_reason_cd", string);
                //sqlParam[10] = new SqlParameter("@never_pay_reason_cd", string);
                //sqlParam[11] = new SqlParameter("@dflt_reason_1st_cd", string);
                //sqlParam[12] = new SqlParameter("@dflt_reason_2nd_cd", string);
                //sqlParam[13] = new SqlParameter("@hud_termination_reason_cd", string);
                //sqlParam[14] = new SqlParameter("@hud_termination_dt", DateTime);
                //sqlParam[15] = new SqlParameter("@hud_outcome_cd", string);
                //sqlParam[16] = new SqlParameter("@AMI_percentage", int);
                //sqlParam[17] = new SqlParameter("@counseling_duration_cd", string);
                //sqlParam[18] = new SqlParameter("@gender_cd", string);
                //sqlParam[19] = new SqlParameter("@borrower_fname", string);
                //sqlParam[20] = new SqlParameter("@borrower_lname", string);
                //sqlParam[21] = new SqlParameter("@borrower_mname", string);
                //sqlParam[22] = new SqlParameter("@mother_maiden_lname", string);
                //sqlParam[23] = new SqlParameter("@borrower_ssn", string);
                //sqlParam[24] = new SqlParameter("@borrower_last4_SSN", string);
                //sqlParam[25] = new SqlParameter("@borrower_DOB", DateTime);
                //sqlParam[26] = new SqlParameter("@co_borrower_fname", string);
                //sqlParam[27] = new SqlParameter("@co_borrower_lname", string);
                //sqlParam[28] = new SqlParameter("@co_borrower_mname", string);
                //sqlParam[29] = new SqlParameter("@co_borrower_ssn", string);
                //sqlParam[30] = new SqlParameter("@co_borrower_last4_SSN", string);
                //sqlParam[31] = new SqlParameter("@co_borrower_DOB", DateTime);
                //sqlParam[32] = new SqlParameter("@primary_contact_no", string);
                //sqlParam[33] = new SqlParameter("@second_contact_no", string);
                //sqlParam[34] = new SqlParameter("@email_1", string);
                //sqlParam[35] = new SqlParameter("@contact_zip_plus4", string);
                //sqlParam[36] = new SqlParameter("@email_2", string);
                //sqlParam[37] = new SqlParameter("@contact_addr1", string);
                //sqlParam[38] = new SqlParameter("@contact_addr2", string);
                //sqlParam[39] = new SqlParameter("@contact_city", string);
                //sqlParam[40] = new SqlParameter("@contact_state", string);
                //sqlParam[41] = new SqlParameter("@contact_zip", string);
                //sqlParam[42] = new SqlParameter("@prop_addr1", string);
                //sqlParam[43] = new SqlParameter("@prop_addr2", string);
                //sqlParam[44] = new SqlParameter("@prop_city", string);
                //sqlParam[45] = new SqlParameter("@prop_state", string);
                //sqlParam[46] = new SqlParameter("@prop_zip", string);
                //sqlParam[47] = new SqlParameter("@prop_zip_plus_4", string);
                //sqlParam[48] = new SqlParameter("@bankruptcy_ind", string);
                //sqlParam[49] = new SqlParameter("@bankruptcy_attorney", string);
                //sqlParam[50] = new SqlParameter("@bankruptcy_pmt_current_ind", string);
                //sqlParam[51] = new SqlParameter("@borrower_educ_level_completed_cd", string);
                //sqlParam[52] = new SqlParameter("@borrower_marital_status_cd", string);
                //sqlParam[53] = new SqlParameter("@borrower_preferred_lang_cd", string);
                //sqlParam[54] = new SqlParameter("@borrower_occupation", string);
                //sqlParam[55] = new SqlParameter("@co_borrower_occupation", string);
                //sqlParam[56] = new SqlParameter("@hispanic_ind", string);
                //sqlParam[57] = new SqlParameter("@duplicate_ind", string);
                //sqlParam[58] = new SqlParameter("@fc_notice_receive_ind", string);
                //sqlParam[59] = new SqlParameter("@case_complete_ind", string);
                //sqlParam[60] = new SqlParameter("@completed_dt", DateTime);
                //sqlParam[61] = new SqlParameter("@funding_consent_ind", string);
                //sqlParam[62] = new SqlParameter("@servicer_consent_ind", string);
                //sqlParam[63] = new SqlParameter("@agency_media_consent_ind", string);
                //sqlParam[64] = new SqlParameter("@hpf_media_candidate_ind", string);
                //sqlParam[65] = new SqlParameter("@hpf_network_candidate_ind", string);
                //sqlParam[66] = new SqlParameter("@hpf_success_story_ind", string);
                //sqlParam[67] = new SqlParameter("@agency_success_story_ind", string);
                //sqlParam[68] = new SqlParameter("@borrower_disabled_ind", string);
                //sqlParam[69] = new SqlParameter("@co_borrower_disabled_ind", string);
                //sqlParam[70] = new SqlParameter("@summary_sent_other_cd", string);
                //sqlParam[71] = new SqlParameter("@summary_sent_other_dt", DateTime);
                //sqlParam[72] = new SqlParameter("@summary_sent_dt", DateTime);
                //sqlParam[73] = new SqlParameter("@occupant_num", byte);
                //sqlParam[74] = new SqlParameter("@loan_dflt_reason_notes", string);
                //sqlParam[75] = new SqlParameter("@action_items_notes", string);
                //sqlParam[76] = new SqlParameter("@followup_notes", string);
                //sqlParam[77] = new SqlParameter("@prim_res_est_mkt_value", decimal);
                //sqlParam[78] = new SqlParameter("@counselor_id_ref", string);
                //sqlParam[79] = new SqlParameter("@counselor_full_name", string);
                //sqlParam[80] = new SqlParameter("@counselor_email", string);
                //sqlParam[81] = new SqlParameter("@counselor_phone", string);
                //sqlParam[82] = new SqlParameter("@counselor_ext", string);
                //sqlParam[83] = new SqlParameter("@discussed_solution_with_srvcr_ind", string);
                //sqlParam[84] = new SqlParameter("@worked_with_another_agency_ind", string);
                //sqlParam[85] = new SqlParameter("@contacted_srvcr_recently_ind", string);
                //sqlParam[86] = new SqlParameter("@has_workout_plan_ind", string);
                //sqlParam[87] = new SqlParameter("@srvcr_workout_plan_current_ind", string);
                //sqlParam[88] = new SqlParameter("@fc_sale_date_set_ind", string);
                //sqlParam[89] = new SqlParameter("@opt_out_newsletter_ind", string);
                //sqlParam[90] = new SqlParameter("@opt_out_survey_ind", string);
                //sqlParam[91] = new SqlParameter("@do_not_call_ind", string);
                //sqlParam[92] = new SqlParameter("@owner_occupied_ind", string);
                //sqlParam[93] = new SqlParameter("@primary_residence_ind", string);
                //sqlParam[94] = new SqlParameter("@realty_company", string);
                //sqlParam[95] = new SqlParameter("@property_cd", string);
                //sqlParam[96] = new SqlParameter("@for_sale_ind", string);
                //sqlParam[97] = new SqlParameter("@home_sale_price", decimal);
                //sqlParam[98] = new SqlParameter("@home_purchase_year", int);
                //sqlParam[99] = new SqlParameter("@home_purchase_price", decimal);
                //sqlParam[100] = new SqlParameter("@home_current_market_value", decimal);
                //sqlParam[101] = new SqlParameter("@military_service_cd", string);
                //sqlParam[102] = new SqlParameter("@create_dt", DateTime);
                //sqlParam[103] = new SqlParameter("@create_user_id", string);
                //sqlParam[104] = new SqlParameter("@create_app_name", string);
                //sqlParam[105] = new SqlParameter("@chg_lst_dt", DateTime);
                //sqlParam[106] = new SqlParameter("@chg_lst_user_id", string);
                //sqlParam[107] = new SqlParameter("@chg_lst_app_name", string);
                //sqlParam[108] = new SqlParameter("@fc_id", SqlDbType.Int){Direction = ParameterDirection.Output};
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                //
                command.ExecuteNonQuery();
                foreClosureCase.FcId = ConvertToInt(sqlParam[108].Value);//@fc_id	
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
                    
        }

        public CaseLoanDTO GetExistingCaseLoan(CaseLoanDTO caseLoan)
        {
            return null;
        }

        public void InsertCaseLoan(CaseLoanDTO caseLoan)
        {

        }

        public void UpdateCaseLoan(CaseLoanDTO caseLoan)
        {

        }

        /// <summary>
        /// return all search results retrieved from database
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public ForeClosureCaseSearchResult SearchForeClosureCase(ForeClosureCaseSearchCriteriaDTO searchCriteria)
        {           
            ForeClosureCaseSearchResult results = null;

            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_foreclosure_case_search", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[6];
             	
            sqlParam[0] = new SqlParameter("@agency_case_num", searchCriteria.AgencyCaseNumber );
            sqlParam[1] = new SqlParameter("@borrower_fname", searchCriteria.FirstName);
            sqlParam[2] = new SqlParameter("@borrower_lname", searchCriteria.LastName);
            sqlParam[3] = new SqlParameter("@borrower_last4_SSN", searchCriteria.Last4_SSN);
            sqlParam[4] = new SqlParameter("@prop_zip", searchCriteria.PropertyZip );
            sqlParam[5] = new SqlParameter("@loan_number", searchCriteria.LoanNumber);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new ForeClosureCaseSearchResult();
                    while (reader.Read())
                    {
                        ForeClosureCaseWSDTO item = new ForeClosureCaseWSDTO();
                        //item.Counseled = ConvertToString(reader["counseled"]);
                        item.FcId = ConvertToInt(reader["fc_id"]);
                        item.IntakeDt = ConvertToDateTime(reader["intake_dt"]);
                        item.BorrowerFname = ConvertToString(reader["borrower_fname"]);
                        item.BorrowerLname = ConvertToString(reader["borrower_lname"]);
                        item.BorrowerLast4SSN= ConvertToString(reader["borrower_last4_SSN"]);
                        item.CoBorrowerFname = ConvertToString(reader["co_borrower_fname"]);
                        item.CoBorrowerLname = ConvertToString(reader["co_borrower_lname"]);
                        item.CoBorrowerLast4SSN = ConvertToString(reader["co_borrower_last4_SSN"]);
                        item.PropAddr1 = ConvertToString(reader["prop_addr1"]);
                        item.PropAddr2 = ConvertToString(reader["prop_addr2"]);
                        item.PropCity = ConvertToString(reader["prop_city"]);
                        item.PropStateCd = ConvertToString(reader["prop_state_cd"]);
                        item.PropZip = ConvertToString(reader["prop_zip"]);
                        item.AgencyName = ConvertToString(reader["agency_name"]);
                        item.CounselorFullName = ConvertToString(reader["counselor_full_name"]);
                        item.CounselorPhone = ConvertToString(reader["counselor_phone"]);
                        item.CounselorExt = ConvertToString(reader["counselor_ext"]);
                        item.CounselorEmail = ConvertToString(reader["counselor_email"]);
                        item.CompletedDt = ConvertToDateTime(reader["completed_dt"]);
                        //item.DelinquentDt = ConvertToDateTime(reader["Delinquent_dt"]);
                        item.BankruptcyInd = ConvertToString(reader["bankruptcy_ind"]);
                        item.FcNoticeReceivedInd = ConvertToString(reader["fc_notice_received_ind"]);
                        item.LoanNumber = ConvertToString(reader["loan_number"]);
                        item.LoanServicer = ConvertToString(reader["loan_servicer"]);
                        item.AgencyCaseNum = ConvertToString(reader["agency_case_num"]);
                        item.CaseLoanID = ConvertToString(reader["case_loan_id"]);


                        results.Add(item);
                        
                    }
                    reader.Close();
                }
                dbConnection.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }            
            return results;
        }
    }
}

