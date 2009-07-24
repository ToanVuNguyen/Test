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
    public class AdminTaskLogDAO: BaseDAO
    {
        private static readonly AdminTaskLogDAO instance = new AdminTaskLogDAO();

        public static AdminTaskLogDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected AdminTaskLogDAO()
        {
            
        }

        public int InsertAdminTaskLog(AdminTaskLogDTO data)
        {
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_admin_task_log_insert", dbConnection);
            
            try
            {
                dbConnection.Open();
                var sqlParam = new SqlParameter[7];
                sqlParam[0] = new SqlParameter("@pi_task_name", data.TaskName);
                sqlParam[1] = new SqlParameter("@pi_record_count", data.RecordCount);
                sqlParam[2] = new SqlParameter("@pi_fc_id_list", data.FcIdList);
                sqlParam[3] = new SqlParameter("@pi_task_notes", data.TaskNotes);
                sqlParam[4] = new SqlParameter("@pi_create_user_id", data.CreateUserId);
                sqlParam[5] = new SqlParameter("@pi_create_app", data.CreateAppName);
                sqlParam[6] = new SqlParameter("@po_admin_task_log_id", SqlDbType.Int) { Direction = ParameterDirection.Output };

                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();

                return ConvertToInt(sqlParam[6].Value).Value;
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
