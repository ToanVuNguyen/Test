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
    public class CreditReportDTO : BaseDTO
    {
        [XmlIgnore]
        public int? CreditReportId { get; set; }
        [XmlIgnore]
        public int? FcId { get; set; }

        [RequiredObjectValidator(Tag = ErrorMessages.ERR1186, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "CreditPullDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? CreditPullDt { get; set; }

        [NullableOrStringLengthValidator(true, 4, "Credit Score", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR1175)]
        public string CreditScore { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1187, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public string CreditBureauCd { get; set; }

        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR1182)]
        public double? RevolvingBal { get; set; }

        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR1183)]
        public double? RevolvingLimitAmt { get; set; }

        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR1184)]
        public double? InstallmentBal { get; set; }

        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR1185)]
        public double? InstallmentLimitAmt { get; set; }
    }
}
