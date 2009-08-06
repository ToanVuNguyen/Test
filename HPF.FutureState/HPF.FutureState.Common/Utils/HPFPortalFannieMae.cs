using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.Utils
{    
    public class HPFPortalFannieMae
    {
        public byte[] ReportFile { get; set; }
        public string ReportFileName { get; set; }        
        public string SPFolderName { get; set; }
        public DateTime? StartDt { get; set; }
        public DateTime? EndDt { get; set; }
    }
}
