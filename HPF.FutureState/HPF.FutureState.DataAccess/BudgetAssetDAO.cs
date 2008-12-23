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
        /// Select a BudgetItem to database.
        /// Where Max BudgetSet_ID and FC_ID
        /// </summary>
        /// <param name="budgetItem">BudgetItemDTO</param>
        /// <returns></returns>
        public BudgetAssetDTOCollection GetBudgetSet(int fcId)
        {
            BudgetAssetDTOCollection results = new BudgetAssetDTOCollection();

            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_budget_asset_get", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
            
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
