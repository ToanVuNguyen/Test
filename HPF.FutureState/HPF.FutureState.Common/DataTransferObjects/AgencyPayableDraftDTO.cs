using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class AgencyPayableDraftDTO
    {
        public int AgencyId { get; set; }        
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }
        public int TotalCases 
        {
            get
            {
                return ForclosureCaseDrafts.Count;
            }
        }
        public double TotalAmout
        {
            get
            {
                double total = 0;
                foreach (ForeclosureCaseDraftDTO fc in ForclosureCaseDrafts)
                    total += fc.Amount;

                return total;
            }
        }
        public ForeclosureCaseDraftDTOCollection ForclosureCaseDrafts { get; set; }
    }
}
