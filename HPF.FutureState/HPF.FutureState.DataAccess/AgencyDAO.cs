using System;
using System.Collections.Generic;
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
                SqlCommand command = base.CreateCommand("hpf_agency_detail_get", dbConnection);
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

        /// <summary>
        /// Get ID and Name from table Program
        /// </summary>
        /// <returns>ProgramDTOCollection contains all Program </returns>
        public AgencyDTOCollection GetAgency()
        {
            AgencyDTOCollection results = HPFCacheManager.Instance.GetData<AgencyDTOCollection>(Constant.HPF_CACHE_AGENCY);
            if (results == null)
            {
                var dbConnection = CreateConnection();
                var command = new SqlCommand("hpf_agency_get", dbConnection);
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new AgencyDTOCollection();
                        while (reader.Read())
                        {
                            var item = new AgencyDTO();
                            item.AgencyID = ConvertToString(reader["agency_id"]);
                            item.AgencyName = ConvertToString(reader["agency_name"]);
                            if (item.AgencyID != "-1")
                                results.Add(item);
                        }
                        reader.Close();
                    }
                    HPFCacheManager.Instance.Add(Constant.HPF_CACHE_AGENCY, results);
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
            return results;
        }        

    }
}
