using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using System.Data.SqlClient;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common;
using System.Data;

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
        public BudgetSetDTOCollection GetBudgetSetList(int caseId)
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

        /// <summary>
        /// Get ID and Name from table Budget Subcategory
        /// </summary>
        /// <returns>ProgramDTOCollection contains all Program </returns>
        public BudgetSubcategoryDTOCollection GetBudgetSubcategory()
        {
            BudgetSubcategoryDTOCollection results = HPFCacheManager.Instance.GetData<BudgetSubcategoryDTOCollection>(Constant.HPF_CACHE_BUDGET_SUBCATEGORY);
            if (results == null)
            {
                var dbConnection = CreateConnection();
                var command = new SqlCommand("hpf_budget_subcategory_get", dbConnection);
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new BudgetSubcategoryDTOCollection();
                        while (reader.Read())
                        {
                            var item = new BudgetSubcategoryDTO();
                            item.BudgetSubcategoryID = ConvertToInt(reader["budget_subcategory_id"]).Value;
                            item.BudgetSubcategoryName = ConvertToString(reader["budget_subcategory_name"]);
                            results.Add(item);
                        }
                        reader.Close();
                    }
                    HPFCacheManager.Instance.Add(Constant.HPF_CACHE_BUDGET_SUBCATEGORY, results);
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

        /// <summary>
        /// Select all OutcomeItem from database by Fc_ID. 
        /// </summary>
        /// <param name=""></param>
        /// <returns>OutcomeItemDTOCollection</returns>
        public BudgetDTOCollection GetBudget()
        {
            BudgetDTOCollection results = HPFCacheManager.Instance.GetData<BudgetDTOCollection>(Constant.HPF_CACHE_BUDGET_CATEGORY_CODE);
            if (results == null)
            {
                var dbConnection = CreateConnection();
                var command = new SqlCommand("hpf_view_budget_category_code", dbConnection);
                try
                {
                    //</Parameter>                   
                    command.CommandType = CommandType.StoredProcedure;
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new BudgetDTOCollection();
                        while (reader.Read())
                        {
                            BudgetDTO item = new BudgetDTO();
                            item.BudgetSubcategoryId = ConvertToInt(reader["budget_subcategory_id"]);
                            item.BudgetCategoryCode = ConvertToString(reader["budget_category_cd"]);
                            results.Add(item);
                        }
                        reader.Close();
                    }
                    HPFCacheManager.Instance.Add(Constant.HPF_CACHE_BUDGET_CATEGORY_CODE, results);
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

        /// <summary>
        /// Get latest budget set by fcId
        /// </summary>
        /// <param name=""></param>
        /// <returns>OutcomeItemDTOCollection</returns>
        public BudgetSetDTO GetBudgetSet(int? fcId)
        {
            BudgetSetDTO budgetSet = null;
            var dbConnection = CreateConnection();
            try
            {
                var command = CreateSPCommand("hpf_budget_set_get", dbConnection);
                var sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    budgetSet = new BudgetSetDTO();
                    if (reader.Read())
                    {
                        budgetSet.BudgetSetId = ConvertToInt(reader["budget_set_id"]);
                        budgetSet.TotalIncome = ConvertToDouble(reader["total_income"]);
                        budgetSet.TotalExpenses = ConvertToDouble(reader["total_expenses"]);
                        budgetSet.TotalAssets = ConvertToDouble(reader["total_assets"]);
                        budgetSet.BudgetSetDt = ConvertToDateTime(reader["budget_dt"]);
                        budgetSet.TotalSurplus = ConvertToDouble(reader["Total_surplus"]);
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
            return budgetSet;
        }

        /// <summary>
        /// Select a BudgetItem to database.
        /// Where Max BudgetSet_ID and FC_ID
        /// </summary>
        /// <param name="budgetItem">BudgetItemDTO</param>
        /// <returns></returns>
        public BudgetAssetDTOCollection GetBudgetAssetSet(int? fcId)
        {
            BudgetAssetDTOCollection results = new BudgetAssetDTOCollection();
            var dbConnection = CreateConnection();
            var command = new SqlCommand("hpf_budget_asset_get", dbConnection);
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
                    results = new BudgetAssetDTOCollection();
                    while (reader.Read())
                    {
                        BudgetAssetDTO item = new BudgetAssetDTO();
                        item.BudgetAssetId = ConvertToInt(reader["budget_asset_id"]);
                        item.BudgetSetId = ConvertToInt(reader["budget_set_id"]);
                        item.AssetName = ConvertToString(reader["asset_name"]);
                        item.AssetValue = ConvertToDouble(reader["asset_value"]);
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

        /// <summary>
        /// Select a BudgetItem to database.
        /// Where Max BudgetSet_ID and FC_ID
        /// </summary>
        /// <param name="budgetItem">BudgetItemDTO</param>
        /// <returns></returns>
        public BudgetItemDTOCollection GetBudgetItemSet(int? fcId)
        {
            BudgetItemDTOCollection results = new BudgetItemDTOCollection();
            var dbConnection = CreateConnection();
            var command = new SqlCommand("hpf_budget_item_get", dbConnection);
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
                    results = new BudgetItemDTOCollection();
                    while (reader.Read())
                    {
                        BudgetItemDTO item = new BudgetItemDTO();
                        item.BudgetItemId = ConvertToInt(reader["budget_item_id"]);
                        item.BudgetSetId = ConvertToInt(reader["budget_set_id"]);
                        item.BudgetSubcategoryId = ConvertToInt(reader["budget_subcategory_id"]);
                        item.BudgetItemAmt = ConvertToDouble(reader["budget_item_amt"]);
                        item.BudgetNote = ConvertToString(reader["budget_note"]);
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
    }
}
