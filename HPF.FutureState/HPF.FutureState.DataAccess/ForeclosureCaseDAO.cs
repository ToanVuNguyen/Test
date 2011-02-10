﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common;

namespace HPF.FutureState.DataAccess
{
    public class ForeclosureCaseDAO : BaseDAO
    {
        private const string DELINQUENT_CODE_1ST = "1st";
        private const string COUNSELED_LESS_THAN_1YEAR = "<1yr";
        private const string COUNSELED_GREATER_THAN_1YEAR = ">1yr";
        protected ForeclosureCaseDAO()
        {

        }

        public static ForeclosureCaseDAO CreateInstance()
        {
            return new ForeclosureCaseDAO();
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
        /// get Foreclosure case
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
                        returnObject.AgencyMediaInterestInd = ConvertToString(reader["agency_media_interest_ind"]);
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
                        //returnObject.CaseCompleteInd = ConvertToString(reader["case_complete_ind"]);
                        returnObject.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                        returnObject.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                        returnObject.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
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
                        //returnObject.HpfNetworkCandidateInd = ConvertToString(reader["hpf_network_candidate_ind"]);
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
                        //returnObject.PrimResEstMktValue = ConvertToDouble(reader["prim_res_est_mkt_value"]);
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
                        returnObject.FcSaleDate = ConvertToDateTime(reader["fc_sale_dt"]);
                        returnObject.CaseSourceCd = ConvertToString(reader["case_source_cd"]);
                        returnObject.HouseholdGrossAnnualIncomeAmt = ConvertToDouble(reader["household_gross_annual_income_amt"]);
                        returnObject.IntakeCreditScore = ConvertToString(reader["intake_credit_score"]);
                        returnObject.IntakeCreditBureauCd = ConvertToString(reader["intake_credit_bureau_cd"]);
                        returnObject.LoanList = ConvertToString(reader["loan_list"]);
                        //description
                        returnObject.PropertyDesc = ConvertToString(reader["property_desc"]);
                        returnObject.GenderDesc = ConvertToString(reader["gender_desc"]);
                        returnObject.RaceDesc = ConvertToString(reader["race_desc"]);
                        returnObject.LanguageDesc = ConvertToString(reader["language_desc"]);
                        returnObject.EducationDesc = ConvertToString(reader["education_desc"]);
                        returnObject.MaritalDesc = ConvertToString(reader["marital_desc"]);
                        returnObject.MilitaryDesc = ConvertToString(reader["military_desc"]);
                        returnObject.CounselingDesc = ConvertToString(reader["counseling_desc"]);
                        returnObject.CaseSourceDesc = ConvertToString(reader["case_source_desc"]);
                        returnObject.SummaryDesc = ConvertToString(reader["summary_desc"]);
                        returnObject.DefaultReason1Desc = ConvertToString(reader["default_reason1_desc"]);
                        returnObject.DefaultReason2Desc = ConvertToString(reader["default_reason2_desc"]);
                        returnObject.HouseholdDesc = ConvertToString(reader["household_desc"]);
                        returnObject.IncomeDesc = ConvertToString(reader["income_desc"]);
                        returnObject.CreditDesc = ConvertToString(reader["credit_desc"]);
                        returnObject.HudTerminationDesc = ConvertToString(reader["hud_termination_desc"]);
                        returnObject.HudOutcomeDesc = ConvertToString(reader["hud_outcome_desc"]);
                        returnObject.ProgramName = ConvertToString(reader["program_name"]);
                        returnObject.CounselorAttemptedSrvcrContactInd = ConvertToString(reader["counselor_attempted_srvcr_contact_ind"]);
                        returnObject.DependentNum = ConvertToByte(reader["dependent_num"]);
                        returnObject.PrimaryContactNoTypeCd = ConvertToString(reader["primary_contact_no_type_cd"]);
                        returnObject.SecondContactNoTypeCd = ConvertToString(reader["second_contact_no_type_cd"]);
                        returnObject.PreferredContactTime = ConvertToString(reader["preferred_contact_time"]);

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
        /// return all search results retrieved from database
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public ForeclosureCaseSearchResult SearchForeclosureCase(ForeclosureCaseSearchCriteriaDTO searchCriteria, int pageSize)
        {
            ForeclosureCaseSearchResult results = new ForeclosureCaseSearchResult();
            SqlConnection dbConnection = base.CreateConnection();
            try
            {
                SqlCommand command = base.CreateCommand("hpf_foreclosure_case_search_ws", dbConnection);
                string whereClause = GenerateWhereClause(searchCriteria);
                //<Parameter>
                SqlParameter[] sqlParam = new SqlParameter[8];
                sqlParam[0] = new SqlParameter("@pi_agency_case_num", searchCriteria.AgencyCaseNumber);
                sqlParam[1] = new SqlParameter("@pi_borrower_fname", searchCriteria.FirstName);
                sqlParam[2] = new SqlParameter("@pi_borrower_lname", searchCriteria.LastName);
                sqlParam[3] = new SqlParameter("@pi_borrower_last4_SSN", searchCriteria.Last4_SSN);
                sqlParam[4] = new SqlParameter("@pi_prop_zip", searchCriteria.PropertyZip);
                sqlParam[5] = new SqlParameter("@pi_loan_number", searchCriteria.LoanNumber);
                sqlParam[6] = new SqlParameter("@pi_servicer", searchCriteria.Servicer);
                //sqlParam[7] = new SqlParameter("@pi_where_clause", whereClause);
               // sqlParam[8] = new SqlParameter("@pi_number_of_rows", pageSize);
                sqlParam[7] = new SqlParameter("@po_total_rows", SqlDbType.Int) { Direction = ParameterDirection.Output };

                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;

                dbConnection.Open();
                var reader = command.ExecuteReader();
                results = new ForeclosureCaseSearchResult();
                int rowCount = 0;
                int limitRowCount;
                if (!int.TryParse(HPFConfigurationSettings.WS_SEARCH_RESULT_MAXROW, out limitRowCount))
                    limitRowCount = 50;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        #region set value for ForeclosureCaseWSDTO
                        ForeclosureCaseWSDTO item = new ForeclosureCaseWSDTO();
                        //item.Counseled = ConvertToString(reader["counseled"]);
                        item.FcId = ConvertToInt(reader["fc_id"]);
                        item.AgencyClientNum = ConvertToString(reader["agency_client_num"]);
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
                        item.CounselorFullName = ConvertToString(reader["counselor_fname"]) + " " + ConvertToString(reader["counselor_lname"]);
                        item.CounselorPhone = ConvertToString(reader["counselor_phone"]);
                        item.CounselorExt = ConvertToString(reader["counselor_ext"]);
                        item.CounselorEmail = ConvertToString(reader["counselor_email"]);
                        item.CompletedDt = ConvertToDateTime(reader["completed_dt"]);
                        item.DelinquentCd = Show1STCodeOnly(ConvertToString(reader["Delinquent_cd"]), ConvertToString(reader["loan_1st_2nd_cd"]));
                        item.BankruptcyInd = ConvertToString(reader["bankruptcy_ind"]);
                        item.FcNoticeReceivedInd = ConvertToString(reader["fc_notice_received_ind"]);
                        item.LoanNumber = ConvertToString(reader["loan_number"]);
                        item.LoanServicer = ConvertToString(reader["loan_servicer"]);
                        item.AgencyCaseNum = ConvertToString(reader["agency_case_num"]);
                        item.CaseLoanID = ConvertToString(reader["case_loan_id"]);
                        item.Counseled = GetCounseledProperty(item.CompletedDt);
                        item.SummarySentDate = ConvertToDateTime(reader["summary_sent_dt"]);
                        item.SummarySentOtherDate = ConvertToDateTime(reader["summary_sent_other_dt"]);
                        item.SummarySentOtherCode = ConvertToString(reader["summary_sent_other_cd"]);
                        #endregion
                        results.Add(item);
                        rowCount++;
                        if (rowCount >= limitRowCount) break;
                    }
                    reader.Close();
                }
                int? resultCount = ConvertToInt(sqlParam[7].Value);
                results.SearchResultCount =  resultCount.HasValue ? resultCount.Value : 0;
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return results;
        }
        private string GenerateWhereClause(ForeclosureCaseSearchCriteriaDTO searchCriteria)
        {
            StringBuilder whereClause = new StringBuilder();
            whereClause.Append(" WHERE foreClosure_case.agency_id = Agency.agency_id");
            whereClause.Append(" AND foreclosure_case.fc_id = case_loan.fc_id");
            whereClause.Append(" AND case_loan.servicer_id = servicer.servicer_id");
            whereClause.Append((searchCriteria.Servicer <= 0) ? "" : " AND (case_loan.servicer_id=@pi_servicer)");
            //whereClause.Append(" AND case_loan.loan_1st_2nd_cd = '1st'");
            whereClause.Append(" AND (foreclosure_case.duplicate_ind = 'N' OR foreclosure_case.duplicate_ind is null)");
            //whereClause.Append((searchCriteria.AgencyCaseNumber == null) ? "" : " AND agency_case_num like @pi_agency_case_num ESCAPE '/'");
            //whereClause.Append((searchCriteria.FirstName == null) ? "" : " AND (borrower_fname like @pi_borrower_fname  ESCAPE '/' OR co_borrower_fname like @pi_borrower_fname  ESCAPE '/')");
            //whereClause.Append((searchCriteria.LastName == null) ? "" : " AND (borrower_lname like @pi_borrower_lname  ESCAPE '/' OR co_borrower_lname like @pi_borrower_lname  ESCAPE '/')");
            whereClause.Append(ParsingAgencyCaseNumber(searchCriteria.AgencyCaseNumber));
            whereClause.Append(ParsingFirstName(searchCriteria.FirstName));
            whereClause.Append(ParsingLastName(searchCriteria.LastName));
            whereClause.Append((string.IsNullOrEmpty(searchCriteria.Last4_SSN)) ? "" : " AND (borrower_last4_SSN = @pi_borrower_last4_SSN OR co_borrower_last4_SSN = @pi_borrower_last4_SSN)");
            whereClause.Append((string.IsNullOrEmpty(searchCriteria.LoanNumber)) ? " AND case_loan.loan_1st_2nd_cd = '1st'" : " AND replace(ltrim(rtrim(case_loan.acct_num)), '-', '')= @pi_loan_number");
            whereClause.Append((string.IsNullOrEmpty(searchCriteria.PropertyZip)) ? "" : " AND prop_zip = @pi_prop_zip");
            return whereClause.ToString();
        }

