using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BillingInfoDTO : BaseDTO
    {
        public DateTime InvoiceDate { get; set; }
        public string FundingSourceName { get; set; }
        public int InvoiceId { get; set; }
        public string Loan { get; set; }
        public string InDisputeIndicator { get; set; }
        public double InvoiceCaseBillAmount { get; set; }
        public double InvoiceCasePaymentAmount { get; set; }
        public DateTime PaidDate { get; set; }
        public string PaymentRejectReasonCode { get; set; }
        
    }
}