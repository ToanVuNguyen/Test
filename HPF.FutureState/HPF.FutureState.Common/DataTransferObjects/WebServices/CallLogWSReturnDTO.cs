using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class CallLogWSReturnDTO : BaseDTO
    {
        #region property
        public string CallId { get; set; }                

        [IgnoreNulls()]
        public DateTime StartDate { get; set; }

        [IgnoreNulls()]
        public DateTime EndDate { get; set; }                

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
        [StringLengthValidator(50, Ruleset = "Default", MessageTemplate = "OtherServicerName's Maximum length is 50 characters")]
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
        [StringLengthValidator(15, Ruleset = "Default", MessageTemplate = "FinalDispoCd's Maximum length is 15 characters")]
        public string FinalDispoCd { get; set; }                

        [IgnoreNulls()]
        [StringLengthValidator(15, Ruleset = "Default", MessageTemplate = "LoanDelinqStatusCd's Maximum length is 15 characters")]
        public string LoanDelinqStatusCd { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(40, Ruleset = "Default", MessageTemplate = "SelectedCounselor's Maximum length is 40 characters")]
        public string SelectedCounselor { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(1, Ruleset = "Default", MessageTemplate = "HomeownerInd's Maximum length is 1 characters")]
        public string HomeownerInd { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(1, Ruleset = "Default", MessageTemplate = "PowerOfAttorneyInd's Maximum length is 1 characters")]
        public string PowerOfAttorneyInd { get; set; }

        [IgnoreNulls()]
        [StringLengthValidator(1, Ruleset = "Default", MessageTemplate = "AuthorizedInd's Maximum length is 1 characters")]
        public string AuthorizedInd { get; set; }

        #endregion           
    }
}
