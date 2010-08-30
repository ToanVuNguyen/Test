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
        //Determine if section has questions or not in one template
        public bool IsInUse { get; set; }
        //Determine status of this: is insert, remove or update
        //Use in ManageEvalTemplate page
        public byte StatusChanged { get; set; }
        public EvalSectionDTO EvalSection { get; set; }
    }
}
