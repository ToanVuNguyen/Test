﻿using System;
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
        [NullableOrStringLengthValidator(false, 30, "Agency Case Number", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0001)]        
        public string AgencyCaseNum { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Agency Client Number", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0002)]
        public string AgencyClientNum { get; set; }

        [XmlElement(IsNullable = true)]        
        [RequiredObjectValidator(Tag = ErrorMessages.ERR0103, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrInRangeNumberValidator(false,"1-1-1753","12-31-9999",Ruleset = Constant.RULESET_LENGTH, MessageTemplate= "IntakeDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? IntakeDt { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN0300, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Income Earner Code", Ruleset = Constant.RULESET_LENGTH)]
        public string IncomeEarnersCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0104, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 15, "Case Source Code", Ruleset = Constant.RULESET_LENGTH)]
        public string CaseSourceCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN0301, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Race Code", Ruleset = Constant.RULESET_LENGTH)]
        public string RaceCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN0302, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Household Code", Ruleset = Constant.RULESET_LENGTH)]
        public string HouseholdCd { get; set; }

        [XmlIgnore]
        [NullableOrStringLengthValidator(true, 15, "Never Bill Reason Code", Ruleset = Constant.RULESET_LENGTH)]
        public string NeverBillReasonCd { get; set; }

        [XmlIgnore]
        [NullableOrStringLengthValidator(true, 15, "Never Pay Reason Code", Ruleset = Constant.RULESET_LENGTH)]
        public string NeverPayReasonCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN0303, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Default Reason 1st Code", Ruleset = Constant.RULESET_LENGTH)]
        public string DfltReason1stCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN0304, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Default Reason 2nd Code", Ruleset = Constant.RULESET_LENGTH)]
        public string DfltReason2ndCd { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Hud Termination Reason Code", Ruleset = Constant.RULESET_LENGTH)]
        public string HudTerminationReasonCd { get; set; }

        [XmlElement(IsNullable = true)]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "HudTerminationDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? HudTerminationDt { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN0305, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Hud Outcome code", Ruleset = Constant.RULESET_LENGTH)]
        public string HudOutcomeCd { get; set; }

        [XmlIgnore]
        public int? AmiPercentage { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN0306, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Counseling Duration Code", Ruleset = Constant.RULESET_LENGTH)]
        public string CounselingDurationCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN0307, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Gender Code", Ruleset = Constant.RULESET_LENGTH)]
        public string GenderCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0105, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, "Borrower First Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0003)]
        public string BorrowerFname { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0106, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, "Borrower Last Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0004)]
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
        [NullableOrStringLengthValidator(false, 20, "Primary Contact Number", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0013)]
        public string PrimaryContactNo { get; set; }

        [NullableOrStringLengthValidator(true, 20, "Second Contact Number", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0014)]
        public string SecondContactNo { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Email 1", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0015)]
        public string Email1 { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Email 2", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0016)]
        public string Email2 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0108, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 50, "Contact Address 1", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0017)]
        public string ContactAddr1 { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Contact Address 2", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0018)]
        public string ContactAddr2 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0109, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, "Contact City", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0019)]
        public string ContactCity { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0110, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 15, "Contact State Code", Ruleset = Constant.RULESET_LENGTH)]
        public string ContactStateCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0111, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 5, "Contact Zip", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0020)]
        public string ContactZip { get; set; }

        [NullableOrStringLengthValidator(true, 4, "Contact Zip Plus 4", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0021)]
        public string ContactZipPlus4 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0112, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 50, "Prop Address 1", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0022)]
        public string PropAddr1 { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Prop Address 2", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0023)]
        public string PropAddr2 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0113, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, "Prop City", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0024)]
        public string PropCity { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0114, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 15, "Prop State Code", Ruleset = Constant.RULESET_LENGTH)]
        public string PropStateCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0115, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 5, "Prop Zip", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0025)]
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
                if (value != null && value != string.Empty)
                    bankruptcyInd = value.ToUpper().Trim();
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
                if (value != null && value != string.Empty)                
                    bankruptcyPmtCurrentInd = value.ToUpper().Trim();                
            }
        }

        [NullableOrStringLengthValidator(true, 15, "Borrower Educucation Level Completed Code", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerEducLevelCompletedCd { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Borrower Marital Status Code", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerMaritalStatusCd { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Borrower Preferred Language Code", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerPreferredLangCd { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Borrower Occupation Code", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerOccupationCd { get; set; }

        [NullableOrStringLengthValidator(true, 50, "CoBorrower Occupation Code", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerOccupationCd { get; set; }

        private string hispanicInd = null;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0310, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0030)]
        public string HispanicInd
        {
            get{return hispanicInd;}
            set
            {
                if (value != null && value != string.Empty)
                    hispanicInd = value.ToUpper().Trim();
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
                if (value != null && value != string.Empty)
                    duplicateInd = value.ToUpper().Trim();
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
                if (value != null && value != string.Empty)
                    fcNoticeReceiveInd = value.ToUpper().Trim();
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
                if (value != null && value != string.Empty)
                    fundingConsentInd = value.ToUpper().Trim();
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
                if (value != null && value != string.Empty)
                    servicerConsentInd = value.ToUpper().Trim();
            }
        }

        private string agencyMediaInterestInd = null;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0034)]
        public string AgencyMediaInterestInd
        {
            get { return agencyMediaInterestInd; }
            set
            {
                if (value != null && value != string.Empty)
                    agencyMediaInterestInd = value.ToUpper().Trim();
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
                if (value != null && value != string.Empty)
                    hpfMediaCandidateInd = value.ToUpper().Trim();
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
                if (value != null && value != string.Empty)
                    hpfSuccessStoryInd = value.ToUpper().Trim();
            }
        }

        private string agencySuccessStoryInd = null;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0035)]
        public string AgencySuccessStoryInd
        {
            get { return agencySuccessStoryInd; }
            set
            {
                if (value != null && value != string.Empty)
                    agencySuccessStoryInd = value.ToUpper().Trim();
            }
        }

        private string borrowerDisabledInd = null;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0036)]
        public string BorrowerDisabledInd
        {
            get { return borrowerDisabledInd; }
            set
            {
                if (value != null && value != string.Empty)
                    borrowerDisabledInd = value.ToUpper().Trim();
            }
        }

        private string coBorrowerDisabledInd = null;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0037)]
        public string CoBorrowerDisabledInd
        {
            get { return coBorrowerDisabledInd; }
            set
            {
                if (value != null && value != string.Empty)
                    coBorrowerDisabledInd = value.ToUpper().Trim();
            }
        }

        [NullableOrStringLengthValidator(true, 15, "Summary Sent Other Code", Ruleset = Constant.RULESET_LENGTH)]
        public string SummarySentOtherCd { get; set; }

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
        [NullableOrStringLengthValidator(false, 30, "Assigned Counselor Id Referrence", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0041)]
        public string AssignedCounselorIdRef { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0119, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, "Counselor F Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0042)]
        public string CounselorFname { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0120, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, "Counselor L name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0043)]
        public string CounselorLname { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0121, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 50, "Counselor Email", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0044)]
        public string CounselorEmail { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0122, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 20, "Counselor Phone", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0045)]
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
                if (value != null && value != string.Empty)
                    discussedSolutionWithSrvcrInd = value.ToUpper().Trim();
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
                if (value != null && value != string.Empty)
                    workedWithAnotherAgencyInd = value.ToUpper().Trim();
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
                if (value != null && value != string.Empty)
                    contactedSrvcrRecentlyInd = value.ToUpper().Trim();
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
                if (value != null && value != string.Empty)
                    hasWorkoutPlanInd = value.ToUpper().Trim();
            }
        }

        private string srvcrWorkoutPlanCurrentInd = null;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0051)]
        public string SrvcrWorkoutPlanCurrentInd
        {
            get { return srvcrWorkoutPlanCurrentInd; }
            set
            {
                if (value != null && value != string.Empty)
                    srvcrWorkoutPlanCurrentInd = value.ToUpper().Trim();
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
                if (value != null && value != string.Empty)
                    optOutNewsletterInd = value.ToUpper().Trim();
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
                if (value != null && value != string.Empty)
                    optOutSurveyInd = value.ToUpper().Trim();
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
                if (value != null && value != string.Empty)
                    doNotCallInd = value.ToUpper().Trim();
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
                if (value != null && value != string.Empty)
                    ownerOccupiedInd = value.ToUpper().Trim();
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
                if (value != null && value != string.Empty)
                    primaryResidenceInd = value.ToUpper().Trim();
            }
        }

        [NullableOrStringLengthValidator(true, 50, "Realty Company", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0054)]
        public string RealtyCompany { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Property Code", Ruleset = Constant.RULESET_LENGTH)]
        public string PropertyCd { get; set; }

        private string forSaleInd = null;
        [StringRequiredValidator(Tag = ErrorMessages.WARN0330, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0055)]
        public string ForSaleInd
        {
            get { return forSaleInd; }
            set
            {
                if (value != null && value != string.Empty)
                    forSaleInd = value.ToUpper().Trim();
            }
        }

        [XmlElement(IsNullable = true)]        
        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0073)]
        public double? HomeSalePrice { get; set; }

        [XmlElement(IsNullable = true)]
        public int? HomePurchaseYear { get; set; }

        [XmlElement(IsNullable = true)]
        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0080)]
        public double? HomePurchasePrice { get; set; }

        [XmlElement(IsNullable = true)]
        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0074)]        
        public double? HomeCurrentMarketValue { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Military Service Cd", Ruleset = Constant.RULESET_LENGTH)]
        public string MilitaryServiceCd { get; set; }

        [XmlElement(IsNullable = true)]        
        [RequiredObjectValidator(Tag = ErrorMessages.WARN0319, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0075)]
        public double? HouseholdGrossAnnualIncomeAmt { get; set; }

        [XmlIgnore]
        [NullableOrStringLengthValidator(true, 50, "Loan List", Ruleset = Constant.RULESET_LENGTH)]
        public string LoanList { get; set; }

        [NullableOrStringLengthValidator(true, 4, "Intake Credit Score", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0056)]
        public string IntakeCreditScore { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Intake Credit Bureau Code", Ruleset = Constant.RULESET_LENGTH)]
        public string IntakeCreditBureauCd { get; set; }

        [XmlElement(IsNullable = true)]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "FcSaleDate must be between 1/1/1753 and 12/31/9999")]
        public DateTime? FcSaleDate { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0125, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(false, 30, "Working User ID", Ruleset = Constant.RULESET_LENGTH)]
        public string ChangeUserID { get; set; }
    }
}
