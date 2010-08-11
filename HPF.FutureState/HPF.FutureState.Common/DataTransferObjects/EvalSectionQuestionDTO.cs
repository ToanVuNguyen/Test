using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class EvalSectionQuestionDTO:BaseDTO
    {
        public int? EvalSectionId { get; set; }
        public int? EvalQuestionId { get; set; }
        public int? QuestionOrder { get; set; }
        public EvalQuestionDTO EvalQuestion { get; set; }
    }
}
