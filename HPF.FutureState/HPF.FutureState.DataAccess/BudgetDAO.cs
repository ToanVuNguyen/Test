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
    }
}
