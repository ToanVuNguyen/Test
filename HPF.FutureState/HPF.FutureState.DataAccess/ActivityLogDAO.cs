using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    public class ActivityLogDAO : BaseDAO
    {
        private static readonly ActivityLogDAO instance = new ActivityLogDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static ActivityLogDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected ActivityLogDAO()
        {
        }

        public void InsertActivityLog(ActivityLogDTO activityLog)
        {
            var dbConnection=CreateConnection();
            var command = CreateSPCommand("hpf_activity_log_insert",dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[10];
            sqlParam[0] = new SqlParameter("@pi_fc_id", activityLog.FcId);
            sqlParam[1] = new SqlParameter("@pi_activity_cd", activityLog.ActivityCd);
            sqlParam[2] = new SqlParameter("@pi_activity_dt", activityLog.ActivityDt);
            sqlParam[3] = new SqlParameter("@pi_activity_note", activityLog.ActivityNote);
            sqlParam[4] = new SqlParameter("@pi_create_dt", NullableDateTime(activityLog.CreateDate));
            sqlParam[5] = new SqlParameter("@pi_create_user_id", activityLog.CreateUserId);
            sqlParam[6] = new SqlParameter("@pi_create_app_name", activityLog.CreateAppName);
            sqlParam[7] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(activityLog.ChangeLastDate));
            sqlParam[8] = new SqlParameter("@pi_chg_lst_user_id", activityLog.ChangeLastUserId);
            sqlParam[9] = new SqlParameter("@pi_chg_lst_app_name", activityLog.ChangeLastAppName);
            
            command.Parameters.AddRange(sqlParam);
            //</Parameter>
            try
            {
                dbConnection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
            dbConnection.Close();
            }
        }

        public ActivityLogDTOCollection GetActivityLog(int fcId)
        {
            ActivityLogDTOCollection result = new ActivityLogDTOCollection();
            SqlConnection dbConnection = CreateConnection();
            SqlCommand command = CreateSPCommand("hpf_activity_log_get", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);            
            command.Parameters.AddRange(sqlParam);
            //</Parameter>
            try
            {
                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ActivityLogDTO activity = new ActivityLogDTO();
                    activity.ActivityCd = ConvertToString(reader["activity_cd"]);
                    activity.ActivityCdDesc = ConvertToString(reader["activity_cd_desc"]);
                    activity.ActivityDt = ConvertToDateTime(reader["activity_dt"]);
                    activity.ActivityNote = ConvertToString(reader["activity_note"]);
                    activity.CreateDate = ConvertToDateTime(reader["create_dt"]);
                    activity.CreateUserId = ConvertToString(reader["create_user_id"]);
                    activity.FcId = fcId;
                    activity.ActivityLogId = ConvertToInt(reader["activity_log_id"]);
                    activity.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                    activity.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                    activity.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                    activity.CreateAppName = ConvertToString(reader["create_app_name"]);
                    result.Add(activity);
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

        }
    }

