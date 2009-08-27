using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class RefCodeSetDTO: BaseDTO
    {
        public string RefCodeSetName { get; set; }
        public string CodeSetComment { get; set; }
        public string AgencyUsageInd { get; set; }
    }
}
