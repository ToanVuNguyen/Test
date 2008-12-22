using System;
using System.Data;
using System.Data.SqlClient;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

namespace HPF.FutureState.DataAccess
{
    public class OutcomeItemDAO : BaseDAO
    {
        private static readonly OutcomeItemDAO instance = new OutcomeItemDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static OutcomeItemDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected OutcomeItemDAO()
        {
            
        }

        /// <summary>
        /// Insert a Outcome Item to database.
        /// </summary>
        /// <param name="outComeItem">OutcomeItemDTO</param>
        /// <returns></returns>
        public void InsertOutcomeItem(OutcomeItemDTO outComeItem)
        { 
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_outcome_item_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[6];
            sqlParam[0] = new SqlParameter("@fc_id", outComeItem.OutcomeSetId);
            sqlParam[1] = new SqlParameter("@outcome_type_id", outComeItem.OutcomeTypeId);
            sqlParam[2] = new SqlParameter("@outcome_dt", outComeItem.OutcomeDt);
            sqlParam[3] = new SqlParameter("@outcome_deleted_dt", outComeItem.OutcomeDeletedDt);
            sqlParam[4] = new SqlParameter("@nonprofitreferral_key_num", outComeItem.NonprofitreferralKeyNum);
            sqlParam[5] = new SqlParameter("@ext_ref_other_name", outComeItem.ExtRefOtherName);

            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            dbConnection.Open();
            var trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = trans;
            try
            {
                command.ExecuteNonQuery();
                trans.Commit();
                dbConnection.Close();                
            }
            catch(Exception Ex)
            {
                trans.Rollback();
                dbConnection.Close();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }            
        }

        /// <summary>
        /// Update a Outcome Item to database.
        /// </summary>
        /// <param name="outComeItem">OutcomeItemDTO</param>
        /// <returns></returns>
        public void UpdateOutcomeItem(OutcomeItemDTO outComeItem)
        {   
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_outcome_item_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[7];
            sqlParam[0] = new SqlParameter("@fc_id", outComeItem.OutcomeSetId);
            sqlParam[1] = new SqlParameter("@outcome_type_id", outComeItem.OutcomeTypeId);
            sqlParam[2] = new SqlParameter("@outcome_dt", outComeItem.OutcomeDt);
            sqlParam[3] = new SqlParameter("@outcome_deleted_dt", outComeItem.OutcomeDeletedDt);
            sqlParam[4] = new SqlParameter("@nonprofitreferral_key_num", outComeItem.NonprofitreferralKeyNum);
            sqlParam[5] = new SqlParameter("@ext_ref_other_name", outComeItem.ExtRefOtherName);
            sqlParam[6] = new SqlParameter("@outcome_item_id", outComeItem.OutcomeItemId);
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            dbConnection.Open();
            var trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = trans;
            try
            {
                command.ExecuteNonQuery();
                trans.Commit();
                dbConnection.Close();                
            }
            catch(Exception Ex)
            {
                trans.Rollback();
                dbConnection.Close();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }                    
        }

        /// <summary>
        /// Select all RefCodeItem from database.        
        /// </summary>
        /// <param name=""></param>
        /// <returns>RefCodeItemDTOCollection</returns>
        public OutcomeItemDTOCollection GetOutcomeItemCollection(int fc_id)
        {
            OutcomeItemDTOCollection results = new OutcomeItemDTOCollection();

            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_get_outcome_item_list", dbConnection);
            //<Parameter>            
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@fc_id", fc_id);
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
                        item.OutcomeDt = ConvertToDateTime(reader["outcome_dt"]);
                        item.OutcomeDeletedDt = ConvertToDateTime(reader["outcome_deleted_dt"]);
                        item.NonprofitreferralKeyNum = ConvertToString(reader["nonprofitreferral_key_num"]);
                        item.ExtRefOtherName = ConvertToString(reader["ext_ref_other_name"]);
                        results.Add(item);
                    }
                    reader.Close();
                }
                dbConnection.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            return results;
        }   
    }
}
