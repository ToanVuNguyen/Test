using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BudgetAssetDTO : BaseDTO
    {
        public int BudgetAssetId { get; set; }

        public int BudgetSetId { get; set; }

        public string AssetName { get; set; }

        public string AssetValue { get; set; }          
    }
}
