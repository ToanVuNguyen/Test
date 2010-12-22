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
    public class ApplicantDTO:BaseDTO
    {
        [XmlIgnore]
        public int? ApplicantId { get; set; }

        [XmlIgnore]
        public int? ServicerApplicantId { get; set; }

        [XmlIgnore]
        public int? ProgramId { get; set; }

        private string _groupCd;
        [XmlIgnore]
        [NullableOrStringLengthValidator(true, 15, "Group Code", Ruleset = Constant.RULESET_LENGTH)]
        public string GroupCd
        {
            get { return _groupCd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _groupCd = value.Trim().ToUpper();
                else _groupCd = value;
            }
        }

        [XmlIgnore]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "AssignedToGroupDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? AssignedToGroupDt { get; set; }

        [XmlIgnore]
        public int? SentToAgencyId{get;set;}

        [XmlIgnore]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "SentToAgencyDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? SentToAgencyDt { get; set; }

        [XmlIgnore]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "SentToSurveyorDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? SentToSurveyorDt { get; set; }

        private string _rightPartyContactInd;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR1135)]
        public string RightPartyContactInd
        {
            get { return _rightPartyContactInd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _rightPartyContactInd = value.Trim().ToUpper();
                else _rightPartyContactInd = value;
            }
        }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "RpcMostRecentDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? RpcMostRecentDt { get; set; }

        [NullableOrStringLengthValidator(true, 300, "No Rpc Reason", Ruleset = Constant.RULESET_LENGTH)]
        public string NoRpcReason { get; set; }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "CounselingAcceptedDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? CounselingAcceptedDt { get; set; }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "CounselingScheduledDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? CounselingScheduledDt { get; set; }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "CounselingCompletedDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? CounselingCompletedDt { get; set; }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "CounselingRefusedDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? CounselingRefusedDt { get; set; }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "FirstCounseledDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? FirstCounseledDt { get; set; }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "SecondCounseledDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? SecondCounseledDt { get; set; }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "EdModuleCompletedDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? EdModuleCompletedDt { get; set; }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "InboundCallToNumDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? InboundCallToNumDt { get; set; }

        [NullableOrStringLengthValidator(true, 300, "InboundCallToNumReason", Ruleset = Constant.RULESET_LENGTH)]
        public string InboundCallToNumReason { get; set; }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "ActualCloseDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? ActualCloseDt { get; set; }

    }
}
