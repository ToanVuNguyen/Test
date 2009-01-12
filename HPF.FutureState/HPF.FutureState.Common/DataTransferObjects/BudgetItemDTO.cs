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

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Tag="WARN336" , Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public int BudgetSubcategoryId { get; set; }

        [NotNullValidator(Ruleset = "RequirePartialValidate", MessageTemplate = "Required!", Tag="WARN337")]
        public decimal BudgetItemAmt { get; set; }

        public string BudgetNote { get; set; }       
    }
}
