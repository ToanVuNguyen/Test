using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class CaseEvalDTO:BaseDTO
    {
        public CaseEvalHeaderDTO CaseEvalHeader { get; set; }
        public CaseEvalSetDTOCollection CaseEvalSets { get; set; }
    }
}
