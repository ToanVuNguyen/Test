using System;
using System.Data;
using System.Data.SqlClient;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

namespace HPF.FutureState.DataAccess
{
    public class BudgetItemDAO : BaseDAO
    {
        private static readonly BudgetItemDAO instance = new BudgetItemDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static BudgetItemDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected BudgetItemDAO()
        {
            
        }

        /// <summary>
        /// Insert a BudgetItem to database.
        /// </summary>
        /// <param name="budgetItem">BudgetItemDTO</param>
        /// <returns></returns>
        public void InsertBudgetItem(BudgetItemDTO budgetItem)
        { 
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_budget_item_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@budget_set_id", budgetItem.BudgetSetId);
            sqlParam[1] = new SqlParameter("@budget_subcategory_id", budgetItem.BudgetSubcategoryId);
            sqlParam[2] = new SqlParameter("@budget_item_amt", budgetItem.BudgetItemAmt);
            sqlParam[3] = new SqlParameter("@budget_note", budgetItem.BudgetNote);

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
        public BudgetItemDTOCollection GetBudgetSet(int fc_id)
        {
            BudgetItemDTOCollection results = new BudgetItemDTOCollection();

            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_get_budget_item_list", dbConnection);
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
                    results = new BudgetItemDTOCollection();
                    while (reader.Read())
                    {
                        BudgetItemDTO item = new BudgetItemDTO();
                        item.BudgetItemId = ConvertToInt(reader["budget_item_id"]);
                        item.BudgetSetId = ConvertToInt(reader["budget_set_id"]);
                        item.BudgetSubcategoryId = ConvertToInt(reader["budget_subcategory_id"]);
                        item.BudgetItemAmt = ConvertToDecimal(reader["budget_item_amt"]);
                        item.BudgetNote = ConvertToString(reader["budget_note"]);                        
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
