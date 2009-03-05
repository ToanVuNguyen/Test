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

        private HPFUserDTOCollection GetHpfUsersFromDatabase()
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
                       hpfUsers.Add(new HPFUserDTO(){
                           HpfUserId = ConvertToInt(reader["hpf_user_id"]),
                           UserLoginId = ConvertToString(reader["user_login_id"]),
                           FirstName = ConvertToString(reader["fname"]),
                           LastName = ConvertToString(reader["lname"])
                       });
                    }
                    reader.Close();
                }
                dbConnection.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            return hpfUsers;
        }

    }
}
