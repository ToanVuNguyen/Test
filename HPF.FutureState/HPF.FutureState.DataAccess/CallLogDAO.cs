using System;
using System.Data;
using System.Data.SqlClient;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

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
        public int InsertCallLog(CallLogDTO aCallLog)
        {            
            //var dbConnection = CreateConnection();

            //var command = CreateSPCommand("hpf_call_insert", dbConnection);
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_call_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[34];
            sqlParam[0] = new SqlParameter("@ext_call_num", aCallLog.ExtCallNumber);
            sqlParam[1] = new SqlParameter("@agency_id", aCallLog.AgencyId);
            sqlParam[2] = new SqlParameter("@start_dt", aCallLog.StartDate);
            sqlParam[3] = new SqlParameter("@end_dt", aCallLog.EndDate);
            sqlParam[4] = new SqlParameter("@dnis", aCallLog.DNIS);
            sqlParam[5] = new SqlParameter("@call_center", aCallLog.CallCenter);
            sqlParam[6] = new SqlParameter("@call_center_cd",  aCallLog.CallCenterCD);
            sqlParam[7] = new SqlParameter("@call_resource", aCallLog.CallResource);
            sqlParam[8] = new SqlParameter("@reason_for_call", aCallLog.ReasonToCall);
            sqlParam[9] = new SqlParameter("@acct_num", aCallLog.AccountNumber);
            sqlParam[10] = new SqlParameter("@fname", aCallLog.FirstName);
            sqlParam[11] = new SqlParameter("@lname", aCallLog.LastName);
            sqlParam[12] = new SqlParameter("@counsel_past_YR_ind",  aCallLog.CounselPastYRInd);
            sqlParam[13] = new SqlParameter("@mtg_prob_ind",  aCallLog.MtgProbInd);
            sqlParam[14] = new SqlParameter("@past_due_ind", aCallLog.PastDueInd);
            sqlParam[15] = new SqlParameter("@past_due_soon_ind",  aCallLog.PastDueSoonInd);
            sqlParam[16] = new SqlParameter("@past_due_months", aCallLog.PastDueMonths);
            sqlParam[17] = new SqlParameter("@servicer_id", aCallLog.ServicerId);
            sqlParam[18] = new SqlParameter("@other_servicer_name",  aCallLog.OtherServicerName);
            sqlParam[19] = new SqlParameter("@prop_zip", aCallLog.PropZip);
            sqlParam[20] = new SqlParameter("@prev_counselor_id", aCallLog.PrevCounselorId);
            sqlParam[21] = new SqlParameter("@prev_agency_id", aCallLog.PrevAgencyId);
            sqlParam[22] = new SqlParameter("@selected_agency_id", aCallLog.SelectedAgencyId);
            sqlParam[23] = new SqlParameter("@screen_rout", aCallLog.ScreenRout);
            sqlParam[24] = new SqlParameter("@final_dispo", aCallLog.FinalDispo);
            sqlParam[25] = new SqlParameter("@trans_num", aCallLog.TransNumber);
            sqlParam[26] = new SqlParameter("@out_of_network_referral_TBD", aCallLog.OutOfNetworkReferralTBD);
            sqlParam[27] = new SqlParameter("@create_dt", aCallLog.CreateDate);
            sqlParam[28] = new SqlParameter("@create_user_id", aCallLog.CreateUserId);
            sqlParam[29] = new SqlParameter("@create_app_name", aCallLog.CreateAppName);
            sqlParam[30] = new SqlParameter("@chg_lst_dt", aCallLog.ChangeLastDate);
            sqlParam[31] = new SqlParameter("@chg_lst_user_id", aCallLog.ChangeLastUserId);
            sqlParam[32] = new SqlParameter("@chg_lst_app_name", aCallLog.ChangeLastAppName);
            sqlParam[33] = new SqlParameter("@call_id", SqlDbType.Int) {Direction = ParameterDirection.Output};
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
                aCallLog.CallId = ConvertToInt(sqlParam[33].Value);
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
            var sqlParam = new SqlParameter[34];
            sqlParam[0] = new SqlParameter("@call_id", aCallLog.CallId);
            sqlParam[1] = new SqlParameter("@ext_call_num", aCallLog.ExtCallNumber);
            sqlParam[2] = new SqlParameter("@agency_id", aCallLog.AgencyId);
            sqlParam[3] = new SqlParameter("@start_dt", aCallLog.StartDate);
            sqlParam[4] = new SqlParameter("@end_dt", aCallLog.EndDate);
            sqlParam[5] = new SqlParameter("@dnis", aCallLog.DNIS);
            sqlParam[6] = new SqlParameter("@call_center", aCallLog.CallCenter);
            sqlParam[7] = new SqlParameter("@call_center_cd", aCallLog.CallCenterCD);
            sqlParam[8] = new SqlParameter("@call_resource", aCallLog.CallResource);
            sqlParam[9] = new SqlParameter("@reason_for_call", aCallLog.ReasonToCall);
            sqlParam[10] = new SqlParameter("@acct_num", aCallLog.AccountNumber);
            sqlParam[11] = new SqlParameter("@fname", aCallLog.FirstName);
            sqlParam[12] = new SqlParameter("@lname", aCallLog.LastName);
            sqlParam[13] = new SqlParameter("@counsel_past_YR_ind", aCallLog.CounselPastYRInd);
            sqlParam[14] = new SqlParameter("@mtg_prob_ind", aCallLog.MtgProbInd);
            sqlParam[15] = new SqlParameter("@past_due_ind", aCallLog.PastDueInd);
            sqlParam[16] = new SqlParameter("@past_due_soon_ind", aCallLog.PastDueSoonInd);
            sqlParam[17] = new SqlParameter("@past_due_months", aCallLog.PastDueMonths);
            sqlParam[18] = new SqlParameter("@servicer_id", aCallLog.ServicerId);
            sqlParam[19] = new SqlParameter("@other_servicer_name", aCallLog.OtherServicerName);
            sqlParam[20] = new SqlParameter("@prop_zip", aCallLog.PropZip);
            sqlParam[21] = new SqlParameter("@prev_counselor_id", aCallLog.PrevCounselorId);
            sqlParam[22] = new SqlParameter("@prev_agency_id", aCallLog.PrevAgencyId);
            sqlParam[23] = new SqlParameter("@selected_agency_id", aCallLog.SelectedAgencyId);
            sqlParam[24] = new SqlParameter("@screen_rout", aCallLog.ScreenRout);
            sqlParam[25] = new SqlParameter("@final_dispo", aCallLog.FinalDispo);
            sqlParam[26] = new SqlParameter("@trans_num", aCallLog.TransNumber);
            sqlParam[27] = new SqlParameter("@out_of_network_referral_TBD", aCallLog.OutOfNetworkReferralTBD);
            sqlParam[28] = new SqlParameter("@create_dt", aCallLog.CreateDate);
            sqlParam[29] = new SqlParameter("@create_user_id", aCallLog.CreateUserId);
            sqlParam[30] = new SqlParameter("@create_app_name", aCallLog.CreateAppName);
            sqlParam[31] = new SqlParameter("@chg_lst_dt", aCallLog.ChangeLastDate);
            sqlParam[32] = new SqlParameter("@chg_lst_user_id", aCallLog.ChangeLastUserId);
            sqlParam[33] = new SqlParameter("@chg_lst_app_name", aCallLog.ChangeLastAppName);
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
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_call_load", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@call_id", callLogId);
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
                        callLogDTO.CallId = ConvertToInt(reader["call_id"]);
                        callLogDTO.ExtCallNumber = ConvertToString(reader["ext_call_num"]);
                        callLogDTO.AgencyId = ConvertToString(reader["agency_id"]);
                        callLogDTO.StartDate = ConvertToDateTime(reader["start_dt"]);
                        callLogDTO.EndDate = ConvertToDateTime(reader["end_dt"]);
                        callLogDTO.DNIS = ConvertToString(reader["dnis"]);
                        callLogDTO.CallCenter = ConvertToString(reader["call_center"]);
                        callLogDTO.CallCenterCD = ConvertToString(reader["call_center_cd"]);
                        callLogDTO.CallResource = ConvertToString(reader["call_resource"]);
                        callLogDTO.ReasonToCall = ConvertToString(reader["reason_for_call"]);
                        callLogDTO.AccountNumber = ConvertToString(reader["acct_num"]);
                        callLogDTO.FirstName = ConvertToString(reader["fname"]);
                        callLogDTO.LastName = ConvertToString(reader["lname"]);
                        callLogDTO.CounselPastYRInd = ConvertToString(reader["counsel_past_YR_ind"]);
                        callLogDTO.MtgProbInd = ConvertToString(reader["mtg_prob_ind"]);
                        callLogDTO.PastDueInd = ConvertToString(reader["past_due_ind"]);
                        callLogDTO.PastDueSoonInd = ConvertToString(reader["past_due_soon_ind"]);
                        callLogDTO.PastDueMonths = ConvertToInt(reader["past_due_months"]);
                        callLogDTO.ServicerId = ConvertToInt(reader["servicer_id"]);
                        callLogDTO.OtherServicerName = ConvertToString(reader["other_servicer_name"]);
                        callLogDTO.PropZip = ConvertToString(reader["prop_zip"]);
                        callLogDTO.PrevCounselorId = ConvertToInt(reader["prev_counselor_id"]);
                        callLogDTO.PrevAgencyId = ConvertToInt(reader["prev_agency_id"]);
                        callLogDTO.SelectedAgencyId = ConvertToString(reader["selected_agency_id"]);
                        callLogDTO.ScreenRout = ConvertToString(reader["screen_rout"]);
                        callLogDTO.FinalDispo = ConvertToInt(reader["final_dispo"]);
                        callLogDTO.TransNumber = ConvertToString(reader["trans_num"]);
                        callLogDTO.OutOfNetworkReferralTBD = ConvertToString(reader["out_of_network_referral_TBD"]);
                        callLogDTO.CreateDate = ConvertToDateTime(reader["create_dt"]);
                        callLogDTO.CreateUserId = ConvertToString(reader["create_user_id"]);
                        callLogDTO.CreateAppName = ConvertToString(reader["create_app_name"]);
                        callLogDTO.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                        callLogDTO.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                        callLogDTO.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                    }
                    reader.Close();
                }
                dbConnection.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }            
            return callLogDTO;
        }       
    }
}
