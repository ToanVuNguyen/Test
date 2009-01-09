using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class InvoiceSearchResultDTO
    {
        public int FundingSourceId { get; set; }
        public string FundingSourceName { get; set; }
        public int InvoiceId { get; set; }
        public string InvoicePeriod { get; set; }
        public decimal InvoicePmtAmt { get; set; }
        public decimal InvoiceBillAmt { get; set; }
        public string StatusCd { get; set; }
        public string InvoiceComment { get; set; }
        public DateTime InvoiceDate { get; set; }

    }
}
