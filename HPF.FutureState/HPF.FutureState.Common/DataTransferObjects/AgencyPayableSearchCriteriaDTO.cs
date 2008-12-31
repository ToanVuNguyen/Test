using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public enum CustomBoolean { None = -1, No = 0, Yes = 1 }        

    [Serializable]
    public class AgencyPayableSearchCriteriaDTO
    {        
        public int AgencyId { get; set; }
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }
        public CustomBoolean CaseComplete { get; set; }
        public CustomBoolean ServicerConsent { get; set; }
        public CustomBoolean FundingConsent { get; set; }
        public int MaxNumberOfCase { get; set; }
        public string LoanIndicator { get; set; }
    }
}
