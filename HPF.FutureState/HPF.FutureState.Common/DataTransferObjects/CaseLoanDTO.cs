using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CaseLoanDTO : BaseDTO
    {
        public int CaseLoanId { get; set; }

        public int FcId { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = "Min Request Validate", MessageTemplate = "Required!")]
        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = "Complete", MessageTemplate = "Required!")]        
        public int ServicerId { get; set; }

        public string OtherServicerName { get; set; }

        [NotNullValidator(Ruleset = "Min Request Validate", MessageTemplate = "Required!")]
        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string AcctNum { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string Loan1st2nd { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string MortgageTypeCd { get; set; }        

        public string ArmResetInd { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string TermLengthCd { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string LoanDelinqStatusCd { get; set; }

        public decimal CurrentLoanBalanceAmt { get; set; }

        public decimal OrigLoanAmt { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public double InterestRate { get; set; }

        public string OriginatingLenderName { get; set; }

        public string OrigMortgageCoFdicNcusNum { get; set; }

        public string OrigMortgageCoName { get; set; }

        public string OrginalLoanNum { get; set; }

        public string FdicNcusNumCurrentServicerTbd { get; set; }

        public string CurrentServicerNameTbd { get; set; }

        public string FreddieLoanNum { get; set; }    

    }
}
