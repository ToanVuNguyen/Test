using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    [Serializable]
    public class CallLogSearchResponse: BaseResponse
    {
        public CallLogWSReturnDTOCollection CallLogs { get; set; }
    }
}
