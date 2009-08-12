using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class SendSummaryServicerDTO: ServicerDTO
    {
        public string Description { get; set; }
        public string SummaryDeliveryMethodDesc { get; set; }
    }
}
