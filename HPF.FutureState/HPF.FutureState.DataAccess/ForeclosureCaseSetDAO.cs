using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Reflection;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.Utils.Exceptions;
using System.Data.SqlTypes;

namespace HPF.FutureState.DataAccess
{
    public class ForeclosureCaseSetDAO : BaseDAO
    {
        public SqlConnection dbConnection;

        public SqlTransaction trans;


        protected ForeclosureCaseSetDAO()
        {

        }

        public static ForeclosureCaseSetDAO CreateInstance()
        {
            return new ForeclosureCaseSetDAO();
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
        /// Get Foreclosure
        /// </summary>
        /// <param name="foreClosureCase"></param>
        /// <returns>ForeclosureCaseDTO if it exists, otherwise: null</returns>
        public ForeclosureCaseDTO GetExistingForeclosureCase(ForeclosureCaseDTO foreClosureCase)
        {
            return null;
        }

        /// <summary>
        /// get Foreclosure
        /// </summary>
        /// <param name="fc_id">id of a Foreclosure</param>
        /// <returns>ForeclosureCase if exists, otherwise: null</returns>
        public ForeclosureCaseDTO GetForeclosureCase(int fcId)
        {
            ForeclosureCaseDTO returnObject = new ForeclosureCaseDTO();

            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_foreclosure_case_get_from_fcid", dbConnection);
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
                    if (reader.Read())
                    {

                        returnObject.FcId = ConvertToInt(reader["fc_id"]);

                        returnObject.ActionItemsNotes = ConvertToString(reader["action_items_notes"]);
                        returnObject.AgencyCaseNum = ConvertToString(reader["agency_case_num"]);
                        returnObject.AgencyClientNum = ConvertToString(reader["agency_client_num"]);
                        returnObject.AgencyId = ConvertToInt(reader["agency_id"]);
                        returnObject.AgencyMediaConsentInd = ConvertToString(reader["agency_media_consent_ind"]);
                        returnObject.AgencySuccessStoryInd = ConvertToString(reader["agency_success_story_ind"]);
                        returnObject.AmiPercentage = ConvertToInt(reader["AMI_percentage"]);
                        returnObject.AssignedCounselorIdRef = ConvertToString(reader["counselor_id_ref"]);

                        returnObject.BankruptcyAttorney = ConvertToString(reader["bankruptcy_attorney"]);
                        returnObject.BankruptcyInd = ConvertToString(reader["bankruptcy_ind"]);
                        returnObject.BankruptcyPmtCurrentInd = ConvertToString(reader["bankruptcy_pmt_current_ind"]);
                        returnObject.BorrowerDisabledInd = ConvertToString(reader["borrower_disabled_ind"]);
                        returnObject.BorrowerDob = ConvertToDateTime(reader["borrower_dob"]);
                        returnObject.BorrowerEducLevelCompletedCd = ConvertToString(reader["borrower_educ_level_completed_cd"]);
                        returnObject.BorrowerFname = ConvertToString(reader["borrower_fname"]);
                        returnObject.BorrowerLname = ConvertToString(reader["borrower_lname"]);
                        returnObject.BorrowerMaritalStatusCd = ConvertToString(reader["borrower_marital_status_cd"]);
                        returnObject.BorrowerLast4Ssn = ConvertToString(reader["borrower_last4_SSN"]);
                        returnObject.BorrowerMname = ConvertToString(reader["borrower_mname"]);
                        returnObject.BorrowerOccupationCd = ConvertToString(reader["borrower_occupation"]);
                        returnObject.BorrowerPreferredLangCd = ConvertToString(reader["borrower_preferred_lang_cd"]);
                        returnObject.BorrowerSsn = ConvertToString(reader["borrower_ssn"]);

                        returnObject.CallId = ConvertToInt(reader["call_id"]);
                        returnObject.CaseCompleteInd = ConvertToString(reader["case_complete_ind"]);
                        returnObject.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                        returnObject.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                        returnObject.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                        returnObject.CoBorrowerDisabledInd = ConvertToString(reader["co_borrower_disabled_ind"]);
                        returnObject.CoBorrowerDob = ConvertToDateTime(reader["co_borrower_DOB"]);
                        returnObject.CoBorrowerFname = ConvertToString(reader["co_borrower_fname"]);
                        returnObject.CoBorrowerLname = ConvertToString(reader["co_borrower_lname"]);
                        returnObject.CoBorrowerLast4Ssn = ConvertToString(reader["co_borrower_last4_SSN"]);
                        returnObject.CoBorrowerMname = ConvertToString(reader["co_borrower_mname"]);
                        returnObject.CoBorrowerOccupationCd = ConvertToString(reader["co_borrower_occupation"]);
                        returnObject.CoBorrowerSsn = ConvertToString(reader["co_borrower_ssn"]);
                        returnObject.CompletedDt = ConvertToDateTime(reader["completed_dt"]);
                        returnObject.ContactAddr1 = ConvertToString(reader["contact_addr1"]);
                        returnObject.ContactAddr2 = ConvertToString(reader["contact_addr2"]);
                        returnObject.ContactCity = ConvertToString(reader["contact_city"]);
                        returnObject.ContactedSrvcrRecentlyInd = ConvertToString(reader["contacted_srvcr_recently_ind"]);
                        returnObject.ContactStateCd = ConvertToString(reader["contact_state_cd"]);
                        returnObject.ContactZip = ConvertToString(reader["contact_zip"]);
                        returnObject.ContactZipPlus4 = ConvertToString(reader["contact_zip_plus4"]);
                        returnObject.CounselingDurationCd = ConvertToString(reader["counseling_duration_cd"]);
                        returnObject.CounselorFname = ConvertToString(reader["counselor_fname"]);
                        returnObject.CounselorLname = ConvertToString(reader["counselor_lname"]);
                        returnObject.CounselorPhone = ConvertToString(reader["counselor_phone"]);
                        returnObject.CounselorExt = ConvertToString(reader["counselor_ext"]);
                        returnObject.CounselorEmail = ConvertToString(reader["counselor_email"]);
                        returnObject.CreateAppName = ConvertToString(reader["create_app_name"]);
                        returnObject.CreateDate = ConvertToDateTime(reader["create_dt"]);
                        returnObject.CreateUserId = ConvertToString(reader["create_user_id"]);

                        returnObject.DfltReason1stCd = ConvertToString(reader["dflt_reason_1st_cd"]);
                        returnObject.DfltReason2ndCd = ConvertToString(reader["dflt_reason_2nd_cd"]);
                        returnObject.DiscussedSolutionWithSrvcrInd = ConvertToString(reader["discussed_solution_with_srvcr_ind"]);
                        returnObject.DoNotCallInd = ConvertToString(reader["do_not_call_ind"]);
                        returnObject.DuplicateInd = ConvertToString(reader["duplicate_ind"]);

                        returnObject.Email1 = ConvertToString(reader["email_1"]);
                        returnObject.Email2 = ConvertToString(reader["email_2"]);

                        returnObject.FcSaleDateSetInd = ConvertToString(reader["fc_sale_date_set_ind"]);
                        returnObject.FcNoticeReceiveInd = ConvertToString(reader["fc_notice_received_ind"]);
                        returnObject.FollowupNotes = ConvertToString(reader["followup_notes"]);
                        returnObject.ForSaleInd = ConvertToString(reader["for_sale_ind"]);
                        returnObject.FundingConsentInd = ConvertToString(reader["funding_consent_ind"]);

                        returnObject.GenderCd = ConvertToString(reader["gender_cd"]);

                        returnObject.HasWorkoutPlanInd = ConvertToString(reader["has_workout_plan_ind"]);
                        returnObject.HispanicInd = ConvertToString(reader["hispanic_ind"]);
                        returnObject.HomeCurrentMarketValue = ConvertToDecimal(reader["home_current_market_value"]);
                        returnObject.HomePurchasePrice = ConvertToDecimal(reader["home_purchase_price"]);
                        returnObject.HomePurchaseYear = ConvertToInt(reader["home_purchase_year"]);
                        returnObject.HomeSalePrice = ConvertToDecimal(reader["home_sale_price"]);
                        returnObject.HouseholdCd = ConvertToString(reader["household_cd"]);
                        returnObject.HpfMediaCandidateInd = ConvertToString(reader["hpf_media_candidate_ind"]);
                        returnObject.HpfNetworkCandidateInd = ConvertToString(reader["hpf_network_candidate_ind"]);
                        returnObject.HpfSuccessStoryInd = ConvertToString(reader["hpf_success_story_ind"]);
                        returnObject.HudOutcomeCd = ConvertToString(reader["hud_outcome_cd"]);
                        returnObject.HudTerminationDt = ConvertToDateTime(reader["hud_termination_dt"]);
                        returnObject.HudTerminationReasonCd = ConvertToString(reader["hud_termination_reason_cd"]);

                        returnObject.IncomeEarnersCd = ConvertToString(reader["income_earners_cd"]);
                        returnObject.IntakeDt = ConvertToDateTime(reader["intake_dt"]);

                        returnObject.LoanDfltReasonNotes = ConvertToString(reader["loan_dflt_reason_notes"]);

                        returnObject.MilitaryServiceCd = ConvertToString(reader["military_service_cd"]);
                        returnObject.MotherMaidenLname = ConvertToString(reader["mother_maiden_lname"]);

                        returnObject.NeverBillReasonCd = ConvertToString(reader["never_bill_reason_cd"]);
                        returnObject.NeverPayReasonCd = ConvertToString(reader["never_pay_reason_cd"]);

                        returnObject.OccupantNum = ConvertToByte(reader["occupant_num"]);
                        returnObject.OptOutNewsletterInd = ConvertToString(reader["opt_out_newsletter_ind"]);
                        returnObject.OptOutSurveyInd = ConvertToString(reader["opt_out_survey_ind"]);
                        returnObject.OwnerOccupiedInd = ConvertToString(reader["owner_occupied_ind"]);

                        returnObject.PrimaryContactNo = ConvertToString(reader["primary_contact_no"]);
                        returnObject.PrimaryResidenceInd = ConvertToString(reader["primary_residence_ind"]);
                        returnObject.PrimResEstMktValue = ConvertToDecimal(reader["prim_res_est_mkt_value"]);
                        returnObject.ProgramId = ConvertToInt(reader["program_id"]);
                        returnObject.PropAddr1 = ConvertToString(reader["prop_addr1"]);
                        returnObject.PropAddr2 = ConvertToString(reader["prop_addr2"]);
                        returnObject.PropCity = ConvertToString(reader["prop_city"]);
                        returnObject.PropStateCd = ConvertToString(reader["prop_state_cd"]);
                        returnObject.PropZip = ConvertToString(reader["prop_zip"]);
                        returnObject.PropertyCd = ConvertToString(reader["property_cd"]);
                        returnObject.PropZipPlus4 = ConvertToString(reader["prop_zip_plus_4"]);

                        returnObject.RaceCd = ConvertToString(reader["race_cd"]);
                        returnObject.RealtyCompany = ConvertToString(reader["realty_company"]);
                        
                        returnObject.SecondContactNo = ConvertToString(reader["second_contact_no"]);
                        returnObject.ServicerConsentInd = ConvertToString(reader["servicer_consent_ind"]);
                        returnObject.SrvcrWorkoutPlanCurrentInd = ConvertToString(reader["srvcr_workout_plan_current_ind"]);
                        returnObject.SummarySentDt = ConvertToDateTime(reader["summary_sent_dt"]);
                        returnObject.SummarySentOtherCd = ConvertToString(reader["summary_sent_other_cd"]);
                        returnObject.SummarySentOtherDt = ConvertToDateTime(reader["summary_sent_other_dt"]);

                        returnObject.WorkedWithAnotherAgencyInd = ConvertToString(reader["worked_with_another_agency_ind"]);
                    }
                    reader.Close();
                }
                else
                    returnObject = null;
                dbConnection.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            return returnObject;

        }

