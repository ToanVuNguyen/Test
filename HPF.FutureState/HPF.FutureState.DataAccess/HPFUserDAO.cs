using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

using System.Collections.Generic;
namespace HPF.FutureState.DataAccess
{
    public class HPFUserDAO : BaseDAO
    {
        private static readonly HPFUserDAO instance = new HPFUserDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static HPFUserDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected HPFUserDAO()
        {
        }

        /// <summary>
        /// Select all HPFUser from database
        /// </summary>
        /// <returns>RefCodeItemDTOCollection</returns>
        public HPFUserDTOCollection GetHpfUsers()
        {
            var results = HPFCacheManager.Instance.GetData<HPFUserDTOCollection>(Constant.HPF_CACHE_HPFUSER);
            if (results == null)
            {
                results = GetHpfUsersFromDatabase();
                HPFCacheManager.Instance.Add(Constant.HPF_CACHE_HPFUSER, results);
            }
            return results;
        }

        public HPFUserDTOCollection GetHpfUsersFromDatabase()
        {
            HPFUserDTOCollection hpfUsers = null;
            var dbConnection = CreateConnection();
            var command = CreateCommand("hpf_hpf_user_get", dbConnection);
            command.Parameters.Add(new SqlParameter("@pi_hpf_user_id", -1));
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    hpfUsers = new HPFUserDTOCollection();
                    while (reader.Read())
                    {
                        hpfUsers.Add(new HPFUserDTO()
                        {
                            HpfUserId = ConvertToInt(reader["hpf_user_id"]),
                            UserLoginId = ConvertToString(reader["user_login_id"]),
                            Password = ConvertToString(reader["password"]),
                            FirstName = ConvertToString(reader["fname"]),
                            LastName = ConvertToString(reader["lname"]),
                            Email = ConvertToString(reader["email"]),
                            ActiveInd = ConvertToString(reader["active_ind"])
                        });
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
            return hpfUsers;
        }
        public UserDTO GetHpfUserById(int hpfUserId)
        {
            UserDTO user = new UserDTO();
            var dbConnection = CreateConnection();
            var command = CreateCommand("hpf_hpf_user_get", dbConnection);
            command.Parameters.Add(new SqlParameter("@pi_hpf_user_id", hpfUserId));
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        user.FirstName = ConvertToString(reader["fname"]);
                        user.LastName = ConvertToString(reader["lname"]);
                        user.UserName = ConvertToString(reader["user_login_id"]);
                        user.UserRole = ConvertToString(reader["user_role_str_TBD"]);
                        user.Phone = ConvertToString(reader["phone"]);
                        user.AgencyId = ConvertToInt(reader["agency_id"]);
                        user.UserType = ConvertToString(reader["user_type"]);
                        user.HPFUserId = ConvertToInt(reader["hpf_user_id"]);
                        user.Password = ConvertToString(reader["password"]);
                        user.IsActivate = ConvertToString(reader["active_ind"])[0];
                        user.Email = ConvertToString(reader["email"]);
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
                command.Dispose();
                dbConnection.Close();
            }
            return user;
        }
        public HPFUserDTOCollection GetHpfUsersByAgencyId(int agencyId)
        {
            HPFUserDTOCollection hpfUsers = new HPFUserDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateCommand("hpf_hpf_user_get_by_agency_id", dbConnection);
            command.Parameters.Add(new SqlParameter("@pi_agency_id", agencyId));
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        hpfUsers.Add(new HPFUserDTO()
                        {
                            FirstName = ConvertToString(reader["fname"]),
                            LastName = ConvertToString(reader["lname"]),
                            Email = ConvertToString(reader["email"])
                        });
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
                command.Dispose();
                dbConnection.Close();
            }
            return hpfUsers;
        }
        /// <summary>
        /// Update hpfUser DTO
        /// </summary>
        /// <param name="webUser">HPFUserDTO</param>
        public void UpdateHpfUser(HPFUserDTO hpfUser)
        {
            var dbConnection = CreateConnection();
            //Add store here
            var command = CreateSPCommand("hpf_hpf_user_update", dbConnection);
            SqlTransaction trans = null;
            //<Parameter>
            var sqlParam = new SqlParameter[15];
            sqlParam[0] = new SqlParameter("@pi_user_login_id", hpfUser.UserLoginId);
            sqlParam[1] = new SqlParameter("@pi_hpf_user_id", null);
            sqlParam[2] = new SqlParameter("@pi_password", null);
            sqlParam[3] = new SqlParameter("@pi_active_ind", hpfUser.ActiveInd);
            sqlParam[4] = new SqlParameter("@pi_user_role_str_tbd", null);
            sqlParam[5] = new SqlParameter("@pi_fname", null);
            sqlParam[6] = new SqlParameter("@pi_lname", null);
            sqlParam[7] = new SqlParameter("@pi_email", null);
            sqlParam[8] = new SqlParameter("@pi_phone", null);
            sqlParam[9] = new SqlParameter("@pi_user_type", null);
            sqlParam[11] = new SqlParameter("@pi_agency_id", null);
            sqlParam[10] = new SqlParameter("@pi_last_login_dt", null);
            sqlParam[11] = new SqlParameter("@pi_agency_id", null);
            sqlParam[12] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(hpfUser.ChangeLastDate));
            sqlParam[13] = new SqlParameter("@pi_chg_lst_user_id", hpfUser.ChangeLastUserId);
            sqlParam[14] = new SqlParameter("@pi_chg_lst_app_name", hpfUser.ChangeLastAppName);

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
                if (trans != null)
                    trans.Rollback();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                command.Dispose();
                dbConnection.Close();
            }
        }
    }
}
