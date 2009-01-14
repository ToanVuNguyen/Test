using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BudgetItemDTO : BaseDTO
    {
        private const string RULE_SET_LENGTH = "Length";

        public int BudgetItemId { get; set; }

        public int BudgetSetId { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Tag="WARN336" , Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public int BudgetSubcategoryId { get; set; }

        [NotNullValidator(Ruleset = "RequirePartialValidate", MessageTemplate = "Required!", Tag="WARN337")]
        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = RULE_SET_LENGTH)]
        public double BudgetItemAmt { get; set; }

        [NullableOrStringLengthValidator(true, 100, Ruleset = RULE_SET_LENGTH)]
        public string BudgetNote { get; set; }       
    }
}
