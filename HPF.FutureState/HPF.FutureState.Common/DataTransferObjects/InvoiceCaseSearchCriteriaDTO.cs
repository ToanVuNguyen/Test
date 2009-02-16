using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class NumberRange
    {
        public int Min{get;set;}
        public int Max { get; set; }
    }
    public class DoubleRange
    {
        public Double Min { get; set; }
        public Double Max { get; set; }
    }
    [Serializable]
    public class InvoiceCaseSearchCriteriaDTO
    {
        public string FundingSourceId { get; set; }
        public int ServicerId { get; set; }
        public string ProgramId { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public CustomBoolean Duplicate { get; set; }
        public CustomBoolean Completed { get; set; }
        public CustomBoolean AlreadyBill { get; set; }
        public CustomBoolean IgnoreFundingConsent { get; set; }
        public int MaxNumOfCases { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public CustomBoolean Hispanic { get; set; }
        public NumberRange Age { get; set; }
        public DoubleRange HouseholdGrossAnnualIncome { get; set; }
        public string HouseholdCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool ServicerRejected { get; set; }
        public bool ServicerRejectedFreddie { get; set; }
        public bool NeighborworkRejected { get; set; }
        public bool SelectAllServicer { get; set; }
        public bool SelectUnfunded { get; set; }
        public InvoiceCaseSearchCriteriaDTO()
        {
            Age = new NumberRange();
            HouseholdGrossAnnualIncome = new DoubleRange();
        }
    }
}
