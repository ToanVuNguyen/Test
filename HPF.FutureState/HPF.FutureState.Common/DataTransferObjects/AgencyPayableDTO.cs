using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class AgencyPayableDTO: BaseDTO
    {

        public int? AgencyId { get; set; }
        public string AgencyName { get; set; }
        public DateTime? PeriodStartDate { get; set; }
        public DateTime? PeriodEndDate { get; set; }
        public string AccountLinkTBD { get; set; }
        public double? AgencyPayablePaymentAmount { get; set; }
        public double? TotalAmount { get; set; }
        public string PaymentComment { get; set; }
        public string StatusCode { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string StatusDesc { get; set; }
        //---------------
        public int? AgencyPayableId { get; set; }
        //---------------view/edit agency payable
        public int? PayableNum { get; set; }
        public string SPFolderName { get; set; }
      
        public AgencyPayableCaseDTOCollection AgencyPayableCases { get; set; }
    }
}
