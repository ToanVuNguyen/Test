using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

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
            SqlParameter[] sqlParam = new SqlParameter[37];
            sqlParam[0] = new SqlParameter("@pi_call_center_id", aCallLog.CallCenterID);
            sqlParam[1] = new SqlParameter("@pi_cc_agent_id_key", aCallLog.CcAgentIdKey);            
            sqlParam[2] = new SqlParameter("@pi_start_dt", NullableDateTime(aCallLog.StartDate));
            sqlParam[3] = new SqlParameter("@pi_end_dt", NullableDateTime(aCallLog.EndDate));
            sqlParam[4] = new SqlParameter("@pi_dnis", aCallLog.DNIS);
            sqlParam[5] = new SqlParameter("@pi_call_center", aCallLog.CallCenter);
            sqlParam[6] = new SqlParameter("@pi_call_source_cd", aCallLog.CallSourceCd);
            sqlParam[7] = new SqlParameter("@pi_reason_for_call", aCallLog.ReasonToCall);
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
            
            sqlParam[36] = new SqlParameter("@po_call_id", SqlDbType.Int) {Direction = ParameterDirection.Output};
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
            catch(Exception Ex)
            {
                if (trans != null) trans.Rollback();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return aCallLog.CallId;
        }

        /// <summary>
        /// Update a CallLog in Database
        /// </summary>
        /// <param name="aCallLog"></param>
        public void UpdateCallLog(CallLogDTO aCallLog)
        {
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("USPCallUpdate", dbConnection);
            //<Parameter>

            var sqlParam = new SqlParameter[32];
            sqlParam[0] = new SqlParameter("@call_center_id", aCallLog.CallCenterID);
            sqlParam[1] = new SqlParameter("@cc_agent_id_key", aCallLog.CcAgentIdKey);
            sqlParam[2] = new SqlParameter("@start_dt", aCallLog.StartDate);
            sqlParam[3] = new SqlParameter("end_dt", aCallLog.EndDate);
            sqlParam[4] = new SqlParameter("dnis", aCallLog.DNIS);
            sqlParam[5] = new SqlParameter("call_center", aCallLog.CallCenter);
            sqlParam[6] = new SqlParameter("@call_source_cd", aCallLog.CallSourceCd);
            sqlParam[7] = new SqlParameter("@reason_for_call", aCallLog.ReasonToCall);
            sqlParam[8] = new SqlParameter("@loan_acct_num", aCallLog.LoanAccountNumber);
            sqlParam[9] = new SqlParameter("@fname", aCallLog.FirstName);
            sqlParam[10] = new SqlParameter("@lname", aCallLog.LastName);
            sqlParam[11] = new SqlParameter("@servicer_id", aCallLog.ServicerId);
            sqlParam[12] = new SqlParameter("@other_servicer_name", aCallLog.OtherServicerName);
            sqlParam[13] = new SqlParameter("@prop_zip_full9", aCallLog.PropZipFull9);
            sqlParam[14] = new SqlParameter("@prev_agency_id", aCallLog.PrevAgencyId);
            sqlParam[15] = new SqlParameter("@selected_agency_id", aCallLog.SelectedAgencyId);
            sqlParam[16] = new SqlParameter("@screen_rout", aCallLog.ScreenRout);
            sqlParam[17] = new SqlParameter("@final_dispo_cd", aCallLog.FinalDispoCd);
            sqlParam[18] = new SqlParameter("@trans_num", aCallLog.TransNumber);
            sqlParam[19] = new SqlParameter("@cc_call_key", aCallLog.CcCallKey);
            sqlParam[20] = new SqlParameter("@loan_delinq_status_cd", aCallLog.LoanDelinqStatusCd);
            sqlParam[21] = new SqlParameter("@selected_counselor", aCallLog.SelectedCounselor);
            sqlParam[22] = new SqlParameter("@homeowner_ind", aCallLog.HomeownerInd);
            sqlParam[23] = new SqlParameter("@power_of_attorney_ind", aCallLog.PowerOfAttorneyInd);
            sqlParam[24] = new SqlParameter("@authorized_ind", aCallLog.AuthorizedInd);
            sqlParam[25] = new SqlParameter("create_dt", aCallLog.CreateDate);
            sqlParam[26] = new SqlParameter("create_user_id", aCallLog.CreateUserId);
            sqlParam[27] = new SqlParameter("create_app_name", aCallLog.CreateAppName);
            sqlParam[28] = new SqlParameter("chg_lst_dt", aCallLog.ChangeLastDate);
            sqlParam[29] = new SqlParameter("chg_lst_user_id", aCallLog.ChangeLastUserId);
            sqlParam[30] = new SqlParameter("chg_lst_app_name", aCallLog.ChangeLastAppName);

            sqlParam[31] = new SqlParameter("@call_id", aCallLog.CallId);            

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
            catch(Exception Ex)
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
        /// Read a CallLog in Database by CallLogId
        /// </summary>
        /// <param name="callLogId">CallLogId</param>
        /// <returns>CallLogDTO</returns>
        public CallLogDTO ReadCallLog(int callLogId)
        {
            CallLogDTO callLogDTO = null;
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_call_get", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_call_id", callLogId);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    callLogDTO = new CallLogDTO();
                    while (reader.Read())
                    {
                        #region set value

                        callLogDTO.CallId = ConvertToInt(reader["call_id"]);
                        callLogDTO.CcAgentIdKey = ConvertToString(reader["cc_agent_id_key"]);
                        callLogDTO.StartDate = ConvertToDateTime(reader["start_dt"]);
                        callLogDTO.EndDate = ConvertToDateTime(reader["end_dt"]);
                        callLogDTO.DNIS = ConvertToString(reader["dnis"]);
                        callLogDTO.CallCenter = ConvertToString(reader["call_center"]);
                        callLogDTO.CallSourceCd = ConvertToString(reader["call_source_cd"]);
                        callLogDTO.ReasonToCall = ConvertToString(reader["reason_for_call"]);
                        callLogDTO.LoanAccountNumber = ConvertToString(reader["loan_acct_num"]);
                        callLogDTO.FirstName = ConvertToString(reader["fname"]);
                        callLogDTO.LastName = ConvertToString(reader["lname"]);                                                
                        callLogDTO.ServicerId = ConvertToInt(reader["servicer_id"]);
                        callLogDTO.OtherServicerName = ConvertToString(reader["other_servicer_name"]);
                        callLogDTO.PropZipFull9 = ConvertToString(reader["prop_zip_full9"]);                        
                        callLogDTO.PrevAgencyId = ConvertToInt(reader["prev_agency_id"]);
                        callLogDTO.SelectedAgencyId = ConvertToInt(reader["selected_agency_id"]);
                        callLogDTO.ScreenRout = ConvertToString(reader["screen_rout"]);
                        callLogDTO.FinalDispoCd = ConvertToString(reader["final_dispo_cd"]);
                        callLogDTO.TransNumber = ConvertToString(reader["trans_num"]);                        
                        callLogDTO.CreateDate = ConvertToDateTime(reader["create_dt"]);
                        callLogDTO.CreateUserId = ConvertToString(reader["create_user_id"]);
                        callLogDTO.CreateAppName = ConvertToString(reader["create_app_name"]);
                        callLogDTO.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                        callLogDTO.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                        callLogDTO.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                        callLogDTO.CcCallKey = ConvertToString(reader["cc_call_key"]);
                        callLogDTO.LoanDelinqStatusCd = ConvertToString(reader["loan_delinq_status_cd"]);
                        callLogDTO.SelectedCounselor = ConvertToString(reader["selected_counselor"]);
                        callLogDTO.HomeownerInd = ConvertToString(reader["homeowner_ind"]);
                        callLogDTO.PowerOfAttorneyInd = ConvertToString(reader["power_of_attorney_ind"]);
                        callLogDTO.AuthorizedInd = ConvertToString(reader["authorized_ind"]);
                        callLogDTO.City = ConvertToString(reader["city"]);
                        callLogDTO.State = ConvertToString(reader["state"]);
                        callLogDTO.NonprofitReferralKeyNum1 = ConvertToString(reader["nonprofitreferral_key_num1"]); 
                        callLogDTO.NonprofitReferralKeyNum2 = ConvertToString(reader["nonprofitreferral_key_num2"]); 
                        callLogDTO.NonprofitReferralKeyNum3 = ConvertToString(reader["nonprofitreferral_key_num3"]); 
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
            return callLogDTO;
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

        public ServicerDTO GetServicer(CallLogDTO aCallLog)
        {
            ServicerDTO servicer = null;
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_servicer_get", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_servicer_id", aCallLog.ServicerId);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    
                    int? servicerId = 0;
                    while (reader.Read())
                    {
                        servicerId = ConvertToInt(reader["servicer_id"]);
                        if (servicerId.HasValue && servicerId.Value == aCallLog.ServicerId)
                        {
                            servicer = new ServicerDTO();
                            servicer.ServicerID = aCallLog.ServicerId;
                            servicer.ServicerName = ConvertToString(reader["servicer_name"]);
                            break;
                        }
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
            return servicer;

        }
        
    }
}
