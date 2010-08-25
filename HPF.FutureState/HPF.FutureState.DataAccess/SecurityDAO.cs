using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;

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
            //Get user and compare with the password.

            UserDTO user = GetWebUser(userName);
            if (user == null)
                return false;
            if (user.UserName.ToLower() == userName.ToLower() && user.Password == password)
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
            var wsUser = HPFCacheManager.Instance.GetData<WSUserDTO>(Constant.HPF_CACHE_WS_USER + userName.ToUpper());
            if (wsUser == null)
            {
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

                        HPFCacheManager.Instance.Add(Constant.HPF_CACHE_WS_USER + userName.ToUpper(), wsUser);
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
            }
            else if (password != wsUser.LoginPassword)
                wsUser = null;

            return wsUser;
        }
        /// <summary>
        /// get Web user from table CCRC_User
        /// </summary>
        /// <param name="userName">Username to login</param>
        /// <returns>UserDTO object if user exists in database, null for non-exists</returns>
        public UserDTO GetWebUser(string userName)
        {
            UserDTO webUser = null;
            var dbConnection = CreateConnection();
            //Add store here
            var command = CreateSPCommand("hpf_hpf_user_get_from_login", dbConnection);

            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_username", userName);
            //</Parameter>            
            try
            {
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    webUser = new UserDTO();
                    while (reader.Read())
                    {
                        webUser.HPFUserId = ConvertToInt(reader["hpf_user_id"]);
                        webUser.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                        webUser.Password = ConvertToString(reader["password"]);
                        if (webUser.Password == null)
                            webUser.Password = "";
                        webUser.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                        webUser.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                        webUser.CreateAppName = ConvertToString(reader["create_app_name"]);
                        webUser.CreateDate = ConvertToDateTime(reader["create_dt"]);
                        webUser.CreateUserId = ConvertToString(reader["create_user_id"]);
                        webUser.Email = ConvertToString(reader["email"]);
                        webUser.FirstName = ConvertToString(reader["fname"]);
                        webUser.IsActivate = ConvertToString(reader["active_ind"])[0];
                        webUser.LastLogin = ConvertToDateTime(reader["last_login_dt"]);
                        webUser.LastName = ConvertToString(reader["lname"]);
                        webUser.Phone = ConvertToString(reader["phone"]);
                        webUser.UserName = ConvertToString(reader["user_login_id"]);
                        webUser.UserType = ConvertToString(reader["user_type"]);
                        webUser.AgencyId = ConvertToInt(reader["agency_id"]);
                    }
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

            return webUser;
        }
        /// <summary>
        /// Update user DTO
        /// </summary>
        /// <param name="webUser">UserDTO</param>
        public void UpdateWebUser(UserDTO webUser)
        {
            var dbConnection = CreateConnection();
            //Add store here
            var command = CreateSPCommand("hpf_hpf_user_update", dbConnection);
            SqlTransaction trans = null;
            //<Parameter>
            var sqlParam = new SqlParameter[15];
            sqlParam[0] = new SqlParameter("@pi_user_login_id",webUser.UserName);
            sqlParam[1] = new SqlParameter("@pi_password",webUser.Password);
            sqlParam[2] = new SqlParameter("@pi_active_ind",webUser.IsActivate);
            sqlParam[3] = new SqlParameter("@pi_user_role_str_tbd", webUser.UserRole);
            sqlParam[4] = new SqlParameter("@pi_fname",webUser.FirstName); 
            sqlParam[5] = new SqlParameter("@pi_lname",webUser.LastName);
            sqlParam[6] = new SqlParameter("@pi_email" ,webUser.Email);
            sqlParam[7] = new SqlParameter("@pi_phone" ,webUser.Phone);
            sqlParam[8] = new SqlParameter("@pi_last_login_dt",NullableDateTime(webUser.LastLogin));
            sqlParam[9] = new SqlParameter("@pi_create_dt",NullableDateTime(webUser.CreateDate));
            sqlParam[10] = new SqlParameter("@pi_create_user_id",webUser.CreateUserId); 
            sqlParam[11] = new SqlParameter("@pi_create_app_name",webUser.CreateAppName);
            sqlParam[12] = new SqlParameter("@pi_chg_lst_dt",NullableDateTime(webUser.ChangeLastDate));
            sqlParam[13] = new SqlParameter("@pi_chg_lst_user_id",webUser.ChangeLastUserId); 
            sqlParam[14] = new SqlParameter("@pi_chg_lst_app_name",webUser.ChangeLastAppName);
            
            //</Parameter>            
            try
            {
                dbConnection.Open();
                command.Parameters.AddRange(sqlParam);
                trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                command.Transaction = trans;
                command.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception Ex)
            {
                if(trans!=null)
                    trans.Rollback();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }
        
        public CallCenterDTO GetCallCenter(int callCenterId)
        {
            CallCenterDTO callCenter = new CallCenterDTO();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_call_center_get", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_call_center_id", callCenterId);                
            //</Parameter>            
            try
            {
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    
                    if (reader.Read())
                    {
                        callCenter.CallCenterID = ConvertToInt(reader["call_center_id"]);
                        callCenter.CallCenterName = ConvertToString(reader["call_center_name"]);
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

            return callCenter;
        }
        #endregion
    }
}
