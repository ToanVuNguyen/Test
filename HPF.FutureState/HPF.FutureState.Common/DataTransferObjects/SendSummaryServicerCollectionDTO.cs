using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class SendSummaryServicerCollectionDTO: BaseDTOCollection<SendSummaryServicerDTO>
    {
        public SendSummaryServicerDTO GetServicerById(int? servicerId)
        {
            return this.SingleOrDefault(i => i.ServicerID == servicerId);
        }
    }
}
