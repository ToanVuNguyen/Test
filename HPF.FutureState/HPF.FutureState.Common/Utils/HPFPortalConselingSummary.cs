using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.Utils
{
    public class HPFPortalConselingSummary
    {
        public byte[] ReportFile { get; set; }

        public string ReportFileName { get; set; }

        public string LoanNumber { get; set; }

        public string Servicer { get; set; }

        public DateTime? CompletedDate { get; set; }

        public DateTime? ForeclosureSaleDate { get; set; }

        public string Delinquency { get; set; }        
    }
}
