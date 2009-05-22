using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    public class RefCodeItemDAO : BaseDAO
    {
        private static readonly RefCodeItemDAO instance = new RefCodeItemDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static RefCodeItemDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected RefCodeItemDAO()
        {
            
        }
               
        /// <summary>
        /// Select all RefCodeItem from database
        /// </summary>
        /// <returns>RefCodeItemDTOCollection</returns>
        public RefCodeItemDTOCollection GetRefCodeItems()
        {            
            //Read RefCodeItems from Cache if any
            var results = HPFCacheManager.Instance.GetData<RefCodeItemDTOCollection>(Constant.HPF_CACHE_REF_CODE_ITEM);
            if (results == null)//not in cached
            {
                results = GetRefCodeItemsFromDatabase(null, null);
                HPFCacheManager.Instance.Add(Constant.HPF_CACHE_REF_CODE_ITEM, results);
            }
            return results;
        }

        public RefCodeItemDTOCollection GetRefCodeItemsFromDatabase(string agencyUsageInd, string codeSetName)
        {
            RefCodeItemDTOCollection refCodeItems = null;
            var dbConnection = CreateConnection();
            var command = CreateCommand("hpf_ref_code_item_get", dbConnection);            
            command.CommandType = CommandType.StoredProcedure;
            ArrayList sqlParams = new ArrayList();            

            try
            {
                if (agencyUsageInd != null && agencyUsageInd != string.Empty)
                    sqlParams.Add(new SqlParameter("@pi_agency_usage_ind", agencyUsageInd));
                if (codeSetName != null && codeSetName != string.Empty)
                    sqlParams.Add(new SqlParameter("@pi_code_set_name", codeSetName));

                if (sqlParams.Count > 0)
                    command.Parameters.AddRange((SqlParameter[])sqlParams.ToArray(typeof(SqlParameter)));

                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    refCodeItems = new RefCodeItemDTOCollection();
                    while (reader.Read())
                    {
                        var item = new RefCodeItemDTO();
                        item.RefCodeItemId = ConvertToInt(reader["ref_code_item_id"]);
                        item.RefCodeSetName = ConvertToString(reader["ref_code_set_name"]);
                        item.CodeValue = ConvertToString(reader["code"]);
                        item.CodeDescription = ConvertToString(reader["code_desc"]);
                        item.CodeComment = ConvertToString(reader["code_comment"]);     
                        item.SortOrder = ConvertToInt(reader["sort_order"]);
                        item.ActiveInd = ConvertToString(reader["active_ind"]);
                        //item.AgencyUsageInd = ConvertToString(reader["agency_usage_ind"]);
                        refCodeItems.Add(item);
                    }
                    reader.Close();
                }
                dbConnection.Close();                    
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            return refCodeItems;
        }
    }
}
