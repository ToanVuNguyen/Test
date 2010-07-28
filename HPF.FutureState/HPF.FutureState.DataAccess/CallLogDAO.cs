using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common;

using System.Collections.Generic;

namespace HPF.FutureState.DataAccess
{
    public class CallLogDAO : BaseDAO
    {
        private static readonly CallLogDAO instance = new CallLogDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CallLogDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected CallLogDAO()
        {
            
        }

        /// <summary>
        /// Insert a CallLog to database.
        /// </summary>
        /// <param name="aCallLog">CallLogDTO</param>
        /// <returns>a new CallLogId</returns>
        public int? InsertCallLog(CallLogDTO aCallLog)
        {
        
            SqlConnection dbConnection = CreateConnection();
            SqlCommand command = CreateSPCommand("hpf_call_insert", dbConnection);
            SqlTransaction trans = null;

            #region parameters
            //<Parameter>
            SqlParameter[] sqlParam = new SqlParameter[60];
            sqlParam[0] = new SqlParameter("@pi_call_center_id", aCallLog.CallCenterID);
            sqlParam[1] = new SqlParameter("@pi_cc_agent_id_key", aCallLog.CcAgentIdKey);            
            sqlParam[2] = new SqlParameter("@pi_start_dt", NullableDateTime(aCallLog.StartDate));
            sqlParam[3] = new SqlParameter("@pi_end_dt", NullableDateTime(aCallLog.EndDate));
            sqlParam[4] = new SqlParameter("@pi_dnis", aCallLog.DNIS);
            sqlParam[5] = new SqlParameter("@pi_call_center", aCallLog.CallCenter);
            sqlParam[6] = new SqlParameter("@pi_call_source_cd", aCallLog.CallSourceCd);
            sqlParam[7] = new SqlParameter("@pi_reason_for_call", aCallLog.ReasonForCall);
            sqlParam[8] = new SqlParameter("@pi_loan_acct_num", aCallLog.LoanAccountNumber);
            sqlParam[9] = new SqlParameter("@pi_fname", aCallLog.FirstName);
            sqlParam[10] = new SqlParameter("@pi_lname", aCallLog.LastName);
            sqlParam[11] = new SqlParameter("@pi_servicer_id", NullableInteger(aCallLog.ServicerId));
            sqlParam[12] = new SqlParameter("@pi_other_servicer_name", aCallLog.OtherServicerName);
            sqlParam[13] = new SqlParameter("@pi_prop_zip_full9", aCallLog.PropZipFull9);
            sqlParam[14] = new SqlParameter("@pi_prev_agency_id", NullableInteger(aCallLog.PrevAgencyId));
            sqlParam[15] = new SqlParameter("@pi_selected_agency_id", aCallLog.SelectedAgencyId);
            sqlParam[16] = new SqlParameter("@pi_screen_rout", aCallLog.ScreenRout);
            sqlParam[17] = new SqlParameter("@pi_final_dispo_cd", aCallLog.FinalDispoCd);
            sqlParam[18] = new SqlParameter("@pi_trans_num", aCallLog.TransNumber);
            sqlParam[19] = new SqlParameter("@pi_cc_call_key", aCallLog.CcCallKey);
            sqlParam[20] = new SqlParameter("@pi_loan_delinq_status_cd", aCallLog.LoanDelinqStatusCd);
            sqlParam[21] = new SqlParameter("@pi_selected_counselor", aCallLog.SelectedCounselor);
            sqlParam[22] = new SqlParameter("@pi_homeowner_ind", aCallLog.HomeownerInd);
            sqlParam[23] = new SqlParameter("@pi_power_of_attorney_ind", aCallLog.PowerOfAttorneyInd);
            sqlParam[24] = new SqlParameter("@pi_authorized_ind", aCallLog.AuthorizedInd);
            sqlParam[25] = new SqlParameter("@pi_create_dt", NullableDateTime(aCallLog.CreateDate));
            sqlParam[26] = new SqlParameter("@pi_create_user_id", aCallLog.CreateUserId);
            sqlParam[27] = new SqlParameter("@pi_create_app_name", aCallLog.CreateAppName);
            sqlParam[28] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(aCallLog.ChangeLastDate));
            sqlParam[29] = new SqlParameter("@pi_chg_lst_user_id", aCallLog.ChangeLastUserId);
            sqlParam[30] = new SqlParameter("@pi_chg_lst_app_name", aCallLog.ChangeLastAppName);

