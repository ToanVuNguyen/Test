using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace HPF.FutureState.Common.DataTransferObjects
{
    public class CaseAuditDTO : BaseDTO
    {
        public int? FcId { get; set; }
        public int? CaseAuditId { get; set; }
        public DateTime? AuditDt { get; set; }
        public string AuditTypeCode { get; set; }
        public string ReviewedBy { get; set; }
        public string CompliantInd { get; set; }
        public string AuditFailureReasonCode { get; set; }
        public string AuditComments { get; set; }
        public string ReasonForDefaultInd { get; set; }
        public string BudgetCompletedInd { get; set; }
        public string AppropriateOutcomeInd { get; set; }
        public string ClientActionPlanInd { get; set; }
        public string VerbalPrivacyConsentInd { get; set; }
        public string WrittenActionConsentInd { get; set; }
    }
}
