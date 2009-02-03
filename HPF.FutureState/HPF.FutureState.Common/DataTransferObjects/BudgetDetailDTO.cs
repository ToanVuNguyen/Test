using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BudgetDetailDTO:BaseDTO
    {
        public BudgetItemDTOCollection BudgetItemCollection { get; set; }
        public BudgetAssetDTOCollection BudgetAssetCollection { get; set; }
        public BudgetDetailDTO()
        {
            BudgetItemCollection = new BudgetItemDTOCollection();
            BudgetAssetCollection = new BudgetAssetDTOCollection();
        }
    }
}
