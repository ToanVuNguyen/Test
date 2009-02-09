using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class ReconciliationDTO:BaseDTO
    {
        public int ForeclosureCaseId { get; set; }
        public string LoanNumber { get; set; }
        public int InvoiceCaseId { get; set; }
        public double PaymentAmount { get; set; }
        public string PaymentRejectReasonCode { get; set; }
        public string FreddieMacLoanNumber {get;set;}
        public string InvestorNumber { get; set; }
        public string InvestorName { get; set; }
    }
}
