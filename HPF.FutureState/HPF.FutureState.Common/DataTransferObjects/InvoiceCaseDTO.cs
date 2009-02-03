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
        public int ForeclosureCaseId { get; set; }
        
        //For View/Edit Invoice UC
        public string AgencyCaseNum { get; set; }
        public string CaseCompleteDate { get; set; }
        public string LoanNumber { get; set; }
        public string ServicerName { get; set; }
        public string BorrowerName { get; set; }
        public string PaidDate { get; set; }
        public string InvenstorLoanId { get; set; }
    }
}
