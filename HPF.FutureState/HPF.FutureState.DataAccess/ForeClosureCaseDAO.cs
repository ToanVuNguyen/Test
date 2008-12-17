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
    public class ForeClosureCaseDAO : BaseDAO
    {
        public SqlConnection dbConnection;

        public SqlTransaction trans;

        protected ForeClosureCaseDAO()
        {

        }

        public static ForeClosureCaseDAO CreateInstance()
        {
            return new ForeClosureCaseDAO();
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
        /// Insert a ForeclosureCase to database.
        /// </summary>
        /// <param name="foreclosureCase">ForeclosureCase</param>
        /// <returns>a new Fc_id</returns>
        public int InsertForeclosureCase(ForeClosureCaseDTO foreclosureCase)
        {
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_foreclosure_case_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[108];
            sqlParam[0] = new SqlParameter("@agency_id", foreclosureCase.AgencyId);
            sqlParam[1] = new SqlParameter("@completed_dt", foreclosureCase.CompletedDt);
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
            sqlParam[16] = new SqlParameter("@hud_termination_dt", foreclosureCase.HudTerminationDt);
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
            sqlParam[27] = new SqlParameter("@borrower_DOB", foreclosureCase.BorrowerDob);
            sqlParam[28] = new SqlParameter("@co_borrower_fname", foreclosureCase.CoBorrowerFname);
            sqlParam[29] = new SqlParameter("@co_borrower_lname", foreclosureCase.CoBorrowerLname);
            sqlParam[30] = new SqlParameter("@co_borrower_mname", foreclosureCase.CoBorrowerMname);
            sqlParam[31] = new SqlParameter("@co_borrower_ssn", foreclosureCase.CoBorrowerSsn);
            sqlParam[32] = new SqlParameter("@co_borrower_last4_SSN", foreclosureCase.CoBorrowerLast4Ssn);
            sqlParam[33] = new SqlParameter("@co_borrower_DOB", foreclosureCase.CoBorrowerDob);
            sqlParam[34] = new SqlParameter("@primary_contact_no", foreclosureCase.PrimaryContactNo);
            sqlParam[35] = new SqlParameter("@second_contact_no", foreclosureCase.SecondContactNo);
            sqlParam[36] = new SqlParameter("@email_1", foreclosureCase.Email1);
            sqlParam[37] = new SqlParameter("@email_2", foreclosureCase.Email2);
            sqlParam[38] = new SqlParameter("@contact_addr1", foreclosureCase.ContactAddr1);
            sqlParam[39] = new SqlParameter("@contact_addr2", foreclosureCase.ContactAddr2);
            sqlParam[40] = new SqlParameter("@contact_city", foreclosureCase.ContactCity);
            sqlParam[41] = new SqlParameter("@contact_state_cd", foreclosureCase.ContactStateCd);
            sqlParam[42] = new SqlParameter("@contact_zip", foreclosureCase.ContactZip);
            sqlParam[43] = new SqlParameter("@contact_zip_plus_4", foreclosureCase.ContactZipPlus4);
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
            sqlParam[56] = new SqlParameter("@borrower_occupation_cd", foreclosureCase.BorrowerOccupationCd);
            sqlParam[57] = new SqlParameter("@co_borrower_occupation_cd", foreclosureCase.CoBorrowerOccupationCd);
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
            sqlParam[73] = new SqlParameter("@summary_sent_other_dt", foreclosureCase.SummarySentOtherDt);
            sqlParam[74] = new SqlParameter("@summary_sent_dt", foreclosureCase.SummarySentDt);
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
            sqlParam[107] = new SqlParameter("@fc_id", SqlDbType.Int){Direction = ParameterDirection.Output};
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

        /// <summary>
        /// Update a ForeclosureCase to database.
        /// </summary>
        /// <param name="foreclosureCase">ForeclosureCase</param>
        /// <returns>a new Fc_id</returns>
        public int UpdateForeclosureCase(ForeClosureCaseDTO foreclosureCase)
        {
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_foreclosure_case_update", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[108];
            sqlParam[0] = new SqlParameter("@agency_id", foreclosureCase.AgencyId);
            sqlParam[1] = new SqlParameter("@completed_dt", foreclosureCase.CompletedDt);
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
            sqlParam[16] = new SqlParameter("@hud_termination_dt", foreclosureCase.HudTerminationDt);
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
            sqlParam[27] = new SqlParameter("@borrower_DOB", foreclosureCase.BorrowerDob);
            sqlParam[28] = new SqlParameter("@co_borrower_fname", foreclosureCase.CoBorrowerFname);
            sqlParam[29] = new SqlParameter("@co_borrower_lname", foreclosureCase.CoBorrowerLname);
            sqlParam[30] = new SqlParameter("@co_borrower_mname", foreclosureCase.CoBorrowerMname);
            sqlParam[31] = new SqlParameter("@co_borrower_ssn", foreclosureCase.CoBorrowerSsn);
            sqlParam[32] = new SqlParameter("@co_borrower_last4_SSN", foreclosureCase.CoBorrowerLast4Ssn);
            sqlParam[33] = new SqlParameter("@co_borrower_DOB", foreclosureCase.CoBorrowerDob);
            sqlParam[34] = new SqlParameter("@primary_contact_no", foreclosureCase.PrimaryContactNo);
            sqlParam[35] = new SqlParameter("@second_contact_no", foreclosureCase.SecondContactNo);
            sqlParam[36] = new SqlParameter("@email_1", foreclosureCase.Email1);
            sqlParam[37] = new SqlParameter("@email_2", foreclosureCase.Email2);
            sqlParam[38] = new SqlParameter("@contact_addr1", foreclosureCase.ContactAddr1);
            sqlParam[39] = new SqlParameter("@contact_addr2", foreclosureCase.ContactAddr2);
            sqlParam[40] = new SqlParameter("@contact_city", foreclosureCase.ContactCity);
            sqlParam[41] = new SqlParameter("@contact_state_cd", foreclosureCase.ContactStateCd);
            sqlParam[42] = new SqlParameter("@contact_zip", foreclosureCase.ContactZip);
            sqlParam[43] = new SqlParameter("@contact_zip_plus_4", foreclosureCase.ContactZipPlus4);
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
            sqlParam[56] = new SqlParameter("@borrower_occupation_cd", foreclosureCase.BorrowerOccupationCd);
            sqlParam[57] = new SqlParameter("@co_borrower_occupation_cd", foreclosureCase.CoBorrowerOccupationCd);
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
            sqlParam[73] = new SqlParameter("@summary_sent_other_dt", foreclosureCase.SummarySentOtherDt);
            sqlParam[74] = new SqlParameter("@summary_sent_dt", foreclosureCase.SummarySentDt);
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
            sqlParam[107] = new SqlParameter("@fc_id", foreclosureCase.FcId);
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
            ForeClosureCaseSearchResult results = new ForeClosureCaseSearchResult();

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