        private string ParsingAgencyCaseNumber(string s)
        {
            if (!string.IsNullOrEmpty(s))
                s = ContainingSQLSpecialCharacter(s) ? " AND agency_case_num like @pi_agency_case_num ESCAPE '/'" : " AND agency_case_num = @pi_agency_case_num";
            return s;
        }

        private string ParsingFirstName(string s)
        {
            if (!string.IsNullOrEmpty(s))
                s = ContainingSQLSpecialCharacter(s) ? " AND (borrower_fname like @pi_borrower_fname  ESCAPE '/' OR co_borrower_fname like @pi_borrower_fname  ESCAPE '/')"
                                                    : " AND (borrower_fname like @pi_borrower_fname OR co_borrower_fname like @pi_borrower_fname)";
            return s;
        }

        private string ParsingLastName(string s)
        {
            if (!string.IsNullOrEmpty(s))
                s = ContainingSQLSpecialCharacter(s) ? " AND (borrower_lname like @pi_borrower_lname  ESCAPE '/' OR co_borrower_lname like @pi_borrower_lname  ESCAPE '/')"
                                                    : " AND (borrower_lname like @pi_borrower_lname OR co_borrower_lname like @pi_borrower_lname)";
            return s;
        }

        private bool ContainingSQLSpecialCharacter(string s)
        {
            //if (s.Contains("/%") || s.Contains("/_") || s.Contains("/["))
            if (s.Contains("/"))
                return true;
            return false;
        }

