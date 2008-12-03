using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BudgetSetDTO : BaseDTO
    {
        public int BudgetSetId { get; set; }
        public int FcId { get; set; }
        public string BudgetSetName { get; set; }
        public string BudgetSetComment { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalAssets { get; set; }        
    }
}
