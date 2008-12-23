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
    }
}
