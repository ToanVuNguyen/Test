using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.Utils
{
    public class HPFPortalInvoice
    {
        public byte[] File { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public string FundingSource { get; set; }
        public string InvoiceNumber { get; set; }
        public string FileName { get; set; }
        public string InvoiceFolderName { get; set; }
    }
}
