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

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive,Tag="ERR127", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public int ServicerId { get; set; }

        public string OtherServicerName { get; set; }

        [NotNullValidator(Tag = "ERR128", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string AcctNum { get; set; }

        [NotNullValidator(Tag = "WARN320", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string Loan1st2nd { get; set; }

        [NotNullValidator(Tag = "WARN321", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string MortgageTypeCd { get; set; }        

        public string ArmResetInd { get; set; }

        [NotNullValidator(Tag = "WARN322", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string TermLengthCd { get; set; }

        [NotNullValidator(Tag = "WARN323", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string LoanDelinqStatusCd { get; set; }

        public decimal CurrentLoanBalanceAmt { get; set; }

        public decimal OrigLoanAmt { get; set; }

        [NotNullValidator(Tag = "WARN324", Ruleset = "Complete", MessageTemplate = "Required!")]
        public decimal InterestRate { get; set; }

        public string OriginatingLenderName { get; set; }

        public string OrigMortgageCoFdicNcusNum { get; set; }

        public string OrigMortgageCoName { get; set; }

        public string OrginalLoanNum { get; set; }

        public string FdicNcusNumCurrentServicerTbd { get; set; }

        public string CurrentServicerNameTbd { get; set; }

        public string InvestorLoanNum { get; set; }    

    }
}
