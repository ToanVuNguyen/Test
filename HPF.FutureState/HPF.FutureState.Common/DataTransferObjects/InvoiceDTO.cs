using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class InvoiceDTO: BaseDTO
    {
        public int InvoiceId { get; set; }
        public int FundingSourceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceCode { get; set; }
        public string StatusCode { get; set; }
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }
        public string InvoiceComment { get; set; }
        public string AccountingLinkBTD { get; set; }
        public double InvoicePaymentAmount { get; set; }
        public double InvoiceBillAmount { get; set; }
        public string FundingSourceName { get; set; }
        public string InvoicePeriod { get; set; }
        public InvoiceCaseDTOCollection InvoiceCases { get; set; }

        
    }
}
