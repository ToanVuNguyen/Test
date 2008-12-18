using System;
using System.Data;
using System.Data.SqlClient;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

namespace HPF.FutureState.DataAccess
{
    public class BudgetSetDAO : BaseDAO
    {
        private static readonly BudgetSetDAO instance = new BudgetSetDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static BudgetSetDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected BudgetSetDAO()
        {
            
        }

        /// <summary>
        /// Insert a BudgetSet to database.
        /// </summary>
        /// <param name="budgetSet">BudgetSetDTO</param>
        /// <returns></returns>
        public void InsertBudgetSet(BudgetSetDTO budgetSet)
        { 
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_budget_set_insert", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@fc_id", budgetSet.FcId);            
            sqlParam[1] = new SqlParameter("@total_income", budgetSet.TotalIncome);
            sqlParam[2] = new SqlParameter("@total_expenses", budgetSet.TotalExpenses);
            sqlParam[3] = new SqlParameter("@total_assets", budgetSet.TotalAssets);
            sqlParam[4] = new SqlParameter("@budget_set_dt", budgetSet.TotalAssets);

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
        /// Update a BudgetSet to database.
        /// </summary>
        /// <param name="budgetSet">BudgetSetDTO</param>
        /// <returns></returns>
        public void UpdateBudgetSet(BudgetSetDTO budgetSet)
        {
            var dbConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("hpf_budget_set_update", dbConnection);
            //<Parameter>
            var sqlParam = new SqlParameter[6];
            sqlParam[0] = new SqlParameter("@fc_id", budgetSet.FcId);            
            sqlParam[1] = new SqlParameter("@total_income", budgetSet.TotalIncome);
            sqlParam[2] = new SqlParameter("@total_expenses", budgetSet.TotalExpenses);
            sqlParam[3] = new SqlParameter("@total_assets", budgetSet.TotalAssets);
            sqlParam[4] = new SqlParameter("@budget_set_dt", budgetSet.TotalAssets);
            sqlParam[5] = new SqlParameter("@budget_set_id", budgetSet.BudgetSetId);           

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
            catch (Exception Ex)
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