        private string Show1STCodeOnly(string delinquentCd, string loan_1st_2nd)
        {
            if (string.IsNullOrEmpty(loan_1st_2nd))
                return string.Empty;
            return (loan_1st_2nd == DELINQUENT_CODE_1ST) ? delinquentCd : string.Empty;
        }
        private string GetCounseledProperty(DateTime? completedDt)
        {
            DateTime oneYearBefore = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
            if (!completedDt.HasValue)
                return COUNSELED_LESS_THAN_1YEAR;
            if (completedDt.Value.CompareTo(oneYearBefore) >= 0 && completedDt.Value.CompareTo(DateTime.Now) <= 0)
                return COUNSELED_LESS_THAN_1YEAR;

            return COUNSELED_GREATER_THAN_1YEAR;
        }
        /// <summary>
        /// Search ForeclosureCase
        /// </summary>
        /// <param name="searchCriteria">Criteria to search</param>
        /// <returns>Search Result</returns>
        public AppForeclosureCaseSearchResultDTOCollection AppSearchForeclosureCase(AppForeclosureCaseSearchCriteriaDTO searchCriteria)
        {
            AppForeclosureCaseSearchResultDTOCollection results = new AppForeclosureCaseSearchResultDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_foreclosure_case_search_app_dynamic", dbConnection);
            string whereclause = AppGenerateWhereClause(searchCriteria);
            var sqlParam = new SqlParameter[14];
            sqlParam[0] = new SqlParameter("@pi_last4SSN", searchCriteria.Last4SSN);
            sqlParam[1] = new SqlParameter("@pi_fname", searchCriteria.FirstName);
            sqlParam[2] = new SqlParameter("@pi_lname", searchCriteria.LastName);
            sqlParam[3] = new SqlParameter("@pi_fc_id", searchCriteria.ForeclosureCaseID);
            sqlParam[4] = new SqlParameter("@pi_agencycaseid", searchCriteria.AgencyCaseID);
            sqlParam[5] = new SqlParameter("@pi_loannum", searchCriteria.LoanNumber);
            sqlParam[6] = new SqlParameter("@pi_propzip", searchCriteria.PropertyZip);
            sqlParam[7] = new SqlParameter("@pi_propstate", searchCriteria.PropertyState);
            sqlParam[8] = new SqlParameter("@pi_duplicate", searchCriteria.Duplicates);
            sqlParam[9] = new SqlParameter("@pi_agencyid", searchCriteria.Agency);
            sqlParam[10] = new SqlParameter("@pi_programid ", searchCriteria.Program);
            //sqlParam[11] = new SqlParameter("@pi_pagesize", searchCriteria.PageSize);
            //sqlParam[12] = new SqlParameter("@pi_pagenum", searchCriteria.PageNum);
            sqlParam[11] = new SqlParameter("@po_totalrownum", searchCriteria.TotalRowNum) { Direction = ParameterDirection.Output };
            sqlParam[12] = new SqlParameter("@whereclause", whereclause);
            sqlParam[13] = new SqlParameter("@pi_servicer", searchCriteria.Servicer);
            command.Parameters.AddRange(sqlParam);
            //command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AppForeclosureCaseSearchResultDTO item = new AppForeclosureCaseSearchResultDTO();
                        item.CaseID = ConvertToString(reader["fc_id"]);
                        item.AgencyCaseNum = ConvertToString(reader["agency_case_num"]);
                        item.AgencyCaseID = ConvertToString(reader["agency_id"]);
                        item.CaseCompleteDate = ConvertToDateTime(reader["completed_dt"]);
                        item.CaseDate = ConvertToDateTime(reader["intake_dt"]);
                        item.BorrowerFirstName = ConvertToString(reader["borrower_fname"]);
                        item.BorrowerLastName = ConvertToString(reader["borrower_lname"]);
                        item.Last4SSN = ConvertToString(reader["borrower_last4_SSN"]);
                        item.CoborrowerFirstName = ConvertToString(reader["co_borrower_fname"]);
                        item.CoborrowerLastName = ConvertToString(reader["co_borrower_lname"]);
                        item.CoborrowerLast4SSN = ConvertToString(reader["co_borrower_last4_SSN"]);
                        item.PropertyAddress = ConvertToString(reader["prop_addr1"]);
                        item.PropertyCity = ConvertToString(reader["prop_city"]);
                        item.PropertyState = ConvertToString(reader["prop_state_cd"]);
                        item.PropertyZip = ConvertToString(reader["prop_zip"]);
                        item.AgencyName = ConvertToString(reader["agency_name"]);
                        item.AgentFirstName = ConvertToString(reader["counselor_fname"]);
                        item.AgentLastName = ConvertToString(reader["counselor_lname"]);
                        item.AgentEmail = ConvertToString(reader["counselor_email"]);
                        item.AgentPhone = ConvertToString(reader["counselor_phone"]);
                        item.AgentExtension = ConvertToString(reader["counselor_ext"]);
                        item.LoanList = ConvertToString(reader["loan_list"]);
                        item.BankruptcyIndicator = ConvertToString(reader["bankruptcy_ind"]);
                        item.ForeclosureNoticeReceivedIndicator = ConvertToString(reader["fc_notice_received_ind"]);
                        results.Add(item);
                    }
                    reader.Close();
                    results.SearchResultCount = Convert.ToDouble(sqlParam[11].Value);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return results;
        }
        /// <summary>
        /// Get ID and Name from table Program to bind on DDLB
        /// </summary>
        /// <returns>ProgramDTOCollection contains all Program </returns>
        public ProgramDTOCollection AppGetProgram()
        {
            ProgramDTOCollection result = new ProgramDTOCollection();
            var dbConnection = CreateConnection();
            var command = new SqlCommand("hpf_program_get", dbConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = dbConnection;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new ProgramDTO();
                        item.ProgramID = ConvertToString(reader["program_id"]);
                        item.ProgramName = ConvertToString(reader["program_name"]);
                        result.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return result;
        }
        /// <summary>
        /// Get ID and Name from table Agency to bind on DDLB
        /// </summary>
        /// <returns>AgencyDTOCollection contains all Agency </returns>
        public AgencyDTOCollection AppGetAgency()
        {
            AgencyDTOCollection results = new AgencyDTOCollection();
            //results = HPFCacheManager.Instance.GetData<AgencyDTOCollection>("refAgency");
                var dbConnection = CreateConnection();
                var command = new SqlCommand("hpf_agency_get", dbConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = dbConnection;
                try
                {
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var item = new AgencyDTO();
                            item.AgencyID = ConvertToString(reader["agency_id"]);
                            item.AgencyName = ConvertToString(reader["agency_name"]);
                            results.Add(item);
                        }
                        reader.Close();
                        //HPFCacheManager.Instance.Add("refAgency", results);
                    }
                }
                catch (Exception ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(ex);
                }
                finally
                {
                    dbConnection.Close();
                }
            
            return results;
        }
        ///<summary>
        ///

