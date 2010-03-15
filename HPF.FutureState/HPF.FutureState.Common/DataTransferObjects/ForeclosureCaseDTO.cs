using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using HPF.FutureState.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class ForeclosureCaseDTO : BaseDTO
    {
        [XmlElement(IsNullable = true)]
        public int? FcId { get; set; }

        [XmlElement(IsNullable = true)]        
        [RequiredObjectValidator(Tag = ErrorMessages.ERR0100, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public int? AgencyId { get; set; }

        [XmlIgnore]
        public DateTime? CompletedDt { get; set; }
        
        public string CallId { get; set; }

        [XmlElement(IsNullable = true)]        
        [RequiredObjectValidator(Tag = ErrorMessages.ERR0101, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]        
        public int? ProgramId { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0102, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "Agency Case Number", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0001)]        
        public string AgencyCaseNum { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Agency Client Number", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0002)]
        public string AgencyClientNum { get; set; }

        [XmlElement(IsNullable = true)]        
        [RequiredObjectValidator(Tag = ErrorMessages.ERR0103, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrInRangeNumberValidator(true,"1-1-1753","12-31-9999",Ruleset = Constant.RULESET_LENGTH, MessageTemplate= "IntakeDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? IntakeDt { get; set; }

        private string _incomeEarnersCd;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0300, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Income Earner Code", Ruleset = Constant.RULESET_LENGTH)]
        public string IncomeEarnersCd 
        {
            get { return _incomeEarnersCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _incomeEarnersCd = value.Trim().ToUpper();
                else _incomeEarnersCd = value;
            }
        }

        private string _caseSourceCd;
        [StringRequiredValidator(Tag = ErrorMessages.ERR0104, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Case Source Code", Ruleset = Constant.RULESET_LENGTH)]
        public string CaseSourceCd 
        {
            get { return _caseSourceCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _caseSourceCd = value.Trim().ToUpper();
                else _caseSourceCd = value;
            }
        }


        string _raceCd;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0301, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Race Code", Ruleset = Constant.RULESET_LENGTH)]
        public string RaceCd 
        {
            get { return _raceCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _raceCd = value.Trim().ToUpper();
                else _raceCd = value;
            }
        }

        string _householdCd;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0302, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Household Code", Ruleset = Constant.RULESET_LENGTH)]
        public string HouseholdCd
        {
            get { return _householdCd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _householdCd = value.Trim().ToUpper();
                else _householdCd = value;
            }
        }

        string _neverBillReasonCd;
        [XmlIgnore]
        [NullableOrStringLengthValidator(true, 15, "Never Bill Reason Code", Ruleset = Constant.RULESET_LENGTH)]
        public string NeverBillReasonCd
        {
            get { return _neverBillReasonCd; }
            set 
            { 
                if (!string.IsNullOrEmpty(value)) _neverBillReasonCd = value.Trim().ToUpper();
                else _neverBillReasonCd = value;
            }
        }

        string _nerverPayReasonCd;
        [XmlIgnore]
        [NullableOrStringLengthValidator(true, 15, "Never Pay Reason Code", Ruleset = Constant.RULESET_LENGTH)]
        public string NeverPayReasonCd
        {
            get { return _nerverPayReasonCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _nerverPayReasonCd = value.Trim().ToUpper();
                else _nerverPayReasonCd = value;
            }
        }

        string _dfltReason1stCd;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0303, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Default Reason 1st Code", Ruleset = Constant.RULESET_LENGTH)]
        public string DfltReason1stCd
        {
            get { return _dfltReason1stCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _dfltReason1stCd = value.Trim().ToUpper();
                else _dfltReason1stCd = value;
            }
        }

        string _dfltReason2ndCd;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0304, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Default Reason 2nd Code", Ruleset = Constant.RULESET_LENGTH)]
        public string DfltReason2ndCd 
        {
            get { return _dfltReason2ndCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _dfltReason2ndCd = value.Trim().ToUpper();
                else _dfltReason2ndCd = value;
            }
        }

        string _hudTerminationReasonCd;
        [NullableOrStringLengthValidator(true, 15, "Hud Termination Reason Code", Ruleset = Constant.RULESET_LENGTH)]
        public string HudTerminationReasonCd 
        {
            get { return _hudTerminationReasonCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _hudTerminationReasonCd = value.Trim().ToUpper();
                else _hudTerminationReasonCd = value;
            }
        }
    
        [XmlElement(IsNullable = true)]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "HudTerminationDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? HudTerminationDt { get; set; }

        string _hudOutcomeCd;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0305, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Hud Outcome code", Ruleset = Constant.RULESET_LENGTH)]
        public string HudOutcomeCd
        {
            get { return _hudOutcomeCd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _hudOutcomeCd = value.Trim().ToUpper();
                else _hudOutcomeCd = value;
            }
        }

        [XmlIgnore]
        public int? AmiPercentage { get; set; }

        string _counselingDurationCd;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0306, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Counseling Duration Code", Ruleset = Constant.RULESET_LENGTH)]
        public string CounselingDurationCd
        {
            get { return _counselingDurationCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _counselingDurationCd = value.Trim().ToUpper();
                else _counselingDurationCd = value;
            }
        }

        string _genderCd;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0307, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Gender Code", Ruleset = Constant.RULESET_LENGTH)]
        public string GenderCd 
        {
            get { return _genderCd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _genderCd = value.Trim().ToUpper();
                else _genderCd = value;
            }
        }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0105, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "Borrower First Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0003)]
        public string BorrowerFname { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0106, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "Borrower Last Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0004)]
        public string BorrowerLname { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Borrower M Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0005)]
        public string BorrowerMname { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Mother Maiden Last Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0006)]
        public string MotherMaidenLname { get; set; }

        [NullableOrStringLengthValidator(true, 9, "Borrower SSN", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0007)]
        public string BorrowerSsn { get; set; }

        [XmlIgnore]
        [NullableOrStringLengthValidator(true, 4, "Borrower Last 4 SSN", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0008)]
        public string BorrowerLast4Ssn { get; set; }

        [XmlElement(IsNullable = true)]
        [RequiredObjectValidator(Tag = ErrorMessages.WARN0308, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "BorrowerDob must be between 1/1/1753 and 12/31/9999")]
        public DateTime? BorrowerDob { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Co-Borrower F Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0009)]
        public string CoBorrowerFname { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Co-Borrwer L Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0010)]
        public string CoBorrowerLname { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Co-Borrower M Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0011)]
        public string CoBorrowerMname { get; set; }

        [NullableOrStringLengthValidator(true, 9, "Co-Borrower SSN", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerSsn { get; set; }

        [XmlIgnore]
        [NullableOrStringLengthValidator(true, 4, "Co-Borrower Last 4 SSN", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0012)]
        public string CoBorrowerLast4Ssn { get; set; }

        [XmlElement(IsNullable = true)]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "CoBorrowerDob must be between 1/1/1753 and 12/31/9999")]
        public DateTime? CoBorrowerDob { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0107, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 20, "Primary Contact Number", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0013)]
        public string PrimaryContactNo { get; set; }

        [NullableOrStringLengthValidator(true, 20, "Second Contact Number", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0014)]
        public string SecondContactNo { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Email 1", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0015)]
        public string Email1 { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Email 2", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0016)]
        public string Email2 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0108, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 50, "Contact Address 1", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0017)]
        public string ContactAddr1 { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Contact Address 2", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0018)]
        public string ContactAddr2 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0109, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "Contact City", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0019)]
        public string ContactCity { get; set; }

        string _contactStateCd;
        [StringRequiredValidator(Tag = ErrorMessages.ERR0110, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Contact State Code", Ruleset = Constant.RULESET_LENGTH)]
        public string ContactStateCd
        {
            get { return _contactStateCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _contactStateCd = value.Trim().ToUpper();
                else _contactStateCd = value;
            }
        }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0111, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 5, "Contact Zip", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0020)]
        public string ContactZip { get; set; }

        [NullableOrStringLengthValidator(true, 4, "Contact Zip Plus 4", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0021)]
        public string ContactZipPlus4 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0112, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 50, "Prop Address 1", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0022)]
        public string PropAddr1 { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Prop Address 2", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0023)]
        public string PropAddr2 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0113, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "Prop City", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0024)]
        public string PropCity { get; set; }

        string _propStateCd;
        [StringRequiredValidator(Tag = ErrorMessages.ERR0114, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Prop State Code", Ruleset = Constant.RULESET_LENGTH)]
        public string PropStateCd
        {
            get { return _propStateCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _propStateCd = value.Trim().ToUpper();
                else _propStateCd = value;
            }
        }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0115, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 5, "Prop Zip", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0025)]
        public string PropZip { get; set; }

        [NullableOrStringLengthValidator(true, 4, "Prop Zip Plus 4", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0026)]
        public string PropZipPlus4 { get; set; }

        private string bankruptcyInd = null;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0309, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0027)]        
        public string BankruptcyInd 
        {   
            get{return bankruptcyInd;}
            set 
            { 
                if (!string.IsNullOrEmpty(value))
                    bankruptcyInd = value.ToUpper().Trim();
                else bankruptcyInd = value;
            } 
        }

        [NullableOrStringLengthValidator(true, 50, "Bankruptcy Attorney", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0028)]
        public string BankruptcyAttorney { get; set; }

        private string bankruptcyPmtCurrentInd = null;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0029)]
        public string BankruptcyPmtCurrentInd
        {
            get{return bankruptcyPmtCurrentInd;}
            set
            {                
                if (!string.IsNullOrEmpty(value))                
                    bankruptcyPmtCurrentInd = value.ToUpper().Trim();
                else bankruptcyPmtCurrentInd = value;
            }
        }

        string _borrowerEducLevelCompletedCd;
        [NullableOrStringLengthValidator(true, 15, "Borrower Educucation Level Completed Code", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerEducLevelCompletedCd
        {
            get { return _borrowerEducLevelCompletedCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _borrowerEducLevelCompletedCd = value.Trim().ToUpper();
                else _borrowerEducLevelCompletedCd = value;
            }
        }

        string _borrowerMaritalStatusCd;
        [NullableOrStringLengthValidator(true, 15, "Borrower Marital Status Code", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerMaritalStatusCd
        {
            get { return _borrowerMaritalStatusCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _borrowerMaritalStatusCd = value.Trim().ToUpper();
                else _borrowerMaritalStatusCd = value;
            }
        }

        string _borrowerPreferredLangCd;
        [NullableOrStringLengthValidator(true, 15, "Borrower Preferred Language Code", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerPreferredLangCd
        {
            get { return _borrowerPreferredLangCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _borrowerPreferredLangCd = value.Trim().ToUpper();
                else _borrowerPreferredLangCd = value;
            }
        }

        [NullableOrStringLengthValidator(true, 50, "Borrower Occupation", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerOccupation { get; set; }

        [NullableOrStringLengthValidator(true, 50, "CoBorrower Occupation", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerOccupation { get; set; }

        private string hispanicInd = null;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0310, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0030)]
        public string HispanicInd
        {
            get{return hispanicInd;}
            set
            {
                if (!string.IsNullOrEmpty(value))
                    hispanicInd = value.ToUpper().Trim();
                else hispanicInd = value;
            }
        }
                
        private string duplicateInd = null;
        [XmlIgnore]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string DuplicateInd
        {
            get { return duplicateInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    duplicateInd = value.ToUpper().Trim();
                else duplicateInd = value;
            }
        }

        private string fcNoticeReceiveInd = null;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0311, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0031)]
        public string FcNoticeReceiveInd
        {
            get { return fcNoticeReceiveInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    fcNoticeReceiveInd = value.ToUpper().Trim();
                else fcNoticeReceiveInd = value;
            }
        }

        private string fundingConsentInd = null;
        [StringRequiredValidator(Tag = ErrorMessages.ERR0116, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0033)]
        public string FundingConsentInd
        {
            get { return fundingConsentInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    fundingConsentInd = value.ToUpper().Trim();
                else fundingConsentInd = value;
            }
        }

        private string servicerConsentInd = null;
        [StringRequiredValidator(Tag = ErrorMessages.ERR0117, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0032)]
        public string ServicerConsentInd
        {
            get { return servicerConsentInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    servicerConsentInd = value.ToUpper().Trim();
                else servicerConsentInd = value;
            }
        }

        private string agencyMediaInterestInd = null;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0034)]
        public string AgencyMediaInterestInd
        {
            get { return agencyMediaInterestInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    agencyMediaInterestInd = value.ToUpper().Trim();
                else agencyMediaInterestInd = value;
            }
        }
        
        private string hpfMediaCandidateInd = null;
        [XmlIgnore]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string HpfMediaCandidateInd
        {
            get { return hpfMediaCandidateInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    hpfMediaCandidateInd = value.ToUpper().Trim();
                else hpfMediaCandidateInd = value;
            }
        }

        private string hpfSuccessStoryInd = null;
        [XmlIgnore]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string HpfSuccessStoryInd
        {
            get { return hpfSuccessStoryInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    hpfSuccessStoryInd = value.ToUpper().Trim();
                else hpfSuccessStoryInd = value;
            }
        }

        private string agencySuccessStoryInd = null;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0035)]
        public string AgencySuccessStoryInd
        {
            get { return agencySuccessStoryInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    agencySuccessStoryInd = value.ToUpper().Trim();
                else agencySuccessStoryInd = value;
            }
        }

        private string borrowerDisabledInd = null;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0036)]
        public string BorrowerDisabledInd
        {
            get { return borrowerDisabledInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    borrowerDisabledInd = value.ToUpper().Trim();
                else borrowerDisabledInd = value;
            }
        }

        private string coBorrowerDisabledInd = null;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0037)]
        public string CoBorrowerDisabledInd
        {
            get { return coBorrowerDisabledInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    coBorrowerDisabledInd = value.ToUpper().Trim();
                else coBorrowerDisabledInd = value;
            }
        }

        string _summarySentOtherCd;
        [NullableOrStringLengthValidator(true, 15, "Summary Sent Other Code", Ruleset = Constant.RULESET_LENGTH)]
        public string SummarySentOtherCd
        {
            get { return _summarySentOtherCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _summarySentOtherCd = value.Trim().ToUpper();
                else _summarySentOtherCd = value;
            }
        }

        [XmlElement(IsNullable = true)]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "SummarySentOtherDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? SummarySentOtherDt { get; set; }

        [XmlIgnore]
        public DateTime? SummarySentDt { get; set; }

        [XmlElement(IsNullable = true)]        
        [RequiredObjectValidator(Tag = ErrorMessages.WARN0312, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        public byte? OccupantNum { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN0313, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 8000, "Loan Default Reason Notes", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0038)]
        public string LoanDfltReasonNotes { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN0314, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 8000, "Action Items Notes", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0039)]
        public string ActionItemsNotes { get; set; }

        [NullableOrStringLengthValidator(true, 8000, "Followup Notes", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0040)]
        public string FollowupNotes { get; set; }

        [XmlElement(IsNullable = true)]        
        [NullableOrInRangeNumberValidator(true ,"-9999999999999.99", "9999999999999.99",Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0072)]
        public double? PrimResEstMktValue { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0118, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "Assigned Counselor Id Referrence", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0041)]
        public string AssignedCounselorIdRef { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0119, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "Counselor F Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0042)]
        public string CounselorFname { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0120, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "Counselor L name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0043)]
        public string CounselorLname { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0121, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 50, "Counselor Email", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0044)]
        public string CounselorEmail { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0122, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 20, "Counselor Phone", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0045)]
        public string CounselorPhone { get; set; }

        [NullableOrStringLengthValidator(true, 20, "Counselor Ext", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0046)]
        public string CounselorExt { get; set; }

        private string discussedSolutionWithSrvcrInd = null;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0315, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0047)]
        public string DiscussedSolutionWithSrvcrInd
        {
            get { return discussedSolutionWithSrvcrInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    discussedSolutionWithSrvcrInd = value.ToUpper().Trim();
                else discussedSolutionWithSrvcrInd = value;
            }
        }

        private string workedWithAnotherAgencyInd = null;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0316, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0048)]
        public string WorkedWithAnotherAgencyInd
        {
            get { return workedWithAnotherAgencyInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    workedWithAnotherAgencyInd = value.ToUpper().Trim();
                else workedWithAnotherAgencyInd = value;
            }
        }

        private string contactedSrvcrRecentlyInd = null;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0317, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0049)]
        public string ContactedSrvcrRecentlyInd
        {
            get { return contactedSrvcrRecentlyInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    contactedSrvcrRecentlyInd = value.ToUpper().Trim();
                else contactedSrvcrRecentlyInd = value;
            }
        }

        private string hasWorkoutPlanInd = null;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0318, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0050)]
        public string HasWorkoutPlanInd
        {
            get { return hasWorkoutPlanInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    hasWorkoutPlanInd = value.ToUpper().Trim();
                else hasWorkoutPlanInd = value;
            }
        }

        private string srvcrWorkoutPlanCurrentInd = null;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0051)]
        public string SrvcrWorkoutPlanCurrentInd
        {
            get { return srvcrWorkoutPlanCurrentInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    srvcrWorkoutPlanCurrentInd = value.ToUpper().Trim();
                else srvcrWorkoutPlanCurrentInd = value;
            }
        }

        private string optOutNewsletterInd = null;
        [XmlIgnore]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string OptOutNewsletterInd
        {
            get { return optOutNewsletterInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    optOutNewsletterInd = value.ToUpper().Trim();
                else optOutNewsletterInd = value;
            }
        }

        private string optOutSurveyInd = null;
        [XmlIgnore]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string OptOutSurveyInd
        {
            get { return optOutSurveyInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    optOutSurveyInd = value.ToUpper().Trim();
                else optOutSurveyInd = value;
            }
        }

        private string doNotCallInd = null;
        [XmlIgnore]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string DoNotCallInd
        {
            get { return doNotCallInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    doNotCallInd = value.ToUpper().Trim();
                else doNotCallInd = value;
            }
        }

        private string ownerOccupiedInd = null;
        [StringRequiredValidator(Tag = ErrorMessages.ERR0123, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0052)]
        public string OwnerOccupiedInd
        {
            get { return ownerOccupiedInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    ownerOccupiedInd = value.ToUpper().Trim();
                else ownerOccupiedInd = value;
            }
        }

        private string primaryResidenceInd = null;
        [StringRequiredValidator(Tag = ErrorMessages.ERR0124, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0053)]
        public string PrimaryResidenceInd
        {
            get { return primaryResidenceInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    primaryResidenceInd = value.ToUpper().Trim();
                else primaryResidenceInd = value;
            }
        }

        [NullableOrStringLengthValidator(true, 50, "Realty Company", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0054)]
        public string RealtyCompany { get; set; }

        string _propertyCd;
        [NullableOrStringLengthValidator(true, 15, "Property Code", Ruleset = Constant.RULESET_LENGTH)]
        public string PropertyCd
        {
            get { return _propertyCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _propertyCd = value.Trim().ToUpper();
                else _propertyCd = value;
            }
        }

        private string forSaleInd = null;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0330, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0055)]
        public string ForSaleInd
        {
            get { return forSaleInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    forSaleInd = value.ToUpper().Trim();
                else forSaleInd = value;
            }
        }

        [XmlElement(IsNullable = true)]        
        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0073)]
        public double? HomeSalePrice { get; set; }

        [XmlElement(IsNullable = true)]
        [NullableOrInRangeNumberValidator(true, "0", "9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "An invalid HomePurchaseYear was provided.")]
        public int? HomePurchaseYear { get; set; }

        [XmlElement(IsNullable = true)]
        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0080)]
        public double? HomePurchasePrice { get; set; }

        [XmlElement(IsNullable = true)]
        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0074)]        
        public double? HomeCurrentMarketValue { get; set; }


        string _militaryServiceCd;
        [NullableOrStringLengthValidator(true, 15, "Military Service Cd", Ruleset = Constant.RULESET_LENGTH)]
        public string MilitaryServiceCd
        {
            get { return _militaryServiceCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _militaryServiceCd = value.Trim().ToUpper();
                else _militaryServiceCd = value;
            }
        }
        [XmlElement(IsNullable = true)]        
        [RequiredObjectValidator(Tag = ErrorMessages.WARN0319, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrInRangeNumberValidator(true, "0.01", "99999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0081)]
        public double? HouseholdGrossAnnualIncomeAmt { get; set; }

        [XmlIgnore]
        [NullableOrStringLengthValidator(true, 50, "Loan List", Ruleset = Constant.RULESET_LENGTH)]
        public string LoanList { get; set; }

        [NullableOrStringLengthValidator(true, 4, "Intake Credit Score", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0056)]
        public string IntakeCreditScore { get; set; }

        string _intakeCreditBureauCd;
        [NullableOrStringLengthValidator(true, 15, "Intake Credit Bureau Code", Ruleset = Constant.RULESET_LENGTH)]
        public string IntakeCreditBureauCd
        {
            get { return _intakeCreditBureauCd; }
            set 
            {
                if (!string.IsNullOrEmpty(value)) _intakeCreditBureauCd = value.Trim().ToUpper();
                else _intakeCreditBureauCd = value;
            }
        }

        [XmlElement(IsNullable = true)]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "FcSaleDate must be between 1/1/1753 and 12/31/9999")]
        public DateTime? FcSaleDate { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0125, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 30, "Change Last User ID", Ruleset = Constant.RULESET_LENGTH)]
        public string ChgLstUserId { get; set; }

        [XmlIgnore]
        public string PropertyDesc{get;set;}
        
        [XmlIgnore]
        public string GenderDesc{get;set;}

        [XmlIgnore]
        public string RaceDesc{get;set;}
        
        [XmlIgnore]
        public string LanguageDesc { get; set; }
        
        [XmlIgnore]
        public string EducationDesc { get; set; }
        
        [XmlIgnore]
        public string MaritalDesc { get; set; }
        
        [XmlIgnore]
        public string MilitaryDesc { get; set; }
        
        [XmlIgnore]
        public string CounselingDesc { get; set; }
        
        [XmlIgnore]
        public string CaseSourceDesc { get; set; }
        
        [XmlIgnore]
        public string SummaryDesc { get; set; }
        
        [XmlIgnore]
        public string DefaultReason1Desc { get; set; }
        
        [XmlIgnore]
        public string DefaultReason2Desc { get; set; }
        
        [XmlIgnore]
        public string HouseholdDesc { get; set; }
        
        [XmlIgnore]
        public string IncomeDesc { get; set; }
        
        [XmlIgnore]
        public string CreditDesc { get; set; }
        
        [XmlIgnore]
        public string HudTerminationDesc { get; set; }
        
        [XmlIgnore]
        public string HudOutcomeDesc { get; set; }
        
        [XmlIgnore]
        public string ProgramName { get; set; }

        string vipInd;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0082)]
        public string VipInd 
        {
            get { return vipInd; }
            set { vipInd = (string.IsNullOrEmpty(value))?null:value.ToUpper();} 
        }
        
        string vipReason;
        [NullableOrStringLengthValidator(true, 100, "Vip Reason", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0083)]
        public string VipReason 
        {
            get { return vipReason; }
            set { vipReason = (string.IsNullOrEmpty(value)) ? null : value; } 
        }
        
        string counseledLanguageCd;        
        public string CounseledLanguageCd
        {
            get { return counseledLanguageCd; }
            set { counseledLanguageCd = (string.IsNullOrEmpty(value)) ? null : value.ToUpper(); }
        }
        
        string ercpOutcomeCd;
        //BUG-439
        //[RequiredObjectValidator(Tag = ErrorMessages.WARN0331, Ruleset = Constant.RULESET_COMPLETE)]
        public string ErcpOutcomeCd 
        {
            get { return ercpOutcomeCd; }
            set { ercpOutcomeCd = (string.IsNullOrEmpty(value)) ? null : value.ToUpper(); }
        }
        
        string counselorContactedSrvcrInd;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0084)]
        public string CounselorContactedSrvcrInd 
        {
            get { return counselorContactedSrvcrInd; }
            set { counselorContactedSrvcrInd = (string.IsNullOrEmpty(value)) ? null : value.ToUpper(); }
        }
        
        [NullableOrInRangeNumberValidator(true, "1", "999", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0085)]
        public int? NumberOfUnits { get; set; }

        string vacantOrCondemedInd;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0086)]
        public string VacantOrCondemedInd 
        {
            get { return vacantOrCondemedInd; }
            set { vacantOrCondemedInd = (string.IsNullOrEmpty(value)) ? null : value.ToUpper(); }
        }

        [NullableOrInRangeNumberValidator(true, "0.1", "999.9", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0087)]
        public double? MortgagePmtRatio { get; set; }

        string certificateId = null;
        [NullableOrStringLengthValidator(true, 10, "CertificateId", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0098)]        
        public string CertificateId 
        {
            get { return certificateId; }
            set { certificateId = string.IsNullOrEmpty(value) ? null : value.Trim(); }
        }
    }
}
