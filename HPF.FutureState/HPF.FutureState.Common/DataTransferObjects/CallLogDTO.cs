using System;

using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CallLogDTO : BaseDTO
    {

        #region property
        public int CallId { get; set; }

        [IgnoreNulls()]
        public int CallCenterID { get; set; }

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

        #region rem

        //public CallLogDTO(CallLogWSDTO callLog)
        //{
        //    if (callLog.CallId != null)
        //    {
        //        callLog.CallId = callLog.CallId.Replace("HPF_", "");
        //        int id = 0;
        //        int.TryParse(callLog.CallId, out id);
        //        this.CallId = id;
        //    }
        //    this.CallCenterID = callLog.CallCenterID;
        //    this.CcAgentIdKey = callLog.CcAgentIdKey;
        //    this.StartDate = callLog.StartDate;
        //    this.EndDate = callLog.EndDate;
        //    this.DNIS = callLog.DNIS;
        //    this.CallCenter = callLog.CallCenter;            
        //    this.CallSourceCd = callLog.CallSourceCd;
        //    this.ReasonToCall = callLog.ReasonToCall;
        //    this.LoanAccountNumber = callLog.LoanAccountNumber;
        //    this.FirstName = callLog.FirstName;
        //    this.LastName = callLog.LastName;              
        //    this.ServicerId = callLog.ServicerId;
        //    this.OtherServicerName = callLog.OtherServicerName;
        //    this.PropZipFull9 = callLog.PropZipFull9;            
        //    this.PrevAgencyId = callLog.PrevAgencyId;
        //    this.SelectedAgencyId = callLog.SelectedAgencyId;
        //    this.ScreenRout = callLog.ScreenRout;
        //    this.FinalDispoCd = callLog.FinalDispoCd;
        //    this.TransNumber = callLog.TransNumber;            
        //    this.CreateDate = callLog.CreateDate;
        //    this.CreateUserId = callLog.CreateUserId;
        //    this.CreateAppName = callLog.CreateAppName;
        //    this.ChangeLastDate = callLog.ChangeLastDate;
        //    this.ChangeLastUserId = callLog.ChangeLastUserId;
        //    this.ChangeLastAppName = callLog.ChangeLastAppName;
        //}

        //public CallLogDTO() { }
        #endregion
    }
}
