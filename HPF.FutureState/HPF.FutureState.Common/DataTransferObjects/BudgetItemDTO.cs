using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BudgetItemDTO : BaseDTO
    {
        public int BudgetItemId { get; set; }
        public int BudgetSetId { get; set; }
        public int BudgetSubcategoryId { get; set; }
        public decimal BudgetItemAmt { get; set; }
        public string BudgetNote { get; set; }       
    }
}
