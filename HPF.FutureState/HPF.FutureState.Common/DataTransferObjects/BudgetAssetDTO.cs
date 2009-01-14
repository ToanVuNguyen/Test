using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BudgetAssetDTO : BaseDTO
    {
        private const string RULE_SET_LENGTH = "Length";

        public int BudgetAssetId { get; set; }

        public int BudgetSetId { get; set; }

        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string AssetName { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = RULE_SET_LENGTH)]
        public double AssetValue { get; set; }          
    }
}
