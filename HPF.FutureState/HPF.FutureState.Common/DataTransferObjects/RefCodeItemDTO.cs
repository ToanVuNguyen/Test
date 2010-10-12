using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class RefCodeItemDTO : BaseDTO
    {
        [StringRequiredValidator(Tag = ErrorMessages.ERR1120, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public string CodeValue { get; set; }
        [StringRequiredValidator(Tag = ErrorMessages.ERR1121, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public string CodeDescription { get; set; }
        [XmlIgnore]
        [StringRequiredValidator(Tag = ErrorMessages.ERR1119, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public string RefCodeSetName { get; set; }
        [XmlIgnore]
        public string CodeComment { get; set; }
        [XmlIgnore]
        public int? RefCodeItemId { get; set; }                                        
        [XmlIgnore]
        [RequiredObjectValidator(Tag = ErrorMessages.ERR1122, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public int? SortOrder { get; set; }

        string activeInd;
        [XmlIgnore]
        public string ActiveInd {
            get { return activeInd; }
            set { activeInd = string.IsNullOrEmpty(value) ? null : value.Trim().ToUpper(); }
        }
        //[XmlIgnore]
        //public string AgencyUsageInd { get; set; }
    }
}
