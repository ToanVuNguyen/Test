using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class CaseAuditDTO : BaseDTO
    {
        public int? FcId { get; set; }
        public int? CaseAuditId { get; set; }

        [NullableOrInRangeNumberValidator(false, "1-1-1753", "12-31-9999", Ruleset = "Default", Tag=ErrorMessages.ERR0701)]
        public DateTime? AuditDt { get; set; }

        [NullableOrStringLengthValidator(false, 15, "AuditTypeCode", Ruleset = "Default", Tag = ErrorMessages.ERR0702)]
        public string AuditTypeCode { get; set; }

        
        [NullableOrStringLengthValidator(true, 30, "ReviewedBy", Ruleset = "Default")]
        public string ReviewedBy { get; set; }

        [NullableOrStringLengthValidator(true, 15, "AuditFailureReasonCode", Ruleset = "Default")]
        public string AuditFailureReasonCode { get; set; }

        [NullableOrStringLengthValidator(true, 300, "AuditComments", Ruleset = "Default")]
        public string AuditComments { get; set; }

        
        [YesNoIndicatorValidator(true, Ruleset = "Defalut")]        
        public string CompliantInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = "Defalut")]        
        public string ReasonForDefaultInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = "Defalut")]        
        public string BudgetCompletedInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = "Defalut")]        
        public string AppropriateOutcomeInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = "Defalut")]        
        public string ClientActionPlanInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = "Defalut")]        
        public string VerbalPrivacyConsentInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = "Defalut")]        
        public string WrittenActionConsentInd { get; set; }

        public bool Equals(CaseAuditDTO caseAudit)
        {
            if (FcId != caseAudit.FcId
                || CaseAuditId != caseAudit.CaseAuditId
                || AuditDt != caseAudit.AuditDt
                || AuditTypeCode != caseAudit.AuditTypeCode
                || ReviewedBy != caseAudit.ReviewedBy
                || AuditFailureReasonCode != caseAudit.AuditFailureReasonCode
                || AuditComments != caseAudit.AuditComments
                || CompliantInd != caseAudit.CompliantInd
                || ReasonForDefaultInd != caseAudit.ReasonForDefaultInd
                || BudgetCompletedInd != caseAudit.BudgetCompletedInd
                || AppropriateOutcomeInd != caseAudit.AppropriateOutcomeInd
                || ClientActionPlanInd != caseAudit.ClientActionPlanInd
                || VerbalPrivacyConsentInd != caseAudit.VerbalPrivacyConsentInd
                || WrittenActionConsentInd != caseAudit.WrittenActionConsentInd) 
                return false;
                
            return true;
        }

        public bool IsNull()
        {
            if (CaseAuditId.HasValue
                || AuditDt.HasValue
                || !string.IsNullOrEmpty(AuditTypeCode)
                || !string.IsNullOrEmpty(ReviewedBy)
                || !string.IsNullOrEmpty(AuditFailureReasonCode)
                || !string.IsNullOrEmpty(AuditComments)
                || !string.IsNullOrEmpty(CompliantInd)
                || !string.IsNullOrEmpty(ReasonForDefaultInd)
                || !string.IsNullOrEmpty(BudgetCompletedInd)
                || !string.IsNullOrEmpty(AppropriateOutcomeInd)
                || !string.IsNullOrEmpty(ClientActionPlanInd)
                || !string.IsNullOrEmpty(VerbalPrivacyConsentInd)
                || !string.IsNullOrEmpty(WrittenActionConsentInd))
                return false;
            return true;
        }
    }
}
