using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public struct NumberRange
    {
        int Min;
        int Max;
    }
    public class InvoiceCaseSearchCriteriaDTO
    {
        public int FundingSourceId { get; set; }
        public int ServicerId { get; set; }
        public int ProgramId { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public CustomBoolean Duplicate { get; set; }
        public CustomBoolean CompleteCase { get; set; }
        public CustomBoolean AlreadyBill { get; set; }
        public CustomBoolean ServicerConsent { get; set; }
        public CustomBoolean FundingConsent { get; set; }
        public int MaxNumOfCases { get; set; }
        public string LoanIndicator { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public CustomBoolean Ethnicity { get; set; }
        public NumberRange Age { get; set; }
        public NumberRange HouseholdGrossAnnualIncome { get; set; }
        public string HouseholdCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
