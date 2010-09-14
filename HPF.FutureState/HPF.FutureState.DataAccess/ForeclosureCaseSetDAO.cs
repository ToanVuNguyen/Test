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
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common;

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
        public ForeclosureCaseDTO GetForeclosureCase(int? fcId)
        {
            ForeclosureCaseDTO returnObject = new ForeclosureCaseDTO();
            SqlConnection dbConnection = base.CreateConnection();
            try
            {
                SqlCommand command = base.CreateCommand("hpf_foreclosure_case_detail_get", dbConnection);

                //<Parameter>
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);

                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;                
                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        #region set ForeclosureCase value
                        returnObject.FcId = ConvertToInt(reader["fc_id"]);

                        returnObject.ActionItemsNotes = ConvertToString(reader["action_items_notes"]);
                        returnObject.AgencyCaseNum = ConvertToString(reader["agency_case_num"]);
                        returnObject.AgencyClientNum = ConvertToString(reader["agency_client_num"]);
                        returnObject.AgencyId = ConvertToInt(reader["agency_id"]);
                        returnObject.AgencyMediaInterestInd= ConvertToString(reader["agency_media_interest_ind"]);
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
                        returnObject.BorrowerOccupation = ConvertToString(reader["borrower_occupation"]);
                        returnObject.BorrowerPreferredLangCd = ConvertToString(reader["borrower_preferred_lang_cd"]);
                        //returnObject.BorrowerSsn = ConvertToString(reader["borrower_ssn"]);

                        returnObject.CallId = ConvertToString(reader["call_id"]);
                        returnObject.CaseSourceCd = ConvertToString(reader["case_source_cd"]);
                        returnObject.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                        returnObject.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                        returnObject.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                        returnObject.ChgLstUserId = returnObject.ChangeLastUserId;
                        returnObject.CoBorrowerDisabledInd = ConvertToString(reader["co_borrower_disabled_ind"]);
                        returnObject.CoBorrowerDob = ConvertToDateTime(reader["co_borrower_DOB"]);
                        returnObject.CoBorrowerFname = ConvertToString(reader["co_borrower_fname"]);
                        returnObject.CoBorrowerLname = ConvertToString(reader["co_borrower_lname"]);
                        returnObject.CoBorrowerLast4Ssn = ConvertToString(reader["co_borrower_last4_SSN"]);
                        returnObject.CoBorrowerMname = ConvertToString(reader["co_borrower_mname"]);
                        returnObject.CoBorrowerOccupation = ConvertToString(reader["co_borrower_occupation"]);
                        //returnObject.CoBorrowerSsn = ConvertToString(reader["co_borrower_ssn"]);
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

                        
                        returnObject.FcNoticeReceiveInd = ConvertToString(reader["fc_notice_received_ind"]);
                        returnObject.FollowupNotes = ConvertToString(reader["followup_notes"]);
                        returnObject.ForSaleInd = ConvertToString(reader["for_sale_ind"]);
                        returnObject.FundingConsentInd = ConvertToString(reader["funding_consent_ind"]);

                        returnObject.GenderCd = ConvertToString(reader["gender_cd"]);

                        returnObject.HasWorkoutPlanInd = ConvertToString(reader["has_workout_plan_ind"]);
                        returnObject.HispanicInd = ConvertToString(reader["hispanic_ind"]);
                        returnObject.HomeCurrentMarketValue = ConvertToDouble(reader["home_current_market_value"]);
                        returnObject.HomePurchasePrice = ConvertToDouble(reader["home_purchase_price"]);
                        returnObject.HomePurchaseYear = ConvertToInt(reader["home_purchase_year"]);
                        returnObject.HomeSalePrice = ConvertToDouble(reader["home_sale_price"]);
                        returnObject.HouseholdCd = ConvertToString(reader["household_cd"]);
                        returnObject.HpfMediaCandidateInd = ConvertToString(reader["hpf_media_candidate_ind"]);                        
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
                        returnObject.PrimResEstMktValue = ConvertToDouble(reader["prim_res_est_mkt_value"]);
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

                        returnObject.VipInd = ConvertToString(reader["vip_ind"]);
                        returnObject.VipReason = ConvertToString(reader["vip_reason"]);
                        returnObject.CounseledLanguageCd = ConvertToString(reader["counseled_language_cd"]);
                        returnObject.ErcpOutcomeCd = ConvertToString(reader["ercp_outcome_cd"]);
                        returnObject.CounselorContactedSrvcrInd = ConvertToString(reader["counselor_contacted_srvcr_ind"]);
                        returnObject.NumberOfUnits = ConvertToInt(reader["number_of_units"]);
                        returnObject.VacantOrCondemedInd = ConvertToString(reader["vacant_or_condemed_ind"]);
                        returnObject.MortgagePmtRatio = ConvertToDouble(reader["mortgage_pmt_ratio"]);
                        returnObject.CertificateId = ConvertToString(reader["certificate_id"]);
                        returnObject.CounseledProgramId = ConvertToInt(reader["counseled_program_id"]);
                        returnObject.ReferralClientNum = ConvertToString(reader["referral_client_num"]);
                        returnObject.SponsorId = ConvertToInt(reader["sponsor_id"]);
                        returnObject.CampaignId = ConvertToInt(reader["campaign_id"]);
                        #endregion
                    }                    
                }
                else
                    returnObject = null;
                reader.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return returnObject;

        }
        
        /// <summary>
        /// Insert a ForeclosureCase to database.
        /// </summary>
        /// <param name="foreclosureCase">ForeclosureCase</param>
        /// <returns>a new Fc_id</returns>
        public int? InsertForeclosureCase(ForeclosureCaseDTO foreclosureCase)
        {
            var command = CreateSPCommand("hpf_foreclosure_case_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[119];
                sqlParam[0] = new SqlParameter("@pi_agency_id", foreclosureCase.AgencyId);
                sqlParam[1] = new SqlParameter("@pi_completed_dt", NullableDateTime(foreclosureCase.CompletedDt));
                sqlParam[2] = new SqlParameter("@pi_call_id", foreclosureCase.CallId);
                sqlParam[3] = new SqlParameter("@pi_program_id", foreclosureCase.ProgramId);
                sqlParam[4] = new SqlParameter("@pi_agency_case_num", NullableString(foreclosureCase.AgencyCaseNum));
                sqlParam[5] = new SqlParameter("@pi_agency_client_num", NullableString(foreclosureCase.AgencyClientNum));
                sqlParam[6] = new SqlParameter("@pi_intake_dt", foreclosureCase.IntakeDt);
                sqlParam[7] = new SqlParameter("@pi_income_earners_cd", NullableString(foreclosureCase.IncomeEarnersCd));
                sqlParam[8] = new SqlParameter("@pi_case_source_cd", NullableString(foreclosureCase.CaseSourceCd));
                sqlParam[9] = new SqlParameter("@pi_race_cd", NullableString(foreclosureCase.RaceCd));
                sqlParam[10] = new SqlParameter("@pi_household_cd", NullableString(foreclosureCase.HouseholdCd));
                sqlParam[11] = new SqlParameter("@pi_dflt_reason_1st_cd", NullableString(foreclosureCase.DfltReason1stCd));
                sqlParam[12] = new SqlParameter("@pi_dflt_reason_2nd_cd", NullableString(foreclosureCase.DfltReason2ndCd));
                sqlParam[13] = new SqlParameter("@pi_hud_termination_reason_cd", NullableString(foreclosureCase.HudTerminationReasonCd));
                sqlParam[14] = new SqlParameter("@pi_hud_termination_dt", NullableDateTime(foreclosureCase.HudTerminationDt));
                sqlParam[15] = new SqlParameter("@pi_hud_outcome_cd", NullableString(foreclosureCase.HudOutcomeCd));
                sqlParam[16] = new SqlParameter("@pi_counseling_duration_cd", NullableString(foreclosureCase.CounselingDurationCd));
                sqlParam[17] = new SqlParameter("@pi_gender_cd", NullableString(foreclosureCase.GenderCd));
                sqlParam[18] = new SqlParameter("@pi_borrower_fname", NullableString(foreclosureCase.BorrowerFname));
                sqlParam[19] = new SqlParameter("@pi_borrower_lname", NullableString(foreclosureCase.BorrowerLname));
                sqlParam[20] = new SqlParameter("@pi_borrower_mname", NullableString(foreclosureCase.BorrowerMname));
                sqlParam[21] = new SqlParameter("@pi_mother_maiden_lname", NullableString(foreclosureCase.MotherMaidenLname));
                sqlParam[22] = new SqlParameter("@pi_borrower_ssn", NullableString(foreclosureCase.BorrowerSsn));
                sqlParam[23] = new SqlParameter("@pi_borrower_last4_SSN", NullableString(foreclosureCase.BorrowerLast4Ssn));
                sqlParam[24] = new SqlParameter("@pi_borrower_DOB", NullableDateTime(foreclosureCase.BorrowerDob));
                sqlParam[25] = new SqlParameter("@pi_co_borrower_fname", NullableString(foreclosureCase.CoBorrowerFname));
                sqlParam[26] = new SqlParameter("@pi_co_borrower_lname", NullableString(foreclosureCase.CoBorrowerLname));
                sqlParam[27] = new SqlParameter("@pi_co_borrower_mname", NullableString(foreclosureCase.CoBorrowerMname));
                sqlParam[28] = new SqlParameter("@pi_co_borrower_ssn", NullableString(foreclosureCase.CoBorrowerSsn));
                sqlParam[29] = new SqlParameter("@pi_co_borrower_last4_SSN", NullableString(foreclosureCase.CoBorrowerLast4Ssn));
                sqlParam[30] = new SqlParameter("@pi_co_borrower_DOB", NullableDateTime(foreclosureCase.CoBorrowerDob));
                sqlParam[31] = new SqlParameter("@pi_primary_contact_no", NullableString(foreclosureCase.PrimaryContactNo));
                sqlParam[32] = new SqlParameter("@pi_second_contact_no", NullableString(foreclosureCase.SecondContactNo));
                sqlParam[33] = new SqlParameter("@pi_email_1", NullableString(foreclosureCase.Email1));
                sqlParam[34] = new SqlParameter("@pi_email_2", NullableString(foreclosureCase.Email2));
                sqlParam[35] = new SqlParameter("@pi_contact_addr1", NullableString(foreclosureCase.ContactAddr1));
                sqlParam[36] = new SqlParameter("@pi_contact_addr2", NullableString(foreclosureCase.ContactAddr2));
                sqlParam[37] = new SqlParameter("@pi_contact_city", NullableString(foreclosureCase.ContactCity));
                sqlParam[38] = new SqlParameter("@pi_contact_state_cd", NullableString(foreclosureCase.ContactStateCd));
                sqlParam[39] = new SqlParameter("@pi_contact_zip", NullableString(foreclosureCase.ContactZip));
                sqlParam[40] = new SqlParameter("@pi_contact_zip_plus4", NullableString(foreclosureCase.ContactZipPlus4));
                sqlParam[41] = new SqlParameter("@pi_prop_addr1", NullableString(foreclosureCase.PropAddr1));
                sqlParam[42] = new SqlParameter("@pi_prop_addr2", NullableString(foreclosureCase.PropAddr2));
                sqlParam[43] = new SqlParameter("@pi_prop_city", NullableString(foreclosureCase.PropCity));
                sqlParam[44] = new SqlParameter("@pi_prop_state_cd", NullableString(foreclosureCase.PropStateCd));
                sqlParam[45] = new SqlParameter("@pi_prop_zip", NullableString(foreclosureCase.PropZip));
                sqlParam[46] = new SqlParameter("@pi_prop_zip_plus_4", NullableString(foreclosureCase.PropZipPlus4));
                sqlParam[47] = new SqlParameter("@pi_bankruptcy_ind", NullableString(foreclosureCase.BankruptcyInd));
                sqlParam[48] = new SqlParameter("@pi_bankruptcy_attorney", NullableString(foreclosureCase.BankruptcyAttorney));
                sqlParam[49] = new SqlParameter("@pi_bankruptcy_pmt_current_ind", NullableString(foreclosureCase.BankruptcyPmtCurrentInd));
                sqlParam[50] = new SqlParameter("@pi_borrower_educ_level_completed_cd", NullableString(foreclosureCase.BorrowerEducLevelCompletedCd));
                sqlParam[51] = new SqlParameter("@pi_borrower_marital_status_cd", NullableString(foreclosureCase.BorrowerMaritalStatusCd));
                sqlParam[52] = new SqlParameter("@pi_borrower_preferred_lang_cd", NullableString(foreclosureCase.BorrowerPreferredLangCd));
                sqlParam[53] = new SqlParameter("@pi_borrower_occupation", NullableString(foreclosureCase.BorrowerOccupation));
                sqlParam[54] = new SqlParameter("@pi_co_borrower_occupation", NullableString(foreclosureCase.CoBorrowerOccupation));
                sqlParam[55] = new SqlParameter("@pi_owner_occupied_ind", NullableString(foreclosureCase.OwnerOccupiedInd));
                sqlParam[56] = new SqlParameter("@pi_hispanic_ind", NullableString(foreclosureCase.HispanicInd));
                sqlParam[57] = new SqlParameter("@pi_duplicate_ind", NullableString(foreclosureCase.DuplicateInd));
                sqlParam[58] = new SqlParameter("@pi_fc_notice_received_ind", NullableString(foreclosureCase.FcNoticeReceiveInd));
                sqlParam[59] = new SqlParameter("@pi_funding_consent_ind", NullableString(foreclosureCase.FundingConsentInd));
                sqlParam[60] = new SqlParameter("@pi_servicer_consent_ind", NullableString(foreclosureCase.ServicerConsentInd));
                sqlParam[61] = new SqlParameter("@pi_agency_media_interest_ind", NullableString(foreclosureCase.AgencyMediaInterestInd));
                sqlParam[62] = new SqlParameter("@pi_agency_success_story_ind", NullableString(foreclosureCase.AgencySuccessStoryInd));
                sqlParam[63] = new SqlParameter("@pi_borrower_disabled_ind", NullableString(foreclosureCase.BorrowerDisabledInd));
                sqlParam[64] = new SqlParameter("@pi_co_borrower_disabled_ind", NullableString(foreclosureCase.CoBorrowerDisabledInd));
                sqlParam[65] = new SqlParameter("@pi_summary_sent_other_cd", NullableString(foreclosureCase.SummarySentOtherCd));
                sqlParam[66] = new SqlParameter("@pi_summary_sent_other_dt", NullableDateTime(foreclosureCase.SummarySentOtherDt));
                sqlParam[67] = new SqlParameter("@pi_summary_sent_dt", NullableDateTime(foreclosureCase.SummarySentDt));
                sqlParam[68] = new SqlParameter("@pi_occupant_num", foreclosureCase.OccupantNum);
                sqlParam[69] = new SqlParameter("@pi_loan_dflt_reason_notes", NullableString(foreclosureCase.LoanDfltReasonNotes));
                sqlParam[70] = new SqlParameter("@pi_action_items_notes", NullableString(foreclosureCase.ActionItemsNotes));
                sqlParam[71] = new SqlParameter("@pi_followup_notes", NullableString(foreclosureCase.FollowupNotes));
                sqlParam[72] = new SqlParameter("@pi_prim_res_est_mkt_value", foreclosureCase.PrimResEstMktValue);
                sqlParam[73] = new SqlParameter("@pi_counselor_id_ref", NullableString(foreclosureCase.AssignedCounselorIdRef));
                sqlParam[74] = new SqlParameter("@pi_counselor_lname", NullableString(foreclosureCase.CounselorLname));
                sqlParam[75] = new SqlParameter("@pi_counselor_fname", NullableString(foreclosureCase.CounselorFname));
                sqlParam[76] = new SqlParameter("@pi_counselor_email", NullableString(foreclosureCase.CounselorEmail));
                sqlParam[77] = new SqlParameter("@pi_counselor_phone", NullableString(foreclosureCase.CounselorPhone));
                sqlParam[78] = new SqlParameter("@pi_counselor_ext", NullableString(foreclosureCase.CounselorExt));
                sqlParam[79] = new SqlParameter("@pi_discussed_solution_with_srvcr_ind", NullableString(foreclosureCase.DiscussedSolutionWithSrvcrInd));
                sqlParam[80] = new SqlParameter("@pi_worked_with_another_agency_ind", NullableString(foreclosureCase.WorkedWithAnotherAgencyInd));
                sqlParam[81] = new SqlParameter("@pi_contacted_srvcr_recently_ind", NullableString(foreclosureCase.ContactedSrvcrRecentlyInd));
                sqlParam[82] = new SqlParameter("@pi_has_workout_plan_ind", NullableString(foreclosureCase.HasWorkoutPlanInd));
                sqlParam[83] = new SqlParameter("@pi_srvcr_workout_plan_current_ind", NullableString(foreclosureCase.SrvcrWorkoutPlanCurrentInd));
                sqlParam[84] = new SqlParameter("@pi_primary_residence_ind", NullableString(foreclosureCase.PrimaryResidenceInd));
                sqlParam[85] = new SqlParameter("@pi_realty_company", NullableString(foreclosureCase.RealtyCompany));
                sqlParam[86] = new SqlParameter("@pi_property_cd", NullableString(foreclosureCase.PropertyCd));
                sqlParam[87] = new SqlParameter("@pi_for_sale_ind", NullableString(foreclosureCase.ForSaleInd));
                sqlParam[88] = new SqlParameter("@pi_home_sale_price", foreclosureCase.HomeSalePrice);
                sqlParam[89] = new SqlParameter("@pi_home_purchase_year", foreclosureCase.HomePurchaseYear);
                sqlParam[90] = new SqlParameter("@pi_home_purchase_price", foreclosureCase.HomePurchasePrice);
                sqlParam[91] = new SqlParameter("@pi_Home_Current_Market_Value", foreclosureCase.HomeCurrentMarketValue);
                sqlParam[92] = new SqlParameter("@pi_military_service_cd", NullableString(foreclosureCase.MilitaryServiceCd));
                sqlParam[93] = new SqlParameter("@pi_household_gross_annual_income_amt", foreclosureCase.HouseholdGrossAnnualIncomeAmt);
                sqlParam[94] = new SqlParameter("@pi_intake_credit_score", NullableString(foreclosureCase.IntakeCreditScore));
                sqlParam[95] = new SqlParameter("@pi_Intake_credit_bureau_cd ", NullableString(foreclosureCase.IntakeCreditBureauCd));
                sqlParam[96] = new SqlParameter("@pi_fc_sale_dt", NullableDateTime(foreclosureCase.FcSaleDate));
                sqlParam[97] = new SqlParameter("@pi_create_dt", NullableDateTime(foreclosureCase.CreateDate));
                sqlParam[98] = new SqlParameter("@pi_create_user_id", foreclosureCase.CreateUserId);
                sqlParam[99] = new SqlParameter("@pi_create_app_name", foreclosureCase.CreateAppName);
                sqlParam[100] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(foreclosureCase.ChangeLastDate));
                sqlParam[101] = new SqlParameter("@pi_chg_lst_user_id", foreclosureCase.ChangeLastUserId);
                sqlParam[102] = new SqlParameter("@pi_chg_lst_app_name", foreclosureCase.ChangeLastAppName);
                sqlParam[103] = new SqlParameter("@pi_never_bill_reason_cd", foreclosureCase.NeverBillReasonCd);
                sqlParam[104] = new SqlParameter("@pi_never_pay_reason_cd", foreclosureCase.NeverPayReasonCd);

                sqlParam[105] = new SqlParameter("@pi_vip_ind", foreclosureCase.VipInd);
                sqlParam[106] = new SqlParameter("@pi_vip_reason", foreclosureCase.VipReason);
                sqlParam[107] = new SqlParameter("@pi_counseled_language_cd", foreclosureCase.CounseledLanguageCd);
                sqlParam[108] = new SqlParameter("@pi_ercp_outcome_cd", foreclosureCase.ErcpOutcomeCd);
                sqlParam[109] = new SqlParameter("@pi_counselor_contacted_srvcr_ind", foreclosureCase.CounselorContactedSrvcrInd);
                sqlParam[110] = new SqlParameter("@pi_number_of_units", foreclosureCase.NumberOfUnits);
                sqlParam[111] = new SqlParameter("@pi_vacant_or_condemed_ind", foreclosureCase.VacantOrCondemedInd);
                sqlParam[112] = new SqlParameter("@pi_mortgage_pmt_ratio", foreclosureCase.MortgagePmtRatio);
                sqlParam[113] = new SqlParameter("@pi_certificate_id", foreclosureCase.CertificateId);

                sqlParam[114] = new SqlParameter("@pi_counseled_program_id", foreclosureCase.CounseledProgramId);
                sqlParam[115] = new SqlParameter("@pi_referral_client_num", foreclosureCase.ReferralClientNum);
                sqlParam[116] = new SqlParameter("@pi_sponsor_id", foreclosureCase.SponsorId);
                sqlParam[117] = new SqlParameter("@pi_campaign_id", foreclosureCase.CampaignId);

                sqlParam[118] = new SqlParameter("@po_fc_id", SqlDbType.Int) { Direction = ParameterDirection.Output }; 
                //</Parameter> 
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;            
                command.Transaction = this.trans;            
                command.ExecuteNonQuery();                
                foreclosureCase.FcId = ConvertToInt(sqlParam[118].Value);
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
        public void InsertCaseLoan(CaseLoanDTO caseLoan, int? fcId)
        {            
            var command = CreateCommand("hpf_case_loan_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[40];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                sqlParam[1] = new SqlParameter("@pi_servicer_id", caseLoan.ServicerId);
                sqlParam[2] = new SqlParameter("@pi_other_servicer_name", NullableString(caseLoan.OtherServicerName));
                sqlParam[3] = new SqlParameter("@pi_acct_num", NullableString(caseLoan.AcctNum));
                sqlParam[4] = new SqlParameter("@pi_loan_1st_2nd_cd", NullableString(caseLoan.Loan1st2nd));
                sqlParam[5] = new SqlParameter("@pi_mortgage_type_cd", NullableString(caseLoan.MortgageTypeCd));                
                sqlParam[6] = new SqlParameter("@pi_arm_reset_ind", NullableString(caseLoan.ArmResetInd));
                sqlParam[7] = new SqlParameter("@pi_term_length_cd", NullableString(caseLoan.TermLengthCd));
                sqlParam[8] = new SqlParameter("@pi_loan_delinq_status_cd", NullableString(caseLoan.LoanDelinqStatusCd));
                sqlParam[9] = new SqlParameter("@pi_current_loan_balance_amt", caseLoan.CurrentLoanBalanceAmt);
                sqlParam[10] = new SqlParameter("@pi_orig_loan_amt", caseLoan.OrigLoanAmt);
                sqlParam[11] = new SqlParameter("@pi_interest_rate", caseLoan.InterestRate);
                sqlParam[12] = new SqlParameter("@pi_Originating_Lender_Name", NullableString(caseLoan.OriginatingLenderName));
                sqlParam[13] = new SqlParameter("@pi_orig_mortgage_co_FDIC_NCUA_num", NullableString(caseLoan.OrigMortgageCoFdicNcusNum));
                sqlParam[14] = new SqlParameter("@pi_Orig_mortgage_co_name", NullableString(caseLoan.OrigMortgageCoName));
                sqlParam[15] = new SqlParameter("@pi_Orginal_Loan_Num", NullableString(caseLoan.OrginalLoanNum));
                sqlParam[16] = new SqlParameter("@pi_current_servicer_FDIC_NCUA_num", NullableString(caseLoan.CurrentServicerFdicNcuaNum));
                sqlParam[17] = new SqlParameter("@pi_investor_num", NullableString(caseLoan.InvestorNum));
                sqlParam[18] = new SqlParameter("@pi_investor_name", NullableString(caseLoan.InvestorName));
                sqlParam[19] = new SqlParameter("@pi_mortgage_program_cd", NullableString(caseLoan.MortgageProgramCd));
                sqlParam[20] = new SqlParameter("@pi_changed_acct_num", NullableString(caseLoan.ChangedAcctNum));
                sqlParam[21] = new SqlParameter("@pi_create_dt", NullableDateTime(caseLoan.CreateDate));
                sqlParam[22] = new SqlParameter("@pi_create_user_id", caseLoan.CreateUserId);
                sqlParam[23] = new SqlParameter("@pi_create_app_name", caseLoan.CreateAppName);
                sqlParam[24] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(caseLoan.ChangeLastDate));
                sqlParam[25] = new SqlParameter("@pi_chg_lst_user_id", caseLoan.ChangeLastUserId);
                sqlParam[26] = new SqlParameter("@pi_chg_lst_app_name", caseLoan.ChangeLastAppName);

                sqlParam[27] = new SqlParameter("@pi_arm_rate_adjust_dt", caseLoan.ArmRateAdjustDt);
                sqlParam[28] = new SqlParameter("@pi_arm_lock_duration", caseLoan.ArmLockDuration);
                sqlParam[29] = new SqlParameter("@pi_loan_lookup_cd", caseLoan.LoanLookupCd);
                sqlParam[30] = new SqlParameter("@pi_thirty_days_late_past_yr_ind", caseLoan.ThirtyDaysLatePastYrInd);
                sqlParam[31] = new SqlParameter("@pi_pmt_miss_less_one_yr_loan_ind", caseLoan.PmtMissLessOneYrLoanInd);
                sqlParam[32] = new SqlParameter("@pi_sufficient_income_ind", caseLoan.SufficientIncomeInd);
                sqlParam[33] = new SqlParameter("@pi_long_term_afford_ind", caseLoan.LongTermAffordInd);
                sqlParam[34] = new SqlParameter("@pi_harp_eligible_ind", caseLoan.HarpEligibleInd);
                sqlParam[35] = new SqlParameter("@pi_orig_prior_to_2009_ind", caseLoan.OrigPriorTo2009Ind);
                sqlParam[36] = new SqlParameter("@pi_prior_hamp_ind", caseLoan.PriorHampInd);
                sqlParam[37] = new SqlParameter("@pi_prin_bal_within_limit_ind", caseLoan.PrinBalWithinLimitInd);
                sqlParam[38] = new SqlParameter("@pi_hamp_eligible_ind", caseLoan.HampEligibleInd);
                sqlParam[39] = new SqlParameter("@pi_loss_mit_status_cd", caseLoan.LossMitStatusCd);
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
        /// update Foreclosure Case only
        /// update Duplicate_Ind field only
        /// </summary>
        /// <param name="fcid">id of foreclosure case</param>
        /// <param name="ind">Y or N</param>
        public void UpdateFcCase_DuplicateIndicator(int? fcId, string ind)
        {
            //throw NotImplementedException;
            var command = CreateCommand("hpf_foreclosure_case_update_duplicate", this.dbConnection);
            try
            {
                var sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                sqlParam[1] = new SqlParameter("@pi_duplicate_ind", ind);

                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
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
                var sqlParam = new SqlParameter[34];
                sqlParam[0] = new SqlParameter("@pi_fc_id", caseLoan.FcId);
                sqlParam[1] = new SqlParameter("@pi_servicer_id", caseLoan.ServicerId);
                sqlParam[2] = new SqlParameter("@pi_other_servicer_name", NullableString(caseLoan.OtherServicerName));
                sqlParam[3] = new SqlParameter("@pi_acct_num", NullableString(caseLoan.AcctNum));
                sqlParam[4] = new SqlParameter("@pi_loan_1st_2nd_cd", NullableString(caseLoan.Loan1st2nd));
                sqlParam[5] = new SqlParameter("@pi_mortgage_type_cd", NullableString(caseLoan.MortgageTypeCd));
                sqlParam[6] = new SqlParameter("@pi_arm_reset_ind", NullableString(caseLoan.ArmResetInd));
                sqlParam[7] = new SqlParameter("@pi_term_length_cd", NullableString(caseLoan.TermLengthCd));
                sqlParam[8] = new SqlParameter("@pi_loan_delinq_status_cd", NullableString(caseLoan.LoanDelinqStatusCd));
                sqlParam[9] = new SqlParameter("@pi_current_loan_balance_amt", caseLoan.CurrentLoanBalanceAmt);
                sqlParam[10] = new SqlParameter("@pi_orig_loan_amt", caseLoan.OrigLoanAmt);
                sqlParam[11] = new SqlParameter("@pi_interest_rate", caseLoan.InterestRate);
                sqlParam[12] = new SqlParameter("@pi_Originating_Lender_Name", NullableString(caseLoan.OriginatingLenderName));
                sqlParam[13] = new SqlParameter("@pi_orig_mortgage_co_FDIC_NCUA_num", NullableString(caseLoan.OrigMortgageCoFdicNcusNum));
                sqlParam[14] = new SqlParameter("@pi_Orig_mortgage_co_name", NullableString(caseLoan.OrigMortgageCoName));
                sqlParam[15] = new SqlParameter("@pi_Orginal_Loan_Num", NullableString(caseLoan.OrginalLoanNum));
                sqlParam[16] = new SqlParameter("@pi_current_servicer_FDIC_NCUA_num", NullableString(caseLoan.CurrentServicerFdicNcuaNum));
                //sqlParam[17] = new SqlParameter("@pi_investor_num", NullableString(caseLoan.InvestorNum));
                //sqlParam[18] = new SqlParameter("@pi_investor_name", NullableString(caseLoan.InvestorName));
                sqlParam[17] = new SqlParameter("@pi_mortgage_program_cd", NullableString(caseLoan.MortgageProgramCd));
                sqlParam[18] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(caseLoan.ChangeLastDate));
                sqlParam[19] = new SqlParameter("@pi_chg_lst_user_id", caseLoan.ChangeLastUserId);
                sqlParam[20] = new SqlParameter("@pi_chg_lst_app_name", caseLoan.ChangeLastAppName);

                sqlParam[21] = new SqlParameter("@pi_arm_rate_adjust_dt", caseLoan.ArmRateAdjustDt);
                sqlParam[22] = new SqlParameter("@pi_arm_lock_duration", caseLoan.ArmLockDuration);
                sqlParam[23] = new SqlParameter("@pi_loan_lookup_cd", caseLoan.LoanLookupCd);
                sqlParam[24] = new SqlParameter("@pi_thirty_days_late_past_yr_ind", caseLoan.ThirtyDaysLatePastYrInd);
                sqlParam[25] = new SqlParameter("@pi_pmt_miss_less_one_yr_loan_ind", caseLoan.PmtMissLessOneYrLoanInd);
                sqlParam[26] = new SqlParameter("@pi_sufficient_income_ind", caseLoan.SufficientIncomeInd);
                sqlParam[27] = new SqlParameter("@pi_long_term_afford_ind", caseLoan.LongTermAffordInd);
                sqlParam[28] = new SqlParameter("@pi_harp_eligible_ind", caseLoan.HarpEligibleInd);
                sqlParam[29] = new SqlParameter("@pi_orig_prior_to_2009_ind", caseLoan.OrigPriorTo2009Ind);
                sqlParam[30] = new SqlParameter("@pi_prior_hamp_ind", caseLoan.PriorHampInd);
                sqlParam[31] = new SqlParameter("@pi_prin_bal_within_limit_ind", caseLoan.PrinBalWithinLimitInd);
                sqlParam[32] = new SqlParameter("@pi_hamp_eligible_ind", caseLoan.HampEligibleInd);
                sqlParam[33] = new SqlParameter("@pi_loss_mit_status_cd", caseLoan.LossMitStatusCd);
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
                var sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@pi_fc_id", caseLoan.FcId);                
                sqlParam[1] = new SqlParameter("@pi_acct_num", caseLoan.AcctNum);
                sqlParam[2] = new SqlParameter("@pi_servicer_id", caseLoan.ServicerId);
                sqlParam[3] = new SqlParameter("@pi_servicer_name", caseLoan.OtherServicerName);
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
        public void InsertOutcomeItem(OutcomeItemDTO outcomeItem, int? fcId)
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
                sqlParam[4] = new SqlParameter("@pi_nonprofitreferral_key_num", NullableString(outcomeItem.NonprofitreferralKeyNum));
                sqlParam[5] = new SqlParameter("@pi_ext_ref_other_name", NullableString(outcomeItem.ExtRefOtherName));
                sqlParam[6] = new SqlParameter("@pi_create_dt", NullableDateTime(outcomeItem.CreateDate));
                sqlParam[7] = new SqlParameter("@pi_create_user_id", outcomeItem.CreateUserId);
                sqlParam[8] = new SqlParameter("@pi_create_app_name", outcomeItem.CreateAppName);
                sqlParam[9] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(outcomeItem.ChangeLastDate));
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
        public int? InsertBudgetSet(BudgetSetDTO budgetSet, int? fcId)
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
                sqlParam[5] = new SqlParameter("@pi_create_dt", NullableDateTime(budgetSet.CreateDate));
                sqlParam[6] = new SqlParameter("@pi_create_user_id", budgetSet.CreateUserId);
                sqlParam[7] = new SqlParameter("@pi_create_app_name", budgetSet.CreateAppName);
                sqlParam[8] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(budgetSet.ChangeLastDate));
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
        /// Insert a BudgetSet to database.
        /// </summary>
        /// <param name="budgetSet">BudgetSetDTO</param>
        /// <returns></returns>
        public int? InsertProposedBudgetSet(BudgetSetDTO budgetSet, int? fcId)
        {
            var command = new SqlCommand("hpf_proposed_budget_set_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[12];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                sqlParam[1] = new SqlParameter("@pi_total_income", budgetSet.TotalIncome);
                sqlParam[2] = new SqlParameter("@pi_total_expenses", budgetSet.TotalExpenses);
                sqlParam[3] = new SqlParameter("@pi_total_assets", budgetSet.TotalAssets);
                sqlParam[4] = new SqlParameter("@pi_budget_set_dt", NullableDateTime(budgetSet.BudgetSetDt));
                sqlParam[5] = new SqlParameter("@pi_create_dt", NullableDateTime(budgetSet.CreateDate));
                sqlParam[6] = new SqlParameter("@pi_create_user_id", budgetSet.CreateUserId);
                sqlParam[7] = new SqlParameter("@pi_create_app_name", budgetSet.CreateAppName);
                sqlParam[8] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(budgetSet.ChangeLastDate));
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
        public void InsertBudgetItem(BudgetItemDTO budgetItem, int? budgetSetId)
        {            
            var command = CreateCommand("hpf_budget_item_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@pi_budget_set_id", budgetSetId);
                sqlParam[1] = new SqlParameter("@pi_budget_subcategory_id", budgetItem.BudgetSubcategoryId);
                sqlParam[2] = new SqlParameter("@pi_budget_item_amt", budgetItem.BudgetItemAmt);
                sqlParam[3] = new SqlParameter("@pi_budget_note", NullableString(budgetItem.BudgetNote));
                sqlParam[4] = new SqlParameter("@pi_create_dt", NullableDateTime(budgetItem.CreateDate));
                sqlParam[5] = new SqlParameter("@pi_create_user_id", budgetItem.CreateUserId);
                sqlParam[6] = new SqlParameter("@pi_create_app_name", budgetItem.CreateAppName);
                sqlParam[7] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(budgetItem.ChangeLastDate));
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
        /// Insert a BudgetItem to database.
        /// </summary>
        /// <param name="budgetItem">BudgetItemDTO</param>
        /// <returns></returns>
        public void InsertProposedBudgetItem(BudgetItemDTO budgetItem, int? budgetSetId)
        {
            var command = CreateCommand("hpf_proposed_budget_item_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@pi_budget_set_id", budgetSetId);
                sqlParam[1] = new SqlParameter("@pi_budget_subcategory_id", budgetItem.BudgetSubcategoryId);
                sqlParam[2] = new SqlParameter("@pi_budget_item_amt", budgetItem.BudgetItemAmt);
                sqlParam[3] = new SqlParameter("@pi_budget_note", NullableString(budgetItem.BudgetNote));
                sqlParam[4] = new SqlParameter("@pi_create_dt", NullableDateTime(budgetItem.CreateDate));
                sqlParam[5] = new SqlParameter("@pi_create_user_id", budgetItem.CreateUserId);
                sqlParam[6] = new SqlParameter("@pi_create_app_name", budgetItem.CreateAppName);
                sqlParam[7] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(budgetItem.ChangeLastDate));
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
        public void InsertBudgetAsset(BudgetAssetDTO budgetAsset, int? budgetSetId)
        {            
            var command = CreateCommand("hpf_budget_asset_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[9];
                sqlParam[0] = new SqlParameter("@pi_budget_set_id", budgetSetId);
                sqlParam[1] = new SqlParameter("@pi_asset_name", NullableString(budgetAsset.AssetName));
                sqlParam[2] = new SqlParameter("@pi_asset_value", budgetAsset.AssetValue);
                sqlParam[3] = new SqlParameter("@pi_create_dt", NullableDateTime(budgetAsset.CreateDate));
                sqlParam[4] = new SqlParameter("@pi_create_user_id", budgetAsset.CreateUserId);
                sqlParam[5] = new SqlParameter("@pi_create_app_name", budgetAsset.CreateAppName);
                sqlParam[6] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(budgetAsset.ChangeLastDate));
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
        /// Insert a Activity Log to database.
        /// </summary>
        /// <param name="ActivityItem">ActivityDTO</param>
        /// <returns></returns>
        public void InsertActivityLog(ActivityLogDTO activityLog, int? fcId)
        {
            var command = CreateCommand("hpf_activity_log_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                sqlParam[1] = new SqlParameter("@pi_activity_cd", activityLog.ActivityCd);
                sqlParam[2] = new SqlParameter("@pi_activity_dt", activityLog.ActivityDt);
                sqlParam[3] = new SqlParameter("@pi_activity_note", activityLog.ActivityNote);
                sqlParam[4] = new SqlParameter("@pi_create_dt", NullableDateTime(activityLog.CreateDate));
                sqlParam[5] = new SqlParameter("@pi_create_user_id", activityLog.CreateUserId);
                sqlParam[6] = new SqlParameter("@pi_create_app_name", activityLog.CreateAppName);
                sqlParam[7] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(activityLog.ChangeLastDate));
                sqlParam[8] = new SqlParameter("@pi_chg_lst_user_id", activityLog.ChangeLastUserId);
                sqlParam[9] = new SqlParameter("@pi_chg_lst_app_name", activityLog.ChangeLastAppName);
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
        public int? UpdateForeclosureCase(ForeclosureCaseDTO foreclosureCase)
        {            
            var command = CreateSPCommand("hpf_foreclosure_case_update", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[116];
                sqlParam[0] = new SqlParameter("@pi_agency_id", foreclosureCase.AgencyId);
                sqlParam[1] = new SqlParameter("@pi_completed_dt", NullableDateTime(foreclosureCase.CompletedDt));
                sqlParam[2] = new SqlParameter("@pi_call_id", foreclosureCase.CallId);
                sqlParam[3] = new SqlParameter("@pi_program_id", foreclosureCase.ProgramId);
                sqlParam[4] = new SqlParameter("@pi_agency_case_num", NullableString(foreclosureCase.AgencyCaseNum));
                sqlParam[5] = new SqlParameter("@pi_agency_client_num", NullableString(foreclosureCase.AgencyClientNum));
                sqlParam[6] = new SqlParameter("@pi_intake_dt", NullableDateTime(foreclosureCase.IntakeDt));
                sqlParam[7] = new SqlParameter("@pi_income_earners_cd", NullableString(foreclosureCase.IncomeEarnersCd));
                sqlParam[8] = new SqlParameter("@pi_case_source_cd", NullableString(foreclosureCase.CaseSourceCd));
                sqlParam[9] = new SqlParameter("@pi_race_cd", NullableString(foreclosureCase.RaceCd));
                sqlParam[10] = new SqlParameter("@pi_household_cd", NullableString(foreclosureCase.HouseholdCd));
                sqlParam[11] = new SqlParameter("@pi_never_bill_reason_cd", NullableString(foreclosureCase.NeverBillReasonCd));
                sqlParam[12] = new SqlParameter("@pi_never_pay_reason_cd", NullableString(foreclosureCase.NeverPayReasonCd));
                sqlParam[13] = new SqlParameter("@pi_dflt_reason_1st_cd", NullableString(foreclosureCase.DfltReason1stCd));
                sqlParam[14] = new SqlParameter("@pi_dflt_reason_2nd_cd", NullableString(foreclosureCase.DfltReason2ndCd));
                sqlParam[15] = new SqlParameter("@pi_hud_termination_reason_cd", NullableString(foreclosureCase.HudTerminationReasonCd));
                sqlParam[16] = new SqlParameter("@pi_hud_termination_dt", NullableDateTime(foreclosureCase.HudTerminationDt));
                sqlParam[17] = new SqlParameter("@pi_hud_outcome_cd", NullableString(foreclosureCase.HudOutcomeCd));
                sqlParam[18] = new SqlParameter("@pi_counseling_duration_cd", NullableString(foreclosureCase.CounselingDurationCd));
                sqlParam[19] = new SqlParameter("@pi_gender_cd", NullableString(foreclosureCase.GenderCd));
                sqlParam[20] = new SqlParameter("@pi_borrower_fname", NullableString(foreclosureCase.BorrowerFname));
                sqlParam[21] = new SqlParameter("@pi_borrower_lname", NullableString(foreclosureCase.BorrowerLname));
                sqlParam[22] = new SqlParameter("@pi_borrower_mname", NullableString(foreclosureCase.BorrowerMname));
                sqlParam[23] = new SqlParameter("@pi_mother_maiden_lname", NullableString(foreclosureCase.MotherMaidenLname));
                sqlParam[24] = new SqlParameter("@pi_borrower_ssn", NullableString(foreclosureCase.BorrowerSsn));
                sqlParam[25] = new SqlParameter("@pi_borrower_last4_SSN", NullableString(foreclosureCase.BorrowerLast4Ssn));
                sqlParam[26] = new SqlParameter("@pi_borrower_DOB", NullableDateTime(foreclosureCase.BorrowerDob));
                sqlParam[27] = new SqlParameter("@pi_co_borrower_fname", NullableString(foreclosureCase.CoBorrowerFname));
                sqlParam[28] = new SqlParameter("@pi_co_borrower_lname", NullableString(foreclosureCase.CoBorrowerLname));
                sqlParam[29] = new SqlParameter("@pi_co_borrower_mname", NullableString(foreclosureCase.CoBorrowerMname));
                sqlParam[30] = new SqlParameter("@pi_co_borrower_ssn", NullableString(foreclosureCase.CoBorrowerSsn));
                sqlParam[31] = new SqlParameter("@pi_co_borrower_last4_SSN", NullableString(foreclosureCase.CoBorrowerLast4Ssn));
                sqlParam[32] = new SqlParameter("@pi_co_borrower_DOB", NullableDateTime(foreclosureCase.CoBorrowerDob));
                sqlParam[33] = new SqlParameter("@pi_primary_contact_no", NullableString(foreclosureCase.PrimaryContactNo));
                sqlParam[34] = new SqlParameter("@pi_second_contact_no", NullableString(foreclosureCase.SecondContactNo));
                sqlParam[35] = new SqlParameter("@pi_email_1", NullableString(foreclosureCase.Email1));
                sqlParam[36] = new SqlParameter("@pi_email_2", NullableString(foreclosureCase.Email2));
                sqlParam[37] = new SqlParameter("@pi_contact_addr1", NullableString(foreclosureCase.ContactAddr1));
                sqlParam[38] = new SqlParameter("@pi_contact_addr2", NullableString(foreclosureCase.ContactAddr2));
                sqlParam[39] = new SqlParameter("@pi_contact_city", NullableString(foreclosureCase.ContactCity));
                sqlParam[40] = new SqlParameter("@pi_contact_state_cd", NullableString(foreclosureCase.ContactStateCd));
                sqlParam[41] = new SqlParameter("@pi_contact_zip", NullableString(foreclosureCase.ContactZip));
                sqlParam[42] = new SqlParameter("@pi_contact_zip_plus4", NullableString(foreclosureCase.ContactZipPlus4));
                sqlParam[43] = new SqlParameter("@pi_prop_addr1", NullableString(foreclosureCase.PropAddr1));
                sqlParam[44] = new SqlParameter("@pi_prop_addr2", NullableString(foreclosureCase.PropAddr2));
                sqlParam[45] = new SqlParameter("@pi_prop_city", NullableString(foreclosureCase.PropCity));
                sqlParam[46] = new SqlParameter("@pi_prop_state_cd", NullableString(foreclosureCase.PropStateCd));
                sqlParam[47] = new SqlParameter("@pi_prop_zip", NullableString(foreclosureCase.PropZip));
                sqlParam[48] = new SqlParameter("@pi_prop_zip_plus_4", NullableString(foreclosureCase.PropZipPlus4));
                sqlParam[49] = new SqlParameter("@pi_bankruptcy_ind", NullableString(foreclosureCase.BankruptcyInd));
                sqlParam[50] = new SqlParameter("@pi_bankruptcy_attorney", NullableString(foreclosureCase.BankruptcyAttorney));
                sqlParam[51] = new SqlParameter("@pi_bankruptcy_pmt_current_ind", NullableString(foreclosureCase.BankruptcyPmtCurrentInd));
                sqlParam[52] = new SqlParameter("@pi_borrower_educ_level_completed_cd", NullableString(foreclosureCase.BorrowerEducLevelCompletedCd));
                sqlParam[53] = new SqlParameter("@pi_borrower_marital_status_cd", NullableString(foreclosureCase.BorrowerMaritalStatusCd));
                sqlParam[54] = new SqlParameter("@pi_borrower_preferred_lang_cd", NullableString(foreclosureCase.BorrowerPreferredLangCd));
                sqlParam[55] = new SqlParameter("@pi_borrower_occupation", NullableString(foreclosureCase.BorrowerOccupation));
                sqlParam[56] = new SqlParameter("@pi_co_borrower_occupation", NullableString(foreclosureCase.CoBorrowerOccupation));
                sqlParam[57] = new SqlParameter("@pi_owner_occupied_ind", NullableString(foreclosureCase.OwnerOccupiedInd));
                sqlParam[58] = new SqlParameter("@pi_hispanic_ind", NullableString(foreclosureCase.HispanicInd));
                sqlParam[59] = new SqlParameter("@pi_duplicate_ind", NullableString(foreclosureCase.DuplicateInd));
                sqlParam[60] = new SqlParameter("@pi_fc_notice_received_ind", NullableString(foreclosureCase.FcNoticeReceiveInd));
                sqlParam[61] = new SqlParameter("@pi_funding_consent_ind", NullableString(foreclosureCase.FundingConsentInd));
                sqlParam[62] = new SqlParameter("@pi_servicer_consent_ind", NullableString(foreclosureCase.ServicerConsentInd));
                sqlParam[63] = new SqlParameter("@pi_agency_media_interest_ind", NullableString(foreclosureCase.AgencyMediaInterestInd));
                sqlParam[64] = new SqlParameter("@pi_agency_success_story_ind", NullableString(foreclosureCase.AgencySuccessStoryInd));
                sqlParam[65] = new SqlParameter("@pi_borrower_disabled_ind", NullableString(foreclosureCase.BorrowerDisabledInd));
                sqlParam[66] = new SqlParameter("@pi_co_borrower_disabled_ind", NullableString(foreclosureCase.CoBorrowerDisabledInd));
                sqlParam[67] = new SqlParameter("@pi_summary_sent_other_cd", NullableString(foreclosureCase.SummarySentOtherCd));
                sqlParam[68] = new SqlParameter("@pi_summary_sent_other_dt", NullableDateTime(foreclosureCase.SummarySentOtherDt));
                sqlParam[69] = new SqlParameter("@pi_summary_sent_dt", NullableDateTime(foreclosureCase.SummarySentDt));
                sqlParam[70] = new SqlParameter("@pi_occupant_num", foreclosureCase.OccupantNum);
                sqlParam[71] = new SqlParameter("@pi_loan_dflt_reason_notes", NullableString(foreclosureCase.LoanDfltReasonNotes));
                sqlParam[72] = new SqlParameter("@pi_action_items_notes", NullableString(foreclosureCase.ActionItemsNotes));
                sqlParam[73] = new SqlParameter("@pi_followup_notes", NullableString(foreclosureCase.FollowupNotes));
                sqlParam[74] = new SqlParameter("@pi_prim_res_est_mkt_value", foreclosureCase.PrimResEstMktValue);
                sqlParam[75] = new SqlParameter("@pi_counselor_id_ref", NullableString(foreclosureCase.AssignedCounselorIdRef));
                sqlParam[76] = new SqlParameter("@pi_counselor_lname", NullableString(foreclosureCase.CounselorLname));
                sqlParam[77] = new SqlParameter("@pi_counselor_fname", NullableString(foreclosureCase.CounselorFname));
                sqlParam[78] = new SqlParameter("@pi_counselor_email", NullableString(foreclosureCase.CounselorEmail));
                sqlParam[79] = new SqlParameter("@pi_counselor_phone", NullableString(foreclosureCase.CounselorPhone));
                sqlParam[80] = new SqlParameter("@pi_counselor_ext", NullableString(foreclosureCase.CounselorExt));
                sqlParam[81] = new SqlParameter("@pi_discussed_solution_with_srvcr_ind", NullableString(foreclosureCase.DiscussedSolutionWithSrvcrInd));
                sqlParam[82] = new SqlParameter("@pi_worked_with_another_agency_ind", NullableString(foreclosureCase.WorkedWithAnotherAgencyInd));
                sqlParam[83] = new SqlParameter("@pi_contacted_srvcr_recently_ind", NullableString(foreclosureCase.ContactedSrvcrRecentlyInd));
                sqlParam[84] = new SqlParameter("@pi_has_workout_plan_ind", NullableString(foreclosureCase.HasWorkoutPlanInd));
                sqlParam[85] = new SqlParameter("@pi_srvcr_workout_plan_current_ind", NullableString(foreclosureCase.SrvcrWorkoutPlanCurrentInd));
                sqlParam[86] = new SqlParameter("@pi_primary_residence_ind", NullableString(foreclosureCase.PrimaryResidenceInd));
                sqlParam[87] = new SqlParameter("@pi_realty_company", NullableString(foreclosureCase.RealtyCompany));
                sqlParam[88] = new SqlParameter("@pi_property_cd", NullableString(foreclosureCase.PropertyCd));
                sqlParam[89] = new SqlParameter("@pi_for_sale_ind", NullableString(foreclosureCase.ForSaleInd));
                sqlParam[90] = new SqlParameter("@pi_home_sale_price", foreclosureCase.HomeSalePrice);
                sqlParam[91] = new SqlParameter("@pi_home_purchase_year", foreclosureCase.HomePurchaseYear);
                sqlParam[92] = new SqlParameter("@pi_home_purchase_price", foreclosureCase.HomePurchasePrice);
                sqlParam[93] = new SqlParameter("@pi_Home_Current_Market_Value", foreclosureCase.HomeCurrentMarketValue);
                sqlParam[94] = new SqlParameter("@pi_military_service_cd", NullableString(foreclosureCase.MilitaryServiceCd));
                sqlParam[95] = new SqlParameter("@pi_household_gross_annual_income_amt", foreclosureCase.HouseholdGrossAnnualIncomeAmt);
                sqlParam[96] = new SqlParameter("@pi_intake_credit_score", NullableString(foreclosureCase.IntakeCreditScore));
                sqlParam[97] = new SqlParameter("@pi_Intake_credit_bureau_cd ", NullableString(foreclosureCase.IntakeCreditBureauCd));
                sqlParam[98] = new SqlParameter("@pi_fc_sale_dt", NullableDateTime(foreclosureCase.FcSaleDate));
                sqlParam[99] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(foreclosureCase.ChangeLastDate));
                sqlParam[100] = new SqlParameter("@pi_chg_lst_user_id", foreclosureCase.ChangeLastUserId);
                sqlParam[101] = new SqlParameter("@pi_chg_lst_app_name", foreclosureCase.ChangeLastAppName);                

                sqlParam[102] = new SqlParameter("@pi_vip_ind", foreclosureCase.VipInd);
                sqlParam[103] = new SqlParameter("@pi_vip_reason", foreclosureCase.VipReason);
                sqlParam[104] = new SqlParameter("@pi_counseled_language_cd", foreclosureCase.CounseledLanguageCd);
                sqlParam[105] = new SqlParameter("@pi_ercp_outcome_cd", foreclosureCase.ErcpOutcomeCd);
                sqlParam[106] = new SqlParameter("@pi_counselor_contacted_srvcr_ind", foreclosureCase.CounselorContactedSrvcrInd);
                sqlParam[107] = new SqlParameter("@pi_number_of_units", foreclosureCase.NumberOfUnits);
                sqlParam[108] = new SqlParameter("@pi_vacant_or_condemed_ind", foreclosureCase.VacantOrCondemedInd);
                sqlParam[109] = new SqlParameter("@pi_mortgage_pmt_ratio", foreclosureCase.MortgagePmtRatio);
                sqlParam[110] = new SqlParameter("@pi_certificate_id", foreclosureCase.CertificateId);

                sqlParam[111] = new SqlParameter("@pi_counseled_program_id", foreclosureCase.CounseledProgramId);
                sqlParam[112] = new SqlParameter("@pi_referral_client_num", foreclosureCase.ReferralClientNum);
                sqlParam[113] = new SqlParameter("@pi_sponsor_id", foreclosureCase.SponsorId);
                sqlParam[114] = new SqlParameter("@pi_campaign_id", foreclosureCase.CampaignId);

                sqlParam[115] = new SqlParameter("@pi_fc_id", foreclosureCase.FcId);

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
                sqlParam[1] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(outcomeItem.ChangeLastDate));
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
      
        /// <summary>
        /// Get ID and Name from table Program
        /// </summary>
        /// <returns>ProgramDTOCollection contains all Program </returns>
        public ProgramDTOCollection GetProgram()
        {
            ProgramDTOCollection results = HPFCacheManager.Instance.GetData<ProgramDTOCollection>(Constant.HPF_CACHE_PROGRAM);            
            if (results == null)
            {
                var dbConnection = CreateConnection();
                var command = new SqlCommand("hpf_program_get", dbConnection);
                try
                {
                    command.CommandType = CommandType.StoredProcedure;                    
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new ProgramDTOCollection();
                        while (reader.Read())
                        {
                            var item = new ProgramDTO();
                            item.ProgramID = ConvertToString(reader["program_id"]);
                            item.ProgramName = ConvertToString(reader["program_name"]);
                            if (item.ProgramID != "-1")
                                results.Add(item);
                        }
                        reader.Close();
                    }
                    HPFCacheManager.Instance.Add(Constant.HPF_CACHE_PROGRAM, results);
                }
                catch (Exception ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(ex);
                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return results;
        }                
              
        public DateTime? GetSummarySendDt(int? fcId)
        {
            DateTime? returnDt = null;
            var dbConnection = CreateConnection();
            try
            {
                SqlCommand command = base.CreateCommand("hpf_foreclosure_case_detail_get", dbConnection);
                //<Parameter>
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        returnDt = ConvertToDateTime(reader["summary_sent_dt"]);
                    }                    
                }
                reader.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return returnDt;
        }        

        public bool CheckExistingAgencyIdAndCaseNumber(int? agency_id, string agency_case_number)
        {
            bool returnValue = true;
            var dbConnection = CreateConnection();
            try
            {                
                SqlCommand command = base.CreateCommand("hpf_foreclosure_case_get_from_agencyID_and_caseNumber", dbConnection);//new SqlCommand("hpf_foreclosure_case_get_from_agencyID_and_caseNumber", dbConnection);
                //<Parameter>
                var sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@pi_agency_case_num", agency_case_number);
                sqlParam[1] = new SqlParameter("@pi_agency_id", agency_id);
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                var reader = command.ExecuteReader();
                returnValue = reader.HasRows;
                reader.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return returnValue;

        }        

        public DuplicatedCaseLoanDTOCollection GetDuplicatedCases(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            DuplicatedCaseLoanDTOCollection returnCollection = new DuplicatedCaseLoanDTOCollection();
            var dbConnection = CreateConnection();
            try
            {
                ForeclosureCaseDTO fcCase = foreclosureCaseSet.ForeclosureCase;                
                SqlCommand command = base.CreateCommand("hpf_foreclosure_case_get_duplicate", dbConnection);
                //<Parameter>
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@pi_agency_case_num", fcCase.AgencyCaseNum);
                sqlParam[1] = new SqlParameter("@pi_agency_id", fcCase.AgencyId);
                sqlParam[2] = new SqlParameter("@pi_fc_id", NullableInteger(fcCase.FcId));
                sqlParam[3] = new SqlParameter("@pi_where_str", GenerateGetDuplicatedCaseWhereClause(foreclosureCaseSet));
                //</Parameter>

                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    returnCollection = new DuplicatedCaseLoanDTOCollection();
                    while (reader.Read())
                    {
                        DuplicatedCaseLoanDTO obj = new DuplicatedCaseLoanDTO();
                        obj.LoanNumber = ConvertToString(reader["Acct_num"]);
                        obj.FcID = ConvertToInt(reader["FC_ID"]);
                        obj.FcCompletedDt = ConvertToDateTime(reader["Completed_dt"]);
                        obj.ServicerID = ConvertToInt(reader["Servicer_ID"]);
                        obj.AgencyCaseNumber = ConvertToString(reader["Agency_Case_Num"]);
                        obj.AgencyName = ConvertToString(reader["Agency_Name"]);
                        obj.BorrowerFirstName = ConvertToString(reader["borrower_fname"]);
                        obj.BorrowerLastName = ConvertToString(reader["borrower_lname"]);
                        obj.CounselorEmail = ConvertToString(reader["counselor_email"]);
                        obj.CounselorFName = ConvertToString(reader["counselor_fname"]);
                        obj.CounselorLName = ConvertToString(reader["counselor_lname"]);
                        obj.CounselorPhone = ConvertToString(reader["counselor_phone"]);
                        obj.CounselorExt = ConvertToString(reader["counselor_ext"]);
                        obj.ServicerName = ConvertToString(reader["servicer_name"]);
                        obj.PropertyZip = ConvertToString(reader["prop_zip"]);
                        obj.OutcomeDt = ConvertToDateTime(reader["outcome_dt"]);
                        obj.OutcomeTypeCode = ConvertToString(reader["outcome_type_name"]);

                        returnCollection.Add(obj);
                    }
                }
                reader.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return returnCollection;
        }

        private string GenerateGetDuplicatedCaseWhereClause(ForeclosureCaseSetDTO fcCaseSet)
        {
            StringBuilder sb = new StringBuilder();
            string orString = " or ";
            foreach (CaseLoanDTO item in fcCaseSet.CaseLoans)
            {
                if (item.ServicerId == Constant.SERVICER_OTHER_ID)
                {
                    if(string.IsNullOrEmpty(item.OtherServicerName))                        
                        sb.Append(string.Format(" (acct_num = '{0}' and CL.servicer_id = {1} and CL.other_servicer_name is NULL)", item.AcctNum, item.ServicerId));
                    else
                        sb.Append(string.Format(" (acct_num = '{0}' and CL.servicer_id = {1} and CL.other_servicer_name='{2}')", item.AcctNum, item.ServicerId, item.OtherServicerName.Replace("'", "''")));
                }
                else
                    sb.Append(string.Format(" (acct_num = '{0}' and CL.servicer_id = {1})", item.AcctNum, item.ServicerId));
                sb.Append(orString);
            }
            
            sb.Remove(sb.Length - orString.Length, orString.Length);
            return sb.ToString();
        }

        /// <summary>
        /// Update a ForeclosureCase to database.
        /// </summary>
        /// <param name="foreclosureCase">ForeclosureCase</param>
        /// <returns>a new Fc_id</returns>
        public void UpdateSendSummaryDate(int? fcId)
        {
            var dbConnection = CreateConnection();
            try
            {
                var command = CreateSPCommand("hpf_foreclosure_case_update_ws", dbConnection);
                //<Parameter>            
                var sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                sqlParam[1] = new SqlParameter("@pi_summary_sent_dt", DateTime.Now);
                sqlParam[2] = new SqlParameter("@chg_lst_dt", DateTime.Now);                
                //</Parameter> 
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                dbConnection.Close();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }            
        }

        public ForeclosureCaseCallSearchResultDTOCollection GetForclosureCaseFromCall(int callId)
        {
            var dbConnection = CreateConnection();
            ForeclosureCaseCallSearchResultDTOCollection result = new ForeclosureCaseCallSearchResultDTOCollection();
            try
            {
                var command = CreateSPCommand("hpf_foreclosure_case_detail_get_call", dbConnection);
                //<Parameter>            
                var sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@pi_call_id", callId);                
                //</Parameter> 
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ForeclosureCaseCallSearchResultDTO itemRead = new ForeclosureCaseCallSearchResultDTO();
                    itemRead.AccountNum = ConvertToString(reader["acct_num"]);
                    itemRead.AgencyName = ConvertToString(reader["agency_name"]);
                    itemRead.BorrowFName = ConvertToString(reader["borrower_fname"]);
                    itemRead.BorrowLName = ConvertToString(reader["borrower_lname"]);
                    itemRead.CounselorEmail = ConvertToString(reader["counselor_email"]);
                    itemRead.CounselorExt = ConvertToString(reader["counselor_ext"]);
                    itemRead.CounselorFName = ConvertToString(reader["counselor_fname"]);
                    itemRead.CounselorPhone = ConvertToString(reader["counselor_phone"]);
                    itemRead.CountselorLName = ConvertToString(reader["counselor_lname"]);
                    itemRead.OutcomeDate = ConvertToDateTime(reader["outcome_dt"]).Value;
                    itemRead.OutcomeTypeName = ConvertToString(reader["outcome_type_name"]);
                    itemRead.PropZip = ConvertToString(reader["prop_zip"]);
                    itemRead.ServicerName = ConvertToString(reader["servicer_name"]);
                    result.Add(itemRead);
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

        public int MarkDuplicateCases(string fcIdList, string updateUser, string dupeInd, string neverBillCd, string neverPayCd)
        {
            int rowCount = 0;            
            var command = CreateSPCommand("hpf_foreclosure_case_mark_duplicate", this.dbConnection);
            var sqlParam = new SqlParameter[8];

            sqlParam[0] = new SqlParameter("@pi_fc_id_list", fcIdList);
            sqlParam[1] = new SqlParameter("@pi_duplicate_ind", dupeInd);
            sqlParam[2] = new SqlParameter("@pi_never_bill_reason_cd", neverBillCd);
            sqlParam[3] = new SqlParameter("@pi_never_pay_reason_cd", neverPayCd);
            sqlParam[4] = new SqlParameter("@pi_change_app_name", HPFConfigurationSettings.HPF_APPLICATION_NAME);
            sqlParam[5] = new SqlParameter("@pi_change_user_id", updateUser);
            sqlParam[6] = new SqlParameter("@pi_update_partial_case", true);
            sqlParam[7] = new SqlParameter("@po_row_count", SqlDbType.Int) { Direction = ParameterDirection.Output };

            try
            {
                command.Parameters.AddRange(sqlParam);                
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans; 
                command.ExecuteNonQuery();
                rowCount = ConvertToInt(sqlParam[7].Value).Value;
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }            

            return rowCount;
        }
    }
}


