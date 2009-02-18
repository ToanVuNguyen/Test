using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class SendSummaryRequest : BaseRequest
    {
        public string SenderId { get; set; }
        public string EmailToAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public int FCId { get; set; }
    }
}
