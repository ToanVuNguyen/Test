using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using HPF.FutureState.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class EventDTO : BaseDTO
    {
        [XmlIgnore]
        public int? EventId { get; set; }

        [XmlElement(IsNullable = true)]
        [RequiredObjectValidator(Tag = ErrorMessages.ERR1208, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public int? FcId { get; set; }
        
        [XmlElement (IsNullable=true)]
        [RequiredObjectValidator(Tag = ErrorMessages.ERR1209, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public int? ProgramStageId { get; set; }

        private string _eventTypeCd;
        [StringRequiredValidator(Tag = ErrorMessages.ERR1200, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Event Type Code", Ruleset = Constant.RULESET_LENGTH)]
        public string EventTypeCd
        {
            get { return _eventTypeCd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _eventTypeCd = value.Trim().ToUpper();
                else _eventTypeCd = value;
            }
        }

        [RequiredObjectValidator(Tag = ErrorMessages.ERR1201, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "EventDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? EventDt { get; set; }

        private string _rpcInd;
        [StringRequiredValidator(Tag = ErrorMessages.ERR1202, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR1203)]        
        public string RpcInd
        {
            get { return _rpcInd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _rpcInd = value.ToUpper().Trim();
                else _rpcInd = value;
            }
        }

        private string _eventOutcomeCd;
        [StringRequiredValidator(Tag = ErrorMessages.ERR1200, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Event Outcome Code", Ruleset = Constant.RULESET_LENGTH)]
        public string EventOutcomeCd
        {
            get { return _eventOutcomeCd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _eventOutcomeCd = value.Trim().ToUpper();
                else _eventOutcomeCd = value;
            }
        }

        private string _completedInd;
        [StringRequiredValidator(Tag = ErrorMessages.ERR1205, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR1206)]        
        public string CompletedInd
        {
            get { return _completedInd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _completedInd = value.ToUpper().Trim();
                else _completedInd= value;
            }
        }

        private string _counselorIdRef;
        [StringRequiredValidator(Tag = ErrorMessages.ERR1200, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "CounselorIdRef", Ruleset = Constant.RULESET_LENGTH)]
        public string CounselorIdRef
        {
            get { return _counselorIdRef; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _counselorIdRef = value.Trim();
                else _counselorIdRef = value;
            }
        }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "ProgramRefusalDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? ProgramRefusalDt { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1216, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 30, "Change Last User ID", Ruleset = Constant.RULESET_LENGTH)]
        public string ChgLstUserId { get; set; }
    }
}
