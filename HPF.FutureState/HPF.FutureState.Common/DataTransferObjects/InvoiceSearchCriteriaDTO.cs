using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
   
    public class InvoiceSearchCriteriaDTO
    {
        public int FundingSourceId { get; set; }        
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }        
    }
}
