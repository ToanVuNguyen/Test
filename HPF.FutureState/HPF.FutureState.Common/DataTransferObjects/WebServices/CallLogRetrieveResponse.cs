using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    [Serializable]
    public class CallLogRetrieveResponse : BaseResponse
    {
        public CallLogWSReturnDTO CallLog { get; set; }        
    }
}
