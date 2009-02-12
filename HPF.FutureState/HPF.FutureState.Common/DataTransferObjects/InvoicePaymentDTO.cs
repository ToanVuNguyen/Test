using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class InvoicePaymentDTO : BaseDTO
    {
        public int? InvoicePaymentID { get; set; }
        public string FundingSourceName { get; set; }
        public string FundingSourceID { get; set; }
        public string PaymentNum { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentType { get; set; }
        public double? PaymentAmount { get; set; }
        public string Comments { get; set; }
    }
}