            sqlParam[31] = new SqlParameter("@pi_city", aCallLog.City);
            sqlParam[32] = new SqlParameter("@pi_state", aCallLog.State);
            sqlParam[33] = new SqlParameter("@pi_nonprofitreferral_key_num1", aCallLog.NonprofitReferralKeyNum1);
            sqlParam[34] = new SqlParameter("@pi_nonprofitreferral_key_num2", aCallLog.NonprofitReferralKeyNum2);
            sqlParam[35] = new SqlParameter("@pi_nonprofitreferral_key_num3", aCallLog.NonprofitReferralKeyNum3);

            sqlParam[36] = new SqlParameter("@pi_deling_ind", aCallLog.DelinqInd);
            sqlParam[37] = new SqlParameter("@pi_prop_street_addr", aCallLog.PropStreetAddress);
            sqlParam[38] = new SqlParameter("@pi_prim_res_ind", aCallLog.PrimaryResidenceInd);
            sqlParam[39] = new SqlParameter("@pi_max_loan_amt_ind", aCallLog.MaxLoanAmountInd);
            sqlParam[40] = new SqlParameter("@pi_cust_phone", aCallLog.CustomerPhone);
            sqlParam[41] = new SqlParameter("@pi_loan_lookup_cd", aCallLog.LoanLookupCd);
            sqlParam[42] = new SqlParameter("@pi_org_prior2009_ind", aCallLog.OriginatedPrior2009Ind);
            sqlParam[43] = new SqlParameter("@pi_payment_amt", aCallLog.PaymentAmount);
            sqlParam[44] = new SqlParameter("@pi_gross_inc_amt", aCallLog.GrossIncomeAmount);
            sqlParam[45] = new SqlParameter("@pi_dti_ind", aCallLog.DTIInd);
            sqlParam[46] = new SqlParameter("@pi_servicer_ca_num", aCallLog.ServicerCANumber);
            sqlParam[47] = new SqlParameter("@pi_servicer_ca_last_contact_dt", aCallLog.ServicerCALastContactDate);
            sqlParam[48] = new SqlParameter("@pi_servicer_ca_id", aCallLog.ServicerCAId);
            sqlParam[49] = new SqlParameter("@pi_servicer_ca_other_name", aCallLog.ServicerCAOtherName);
            sqlParam[50] = new SqlParameter("@pi_mha_info_share_ind", aCallLog.MHAInfoShareInd);
            sqlParam[51] = new SqlParameter("@pi_ict_call_id", aCallLog.ICTCallId);
            sqlParam[52] = new SqlParameter("@pi_servicer_complaint_cd", aCallLog.ServicerComplaintCd);
            sqlParam[53] = new SqlParameter("@pi_mha_script_started_ind", aCallLog.MHAScriptStartedInd);             
            
            sqlParam[54] = new SqlParameter("@pi_mother_maiden_lname", aCallLog.MotherMaidenLastName);             
            
            sqlParam[55] = new SqlParameter("@pi_unemployed_ind", aCallLog.UnemployedInd);             
            sqlParam[56] = new SqlParameter("@pi_up_benefits_ind", aCallLog.UpBenefitsInd);             
            sqlParam[57] = new SqlParameter("@pi_previous_up_ind", aCallLog.PreviousUpInd);             
            sqlParam[58] = new SqlParameter("@pi_fc_sale_dt", aCallLog.FCSaleDate);             
            
            sqlParam[59] = new SqlParameter("@po_call_id", SqlDbType.Int) {Direction = ParameterDirection.Output};
            //</Parameter>
            #endregion

            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                command.Transaction = trans;
            
