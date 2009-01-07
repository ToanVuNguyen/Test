using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class AgencyPayableCaseDTO: BaseDTO
    {
        public int AgencyPayableCaseId { get; set; }
        public int ForeclosureCaseId { get; set; }
        public int AgencyPayableId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string NFMCDiffererencePaidIndicator { get; set; }
    }
}
