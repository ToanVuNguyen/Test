using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class EvalTemplateSectionDTO:BaseDTO
    {
        public int? EvalTemplateId { get; set; }
        public int? EvalSectionId { get; set; }
        public int? SectionOrder { get; set; }
        public EvalSectionDTO EvalSection { get; set; }
    }
}
