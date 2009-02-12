using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class AgencyPayableCaseDTO: BaseDTO
    {
        public string AgencyName { get; set; }
        public int? ForeclosureCaseId { get; set; }
        public DateTime? PeriodStartDate { get; set; }
        public DateTime? PeriodEndDate { get; set; }
        public double? PaymentAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? AgencyPayableId { get; set; }
        public string NFMCDifferenceEligibleInd {get;set;}
	    public DateTime? TakebackPmtIdentifiedDt {get;set;}
	    public string TakebackPmtReasonCd {get;set;}
        public double? NFMCDifferencePaidAmt { get; set; }
        //---------view/edit agency payable
        public int? AgencyCaseID { get; set; }
        public DateTime? CreateDt { get; set; }
        public DateTime? CompleteDt { get; set; }
        public string LoanNum { get; set; }
        public string ServicerName { get; set; }
        public string BorrowerName { get; set; }
        public string TakebackReason { get; set; }
        public DateTime? TakebackDate { get; set; }
    }
}
