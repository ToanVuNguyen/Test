using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class AgencyPayableDTO: BaseDTO
    {
        
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }
        
        public string PayamentCode { get; set; }
        public string AccountLinkTBD { get; set; }
        public decimal AgencyPayablePaymentAmount { get; set; }
        //public int TotalCases { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentComment { get; set; }
        public string StatusCode { get; set; }
        public DateTime PaymentDate { get; set; }
        //---------------
        public int AgencyPayableId { get; set; }
        //public DateTime PaymentDate { get; set; }
        public AgencyPayableCaseDTOCollection AgencyPayableCases { get; set; }
    }
}
