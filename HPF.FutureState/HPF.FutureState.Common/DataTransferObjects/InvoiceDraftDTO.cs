using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class InvoiceDraftDTO
    {
        public string FundingSourceId { get; set; }
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }
        public int TotalCases
        {
            get
            {
                return ForeclosureCaseDrafts.Count;
            }
        }
        public double TotalAmount
        {
            get
            {
                double total = 0;
                foreach (ForeclosureCaseDraftDTO fc in ForeclosureCaseDrafts)
                    total += fc.Amount;

                return total;
            }
        }
        public ForeclosureCaseDraftDTOCollection ForeclosureCaseDrafts { get; set; }
    }
}
