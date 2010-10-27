using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class PPBudgetAssetDTO : BaseDTO
    {
        [XmlIgnore]
        public int? PPBudgetAssetId { get; set; }

        [XmlIgnore]
        public int? PPBudgetSetId { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1130, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 50, "Asset Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0071)]
        public string PPBudgetAssetName { get; set; }

        [RequiredObjectValidator(Tag=ErrorMessages.ERR1131,Ruleset=Constant.RULESET_MIN_REQUIRE_FIELD,MessageTemplate="Required")]
        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0078)]
        public double? PPBudgetAssetValue { get; set; }

        [NullableOrStringLengthValidator(true, 100, "Asset Note", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR1132)]
        public string PPBudgetAssetNote { get; set; }
    }
}
