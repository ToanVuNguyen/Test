using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    public class AgencyDAO:BaseDAO
    {
        private static readonly AgencyDAO instance = new AgencyDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static AgencyDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected AgencyDAO()
        {
            
        }

        public string GetAgencyName(int AgencyID)
        {
            string returnString=string.Empty;
            SqlConnection dbConnection = base.CreateConnection();
            try
            {
                SqlCommand command = base.CreateCommand("hpf_agency_get_from_agency_id", dbConnection);
                //<Parameter>
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@pi_agency_id", AgencyID);

                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;

                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        returnString = ConvertToString(reader["agency_name"]);
                    }
                    reader.Close();
                }
                else
                    returnString = string.Empty;

            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return returnString;
        }
    }
}
