using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class FundingSourceDTO:BaseDTO
    {
        public int FundingSourceID { get; set; }
        public string FundingSourceName { get; set; }
        
        //for the report
        public string FundingSourceAbbrev { get; set; }
        public string ExportFormatCd { get; set; }
        public string BillingDeliveryMethod { get; set; }
        public string BillingEmail { get; set; }
        public string SharePointFolder { get; set; }
    }
}
