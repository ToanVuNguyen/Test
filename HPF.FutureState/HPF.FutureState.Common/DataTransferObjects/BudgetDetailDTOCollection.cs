using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BudgetDetailDTOCollection:BaseDTOCollection<BudgetItemDTOCollection>
    {
        public int IndexOf(string budgetCategory)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].BudgetCategory.ToLower() == budgetCategory.ToLower())
                    return i;
            }
            return -1;
        }
    }
}
