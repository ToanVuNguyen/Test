using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class InvoiceCaseDTO: BaseDTO
    {
        public int InvoiceCaseId { get; set; }
        public int InvoicePaymentId { get; set; }
        public int InvoiceId { get; set; }
        public string PaymentRejectReasonCode { get; set; }
        public double InvoiceCasePaymentAmount { get; set; }
        public double InvoiceCaseBillAmount { get; set; }
        public string InDisputeIndicator { get; set; }
        public string RebuildIndicator { get; set; }
        public int IntentToPayFlagBTD { get; set; }
    }
}
