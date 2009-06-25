using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class OutcomeTypeDTO:BaseDTO
    {
        public int? OutcomeTypeID { get; set; }
        public string OutcomeTypeName { get; set; }
        public string OutcomeTypeComment { get; set; }
        public string PayableInd { get; set; }
        public DateTime? InactiveDt { get; set; }
    }
}
