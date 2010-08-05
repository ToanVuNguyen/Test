using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class EvalSectionQuestionDTOCollection:BaseDTOCollection<EvalSectionQuestionDTO>
    {
        public EvalSectionQuestionDTO GetEvalSectionQuestionByQuestionId(int? questionId)
        {
            return this.SingleOrDefault(i => i.EvalQuestionId == questionId);
        }
    }
}
