using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class PPBudgetDetailDTO : BaseDTO
    {
        public PPBudgetItemDTOCollection PPBudgetItemCollection { get; set; }
        public PPBudgetAssetDTOCollection PPBudgetAssetCollection { get; set; }
        public PPBudgetDetailDTO()
        {
            PPBudgetItemCollection = new PPBudgetItemDTOCollection();
            PPBudgetAssetCollection = new PPBudgetAssetDTOCollection();
        }
    }
}
