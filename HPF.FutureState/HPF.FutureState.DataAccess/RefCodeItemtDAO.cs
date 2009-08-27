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
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return refCodeItems;
        }

        public RefCodeItemDTOCollection GetRefCodeItems(RefCodeSearchCriteriaDTO criteria)
        {
            RefCodeItemDTOCollection refCodeItems = new RefCodeItemDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateCommand("hpf_ba_ref_code_item_get", dbConnection);
            command.CommandType = CommandType.StoredProcedure;            

            try
            {
                var sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@pi_include_inactive", criteria.IncludedInActive);
                sqlParam[1] = new SqlParameter("@pi_code_set_name", criteria.CodeSetName);
                command.Parameters.AddRange(sqlParam);

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
                        item.CreateDate = ConvertToDateTime(reader["create_dt"]);
                        item.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                        refCodeItems.Add(item);
                    }
                    reader.Close();
                }                
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return refCodeItems;
        }

        public RefCodeItemDTO GetRefCodeItem(int refCodeItemId)
        {
            var item = new RefCodeItemDTO();
            var dbConnection = CreateConnection();
            var command = CreateCommand("hpf_ba_ref_code_item_get_by_id", dbConnection);
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                var sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@pi_ref_code_item_id", refCodeItemId);                
                command.Parameters.AddRange(sqlParam);

                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {                    
                    while (reader.Read())
                    {                        
                        item.RefCodeItemId = ConvertToInt(reader["ref_code_item_id"]);
                        item.RefCodeSetName = ConvertToString(reader["ref_code_set_name"]);
                        item.CodeValue = ConvertToString(reader["code"]);
                        item.CodeDescription = ConvertToString(reader["code_desc"]);
                        item.CodeComment = ConvertToString(reader["code_comment"]);
                        item.SortOrder = ConvertToInt(reader["sort_order"]);
                        item.ActiveInd = ConvertToString(reader["active_ind"]);
                        item.CreateDate = ConvertToDateTime(reader["create_dt"]);
                        item.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);                        
                    }
                    reader.Close();
                }
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return item;
        }

        public void SaveRefCodeItem(RefCodeItemDTO refCode)
        {
            var dbConnection = CreateConnection();
            var command = CreateCommand("hpf_ba_ref_code_item_save", dbConnection);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var sqlParam = new SqlParameter[9];
                sqlParam[0] = new SqlParameter("@pi_ref_code_item_id", refCode.RefCodeItemId);
                sqlParam[1] = new SqlParameter("@pi_ref_code_set_name", refCode.RefCodeSetName);
                sqlParam[2] = new SqlParameter("@pi_code", refCode.CodeValue);
                sqlParam[3] = new SqlParameter("@pi_code_desc", refCode.CodeDescription);
                sqlParam[4] = new SqlParameter("@pi_code_comment", refCode.CodeComment);
                sqlParam[5] = new SqlParameter("@pi_sort_order", refCode.SortOrder);
                sqlParam[6] = new SqlParameter("@pi_active_ind", refCode.ActiveInd);
                sqlParam[7] = new SqlParameter("@pi_create_user_id", refCode.ChangeLastUserId);
                sqlParam[8] = new SqlParameter("@pi_create_app_name", refCode.ChangeLastAppName);
                command.Parameters.AddRange(sqlParam);

                command.ExecuteNonQuery();

            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
