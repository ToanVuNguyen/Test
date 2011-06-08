using System;
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
    public class EventDAO:BaseDAO
    {
        private static readonly EventDAO instance = new EventDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static EventDAO Instance
        {
            get { return instance; }
        }
        protected EventDAO() { }

        /// <summary>
        /// Insert an event to database
        /// </summary>
        /// <param name="anEvent">EventDTO</param>
        /// <returns>a new eventId</returns>
        public int? InsertEvent(EventDTO anEvent)
        {
            SqlConnection dbConnection = CreateConnection();
            SqlCommand command = CreateSPCommand("hpf_event_insert", dbConnection);

            #region parameters
            //<Parameter>
            SqlParameter[] sqlParam = new SqlParameter[16];
            sqlParam[0] = new SqlParameter("@pi_fc_id", anEvent.FcId);
            sqlParam[1] = new SqlParameter("@pi_program_stage_id", anEvent.ProgramStageId);
            sqlParam[2] = new SqlParameter("@pi_event_type_cd", anEvent.EventTypeCd);
            sqlParam[3] = new SqlParameter("@pi_event_dt", NullableDateTime(anEvent.EventDt));
            sqlParam[4] = new SqlParameter("@pi_rpc_ind", anEvent.RpcInd);
            sqlParam[5] = new SqlParameter("@pi_event_outcome_cd",anEvent.EventOutcomeCd);
            sqlParam[6] = new SqlParameter("@pi_completed_ind",anEvent.CompletedInd);
            sqlParam[7] = new SqlParameter("@pi_counselor_id_ref",anEvent.CounselorIdRef);
            sqlParam[8] = new SqlParameter("@pi_program_refusal_dt", NullableDateTime(anEvent.ProgramRefusalDt));

            sqlParam[9] = new SqlParameter("@pi_create_dt", NullableDateTime(anEvent.CreateDate));
            sqlParam[10] = new SqlParameter("@pi_create_user_id", anEvent.CreateUserId);
            sqlParam[11] = new SqlParameter("@pi_create_app_name", anEvent.CreateAppName);
            sqlParam[12] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(anEvent.ChangeLastDate));
            sqlParam[13] = new SqlParameter("@pi_chg_lst_user_id", anEvent.ChangeLastUserId);
            sqlParam[14] = new SqlParameter("@pi_chg_lst_app_name", anEvent.ChangeLastAppName);

            sqlParam[15] = new SqlParameter("@po_event_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
            //</Parameter>
            #endregion
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                command.ExecuteNonQuery();
                anEvent.EventId = ConvertToInt(command.Parameters["@po_event_id"].Value);
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                command.Dispose();
                dbConnection.Close();
            }

            return anEvent.EventId;
        }

        /// <summary>
        /// Update an event to database
        /// </summary>
        /// <param name="anEvent">EventDTO</param>
        /// <returns>an eventId</returns>
        public void UpdateEvent(EventDTO anEvent)
        {
            SqlConnection dbConnection = CreateConnection();
            SqlCommand command = CreateSPCommand("hpf_event_update", dbConnection);

            #region parameters
            //<Parameter>
            SqlParameter[] sqlParam = new SqlParameter[13];
            sqlParam[0] = new SqlParameter("@pi_fc_id", anEvent.FcId);
            sqlParam[1] = new SqlParameter("@pi_event_id", anEvent.EventId);
            sqlParam[2] = new SqlParameter("@pi_program_stage_id", anEvent.ProgramStageId);
            sqlParam[3] = new SqlParameter("@pi_event_type_cd", anEvent.EventTypeCd);
            sqlParam[4] = new SqlParameter("@pi_event_dt", NullableDateTime(anEvent.EventDt));
            sqlParam[5] = new SqlParameter("@pi_rpc_ind", anEvent.RpcInd);
            sqlParam[6] = new SqlParameter("@pi_event_outcome_cd", anEvent.EventOutcomeCd);
            sqlParam[7] = new SqlParameter("@pi_completed_ind", anEvent.CompletedInd);
            sqlParam[8] = new SqlParameter("@pi_counselor_id_ref", anEvent.CounselorIdRef);
            sqlParam[9] = new SqlParameter("@pi_program_refusal_dt", NullableDateTime(anEvent.ProgramRefusalDt));

            
            sqlParam[10] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(anEvent.ChangeLastDate));
            sqlParam[11] = new SqlParameter("@pi_chg_lst_user_id", anEvent.ChangeLastUserId);
            sqlParam[12] = new SqlParameter("@pi_chg_lst_app_name", anEvent.ChangeLastAppName);

            //</Parameter>
            #endregion
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                command.Dispose();
                dbConnection.Close();
            }
           
        }

        public ProgramStageDTO GetProgramStage(int? programStageId)
        {
            ProgramStageDTO returnObject = null;
            SqlConnection dbConnection = base.CreateConnection();
            try
            {
                SqlCommand command = base.CreateCommand("hpf_program_stage_get", dbConnection);
                //<Parameter>
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@pi_program_stage_id", programStageId);

                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;

                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        #region set ProgramStage value
                        returnObject = new ProgramStageDTO();
                        returnObject.ProgramStageId = ConvertToInt(reader["program_stage_id"]);
                        returnObject.ProgramId = ConvertToInt(reader["program_id"]);
                        returnObject.StageName = ConvertToString(reader["stage_name"]);
                        returnObject.StageComment = ConvertToString(reader["stage_comment"]);
                        returnObject.StageDesc = ConvertToString(reader["stage_desc"]);
                        returnObject.StartDt = ConvertToDateTime(reader["start_dt"]);
                        returnObject.EndDt = ConvertToDateTime(reader["end_dt"]);
                        #endregion
                    }
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
            return returnObject;
        }

        public EventDTO GetEvent(int? eventId)
        {
            EventDTO returnObject = null;
            SqlConnection dbConnection = base.CreateConnection();
            try
            {
                SqlCommand command = base.CreateCommand("hpf_event_get", dbConnection);
                //<Parameter>
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@pi_event_id", eventId);

                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;

                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        #region set Event value
                        returnObject = new EventDTO();
                        returnObject.FcId = ConvertToInt(reader["fc_id"]);
                        returnObject.ProgramStageId = ConvertToInt(reader["program_stage_id"]);
                        returnObject.EventTypeCd = ConvertToString(reader["event_type_cd"]);
                        returnObject.EventDt = ConvertToDateTime(reader["event_dt"]);
                        returnObject.RpcInd = ConvertToString(reader["rpc_ind"]);
                        returnObject.EventOutcomeCd = ConvertToString(reader["event_outcome_cd"]);
                        returnObject.CompletedInd = ConvertToString(reader["completed_ind"]);
                        returnObject.CounselorIdRef = ConvertToString(reader["counselor_id_ref"]);
                        returnObject.ProgramRefusalDt = ConvertToDateTime(reader["program_refusal_dt"]);
                        
                        #endregion
                    }
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
            return returnObject;
        }

        /// <summary>
        /// Return 0 if it does not exist event with fcId and eventDt
        /// else return eventId
        /// </summary>
        /// <param name="fcId"></param>
        /// <param name="eventDt"></param>
        /// <returns></returns>
        public int? CheckExistingFcIdAndEventDt(int? fcId, DateTime? eventDt)
        {
            int? eventId = 0;
            var dbConnection = CreateConnection();
            try
            {
                SqlCommand command = base.CreateCommand("hpf_event_get_from_fcId_and_eventDt", dbConnection);
                //<Parameter>
                var sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                sqlParam[1] = new SqlParameter("@pi_event_dt", NullableDateTime(eventDt));
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                        eventId = ConvertToInt(reader["event_id"]);
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
            return eventId;
        }
    }
}
