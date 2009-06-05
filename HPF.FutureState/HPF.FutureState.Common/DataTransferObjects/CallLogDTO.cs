using System;
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
        public int? CallId { get; set; }
        public int? CallCenterID { get; set; }

        [NullableOrStringLengthValidator(false, 55, "Cc Agent Id Key", Ruleset = "Default", Tag = ErrorMessages.ERR0356)]
        string ccAgentIdKey;
        public string CcAgentIdKey 
        {
            get { return ccAgentIdKey; }
            set { ccAgentIdKey = string.IsNullOrEmpty(value) ? null : value; }
        }
        
        [RequiredObjectValidator(Ruleset = "Default", Tag = ErrorMessages.ERR0351)]
        public DateTime? StartDate { get; set; }

        
        [RequiredObjectValidator(Ruleset = "Default", Tag = ErrorMessages.ERR0352)]
        public DateTime? EndDate { get; set; }

        [NullableOrStringLengthValidator(true, 10, "DNIS", Ruleset = "Default", Tag = ErrorMessages.ERR0363)]
        string dnis;
        public string DNIS 
        {
            get { return dnis; }
            set { dnis = string.IsNullOrEmpty(value) ? null : value; }
        }

        [NullableOrStringLengthValidator(true, 4, "CallCenter", Ruleset = "Default", Tag = ErrorMessages.ERR0364)]
        string callCenter;
        public string CallCenter 
        {
            get { return callCenter; }
            set { callCenter = string.IsNullOrEmpty(value) ? null : value; }
        }

        string callSourceCd;
        [NullableOrStringLengthValidator(true, 15, "Call Source Code", Ruleset = "Default")]        
        public string CallSourceCd 
        {
            get { return callSourceCd; }
            set { callSourceCd = string.IsNullOrEmpty(value)?null:value.ToUpper(); }
        }

        //miss Error for string length
        [NullableOrStringLengthValidator(true, 75, "Reason For Call", Ruleset = "Default")]
        string reasonForCall;
        public string ReasonForCall 
        {
            get { return reasonForCall; }
            set { reasonForCall = string.IsNullOrEmpty(value) ? null : value; }
        }

        [NullableOrStringLengthValidator(true, 30, "Loan Account Number", Ruleset = "Default", Tag = ErrorMessages.ERR0365)]
        string loanAccountNumber;
        public string LoanAccountNumber 
        {
            get { return loanAccountNumber; }
            set { loanAccountNumber = string.IsNullOrEmpty(value) ? null : value; }
        }

        [NullableOrStringLengthValidator(true, 30, "First name", Ruleset = "Default", Tag = ErrorMessages.ERR0366)]
        string firstName;
        public string FirstName 
        {
            get { return firstName; }
            set { firstName = string.IsNullOrEmpty(value) ? null : value; }
        }

        [NullableOrStringLengthValidator(true, 30, "Last name", Ruleset = "Default", Tag = ErrorMessages.ERR0367)]
        string lastName;
        public string LastName 
        {
            get { return lastName; }
            set { lastName = string.IsNullOrEmpty(value) ? null : value; }
        }
        
        [NullableOrInRangeValidator(true, "[0-9]", Ruleset = "Default", MessageTemplate = "Servicer Id must be a valid integer")]
        public int? ServicerId { get; set; }

        [NullableOrStringLengthValidator(true, 50, "OtherServicerName", Ruleset = "Default", MessageTemplate = "Other servicer name max length is 50")]
        string otherServicerName;
        public string OtherServicerName 
        {
            get { return otherServicerName; }
            set { otherServicerName = string.IsNullOrEmpty(value) ? null : value; }
        }

        [NullableOrStringLengthValidator(true, 9, "Prop Zip", Ruleset = "Default", Tag = ErrorMessages.ERR0368)]
        string propZipFull9;
        public string PropZipFull9 
        {
            get { return propZipFull9; }
            set { propZipFull9 = string.IsNullOrEmpty(value) ? null : value; } 
        }


        [NullableOrInRangeValidator(true, "[0-9]", Ruleset = "Default", MessageTemplate = "Prev Agency Id must be a valid integer")]
        public int? PrevAgencyId { get; set; }

        [NullableOrInRangeValidator(true, "[0-9]", Ruleset = "Default", MessageTemplate = "Selected Agency Id must be a valid integer")]
        public int? SelectedAgencyId { get; set; }

        [NullableOrStringLengthValidator(true, 2000, "Screen Rout", Ruleset = "Default", Tag = ErrorMessages.ERR0369)]
        string screemRout;
        public string ScreenRout 
        {
            get { return screemRout; }
            set { screemRout = string.IsNullOrEmpty(value) ? null : value; }
        }

        string finalDispoCd;
        [NullableOrStringLengthValidator(false, 15, "Final Dispo Code", Ruleset = "Default", Tag = ErrorMessages.ERR0353)]
        public string FinalDispoCd
        {
            get { return finalDispoCd; }
            set { finalDispoCd = string.IsNullOrEmpty(value)?null:value.ToUpper(); }
        }

        [NullableOrStringLengthValidator(true, 12, "Trans Number", Ruleset = "Default", Tag = ErrorMessages.ERR0370)]
        string transNumber;
        public string TransNumber
        {
            get { return transNumber; }
            set { transNumber = string.IsNullOrEmpty(value) ? null : value; } 
        }

        [NullableOrStringLengthValidator(false, 18, "Cc Call Key", Ruleset = "Default", Tag = ErrorMessages.ERR0350)]
        string ccCallKey;
        public string CcCallKey 
        {
            get { return ccCallKey; }
            set { ccCallKey = string.IsNullOrEmpty(value) ? null : value; } 
        }

        string loanDelinqStatusCd;
        [NullableOrStringLengthValidator(true, 15, "Loan Delinq Status Cd", Ruleset = "Default")]
        public string LoanDelinqStatusCd
        {
            get { return loanDelinqStatusCd; }
            set { loanDelinqStatusCd = string.IsNullOrEmpty(value)?null:value.ToUpper(); }
            
        }

        [NullableOrStringLengthValidator(true, 40, "Selected Counselor", Ruleset = "Default", Tag = ErrorMessages.ERR0371)]
        string selectedCounselor;
        public string SelectedCounselor 
        {
            get { return selectedCounselor; }
            set { selectedCounselor = string.IsNullOrEmpty(value) ? null : value; } 
        }
        
        string homeownerInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default", Tag = ErrorMessages.ERR0372)]
        public string HomeownerInd
        {
            get { return homeownerInd; }
            set { homeownerInd = string.IsNullOrEmpty(value)?null:value.ToUpper(); }
        }
        string powerOfAttorneyInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default", Tag = ErrorMessages.ERR0373)]
        public string PowerOfAttorneyInd 
        {
            get { return powerOfAttorneyInd; }
            set { powerOfAttorneyInd = string.IsNullOrEmpty(value) ? null : value.ToUpper(); }
        }
        
        string authorizedInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default", Tag = ErrorMessages.ERR0374)]
        public string AuthorizedInd 
        { 
            get { return authorizedInd; }
            set { authorizedInd = string.IsNullOrEmpty(value) ? null : value.ToUpper(); }
        }

        [NullableOrStringLengthValidator(true, 30, "City", Ruleset = "Default", Tag = ErrorMessages.ERR0390)]
        string city;
        public string City
        {
            get { return city; }
            set { city = string.IsNullOrEmpty(value) ? null : value; }
        }  //	varchar(30)

        [NullableOrStringLengthValidator(true, 2, "State", Ruleset = "Default", Tag = ErrorMessages.ERR0391)]
        string state;
        public string State	
        {
            get { return state; }
            set { state = string.IsNullOrEmpty(value) ? null : value; }
        } //varchar(2)

        [NullableOrStringLengthValidator(true, 10, "NonprofitReferralKeyNum1", Ruleset = "Default", Tag = ErrorMessages.ERR0392)]
        string nonprofitReferralKeyNum1;
        public string NonprofitReferralKeyNum1 
        {
            get { return nonprofitReferralKeyNum1; }
            set { nonprofitReferralKeyNum1 = string.IsNullOrEmpty(value) ? null : value; }
        }

        [NullableOrStringLengthValidator(true, 10, "NonprofitReferralKeyNum2", Ruleset = "Default", Tag = ErrorMessages.ERR0393)]
        string nonprofitReferralKeyNum2;
        public string NonprofitReferralKeyNum2 
        {
            get { return nonprofitReferralKeyNum2; }
            set { nonprofitReferralKeyNum2 = string.IsNullOrEmpty(value) ? null : value; }
        }

        [NullableOrStringLengthValidator(true, 10, "NonprofitReferralKeyNum3", Ruleset = "Default", Tag = ErrorMessages.ERR0394)]
        string nonprofitReferralKeyNum3;
        public string NonprofitReferralKeyNum3 
        {
            get { return nonprofitReferralKeyNum3; }
            set { nonprofitReferralKeyNum3 = string.IsNullOrEmpty(value) ? null : value; } 
        }


        string delinqInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default", MessageTemplate = "DelinqInd has a maximum length of 1 characters.")]        
        public string DelinqInd
        {
            get { return delinqInd; }
            set { delinqInd =string.IsNullOrEmpty(value)?null:value.ToUpper(); }
        }
        [NullableOrStringLengthValidator(true, 50, "propStreetAddress", Ruleset = "Default", MessageTemplate = "propStreetAddress has a maximum length of 50 characters."/*, Tag=ErrorMessages.ERR0001*/)]
        string propStreetAddress;
        public string PropStreetAddress 
        {
            get { return propStreetAddress; }
            set { propStreetAddress = string.IsNullOrEmpty(value) ? null : value; } 
        }

        string primaryResidenceInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default", MessageTemplate = "PrimaryResidenceInd has a maximum length of 1 characters.")]                
        public string PrimaryResidenceInd
        {
            get { return primaryResidenceInd; }
            set { primaryResidenceInd = string.IsNullOrEmpty(value)?null:value.ToUpper(); }
        }
       
        string maxLoanAmountInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default", MessageTemplate = "MaxLoanAmountInd has a maximum length of 1 characters.")]                
        public string MaxLoanAmountInd
        {
            get { return maxLoanAmountInd; }
            set { maxLoanAmountInd = string.IsNullOrEmpty(value)?null:value.ToUpper(); }
        }

        [NullableOrStringLengthValidator(true, 10, "CustomerPhone", Ruleset = "Default", MessageTemplate = "CustomerPhone has a maximum length of 10 characters."/*, Tag=ErrorMessages.ERR0001*/)]
        string customerPhone;
        public string CustomerPhone 
        {
            get { return customerPhone; }
            set { customerPhone = string.IsNullOrEmpty(value) ? null : value; } 
        }

        [NullableOrStringLengthValidator(true, 15, "LoanLookupCode", Ruleset = "Default", MessageTemplate = "LoanLookupCode has a maximum length of 15 characters."/*, Tag=ErrorMessages.ERR0001*/)]
        string loanLookupCd;
        public string LoanLookupCd 
        {
            get { return loanLookupCd; }
            set { loanLookupCd = string.IsNullOrEmpty(value) ? null : value.ToUpper(); } 
        }

        string originatedPrior2009Ind;
        [YesNoIndicatorValidator(true, Ruleset = "Default", MessageTemplate = "OriginatedPrior2009Ind has a maximum length of 1 characters.")]
        public string OriginatedPrior2009Ind
        {
            get { return originatedPrior2009Ind; }
            set { originatedPrior2009Ind = string.IsNullOrEmpty(value) ? null : value.ToUpper(); }
        }

        [NullableOrInRangeNumberValidator(true, "-9999999", "9999999", Ruleset = "Default", MessageTemplate = "PaymentAmount has minimumvalue -9999999 and maximum value 9999999")]
        public double? PaymentAmount { get; set; }

        [NullableOrInRangeNumberValidator(true, "-99999999", "99999999", Ruleset = "Default", MessageTemplate = "GrossIncomeAmount has minimumvalue -99999999 and maximum value 99999999")]
        public double? GrossIncomeAmount { get; set; }

        string dtiInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default", MessageTemplate = "DTIInd has a maximum length of 1 characters.")]                
        public string DTIInd
        {
            get { return dtiInd; }
            set { dtiInd = string.IsNullOrEmpty(value) ? null : value.ToUpper(); }
        }

        public int? ServicerCANumber { get; set; }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = "Default", MessageTemplate = "ServicerCALastContactDate must be between 1/1/1753 and 12/31/9999")]
        public DateTime? ServicerCALastContactDate { get; set; }

        public int? ServicerCAId { get; set; }

        [NullableOrStringLengthValidator(true, 50, "ServicerCAOtherName", Ruleset = "Default", MessageTemplate = "ServicerCAOtherName has a maximum length of 50 characters."/*, Tag=ErrorMessages.ERR0001*/)]
        string servicerCAOtherName;
        public string ServicerCAOtherName 
        {
            get { return servicerCAOtherName; }
            set { servicerCAOtherName = string.IsNullOrEmpty(value) ? null : value; }
        }

        string mhaInfoShareInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default", MessageTemplate = "MHAInfoShareInd has a maximum length of 1 characters.")]                
        public string MHAInfoShareInd         
        {
            get { return mhaInfoShareInd; }
            set { mhaInfoShareInd = string.IsNullOrEmpty(value) ? null : value.ToUpper(); }
        }

        //[RequiredObjectValidator(Ruleset = "Default", MessageTemplate="ICT Call Id is required to insert the call")]
        [NullableOrStringLengthValidator(true, 40, "ICTCallId", Ruleset = "Default", MessageTemplate = "ICTCallId has a maximum length of 40 characters."/*, Tag=ErrorMessages.ERR0001*/)]
        string ictCallId;
        public string ICTCallId 
        {
            get { return ictCallId; }
            set { ictCallId = string.IsNullOrEmpty(value) ? null : value; } 
        }

        /// <summary>
        /// Auto HPF field
        /// </summary>
        public string MHAEligibilityCd { get; set; }

        #endregion        
    }
}
