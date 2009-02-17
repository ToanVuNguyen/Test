using System;
using System.Data;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using System.Data.SqlClient;

namespace HPF.FutureState.DataAccess
{
    public class ServicerDAO : BaseDAO
    {
        private static readonly ServicerDAO instance = new ServicerDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static ServicerDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected ServicerDAO()
        {
            
        }               


        public ServicerDTOCollection GetServicersByFcId(int? fcId)
        {
            ServicerDTOCollection servicers = null;
            var dbConnection = CreateConnection();
            var command = CreateCommand("hpf_case_loan_get", dbConnection);
            //<Parameter>            
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
            //</Parameter>   
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    servicers = new ServicerDTOCollection();
                    while (reader.Read())
                    {
                        var item = new ServicerDTO();
                        item.ServicerID = ConvertToInt(reader["servicer_id"]);
                        item.ServicerName = ConvertToString(reader["servicer_name"]);
                        item.SecureDeliveryMethodCd = ConvertToString(reader["secure_delivery_method_cd"]);
                        servicers.Add(item);
                    }
                    reader.Close();
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
            return servicers;
        }
    }
}
