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
        private ForeclosureCaseDTO GetForeclosureCase(int fcId)
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
                        returnObject.BorrowerOccupationCd = ConvertToString(reader["borrower_occupation"]);
                        returnObject.BorrowerPreferredLangCd = ConvertToString(reader["borrower_preferred_lang_cd"]);
                        //returnObject.BorrowerSsn = ConvertToString(reader["borrower_ssn"]);

                        returnObject.CallId = ConvertToInt(reader["call_id"]);                        
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
                        #endregion
                    }
                    reader.Close();
                }
                else
                    returnObject = null;

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
        public int InsertForeclosureCase(ForeclosureCaseDTO foreclosureCase)
        {
            var command = CreateSPCommand("hpf_foreclosure_case_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[104];
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
                sqlParam[11] = new SqlParameter("@pi_dflt_reason_1st_cd", foreclosureCase.DfltReason1stCd);
                sqlParam[12] = new SqlParameter("@pi_dflt_reason_2nd_cd", foreclosureCase.DfltReason2ndCd);
                sqlParam[13] = new SqlParameter("@pi_hud_termination_reason_cd", foreclosureCase.HudTerminationReasonCd);
                sqlParam[14] = new SqlParameter("@pi_hud_termination_dt", NullableDateTime(foreclosureCase.HudTerminationDt));
                sqlParam[15] = new SqlParameter("@pi_hud_outcome_cd", foreclosureCase.HudOutcomeCd);
                sqlParam[16] = new SqlParameter("@pi_AMI_percentage", foreclosureCase.AmiPercentage);
                sqlParam[17] = new SqlParameter("@pi_counseling_duration_cd", foreclosureCase.CounselingDurationCd);
                sqlParam[18] = new SqlParameter("@pi_gender_cd", foreclosureCase.GenderCd);
                sqlParam[19] = new SqlParameter("@pi_borrower_fname", foreclosureCase.BorrowerFname);
                sqlParam[20] = new SqlParameter("@pi_borrower_lname", foreclosureCase.BorrowerLname);
                sqlParam[21] = new SqlParameter("@pi_borrower_mname", foreclosureCase.BorrowerMname);
                sqlParam[22] = new SqlParameter("@pi_mother_maiden_lname", foreclosureCase.MotherMaidenLname);
                sqlParam[23] = new SqlParameter("@pi_borrower_ssn", foreclosureCase.BorrowerSsn);
                sqlParam[24] = new SqlParameter("@pi_borrower_last4_SSN", foreclosureCase.BorrowerLast4Ssn);
                sqlParam[25] = new SqlParameter("@pi_borrower_DOB", NullableDateTime(foreclosureCase.BorrowerDob));
                sqlParam[26] = new SqlParameter("@pi_co_borrower_fname", foreclosureCase.CoBorrowerFname);
                sqlParam[27] = new SqlParameter("@pi_co_borrower_lname", foreclosureCase.CoBorrowerLname);
                sqlParam[28] = new SqlParameter("@pi_co_borrower_mname", foreclosureCase.CoBorrowerMname);
                sqlParam[29] = new SqlParameter("@pi_co_borrower_ssn", foreclosureCase.CoBorrowerSsn);
                sqlParam[30] = new SqlParameter("@pi_co_borrower_last4_SSN", foreclosureCase.CoBorrowerLast4Ssn);
                sqlParam[31] = new SqlParameter("@pi_co_borrower_DOB", NullableDateTime(foreclosureCase.CoBorrowerDob));
                sqlParam[32] = new SqlParameter("@pi_primary_contact_no", foreclosureCase.PrimaryContactNo);
                sqlParam[33] = new SqlParameter("@pi_second_contact_no", foreclosureCase.SecondContactNo);
                sqlParam[34] = new SqlParameter("@pi_email_1", foreclosureCase.Email1);
                sqlParam[35] = new SqlParameter("@pi_email_2", foreclosureCase.Email2);
                sqlParam[36] = new SqlParameter("@pi_contact_addr1", foreclosureCase.ContactAddr1);
                sqlParam[37] = new SqlParameter("@pi_contact_addr2", foreclosureCase.ContactAddr2);
                sqlParam[38] = new SqlParameter("@pi_contact_city", foreclosureCase.ContactCity);
                sqlParam[39] = new SqlParameter("@pi_contact_state_cd", foreclosureCase.ContactStateCd);
                sqlParam[40] = new SqlParameter("@pi_contact_zip", foreclosureCase.ContactZip);
                sqlParam[41] = new SqlParameter("@pi_contact_zip_plus4", foreclosureCase.ContactZipPlus4);
                sqlParam[42] = new SqlParameter("@pi_prop_addr1", foreclosureCase.PropAddr1);
                sqlParam[43] = new SqlParameter("@pi_prop_addr2", foreclosureCase.PropAddr2);
                sqlParam[44] = new SqlParameter("@pi_prop_city", foreclosureCase.PropCity);
                sqlParam[45] = new SqlParameter("@pi_prop_state_cd", foreclosureCase.PropStateCd);
                sqlParam[46] = new SqlParameter("@pi_prop_zip", foreclosureCase.PropZip);
                sqlParam[47] = new SqlParameter("@pi_prop_zip_plus_4", foreclosureCase.PropZipPlus4);
                sqlParam[48] = new SqlParameter("@pi_bankruptcy_ind", foreclosureCase.BankruptcyInd);
                sqlParam[49] = new SqlParameter("@pi_bankruptcy_attorney", foreclosureCase.BankruptcyAttorney);
                sqlParam[50] = new SqlParameter("@pi_bankruptcy_pmt_current_ind", foreclosureCase.BankruptcyPmtCurrentInd);
                sqlParam[51] = new SqlParameter("@pi_borrower_educ_level_completed_cd", foreclosureCase.BorrowerEducLevelCompletedCd);
                sqlParam[52] = new SqlParameter("@pi_borrower_marital_status_cd", foreclosureCase.BorrowerMaritalStatusCd);
                sqlParam[53] = new SqlParameter("@pi_borrower_preferred_lang_cd", foreclosureCase.BorrowerPreferredLangCd);
                sqlParam[54] = new SqlParameter("@pi_borrower_occupation", foreclosureCase.BorrowerOccupationCd);
                sqlParam[55] = new SqlParameter("@pi_co_borrower_occupation", foreclosureCase.CoBorrowerOccupationCd);
                sqlParam[56] = new SqlParameter("@pi_owner_occupied_ind", foreclosureCase.OwnerOccupiedInd);
                sqlParam[57] = new SqlParameter("@pi_hispanic_ind", foreclosureCase.HispanicInd);
                sqlParam[58] = new SqlParameter("@pi_duplicate_ind", foreclosureCase.DuplicateInd);
                sqlParam[59] = new SqlParameter("@pi_fc_notice_received_ind", foreclosureCase.FcNoticeReceiveInd);
                sqlParam[60] = new SqlParameter("@pi_funding_consent_ind", foreclosureCase.FundingConsentInd);
                sqlParam[61] = new SqlParameter("@pi_servicer_consent_ind", foreclosureCase.ServicerConsentInd);
                sqlParam[62] = new SqlParameter("@pi_agency_media_interest_ind", foreclosureCase.AgencyMediaInterestInd);
                sqlParam[63] = new SqlParameter("@pi_agency_success_story_ind", foreclosureCase.AgencySuccessStoryInd);
                sqlParam[64] = new SqlParameter("@pi_borrower_disabled_ind", foreclosureCase.BorrowerDisabledInd);
                sqlParam[65] = new SqlParameter("@pi_co_borrower_disabled_ind", foreclosureCase.CoBorrowerDisabledInd);
                sqlParam[66] = new SqlParameter("@pi_summary_sent_other_cd", foreclosureCase.SummarySentOtherCd);
                sqlParam[67] = new SqlParameter("@pi_summary_sent_other_dt", NullableDateTime(foreclosureCase.SummarySentOtherDt));
                sqlParam[68] = new SqlParameter("@pi_summary_sent_dt", NullableDateTime(foreclosureCase.SummarySentDt));
                sqlParam[69] = new SqlParameter("@pi_occupant_num", foreclosureCase.OccupantNum);
                sqlParam[70] = new SqlParameter("@pi_loan_dflt_reason_notes", foreclosureCase.LoanDfltReasonNotes);
                sqlParam[71] = new SqlParameter("@pi_action_items_notes", foreclosureCase.ActionItemsNotes);
                sqlParam[72] = new SqlParameter("@pi_followup_notes", foreclosureCase.FollowupNotes);
                sqlParam[73] = new SqlParameter("@pi_prim_res_est_mkt_value", foreclosureCase.PrimResEstMktValue);
                sqlParam[74] = new SqlParameter("@pi_counselor_id_ref", foreclosureCase.AssignedCounselorIdRef);
                sqlParam[75] = new SqlParameter("@pi_counselor_lname", foreclosureCase.CounselorFname);
                sqlParam[76] = new SqlParameter("@pi_counselor_fname", foreclosureCase.CounselorLname);
                sqlParam[77] = new SqlParameter("@pi_counselor_email", foreclosureCase.CounselorEmail);
                sqlParam[78] = new SqlParameter("@pi_counselor_phone", foreclosureCase.CounselorPhone);
                sqlParam[79] = new SqlParameter("@pi_counselor_ext", foreclosureCase.CounselorExt);
                sqlParam[80] = new SqlParameter("@pi_discussed_solution_with_srvcr_ind", foreclosureCase.DiscussedSolutionWithSrvcrInd);
                sqlParam[81] = new SqlParameter("@pi_worked_with_another_agency_ind", foreclosureCase.WorkedWithAnotherAgencyInd);
                sqlParam[82] = new SqlParameter("@pi_contacted_srvcr_recently_ind", foreclosureCase.ContactedSrvcrRecentlyInd);
                sqlParam[83] = new SqlParameter("@pi_has_workout_plan_ind", foreclosureCase.HasWorkoutPlanInd);
                sqlParam[84] = new SqlParameter("@pi_srvcr_workout_plan_current_ind", foreclosureCase.SrvcrWorkoutPlanCurrentInd);
                sqlParam[85] = new SqlParameter("@pi_primary_residence_ind", foreclosureCase.PrimaryResidenceInd);
                sqlParam[86] = new SqlParameter("@pi_realty_company", foreclosureCase.RealtyCompany);
                sqlParam[87] = new SqlParameter("@pi_property_cd", foreclosureCase.PropertyCd);
                sqlParam[88] = new SqlParameter("@pi_for_sale_ind", foreclosureCase.ForSaleInd);
                sqlParam[89] = new SqlParameter("@pi_home_sale_price", foreclosureCase.HomeSalePrice);
                sqlParam[90] = new SqlParameter("@pi_home_purchase_year", foreclosureCase.HomePurchaseYear);
                sqlParam[91] = new SqlParameter("@pi_home_purchase_price", foreclosureCase.HomePurchasePrice);
                sqlParam[92] = new SqlParameter("@pi_Home_Current_Market_Value", foreclosureCase.HomeCurrentMarketValue);
                sqlParam[93] = new SqlParameter("@pi_military_service_cd", foreclosureCase.MilitaryServiceCd);
                sqlParam[94] = new SqlParameter("@pi_household_gross_annual_income_amt", foreclosureCase.HouseholdGrossAnnualIncomeAmt);
                sqlParam[95] = new SqlParameter("@pi_intake_credit_score", foreclosureCase.IntakeCreditScore);
                sqlParam[96] = new SqlParameter("@pi_Intake_credit_bureau_cd ", foreclosureCase.IntakeCreditBureauCd);
                sqlParam[97] = new SqlParameter("@pi_create_dt", NullableDateTime(foreclosureCase.CreateDate));
                sqlParam[98] = new SqlParameter("@pi_create_user_id", foreclosureCase.CreateUserId);
                sqlParam[99] = new SqlParameter("@pi_create_app_name", foreclosureCase.CreateAppName);
                sqlParam[100] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(foreclosureCase.ChangeLastDate));
                sqlParam[101] = new SqlParameter("@pi_chg_lst_user_id", foreclosureCase.ChangeLastUserId);
                sqlParam[102] = new SqlParameter("@pi_chg_lst_app_name", foreclosureCase.ChangeLastAppName);
                sqlParam[103] = new SqlParameter("@po_fc_id", SqlDbType.Int) { Direction = ParameterDirection.Output };                                
                //</Parameter>            
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;            
                command.Transaction = this.trans;            
                command.ExecuteNonQuery();                
                foreclosureCase.FcId = ConvertToInt(sqlParam[103].Value);
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
                var sqlParam = new SqlParameter[24];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                sqlParam[1] = new SqlParameter("@pi_servicer_id", caseLoan.ServicerId);
                sqlParam[2] = new SqlParameter("@pi_other_servicer_name", caseLoan.OtherServicerName);
                sqlParam[3] = new SqlParameter("@pi_acct_num", caseLoan.AcctNum);
                sqlParam[4] = new SqlParameter("@pi_loan_1st_2nd_cd", caseLoan.Loan1st2nd);
                sqlParam[5] = new SqlParameter("@pi_mortgage_type_cd", caseLoan.MortgageTypeCd);                
                sqlParam[6] = new SqlParameter("@pi_arm_reset_ind", caseLoan.ArmResetInd);
                sqlParam[7] = new SqlParameter("@pi_term_length_cd", caseLoan.TermLengthCd);
                sqlParam[8] = new SqlParameter("@pi_loan_delinq_status_cd", caseLoan.LoanDelinqStatusCd);
                sqlParam[9] = new SqlParameter("@pi_current_loan_balance_amt", caseLoan.CurrentLoanBalanceAmt);
                sqlParam[10] = new SqlParameter("@pi_orig_loan_amt", caseLoan.OrigLoanAmt);
                sqlParam[11] = new SqlParameter("@pi_interest_rate", caseLoan.InterestRate);
                sqlParam[12] = new SqlParameter("@pi_Originating_Lender_Name", caseLoan.OriginatingLenderName);
                sqlParam[13] = new SqlParameter("@pi_orig_mortgage_co_FDIC_NCUS_num", caseLoan.OrigMortgageCoFdicNcusNum);
                sqlParam[14] = new SqlParameter("@pi_Orig_mortgage_co_name", caseLoan.OrigMortgageCoName);
                sqlParam[15] = new SqlParameter("@pi_Orginal_Loan_Num", caseLoan.OrginalLoanNum);
                sqlParam[16] = new SqlParameter("@pi_FDIC_NCUS_Num_current_servicer_TBD", caseLoan.FdicNcusNumCurrentServicerTbd);
                sqlParam[17] = new SqlParameter("@pi_Current_Servicer_Name_TBD", caseLoan.CurrentServicerNameTbd);
                sqlParam[18] = new SqlParameter("@pi_create_dt", NullableDateTime(caseLoan.CreateDate));
                sqlParam[19] = new SqlParameter("@pi_create_user_id", caseLoan.CreateUserId);
                sqlParam[20] = new SqlParameter("@pi_create_app_name", caseLoan.CreateAppName);
                sqlParam[21] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(caseLoan.ChangeLastDate));
                sqlParam[22] = new SqlParameter("@pi_chg_lst_user_id", caseLoan.ChangeLastUserId);
                sqlParam[23] = new SqlParameter("@pi_chg_lst_app_name", caseLoan.ChangeLastAppName);                
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
        public void UpdateFcCase_DuplicateIndicator(int fcid, string ind)
        {
            //throw NotImplementedException;
            var command = CreateCommand("hpf_foreclosure_case_update_duplicate", this.dbConnection);
            try
            {
                var sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcid);
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
                var sqlParam = new SqlParameter[21];
                sqlParam[0] = new SqlParameter("@pi_fc_id", caseLoan.FcId);
                sqlParam[1] = new SqlParameter("@pi_servicer_id", caseLoan.ServicerId);
                sqlParam[2] = new SqlParameter("@pi_other_servicer_name", caseLoan.OtherServicerName);
                sqlParam[3] = new SqlParameter("@pi_acct_num", caseLoan.AcctNum);
                sqlParam[4] = new SqlParameter("@pi_loan_1st_2nd_cd", caseLoan.Loan1st2nd);
                sqlParam[5] = new SqlParameter("@pi_mortgage_type_cd", caseLoan.MortgageTypeCd);
                sqlParam[6] = new SqlParameter("@pi_arm_reset_ind", caseLoan.ArmResetInd);
                sqlParam[7] = new SqlParameter("@pi_term_length_cd", caseLoan.TermLengthCd);
                sqlParam[8] = new SqlParameter("@pi_loan_delinq_status_cd", caseLoan.LoanDelinqStatusCd);
                sqlParam[9] = new SqlParameter("@pi_current_loan_balance_amt", caseLoan.CurrentLoanBalanceAmt);
                sqlParam[10] = new SqlParameter("@pi_orig_loan_amt", caseLoan.OrigLoanAmt);
                sqlParam[11] = new SqlParameter("@pi_interest_rate", caseLoan.InterestRate);
                sqlParam[12] = new SqlParameter("@pi_Originating_Lender_Name", caseLoan.OriginatingLenderName);
                sqlParam[13] = new SqlParameter("@pi_orig_mortgage_co_FDIC_NCUS_num", caseLoan.OrigMortgageCoFdicNcusNum);
                sqlParam[14] = new SqlParameter("@pi_Orig_mortgage_co_name", caseLoan.OrigMortgageCoName);
                sqlParam[15] = new SqlParameter("@pi_Orginal_Loan_Num", caseLoan.OrginalLoanNum);
                sqlParam[16] = new SqlParameter("@pi_FDIC_NCUS_Num_current_servicer_TBD", caseLoan.FdicNcusNumCurrentServicerTbd);
                sqlParam[17] = new SqlParameter("@pi_Current_Servicer_Name_TBD", caseLoan.CurrentServicerNameTbd);
                sqlParam[18] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(caseLoan.ChangeLastDate));
                sqlParam[19] = new SqlParameter("@pi_chg_lst_user_id", caseLoan.ChangeLastUserId);
                sqlParam[20] = new SqlParameter("@pi_chg_lst_app_name", caseLoan.ChangeLastAppName);                         
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
                var sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@pi_fc_id", caseLoan.FcId);                
                sqlParam[1] = new SqlParameter("@pi_acct_num", caseLoan.AcctNum);
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
        public void InsertBudgetItem(BudgetItemDTO budgetItem, int budgetSetId)
        {            
            var command = CreateCommand("hpf_budget_item_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@pi_budget_set_id", budgetSetId);
                sqlParam[1] = new SqlParameter("@pi_budget_subcategory_id", budgetItem.BudgetSubcategoryId);
                sqlParam[2] = new SqlParameter("@pi_budget_item_amt", budgetItem.BudgetItemAmt);
                sqlParam[3] = new SqlParameter("@pi_budget_note", budgetItem.BudgetNote);
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
        public void InsertBudgetAsset(BudgetAssetDTO budgetAsset, int budgetSetId)
        {            
            var command = CreateCommand("hpf_budget_asset_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[9];
                sqlParam[0] = new SqlParameter("@pi_budget_set_id", budgetSetId);
                sqlParam[1] = new SqlParameter("@pi_asset_name", budgetAsset.AssetName);
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
        public void InsertActivityLog(ActivityLogDTO activityLog, int fcId)
        {
            var command = CreateCommand("hpf_activity_log_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                sqlParam[1] = new SqlParameter("@pi_activity_cd", activityLog.ActivityCd);
                sqlParam[2] = new SqlParameter("@pi_activity_dt", activityLog.ActivityDt);
                sqlParam[3] = new SqlParameter("@pi_activity_note", activityLog.ActivityDt);
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
        public int UpdateForeclosureCase(ForeclosureCaseDTO foreclosureCase)
        {            
            var command = CreateSPCommand("hpf_foreclosure_case_update", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[103];
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
                sqlParam[62] = new SqlParameter("@pi_funding_consent_ind", foreclosureCase.FundingConsentInd);
                sqlParam[63] = new SqlParameter("@pi_servicer_consent_ind", foreclosureCase.ServicerConsentInd);
                sqlParam[64] = new SqlParameter("@pi_agency_media_interest_ind", foreclosureCase.AgencyMediaInterestInd);
                sqlParam[65] = new SqlParameter("@pi_agency_success_story_ind", foreclosureCase.AgencySuccessStoryInd);
                sqlParam[66] = new SqlParameter("@pi_borrower_disabled_ind", foreclosureCase.BorrowerDisabledInd);
                sqlParam[67] = new SqlParameter("@pi_co_borrower_disabled_ind", foreclosureCase.CoBorrowerDisabledInd);
                sqlParam[68] = new SqlParameter("@pi_summary_sent_other_cd", foreclosureCase.SummarySentOtherCd);
                sqlParam[69] = new SqlParameter("@pi_summary_sent_other_dt", NullableDateTime(foreclosureCase.SummarySentOtherDt));
                sqlParam[70] = new SqlParameter("@pi_summary_sent_dt", NullableDateTime(foreclosureCase.SummarySentDt));
                sqlParam[71] = new SqlParameter("@pi_occupant_num", foreclosureCase.OccupantNum);
                sqlParam[72] = new SqlParameter("@pi_loan_dflt_reason_notes", foreclosureCase.LoanDfltReasonNotes);
                sqlParam[73] = new SqlParameter("@pi_action_items_notes", foreclosureCase.ActionItemsNotes);
                sqlParam[74] = new SqlParameter("@pi_followup_notes", foreclosureCase.FollowupNotes);
                sqlParam[75] = new SqlParameter("@pi_prim_res_est_mkt_value", foreclosureCase.PrimResEstMktValue);
                sqlParam[76] = new SqlParameter("@pi_counselor_id_ref", foreclosureCase.AssignedCounselorIdRef);
                sqlParam[77] = new SqlParameter("@pi_counselor_lname", foreclosureCase.CounselorFname);
                sqlParam[78] = new SqlParameter("@pi_counselor_fname", foreclosureCase.CounselorLname);
                sqlParam[79] = new SqlParameter("@pi_counselor_email", foreclosureCase.CounselorEmail);
                sqlParam[80] = new SqlParameter("@pi_counselor_phone", foreclosureCase.CounselorPhone);
                sqlParam[81] = new SqlParameter("@pi_counselor_ext", foreclosureCase.CounselorExt);
                sqlParam[82] = new SqlParameter("@pi_discussed_solution_with_srvcr_ind", foreclosureCase.DiscussedSolutionWithSrvcrInd);
                sqlParam[83] = new SqlParameter("@pi_worked_with_another_agency_ind", foreclosureCase.WorkedWithAnotherAgencyInd);
                sqlParam[84] = new SqlParameter("@pi_contacted_srvcr_recently_ind", foreclosureCase.ContactedSrvcrRecentlyInd);
                sqlParam[85] = new SqlParameter("@pi_has_workout_plan_ind", foreclosureCase.HasWorkoutPlanInd);
                sqlParam[86] = new SqlParameter("@pi_srvcr_workout_plan_current_ind", foreclosureCase.SrvcrWorkoutPlanCurrentInd);
                sqlParam[87] = new SqlParameter("@pi_primary_residence_ind", foreclosureCase.PrimaryResidenceInd);
                sqlParam[88] = new SqlParameter("@pi_realty_company", foreclosureCase.RealtyCompany);
                sqlParam[89] = new SqlParameter("@pi_property_cd", foreclosureCase.PropertyCd);
                sqlParam[90] = new SqlParameter("@pi_for_sale_ind", foreclosureCase.ForSaleInd);
                sqlParam[91] = new SqlParameter("@pi_home_sale_price", foreclosureCase.HomeSalePrice);
                sqlParam[92] = new SqlParameter("@pi_home_purchase_year", foreclosureCase.HomePurchaseYear);
                sqlParam[93] = new SqlParameter("@pi_home_purchase_price", foreclosureCase.HomePurchasePrice);
                sqlParam[94] = new SqlParameter("@pi_Home_Current_Market_Value", foreclosureCase.HomeCurrentMarketValue);
                sqlParam[95] = new SqlParameter("@pi_military_service_cd", foreclosureCase.MilitaryServiceCd);
                sqlParam[96] = new SqlParameter("@pi_household_gross_annual_income_amt", foreclosureCase.HouseholdGrossAnnualIncomeAmt);
                sqlParam[97] = new SqlParameter("@pi_intake_credit_score", foreclosureCase.IntakeCreditScore);
                sqlParam[98] = new SqlParameter("@pi_Intake_credit_bureau_cd ", foreclosureCase.IntakeCreditBureauCd);
                sqlParam[99] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(foreclosureCase.ChangeLastDate));
                sqlParam[100] = new SqlParameter("@pi_chg_lst_user_id", foreclosureCase.ChangeLastUserId);
                sqlParam[101] = new SqlParameter("@pi_chg_lst_app_name", foreclosureCase.ChangeLastAppName);
                sqlParam[102] = new SqlParameter("@pi_fc_id", foreclosureCase.FcId);                                
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
        /// Select a BudgetItem to database.
        /// Where Max BudgetSet_ID and FC_ID
        /// </summary>
        /// <param name="budgetItem">BudgetItemDTO</param>
        /// <returns></returns>
        public BudgetAssetDTOCollection GetBudgetAssetSet(int fcId)
        {
            BudgetAssetDTOCollection results = new BudgetAssetDTOCollection();            
            var command = new SqlCommand("hpf_budget_asset_get", this.dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);

            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = this.trans;
            try
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new BudgetAssetDTOCollection();
                    while (reader.Read())
                    {
                        BudgetAssetDTO item = new BudgetAssetDTO();
                        item.BudgetAssetId = ConvertToInt(reader["budget_asset_id"]);
                        item.BudgetSetId = ConvertToInt(reader["budget_set_id"]);
                        item.AssetName = ConvertToString(reader["asset_name"]);
                        item.AssetValue = ConvertToDouble(reader["asset_value"]);
                        results.Add(item);
                    }                    
                }
                reader.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }            
            return results;
        }

        /// <summary>
        /// Select a BudgetItem to database.
        /// Where Max BudgetSet_ID and FC_ID
        /// </summary>
        /// <param name="budgetItem">BudgetItemDTO</param>
        /// <returns></returns>
        public BudgetItemDTOCollection GetBudgetItemSet(int fcId)
        {
            BudgetItemDTOCollection results = new BudgetItemDTOCollection();
            var command = new SqlCommand("hpf_budget_item_get", this.dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);

            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = this.trans;  
            try
            {                
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new BudgetItemDTOCollection();
                    while (reader.Read())
                    {
                        BudgetItemDTO item = new BudgetItemDTO();
                        item.BudgetItemId = ConvertToInt(reader["budget_item_id"]);
                        item.BudgetSetId = ConvertToInt(reader["budget_set_id"]);
                        item.BudgetSubcategoryId = ConvertToInt(reader["budget_subcategory_id"]);
                        item.BudgetItemAmt = ConvertToDouble(reader["budget_item_amt"]);
                        item.BudgetNote = ConvertToString(reader["budget_note"]);
                        results.Add(item);
                    }                    
                }
                reader.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            return results;
        }

        /// <summary>
        /// Select all CAse Loan from database by Fc_ID. 
        /// </summary>
        /// <param name=""></param>
        /// <returns>OutcomeItemDTOCollection</returns>
        public CaseLoanDTOCollection GetCaseLoanCollection(int fcId)
        {
            CaseLoanDTOCollection results = HPFCacheManager.Instance.GetData<CaseLoanDTOCollection>("caseLoanItem");
            if (results == null)
            {                
                var command = new SqlCommand("hpf_case_loan_get", this.dbConnection);
                //<Parameter>            
                var sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                //</Parameter>   
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;  
                try
                {                    
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
                            item.OtherServicerName = ConvertToString(reader["other_servicer_name"]);
                            item.AcctNum = ConvertToString(reader["acct_num"]);
                            item.Loan1st2nd = ConvertToString(reader["loan_1st_2nd_cd"]);
                            item.MortgageTypeCd = ConvertToString(reader["mortgage_type_cd"]);                            
                            item.ArmResetInd = ConvertToString(reader["arm_reset_ind"]);                            
                            item.LoanDelinqStatusCd = ConvertToString(reader["loan_delinq_status_cd"]);
                            item.CurrentLoanBalanceAmt = ConvertToDouble(reader["current_loan_balance_amt"]);
                            item.OrigLoanAmt = ConvertToDouble(reader["orig_loan_amt"]);
                            item.InterestRate = ConvertToDouble(reader["interest_rate"]);
                            item.OriginatingLenderName = ConvertToString(reader["originating_lender_name"]);
                            item.OrigMortgageCoFdicNcusNum = ConvertToString(reader["orig_mortgage_co_FDIC_NCUS_num"]);
                            item.OrigMortgageCoName = ConvertToString(reader["orig_mortgage_co_name"]);
                            item.OrginalLoanNum = ConvertToString(reader["orginal_loan_num"]);
                            item.FdicNcusNumCurrentServicerTbd = ConvertToString(reader["FDIC_NCUS_num_current_servicer_TBD"]);
                            item.CurrentServicerNameTbd = ConvertToString(reader["current_servicer_name_TBD"]);
                            item.InvestorLoanNum = ConvertToString(reader["investor_loan_num"]);
                            item.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                            item.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                            item.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                            results.Add(item);
                        }                        
                    }
                    reader.Close();
                    HPFCacheManager.Instance.Add("caseLoanItem", results);
                }
                catch (Exception Ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
                }
            }
            return results;
        }

        /// <summary>
        /// Select all OutcomeItem from database by Fc_ID. 
        /// </summary>
        /// <param name=""></param>
        /// <returns>OutcomeItemDTOCollection</returns>
        public OutcomeItemDTOCollection GetOutcomeItemCollection(int fcId)
        {
            OutcomeItemDTOCollection results = HPFCacheManager.Instance.GetData<OutcomeItemDTOCollection>("outcomeItem");
            if (results == null)
            {                
                var command = new SqlCommand("hpf_outcome_item_get", this.dbConnection);
                //<Parameter>            
                var sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                //</Parameter>   
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;  
                try
                {                    
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new OutcomeItemDTOCollection();
                        while (reader.Read())
                        {
                            OutcomeItemDTO item = new OutcomeItemDTO();
                            item.OutcomeItemId = ConvertToInt(reader["outcome_item_id"]);
                            item.FcId = ConvertToInt(reader["fc_id"]);
                            item.OutcomeTypeId = ConvertToInt(reader["outcome_type_id"]);
                            item.OutcomeDt = ConvertToDateTime(reader["outcome_dt"]);
                            item.OutcomeDeletedDt = ConvertToDateTime(reader["outcome_deleted_dt"]);
                            item.NonprofitreferralKeyNum = ConvertToString(reader["nonprofitreferral_key_num"]);
                            item.ExtRefOtherName = ConvertToString(reader["ext_ref_other_name"]);
                            item.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                            item.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                            item.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                            results.Add(item);
                        }                        
                    }
                    reader.Close();
                    HPFCacheManager.Instance.Add("outcomeItem", results);
                }
                catch (Exception Ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
                }
            }
            return results;
        }

        /// <summary>
        /// Select all OutcomeItem from database by Fc_ID. 
        /// </summary>
        /// <param name=""></param>
        /// <returns>OutcomeItemDTOCollection</returns>
        public BudgetDTOCollection GetBudget()
        {
            BudgetDTOCollection results = HPFCacheManager.Instance.GetData<BudgetDTOCollection>("budget");
            if (results == null)
            {
                var command = new SqlCommand("hpf_view_budget_category_code", this.dbConnection);
                //<Parameter>            
                
                //</Parameter>                   
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new BudgetDTOCollection();
                        while (reader.Read())
                        {
                            BudgetDTO item = new BudgetDTO();
                            item.BudgetSubcategoryId = ConvertToInt(reader["budget_subcategory_id"]);
                            item.BudgetCategoryCode = ConvertToString(reader["budget_category_cd"]);                            
                            results.Add(item);
                        }
                        reader.Close();
                    }
                    HPFCacheManager.Instance.Add("budget", results);
                }
                catch (Exception Ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
                }
            }
            return results;
        }

        /// <summary>
        /// Get ID and Name from table Program
        /// </summary>
        /// <returns>ProgramDTOCollection contains all Program </returns>
        public ProgramDTOCollection GetProgram()
        {
            ProgramDTOCollection results = HPFCacheManager.Instance.GetData<ProgramDTOCollection>("program");
            if (results == null)
            {
                var command = new SqlCommand("hpf_program_get", this.dbConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                try
                {
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
                    HPFCacheManager.Instance.Add("program", results);
                }
                catch (Exception ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(ex);
                }                
            }
            return results;
        }

        /// <summary>
        /// Get ID and Name from table OutcomeType
        /// </summary>
        /// <returns>ProgramDTOCollection contains all Program </returns>
        public OutcomeTypeDTOCollection GetOutcomeType()
        {
            OutcomeTypeDTOCollection results = HPFCacheManager.Instance.GetData<OutcomeTypeDTOCollection>("outcomeType");
            if (results == null)
            {
                var command = new SqlCommand("hpf_outcome_type_get", this.dbConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new OutcomeTypeDTOCollection();
                        while (reader.Read())
                        {
                            var item = new OutcomeTypeDTO();                            
                            item.OutcomeTypeID = ConvertToInt(reader["outcome_type_id"]);
                            item.OutcomeTypeName = ConvertToString(reader["outcome_type_name"]);
                            item.PayableInd = ConvertToString(reader["payable_ind"]);
                            results.Add(item);
                        }
                        reader.Close();
                    }
                    HPFCacheManager.Instance.Add("outcomeType", results);
                }
                catch (Exception ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(ex);
                }
            }
            return results;
        }

        /// <summary>
        /// Get ID and Name from table Budget Subcategory
        /// </summary>
        /// <returns>ProgramDTOCollection contains all Program </returns>
        public BudgetSubcategoryDTOCollection GetBudgetSubcategory()
        {
            BudgetSubcategoryDTOCollection results = HPFCacheManager.Instance.GetData<BudgetSubcategoryDTOCollection>("budgetSubcategory");
            if (results == null)
            {
                var command = new SqlCommand("hpf_budget_subcategory_get", this.dbConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new BudgetSubcategoryDTOCollection();
                        while (reader.Read())
                        {
                            var item = new BudgetSubcategoryDTO();
                            item.BudgetSubcategoryID = ConvertToInt(reader["budget_subcategory_id"]);
                            item.BudgetSubcategoryName = ConvertToString(reader["budget_subcategory_name"]);                            
                            results.Add(item);
                        }
                        reader.Close();
                    }
                    HPFCacheManager.Instance.Add("budgetSubcategory", results);
                }
                catch (Exception ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(ex);
                }
            }
            return results;
        }

        /// <summary>
        /// Get ID and Name from table Servicer
        /// </summary>
        /// <returns>ProgramDTOCollection contains all Program </returns>
        public ServicerDTOCollection GetServicer()
        {
            ServicerDTOCollection results = HPFCacheManager.Instance.GetData<ServicerDTOCollection>("servicer");
            if (results == null)
            {
                var command = new SqlCommand("hpf_servicer_get", this.dbConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new ServicerDTOCollection();
                        while (reader.Read())
                        {
                            var item = new ServicerDTO();
                            item.ServicerID = ConvertToInt(reader["servicer_id"]);
                            item.ServicerName = ConvertToString(reader["servicer_name"]);
                            results.Add(item);
                        }
                        reader.Close();
                    }
                    HPFCacheManager.Instance.Add("servicer", results);
                }
                catch (Exception ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(ex);
                }
            }
            return results;
        }

        public bool CheckExistingAgencyIdAndCaseNumber(int agency_id, string agency_case_number)
        {
            bool returnValue = true;
            try
            {
                SqlCommand command = base.CreateCommand("hpf_foreclosure_case_get_from_agencyID_and_caseNumber", dbConnection, trans);//new SqlCommand("hpf_foreclosure_case_get_from_agencyID_and_caseNumber", dbConnection);
                //<Parameter>
                var sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@pi_agency_case_num", agency_case_number);
                sqlParam[1] = new SqlParameter("@pi_agency_id", agency_id);
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
            
                var reader = command.ExecuteReader();
                returnValue = reader.HasRows;
                reader.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            return returnValue;

        }        

        public DuplicatedCaseLoanDTOCollection GetDuplicatedCases(ForeclosureCaseSetDTO foreclosureCaseSet)
        {
            DuplicatedCaseLoanDTOCollection returnCollection = null;
            try
            {
                ForeclosureCaseDTO fcCase = foreclosureCaseSet.ForeclosureCase;
                SqlCommand command = base.CreateCommand("hpf_foreclosure_case_get_duplicate", dbConnection, trans);
                //<Parameter>
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@pi_agency_case_num", fcCase.AgencyCaseNum);
                sqlParam[1] = new SqlParameter("@pi_agency_id", fcCase.AgencyId);
                sqlParam[2] = new SqlParameter("@pi_fc_id", NullableInteger(fcCase.FcId));
                sqlParam[3] = new SqlParameter("@pi_where_str", GenerateGetDuplicatedCaseWhereClause(foreclosureCaseSet));
                    //</Parameter>

                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    returnCollection = new DuplicatedCaseLoanDTOCollection();
                    while (reader.Read())
                    {
                        DuplicatedCaseLoanDTO obj = new DuplicatedCaseLoanDTO();
                        obj.LoanNumber = ConvertToString(reader["Acct_num"]);
                        obj.FcID = ConvertToInt(reader["FC_ID"]);
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
                        obj.OutcomeTypeCode = ConvertToString(reader["outcome_type_cd"]);

                        returnCollection.Add(obj);
                    }
                }
                reader.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            return returnCollection;
        }

        private string GenerateGetDuplicatedCaseWhereClause(ForeclosureCaseSetDTO fcCaseSet)
        {
            StringBuilder sb = new StringBuilder();
            string orString = " or ";
            foreach (CaseLoanDTO item in fcCaseSet.CaseLoans)
            {
                sb.Append(string.Format(" (acct_num = '{0}' and CL.servicer_id = {1})", item.AcctNum, item.ServicerId));
                sb.Append(orString);
            }
            
            sb.Remove(sb.Length - orString.Length, orString.Length);
            return sb.ToString();
        }
    }
}


