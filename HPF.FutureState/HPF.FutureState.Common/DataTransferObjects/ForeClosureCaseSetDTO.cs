using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class ForeClosureCaseSetDTO
    {
        public ForeClosureCaseDTO ForeClosureCase { get; set; }

        public CaseLoanDTOCollection CaseLoans { get; set; }

        public BudgetItemDTOCollection BudgetItems { get; set; }

        public BudgetAssetDTOCollection BudgetAssets { get; set; }
    }
}
