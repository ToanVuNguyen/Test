using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BudgetSetDTO : BaseDTO
    {
        public int BudgetSetId { get; set; }

        public int FcId { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH)]
        public double TotalIncome { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH)]
        public double TotalExpenses { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH)]
        public double TotalAssets { get; set; }

        public DateTime BudgetSetDt { get; set; }
    }
}
