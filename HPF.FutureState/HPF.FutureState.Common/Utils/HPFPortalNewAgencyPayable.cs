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
        public string FundingSource { get; set;}
        public string InvoiceNumber { get; set; }
        public DateTime? Date { get; set; }
    }
}
