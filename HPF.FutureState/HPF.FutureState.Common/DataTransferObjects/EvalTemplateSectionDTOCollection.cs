using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class EvalTemplateSectionDTOCollection:BaseDTOCollection<EvalTemplateSectionDTO>
    {
        public EvalTemplateSectionDTO GetEvalTemplateSectionByEvalSectionId(int? evalSectionId)
        {
            return this.SingleOrDefault(i => i.EvalSectionId == evalSectionId);
        }
    }
}
