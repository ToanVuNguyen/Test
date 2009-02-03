using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BudgetSubcategoryDTO : BaseDTO
    {
        public int BudgetSubcategoryID { get; set; }
        public string BudgetSubcategoryName { get; set; }
    }
}
