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
    public class OutcomeDAO : BaseDAO
    {
        private static readonly OutcomeDAO instance = new OutcomeDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static OutcomeDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected OutcomeDAO()
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
                results = new OutcomeTypeDTOCollection();
                var dbConnection = CreateConnection();
                var command = new SqlCommand("hpf_outcome_type_get", dbConnection);
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var item = new OutcomeTypeDTO();
                            item.OutcomeTypeID = ConvertToInt(reader["outcome_type_id"]);
                            item.OutcomeTypeName = ConvertToString(reader["outcome_type_name"]);
                            item.OutcomeTypeComment = ConvertToString(reader["outcome_type_comment"]);
                            item.PayableInd = ConvertToString(reader["payable_ind"]);
                            item.InactiveDt = ConvertToDateTime(reader["inactive_dt"]);
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

        #region Outcomde Item
        /// <summary>
        /// Select all OutcomeItem from database by Fc_ID. 
        /// </summary>
        /// <param name=""></param>
        /// <returns>OutcomeItemDTOCollection</returns>
        public OutcomeItemDTOCollection RetrieveOutcomeItems(int fcId)
        {
            OutcomeItemDTOCollection results = new OutcomeItemDTOCollection();
            var dbConnection = base.CreateConnection();
            var command = new SqlCommand("hpf_outcome_item_get", dbConnection);
            //<Parameter>            
            var sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
            sqlParam[1] = new SqlParameter("@pi_get_all_indicator", 1);
            //</Parameter>   
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new OutcomeItemDTOCollection();
                    while (reader.Read())
                    {
                        OutcomeItemDTO item = new OutcomeItemDTO();
                        item.OutcomeItemId = ConvertToInt(reader["outcome_item_id"]);
                        item.FcId = ConvertToInt(reader["fc_id"]);
                        item.OutcomeTypeId = ConvertToInt(reader["outcome_type_id"]);
                        item.OutcomeTypeName = ConvertToString(reader["outcome_type_name"]);
                        item.OutcomeDt = ConvertToDateTime(reader["outcome_dt"]);
                        item.OutcomeDeletedDt = ConvertToDateTime(reader["outcome_deleted_dt"]);
                        item.NonprofitreferralKeyNum = ConvertToString(reader["nonprofitreferral_key_num"]);
                        item.ExtRefOtherName = ConvertToString(reader["ext_ref_other_name"]);
                        item.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                        item.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                        item.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                        results.Add(item);
                    }
                }
                reader.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return results;
        }

        public bool DeleteOutcomeItem(OutcomeItemDTO outcomeItem)
        {
            var dbConnection = base.CreateConnection();
            var command = new SqlCommand("hpf_outcome_item_update", dbConnection);
            //<Parameter>            
            var sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@pi_outcome_item_id", outcomeItem.OutcomeItemId);
            sqlParam[1] = new SqlParameter("@pi_chg_lst_dt", outcomeItem.ChangeLastDate);
            sqlParam[2] = new SqlParameter("@pi_chg_lst_user_id", outcomeItem.ChangeLastUserId);
            sqlParam[3] = new SqlParameter("@pi_chg_lst_app_name", outcomeItem.ChangeLastAppName);
            //</Parameter>   
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                dbConnection.Open();
                command.ExecuteNonQuery();
                return true;
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

        public bool InstateOutcomeItem(OutcomeItemDTO outcomeItem)
        {
            var dbConnection = base.CreateConnection();
            var command = new SqlCommand("hpf_outcome_item_update", dbConnection);
            //<Parameter>            
            var sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@pi_outcome_item_id", outcomeItem.OutcomeItemId);
            sqlParam[1] = new SqlParameter("@pi_chg_lst_dt", outcomeItem.ChangeLastDate);
            sqlParam[2] = new SqlParameter("@pi_chg_lst_user_id", outcomeItem.ChangeLastUserId);
            sqlParam[3] = new SqlParameter("@pi_chg_lst_app_name", outcomeItem.ChangeLastAppName);
            sqlParam[4] = new SqlParameter("@pi_is_instate", 1);
            //</Parameter>   
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                dbConnection.Open();
                command.ExecuteNonQuery();
                return true;
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

        /// <summary>
        /// Select all OutcomeItem from database by Fc_ID. 
        /// </summary>
        /// <param name=""></param>
        /// <returns>OutcomeItemDTOCollection</returns>
        public OutcomeItemDTOCollection GetOutcomeItemCollection(int? fcId)
        {
            OutcomeItemDTOCollection results = null;// HPFCacheManager.Instance.GetData<OutcomeItemDTOCollection>(Constant.HPF_CACHE_OUTCOME_ITEM);            
            if (results == null)
            {
                var dbConnection = CreateConnection();
                var command = new SqlCommand("hpf_outcome_item_get", dbConnection);
                //<Parameter>            
                var sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                try
                {
                    //</Parameter>                   
                    command.Parameters.AddRange(sqlParam);
                    command.CommandType = CommandType.StoredProcedure;
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new OutcomeItemDTOCollection();
                        while (reader.Read())
                        {
                            OutcomeItemDTO item = new OutcomeItemDTO();
                            item.OutcomeItemId = ConvertToInt(reader["outcome_item_id"]);
                            item.FcId = ConvertToInt(reader["fc_id"]);
                            item.OutcomeTypeId = ConvertToInt(reader["outcome_type_id"]);
                            item.OutcomeDt = ConvertToDateTime(reader["outcome_dt"]);
                            item.OutcomeDeletedDt = ConvertToDateTime(reader["outcome_deleted_dt"]);
                            item.NonprofitreferralKeyNum = ConvertToString(reader["nonprofitreferral_key_num"]);
                            item.ExtRefOtherName = ConvertToString(reader["ext_ref_other_name"]);
                            item.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                            item.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                            item.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                            results.Add(item);
                        }
                    }
                    reader.Close();
                    //HPFCacheManager.Instance.Add(Constant.HPF_CACHE_OUTCOME_ITEM, results);
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
            return results;
        }
        #endregion
             
    }
}
