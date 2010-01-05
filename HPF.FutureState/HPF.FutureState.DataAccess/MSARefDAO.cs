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
    public class MSARefDAO: BaseDAO
    {
        private static readonly MSARefDAO instance = new MSARefDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static MSARefDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected MSARefDAO()
        {
            
        }

        public MSARefDTOCollection GetMSARefs()
        {
            MSARefDTOCollection results = HPFCacheManager.Instance.GetData<MSARefDTOCollection>(Constant.HPF_CACHE_MSAREFCODES);
            if (results == null)
            {
                var dbConnection = CreateConnection();
                var command = new SqlCommand("hpf_msa_ref_get", dbConnection);
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new MSARefDTOCollection();
                        while (reader.Read())
                        {
                            var item = new MSARefDTO();
                            item.MSARefId = ConvertToInt(reader["msa_ref_id"]).Value;
                            item.MSAName = ConvertToString(reader["msa_name"]);
                            item.MSACode = ConvertToString(reader["msa_code"]);
                            item.MSAType = ConvertToString(reader["msa_type"]);
                            results.Add(item);
                        }
                        reader.Close();
                    }
                    HPFCacheManager.Instance.Add(Constant.HPF_CACHE_MSAREFCODES, results);
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
