using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class AgencyPayableCaseDTO: BaseDTO
    {
        //public int AgencyPayableCaseId { get; set; }
        //public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public int ForeclosureCaseId { get; set; }
        
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int AgencyPayableId { get; set; }
        public string NFMCDiffererencePaidInd { get; set; }
        public string NFMCDifferenceEligibleInd{get;set;}
        //public string NFMCDifferenceInd { get; set; }
    }
}
