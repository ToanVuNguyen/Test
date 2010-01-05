using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class InvoiceCaseSearchCriteriaDTO
    {
        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_INVOICE_CASE_VALIDATION, Tag = ErrorMessages.ERR0561)]
        public int FundingSourceId { get; set; }
        public int ServicerId { get; set; }
        public string ProgramId { get; set; }
        [NullableOrInRangeNumberValidator(false, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_INVOICE_CASE_VALIDATION, Tag = ErrorMessages.ERR0562)]
        public DateTime PeriodStart { get; set; }
        [NullableOrInRangeNumberValidator(false, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_INVOICE_CASE_VALIDATION, Tag = ErrorMessages.ERR0563)]
        public DateTime PeriodEnd { get; set; }
        public CustomBoolean Duplicate { get; set; }
        public CustomBoolean Completed { get; set; }
        public CustomBoolean AlreadyBill { get; set; }
        public CustomBoolean IgnoreFundingConsent { get; set; }
        [NullableOrInRangeNumberValidator(false, "-1", "65000", Ruleset = Constant.RULESET_INVOICE_CASE_VALIDATION, Tag = ErrorMessages.ERR0569)]
        public int MaxNumOfCases { get; set; }

        public string Gender { get; set; }
        public string Race { get; set; }
        public CustomBoolean Hispanic { get; set; }
        [NullableOrInRangeNumberValidator(false,"-1", "200",  Ruleset = Constant.RULESET_INVOICE_CASE_VALIDATION, Tag = ErrorMessages.ERR0570)]
        public int AgeMin { get; set; }
        [NullableOrInRangeNumberValidator(false,"-1",  "200",  Ruleset = Constant.RULESET_INVOICE_CASE_VALIDATION, Tag = ErrorMessages.ERR0571)]
        public int AgeMax { get; set; }
        [NullableOrInRangeNumberValidator(false, "-1", "99,999,999.99", Ruleset = Constant.RULESET_INVOICE_CASE_VALIDATION, Tag = ErrorMessages.ERR0572)]
        public double HouseholdGrossAnnualIncomeMin { get; set; }
        [NullableOrInRangeNumberValidator(false, "-1", "99,999,999.99", Ruleset = Constant.RULESET_INVOICE_CASE_VALIDATION, Tag = ErrorMessages.ERR0573)]
        public double HouseholdGrossAnnualIncomeMax { get; set; }
        public string HouseholdCode { get; set; }
        [NullableOrStringLengthValidator(true, 30, "City", Ruleset = Constant.RULESET_INVOICE_CASE_VALIDATION, MessageTemplate = "City has a maximum length of 30 characters.")]
        public string City { get; set; }
        public string State { get; set; }
        public bool ServicerRejected { get; set; }
        public bool ServicerRejectedFreddie { get; set; }
        public bool NeighborworkRejected { get; set; }
        public bool SelectAllServicer { get; set; }
        public bool SelectUnfunded { get; set; }
        public int ServicerConsentQty { get; set; }

        public bool UnableToLocateLoanInPortfolio { get; set; }
        public bool InvalidCounselingDocument { get; set; }

        public int? AgencyId { get; set; }
        public string MSACode { get; set; }

        public InvoiceCaseSearchCriteriaDTO()
        {
            
        }
    }
}
