using System;
using System.Data;
using System.Data.SqlClient;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

namespace HPF.FutureState.DataAccess
{
    public class BudgetAssetDAO : BaseDAO
    {
        private static readonly BudgetAssetDAO instance = new BudgetAssetDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static BudgetAssetDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected BudgetAssetDAO()
        {
            
        }

        /// <summary>
        /// Insert a BudgetAsset to database.
        /// </summary>
        /// <param name="budgetItem">BudgetAssetDTO</param>
        /// <returns></returns>
        public void InsertBudgetAsset(BudgetAssetDTO budgetAsset)
        { 
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_budget_asset_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@budget_set_id", budgetAsset.BudgetSetId);
            sqlParam[1] = new SqlParameter("@asset_name", budgetAsset.AssetName);
            sqlParam[2] = new SqlParameter("@asset_value", budgetAsset.AssetValue);
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
        /// Select a BudgetItem to database.
        /// Where Max BudgetSet_ID and FC_ID
        /// </summary>
        /// <param name="budgetItem">BudgetItemDTO</param>
        /// <returns></returns>
        public BudgetAssetDTOCollection GetBudgetSet(int fcId)
        {
            BudgetAssetDTOCollection results = new BudgetAssetDTOCollection();

            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_get_budget_asset_list", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@fc_id", fcId);
            
            //</Parameter>
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new BudgetAssetDTOCollection();
                    while (reader.Read())
                    {
                        BudgetAssetDTO item = new BudgetAssetDTO();
                        item.BudgetAssetId = ConvertToInt(reader["budget_asset_id"]);
                        item.BudgetSetId = ConvertToInt(reader["budget_set_id"]);
                        item.AssetName = ConvertToString(reader["asset_name"]);
                        item.AssetValue = ConvertToDecimal(reader["asset_value"]);                        
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
