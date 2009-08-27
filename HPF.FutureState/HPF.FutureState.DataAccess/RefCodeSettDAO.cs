using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    public class RefCodeSettDAO: BaseDAO
    {
        private static readonly RefCodeSettDAO instance = new RefCodeSettDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static RefCodeSettDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected RefCodeSettDAO()
        {
            
        }

        public RefCodeSetDTOCollection GetRefCodeSet()
        {
            RefCodeSetDTOCollection refCodeSet = HPFCacheManager.Instance.GetData<RefCodeSetDTOCollection>(Constant.HPF_CACHE_REFCODESET);
            if (refCodeSet == null)
            {
                var dbConnection = CreateConnection();
                var command = CreateCommand("hpf_ba_ref_code_set_get", dbConnection);
                command.CommandType = CommandType.StoredProcedure;
                refCodeSet = new RefCodeSetDTOCollection();
                try
                {
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var item = new RefCodeSetDTO();
                            item.RefCodeSetName = ConvertToString(reader["ref_code_set_name"]);
                            item.CodeSetComment = ConvertToString(reader["code_set_comment"]);
                            item.AgencyUsageInd = ConvertToString(reader["agency_usage_ind"]);
                            refCodeSet.Add(item);
                        }
                        reader.Close();
                    }
                    dbConnection.Close();
                    HPFCacheManager.Instance.Add(Constant.HPF_CACHE_REFCODESET, refCodeSet);
                }
                catch (Exception Ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
                }
            }
            return refCodeSet;
        }
    }
}
