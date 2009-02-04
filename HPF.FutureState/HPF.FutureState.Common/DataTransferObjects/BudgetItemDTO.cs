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
    public class BudgetItemDTO : BaseDTO
    {
        
        [XmlIgnore]
        public int? BudgetItemId { get; set; }

        [XmlIgnore]
        public int? BudgetSetId { get; set; }

        [XmlElement(IsNullable = true)]        
        //[RequiredObjectValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "BudgetSubcategoryId is required to save this case")]
        [RequiredObjectValidator(Tag = ErrorMessages.WARN0328, Ruleset = Constant.RULESET_COMPLETE)]
        public int? BudgetSubcategoryId { get; set; }

        [XmlElement(IsNullable = true)]
        //[RequiredObjectValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "BudgetItemAmt is required to save this case")]
        [RequiredObjectValidator(Tag = ErrorMessages.WARN0329, Ruleset = Constant.RULESET_COMPLETE)]
        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "BudgetItemAmt must be numeric(15,2)")]
        public double? BudgetItemAmt { get; set; }

        [NullableOrStringLengthValidator(true, 100, "Budget Note", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0070)]
        public string BudgetNote { get; set; }

        [XmlIgnore]
        public string BudgetCategory { get; set; }

        [XmlIgnore]
        public string BudgetSubCategory { get; set; }
    }
}
