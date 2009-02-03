﻿using System;
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
        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "BudgetSubcategoryId is required to save this case")]
        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Tag = ErrorMessages.WARN0328, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        public int? BudgetSubcategoryId { get; set; }

        [XmlElement(IsNullable = true)]
        [NotNullValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "BudgetItemAmt is required to save this case")]
        [NotNullValidator(Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!", Tag = ErrorMessages.WARN0329)]
        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "BudgetItemAmt must be numeric(15,2)")]
        public double? BudgetItemAmt { get; set; }

        [NullableOrStringLengthValidator(true, 100, "Budget Note", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0070)]
        public string BudgetNote { get; set; }

        [XmlIgnore]
        public string BudgetCategory { get; set; }

        [XmlIgnore]
        public string BudgetSubCategory { get; set; }
    }
}
