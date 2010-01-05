using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class MSARefDTO: BaseDTO
    {
        public int MSARefId { get; set; }
        public string MSACode { get; set; }
        public string MSAName { get; set; }
        public string MSAType { get; set; }
    }
}