        /// <summary>
        /// Insert a ForeclosureCase to database.
        /// </summary>
        /// <param name="foreclosureCase">ForeclosureCase</param>
        /// <returns>a new Fc_id</returns>
        public int InsertForeclosureCase(ForeclosureCaseDTO foreclosureCase)
        {
            var command = CreateSPCommand("hpf_foreclosure_case_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[114];
                sqlParam[0] = new SqlParameter("@pi_agency_id", foreclosureCase.AgencyId);
                sqlParam[1] = new SqlParameter("@pi_completed_dt", NullableDateTime(foreclosureCase.CompletedDt));
                sqlParam[2] = new SqlParameter("@pi_call_id", foreclosureCase.CallId);
                sqlParam[3] = new SqlParameter("@pi_program_id", foreclosureCase.ProgramId);
                sqlParam[4] = new SqlParameter("@pi_agency_case_num", foreclosureCase.AgencyCaseNum);
                sqlParam[5] = new SqlParameter("@pi_agency_client_num", foreclosureCase.AgencyClientNum);
                sqlParam[6] = new SqlParameter("@pi_intake_dt", foreclosureCase.IntakeDt);
                sqlParam[7] = new SqlParameter("@pi_income_earners_cd", foreclosureCase.IncomeEarnersCd);
                sqlParam[8] = new SqlParameter("@pi_case_source_cd", foreclosureCase.CaseSourceCd);
                sqlParam[9] = new SqlParameter("@pi_race_cd", foreclosureCase.RaceCd);
                sqlParam[10] = new SqlParameter("@pi_household_cd", foreclosureCase.HouseholdCd);
                sqlParam[11] = new SqlParameter("@pi_never_bill_reason_cd", foreclosureCase.NeverBillReasonCd);
                sqlParam[12] = new SqlParameter("@pi_never_pay_reason_cd", foreclosureCase.NeverPayReasonCd);
                sqlParam[13] = new SqlParameter("@pi_dflt_reason_1st_cd", foreclosureCase.DfltReason1stCd);
                sqlParam[14] = new SqlParameter("@pi_dflt_reason_2nd_cd", foreclosureCase.DfltReason2ndCd);
                sqlParam[15] = new SqlParameter("@pi_hud_termination_reason_cd", foreclosureCase.HudTerminationReasonCd);
                sqlParam[16] = new SqlParameter("@pi_hud_termination_dt", NullableDateTime(foreclosureCase.HudTerminationDt));
                sqlParam[17] = new SqlParameter("@pi_hud_outcome_cd", foreclosureCase.HudOutcomeCd);
                sqlParam[18] = new SqlParameter("@pi_AMI_percentage", foreclosureCase.AmiPercentage);
                sqlParam[19] = new SqlParameter("@pi_counseling_duration_cd", foreclosureCase.CounselingDurationCd);
                sqlParam[20] = new SqlParameter("@pi_gender_cd", foreclosureCase.GenderCd);
                sqlParam[21] = new SqlParameter("@pi_borrower_fname", foreclosureCase.BorrowerFname);
                sqlParam[22] = new SqlParameter("@pi_borrower_lname", foreclosureCase.BorrowerLname);
                sqlParam[23] = new SqlParameter("@pi_borrower_mname", foreclosureCase.BorrowerMname);
                sqlParam[24] = new SqlParameter("@pi_mother_maiden_lname", foreclosureCase.MotherMaidenLname);
                sqlParam[25] = new SqlParameter("@pi_borrower_ssn", foreclosureCase.BorrowerSsn);
                sqlParam[26] = new SqlParameter("@pi_borrower_last4_SSN", foreclosureCase.BorrowerLast4Ssn);
                sqlParam[27] = new SqlParameter("@pi_borrower_DOB", NullableDateTime(foreclosureCase.BorrowerDob));
                sqlParam[28] = new SqlParameter("@pi_co_borrower_fname", foreclosureCase.CoBorrowerFname);
                sqlParam[29] = new SqlParameter("@pi_co_borrower_lname", foreclosureCase.CoBorrowerLname);
                sqlParam[30] = new SqlParameter("@pi_co_borrower_mname", foreclosureCase.CoBorrowerMname);
                sqlParam[31] = new SqlParameter("@pi_co_borrower_ssn", foreclosureCase.CoBorrowerSsn);
                sqlParam[32] = new SqlParameter("@pi_co_borrower_last4_SSN", foreclosureCase.CoBorrowerLast4Ssn);
                sqlParam[33] = new SqlParameter("@pi_co_borrower_DOB", NullableDateTime(foreclosureCase.CoBorrowerDob));
                sqlParam[34] = new SqlParameter("@pi_primary_contact_no", foreclosureCase.PrimaryContactNo);
                sqlParam[35] = new SqlParameter("@pi_second_contact_no", foreclosureCase.SecondContactNo);
                sqlParam[36] = new SqlParameter("@pi_email_1", foreclosureCase.Email1);
                sqlParam[37] = new SqlParameter("@pi_email_2", foreclosureCase.Email2);
                sqlParam[38] = new SqlParameter("@pi_contact_addr1", foreclosureCase.ContactAddr1);
                sqlParam[39] = new SqlParameter("@pi_contact_addr2", foreclosureCase.ContactAddr2);
                sqlParam[40] = new SqlParameter("@pi_contact_city", foreclosureCase.ContactCity);
                sqlParam[41] = new SqlParameter("@pi_contact_state_cd", foreclosureCase.ContactStateCd);
                sqlParam[42] = new SqlParameter("@pi_contact_zip", foreclosureCase.ContactZip);
                sqlParam[43] = new SqlParameter("@pi_contact_zip_plus4", foreclosureCase.ContactZipPlus4);
                sqlParam[44] = new SqlParameter("@pi_prop_addr1", foreclosureCase.PropAddr1);
                sqlParam[45] = new SqlParameter("@pi_prop_addr2", foreclosureCase.PropAddr2);
                sqlParam[46] = new SqlParameter("@pi_prop_city", foreclosureCase.PropCity);
                sqlParam[47] = new SqlParameter("@pi_prop_state_cd", foreclosureCase.PropStateCd);
                sqlParam[48] = new SqlParameter("@pi_prop_zip", foreclosureCase.PropZip);
                sqlParam[49] = new SqlParameter("@pi_prop_zip_plus_4", foreclosureCase.PropZipPlus4);
                sqlParam[50] = new SqlParameter("@pi_bankruptcy_ind", foreclosureCase.BankruptcyInd);
                sqlParam[51] = new SqlParameter("@pi_bankruptcy_attorney", foreclosureCase.BankruptcyAttorney);
                sqlParam[52] = new SqlParameter("@pi_bankruptcy_pmt_current_ind", foreclosureCase.BankruptcyPmtCurrentInd);
                sqlParam[53] = new SqlParameter("@pi_borrower_educ_level_completed_cd", foreclosureCase.BorrowerEducLevelCompletedCd);
                sqlParam[54] = new SqlParameter("@pi_borrower_marital_status_cd", foreclosureCase.BorrowerMaritalStatusCd);
                sqlParam[55] = new SqlParameter("@pi_borrower_preferred_lang_cd", foreclosureCase.BorrowerPreferredLangCd);
                sqlParam[56] = new SqlParameter("@pi_borrower_occupation", foreclosureCase.BorrowerOccupationCd);
                sqlParam[57] = new SqlParameter("@pi_co_borrower_occupation", foreclosureCase.CoBorrowerOccupationCd);
                sqlParam[58] = new SqlParameter("@pi_owner_occupied_ind", foreclosureCase.OwnerOccupiedInd);
                sqlParam[59] = new SqlParameter("@pi_hispanic_ind", foreclosureCase.HispanicInd);
                sqlParam[60] = new SqlParameter("@pi_duplicate_ind", foreclosureCase.DuplicateInd);
                sqlParam[61] = new SqlParameter("@pi_fc_notice_received_ind", foreclosureCase.FcNoticeReceiveInd);
                sqlParam[62] = new SqlParameter("@pi_case_complete_ind", foreclosureCase.CaseCompleteInd);
                sqlParam[63] = new SqlParameter("@pi_funding_consent_ind", foreclosureCase.FundingConsentInd);
                sqlParam[64] = new SqlParameter("@pi_servicer_consent_ind", foreclosureCase.ServicerConsentInd);
                sqlParam[65] = new SqlParameter("@pi_agency_media_consent_ind", foreclosureCase.AgencyMediaConsentInd);
                sqlParam[66] = new SqlParameter("@pi_hpf_media_candidate_ind", foreclosureCase.HpfMediaCandidateInd);
                sqlParam[67] = new SqlParameter("@pi_hpf_network_candidate_ind", foreclosureCase.HpfNetworkCandidateInd);
                sqlParam[68] = new SqlParameter("@pi_hpf_success_story_ind", foreclosureCase.HpfSuccessStoryInd);
                sqlParam[69] = new SqlParameter("@pi_agency_success_story_ind", foreclosureCase.AgencySuccessStoryInd);
                sqlParam[70] = new SqlParameter("@pi_borrower_disabled_ind", foreclosureCase.BorrowerDisabledInd);
                sqlParam[71] = new SqlParameter("@pi_co_borrower_disabled_ind", foreclosureCase.CoBorrowerDisabledInd);
                sqlParam[72] = new SqlParameter("@pi_summary_sent_other_cd", foreclosureCase.SummarySentOtherCd);
                sqlParam[73] = new SqlParameter("@pi_summary_sent_other_dt", NullableDateTime(foreclosureCase.SummarySentOtherDt));
                sqlParam[74] = new SqlParameter("@pi_summary_sent_dt", NullableDateTime(foreclosureCase.SummarySentDt));
                sqlParam[75] = new SqlParameter("@pi_occupant_num", foreclosureCase.OccupantNum);
                sqlParam[76] = new SqlParameter("@pi_loan_dflt_reason_notes", foreclosureCase.LoanDfltReasonNotes);
                sqlParam[77] = new SqlParameter("@pi_action_items_notes", foreclosureCase.ActionItemsNotes);
                sqlParam[78] = new SqlParameter("@pi_followup_notes", foreclosureCase.FollowupNotes);
                sqlParam[79] = new SqlParameter("@pi_prim_res_est_mkt_value", foreclosureCase.PrimResEstMktValue);
                sqlParam[80] = new SqlParameter("@pi_counselor_id_ref", foreclosureCase.AssignedCounselorIdRef);
                sqlParam[81] = new SqlParameter("@pi_counselor_lname", foreclosureCase.CounselorFname);
                sqlParam[82] = new SqlParameter("@pi_counselor_fname", foreclosureCase.CounselorLname);
                sqlParam[83] = new SqlParameter("@pi_counselor_email", foreclosureCase.CounselorEmail);
                sqlParam[84] = new SqlParameter("@pi_counselor_phone", foreclosureCase.CounselorPhone);
                sqlParam[85] = new SqlParameter("@pi_counselor_ext", foreclosureCase.CounselorExt);
                sqlParam[86] = new SqlParameter("@pi_discussed_solution_with_srvcr_ind", foreclosureCase.DiscussedSolutionWithSrvcrInd);
                sqlParam[87] = new SqlParameter("@pi_worked_with_another_agency_ind", foreclosureCase.WorkedWithAnotherAgencyInd);
                sqlParam[88] = new SqlParameter("@pi_contacted_srvcr_recently_ind", foreclosureCase.ContactedSrvcrRecentlyInd);
                sqlParam[89] = new SqlParameter("@pi_has_workout_plan_ind", foreclosureCase.HasWorkoutPlanInd);
                sqlParam[90] = new SqlParameter("@pi_srvcr_workout_plan_current_ind", foreclosureCase.SrvcrWorkoutPlanCurrentInd);
                sqlParam[91] = new SqlParameter("@pi_fc_sale_date_set_ind", foreclosureCase.FcSaleDateSetInd);
                sqlParam[92] = new SqlParameter("@pi_opt_out_newsletter_ind", foreclosureCase.OptOutNewsletterInd);
                sqlParam[93] = new SqlParameter("@pi_opt_out_survey_ind", foreclosureCase.OptOutSurveyInd);
                sqlParam[94] = new SqlParameter("@pi_do_not_call_ind", foreclosureCase.DoNotCallInd);
                sqlParam[95] = new SqlParameter("@pi_primary_residence_ind", foreclosureCase.PrimaryResidenceInd);
                sqlParam[96] = new SqlParameter("@pi_realty_company", foreclosureCase.RealtyCompany);
                sqlParam[97] = new SqlParameter("@pi_property_cd", foreclosureCase.PropertyCd);
                sqlParam[98] = new SqlParameter("@pi_for_sale_ind", foreclosureCase.ForSaleInd);
                sqlParam[99] = new SqlParameter("@pi_home_sale_price", foreclosureCase.HomeSalePrice);
                sqlParam[100] = new SqlParameter("@pi_home_purchase_year", foreclosureCase.HomePurchaseYear);
                sqlParam[101] = new SqlParameter("@pi_home_purchase_price", foreclosureCase.HomePurchasePrice);
                sqlParam[102] = new SqlParameter("@pi_Home_Current_Market_Value", foreclosureCase.HomeCurrentMarketValue);
                sqlParam[103] = new SqlParameter("@pi_military_service_cd", foreclosureCase.MilitaryServiceCd);
                sqlParam[104] = new SqlParameter("@pi_household_gross_annual_income_amt", foreclosureCase.HouseholdGrossAnnualIncomeAmt);                
                sqlParam[105] = new SqlParameter("@pi_intake_credit_score", foreclosureCase.IntakeCreditScore);
                sqlParam[106] = new SqlParameter("@pi_Intake_credit_bureau_cd ", foreclosureCase.IntakeCreditBureauCd);
                sqlParam[107] = new SqlParameter("@pi_create_dt", NullableDateTime(foreclosureCase.CreateDate));
                sqlParam[108] = new SqlParameter("@pi_create_user_id", foreclosureCase.CreateUserId);
                sqlParam[109] = new SqlParameter("@pi_create_app_name", foreclosureCase.CreateAppName);
                sqlParam[110] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(foreclosureCase.ChangeLastDate));
                sqlParam[111] = new SqlParameter("@pi_chg_lst_user_id", foreclosureCase.ChangeLastUserId);
                sqlParam[112] = new SqlParameter("@pi_chg_lst_app_name", foreclosureCase.ChangeLastAppName);
                sqlParam[113] = new SqlParameter("@po_fc_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
                //</Parameter>            
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;            
                command.Transaction = this.trans;            
                command.ExecuteNonQuery();                
                foreclosureCase.FcId = ConvertToInt(sqlParam[113].Value);
            }
            catch (Exception Ex)
            {                
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }            
            return foreclosureCase.FcId;
        }

