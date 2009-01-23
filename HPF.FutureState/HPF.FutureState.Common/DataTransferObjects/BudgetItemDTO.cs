using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BudgetItemDTO : BaseDTO
    {
        public int BudgetItemId { get; set; }

        public int BudgetSetId { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "BudgetSubcategoryId is required to save this case")]
        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Tag = ErrorMessages.WARN0328, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        public int BudgetSubcategoryId { get; set; }

        [NotNullValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "BudgetItemAmt is required to save this case")]
        [NotNullValidator(Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!", Tag = ErrorMessages.WARN0329)]
        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "BudgetItemAmt must be numeric(15,2)")]
        public double BudgetItemAmt { get; set; }

        [NullableOrStringLengthValidator(true, 100, "Budget Note", Ruleset = Constant.RULESET_LENGTH)]
        public string BudgetNote { get; set; }

        public string BudgetCategory { get; set; }

        public string BudgetSubCategory { get; set; }
    }
}
