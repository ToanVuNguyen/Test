using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class SendSummaryRequest : BaseRequest
    {
        [NullableOrStringLengthValidator(true, 30, "Sender Id", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0809)]
        public string SenderId { get; set; }

        [NullableOrStringLengthValidator(true, 255, "Email To Address", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0806)]
        public string EmailToAddress { get; set; }

        [NullableOrStringLengthValidator(true, 255, "Email Subject", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0807)]
        public string EmailSubject { get; set; }

        [NullableOrStringLengthValidator(true, 2000, "Email Body", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0808)]
        public string EmailBody { get; set; }

        public int? FCId { get; set; }
    }
}
