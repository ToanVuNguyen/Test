using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class FundingSourceDTO:BaseDTO
    {
        public int FundingSourceID { get; set; }
        public string FundingSourceName { get; set; }
    }
}
