using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CaseLoanDTO : BaseDTO
    {
        public int CaseLoanId { get; set; }

        public int FcId { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive,Tag=ErrorMessages.ERR0127, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]        
        public int ServicerId { get; set; }

        public string ServicerName { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Other Servicer Name", Ruleset = Constant.RULESET_LENGTH)]
        public string OtherServicerName { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0128, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, "Acct Num", Ruleset = Constant.RULESET_LENGTH)]
        public string AcctNum { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN0320, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Loan 1st 2nd ", Ruleset = Constant.RULESET_LENGTH)]
        public string Loan1st2nd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN0321, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Mortgage Type Code", Ruleset = Constant.RULESET_LENGTH)]
        public string MortgageTypeCd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string ArmResetInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN0322, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Term Length Code", Ruleset = Constant.RULESET_LENGTH)]
        public string TermLengthCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN0323, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Loan Delinq Status Code", Ruleset = Constant.RULESET_LENGTH)]
        public string LoanDelinqStatusCd { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "CurrentLoanBalanceAmt must be numeric(15,2)")]
        public double CurrentLoanBalanceAmt { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "OrigLoanAmt must be numeric(15,2)")]
        public double OrigLoanAmt { get; set; }

        [NotNullValidator(Tag = ErrorMessages.WARN0324, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [RangeValidator(-99.999, RangeBoundaryType.Inclusive, 99.999, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "InterestRate must be numeric(5,3)")]
        public double InterestRate { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Originating Lender Name", Ruleset = Constant.RULESET_LENGTH)]
        public string OriginatingLenderName { get; set; }

        [NullableOrStringLengthValidator(true, 20, "Original Mortgage Co FdicNcusNum", Ruleset = Constant.RULESET_LENGTH)]
        public string OrigMortgageCoFdicNcusNum { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Original Mortgage Co Name", Ruleset = Constant.RULESET_LENGTH)]
        public string OrigMortgageCoName { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Orginal Loan Number", Ruleset = Constant.RULESET_LENGTH)]
        public string OrginalLoanNum { get; set; }

        [NullableOrStringLengthValidator(true, 30, "FdicNcusNum Current Servicer Tbd", Ruleset = Constant.RULESET_LENGTH)]
        public string FdicNcusNumCurrentServicerTbd { get; set; }        

        [NullableOrStringLengthValidator(true, 30, "Investor Loan Number", Ruleset = Constant.RULESET_LENGTH)]
        public string InvestorLoanNum { get; set; }    

    }
}
