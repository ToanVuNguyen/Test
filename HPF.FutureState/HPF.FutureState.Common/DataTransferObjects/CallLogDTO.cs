﻿using System;
using System.Xml.Serialization;
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

        [NullableOrStringLengthValidator(true, 55, "Cc Agent Id Key", Ruleset = "Default")]
        public string CcAgentIdKey { get; set; }
        
        [XmlElement(IsNullable=true)]
        [RequiredObjectValidator(Ruleset = "Default", MessageTemplate = ErrorMessages.ERR0351)]
        public DateTime? StartDate { get; set; }

        [XmlElement(IsNullable = true)]
        [RequiredObjectValidator(Ruleset = "Default", MessageTemplate = ErrorMessages.ERR0352)]
        public DateTime? EndDate { get; set; }
        
        [NullableOrStringLengthValidator(true, 10, "DNIS", Ruleset = "Default")]
        public string DNIS { get; set; }

        public string CallCenter { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Call Source Code", Ruleset = "Default")]        
        public string CallSourceCd { get; set; }

        [NullableOrStringLengthValidator(true, 75, "Reason To Call", Ruleset = "Default")]
        public string ReasonToCall { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Loan Account Number", Ruleset = "Default")]
        public string LoanAccountNumber { get; set; }

        [NullableOrStringLengthValidator(true, 30, "First name", Ruleset = "Default")]
        public string FirstName { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Last name", Ruleset = "Default")]
        public string LastName { get; set; }

        [XmlElement(IsNullable = true)]
        [NullableOrInRangeValidator(true, "[0-9]", Ruleset = "Default", MessageTemplate = "Servicer Id must be type of Integer")]
        public int? ServicerId { get; set; }

        public string OtherServicerName { get; set; }

        [NullableOrStringLengthValidator(true, 9, "Prop Zip", Ruleset = "Default")]
        public string PropZipFull9 { get; set; }

        [XmlElement(IsNullable = true)]
        [NullableOrInRangeValidator(true, "[0-9]", Ruleset = "Default", MessageTemplate = "Prev Agency Id must be type of Integer")]
        public int? PrevAgencyId { get; set; }

        [NullableOrStringLengthValidator(true, 20, "Selected Agency Id", Ruleset = "Default")]
        public string SelectedAgencyId { get; set; }

        [NullableOrStringLengthValidator(true, 2000, "Screen Rout", Ruleset = "Default")]
        public string ScreenRout { get; set; }

        [NullableOrStringLengthValidator(false, 15, "Final Dispo Code", Ruleset = "Default", MessageTemplate = ErrorMessages.ERR0353)]
        public string FinalDispoCd { get; set; }

        [NullableOrStringLengthValidator(true, 12, "Trans Number", Ruleset = "Default")]
        public string TransNumber { get; set; }

        [NullableOrStringLengthValidator(false, 18, "Cc Call Key", Ruleset = "Default", MessageTemplate = ErrorMessages.ERR0350)]
        public string CcCallKey { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Loan Delinq Status Cd", Ruleset = "Default")]
        public string LoanDelinqStatusCd { get; set; }

        [NullableOrStringLengthValidator(true, 40, "Selected Counselor", Ruleset = "Default")]
        public string SelectedCounselor { get; set; }

        
        string _homeownerInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default")]
        public string HomeownerInd { get { return _homeownerInd; } set { _homeownerInd = value.ToUpper(); } }

        string _powerOfAttorneyInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default")]
        public string PowerOfAttorneyInd { get { return _powerOfAttorneyInd; } set { _powerOfAttorneyInd = value.ToUpper(); } }
        
        string _authorizedInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default")]
        public string AuthorizedInd { get { return _authorizedInd; } set { _authorizedInd = value.ToUpper(); } }

        [NullableOrStringLengthValidator(false, 30, "Working User ID", Ruleset= "Default")]
        public string WorkingUserId { get; set; }
        #endregion        
    }
}
