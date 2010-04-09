using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CompletedCounselingDetailReportCriteriaDTO: BaseDTO
    {
        public int AgencyId { get; set; }
        public int ProgramId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
