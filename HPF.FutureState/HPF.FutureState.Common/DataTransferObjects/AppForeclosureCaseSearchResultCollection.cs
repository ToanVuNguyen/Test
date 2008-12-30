using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class AppForeclosureCaseSearchResultDTOCollection:BaseDTOCollection<AppForeclosureCaseSearchResultDTO>
    {
        public double SearchResultCount
        {
            get;
            set;
        }
        
    }
}
