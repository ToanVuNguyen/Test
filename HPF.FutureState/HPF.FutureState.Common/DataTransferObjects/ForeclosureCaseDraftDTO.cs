using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class ForeclosureCaseDraftDTO: BaseDTO
    {
        public int ForeclosureCaseId { get; set; }
        public string AgencyCaseId { get; set; }
        public DateTime CompletedDate { get; set; }
        public decimal Amount { get; set; }
        public string AccountLoanNumber { get; set; }
        public string ServicerName { get; set; }
        public string BorrowerName { get; set; }

    }
}
