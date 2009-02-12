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
    public class OutcomeTypeDAO : BaseDAO
    {
        private static readonly OutcomeTypeDAO instance = new OutcomeTypeDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static OutcomeTypeDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected OutcomeTypeDAO()
        {

        }

        /// <summary>
        /// Get ID and Name from table OutcomeType
        /// </summary>
        /// <returns>ProgramDTOCollection contains all Program </returns>
        public OutcomeTypeDTOCollection GetOutcomeType()
        {
            OutcomeTypeDTOCollection results = HPFCacheManager.Instance.GetData<OutcomeTypeDTOCollection>(Constant.HPF_CACHE_OUTCOME_TYPE);
            if (results == null)
            {
                var dbConnection = CreateConnection();
                var command = new SqlCommand("hpf_outcome_type_get", dbConnection);
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new OutcomeTypeDTOCollection();
                        while (reader.Read())
                        {
                            var item = new OutcomeTypeDTO();
                            item.OutcomeTypeID = ConvertToInt(reader["outcome_type_id"]);
                            item.OutcomeTypeName = ConvertToString(reader["outcome_type_name"]);
                            item.PayableInd = ConvertToString(reader["payable_ind"]);
                            results.Add(item);
                        }
                        reader.Close();
                    }
                    HPFCacheManager.Instance.Add(Constant.HPF_CACHE_OUTCOME_TYPE, results);
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
