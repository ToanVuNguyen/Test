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
        #region property
        public string CallId { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "AgencyId's Maximum length is 30 characters")]
        public string CcAgentIdKey { get; set; }

        [IgnoreNulls()]
        public DateTime StartDate { get; set; }

        [IgnoreNulls()]
        public DateTime EndDate { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "DNIS's Maximum length is 30 characters")]
        public string DNIS { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "CallCenter's Maximum length is 30 characters")]
        public string CallCenter { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "CallResource's Maximum length is 30 characters")]
        public string CallSourceCd { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(100, Ruleset = "Default", MessageTemplate = " ReasonToCall's Maximum length is 100 characters")]
        public string ReasonToCall { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(20, Ruleset = "Default", MessageTemplate = "AccountNumber's Maximum length is 20 characters")]
        public string LoanAccountNumber { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "FirstName's Maximum length is 30 characters")]
        public string FirstName { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "LastName's Maximum length is 30 characters")]
        public string LastName { get; set; }

        [IgnoreNulls()]
        public int ServicerId { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(50, Ruleset = "Default", MessageTemplate = "OtherServicerName's Maximum length is 50 characters")]
        public string OtherServicerName { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(20, Ruleset = "Default", MessageTemplate = "PropZip's Maximum length is 20 characters")]
        public string PropZipFull9 { get; set; }

        [IgnoreNulls()]
        public int PrevAgencyId { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(20, Ruleset = "Default", MessageTemplate = "SelectedAgencyId's Maximum length is 20 characters")]
        public string SelectedAgencyId { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "ScreenRout's Maximum length is 30 characters")]
        public string ScreenRout { get; set; }

        [IgnoreNulls()]
        public int FinalDispoCd { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(20, Ruleset = "Default", MessageTemplate = "TransNumber's Maximum length is 20 characters")]
        public string TransNumber { get; set; }

        [IgnoreNulls()]
        public string CcCallKey { get; set; }

        [IgnoreNulls()]
        public string LoanDelinqStatusCd { get; set; }

        [IgnoreNulls()]
        public string SelectedCounselor { get; set; }

        [IgnoreNulls()]
        public string HomeownerInd { get; set; }

        [IgnoreNulls()]
        public string PowerOfAttorneyInd { get; set; }

        [IgnoreNulls()]
        public string AuthorizedInd { get; set; }
        #endregion

        public CallLogWSDTO(CallLogDTO callLog)
        {
            if (callLog.CallId != null)
                this.CallId = "HPF_" + Convert.ToString(callLog.CallId);

            
            this.CcAgentIdKey = callLog.CcAgentIdKey;
            this.StartDate = callLog.StartDate;
            this.EndDate = callLog.EndDate;
            this.DNIS = callLog.DNIS;
            this.CallCenter = callLog.CallCenter;            
            this.CallSourceCd = callLog.CallSourceCd;
            this.ReasonToCall = callLog.ReasonToCall;
            this.LoanAccountNumber = callLog.LoanAccountNumber;
            this.FirstName = callLog.FirstName;
            this.LastName = callLog.LastName;
            this.ServicerId = callLog.ServicerId;
            this.OtherServicerName = callLog.OtherServicerName;
            this.PropZipFull9 = callLog.PropZipFull9; 
            this.PrevAgencyId = callLog.PrevAgencyId;
            this.SelectedAgencyId = callLog.SelectedAgencyId;
            this.ScreenRout = callLog.ScreenRout;
            this.FinalDispoCd = callLog.FinalDispoCd;
            this.TransNumber = callLog.TransNumber;            
            this.CreateDate = callLog.CreateDate;
            this.CreateUserId = callLog.CreateUserId;
            this.CreateAppName = callLog.CreateAppName;
            this.ChangeLastDate = callLog.ChangeLastDate;
            this.ChangeLastUserId = callLog.ChangeLastUserId;
            this.ChangeLastAppName = callLog.ChangeLastAppName;
            this.CcCallKey = callLog.CcCallKey;
            this.LoanDelinqStatusCd = callLog.LoanDelinqStatusCd;
            this.SelectedCounselor = callLog.SelectedCounselor;
            this.HomeownerInd = callLog.HomeownerInd;
            this.PowerOfAttorneyInd = callLog.PowerOfAttorneyInd;
            this.AuthorizedInd = callLog.AuthorizedInd;
        }

        public CallLogWSDTO() { }
    }
}
