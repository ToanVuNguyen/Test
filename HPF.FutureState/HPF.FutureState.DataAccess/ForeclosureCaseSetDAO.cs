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

        private static ForeclosureCaseSetDAO _instance = new ForeclosureCaseSetDAO();

        protected ForeclosureCaseSetDAO()
        {

        }

        public static ForeclosureCaseSetDAO CreateInstance()
        {
            return _instance;
            //return new ForeclosureCaseSetDAO();
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
        public ForeclosureCaseDTO GetForeclosureCase(int fc_id)
        {
            ForeclosureCaseDTO returnObject = new ForeclosureCaseDTO();

            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_foreclosure_case_retrieve_from_fcid", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@fc_id", fc_id);

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
                        returnObject.AssignedCounselorIdRef = ConvertToString(reader["assigned_counselor_id_ref"]);
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
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_foreclosure_case_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[108];
            sqlParam[0] = new SqlParameter("@agency_id", foreclosureCase.AgencyId);
            sqlParam[1] = new SqlParameter("@completed_dt", NullableDateTime(foreclosureCase.CompletedDt));
            sqlParam[2] = new SqlParameter("@call_id", foreclosureCase.CallId);
            sqlParam[3] = new SqlParameter("@program_id", foreclosureCase.ProgramId);
            sqlParam[4] = new SqlParameter("@agency_case_num", foreclosureCase.AgencyCaseNum);
            sqlParam[5] = new SqlParameter("@agency_client_num", foreclosureCase.AgencyClientNum);
            sqlParam[6] = new SqlParameter("@intake_dt", foreclosureCase.IntakeDt);
            sqlParam[7] = new SqlParameter("@income_earners_cd", foreclosureCase.IncomeEarnersCd);
            sqlParam[8] = new SqlParameter("@case_source_cd", foreclosureCase.CaseSourceCd);
            sqlParam[9] = new SqlParameter("@race_cd", foreclosureCase.RaceCd);
            sqlParam[10] = new SqlParameter("@household_cd", foreclosureCase.HouseholdCd);
            sqlParam[11] = new SqlParameter("@never_bill_reason_cd", foreclosureCase.NeverBillReasonCd);
            sqlParam[12] = new SqlParameter("@never_pay_reason_cd", foreclosureCase.NeverPayReasonCd);
            sqlParam[13] = new SqlParameter("@dflt_reason_1st_cd", foreclosureCase.DfltReason1stCd);
            sqlParam[14] = new SqlParameter("@dflt_reason_2nd_cd", foreclosureCase.DfltReason2ndCd);
            sqlParam[15] = new SqlParameter("@hud_termination_reason_cd", foreclosureCase.HudTerminationReasonCd);
            sqlParam[16] = new SqlParameter("@hud_termination_dt", NullableDateTime(foreclosureCase.HudTerminationDt));
            sqlParam[17] = new SqlParameter("@hud_outcome_cd", foreclosureCase.HudOutcomeCd);
            sqlParam[18] = new SqlParameter("@AMI_percentage", foreclosureCase.AmiPercentage);
            sqlParam[19] = new SqlParameter("@counseling_duration_cd", foreclosureCase.CounselingDurationCd);
            sqlParam[20] = new SqlParameter("@gender_cd", foreclosureCase.GenderCd);
            sqlParam[21] = new SqlParameter("@borrower_fname", foreclosureCase.BorrowerFname);
            sqlParam[22] = new SqlParameter("@borrower_lname", foreclosureCase.BorrowerLname);
            sqlParam[23] = new SqlParameter("@borrower_mname", foreclosureCase.BorrowerMname);
            sqlParam[24] = new SqlParameter("@mother_maiden_lname", foreclosureCase.MotherMaidenLname);
            sqlParam[25] = new SqlParameter("@borrower_ssn", foreclosureCase.BorrowerSsn);
            sqlParam[26] = new SqlParameter("@borrower_last4_SSN", foreclosureCase.BorrowerLast4Ssn);
            sqlParam[27] = new SqlParameter("@borrower_DOB", NullableDateTime(foreclosureCase.BorrowerDob));
            sqlParam[28] = new SqlParameter("@co_borrower_fname", foreclosureCase.CoBorrowerFname);
            sqlParam[29] = new SqlParameter("@co_borrower_lname", foreclosureCase.CoBorrowerLname);
            sqlParam[30] = new SqlParameter("@co_borrower_mname", foreclosureCase.CoBorrowerMname);
            sqlParam[31] = new SqlParameter("@co_borrower_ssn", foreclosureCase.CoBorrowerSsn);
            sqlParam[32] = new SqlParameter("@co_borrower_last4_SSN", foreclosureCase.CoBorrowerLast4Ssn);
            sqlParam[33] = new SqlParameter("@co_borrower_DOB", NullableDateTime(foreclosureCase.CoBorrowerDob));
            sqlParam[34] = new SqlParameter("@primary_contact_no", foreclosureCase.PrimaryContactNo);
            sqlParam[35] = new SqlParameter("@second_contact_no", foreclosureCase.SecondContactNo);
            sqlParam[36] = new SqlParameter("@email_1", foreclosureCase.Email1);
            sqlParam[37] = new SqlParameter("@email_2", foreclosureCase.Email2);
            sqlParam[38] = new SqlParameter("@contact_addr1", foreclosureCase.ContactAddr1);
            sqlParam[39] = new SqlParameter("@contact_addr2", foreclosureCase.ContactAddr2);
            sqlParam[40] = new SqlParameter("@contact_city", foreclosureCase.ContactCity);
            sqlParam[41] = new SqlParameter("@contact_state_cd", foreclosureCase.ContactStateCd);
            sqlParam[42] = new SqlParameter("@contact_zip", foreclosureCase.ContactZip);
            sqlParam[43] = new SqlParameter("@contact_zip_plus4", foreclosureCase.ContactZipPlus4);
            sqlParam[44] = new SqlParameter("@prop_addr1", foreclosureCase.PropAddr1);
            sqlParam[45] = new SqlParameter("@prop_addr2", foreclosureCase.PropAddr2);
            sqlParam[46] = new SqlParameter("@prop_city", foreclosureCase.PropCity);
            sqlParam[47] = new SqlParameter("@prop_state_cd", foreclosureCase.PropStateCd);
            sqlParam[48] = new SqlParameter("@prop_zip", foreclosureCase.PropZip);
            sqlParam[49] = new SqlParameter("@prop_zip_plus_4", foreclosureCase.PropZipPlus4);
            sqlParam[50] = new SqlParameter("@bankruptcy_ind", foreclosureCase.BankruptcyInd);
            sqlParam[51] = new SqlParameter("@bankruptcy_attorney", foreclosureCase.BankruptcyAttorney);
            sqlParam[52] = new SqlParameter("@bankruptcy_pmt_current_ind", foreclosureCase.BankruptcyPmtCurrentInd);
            sqlParam[53] = new SqlParameter("@borrower_educ_level_completed_cd", foreclosureCase.BorrowerEducLevelCompletedCd);
            sqlParam[54] = new SqlParameter("@borrower_marital_status_cd", foreclosureCase.BorrowerMaritalStatusCd);
            sqlParam[55] = new SqlParameter("@borrower_preferred_lang_cd", foreclosureCase.BorrowerPreferredLangCd);
            sqlParam[56] = new SqlParameter("@borrower_occupation", foreclosureCase.BorrowerOccupationCd);
            sqlParam[57] = new SqlParameter("@co_borrower_occupation", foreclosureCase.CoBorrowerOccupationCd);
            sqlParam[58] = new SqlParameter("@owner_occupied_ind", foreclosureCase.OwnerOccupiedInd);
            sqlParam[59] = new SqlParameter("@hispanic_ind", foreclosureCase.HispanicInd);
            sqlParam[60] = new SqlParameter("@duplicate_ind", foreclosureCase.DuplicateInd);
            sqlParam[61] = new SqlParameter("@fc_notice_received_ind", foreclosureCase.FcNoticeReceiveInd);
            sqlParam[62] = new SqlParameter("@case_complete_ind", foreclosureCase.CaseCompleteInd);
            sqlParam[63] = new SqlParameter("@funding_consent_ind", foreclosureCase.FundingConsentInd);
            sqlParam[64] = new SqlParameter("@servicer_consent_ind", foreclosureCase.ServicerConsentInd);
            sqlParam[65] = new SqlParameter("@agency_media_consent_ind", foreclosureCase.AgencyMediaConsentInd);
            sqlParam[66] = new SqlParameter("@hpf_media_candidate_ind", foreclosureCase.HpfMediaCandidateInd);
            sqlParam[67] = new SqlParameter("@hpf_network_candidate_ind", foreclosureCase.HpfNetworkCandidateInd);
            sqlParam[68] = new SqlParameter("@hpf_success_story_ind", foreclosureCase.HpfSuccessStoryInd);
            sqlParam[69] = new SqlParameter("@agency_success_story_ind", foreclosureCase.AgencySuccessStoryInd);
            sqlParam[70] = new SqlParameter("@borrower_disabled_ind", foreclosureCase.BorrowerDisabledInd);
            sqlParam[71] = new SqlParameter("@co_borrower_disabled_ind", foreclosureCase.CoBorrowerDisabledInd);
            sqlParam[72] = new SqlParameter("@summary_sent_other_cd", foreclosureCase.SummarySentOtherCd);
            sqlParam[73] = new SqlParameter("@summary_sent_other_dt", NullableDateTime(foreclosureCase.SummarySentOtherDt));
            sqlParam[74] = new SqlParameter("@summary_sent_dt", NullableDateTime(foreclosureCase.SummarySentDt));
            sqlParam[75] = new SqlParameter("@occupant_num", foreclosureCase.OccupantNum);
            sqlParam[76] = new SqlParameter("@loan_dflt_reason_notes", foreclosureCase.LoanDfltReasonNotes);
            sqlParam[77] = new SqlParameter("@action_items_notes", foreclosureCase.ActionItemsNotes);
            sqlParam[78] = new SqlParameter("@followup_notes", foreclosureCase.FollowupNotes);
            sqlParam[79] = new SqlParameter("@prim_res_est_mkt_value", foreclosureCase.PrimResEstMktValue);
            sqlParam[80] = new SqlParameter("@assigned_counselor_id_ref", foreclosureCase.AssignedCounselorIdRef);
            sqlParam[81] = new SqlParameter("@counselor_lname", foreclosureCase.CounselorFname);
            sqlParam[82] = new SqlParameter("@counselor_fname", foreclosureCase.CounselorLname);
            sqlParam[83] = new SqlParameter("@counselor_email", foreclosureCase.CounselorEmail);
            sqlParam[84] = new SqlParameter("@counselor_phone", foreclosureCase.CounselorPhone);
            sqlParam[85] = new SqlParameter("@counselor_ext", foreclosureCase.CounselorExt);
            sqlParam[86] = new SqlParameter("@discussed_solution_with_srvcr_ind", foreclosureCase.DiscussedSolutionWithSrvcrInd);
            sqlParam[87] = new SqlParameter("@worked_with_another_agency_ind", foreclosureCase.WorkedWithAnotherAgencyInd);
            sqlParam[88] = new SqlParameter("@contacted_srvcr_recently_ind", foreclosureCase.ContactedSrvcrRecentlyInd);
            sqlParam[89] = new SqlParameter("@has_workout_plan_ind", foreclosureCase.HasWorkoutPlanInd);
            sqlParam[90] = new SqlParameter("@srvcr_workout_plan_current_ind", foreclosureCase.SrvcrWorkoutPlanCurrentInd);
            sqlParam[91] = new SqlParameter("@fc_sale_date_set_ind", foreclosureCase.FcSaleDateSetInd);
            sqlParam[92] = new SqlParameter("@opt_out_newsletter_ind", foreclosureCase.OptOutNewsletterInd);
            sqlParam[93] = new SqlParameter("@opt_out_survey_ind", foreclosureCase.OptOutSurveyInd);
            sqlParam[94] = new SqlParameter("@do_not_call_ind", foreclosureCase.DoNotCallInd);
            sqlParam[95] = new SqlParameter("@primary_residence_ind", foreclosureCase.PrimaryResidenceInd);
            sqlParam[96] = new SqlParameter("@realty_company", foreclosureCase.RealtyCompany);
            sqlParam[97] = new SqlParameter("@property_cd", foreclosureCase.PropertyCd);
            sqlParam[98] = new SqlParameter("@for_sale_ind", foreclosureCase.ForSaleInd);
            sqlParam[99] = new SqlParameter("@home_sale_price", foreclosureCase.HomeSalePrice);
            sqlParam[100] = new SqlParameter("@home_purchase_year", foreclosureCase.HomePurchaseYear);
            sqlParam[101] = new SqlParameter("@home_purchase_price", foreclosureCase.HomePurchasePrice);
            sqlParam[102] = new SqlParameter("@Home_Current_Market_Value", foreclosureCase.HomeCurrentMarketValue);
            sqlParam[103] = new SqlParameter("@military_service_cd", foreclosureCase.MilitaryServiceCd);
            sqlParam[104] = new SqlParameter("@household_gross_annual_income_amt", foreclosureCase.HouseholdGrossAnnualIncomeAmt);
            sqlParam[105] = new SqlParameter("@loan_list", foreclosureCase.LoanList);
            sqlParam[106] = new SqlParameter("@chg_lst_user_id", foreclosureCase.ChgLstUserId);
            sqlParam[107] = new SqlParameter("@fc_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
            //</Parameter>            
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            dbConnection.Open();
            var trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = trans;
            //
            try
            {
                command.ExecuteNonQuery();
                trans.Commit();
                dbConnection.Close();
                foreclosureCase.FcId = ConvertToInt(sqlParam[107].Value);//@fc_id	
            }
            catch (Exception Ex)
            {
                trans.Rollback();
                dbConnection.Close();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
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
        public void InsertCaseLoan(CaseLoanDTO caseLoan, int fc_id)
        {
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_case_loan_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[20];
            sqlParam[0] = new SqlParameter("@fc_id", fc_id);
            sqlParam[1] = new SqlParameter("@servicer_id", caseLoan.ServicerId);
            sqlParam[2] = new SqlParameter("@other_servicer_name", caseLoan.OtherServicerName);
            sqlParam[3] = new SqlParameter("@acct_num", caseLoan.AcctNum);
            sqlParam[4] = new SqlParameter("@loan_1st_2nd_cd", caseLoan.Loan1st2nd);
            sqlParam[5] = new SqlParameter("@mortgage_type_cd", caseLoan.MortgageTypeCd);
            sqlParam[6] = new SqlParameter("@arm_loan_ind", caseLoan.ArmLoanInd);
            sqlParam[7] = new SqlParameter("@arm_reset_ind", caseLoan.ArmResetInd);
            sqlParam[8] = new SqlParameter("@term_length_cd", caseLoan.TermLengthCd);
            sqlParam[9] = new SqlParameter("@loan_delinq_status_cd", caseLoan.LoanDelinqStatusCd);
            sqlParam[10] = new SqlParameter("@current_loan_balance_amt", caseLoan.CurrentLoanBalanceAmt);
            sqlParam[11] = new SqlParameter("@orig_loan_amt", caseLoan.OrigLoanAmt);
            sqlParam[12] = new SqlParameter("@interest_rate", caseLoan.InterestRate);
            sqlParam[13] = new SqlParameter("@Originating_Lender_Name", caseLoan.OriginatingLenderName);
            sqlParam[14] = new SqlParameter("@orig_mortgage_co_FDIC_NCUS_num", caseLoan.OrigMortgageCoFdicNcusNum);
            sqlParam[15] = new SqlParameter("@Orig_mortgage_co_name", caseLoan.OrigMortgageCoName);
            sqlParam[16] = new SqlParameter("@Orginal_Loan_Num", caseLoan.OrginalLoanNum);
            sqlParam[17] = new SqlParameter("@FDIC_NCUS_Num_current_servicer_TBD", caseLoan.FdicNcusNumCurrentServicerTbd);
            sqlParam[18] = new SqlParameter("@Current_Servicer_Name_TBD", caseLoan.CurrentServicerNameTbd);
            sqlParam[19] = new SqlParameter("@freddie_loan_num", caseLoan.FreddieLoanNum);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            dbConnection.Open();
            var trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = trans;
            try
            {
                command.ExecuteNonQuery();
                trans.Commit();
                dbConnection.Close();
            }
            catch (Exception Ex)
            {
                trans.Rollback();
                dbConnection.Close();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void UpdateCaseLoan(CaseLoanDTO caseLoan)
        {

        }

        /// <summary>
        /// Insert a Outcome Item to database.
        /// </summary>
        /// <param name="outComeItem">OutcomeItemDTO</param>
        /// <returns></returns>
        public void InsertOutcomeItem(OutcomeItemDTO outComeItem, int fc_id)
        {
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_outcome_item_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[6];
            sqlParam[0] = new SqlParameter("@outcome_set_id", outComeItem.OutcomeSetId);
            sqlParam[0] = new SqlParameter("@fc_id", fc_id);
            sqlParam[1] = new SqlParameter("@outcome_type_id", outComeItem.OutcomeTypeId);
            sqlParam[2] = new SqlParameter("@outcome_dt", NullableDateTime(outComeItem.OutcomeDt));
            sqlParam[3] = new SqlParameter("@outcome_deleted_dt",NullableDateTime(outComeItem.OutcomeDeletedDt));
            sqlParam[4] = new SqlParameter("@nonprofitreferral_key_num", outComeItem.NonprofitreferralKeyNum);
            sqlParam[5] = new SqlParameter("@ext_ref_other_name", outComeItem.ExtRefOtherName);

            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            dbConnection.Open();
            var trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = trans;
            try
            {
                command.ExecuteNonQuery();
                trans.Commit();
                dbConnection.Close();
            }
            catch (Exception Ex)
            {
                trans.Rollback();
                dbConnection.Close();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        /// <summary>
        /// Insert a BudgetSet to database.
        /// </summary>
        /// <param name="budgetSet">BudgetSetDTO</param>
        /// <returns></returns>
        public int InsertBudgetSet(BudgetSetDTO budgetSet, int fc_id)
        {
            try
            {
                var dbConnection = new SqlConnection(ConnectionString);
                var command = new SqlCommand("hpf_budget_set_insert", dbConnection);
                //<Parameter>
                var sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@fc_id", fc_id);
                sqlParam[1] = new SqlParameter("@total_income", budgetSet.TotalIncome);
                sqlParam[2] = new SqlParameter("@total_expenses", budgetSet.TotalExpenses);
                sqlParam[3] = new SqlParameter("@total_assets", budgetSet.TotalAssets);
                sqlParam[4] = new SqlParameter("@budget_set_dt", NullableDateTime(budgetSet.BudgetSetDt));
                sqlParam[5] = new SqlParameter("@budget_set_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                var trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                command.Transaction = trans;
            
                command.ExecuteNonQuery();
                trans.Commit();
                dbConnection.Close();
                budgetSet.BudgetSetId = ConvertToInt(sqlParam[5].Value);
            }
            catch (Exception Ex)
            {
                trans.Rollback();
                dbConnection.Close();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
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
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_budget_item_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@budget_set_id", budget_set_id);
            sqlParam[1] = new SqlParameter("@budget_subcategory_id", budgetItem.BudgetSubcategoryId);
            sqlParam[2] = new SqlParameter("@budget_item_amt", budgetItem.BudgetItemAmt);
            sqlParam[3] = new SqlParameter("@budget_note", budgetItem.BudgetNote);

            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            dbConnection.Open();
            var trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = trans;
            try
            {
                command.ExecuteNonQuery();
                trans.Commit();
                dbConnection.Close();
            }
            catch (Exception Ex)
            {
                trans.Rollback();
                dbConnection.Close();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        /// <summary>
        /// Insert a BudgetAsset to database.
        /// </summary>
        /// <param name="budgetItem">BudgetAssetDTO</param>
        /// <returns></returns>
        public void InsertBudgetAsset(BudgetAssetDTO budgetAsset, int budget_set_id)
        {
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_budget_asset_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@budget_set_id", budget_set_id);
            sqlParam[1] = new SqlParameter("@asset_name", budgetAsset.AssetName);
            sqlParam[2] = new SqlParameter("@asset_value", budgetAsset.AssetValue);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            dbConnection.Open();
            var trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = trans;
            try
            {
                command.ExecuteNonQuery();
                trans.Commit();
                dbConnection.Close();
            }
            catch (Exception Ex)
            {
                trans.Rollback();
                dbConnection.Close();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        /// <summary>
        /// return all search results retrieved from database
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public ForeclosureCaseSearchResult SearchForeclosureCase(ForeclosureCaseSearchCriteriaDTO searchCriteria)
        {
            ForeclosureCaseSearchResult results = new ForeclosureCaseSearchResult();

            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_foreclosure_case_search", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[6];

            sqlParam[0] = new SqlParameter("@agency_case_num", searchCriteria.AgencyCaseNumber);
            sqlParam[1] = new SqlParameter("@borrower_fname", searchCriteria.FirstName);
            sqlParam[2] = new SqlParameter("@borrower_lname", searchCriteria.LastName);
            sqlParam[3] = new SqlParameter("@borrower_last4_SSN", searchCriteria.Last4_SSN);
            sqlParam[4] = new SqlParameter("@prop_zip", searchCriteria.PropertyZip);
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
                    results = new ForeclosureCaseSearchResult();
                    while (reader.Read())
                    {
                        ForeclosureCaseWSDTO item = new ForeclosureCaseWSDTO();
                        //item.Counseled = ConvertToString(reader["counseled"]);
                        item.FcId = ConvertToInt(reader["fc_id"]);
                        item.IntakeDt = ConvertToDateTime(reader["intake_dt"]);
                        item.BorrowerFname = ConvertToString(reader["borrower_fname"]);
                        item.BorrowerLname = ConvertToString(reader["borrower_lname"]);
                        item.BorrowerLast4SSN = ConvertToString(reader["borrower_last4_SSN"]);
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

        public bool CheckExistingAgencyIdAndCaseNumber(int agency_id, string agency_case_number)
        {
            bool returnValue = true;
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_foreclosure_case_retrieve_from_agencyID_and_caseNumber", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@agency_case_num", agency_case_number);
            sqlParam[1] = new SqlParameter("@agency_id", agency_id);
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
        public bool CheckDuplicate(int fc_id)
        {
            bool returnValue = true;
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_foreclosure_case_get_duplicate", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];            
            sqlParam[0] = new SqlParameter("@fc_id", fc_id);
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

        public bool CheckDuplicate(int agency_id, string agency_case_number)
        {
            bool returnValue = true;
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_foreclosure_case_get_duplicate", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@agency_case_num", agency_case_number);
            sqlParam[1] = new SqlParameter("@agency_id", agency_id);
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
    }
}


