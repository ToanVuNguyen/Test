using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class ForeclosureCaseSearchRequest : BaseRequest
    {
        public ForeclosureCaseSearchCriteriaDTO SearchCriteria { get; set; }
    }
}
