using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class CallLogWSDTO : BaseDTO
    {
        public string CallId { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset="Default", MessageTemplate="Maximum length is 30 characters")]
        public string ExtCallNumber { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "Maximum length is 30 characters")]
        public string AgencyId { get; set; }

        [IgnoreNulls()]
        public DateTime StartDate { get; set; }

        [IgnoreNulls()]
        public DateTime EndDate { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "Maximum length is 30 characters")]
        public string DNIS { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "Maximum length is 30 characters")]
        public string CallCenter { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(15, Ruleset = "Default", MessageTemplate = "Maximum length is 15 characters")]
        public string CallCenterCD { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "Maximum length is 30 characters")]
        public string CallResource { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(100, Ruleset = "Default", MessageTemplate = "Maximum length is 100 characters")]
        public string ReasonToCall { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(20, Ruleset = "Default", MessageTemplate = "Maximum length is 20 characters")]
        public string AccountNumber { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "Maximum length is 30 characters")]
        public string FirstName { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "Maximum length is 30 characters")]
        public string LastName { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(1, Ruleset = "Default", MessageTemplate = "Maximum length is 1 characters")]
        public string CounselPastYRInd { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(1, Ruleset = "Default", MessageTemplate = "Maximum length is 1 characters")]
        public string MtgProbInd { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(1, Ruleset = "Default", MessageTemplate = "Maximum length is 1 characters")]
        public string PastDueInd { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(1, Ruleset = "Default", MessageTemplate = "Maximum length is 1 characters")]
        public string PastDueSoonInd { get; set; }

        [IgnoreNulls()]
        public int PastDueMonths { get; set; }
        
        [IgnoreNulls()]
        public int ServicerId { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(50, Ruleset = "Default", MessageTemplate = "Maximum length is 50 characters")]
        public string OtherServicerName { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(20, Ruleset = "Default", MessageTemplate = "Maximum length is 20 characters")]
        public string PropZip { get; set; }

        [IgnoreNulls()]
        public int PrevCounselorId { get; set; }

        [IgnoreNulls()]
        public int PrevAgencyId { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(20, Ruleset = "Default", MessageTemplate = "Maximum length is 20 characters")]
        public string SelectedAgencyId { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "Maximum length is 30 characters")]
        public string ScreenRout { get; set; }

        [IgnoreNulls()]
        public int FinalDispo { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(20, Ruleset = "Default", MessageTemplate = "Maximum length is 20 characters")]
        public string TransNumber { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "Maximum length is 30 characters")]
        public string OutOfNetworkReferralTBD { get; set; }

        public CallLogWSDTO(CallLogDTO callLog)
        {
            if (callLog.CallId != null)
                this.CallId = "HPF_" + Convert.ToString(callLog.CallId);

            this.ExtCallNumber = callLog.ExtCallNumber;
            this.AgencyId = callLog.AgencyId;
            this.StartDate = callLog.StartDate;
            this.EndDate = callLog.EndDate;
            this.DNIS = callLog.DNIS;
            this.CallCenter = callLog.CallCenter;
            this.CallCenterCD = callLog.CallCenterCD;
            this.CallResource = callLog.CallResource;
            this.ReasonToCall = callLog.ReasonToCall;
            this.AccountNumber = callLog.AccountNumber;
            this.FirstName = callLog.FirstName;
            this.LastName = callLog.LastName;
            this.CounselPastYRInd = callLog.CounselPastYRInd;
            this.MtgProbInd = callLog.MtgProbInd;
            this.PastDueInd = callLog.PastDueInd;
            this.PastDueSoonInd = callLog.PastDueSoonInd;
            this.PastDueMonths = callLog.PastDueMonths;
            this.ServicerId = callLog.ServicerId;
            this.OtherServicerName = callLog.OtherServicerName;
            this.PropZip = callLog.PropZip;
            this.PrevCounselorId = callLog.PrevCounselorId;
            this.PrevAgencyId = callLog.PrevAgencyId;
            this.SelectedAgencyId = callLog.SelectedAgencyId;
            this.ScreenRout = callLog.ScreenRout;
            this.FinalDispo = callLog.FinalDispo;
            this.TransNumber = callLog.TransNumber;
            this.OutOfNetworkReferralTBD = callLog.OutOfNetworkReferralTBD;
            this.CreateDate = callLog.CreateDate;
            this.CreateUserId = callLog.CreateUserId;
            this.CreateAppName = callLog.CreateAppName;
            this.ChangeLastDate = callLog.ChangeLastDate;
            this.ChangeLastUserId = callLog.ChangeLastUserId;
            this.ChangeLastAppName = callLog.ChangeLastAppName;
        }

        public CallLogWSDTO() { }
    }
}
