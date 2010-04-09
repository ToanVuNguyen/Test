using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.Utils
{
    public class HPFPortalCompletedCounselingDetail
    {
        public byte[] ReportFile { get; set; }
        public string ReportFilename { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SPFolderName { get; set; }
    }
}
