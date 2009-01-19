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
        [StringLengthValidator(55, Ruleset = "Default", MessageTemplate = "Agency Id's maximum length is 55 characters")]
        public string CcAgentIdKey { get; set; }

        //[NotNullValidator(Ruleset = "Default", MessageTemplate = "Start date is required")]
        [RequiredObjectValidator(Ruleset = "Default")]
        public DateTime StartDate { get; set; }

        [RequiredObjectValidator(Ruleset = "Default")]
        public DateTime EndDate { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(10, Ruleset = "Default", MessageTemplate = "DNIS's maximum length is 10 characters")]
        public string DNIS { get; set; }

        [IgnoreNulls()]
        //[StringLengthValidator(4, Ruleset = "Default", MessageTemplate = "CallCenter's Maximum length is 4 characters")]
        public string CallCenter { get; set; }        

        [IgnoreNulls()]
        [StringLengthValidator(15, Ruleset = "Default", MessageTemplate = "Call Resource's maximum length is 15 characters")]
        public string CallSourceCd { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(75, Ruleset = "Default", MessageTemplate = " Reason To Call's maximum length is 75 characters")]
        public string ReasonToCall { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "Account Number's maximum length is 30 characters")]
        public string LoanAccountNumber { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "First Name's maximum length is 30 characters")]
        public string FirstName { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(30, Ruleset = "Default", MessageTemplate = "Last Name's maximum length is 30 characters")]
        public string LastName { get; set; }

        
        [NullableOrInRangeValidator(true, "[0-9]", Ruleset = "Default", MessageTemplate = "Servicer Id must be type of Integer")]
        public int ServicerId { get; set; }

        [IgnoreNulls()]
        //[StringLengthValidator(50, Ruleset = "Default", MessageTemplate = "OtherServicerName's Maximum length is 50 characters")]
        public string OtherServicerName { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(9, Ruleset = "Default", MessageTemplate = "Prop Zip's maximum length is 9 characters")]
        public string PropZipFull9 { get; set; }
        
        [NullableOrInRangeValidator(true, "[0-9]", Ruleset = "Default", MessageTemplate = "Prev Agency Id must be type of Integer")]
        public int PrevAgencyId { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(20, Ruleset = "Default", MessageTemplate = "Selected Agency Id's maximum length is 20 characters")]
        public string SelectedAgencyId { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(2000, Ruleset = "Default", MessageTemplate = "Screen Rout's maximum length is 2000 characters")]
        public string ScreenRout { get; set; }

        
        [NullableOrStringLengthValidator(false, 15, "Final Dispo Code", Ruleset = "Default")]
        public string FinalDispoCd { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(12, Ruleset = "Default", MessageTemplate = "Trans Number's maximum length is 12 characters")]
        public string TransNumber { get; set; }

        
        [NullableOrStringLengthValidator(false, 18, "Cc Call Key", Ruleset = "Default")]
        public string CcCallKey { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(15, Ruleset = "Default", MessageTemplate = "LoanDelinqStatusCd's maximum length is 15 characters")]
        public string LoanDelinqStatusCd { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(40, Ruleset = "Default", MessageTemplate = "Selected Counselor's maximum length is 40 characters")]
        public string SelectedCounselor { get; set; }

        [IgnoreNulls()]
        [YesNoIndicatorValidator(true, Ruleset = "Default")]
        public string HomeownerInd { get; set; }

        [IgnoreNulls()]
        [YesNoIndicatorValidator(true, Ruleset = "Default")]
        public string PowerOfAttorneyInd { get; set; }

        [IgnoreNulls()]
        [YesNoIndicatorValidator(true, Ruleset = "Default")]
        public string AuthorizedInd { get; set; }

        #endregion        
    }
}
