using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CaseLoanDTO : BaseDTO
    {
        private const string RULE_SET_LENGTH = "Length";

        public int CaseLoanId { get; set; }

        public int FcId { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive,Tag="ERR127", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public int ServicerId { get; set; }

        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string OtherServicerName { get; set; }

        [StringRequiredValidator(Tag = "ERR128", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = RULE_SET_LENGTH)]
        public string AcctNum { get; set; }

        [StringRequiredValidator(Tag = "WARN320", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string Loan1st2nd { get; set; }

        [StringRequiredValidator(Tag = "WARN321", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string MortgageTypeCd { get; set; }

        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string ArmResetInd { get; set; }

        [StringRequiredValidator(Tag = "WARN322", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string TermLengthCd { get; set; }

        [StringRequiredValidator(Tag = "WARN323", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string LoanDelinqStatusCd { get; set; }

        public decimal CurrentLoanBalanceAmt { get; set; }

        public decimal OrigLoanAmt { get; set; }

        [NotNullValidator(Tag = "WARN324", Ruleset = "Complete", MessageTemplate = "Required!")]
        public decimal InterestRate { get; set; }

        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string OriginatingLenderName { get; set; }

        [NullableOrStringLengthValidator(true, 20, Ruleset = RULE_SET_LENGTH)]
        public string OrigMortgageCoFdicNcusNum { get; set; }

        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string OrigMortgageCoName { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = RULE_SET_LENGTH)]
        public string OrginalLoanNum { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = RULE_SET_LENGTH)]
        public string FdicNcusNumCurrentServicerTbd { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = RULE_SET_LENGTH)]
        public string CurrentServicerNameTbd { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = RULE_SET_LENGTH)]
        public string InvestorLoanNum { get; set; }    

    }
}
