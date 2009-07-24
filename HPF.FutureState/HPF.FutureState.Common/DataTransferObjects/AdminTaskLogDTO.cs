using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class AdminTaskLogDTO: BaseDTO
    {
        public int AdminTaskLogId { get; set; }
        public string TaskName { get; set; }
        public int RecordCount { get; set; }
        public string FcIdList { get; set; }
        public string TaskNotes { get; set; }
    }
}
