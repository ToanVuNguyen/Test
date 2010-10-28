using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class PPPBudgetItemDTO : BaseDTO
    {
        [XmlIgnore]
        public int? PPPBudgetItemId { get; set; }

        [XmlIgnore]
        public int? PPPBudgetSetId { get; set; }

        [XmlElement(IsNullable = true)]
        [RequiredObjectValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Proposed BudgetSubcategoryId is required to save this case")]
        public int? BudgetSubcategoryId { get; set; }

        [XmlElement(IsNullable = true)]
        [RequiredObjectValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Proposed BudgetItemAmt is required to save this case")]
        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0079)]
        public double? ProposedBudgetItemAmt { get; set; }

        [NullableOrStringLengthValidator(true, 100, "Proposed Pre-Purchased Budget Note", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0070)]
        public string ProposedBudgetNote { get; set; }

        [XmlIgnore]
        public string BudgetCategory { get; set; }

        [XmlIgnore]
        public string BudgetSubCategory { get; set; }
    }
}
