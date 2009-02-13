using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.Utils.DataValidator;



namespace HPF.FutureState.Common.DataTransferObjects
{
    public class CaseFollowUpDTO : BaseDTO
    {
        public int? FcId { get; set; }

        public int? CasePostCounselingStatusId { get; set; }

        public int? OutcomeTypeId { get; set; }

        public string OutcomeTypeName { get; set; }

        [RequiredObjectValidator(Tag = ErrorMessages.ERR0703, Ruleset = Constant.RULESET_FOLLOW_UP)]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_FOLLOW_UP, MessageTemplate = "Follow-Up Date must be between 1/1/1753 and 12/31/9999")]
        public DateTime? FollowUpDt { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Follow-Up Source", Ruleset = Constant.RULESET_FOLLOW_UP, MessageTemplate = "Follow-Up Comment has a maximum length of 8000 characters.")]
        public string FollowUpComment { get; set; }

        [RequiredObjectValidator(Tag = ErrorMessages.ERR0704, Ruleset = Constant.RULESET_FOLLOW_UP)]
        [NullableOrStringLengthValidator(true, 15, "Follow-Up Source", Ruleset = Constant.RULESET_FOLLOW_UP, MessageTemplate = "Follow-Up Source has a maximum length of 15 characters.")]
        public string FollowUpSourceCd { get; set; }

        public string FollowUpSourceCdDesc{ get; set; }

        public string LoanDelinqStatusCd { get; set; }

        public string LoanDelinqStatusCdDesc { get; set; }

        public string StillInHouseInd { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Follow-Up Source", Ruleset = Constant.RULESET_FOLLOW_UP, MessageTemplate = "Credit Score has a maximum length of 4 characters.")]
        public string CreditScore { get; set; }

        public string CreditBureauCd { get; set; }

        public string CreditBureauCdDesc { get; set; }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_FOLLOW_UP, MessageTemplate = "Credit Date must be between 1/1/1753 and 12/31/9999")]
        public DateTime? CreditReportDt { get; set; }        
    }
}
