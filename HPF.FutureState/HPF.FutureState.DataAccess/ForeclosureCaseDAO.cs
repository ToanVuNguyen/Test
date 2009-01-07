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
    public class ForeclosureCaseDAO : BaseDAO
    {
        
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
        public ForeclosureCaseDTO GetForeclosureCase(int fcId)
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
                        //returnObject.BorrowerSsn = ConvertToString(reader["borrower_ssn"]);

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
                        returnObject.FcSaleDate = ConvertToDateTime(reader["fc_sale_dt"]);
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
                SqlParameter[] sqlParam = new SqlParameter[9];
                sqlParam[0] = new SqlParameter("@pi_agency_case_num", searchCriteria.AgencyCaseNumber);
                sqlParam[1] = new SqlParameter("@pi_borrower_fname", searchCriteria.FirstName);
                sqlParam[2] = new SqlParameter("@pi_borrower_lname", searchCriteria.LastName);
                sqlParam[3] = new SqlParameter("@pi_borrower_last4_SSN", searchCriteria.Last4_SSN);
                sqlParam[4] = new SqlParameter("@pi_prop_zip", searchCriteria.PropertyZip);
                sqlParam[5] = new SqlParameter("@pi_loan_number", searchCriteria.LoanNumber);
                sqlParam[6] = new SqlParameter("@pi_where_clause", whereClause);
                sqlParam[7] = new SqlParameter("@pi_number_of_rows", pageSize);
                sqlParam[8] = new SqlParameter("@po_total_rows", SqlDbType.Int) { Direction = ParameterDirection.Output };

                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;

                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new ForeclosureCaseSearchResult();
                    while (reader.Read())
                    {
                        #region set value for ForeclosureCaseWSDTO
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
                        item.CounselorFullName = ConvertToString(reader["counselor_fname"]) + " " + ConvertToString(reader["counselor_fname"]);
                        item.CounselorPhone = ConvertToString(reader["counselor_phone"]);
                        item.CounselorExt = ConvertToString(reader["counselor_ext"]);
                        item.CounselorEmail = ConvertToString(reader["counselor_email"]);
                        item.CompletedDt = ConvertToDateTime(reader["completed_dt"]);
                        item.DelinquentCd = ConvertToString(reader["Delinquent_cd"]);
                        item.BankruptcyInd = ConvertToString(reader["bankruptcy_ind"]);
                        item.FcNoticeReceivedInd = ConvertToString(reader["fc_notice_received_ind"]);
                        item.LoanNumber = ConvertToString(reader["loan_number"]);
                        item.LoanServicer = ConvertToString(reader["loan_servicer"]);
                        item.AgencyCaseNum = ConvertToString(reader["agency_case_num"]);
                        item.CaseLoanID = ConvertToString(reader["case_loan_id"]);
                        item.Counseled = GetCounseledProperty(item.CompletedDt);
                        #endregion

                        results.Add(item);
                    }
                    reader.Close();
                }
                results.SearchResultCount = ConvertToInt(sqlParam[8].Value);
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
            whereClause.Append(" AND case_loan.loan_1st_2nd_cd = '1st'");
            whereClause.Append((searchCriteria.AgencyCaseNumber == null) ? "" : " AND agency_case_num = @pi_agency_case_num");
            whereClause.Append((searchCriteria.FirstName == null) ? "" : " AND (borrower_fname like @pi_borrower_fname OR co_borrower_fname like @pi_borrower_fname)");
            whereClause.Append((searchCriteria.LastName == null) ? "" : " AND (borrower_lname like @pi_borrower_lname OR co_borrower_lname like @pi_borrower_lname)");
            whereClause.Append((searchCriteria.Last4_SSN == null) ? "" : " AND (borrower_last4_SSN = @pi_borrower_last4_SSN OR co_borrower_last4_SSN = @pi_borrower_last4_SSN)");
            whereClause.Append((searchCriteria.LoanNumber == null) ? "" : " AND loan_list like @pi_loan_number");
            whereClause.Append((searchCriteria.PropertyZip == null) ? "" : " AND prop_zip = @pi_prop_zip");
           
            return whereClause.ToString();        
        }

        private string GetCounseledProperty(DateTime completedDt)
        {
            DateTime oneYearBefore = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day - 1);
            if (completedDt.CompareTo(oneYearBefore) >= 0 && completedDt.CompareTo(DateTime.Now) <= 0)
                return "<1yr";
            return ">1yr";
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
            var command = new SqlCommand("hpf_foreclosure_case_search_app_dynamic", dbConnection);
            string whereclause = AppGenerateWhereClause(searchCriteria);
            var sqlParam = new SqlParameter[15];
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
            sqlParam[11] = new SqlParameter("@pi_pagesize", searchCriteria.PageSize);
            sqlParam[12] = new SqlParameter("@pi_pagenum", searchCriteria.PageNum);
            sqlParam[13] = new SqlParameter("@po_totalrownum", searchCriteria.TotalRowNum) { Direction = ParameterDirection.Output };
            sqlParam[14] = new SqlParameter("@whereclause", whereclause);
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
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
                    results.SearchResultCount = Convert.ToDouble(sqlParam[13].Value);
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
        /// Get ID and Name from table State to bind on DDLB
        /// </summary>
        /// <returns>StateDTOCollection contains all State</returns>
        public StateDTOCollection AppGetState()
        {
            StateDTOCollection result = new StateDTOCollection();
            RefCodeItemDTOCollection refcodeitems = RefCodeItemDAO.Instance.GetRefCodeItem();
            try
            {
                StateDTO itemdefault = new StateDTO();
                itemdefault.StateName = "ALL";
                result.Add(itemdefault);
                foreach (RefCodeItemDTO refcodeitem in refcodeitems)
                {
                    StateDTO item = new StateDTO();
                    item.StateName = refcodeitem.Code;
                    result.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
           
            return result;
        }

        /// <summary>
        /// Get ID and Name from table Agency to bind on DDLB
        /// </summary>
        /// <returns>AgencyDTOCollection contains all Agency </returns>
        public AgencyDTOCollection AppGetAgency()
        {
            AgencyDTOCollection result = new AgencyDTOCollection();

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
        ///<summary>
        ///

        private string AppGenerateWhereClause( AppForeclosureCaseSearchCriteriaDTO searchCriteria)
        {
            StringBuilder whereClause = new StringBuilder();
            whereClause.Append((searchCriteria.Last4SSN == null) ? "" : " AND(borrower_last4_SSN = @pi_last4SSN  OR co_borrower_last4_SSN = @pi_last4SSN)");
            whereClause.Append((searchCriteria.FirstName == null) ? "" : " AND (borrower_fname like @pi_fname  OR co_borrower_fname like @pi_fname)");
            whereClause.Append((searchCriteria.LastName == null) ? "" : " AND (borrower_lname like @pi_lname  OR co_borrower_lname like @pi_lname)");
            whereClause.Append((searchCriteria.ForeclosureCaseID ==-1) ? "" : "AND ( f.fc_id = @pi_fc_id)");
            whereClause.Append((searchCriteria.LoanNumber == null) ? "" : "AND( l.acct_num  = @pi_loannum)");
            whereClause.Append((searchCriteria.PropertyZip == null) ? "" : " AND( f.prop_zip = @pi_propzip)");
            whereClause.Append((searchCriteria.PropertyState == null) ? "" : " AND (f.prop_state_cd = @pi_propstate )");
            whereClause.Append((searchCriteria.Agency == -1) ? "" : " AND (f.agency_id=@pi_agencyid )");
            whereClause.Append((searchCriteria.AgencyCaseID == null) ? "" : " AND (f.agency_case_num=@pi_agencycaseid)");
            whereClause.Append((searchCriteria.Duplicates == null) ? "" : " AND (f.duplicate_ind = @pi_duplicate )");
            whereClause.Append((searchCriteria.Program == -1) ? "" : " AND(f.program_id= @pi_programid)");
            return whereClause.ToString();
        }
        ///summary      
        ///
        public int InsertAppForeclosureCase(ForeclosureCaseDTO foreclosureCase)
        {
            var dbConnection = CreateConnection();
            dbConnection.Open();
            var command = CreateSPCommand("hpf_foreclosure_case_insert_app", dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[11];
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
                //</Parameter>            
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                foreclosureCase.FcId = ConvertToInt(sqlParam[10].Value);
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally {
                dbConnection.Close();
            }
            return foreclosureCase.FcId;
        }
       

    }
}

