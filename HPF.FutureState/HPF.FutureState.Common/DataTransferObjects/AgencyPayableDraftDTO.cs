using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class AgencyPayableDraftDTO:BaseDTO
    {
        public int? AgencyId { get; set; }
        public DateTime? PeriodStartDate { get; set; }
        public DateTime? PeriodEndDate { get; set; }
        public int TotalCases 
        {
            get
            {
                return ForclosureCaseDrafts.Count;
            }
            set { int totalcases = value; }
        }
        public decimal? TotalAmount
        {
            get
            {
                decimal? total = 0;
                foreach (ForeclosureCaseDraftDTO fc in ForclosureCaseDrafts)
                    total += fc.Amount.Value;

                return total;
            }
            set {
                decimal? totalamount = value;
            }
        }
        public ForeclosureCaseDraftDTOCollection ForclosureCaseDrafts { get; set; }
    }
}
