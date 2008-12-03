using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CaseLoanDTO : BaseDTO
    {
        public int CaseLoanId { get; set; }
        public int FcId { get; set; }
        public int ServicerId { get; set; }
        public string OtherServicerName { get; set; }
        public string AcctNum { get; set; }
        public string MortgageTypeCd { get; set; }
        public string ArmLoanInd { get; set; }
        public string ArmResetInd { get; set; }
        public string TermLengthCd { get; set; }
        public string LoanDelinqStatusCd { get; set; }
        public decimal CurrentLoanBalanceAmt { get; set; }
        public decimal OrigLoanAmt { get; set; }
        public decimal InterestRate { get; set; }
        public string OriginatingLenderName { get; set; }
        public string OrigMortgageCoFdicNcusNum { get; set; }
        public string OrigMortgageCoName { get; set; }
        public string OrginalLoanNum { get; set; }
        public string FdicNcusNumCurrentServicerTbd { get; set; }
        public string CurrentServicerNameTbd { get; set; }       
    }
}
