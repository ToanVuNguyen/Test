using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using System.Data.SqlClient;

namespace HPF.FutureState.DataAccess
{
    public class BudgetDAO:BaseDAO
    {
        private static readonly BudgetDAO instance = new BudgetDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static BudgetDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected BudgetDAO()
        {
            
        }
        /// <summary>
        /// Get BudgetSet by ForeclosureCaseId
        /// </summary>
        /// <param name="caseId">ForeclosureCaseId</param>
        /// <returns>BudgetSEtDTOCollection</returns>
        public BudgetSetDTOCollection GetBudgetSet(int caseId)
        {
            BudgetSetDTOCollection result = null;
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_budget_set_get", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", caseId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    result = new BudgetSetDTOCollection();
                    while (reader.Read())
                    {
                        BudgetSetDTO budgetSet = new BudgetSetDTO();
                        budgetSet.BudgetSetId = ConvertToInt(reader["budget_set_id"]);
                        budgetSet.BudgetSetDt = ConvertToDateTime(reader["budget_dt"]);
                        budgetSet.TotalIncome = ConvertToDouble(reader["total_income"]);
                        budgetSet.TotalExpenses = ConvertToDouble(reader["total_expenses"]);
                        budgetSet.TotalAssets = ConvertToDouble(reader["total_assets"]);
                        budgetSet.TotalSurplus = ConvertToDouble(reader["Total_surplus"]);
                        budgetSet.FcId = caseId;
                        result.Add(budgetSet);
                    }
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
            return result;
        }
        /// <summary>
        /// Get BudgetDetailDTO from 2 recordset retun by SP.
        /// </summary>
        /// <param name="budgetSetId">BudgetSet ID</param>
        /// <returns>BudgetDetailDTO contains 2 collections: BugetItem and BudgetAsset</returns>
        public BudgetDetailDTO GetBudgetDetail(int? budgetSetId)
        {
            BudgetDetailDTO result = null;
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_budget_detail_get", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_budget_set_id", budgetSetId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    result = new  BudgetDetailDTO();
                    BudgetItemDTOCollection budgetItemCollection = new BudgetItemDTOCollection();
                    while (reader.Read())
                    {
                        BudgetItemDTO budgetItem = new BudgetItemDTO();
                        budgetItem.BudgetCategory = ConvertToString(reader["budget_category"]);
                        budgetItem.BudgetSubCategory = ConvertToString(reader["budget_subcategory"]);
                        budgetItem.BudgetItemAmt = ConvertToDouble(reader["budget_item_amt"]);
                        budgetItem.BudgetNote = ConvertToString(reader["budget_note"]);
                        budgetItemCollection.Add(budgetItem);
                    }
                    result.BudgetItemCollection = budgetItemCollection;
                    reader.NextResult();
                    BudgetAssetDTOCollection budgetAssetCollection = new BudgetAssetDTOCollection();
                    while (reader.Read())
                    {
                        BudgetAssetDTO budgetAsset = new BudgetAssetDTO();
                        budgetAsset.AssetName = ConvertToString(reader["asset_name"]);
                        budgetAsset.AssetValue = ConvertToDouble(reader["asset_value"]);
                        budgetAssetCollection.Add(budgetAsset);
                    }
                    result.BudgetAssetCollection = budgetAssetCollection;
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
            return result;
        }

    }
}
