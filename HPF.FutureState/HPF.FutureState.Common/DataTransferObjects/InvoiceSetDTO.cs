using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class InvoiceSetDTO:BaseDTO
    {
        public InvoiceDTO Invoice { get; set; }
        public InvoiceCaseDTOCollection InvoiceCases { get; set; }
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
                    sum += invoiceCase.InvoiceCaseBillAmount;
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
                    sum += invoiceCase.InvoiceCasePaymentAmount;
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
                        sum += invoiceCase.InvoiceCaseBillAmount;
                return sum;
            }
        }
    }
}
