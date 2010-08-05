using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CaseEvalHeaderDTO:BaseDTO
    {
        public int? CaseEvalHeaderId { get; set; }
        public int? FcId { get; set; }
        public int? AgencyId { get; set; }
        public int? EvalTemplateId { get; set; }

    }
}
