﻿using System;
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

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive,Tag=ErrorMessages.ERR127, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]        
        public int ServicerId { get; set; }

        [NullableOrStringLengthValidator(true, 50, Ruleset = Constant.RULESET_LENGTH)]
        public string OtherServicerName { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR128, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string AcctNum { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN320, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string Loan1st2nd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN321, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string MortgageTypeCd { get; set; }

        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string ArmResetInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN322, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string TermLengthCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN323, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string LoanDelinqStatusCd { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH)]
        public double CurrentLoanBalanceAmt { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH)]
        public double OrigLoanAmt { get; set; }

        [NotNullValidator(Tag = ErrorMessages.WARN324, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [RangeValidator(-99.999, RangeBoundaryType.Inclusive, 99.999, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH)]
        public double InterestRate { get; set; }

        [NullableOrStringLengthValidator(true, 50, Ruleset = Constant.RULESET_LENGTH)]
        public string OriginatingLenderName { get; set; }

        [NullableOrStringLengthValidator(true, 20, Ruleset = Constant.RULESET_LENGTH)]
        public string OrigMortgageCoFdicNcusNum { get; set; }

        [NullableOrStringLengthValidator(true, 50, Ruleset = Constant.RULESET_LENGTH)]
        public string OrigMortgageCoName { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string OrginalLoanNum { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string FdicNcusNumCurrentServicerTbd { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string CurrentServicerNameTbd { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string InvestorLoanNum { get; set; }    

    }
}
