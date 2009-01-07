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
    public class SecurityDAO: BaseDAO
    {
        private static readonly SecurityDAO instance = new SecurityDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static SecurityDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected SecurityDAO()
        {
        }

        #region Implementation of SecurityDAO

        /// <summary>
        /// User login for administrator and billing
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool WebUserLogin(string userName, string password)
        {
            if (userName == "Admin" && password == "")
                return true;
            return false;
        }

        /// <summary>
        /// Add a new Webservice User to the system
        /// </summary>
        /// <param name="aWSUser"></param>
        public void WSAddUser(WSUserDTO aWSUser)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get WS User by username
        /// </summary>
        /// <param name="userName">Username</param>
        /// <returns>WSUserDTO</returns>
        public WSUserDTO GetWSUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get User from username and password provided
        /// </summary>
        /// <param name="userName">username</param>
        /// <param name="password">password</param>        
        /// <returns></returns>
        public WSUserDTO GetWSUser(string userName, string password)
        {
            WSUserDTO wsUser = null;
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_ws_user_get_from_username_password", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@pi_username", userName);
            sqlParam[1] = new SqlParameter("@pi_password", password);            
            //</Parameter>            
            try
            {
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    wsUser = new WSUserDTO();
                    wsUser.LoginUsername = userName;
                    wsUser.LoginPassword = password;
                    while (reader.Read())
                    {
                        wsUser.WsUserId = ConvertToInt(reader["ws_user_id"]);
                        wsUser.AgencyId = ConvertToInt(reader["agency_id"]);
                        wsUser.CallCenterId = ConvertToInt(reader["call_center_id"]);
                        wsUser.ActiveInd = ConvertToString(reader["active_ind"]);
                        wsUser.CreateDt = ConvertToDateTime(reader["create_dt"]);
                        wsUser.CreateUserId = ConvertToString(reader["create_user_id"]);
                        wsUser.ChgLstDt = ConvertToDateTime(reader["chg_lst_dt"]);
                        wsUser.ChgLstUserId = ConvertToString(reader["chg_lst_user_id"]);
                        wsUser.CreateLstAppName = ConvertToString(reader["chg_lst_app_name"]);
                    }
                }
            }
            catch (Exception Ex)
            {
                dbConnection.Close();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return wsUser;
        }
        #endregion
    }
}
