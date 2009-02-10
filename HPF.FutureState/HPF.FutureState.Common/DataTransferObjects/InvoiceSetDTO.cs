using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public enum InvoiceCaseUpdateFlag { Reject = 0, Unpay = 1, Pay = 2 }
    [Serializable]
    public class InvoiceSetDTO:BaseDTO
    {
        public InvoiceDTO Invoice { get; set; }
        public InvoiceCaseDTOCollection InvoiceCases { get; set; }
        //use for Update InvoiceCase
        public string PaymentRejectReason { get; set; }
        public int InvoicePaymentId { get; set; }

        public InvoiceSetDTO()
        {
            InvoiceCases = new InvoiceCaseDTOCollection();
        }
        public int TotalCases
        {
            get { return InvoiceCases.Count; }
        }
        public double InvoiceTotal
        {
            get
            {
                if (TotalCases == 0)
                    return 0;
                double sum = 0;
                foreach (var invoiceCase in InvoiceCases)
                    sum += invoiceCase.InvoiceCaseBillAmount == null ? 0 : invoiceCase.InvoiceCaseBillAmount.Value;
                return sum;
            }
        }
        public double TotalPaid
        {
            get
            {
                if (TotalCases == 0)
                    return 0;
                double sum = 0;
                foreach (var invoiceCase in InvoiceCases)
                    sum += invoiceCase.InvoiceCasePaymentAmount == null ? 0 : invoiceCase.InvoiceCasePaymentAmount.Value;
                return sum;
            }
        }
        public double TotalRejected
        {
            get
            {
                if (TotalCases == 0)
                    return 0;
                double sum = 0;
                foreach (var invoiceCase in InvoiceCases)
                    if(invoiceCase.PaymentRejectReasonCode!=null)
                        sum += invoiceCase.InvoiceCaseBillAmount == null ? 0 : invoiceCase.InvoiceCaseBillAmount.Value;
                return sum;
            }
        }
    }
}
