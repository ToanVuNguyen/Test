using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace HPF.FutureState.Common.DataTransferObjects
{
    public class CaseFollowUpDTO : BaseDTO
    {
        public int? FcId { get; set; }

        public int? CasePostCounselingStatusId { get; set; }

        public int? OutcomeTypeId { get; set; } 

        public DateTime? FollowUpDt { get; set; }

        public string FollowUpComment { get; set; }

        public string FollowupSourceCd { get; set; }

        public string LoanDelinqStatusCd { get; set; }

        public string StillInHouseInd { get; set; }

        public string CreditScore { get; set; }

        public string CreditBureauCd { get; set; }

        public DateTime? CreditReportDt { get; set; }        
    }
}
