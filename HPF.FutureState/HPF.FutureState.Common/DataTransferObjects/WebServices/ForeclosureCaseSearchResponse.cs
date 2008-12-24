using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class ForeclosureCaseSearchResponse : BaseResponse
    {
        public ForeclosureCaseSearchResult Results { get; set; }
        public int SearchResultCount { get; set; }
    }
}