        public CaseLoanDTO GetExistingCaseLoan(CaseLoanDTO caseLoan)
        {
            return null;
        }

        /// <summary>
        /// Insert a Case Loan to database.
        /// </summary>
        /// <param name="caseLoan">CaseLoanDTO</param>
        /// <returns></returns>
        public void InsertCaseLoan(CaseLoanDTO caseLoan, int fcId)
        {            
            var command = CreateCommand("hpf_case_loan_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[26];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                sqlParam[1] = new SqlParameter("@pi_servicer_id", caseLoan.ServicerId);
                sqlParam[2] = new SqlParameter("@pi_other_servicer_name", caseLoan.OtherServicerName);
                sqlParam[3] = new SqlParameter("@pi_acct_num", caseLoan.AcctNum);
                sqlParam[4] = new SqlParameter("@pi_loan_1st_2nd_cd", caseLoan.Loan1st2nd);
                sqlParam[5] = new SqlParameter("@pi_mortgage_type_cd", caseLoan.MortgageTypeCd);
                sqlParam[6] = new SqlParameter("@pi_arm_loan_ind", caseLoan.ArmLoanInd);
                sqlParam[7] = new SqlParameter("@pi_arm_reset_ind", caseLoan.ArmResetInd);
                sqlParam[8] = new SqlParameter("@pi_term_length_cd", caseLoan.TermLengthCd);
                sqlParam[9] = new SqlParameter("@pi_loan_delinq_status_cd", caseLoan.LoanDelinqStatusCd);
                sqlParam[10] = new SqlParameter("@pi_current_loan_balance_amt", caseLoan.CurrentLoanBalanceAmt);
                sqlParam[11] = new SqlParameter("@pi_orig_loan_amt", caseLoan.OrigLoanAmt);
                sqlParam[12] = new SqlParameter("@pi_interest_rate", caseLoan.InterestRate);
                sqlParam[13] = new SqlParameter("@pi_Originating_Lender_Name", caseLoan.OriginatingLenderName);
                sqlParam[14] = new SqlParameter("@pi_orig_mortgage_co_FDIC_NCUS_num", caseLoan.OrigMortgageCoFdicNcusNum);
                sqlParam[15] = new SqlParameter("@pi_Orig_mortgage_co_name", caseLoan.OrigMortgageCoName);
                sqlParam[16] = new SqlParameter("@pi_Orginal_Loan_Num", caseLoan.OrginalLoanNum);
                sqlParam[17] = new SqlParameter("@pi_FDIC_NCUS_Num_current_servicer_TBD", caseLoan.FdicNcusNumCurrentServicerTbd);
                sqlParam[18] = new SqlParameter("@pi_Current_Servicer_Name_TBD", caseLoan.CurrentServicerNameTbd);
                sqlParam[19] = new SqlParameter("@pi_freddie_loan_num", caseLoan.FreddieLoanNum);
                sqlParam[20] = new SqlParameter("@pi_create_dt", caseLoan.CreateDate);
                sqlParam[21] = new SqlParameter("@pi_create_user_id", caseLoan.CreateUserId);
                sqlParam[22] = new SqlParameter("@pi_create_app_name", caseLoan.CreateAppName);
                sqlParam[23] = new SqlParameter("@pi_chg_lst_dt", caseLoan.ChangeLastDate);
                sqlParam[24] = new SqlParameter("@pi_chg_lst_user_id", caseLoan.ChangeLastUserId);
                sqlParam[25] = new SqlParameter("@pi_chg_lst_app_name", caseLoan.ChangeLastAppName);
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;            
                command.Transaction = this.trans;            
                command.ExecuteNonQuery();                
            }
            catch (Exception Ex)
            {                
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }            
        }

