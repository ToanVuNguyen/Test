using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BudgetItemDTO : BaseDTO
    {
        public int BudgetItemId { get; set; }

        public int BudgetSetId { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = "Min Request Validate", MessageTemplate = "Required!")]
        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = "Complete", MessageTemplate = "Required!")]
        public int BudgetSubcategoryId { get; set; }

        [RangeValidator(0, RangeBoundaryType.Exclusive, double.MaxValue, RangeBoundaryType.Inclusive, Ruleset = "Min Request Validate", MessageTemplate = "Required!")]
        [RangeValidator(0, RangeBoundaryType.Exclusive, double.MaxValue, RangeBoundaryType.Inclusive, Ruleset = "Complete", MessageTemplate = "Required!")]
        public double BudgetItemAmt { get; set; }

        public string BudgetNote { get; set; }       
    }
}
