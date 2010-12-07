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
    public class PPBudgetDAO:BaseDAO
    {
        private static readonly PPBudgetDAO _instance = new PPBudgetDAO();
        public static PPBudgetDAO Instance
        {
            get { return _instance; }
        }
        protected PPBudgetDAO(){}

        /// <summary>
        /// Get PPBudgetDetailDTO from 2 recordset retun by SP.
        /// </summary>
        /// <param name="ppBudgetSetId">PPBudgetSet ID</param>
        /// <returns>PPBudgetDetailDTO contains 2 collections: PPBugetItem and PPBudgetAsset</returns>
        public PPBudgetDetailDTO GetPPBudgetDetail(int? ppBudgetSetId)
        {
            PPBudgetDetailDTO result = null;
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_pp_budget_detail_get", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_pp_budget_set_id", ppBudgetSetId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    result = new PPBudgetDetailDTO();
                    PPBudgetItemDTOCollection ppBudgetItemCollection = new PPBudgetItemDTOCollection();
                    while (reader.Read())
                    {
                        PPBudgetItemDTO ppBudgetItem = new PPBudgetItemDTO();
                        ppBudgetItem.BudgetCategory = ConvertToString(reader["budget_category"]);
                        ppBudgetItem.BudgetSubCategory = ConvertToString(reader["budget_subcategory"]);
                        ppBudgetItem.PPBudgetItemAmt = ConvertToDouble(reader["pp_budget_item_amt"]);
                        ppBudgetItem.PPBudgetNote = ConvertToString(reader["pp_budget_note"]);
                        ppBudgetItemCollection.Add(ppBudgetItem);
                    }
                    result.PPBudgetItemCollection = ppBudgetItemCollection;
                    reader.NextResult();
                    PPBudgetAssetDTOCollection ppBudgetAssetCollection = new PPBudgetAssetDTOCollection();
                    while (reader.Read())
                    {
                        PPBudgetAssetDTO ppBudgetAsset = new PPBudgetAssetDTO();
                        ppBudgetAsset.PPBudgetAssetName = ConvertToString(reader["pp_budget_asset_name"]);
                        ppBudgetAsset.PPBudgetAssetValue = ConvertToDouble(reader["pp_budget_asset_value"]);
                        ppBudgetAsset.PPBudgetAssetNote = ConvertToString(reader["pp_budget_asset_note"]);

                        ppBudgetAssetCollection.Add(ppBudgetAsset);
                    }
                    result.PPBudgetAssetCollection = ppBudgetAssetCollection;
                }
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                command.Dispose();
                dbConnection.Close();
            }
            return result;
        }

        /// <summary>
        /// Get latest pre-purchase budget set by ppcId
        /// </summary>
        /// <param name=""></param>
        /// <returns>PPBudgetSetDTO</returns>
        public PPBudgetSetDTO GetPPBudgetSet(int? ppcId)
        {
            PPBudgetSetDTO ppBudgetSet = null;
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_pp_budget_set_get", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_ppc_id", ppcId);
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    ppBudgetSet = new PPBudgetSetDTO();
                    if (reader.Read())
                    {
                        ppBudgetSet.PPBudgetSetId = ConvertToInt(reader["pp_budget_set_id"]);
                        ppBudgetSet.TotalIncome = ConvertToDouble(reader["total_income"]);
                        ppBudgetSet.TotalExpenses = ConvertToDouble(reader["total_expenses"]);
                        ppBudgetSet.TotalAssets = ConvertToDouble(reader["total_assets"]);
                        ppBudgetSet.PPBudgetSetDt = ConvertToDateTime(reader["budget_dt"]);
                        ppBudgetSet.TotalSurplus = ConvertToDouble(reader["Total_surplus"]);
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
                command.Dispose();
                dbConnection.Close();
            }
            return ppBudgetSet;
        }

        /// <summary>
        /// Get latest pre-purchase budget set by ppcId
        /// </summary>
        /// <param name=""></param>
        /// <returns>PPBudgetSetDTO</returns>
        public PPPBudgetSetDTO GetProposedPPBudgetSet(int? ppcId)
        {
            PPPBudgetSetDTO ppBudgetSet = null;
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_ppp_budget_set_get", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_ppc_id", ppcId);
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    ppBudgetSet = new PPPBudgetSetDTO();
                    if (reader.Read())
                    {
                        ppBudgetSet.PPPBudgetSetId = ConvertToInt(reader["ppp_budget_set_id"]);
                        ppBudgetSet.TotalIncome = ConvertToDouble(reader["total_income"]);
                        ppBudgetSet.TotalExpenses = ConvertToDouble(reader["total_expenses"]);
                        ppBudgetSet.TotalAssets = ConvertToDouble(reader["total_assets"]);
                        ppBudgetSet.PPPBudgetSetDt = ConvertToDateTime(reader["budget_dt"]);
                        ppBudgetSet.TotalSurplus = ConvertToDouble(reader["Total_surplus"]);
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
                command.Dispose();
                dbConnection.Close();
            }
            return ppBudgetSet;
        }

        /// <summary>
        /// Select a Pre-Purchase BudgetAsset from database.
        /// Where Max PP_BudgetSet_ID and pp_case_id
        /// </summary>
        /// <param name="ppcId">ppcId</param>
        /// <returns></returns>
        public PPBudgetAssetDTOCollection GetPPBudgetAssetSet(int? ppcId)
        {
            PPBudgetAssetDTOCollection results = null;
            var dbConnection = CreateConnection();
            var command = new SqlCommand("hpf_pp_budget_asset_get", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_ppc_id", ppcId);
            try
            {
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new PPBudgetAssetDTOCollection();
                    while (reader.Read())
                    {
                        PPBudgetAssetDTO item = new PPBudgetAssetDTO();
                        item.PPBudgetAssetId = ConvertToInt(reader["pp_budget_asset_id"]);
                        item.PPBudgetSetId = ConvertToInt(reader["pp_budget_set_id"]);
                        item.PPBudgetAssetName = ConvertToString(reader["pp_budget_asset_name"]);
                        item.PPBudgetAssetValue = ConvertToDouble(reader["pp_budget_asset_value"]);
                        item.PPBudgetAssetNote = ConvertToString(reader["pp_budget_asset_note"]);
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
                command.Dispose();
                dbConnection.Close();
            }
            return results;
        }

        /// <summary>
        /// Select Pre-Purchase Budget Items from database.
        /// Where Max PP_BudgetSet_ID and Ppc_ID
        /// </summary>
        /// <param name="ppcId">ppcId</param>
        /// <returns></returns>
        public PPBudgetItemDTOCollection GetPPBudgetItemSet(int? ppcId)
        {
            PPBudgetItemDTOCollection results = null;
            var dbConnection = CreateConnection();
            var command = new SqlCommand("hpf_pp_budget_item_get", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_ppc_id", ppcId);
            try
            {
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new PPBudgetItemDTOCollection();
                    while (reader.Read())
                    {
                        PPBudgetItemDTO item = new PPBudgetItemDTO();
                        item.PPBudgetItemId = ConvertToInt(reader["pp_budget_item_id"]);
                        item.PPBudgetSetId = ConvertToInt(reader["pp_budget_set_id"]);
                        item.BudgetSubcategoryId = ConvertToInt(reader["budget_subcategory_id"]);
                        item.PPBudgetItemAmt = ConvertToDouble(reader["pp_budget_item_amt"]);
                        item.PPBudgetNote = ConvertToString(reader["pp_budget_note"]);
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
                command.Dispose();
                dbConnection.Close();
            }
            return results;
        }

        /// <summary>
        /// Select Proposed Pre-Purchase Budget Items from database.
        /// Where Max PPP_BudgetSet_ID and PP_Case_ID
        /// </summary>
        /// <param name="ppcId">ppcId</param>
        /// <returns></returns>
        public PPPBudgetItemDTOCollection GetProposedPPBudgetItemSet(int? ppcId)
        {
            PPPBudgetItemDTOCollection results = null;
            var dbConnection = CreateConnection();
            var command = new SqlCommand("hpf_ppp_budget_item_get", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_ppc_id", ppcId);
            try
            {
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new PPPBudgetItemDTOCollection();
                    while (reader.Read())
                    {
                        PPPBudgetItemDTO item = new PPPBudgetItemDTO();
                        item.PPPBudgetItemId = ConvertToInt(reader["ppp_budget_item_id"]);
                        item.PPPBudgetSetId = ConvertToInt(reader["ppp_budget_set_id"]);
                        item.BudgetSubcategoryId = ConvertToInt(reader["budget_subcategory_id"]);
                        item.ProposedBudgetItemAmt = ConvertToDouble(reader["proposed_budget_item_amt"]);
                        item.ProposedBudgetNote = ConvertToString(reader["proposed_budget_note"]);
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
                command.Dispose();
                dbConnection.Close();
            }
            return results;
        }
    }
}
