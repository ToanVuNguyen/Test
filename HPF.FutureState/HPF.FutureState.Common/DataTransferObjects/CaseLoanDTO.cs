﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CaseLoanDTO : BaseDTO
    {
        [XmlElement(IsNullable=true)]
        [XmlIgnore]
        public int? CaseLoanId { get; set; }

        [XmlElement(IsNullable = true)]
        [XmlIgnore]
        public int? FcId { get; set; }

        [XmlElement(IsNullable = true)]        
        //[RequiredObjectValidator(Tag = ErrorMessages.ERR0127, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public int? ServicerId { get; set; }

        [XmlIgnore]
        public string ServicerName { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Other Servicer Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0057)]
        public string OtherServicerName { get; set; }

        string _acctNum = null;
        [StringRequiredValidator(Tag = ErrorMessages.ERR0128, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "Acct Num", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0058)]        
        public string AcctNum
        {
            get { return _acctNum; }
            set
            {
                if (value != null)
                {
                    _acctNum = value;
                    Regex exp = new Regex(@"[^a-zA-Z0-9]");
                    MatchCollection matches = exp.Matches(_acctNum);
                    foreach (Match item in matches)
                    {
                        _acctNum = _acctNum.Replace(item.Value, string.Empty);
                    }

                }
            }
        }

        //[StringRequiredValidator(Tag = ErrorMessages.WARN0320, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        string _loan1st2nd;
        [NullableOrStringLengthValidator(true, 15, "Loan 1st 2nd ", Ruleset = Constant.RULESET_LENGTH)]
        public string Loan1st2nd
        {
            get { return _loan1st2nd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _loan1st2nd = value.Trim().ToUpper();
                else _loan1st2nd = value;
            }
        }

        string _mortgageTypeCd;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0321, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Mortgage Type Code", Ruleset = Constant.RULESET_LENGTH)]
        public string MortgageTypeCd 
        {
            get { return _mortgageTypeCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _mortgageTypeCd = value.Trim().ToUpper();
                else _mortgageTypeCd = value;
            }
        }

        private string armResetInd = null;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0059)]
        public string ArmResetInd
        {
            get { return armResetInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    armResetInd = value.Trim();
                else armResetInd = value;
            }
        }

        string _termLengthCd;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0322, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Term Length Code", Ruleset = Constant.RULESET_LENGTH)]
        public string TermLengthCd
        {
            get { return _termLengthCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _termLengthCd = value.Trim().ToUpper();
                else _termLengthCd = value;
            }
        }

        string _loanDelinqStatusCd;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0323, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Loan Delinq Status Code", Ruleset = Constant.RULESET_LENGTH)]
        public string LoanDelinqStatusCd
        {
            get { return _loanDelinqStatusCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _loanDelinqStatusCd = value.Trim().ToUpper();
                else _loanDelinqStatusCd = value;
            }
        }
        [XmlElement(IsNullable = true)]
        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0076)]
        public double? CurrentLoanBalanceAmt { get; set; }

        [XmlElement(IsNullable = true)]
        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0077)]
        public double? OrigLoanAmt { get; set; }

        [XmlElement(IsNullable = true)]
        [NotNullValidator(Tag = ErrorMessages.WARN0324, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        private double? _InterestRate;
        [NullableOrInRangeNumberValidator(true, "0", "99.999", Ruleset = Constant.RULESET_LENGTH, Tag= ErrorMessages.ERR0395)]
        public double? InterestRate
        {
            get { return _InterestRate; }
            set
            {
                _InterestRate = value;
                if (value != null)
                    _InterestRate = value / 100;
            }
        }

        [NullableOrStringLengthValidator(true, 50, "Originating Lender Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0060)]
        public string OriginatingLenderName { get; set; }

        [NullableOrStringLengthValidator(true, 20, "Original Mortgage Co FdicNcusNum", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0061)]
        public string OrigMortgageCoFdicNcusNum { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Original Mortgage Co Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0062)]
        public string OrigMortgageCoName { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Orginal Loan Number", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0063)]
        public string OrginalLoanNum { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Current Servicer FdicNcuaNum", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0064)]
        public string CurrentServicerFdicNcuaNum { get; set; }

        [XmlIgnore]
        [NullableOrStringLengthValidator(true, 30, "Investor Loan Number", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0065)]
        public string InvestorLoanNum { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Investor Number", Ruleset = Constant.RULESET_LENGTH)]
        public string InvestorNum { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Investor Name", Ruleset = Constant.RULESET_LENGTH)]
        public string InvestorName { get; set; }

        string _mortgateProgramCd;
        [StringRequiredValidator(Tag=ErrorMessages.WARN0325, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "One or more MortgageProgramCd required to complete a foreclosure case.")]
        [NullableOrStringLengthValidator(true, 15, "Mortgage Program Code", Ruleset = Constant.RULESET_LENGTH)]
        public string MortgageProgramCd
        { 
            get { return _mortgateProgramCd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _mortgateProgramCd = value.Trim().ToUpper();
                else _mortgageTypeCd = value;
            }
        }

        [XmlIgnore]
        public string ChangedAcctNum { get; set; }

        [XmlIgnore]
        public string LoanDelinquencyDesc { get; set; }

        [XmlIgnore]
        public string TermLengthDesc { get; set; }

        [XmlIgnore]
        public string MortgageTypeCdDesc { get; set; }

        [XmlIgnore]
        public string MortgageProgramCdDesc { get; set; }

    }
}
