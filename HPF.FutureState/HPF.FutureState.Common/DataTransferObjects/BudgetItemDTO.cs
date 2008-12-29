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

        [NotNullValidator(Ruleset = "Min Request Validate", MessageTemplate = "Required!")]
        public int BudgetSubcategoryId { get; set; }

        [NotNullValidator(Ruleset = "Min Request Validate", MessageTemplate = "Required!")]
        public decimal BudgetItemAmt { get; set; }

        public string BudgetNote { get; set; }       
    }
}
