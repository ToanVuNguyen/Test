using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class ForeclosureCaseSearchResult:BaseDTOCollection <ForeclosureCaseWSDTO>
    {
        public int SearchResultCount { get; set; } 
    }
}
