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
    public class OutcomeItemDTO : BaseDTO
    {
        public int OutcomeItemId { get; set; }

        public int FcId { get; set; }  

        public int OutcomeSetId { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, Tag = ErrorMessages.ERR0129 ,MessageTemplate = "Required!")]
        public int OutcomeTypeId { get; set; }

        public string OutcomeTypeName { get; set; }

        public DateTime OutcomeDt { get; set; }  

        public DateTime OutcomeDeletedDt { get; set; }

        [NullableOrStringLengthValidator(true, 10, "Nonprofit Referral Key Number", Ruleset = Constant.RULESET_LENGTH)]
        public string NonprofitreferralKeyNum { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Ext Reference Other Name", Ruleset = Constant.RULESET_LENGTH)]
        public string ExtRefOtherName { get; set; }  
    }
}
