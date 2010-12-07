using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class ServicerApplicantDTO:BaseDTO
    {
        [XmlElement(IsNullable = true)]
        public int? ServicerApplicantId { get; set; }

        [XmlElement(IsNullable = true)]
        public int? ServicerId { get; set; }

        [RequiredObjectValidator(Tag = ErrorMessages.ERR1151, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "ReceivedDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? ReceivedDt { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1152, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "AcctNum", Ruleset = Constant.RULESET_LENGTH)]
        public string AcctNum { get; set; }

        [NullableOrStringLengthValidator(true, 30, "BorrowerFName", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerFName { get; set; }

        [NullableOrStringLengthValidator(true, 30, "BorrowerLName", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerLName { get; set; }

        [NullableOrStringLengthValidator(true, 30, "CoBorrowerFName", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerFName { get; set; }

        [NullableOrStringLengthValidator(true, 30, "CoBorrowerFName", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerLName { get; set; }

        [NullableOrStringLengthValidator(true, 50, "PropAddr1", Ruleset = Constant.RULESET_LENGTH)]
        public string PropAddr1 { get; set; }

        [NullableOrStringLengthValidator(true, 50, "PropAddr2", Ruleset = Constant.RULESET_LENGTH)]
        public string PropAddr2 { get; set; }

        [NullableOrStringLengthValidator(true, 30, "PropCity", Ruleset = Constant.RULESET_LENGTH)]
        public string PropCity { get; set; }

        [NullableOrStringLengthValidator(true, 15, "PropState", Ruleset = Constant.RULESET_LENGTH)]
        public string PropState { get; set; }

        [NullableOrStringLengthValidator(true, 5, "PropZip", Ruleset = Constant.RULESET_LENGTH)]
        public string PropZip { get; set; }

        [NullableOrStringLengthValidator(true, 50, "MailAddr1", Ruleset = Constant.RULESET_LENGTH)]
        public string MailAddr1 { get; set; }

        [NullableOrStringLengthValidator(true, 50, "MailAddr2", Ruleset = Constant.RULESET_LENGTH)]
        public string MailAddr2 { get; set; }

        [NullableOrStringLengthValidator(true, 30, "MailCity", Ruleset = Constant.RULESET_LENGTH)]
        public string MailCity { get; set; }

        [NullableOrStringLengthValidator(true, 15, "MailState", Ruleset = Constant.RULESET_LENGTH)]
        public string MailState { get; set; }

        [NullableOrStringLengthValidator(true, 5, "MailZip", Ruleset = Constant.RULESET_LENGTH)]
        public string MailZip { get; set; }

        [NullableOrStringLengthValidator(true, 20, "HomePhone", Ruleset = Constant.RULESET_LENGTH)]
        public string HomePhone { get; set; }

        [NullableOrStringLengthValidator(true, 20, "WorkPhone", Ruleset = Constant.RULESET_LENGTH)]
        public string WorkPhone { get; set; }

        [NullableOrStringLengthValidator(true, 50, "EmailAddr", Ruleset = Constant.RULESET_LENGTH)]
        public string EmailAddr { get; set; }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "ScheduledCloseDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? ScheduledCloseDt { get; set; }

        [NullableOrStringLengthValidator(true, 8000, "Comments", Ruleset = Constant.RULESET_LENGTH)]
        public string Comments { get; set; }
    }
}
