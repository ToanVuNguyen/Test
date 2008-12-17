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
            sqlParam[0] = new SqlParameter("@outcome_set_id", outComeItem.OutcomeSetId);
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
            sqlParam[0] = new SqlParameter("@outcome_set_id", outComeItem.OutcomeSetId);
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
    }
}
