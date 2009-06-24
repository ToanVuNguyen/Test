using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    [Serializable]
    public class CallLogSearchRequest: BaseRequest
    {
        public CallLogSearchCriteriaDTO SearchCriteria { get; set; }
    }
}
