using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class AppForeclosureCaseSearchResultDTOCollection:BaseDTOCollection<AppForeclosureCaseSearchResultDTO>
    {
        public int SearchResultCount { get; set; }
    }
}