                command.ExecuteNonQuery();
                trans.Commit();
                aCallLog.CallId = ConvertToInt(command.Parameters["@po_call_id"].Value);
            }
            catch(SqlException Ex)
            {
                if (trans != null) trans.Rollback();
                if (Ex.Errors[0].Number == 2627)
                {
                    ExceptionMessageCollection errorList = new ExceptionMessageCollection();
                    errorList.AddExceptionMessage(ErrorMessages.ERR0399, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0399));
                    throw new DataValidationException(errorList);
                }
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return aCallLog.CallId;
        }
             
        public Dictionary<string, int?> GetForeignKey(CallLogDTO aCallLog)
        {
            Dictionary<string, int?> idList = new Dictionary<string, int?>();

            SqlConnection dbConnection = CreateConnection();
            SqlCommand command = CreateSPCommand("hpf_call_check_foreign_key", dbConnection);
            
            
            int? callCenterID = 0;
            //int isValidCCAgentIdKey = 1;
            int? prevAgencyID = 0;
            int? selectedAgencyId = 0;
            int? servicerID = 0;

           

            command.Parameters.Add(new SqlParameter("@pi_call_center_id", aCallLog.CallCenterID));
            command.Parameters.Add(new SqlParameter("@pi_prev_agency_id", aCallLog.PrevAgencyId));
            command.Parameters.Add(new SqlParameter("@pi_servicer_id", aCallLog.ServicerId));
            command.Parameters.Add(new SqlParameter("@pi_selected_agency_id", aCallLog.SelectedAgencyId));
            command.Parameters.Add(new SqlParameter("@po_call_center_id", SqlDbType.Int) { Direction = ParameterDirection.Output });
            command.Parameters.Add(new SqlParameter("@po_prev_agency_id", SqlDbType.Int) { Direction = ParameterDirection.Output });
            command.Parameters.Add(new SqlParameter("@po_servicer_id", SqlDbType.Int) { Direction = ParameterDirection.Output });
            command.Parameters.Add(new SqlParameter("@po_selected_agency_id", SqlDbType.Int) { Direction = ParameterDirection.Output });
            
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                command.ExecuteNonQuery();
                
                callCenterID = ConvertToInt(command.Parameters["@po_call_center_id"].Value);
                prevAgencyID = ConvertToInt(command.Parameters["@po_prev_agency_id"].Value);
                servicerID = ConvertToInt(command.Parameters["@po_servicer_id"].Value);
                selectedAgencyId = ConvertToInt(command.Parameters["@po_selected_agency_id"].Value);                        

            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            
            idList.Add("CallCenterID", callCenterID);
            idList.Add("PrevAgencyID", prevAgencyID);
            idList.Add("ServicerID", servicerID);
            idList.Add("SelectedAgencyID", selectedAgencyId);
            return idList;

        }

        public bool GetCall(string callID)
        {
            bool results = false;
            var dbConnection = CreateConnection();
            try
            {
                SqlCommand command = base.CreateCommand("hpf_call_get", dbConnection);
                //<Parameter>
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@pi_call_id", callID);

                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = true;
                    reader.Close();
                }
                else
                {
                    reader.Close();
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
            return results;
        }    

        public CallCenterDTO GetCallCenter(CallLogDTO aCallLog)
        {
            CallCenterDTO callCenter = null;
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_call_center_get", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_call_center_id", aCallLog.CallCenterID);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    callCenter = new CallCenterDTO();
                    if (reader.Read())
                    {
                        callCenter.CallCenterID = aCallLog.CallCenterID;
                        callCenter.CallCenterName = ConvertToString(reader["call_center_name"]);
                    }
                    reader.Close();
                }
                dbConnection.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return callCenter;
        }

        /// <summary>
        /// Read a CallLog in Database by CallLogId
        /// </summary>
        /// <param name="callLogId">CallLogId</param>
        /// <returns>CallLogDTO</returns>
        public CallLogWSReturnDTOCollection ICTReadCallLog(string ICTcallLogId)
        {
            CallLogWSReturnDTOCollection results = new CallLogWSReturnDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_call_get_ICT", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_ict_call_id", ICTcallLogId);
            //</Parameter>
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
                        #region set value
                        CallLogWSReturnDTO callLogDTO = new CallLogWSReturnDTO();
                        callLogDTO.HopeNetCallId = "HPF" + ConvertToInt(reader["call_id"]);
                        //callLogDTO.CcAgentIdKey = ConvertToString(reader["cc_agent_id_key"]);
                        callLogDTO.StartDate = ConvertToDateTime(reader["start_dt"]);
                        callLogDTO.EndDate = ConvertToDateTime(reader["end_dt"]);
                        //callLogDTO.DNIS = ConvertToString(reader["dnis"]);
                        //callLogDTO.CallCenter = ConvertToString(reader["call_center_name"]);
                        callLogDTO.CallSourceCd = ConvertToString(reader["call_source_cd"]);
                        callLogDTO.ReasonForCall = ConvertToString(reader["reason_for_call"]);
                        callLogDTO.LoanAccountNumber = ConvertToString(reader["loan_acct_num"]);
                        callLogDTO.FirstName = ConvertToString(reader["fname"]);
                        callLogDTO.LastName = ConvertToString(reader["lname"]);

                        callLogDTO.MotherMaidenLastName = ConvertToString(reader["mother_maiden_lname"]);

                        callLogDTO.ServicerId = ConvertToInt(reader["servicer_id"]);
                        callLogDTO.OtherServicerName = ConvertToString(reader["other_servicer_name"]);
                        callLogDTO.PropZipFull9 = ConvertToString(reader["prop_zip_full9"]);
                        //callLogDTO.PrevAgencyId = ConvertToInt(reader["prev_agency_id"]);
                        //callLogDTO.SelectedAgencyId = ConvertToInt(reader["selected_agency_id"]);
                        //callLogDTO.ScreenRout = ConvertToString(reader["screen_rout"]);
                        callLogDTO.FinalDispoCd = ConvertToString(reader["final_dispo_cd"]);
                        //callLogDTO.TransNumber = ConvertToString(reader["trans_num"]);
                        //callLogDTO.CreateDate = ConvertToDateTime(reader["create_dt"]);
                        //callLogDTO.CreateUserId = ConvertToString(reader["create_user_id"]);
                        //callLogDTO.CreateAppName = ConvertToString(reader["create_app_name"]);
                        //callLogDTO.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                        //callLogDTO.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                        //callLogDTO.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                        //callLogDTO.CcCallKey = ConvertToString(reader["cc_call_key"]);
                        callLogDTO.LoanDelinqStatusCd = ConvertToString(reader["loan_delinq_status_cd"]);
                        //callLogDTO.SelectedCounselor = ConvertToString(reader["selected_counselor"]);
                        callLogDTO.HomeownerInd = ConvertToString(reader["homeowner_ind"]);
                        callLogDTO.PowerOfAttorneyInd = ConvertToString(reader["power_of_attorney_ind"]);
                        callLogDTO.AuthorizedInd = ConvertToString(reader["authorized_ind"]);
                        callLogDTO.City = ConvertToString(reader["city"]);
                        callLogDTO.State = ConvertToString(reader["state"]);
                        callLogDTO.NonprofitReferralKeyNum1 = ConvertToString(reader["nonprofitreferral_key_num1"]);
                        callLogDTO.NonprofitReferralKeyNum2 = ConvertToString(reader["nonprofitreferral_key_num2"]);
                        callLogDTO.NonprofitReferralKeyNum3 = ConvertToString(reader["nonprofitreferral_key_num3"]);

                        callLogDTO.DelinqInd = ConvertToString(reader["delinq_ind"]);
                        callLogDTO.PropStreetAddress = ConvertToString(reader["prop_street_addr"]);
                        callLogDTO.PrimaryResidenceInd = ConvertToString(reader["prim_res_ind"]);
                        callLogDTO.MaxLoanAmountInd = ConvertToString(reader["max_loan_amt_ind"]);
                        callLogDTO.CustomerPhone = ConvertToString(reader["cust_phone"]);
                        callLogDTO.LoanLookupCd = ConvertToString(reader["loan_lookup_cd"]);
                        callLogDTO.OriginatedPrior2009Ind = ConvertToString(reader["orig_prior2009_ind"]);
                        callLogDTO.PaymentAmount = ConvertToDouble(reader["payment_amt"]);
                        callLogDTO.GrossIncomeAmount = ConvertToDouble(reader["gross_inc_amt"]);
                        callLogDTO.DTIInd = ConvertToString(reader["dti_ind"]);
                        callLogDTO.ServicerCANumber = ConvertToInt(reader["servicer_ca_num"]);
                        callLogDTO.ServicerCALastContactDate = ConvertToDateTime(reader["servicer_ca_last_contact_dt"]);
                        callLogDTO.ServicerCAId = ConvertToInt(reader["servicer_ca_id"]);
                        callLogDTO.ServicerCAOtherName = ConvertToString(reader["servicer_ca_other_name"]);
                        callLogDTO.MHAInfoShareInd = ConvertToString(reader["mha_info_share_ind"]);
                        callLogDTO.ICTCallId = ConvertToString(reader["ict_call_id"]);
                        callLogDTO.MHAEligibilityCd = ConvertToString(reader["mha_eligibility_cd"]);

                        callLogDTO.MHAIneligibilityReasonCd = ConvertToString(reader["mha_inelig_reason_cd"]);
                        callLogDTO.ServicerName = ConvertToString(reader["servicer_name"]);
                        callLogDTO.FinalDispoDesc = ConvertToString(reader["final_dispo_desc"]);
                        callLogDTO.NonprofitReferralDesc1 = ConvertToString(reader["nonprofitreferral_desc1"]);
                        callLogDTO.NonprofitReferralDesc2 = ConvertToString(reader["nonprofitreferral_desc2"]);
                        callLogDTO.NonprofitReferralDesc3 = ConvertToString(reader["nonprofitreferral_desc3"]);
                        callLogDTO.ServicerCAName = ConvertToString(reader["servicer_ca_name"]);
                        callLogDTO.MHAEligibilityDesc = ConvertToString(reader["mha_eligibility_desc"]);
                        callLogDTO.MHAIneligibilityReasonDesc = ConvertToString(reader["mha_inelig_reason_desc"]);

                        callLogDTO.UnemployedInd=ConvertToString(reader["unemployed_ind"]);
                        callLogDTO.UpBenefitsInd=ConvertToString(reader["up_benefits_ind"]);
                        callLogDTO.PreviousUpInd=ConvertToString(reader["previous_up_ind"]);
                        callLogDTO.FCSaleDate=ConvertToDateTime(reader["fc_sale_dt"]);

                        results.Add(callLogDTO);
                        if (results.Count == 100) break; //Only get first 100 records
                        #endregion
                    }
                    reader.Close();
                }
                dbConnection.Close();
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

        public CallLogWSReturnDTOCollection ICTSearchCallLog(CallLogSearchCriteriaDTO searchCriteria)
        {
            CallLogWSReturnDTOCollection results = new CallLogWSReturnDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_call_search", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@pi_fname", searchCriteria.FirstName);
            sqlParam[1] = new SqlParameter("@pi_lname", searchCriteria.LastName);
            sqlParam[2] = new SqlParameter("@pi_loan_acct_num", searchCriteria.LoanNumber);
            //</Parameter>
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
                        #region set value
                        CallLogWSReturnDTO callLogDTO = new CallLogWSReturnDTO();
                        callLogDTO.HopeNetCallId = "HPF" + ConvertToInt(reader["call_id"]);
                        //callLogDTO.CcAgentIdKey = ConvertToString(reader["cc_agent_id_key"]);
                        callLogDTO.StartDate = ConvertToDateTime(reader["start_dt"]);
                        callLogDTO.EndDate = ConvertToDateTime(reader["end_dt"]);
                        //callLogDTO.DNIS = ConvertToString(reader["dnis"]);
                        //callLogDTO.CallCenter = ConvertToString(reader["call_center_name"]);
                        callLogDTO.CallSourceCd = ConvertToString(reader["call_source_cd"]);
                        callLogDTO.ReasonForCall = ConvertToString(reader["reason_for_call"]);
                        callLogDTO.LoanAccountNumber = ConvertToString(reader["loan_acct_num"]);
                        callLogDTO.FirstName = ConvertToString(reader["fname"]);
                        callLogDTO.LastName = ConvertToString(reader["lname"]);

                        callLogDTO.MotherMaidenLastName = ConvertToString(reader["mother_maiden_lname"]);

                        callLogDTO.ServicerId = ConvertToInt(reader["servicer_id"]);
                        callLogDTO.OtherServicerName = ConvertToString(reader["other_servicer_name"]);
                        callLogDTO.PropZipFull9 = ConvertToString(reader["prop_zip_full9"]);
                        //callLogDTO.PrevAgencyId = ConvertToInt(reader["prev_agency_id"]);
                        //callLogDTO.SelectedAgencyId = ConvertToInt(reader["selected_agency_id"]);
                        //callLogDTO.ScreenRout = ConvertToString(reader["screen_rout"]);
                        callLogDTO.FinalDispoCd = ConvertToString(reader["final_dispo_cd"]);
                        //callLogDTO.TransNumber = ConvertToString(reader["trans_num"]);
                        //callLogDTO.CreateDate = ConvertToDateTime(reader["create_dt"]);
                        //callLogDTO.CreateUserId = ConvertToString(reader["create_user_id"]);
                        //callLogDTO.CreateAppName = ConvertToString(reader["create_app_name"]);
                        //callLogDTO.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                        //callLogDTO.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                        //callLogDTO.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                        //callLogDTO.CcCallKey = ConvertToString(reader["cc_call_key"]);
                        callLogDTO.LoanDelinqStatusCd = ConvertToString(reader["loan_delinq_status_cd"]);
                        //callLogDTO.SelectedCounselor = ConvertToString(reader["selected_counselor"]);
                        callLogDTO.HomeownerInd = ConvertToString(reader["homeowner_ind"]);
                        callLogDTO.PowerOfAttorneyInd = ConvertToString(reader["power_of_attorney_ind"]);
                        callLogDTO.AuthorizedInd = ConvertToString(reader["authorized_ind"]);
                        callLogDTO.City = ConvertToString(reader["city"]);
                        callLogDTO.State = ConvertToString(reader["state"]);
                        callLogDTO.NonprofitReferralKeyNum1 = ConvertToString(reader["nonprofitreferral_key_num1"]);
                        callLogDTO.NonprofitReferralKeyNum2 = ConvertToString(reader["nonprofitreferral_key_num2"]);
                        callLogDTO.NonprofitReferralKeyNum3 = ConvertToString(reader["nonprofitreferral_key_num3"]);

                        callLogDTO.DelinqInd = ConvertToString(reader["delinq_ind"]);
                        callLogDTO.PropStreetAddress = ConvertToString(reader["prop_street_addr"]);
                        callLogDTO.PrimaryResidenceInd = ConvertToString(reader["prim_res_ind"]);
                        callLogDTO.MaxLoanAmountInd = ConvertToString(reader["max_loan_amt_ind"]);
                        callLogDTO.CustomerPhone = ConvertToString(reader["cust_phone"]);
                        callLogDTO.LoanLookupCd = ConvertToString(reader["loan_lookup_cd"]);
                        callLogDTO.OriginatedPrior2009Ind = ConvertToString(reader["orig_prior2009_ind"]);
                        callLogDTO.PaymentAmount = ConvertToDouble(reader["payment_amt"]);
                        callLogDTO.GrossIncomeAmount = ConvertToDouble(reader["gross_inc_amt"]);
                        callLogDTO.DTIInd = ConvertToString(reader["dti_ind"]);
                        callLogDTO.ServicerCANumber = ConvertToInt(reader["servicer_ca_num"]);
                        callLogDTO.ServicerCALastContactDate = ConvertToDateTime(reader["servicer_ca_last_contact_dt"]);
                        callLogDTO.ServicerCAId = ConvertToInt(reader["servicer_ca_id"]);
                        callLogDTO.ServicerCAOtherName = ConvertToString(reader["servicer_ca_other_name"]);
                        callLogDTO.MHAInfoShareInd = ConvertToString(reader["mha_info_share_ind"]);
                        callLogDTO.ICTCallId = ConvertToString(reader["ict_call_id"]);
                        callLogDTO.MHAEligibilityCd = ConvertToString(reader["mha_eligibility_cd"]);

                        callLogDTO.MHAIneligibilityReasonCd = ConvertToString(reader["mha_inelig_reason_cd"]);
                        callLogDTO.ServicerName = ConvertToString(reader["servicer_name"]);
                        callLogDTO.FinalDispoDesc = ConvertToString(reader["final_dispo_desc"]);
                        callLogDTO.NonprofitReferralDesc1 = ConvertToString(reader["nonprofitreferral_desc1"]);
                        callLogDTO.NonprofitReferralDesc2 = ConvertToString(reader["nonprofitreferral_desc2"]);
                        callLogDTO.NonprofitReferralDesc3 = ConvertToString(reader["nonprofitreferral_desc3"]);
                        callLogDTO.ServicerCAName = ConvertToString(reader["servicer_ca_name"]);
                        callLogDTO.MHAEligibilityDesc = ConvertToString(reader["mha_eligibility_desc"]);
                        callLogDTO.MHAIneligibilityReasonDesc = ConvertToString(reader["mha_inelig_reason_desc"]);

                        callLogDTO.UnemployedInd=ConvertToString(reader["unemployed_ind"]);
                        callLogDTO.UpBenefitsInd=ConvertToString(reader["up_benefits_ind"]);
                        callLogDTO.PreviousUpInd=ConvertToString(reader["previous_up_ind"]);
                        callLogDTO.FCSaleDate=ConvertToDateTime(reader["fc_sale_dt"]);

                        results.Add(callLogDTO);
                        if (results.Count == 100) break; //Only get first 100 records
                        #endregion
                    }
                    reader.Close();
                }
                dbConnection.Close();
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
    }
}