        /// <summary>
        /// Insert a Case Loan to database.
        /// </summary>
        /// <param name="caseLoan">CaseLoanDTO</param>
        /// <returns></returns>
        public void UpdateCaseLoan(CaseLoanDTO caseLoan)
        {
            var command = CreateCommand("hpf_case_loan_update", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[23];
                sqlParam[0] = new SqlParameter("@pi_fc_id", caseLoan.FcId);
                sqlParam[1] = new SqlParameter("@pi_servicer_id", caseLoan.ServicerId);
                sqlParam[2] = new SqlParameter("@pi_other_servicer_name", caseLoan.OtherServicerName);
                sqlParam[3] = new SqlParameter("@pi_acct_num", caseLoan.AcctNum);
                sqlParam[4] = new SqlParameter("@pi_loan_1st_2nd_cd", caseLoan.Loan1st2nd);
                sqlParam[5] = new SqlParameter("@pi_mortgage_type_cd", caseLoan.MortgageTypeCd);
                sqlParam[6] = new SqlParameter("@pi_arm_loan_ind", caseLoan.ArmLoanInd);
                sqlParam[7] = new SqlParameter("@pi_arm_reset_ind", caseLoan.ArmResetInd);
                sqlParam[8] = new SqlParameter("@pi_term_length_cd", caseLoan.TermLengthCd);
                sqlParam[9] = new SqlParameter("@pi_loan_delinq_status_cd", caseLoan.LoanDelinqStatusCd);
                sqlParam[10] = new SqlParameter("@pi_current_loan_balance_amt", caseLoan.CurrentLoanBalanceAmt);
                sqlParam[11] = new SqlParameter("@pi_orig_loan_amt", caseLoan.OrigLoanAmt);
                sqlParam[12] = new SqlParameter("@pi_interest_rate", caseLoan.InterestRate);
                sqlParam[13] = new SqlParameter("@pi_Originating_Lender_Name", caseLoan.OriginatingLenderName);
                sqlParam[14] = new SqlParameter("@pi_orig_mortgage_co_FDIC_NCUS_num", caseLoan.OrigMortgageCoFdicNcusNum);
                sqlParam[15] = new SqlParameter("@pi_Orig_mortgage_co_name", caseLoan.OrigMortgageCoName);
                sqlParam[16] = new SqlParameter("@pi_Orginal_Loan_Num", caseLoan.OrginalLoanNum);
                sqlParam[17] = new SqlParameter("@pi_FDIC_NCUS_Num_current_servicer_TBD", caseLoan.FdicNcusNumCurrentServicerTbd);
                sqlParam[18] = new SqlParameter("@pi_Current_Servicer_Name_TBD", caseLoan.CurrentServicerNameTbd);
                sqlParam[19] = new SqlParameter("@pi_freddie_loan_num", caseLoan.FreddieLoanNum);
                sqlParam[20] = new SqlParameter("@pi_chg_lst_dt", caseLoan.ChangeLastDate);
                sqlParam[21] = new SqlParameter("@pi_chg_lst_user_id", caseLoan.ChangeLastUserId);
                sqlParam[22] = new SqlParameter("@pi_chg_lst_app_name", caseLoan.ChangeLastAppName);
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;           
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
        }

        /// <summary>
        /// delete a Case Loan to database.
        /// </summary>
        /// <param name="caseLoan">CaseLoanDTO</param>
        /// <returns></returns>
        public void DeleteCaseLoan(CaseLoanDTO caseLoan)
        {            
            var command = CreateCommand("hpf_case_loan_delete", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@pi_fc_id", caseLoan.FcId);
                sqlParam[1] = new SqlParameter("@pi_servicer_id", caseLoan.ServicerId);
                sqlParam[2] = new SqlParameter("@pi_acct_num", caseLoan.AcctNum);
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;            
                command.Transaction = this.trans;            
                command.ExecuteNonQuery();                
            }
            catch (Exception Ex)
            {                
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }            
        }

        /// <summary>
        /// Insert a Outcome Item to database.
        /// </summary>
        /// <param name="outComeItem">OutcomeItemDTO</param>
        /// <returns></returns>
        public void InsertOutcomeItem(OutcomeItemDTO outcomeItem, int fcId)
        {            
            var command = CreateCommand("hpf_outcome_item_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[12];            
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                sqlParam[1] = new SqlParameter("@pi_outcome_type_id", outcomeItem.OutcomeTypeId);
                sqlParam[2] = new SqlParameter("@pi_outcome_dt", NullableDateTime(outcomeItem.OutcomeDt));
                sqlParam[3] = new SqlParameter("@pi_outcome_deleted_dt", NullableDateTime(outcomeItem.OutcomeDeletedDt));
                sqlParam[4] = new SqlParameter("@pi_nonprofitreferral_key_num", outcomeItem.NonprofitreferralKeyNum);
                sqlParam[5] = new SqlParameter("@pi_ext_ref_other_name", outcomeItem.ExtRefOtherName);
                sqlParam[6] = new SqlParameter("@pi_create_dt", outcomeItem.CreateDate);
                sqlParam[7] = new SqlParameter("@pi_create_user_id", outcomeItem.CreateUserId);
                sqlParam[8] = new SqlParameter("@pi_create_app_name", outcomeItem.CreateAppName);
                sqlParam[9] = new SqlParameter("@pi_chg_lst_dt", outcomeItem.ChangeLastDate);
                sqlParam[10] = new SqlParameter("@pi_chg_lst_user_id", outcomeItem.ChangeLastUserId);
                sqlParam[11] = new SqlParameter("@pi_chg_lst_app_name", outcomeItem.ChangeLastAppName);
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;            
                command.Transaction = this.trans;            
                command.ExecuteNonQuery();               
            }
            catch (Exception Ex)
            {                
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }            
        }

        /// <summary>
        /// Insert a BudgetSet to database.
        /// </summary>
        /// <param name="budgetSet">BudgetSetDTO</param>
        /// <returns></returns>
        public int InsertBudgetSet(BudgetSetDTO budgetSet, int fcId)
        {
            var command = new SqlCommand("hpf_budget_set_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[12];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                sqlParam[1] = new SqlParameter("@pi_total_income", budgetSet.TotalIncome);
                sqlParam[2] = new SqlParameter("@pi_total_expenses", budgetSet.TotalExpenses);
                sqlParam[3] = new SqlParameter("@pi_total_assets", budgetSet.TotalAssets);
                sqlParam[4] = new SqlParameter("@pi_budget_set_dt", NullableDateTime(budgetSet.BudgetSetDt));
                sqlParam[5] = new SqlParameter("@pi_create_dt", budgetSet.CreateDate);
                sqlParam[6] = new SqlParameter("@pi_create_user_id", budgetSet.CreateUserId);
                sqlParam[7] = new SqlParameter("@pi_create_app_name", budgetSet.CreateAppName);
                sqlParam[8] = new SqlParameter("@pi_chg_lst_dt", budgetSet.ChangeLastDate);
                sqlParam[9] = new SqlParameter("@pi_chg_lst_user_id", budgetSet.ChangeLastUserId);
                sqlParam[10] = new SqlParameter("@pi_chg_lst_app_name", budgetSet.ChangeLastAppName);
                sqlParam[11] = new SqlParameter("@po_budget_set_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;                                
                command.Transaction = this.trans;            
                command.ExecuteNonQuery();            
                budgetSet.BudgetSetId = ConvertToInt(sqlParam[11].Value);
            }
            catch (Exception Ex)
            {                
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }            
            return budgetSet.BudgetSetId;
        }

        /// <summary>
        /// Insert a BudgetItem to database.
        /// </summary>
        /// <param name="budgetItem">BudgetItemDTO</param>
        /// <returns></returns>
        public void InsertBudgetItem(BudgetItemDTO budgetItem, int budget_set_id)
        {            
            var command = CreateCommand("hpf_budget_item_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@pi_budget_set_id", budget_set_id);
                sqlParam[1] = new SqlParameter("@pi_budget_subcategory_id", budgetItem.BudgetSubcategoryId);
                sqlParam[2] = new SqlParameter("@pi_budget_item_amt", budgetItem.BudgetItemAmt);
                sqlParam[3] = new SqlParameter("@pi_budget_note", budgetItem.BudgetNote);
                sqlParam[4] = new SqlParameter("@pi_create_dt", budgetItem.CreateDate);
                sqlParam[5] = new SqlParameter("@pi_create_user_id", budgetItem.CreateUserId);
                sqlParam[6] = new SqlParameter("@pi_create_app_name", budgetItem.CreateAppName);
                sqlParam[7] = new SqlParameter("@pi_chg_lst_dt", budgetItem.ChangeLastDate);
                sqlParam[8] = new SqlParameter("@pi_chg_lst_user_id", budgetItem.ChangeLastUserId);
                sqlParam[9] = new SqlParameter("@pi_chg_lst_app_name", budgetItem.ChangeLastAppName);

                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;            
                command.Transaction = this.trans;            
                command.ExecuteNonQuery();                
            }
            catch (Exception Ex)
            {                
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }            
        }

        /// <summary>
        /// Insert a BudgetAsset to database.
        /// </summary>
        /// <param name="budgetItem">BudgetAssetDTO</param>
        /// <returns></returns>
        public void InsertBudgetAsset(BudgetAssetDTO budgetAsset, int budget_set_id)
        {            
            var command = CreateCommand("hpf_budget_asset_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[9];
                sqlParam[0] = new SqlParameter("@pi_budget_set_id", budget_set_id);
                sqlParam[1] = new SqlParameter("@pi_asset_name", budgetAsset.AssetName);
                sqlParam[2] = new SqlParameter("@pi_asset_value", budgetAsset.AssetValue);
                sqlParam[3] = new SqlParameter("@pi_create_dt", budgetAsset.CreateDate);
                sqlParam[4] = new SqlParameter("@pi_create_user_id", budgetAsset.CreateUserId);
                sqlParam[5] = new SqlParameter("@pi_create_app_name", budgetAsset.CreateAppName);
                sqlParam[6] = new SqlParameter("@pi_chg_lst_dt", budgetAsset.ChangeLastDate);
                sqlParam[7] = new SqlParameter("@pi_chg_lst_user_id", budgetAsset.ChangeLastUserId);
                sqlParam[8] = new SqlParameter("@pi_chg_lst_app_name", budgetAsset.ChangeLastAppName);
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;            
                command.Transaction = this.trans;
           
                command.ExecuteNonQuery();                
            }
            catch (Exception Ex)
            {                
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }           
        }

        /// <summary>
        /// Update a ForeclosureCase to database.
        /// </summary>
        /// <param name="foreclosureCase">ForeclosureCase</param>
        /// <returns>a new Fc_id</returns>
        public int UpdateForeclosureCase(ForeclosureCaseDTO foreclosureCase)
        {            
            var command = CreateSPCommand("hpf_foreclosure_case_update", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[111];
                sqlParam[0] = new SqlParameter("@pi_agency_id", foreclosureCase.AgencyId);
                sqlParam[1] = new SqlParameter("@pi_completed_dt", NullableDateTime(foreclosureCase.CompletedDt));
                sqlParam[2] = new SqlParameter("@pi_call_id", foreclosureCase.CallId);
                sqlParam[3] = new SqlParameter("@pi_program_id", foreclosureCase.ProgramId);
                sqlParam[4] = new SqlParameter("@pi_agency_case_num", foreclosureCase.AgencyCaseNum);
                sqlParam[5] = new SqlParameter("@pi_agency_client_num", foreclosureCase.AgencyClientNum);
                sqlParam[6] = new SqlParameter("@pi_intake_dt", foreclosureCase.IntakeDt);
                sqlParam[7] = new SqlParameter("@pi_income_earners_cd", foreclosureCase.IncomeEarnersCd);
                sqlParam[8] = new SqlParameter("@pi_case_source_cd", foreclosureCase.CaseSourceCd);
                sqlParam[9] = new SqlParameter("@pi_race_cd", foreclosureCase.RaceCd);
                sqlParam[10] = new SqlParameter("@pi_household_cd", foreclosureCase.HouseholdCd);
                sqlParam[11] = new SqlParameter("@pi_never_bill_reason_cd", foreclosureCase.NeverBillReasonCd);
                sqlParam[12] = new SqlParameter("@pi_never_pay_reason_cd", foreclosureCase.NeverPayReasonCd);
                sqlParam[13] = new SqlParameter("@pi_dflt_reason_1st_cd", foreclosureCase.DfltReason1stCd);
                sqlParam[14] = new SqlParameter("@pi_dflt_reason_2nd_cd", foreclosureCase.DfltReason2ndCd);
                sqlParam[15] = new SqlParameter("@pi_hud_termination_reason_cd", foreclosureCase.HudTerminationReasonCd);
                sqlParam[16] = new SqlParameter("@pi_hud_termination_dt", NullableDateTime(foreclosureCase.HudTerminationDt));
                sqlParam[17] = new SqlParameter("@pi_hud_outcome_cd", foreclosureCase.HudOutcomeCd);
                sqlParam[18] = new SqlParameter("@pi_AMI_percentage", foreclosureCase.AmiPercentage);
                sqlParam[19] = new SqlParameter("@pi_counseling_duration_cd", foreclosureCase.CounselingDurationCd);
                sqlParam[20] = new SqlParameter("@pi_gender_cd", foreclosureCase.GenderCd);
                sqlParam[21] = new SqlParameter("@pi_borrower_fname", foreclosureCase.BorrowerFname);
                sqlParam[22] = new SqlParameter("@pi_borrower_lname", foreclosureCase.BorrowerLname);
                sqlParam[23] = new SqlParameter("@pi_borrower_mname", foreclosureCase.BorrowerMname);
                sqlParam[24] = new SqlParameter("@pi_mother_maiden_lname", foreclosureCase.MotherMaidenLname);
                sqlParam[25] = new SqlParameter("@pi_borrower_ssn", foreclosureCase.BorrowerSsn);
                sqlParam[26] = new SqlParameter("@pi_borrower_last4_SSN", foreclosureCase.BorrowerLast4Ssn);
                sqlParam[27] = new SqlParameter("@pi_borrower_DOB", NullableDateTime(foreclosureCase.BorrowerDob));
                sqlParam[28] = new SqlParameter("@pi_co_borrower_fname", foreclosureCase.CoBorrowerFname);
                sqlParam[29] = new SqlParameter("@pi_co_borrower_lname", foreclosureCase.CoBorrowerLname);
                sqlParam[30] = new SqlParameter("@pi_co_borrower_mname", foreclosureCase.CoBorrowerMname);
                sqlParam[31] = new SqlParameter("@pi_co_borrower_ssn", foreclosureCase.CoBorrowerSsn);
                sqlParam[32] = new SqlParameter("@pi_co_borrower_last4_SSN", foreclosureCase.CoBorrowerLast4Ssn);
                sqlParam[33] = new SqlParameter("@pi_co_borrower_DOB", NullableDateTime(foreclosureCase.CoBorrowerDob));
                sqlParam[34] = new SqlParameter("@pi_primary_contact_no", foreclosureCase.PrimaryContactNo);
                sqlParam[35] = new SqlParameter("@pi_second_contact_no", foreclosureCase.SecondContactNo);
                sqlParam[36] = new SqlParameter("@pi_email_1", foreclosureCase.Email1);
                sqlParam[37] = new SqlParameter("@pi_email_2", foreclosureCase.Email2);
                sqlParam[38] = new SqlParameter("@pi_contact_addr1", foreclosureCase.ContactAddr1);
                sqlParam[39] = new SqlParameter("@pi_contact_addr2", foreclosureCase.ContactAddr2);
                sqlParam[40] = new SqlParameter("@pi_contact_city", foreclosureCase.ContactCity);
                sqlParam[41] = new SqlParameter("@pi_contact_state_cd", foreclosureCase.ContactStateCd);
                sqlParam[42] = new SqlParameter("@pi_contact_zip", foreclosureCase.ContactZip);
                sqlParam[43] = new SqlParameter("@pi_contact_zip_plus4", foreclosureCase.ContactZipPlus4);
                sqlParam[44] = new SqlParameter("@pi_prop_addr1", foreclosureCase.PropAddr1);
                sqlParam[45] = new SqlParameter("@pi_prop_addr2", foreclosureCase.PropAddr2);
                sqlParam[46] = new SqlParameter("@pi_prop_city", foreclosureCase.PropCity);
                sqlParam[47] = new SqlParameter("@pi_prop_state_cd", foreclosureCase.PropStateCd);
                sqlParam[48] = new SqlParameter("@pi_prop_zip", foreclosureCase.PropZip);
                sqlParam[49] = new SqlParameter("@pi_prop_zip_plus_4", foreclosureCase.PropZipPlus4);
                sqlParam[50] = new SqlParameter("@pi_bankruptcy_ind", foreclosureCase.BankruptcyInd);
                sqlParam[51] = new SqlParameter("@pi_bankruptcy_attorney", foreclosureCase.BankruptcyAttorney);
                sqlParam[52] = new SqlParameter("@pi_bankruptcy_pmt_current_ind", foreclosureCase.BankruptcyPmtCurrentInd);
                sqlParam[53] = new SqlParameter("@pi_borrower_educ_level_completed_cd", foreclosureCase.BorrowerEducLevelCompletedCd);
                sqlParam[54] = new SqlParameter("@pi_borrower_marital_status_cd", foreclosureCase.BorrowerMaritalStatusCd);
                sqlParam[55] = new SqlParameter("@pi_borrower_preferred_lang_cd", foreclosureCase.BorrowerPreferredLangCd);
                sqlParam[56] = new SqlParameter("@pi_borrower_occupation", foreclosureCase.BorrowerOccupationCd);
                sqlParam[57] = new SqlParameter("@pi_co_borrower_occupation", foreclosureCase.CoBorrowerOccupationCd);
                sqlParam[58] = new SqlParameter("@pi_owner_occupied_ind", foreclosureCase.OwnerOccupiedInd);
                sqlParam[59] = new SqlParameter("@pi_hispanic_ind", foreclosureCase.HispanicInd);
                sqlParam[60] = new SqlParameter("@pi_duplicate_ind", foreclosureCase.DuplicateInd);
                sqlParam[61] = new SqlParameter("@pi_fc_notice_received_ind", foreclosureCase.FcNoticeReceiveInd);
                sqlParam[62] = new SqlParameter("@pi_case_complete_ind", foreclosureCase.CaseCompleteInd);
                sqlParam[63] = new SqlParameter("@pi_funding_consent_ind", foreclosureCase.FundingConsentInd);
                sqlParam[64] = new SqlParameter("@pi_servicer_consent_ind", foreclosureCase.ServicerConsentInd);
                sqlParam[65] = new SqlParameter("@pi_agency_media_consent_ind", foreclosureCase.AgencyMediaConsentInd);
                sqlParam[66] = new SqlParameter("@pi_hpf_media_candidate_ind", foreclosureCase.HpfMediaCandidateInd);
                sqlParam[67] = new SqlParameter("@pi_hpf_network_candidate_ind", foreclosureCase.HpfNetworkCandidateInd);
                sqlParam[68] = new SqlParameter("@pi_hpf_success_story_ind", foreclosureCase.HpfSuccessStoryInd);
                sqlParam[69] = new SqlParameter("@pi_agency_success_story_ind", foreclosureCase.AgencySuccessStoryInd);
                sqlParam[70] = new SqlParameter("@pi_borrower_disabled_ind", foreclosureCase.BorrowerDisabledInd);
                sqlParam[71] = new SqlParameter("@pi_co_borrower_disabled_ind", foreclosureCase.CoBorrowerDisabledInd);
                sqlParam[72] = new SqlParameter("@pi_summary_sent_other_cd", foreclosureCase.SummarySentOtherCd);
                sqlParam[73] = new SqlParameter("@pi_summary_sent_other_dt", NullableDateTime(foreclosureCase.SummarySentOtherDt));
                sqlParam[74] = new SqlParameter("@pi_summary_sent_dt", NullableDateTime(foreclosureCase.SummarySentDt));
                sqlParam[75] = new SqlParameter("@pi_occupant_num", foreclosureCase.OccupantNum);
                sqlParam[76] = new SqlParameter("@pi_loan_dflt_reason_notes", foreclosureCase.LoanDfltReasonNotes);
                sqlParam[77] = new SqlParameter("@pi_action_items_notes", foreclosureCase.ActionItemsNotes);
                sqlParam[78] = new SqlParameter("@pi_followup_notes", foreclosureCase.FollowupNotes);
                sqlParam[79] = new SqlParameter("@pi_prim_res_est_mkt_value", foreclosureCase.PrimResEstMktValue);
                sqlParam[80] = new SqlParameter("@pi_counselor_id_ref", foreclosureCase.AssignedCounselorIdRef);
                sqlParam[81] = new SqlParameter("@pi_counselor_lname", foreclosureCase.CounselorFname);
                sqlParam[82] = new SqlParameter("@pi_counselor_fname", foreclosureCase.CounselorLname);
                sqlParam[83] = new SqlParameter("@pi_counselor_email", foreclosureCase.CounselorEmail);
                sqlParam[84] = new SqlParameter("@pi_counselor_phone", foreclosureCase.CounselorPhone);
                sqlParam[85] = new SqlParameter("@pi_counselor_ext", foreclosureCase.CounselorExt);
                sqlParam[86] = new SqlParameter("@pi_discussed_solution_with_srvcr_ind", foreclosureCase.DiscussedSolutionWithSrvcrInd);
                sqlParam[87] = new SqlParameter("@pi_worked_with_another_agency_ind", foreclosureCase.WorkedWithAnotherAgencyInd);
                sqlParam[88] = new SqlParameter("@pi_contacted_srvcr_recently_ind", foreclosureCase.ContactedSrvcrRecentlyInd);
                sqlParam[89] = new SqlParameter("@pi_has_workout_plan_ind", foreclosureCase.HasWorkoutPlanInd);
                sqlParam[90] = new SqlParameter("@pi_srvcr_workout_plan_current_ind", foreclosureCase.SrvcrWorkoutPlanCurrentInd);
                sqlParam[91] = new SqlParameter("@pi_fc_sale_date_set_ind", foreclosureCase.FcSaleDateSetInd);
                sqlParam[92] = new SqlParameter("@pi_opt_out_newsletter_ind", foreclosureCase.OptOutNewsletterInd);
                sqlParam[93] = new SqlParameter("@pi_opt_out_survey_ind", foreclosureCase.OptOutSurveyInd);
                sqlParam[94] = new SqlParameter("@pi_do_not_call_ind", foreclosureCase.DoNotCallInd);
                sqlParam[95] = new SqlParameter("@pi_primary_residence_ind", foreclosureCase.PrimaryResidenceInd);
                sqlParam[96] = new SqlParameter("@pi_realty_company", foreclosureCase.RealtyCompany);
                sqlParam[97] = new SqlParameter("@pi_property_cd", foreclosureCase.PropertyCd);
                sqlParam[98] = new SqlParameter("@pi_for_sale_ind", foreclosureCase.ForSaleInd);
                sqlParam[99] = new SqlParameter("@pi_home_sale_price", foreclosureCase.HomeSalePrice);
                sqlParam[100] = new SqlParameter("@pi_home_purchase_year", foreclosureCase.HomePurchaseYear);
                sqlParam[101] = new SqlParameter("@pi_home_purchase_price", foreclosureCase.HomePurchasePrice);
                sqlParam[102] = new SqlParameter("@pi_Home_Current_Market_Value", foreclosureCase.HomeCurrentMarketValue);
                sqlParam[103] = new SqlParameter("@pi_military_service_cd", foreclosureCase.MilitaryServiceCd);
                sqlParam[104] = new SqlParameter("@pi_household_gross_annual_income_amt", foreclosureCase.HouseholdGrossAnnualIncomeAmt);                
                sqlParam[105] = new SqlParameter("@pi_intake_credit_score", foreclosureCase.IntakeCreditScore);
                sqlParam[106] = new SqlParameter("@pi_Intake_credit_bureau_cd ", foreclosureCase.IntakeCreditBureauCd);
                sqlParam[107] = new SqlParameter("@pi_chg_lst_dt", foreclosureCase.ChangeLastDate);
                sqlParam[108] = new SqlParameter("@pi_chg_lst_user_id", foreclosureCase.ChangeLastUserId);
                sqlParam[109] = new SqlParameter("@pi_chg_lst_app_name", foreclosureCase.ChangeLastAppName);
                sqlParam[110] = new SqlParameter("@pi_fc_id", foreclosureCase.FcId);
                //</Parameter>            
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;            
                command.Transaction = this.trans;            
                command.ExecuteNonQuery();                             
            }
            catch (Exception Ex)
            {                
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }            
            return foreclosureCase.FcId;
        }

        /// <summary>
        /// Insert a Outcome Item to database.
        /// </summary>
        /// <param name="outComeItem">OutcomeItemDTO</param>
        /// <returns></returns>
        public void UpdateOutcomeItem(OutcomeItemDTO outcomeItem)
        {
            var command = CreateCommand("hpf_outcome_item_update", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@pi_outcome_item_id", outcomeItem.OutcomeItemId);                
                sqlParam[1] = new SqlParameter("@pi_chg_lst_dt", outcomeItem.ChangeLastDate);
                sqlParam[2] = new SqlParameter("@pi_chg_lst_user_id", outcomeItem.ChangeLastUserId);
                sqlParam[3] = new SqlParameter("@pi_chg_lst_app_name", outcomeItem.ChangeLastAppName);
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;            
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
        }

        public bool CheckExistingAgencyIdAndCaseNumber(int agency_id, string agency_case_number)
        {
            bool returnValue = true;
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_foreclosure_case_get_from_agencyID_and_caseNumber", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@pi_agency_case_num", agency_case_number);
            sqlParam[1] = new SqlParameter("@pi_agency_id", agency_id);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                    returnValue = true;
                else
                    returnValue = false;
                reader.Close();
                dbConnection.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            return returnValue;

        }

        /// <summary>        
        /// Check if there are any active Foreclosure case in DB which are different from current FC_Case
        /// </summary>
        /// <param name="fc_id">id of current FC_Case</param>       
        /// <returns>true if another FC_Case with the same acct_num and servicer_id exists in db, otherwise false</returns>
        public bool CheckDuplicate(int fcId)
        {
            bool returnValue = true;
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_foreclosure_case_get_duplicate", dbConnection);
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
                returnValue = reader.HasRows;                
                reader.Close();
                dbConnection.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            return returnValue;
        }

        public bool CheckDuplicate(int agency_id, string agency_case_number)
        {
            bool returnValue = true;
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_foreclosure_case_get_duplicate", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@pi_agency_case_num", agency_case_number);
            sqlParam[1] = new SqlParameter("@pi_agency_id", agency_id);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                returnValue = reader.HasRows; 
                reader.Close();
                dbConnection.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            return returnValue;
        }
    }
}


