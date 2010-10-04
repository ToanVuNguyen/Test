using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using System.Data.SqlClient;
using System.Data;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    public class MenuSecurityDAO : BaseDAO
    {
        public static MenuSecurityDAO CreateInstance()
        {
            return new MenuSecurityDAO();
        }
        public SqlConnection dbConnection;
        public SqlTransaction trans;
        /// <summary>
        /// Begin Transaction
        /// </summary>
        public void Begin()
        {
            dbConnection = CreateConnection();
            dbConnection.Open();
            trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }
        /// <summary>
        /// Commit Working
        /// </summary>
        public void Commit()
        {
            try
            {
                trans.Commit();
                dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        /// <summary>
        /// Rollback working
        /// </summary>
        public void Cancel()
        {
            try
            {
                trans.Rollback();
                dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        protected MenuSecurityDAO()
        {
            
        }
        /// <summary>
        /// Get list of menu security by UserID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public MenuSecurityDTOCollection GetMenuSecurityListByUserID(int userId)
        { 
            MenuSecurityDTOCollection result = null;
            var dbConnection = CreateConnection();
            //Add store here
            var command = CreateSPCommand("hpf_menu_security_get", dbConnection);

            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_userid", userId);
            //</Parameter>            
            try
            {
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    result=new MenuSecurityDTOCollection();
                    while (reader.Read())
                    {
                        MenuSecurityDTO menuSecurity = new MenuSecurityDTO();
                        menuSecurity.Permission= ConvertToString(reader["permission_value"])[0];
                        menuSecurity.Target = ConvertToString(reader["item_target"]);
                        result.Add(menuSecurity);
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

            return result;
        }
        /// <summary>
        /// Get menu_security_id
        /// </summary>
        /// <returns>latest menu_security_id</returns>
        public int GetLatestMenuSecurityId()
        {
            int result=0;
            var dbConnection = CreateConnection();
            //Add store here
            var command = CreateSPCommand("hpf_menu_security_get_latest", dbConnection);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        result = ConvertToInt(reader["menu_security_id"]).Value;
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

            return result;
        }
        /// <summary>
        /// Get list of menu security item belong to user and not belong to user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public MenuSecurityDTOCollection GetAllMenuSecurityListByUserID(int userId)
        {
            MenuSecurityDTOCollection result = new MenuSecurityDTOCollection();
            var dbConnection = CreateConnection();
            //Add store here
            var command = CreateSPCommand("hpf_hpf_user_get_all_menu_security", dbConnection);

            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_hpf_user_id", userId);
            //</Parameter>            
            try
            {
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MenuSecurityDTO menuSecurity = new MenuSecurityDTO();
                        menuSecurity.MenuName = ConvertToString(reader["item_name"]);
                        menuSecurity.MenuItemId = ConvertToInt(reader["menu_item_id"]);
                        menuSecurity.MenuSecurityId = ConvertToInt(reader["menu_security_id"]);
                        menuSecurity.Permission = ConvertToString(reader["permission_value"])[0];
                        menuSecurity.HpfUserId = (int?)userId;
                        result.Add(menuSecurity);
                    }
                }
                reader.NextResult();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MenuSecurityDTO menuSecurity = new MenuSecurityDTO();
                        menuSecurity.MenuName = ConvertToString(reader["item_name"]);
                        menuSecurity.MenuItemId = ConvertToInt(reader["menu_item_id"]);
                        menuSecurity.MenuSecurityId = ConvertToInt(reader["menu_security_id"]);
                        menuSecurity.Permission = ConvertToString(reader["permission_value"])[0];
                        menuSecurity.HpfUserId = (int?)userId;
                        result.Add(menuSecurity);
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

            return result;
        }
        public int? InsertHpfUser(UserDTO user)
        {
            var command = CreateSPCommand("hpf_hpf_user_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[18];
            sqlParam[0] = new SqlParameter("@pi_user_login_id", user.UserName);
            sqlParam[1] = new SqlParameter("@pi_password", user.Password);
            sqlParam[2] = new SqlParameter("@pi_active_ind", user.IsActivate);
            sqlParam[3] = new SqlParameter("@pi_user_role_str_tbd", null);
            sqlParam[4] = new SqlParameter("@pi_fname", user.FirstName);
            sqlParam[5] = new SqlParameter("@pi_lname", user.LastName);
            sqlParam[6] = new SqlParameter("@pi_email", user.Email);
            sqlParam[7] = new SqlParameter("@pi_phone", user.Phone);
            sqlParam[8] = new SqlParameter("@pi_user_type", user.UserType);
            sqlParam[9] = new SqlParameter("@pi_last_login_dt", null);
            sqlParam[10] = new SqlParameter("@pi_agency_id", user.AgencyId);
            sqlParam[11] = new SqlParameter("@pi_create_dt",NullableDateTime(user.CreateDate));
            sqlParam[12] = new SqlParameter("@pi_create_user_id",user.CreateUserId);
            sqlParam[13] = new SqlParameter("@pi_create_app_name",user.CreateAppName);
            sqlParam[14] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(user.ChangeLastDate));
            sqlParam[15] = new SqlParameter("@pi_chg_lst_user_id", user.ChangeLastUserId);
            sqlParam[16] = new SqlParameter("@pi_chg_lst_app_name", user.ChangeLastAppName);

            sqlParam[17] = new SqlParameter("@po_hpf_user_id", SqlDbType.Int) { Direction = ParameterDirection.Output };

            //</Parameter>            
            try
            {
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
                return ConvertToInt(command.Parameters["@po_hpf_user_id"].Value);
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
        }
        /// <summary>
        /// Update User DTO
        /// </summary>
        /// <param name="webUser">UserDTO</param>
        public void UpdateHpfUser(UserDTO user)
        {
            var command = CreateSPCommand("hpf_hpf_user_update", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[15];
            sqlParam[0] = new SqlParameter("@pi_user_login_id", user.UserName);
            sqlParam[1] = new SqlParameter("@pi_hpf_user_id", user.HPFUserId);
            sqlParam[2] = new SqlParameter("@pi_password", user.Password);
            sqlParam[3] = new SqlParameter("@pi_active_ind", user.IsActivate);
            sqlParam[4] = new SqlParameter("@pi_user_role_str_tbd", null);
            sqlParam[5] = new SqlParameter("@pi_fname", user.FirstName);
            sqlParam[6] = new SqlParameter("@pi_lname", user.LastName);
            sqlParam[7] = new SqlParameter("@pi_email", user.Email);
            sqlParam[8] = new SqlParameter("@pi_phone", user.Phone);
            sqlParam[9] = new SqlParameter("@pi_user_type", user.UserType);
            sqlParam[10] = new SqlParameter("@pi_last_login_dt", null);
            sqlParam[11] = new SqlParameter("@pi_agency_id", user.AgencyId);
            sqlParam[12] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(user.ChangeLastDate));
            sqlParam[13] = new SqlParameter("@pi_chg_lst_user_id", user.ChangeLastUserId);
            sqlParam[14] = new SqlParameter("@pi_chg_lst_app_name", user.ChangeLastAppName);

            //</Parameter>            
            try
            {
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
        }
        public void UpdateMenuSecurity(MenuSecurityDTO item)
        {
            var command = CreateSPCommand("hpf_menu_security_update", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@pi_menu_security_id", item.MenuSecurityId);
            sqlParam[1] = new SqlParameter("@pi_permission_value", item.Permission);
            //</Parameter>            
            try
            {
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
        }
        public void InsertMenuSecurity(MenuSecurityDTO item)
        {
            var command = CreateSPCommand("hpf_menu_security_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@pi_menu_security_id", item.MenuSecurityId);
            sqlParam[1] = new SqlParameter("@pi_menu_item_id", item.MenuItemId);
            sqlParam[2] = new SqlParameter("@pi_permission_value", item.Permission);
            sqlParam[3] = new SqlParameter("@pi_hpf_user_id", item.HpfUserId);
            //</Parameter>            
            try
            {
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
        }
        public void DeleteMenuSecurity(int menuSecurityId)
        {
            var command = CreateSPCommand("hpf_menu_security_delete", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_menu_security_id", menuSecurityId);
            try
            {
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
        }
    }
}