        private string AppGenerateWhereClause(AppForeclosureCaseSearchCriteriaDTO searchCriteria)
        {
            StringBuilder whereClause = new StringBuilder();
            whereClause.Append((searchCriteria.Last4SSN == null) ? "" : " AND(borrower_last4_SSN = @pi_last4SSN  OR co_borrower_last4_SSN = @pi_last4SSN)");
            whereClause.Append((searchCriteria.FirstName == null) ? "" : " AND (borrower_fname like @pi_fname ESCAPE '/'  OR co_borrower_fname like @pi_fname ESCAPE '/')");
            whereClause.Append((searchCriteria.LastName == null) ? "" : " AND (borrower_lname like @pi_lname ESCAPE '/'  OR co_borrower_lname like @pi_lname ESCAPE '/')");
            whereClause.Append((searchCriteria.ForeclosureCaseID == -1) ? "" : "AND ( f.fc_id = @pi_fc_id)");
            whereClause.Append((searchCriteria.LoanNumber == null) ? "" : "AND(replace(ltrim(rtrim(l.acct_num)), '-', '') = @pi_loannum)");
            whereClause.Append((searchCriteria.PropertyZip == null) ? "" : " AND( f.prop_zip = @pi_propzip)");
            whereClause.Append((searchCriteria.PropertyState == null) ? "" : " AND (f.prop_state_cd = @pi_propstate )");
            whereClause.Append((searchCriteria.Agency == -1) ? "" : " AND (f.agency_id=@pi_agencyid )");
            whereClause.Append((searchCriteria.AgencyCaseID == null) ? "" : " AND (f.agency_case_num=@pi_agencycaseid)");
            whereClause.Append((searchCriteria.Duplicates == null) ? "" : " AND (f.duplicate_ind = @pi_duplicate )");
            whereClause.Append((searchCriteria.Servicer == -1) ? "" : " AND (l.servicer_id=@pi_servicer)");

            whereClause.Append((searchCriteria.Program == -1) ? "" : " AND(f.program_id= @pi_programid)");
            return whereClause.ToString();
        }
        ///summary      
        ///
        public int? UpdateAppForeclosureCase(ForeclosureCaseDTO foreclosureCase)
        {
            var dbConnection = CreateConnection();
            dbConnection.Open();
            var trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            var command = CreateSPCommand("hpf_foreclosure_case_update_app", dbConnection);
            command.Transaction = trans;
            var sqlParam = new SqlParameter[16];
            sqlParam[0] = new SqlParameter("@pi_agency_id", foreclosureCase.AgencyId);
            sqlParam[1] = new SqlParameter("@pi_duplicate_ind", foreclosureCase.DuplicateInd);
            sqlParam[2] = new SqlParameter("@pi_loan_dflt_reason_notes", foreclosureCase.LoanDfltReasonNotes);
            sqlParam[3] = new SqlParameter("@pi_action_items_notes", foreclosureCase.ActionItemsNotes);
            sqlParam[4] = new SqlParameter("@pi_followup_notes", foreclosureCase.FollowupNotes);
            sqlParam[5] = new SqlParameter("@pi_opt_out_newsletter_ind", foreclosureCase.OptOutNewsletterInd);
            sqlParam[6] = new SqlParameter("@pi_opt_out_survey_ind", foreclosureCase.OptOutSurveyInd);
            sqlParam[7] = new SqlParameter("@pi_do_not_call_ind", foreclosureCase.DoNotCallInd);
            sqlParam[8] = new SqlParameter("@pi_hpf_media_candidate_ind", foreclosureCase.HpfMediaCandidateInd);
            sqlParam[9] = new SqlParameter("@pi_hpf_success_story_ind", foreclosureCase.HpfSuccessStoryInd);
            sqlParam[10] = new SqlParameter("@pi_fc_id", foreclosureCase.FcId);
            sqlParam[11] = new SqlParameter("@pi_never_pay_reason",null);
            sqlParam[12] = new SqlParameter("@pi_never_bill_reason",null);
            sqlParam[13] = new SqlParameter("@pi_chg_lst_dt",foreclosureCase.ChangeLastDate);
            sqlParam[14] = new SqlParameter("@pi_chg_lst_user_id",foreclosureCase.ChangeLastUserId);
            sqlParam[15] = new SqlParameter("@pi_chg_lst_app_name",foreclosureCase.ChangeLastAppName);
            //</Parameter>            
            command.Parameters.AddRange(sqlParam);
            //<Parameter>
            try
            {
                command.ExecuteNonQuery();
                foreclosureCase.FcId = ConvertToInt(sqlParam[10].Value);
                trans.Commit();
            }
            catch (Exception Ex)
            {
                trans.Rollback();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return foreclosureCase.FcId;
        }

        public int[] FCSearchToSendSummaries(AppSummariesToServicerCriteriaDTO searchCriteria)
        {
            ArrayList results = new ArrayList();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_foreclosure_case_SummariesToServicer_Search", dbConnection);
            var sqlParam = new SqlParameter[3];

            sqlParam[0] = new SqlParameter("@pi_servicer_id", searchCriteria.ServicerId);
            sqlParam[1] = new SqlParameter("@pi_start_dt", searchCriteria.StartDt);
            sqlParam[2] = new SqlParameter("@pi_end_dt", searchCriteria.EndDt.Value.AddDays(1));
            
            try
            {
                command.Parameters.AddRange(sqlParam);
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        results.Add(ConvertToInt(reader["fc_id"]).Value);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return (int[])results.ToArray(typeof(int));
        }
        public int MarkDuplicateCases(string fcIdList, string updateUser)
        {
            int rowCount = 0;
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_foreclosure_case_mark_duplicate", dbConnection);
            var sqlParam = new SqlParameter[4];

            sqlParam[0] = new SqlParameter("@pi_fc_id_list", fcIdList);
            sqlParam[1] = new SqlParameter("@pi_change_app_name", HPFConfigurationSettings.HPF_APPLICATION_NAME);
            sqlParam[2] = new SqlParameter("@pi_change_user_id", updateUser);
            sqlParam[3] = new SqlParameter("@po_row_count", SqlDbType.Int) { Direction = ParameterDirection.Output };

            try
            {
                command.Parameters.AddRange(sqlParam);
                dbConnection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                rowCount = ConvertToInt(sqlParam[3].Value).Value;
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return rowCount;
        }
    }
}

