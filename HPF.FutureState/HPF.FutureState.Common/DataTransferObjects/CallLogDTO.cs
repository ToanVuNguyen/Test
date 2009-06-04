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
        public string CcAgentIdKey { get; set; }
        
        [RequiredObjectValidator(Ruleset = "Default", Tag = ErrorMessages.ERR0351)]
        public DateTime? StartDate { get; set; }

        
        [RequiredObjectValidator(Ruleset = "Default", Tag = ErrorMessages.ERR0352)]
        public DateTime? EndDate { get; set; }

        [NullableOrStringLengthValidator(true, 10, "DNIS", Ruleset = "Default", Tag = ErrorMessages.ERR0363)]
        public string DNIS { get; set; }

        [NullableOrStringLengthValidator(true, 4, "CallCenter", Ruleset = "Default", Tag = ErrorMessages.ERR0364)]
        public string CallCenter { get; set; }

        string _callSourceCd;
        [NullableOrStringLengthValidator(true, 15, "Call Source Code", Ruleset = "Default")]        
        public string CallSourceCd 
        {
            get { return _callSourceCd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _callSourceCd = value.Trim().ToUpper();
                else _callSourceCd = value;
            }
        }

        //miss Error for string length
        [NullableOrStringLengthValidator(true, 75, "Reason For Call", Ruleset = "Default")]
        public string ReasonForCall { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Loan Account Number", Ruleset = "Default", Tag = ErrorMessages.ERR0365)]
        public string LoanAccountNumber { get; set; }

        [NullableOrStringLengthValidator(true, 30, "First name", Ruleset = "Default", Tag = ErrorMessages.ERR0366)]
        public string FirstName { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Last name", Ruleset = "Default", Tag = ErrorMessages.ERR0367)]
        public string LastName { get; set; }
        
        [NullableOrInRangeValidator(true, "[0-9]", Ruleset = "Default", MessageTemplate = "Servicer Id must be a valid integer")]
        public int? ServicerId { get; set; }

        [NullableOrStringLengthValidator(true, 50, "OtherServicerName", Ruleset = "Default", MessageTemplate = "Other servicer name max length is 50")]
        public string OtherServicerName { get; set; }

        [NullableOrStringLengthValidator(true, 9, "Prop Zip", Ruleset = "Default", Tag = ErrorMessages.ERR0368)]
        public string PropZipFull9 { get; set; }


        [NullableOrInRangeValidator(true, "[0-9]", Ruleset = "Default", MessageTemplate = "Prev Agency Id must be a valid integer")]
        public int? PrevAgencyId { get; set; }

        [NullableOrInRangeValidator(true, "[0-9]", Ruleset = "Default", MessageTemplate = "Selected Agency Id must be a valid integer")]
        public int? SelectedAgencyId { get; set; }

        [NullableOrStringLengthValidator(true, 2000, "Screen Rout", Ruleset = "Default", Tag = ErrorMessages.ERR0369)]
        public string ScreenRout { get; set; }

        string _finalDispoCd;
        [NullableOrStringLengthValidator(false, 15, "Final Dispo Code", Ruleset = "Default", Tag = ErrorMessages.ERR0353)]
        public string FinalDispoCd
        {
            get { return _finalDispoCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _finalDispoCd = value.Trim().ToUpper();
                else _finalDispoCd = value;
            }
        }

        [NullableOrStringLengthValidator(true, 12, "Trans Number", Ruleset = "Default", Tag = ErrorMessages.ERR0370)]
        public string TransNumber { get; set; }

        [NullableOrStringLengthValidator(false, 18, "Cc Call Key", Ruleset = "Default", Tag = ErrorMessages.ERR0350)]
        public string CcCallKey { get; set; }

        string _loanDelinqStatusCd;
        [NullableOrStringLengthValidator(true, 15, "Loan Delinq Status Cd", Ruleset = "Default")]
        public string LoanDelinqStatusCd
        {
            get { return _loanDelinqStatusCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _loanDelinqStatusCd = value.Trim().ToUpper();
                else _loanDelinqStatusCd = value;
            }
        }

        [NullableOrStringLengthValidator(true, 40, "Selected Counselor", Ruleset = "Default", Tag = ErrorMessages.ERR0371)]
        public string SelectedCounselor { get; set; }
        
        string _homeownerInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default", Tag = ErrorMessages.ERR0372)]
        public string HomeownerInd
        {
            get { return _homeownerInd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _homeownerInd = value.Trim().ToUpper();
                else _homeownerInd = value;
            }
        }
        string _powerOfAttorneyInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default", Tag = ErrorMessages.ERR0373)]
        public string PowerOfAttorneyInd 
        {
            get { return _powerOfAttorneyInd; } 
            set            
            {
                if (!string.IsNullOrEmpty(value)) _powerOfAttorneyInd = value.Trim().ToUpper();
                else _powerOfAttorneyInd = value;
            }
        }
        
        string _authorizedInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default", Tag = ErrorMessages.ERR0374)]
        public string AuthorizedInd 
        { 
            get { return _authorizedInd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _authorizedInd = value.Trim().ToUpper();
                else _authorizedInd = value;
            }
        }

        [NullableOrStringLengthValidator(true, 30, "City", Ruleset = "Default", Tag = ErrorMessages.ERR0390)]
        public string City {get; set;}  //	varchar(30)

        [NullableOrStringLengthValidator(true, 2, "State", Ruleset = "Default", Tag = ErrorMessages.ERR0391)]
        public string State	{get; set;} //varchar(2)

        [NullableOrStringLengthValidator(true, 10, "NonprofitReferralKeyNum1", Ruleset = "Default", Tag = ErrorMessages.ERR0392)]
        public string NonprofitReferralKeyNum1 {get; set;}

        [NullableOrStringLengthValidator(true, 10, "NonprofitReferralKeyNum2", Ruleset = "Default", Tag = ErrorMessages.ERR0393)]        
        public string NonprofitReferralKeyNum2 {get; set;}

        [NullableOrStringLengthValidator(true, 10, "NonprofitReferralKeyNum3", Ruleset = "Default", Tag = ErrorMessages.ERR0394)]
        public string NonprofitReferralKeyNum3 { get; set; }


        string delinqInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default", MessageTemplate = "DelinqInd has a maximum length of 1 characters.")]        
        public string DelinqInd
        {
            get { return delinqInd; }
            set
            {
                delinqInd = value;
                if (delinqInd != null)
                    delinqInd = delinqInd.ToUpper();
            }
        }
        [NullableOrStringLengthValidator(true, 50, "propStreetAddress", Ruleset = "Default", MessageTemplate = "propStreetAddress has a maximum length of 50 characters."/*, Tag=ErrorMessages.ERR0001*/)]
        public string PropStreetAddress { get; set; }

        string primaryResidenceInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default", MessageTemplate = "PrimaryResidenceInd has a maximum length of 1 characters.")]                
        public string PrimaryResidenceInd
        {
            get { return primaryResidenceInd; }
            set
            {
                primaryResidenceInd = value;
                if (primaryResidenceInd != null)
                    primaryResidenceInd = primaryResidenceInd.ToUpper();
            }
        }
       
        string maxLoanAmountInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default", MessageTemplate = "MaxLoanAmountInd has a maximum length of 1 characters.")]                
        public string MaxLoanAmountInd
        {
            get { return maxLoanAmountInd; }
            set
            {
                maxLoanAmountInd = value;
                if (maxLoanAmountInd != null)
                    maxLoanAmountInd = maxLoanAmountInd.ToUpper();
            }
        }

        [NullableOrStringLengthValidator(true, 10, "CustomerPhone", Ruleset = "Default", MessageTemplate = "CustomerPhone has a maximum length of 10 characters."/*, Tag=ErrorMessages.ERR0001*/)]
        public string CustomerPhone { get; set; }

        [NullableOrStringLengthValidator(true, 15, "LoanLookupCode", Ruleset = "Default", MessageTemplate = "LoanLookupCode has a maximum length of 15 characters."/*, Tag=ErrorMessages.ERR0001*/)]
        public string LoanLookupCd { get; set; }

        string originatedPrior2009Ind;
        [YesNoIndicatorValidator(true, Ruleset = "Default", MessageTemplate = "OriginatedPrior2009Ind has a maximum length of 1 characters.")]
        public string OriginatedPrior2009Ind
        {
            get { return originatedPrior2009Ind; }
            set
            {
                originatedPrior2009Ind = value;
                if (originatedPrior2009Ind != null)
                    originatedPrior2009Ind = originatedPrior2009Ind.ToUpper();
            }
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
            set
            {
                dtiInd = value;
                if (dtiInd != null)
                    dtiInd = dtiInd.ToUpper();
            }
        }

        public int? ServicerCANumber { get; set; }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = "Default", MessageTemplate = "ServicerCALastContactDate must be between 1/1/1753 and 12/31/9999")]
        public DateTime? ServicerCALastContactDate { get; set; }

        public int? ServicerCAId { get; set; }

        [NullableOrStringLengthValidator(true, 50, "ServicerCAOtherName", Ruleset = "Default", MessageTemplate = "ServicerCAOtherName has a maximum length of 50 characters."/*, Tag=ErrorMessages.ERR0001*/)]
        public string ServicerCAOtherName { get; set; }

        string mhaInfoShareInd;
        [YesNoIndicatorValidator(true, Ruleset = "Default", MessageTemplate = "MHAInfoShareInd has a maximum length of 1 characters.")]                
        public string MHAInfoShareInd         
        {
            get { return mhaInfoShareInd; }
            set
            {
                mhaInfoShareInd = value;
                if (mhaInfoShareInd != null)
                    mhaInfoShareInd = mhaInfoShareInd.ToUpper();
            }
        }

        [RequiredObjectValidator(Ruleset = "Default", MessageTemplate="ICT Call Id is required to insert the call")]
        [NullableOrStringLengthValidator(true, 40, "ICTCallId", Ruleset = "Default", MessageTemplate = "ICTCallId has a maximum length of 40 characters."/*, Tag=ErrorMessages.ERR0001*/)]
        public string ICTCallId { get; set; }

        /// <summary>
        /// Auto HPF field
        /// </summary>
        public string MHAEligibilityCd { get; set; }

        #endregion        
    }
}
