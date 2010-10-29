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

    }
}
