using System;
using System.Data;
using System.Data.SqlClient;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

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
        /// Select all RefCodeItem from database. 
        /// Use cache
        /// </summary>
        /// <param name=""></param>
        /// <returns>RefCodeItemDTOCollection</returns>
        public RefCodeItemDTOCollection GetRefCodeItem()
        {            
            RefCodeItemDTOCollection results = HPFCacheManager.Instance.GetData<RefCodeItemDTOCollection>("refCodeItem");
            if (results == null)
            {                
                var dbConnection = CreateConnection();
                var command = CreateCommand("hpf_ref_code_item_get", dbConnection);            
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new RefCodeItemDTOCollection();
                        while (reader.Read())
                        {
                            RefCodeItemDTO item = new RefCodeItemDTO();
                            item.RefCodeItemId = ConvertToInt(reader["ref_code_item_id"]);
                            item.RefCodeSetName = ConvertToString(reader["ref_code_set_name"]);
                            item.Code = ConvertToString(reader["code"]);
                            item.CodeDesc = ConvertToString(reader["code_desc"]);
                            item.CodeComment = ConvertToString(reader["code_comment"]);     
                            item.SortOrder = ConvertToInt(reader["sort_order"]);
                            item.ActiveInd = ConvertToString(reader["active_ind"]);                        
                            results.Add(item);
                        }
                        reader.Close();
                    }
                    dbConnection.Close();
                    HPFCacheManager.Instance.Add("refCodeItem", results);
                }
                catch (Exception Ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
                }
            }            
            return results;
        }   
    }
}
