using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.Utils
{
    public class HPFPortalNewAgencyPayable
    {
        public byte[] ReportFile { get; set; }
        public string ReportFileName { get; set; }
        public string AgencyName { get; set;}
        public string PayableNumber { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? PayableDate{get;set;}
    }
}
