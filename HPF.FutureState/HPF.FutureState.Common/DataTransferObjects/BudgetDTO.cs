using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BudgetDTO : BaseDTO
    {
        public int BudgetSubcategoryId { get; set; }

        public string BudgetCategoryCode { get; set; }
    }
}
