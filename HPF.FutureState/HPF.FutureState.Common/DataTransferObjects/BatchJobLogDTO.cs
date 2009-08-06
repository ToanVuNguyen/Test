using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public enum Status{FAIL, SUCCESS};

    [Serializable]
    public class BatchJobLogDTO : BaseDTO
    {
        public int BatchJobLogId { get; set; }
        public int BatchJobId { get; set; }
        public Status JobResult { get; set; }
        public int RecordCount { get; set; }
        public string JobNotes { get; set; }
    }
}
