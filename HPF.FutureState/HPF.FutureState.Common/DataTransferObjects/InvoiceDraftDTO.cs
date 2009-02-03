using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class InvoiceDraftDTO:BaseDTO
    {
        public string FundingSourceId { get; set; }
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }
        public int TotalCases
        {
            get
            {
                return ForeclosureCaseDrafts==null?0:ForeclosureCaseDrafts.Count;
            }
        }
        public decimal TotalAmount
        {
            get
            {
                decimal total = 0;
                if (ForeclosureCaseDrafts == null)
                    return total;
                foreach (ForeclosureCaseDraftDTO fc in ForeclosureCaseDrafts)
                    total += fc.Amount;

                return total;
            }
        }
        public ForeclosureCaseDraftDTOCollection ForeclosureCaseDrafts { get; set; }
        public InvoiceDraftDTO()
        {
            ForeclosureCaseDrafts = new ForeclosureCaseDraftDTOCollection();
        }
    }
}
