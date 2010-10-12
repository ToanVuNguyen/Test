using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class RefCodeSearchCriteriaDTO: BaseDTO
    {
        public string CodeSetName { get; set; }
        public bool? IncludedInActive { get; set; }
    }
}
