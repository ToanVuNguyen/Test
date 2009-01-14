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

        
        public int CallCenterID { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(55, Ruleset = "Default", MessageTemplate = "AgencyId's Maximum length is 55 characters")]
        public string CcAgentIdKey { get; set; }

        //[NotNullValidator(Ruleset = "Default", MessageTemplate = "Start date is required")]
        [RequiredObjectValidator(Ruleset = "Default", MessageTemplate = "Start date is required")]
        public DateTime StartDate { get; set; }

        [RequiredObjectValidator(Ruleset = "Default", MessageTemplate = "End date is required")]
        public DateTime EndDate { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(10, Ruleset = "Default", MessageTemplate = "DNIS's Maximum length is 10 characters")]
        public string DNIS { get; set; }

        [IgnoreNulls()]
        //[StringLengthValidator(4, Ruleset = "Default", MessageTemplate = "CallCenter's Maximum length is 4 characters")]
        public string CallCenter { get; set; }        

        [IgnoreNulls()]
        [StringLengthValidator(15, Ruleset = "Default", MessageTemplate = "CallResource's Maximum length is 15 characters")]
        public string CallSourceCd { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(75, Ruleset = "Default", MessageTemplate = " ReasonToCall's Maximum length is 75 characters")]
        public string ReasonToCall { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "AccountNumber's Maximum length is 30 characters")]
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
        //[StringLengthValidator(50, Ruleset = "Default", MessageTemplate = "OtherServicerName's Maximum length is 50 characters")]
        public string OtherServicerName { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(9, Ruleset = "Default", MessageTemplate = "PropZip's Maximum length is 9 characters")]
        public string PropZipFull9 { get; set; }
        
        [IgnoreNulls()]
        public int PrevAgencyId { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(20, Ruleset = "Default", MessageTemplate = "SelectedAgencyId's Maximum length is 20 characters")]
        public string SelectedAgencyId { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(2000, Ruleset = "Default", MessageTemplate = "ScreenRout's Maximum length is 2000 characters")]
        public string ScreenRout { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(15, Ruleset = "Default", MessageTemplate = "FinalDispoCd's Maximum length is 15 characters")]
        public string FinalDispoCd { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(12, Ruleset = "Default", MessageTemplate = "TransNumber's Maximum length is 12 characters")]
        public string TransNumber { get; set; }

        
        [StringLengthValidator(1, 18, Ruleset = "Default", MessageTemplate = "CcCallKey's Maximum length is 18 characters")]
        public string CcCallKey { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(15, Ruleset = "Default", MessageTemplate = "LoanDelinqStatusCd's Maximum length is 15 characters")]
        public string LoanDelinqStatusCd { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(40, Ruleset = "Default", MessageTemplate = "SelectedCounselor's Maximum length is 40 characters")]
        public string SelectedCounselor { get; set; }

        [IgnoreNulls()]
        [YesNoIndicatorValidator(Ruleset = "Default", MessageTemplate = "HomeownerInd must be either Y or N")]
        public string HomeownerInd { get; set; }

        [IgnoreNulls()]
        [YesNoIndicatorValidator(Ruleset = "Default", MessageTemplate = "PowerOfAttorneyInd  must be either Y or N")]
        public string PowerOfAttorneyInd { get; set; }

        [IgnoreNulls()]
        [YesNoIndicatorValidator(Ruleset = "Default", MessageTemplate = "AuthorizedInd  must be either Y or N")]
        public string AuthorizedInd { get; set; }

        #endregion        
    }
}
