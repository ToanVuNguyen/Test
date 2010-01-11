using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class FundingSourceDTO:BaseDTO
    {
        string activeInd;
        public string ActiveIndicator 
        {
            get { return activeInd; }
            set { activeInd = string.IsNullOrEmpty(value) ? null : value.Trim().ToUpper(); }
        }
        public int FundingSourceID { get; set; }
        public string FundingSourceName { get; set; }
        
        //for the report
        public string FundingSourceAbbrev { get; set; }
        public string ExportFormatCd { get; set; }
        public string BillingDeliveryMethod { get; set; }
        public string BillingEmail { get; set; }
        public string SharePointFolder { get; set; }
        public DateTime? EffectedDt { get; set; }
        public DateTime? ExpiredDt { get; set; }
    }
}
