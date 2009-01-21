using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class BudgetDetailDTO:BaseDTO
    {
        public string BudgetCategory { get; set; }
        public string BudgetSubCategory { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
    }
}
