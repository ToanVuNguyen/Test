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
        private static readonly MenuSecurityDAO instance = new MenuSecurityDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static MenuSecurityDAO Instance
        {
            get
            {
                return instance;
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
                dbConnection.Close();
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
