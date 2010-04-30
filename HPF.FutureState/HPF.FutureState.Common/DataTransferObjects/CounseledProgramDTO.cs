using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class CounseledProgramDTO: BaseDTO
    {
        public int CounseledProgramId { get; set; }
        public string CounseledProgramName { get; set; }
        public string CounseledProgramComment { get; set; }
        public int? ProgramId { get; set; }
        public DateTime? StartDt { get; set; }
        public DateTime? EndDt { get; set; }
    }
}
