using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using HPF.FutureState.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class ForeclosureCaseDTO : BaseDTO
    {
        public int FcId { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Tag = ErrorMessages.ERR100, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public int AgencyId { get; set; }

        public DateTime CompletedDt { get; set; }

        public int CallId { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Tag = ErrorMessages.ERR101, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public int ProgramId { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR102, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, "Agency Case Number", Ruleset = Constant.RULESET_LENGTH)]
        public string AgencyCaseNum { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Agency Client Number", Ruleset = Constant.RULESET_LENGTH)]
        public string AgencyClientNum { get; set; }

        [DateTimeRangeValidator("0001-01-02T00:00:00", "9999-12-31T00:00:00", Tag = ErrorMessages.ERR103, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public DateTime IntakeDt { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN300, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Income Earner Code", Ruleset = Constant.RULESET_LENGTH)]
        public string IncomeEarnersCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR104, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 15, "Case Source Code", Ruleset = Constant.RULESET_LENGTH)]
        public string CaseSourceCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN301, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Race Code", Ruleset = Constant.RULESET_LENGTH)]
        public string RaceCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN302, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Household Code", Ruleset = Constant.RULESET_LENGTH)]
        public string HouseholdCd { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Never Bill Reason Code", Ruleset = Constant.RULESET_LENGTH)]
        public string NeverBillReasonCd { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Never Pay Reason Code", Ruleset = Constant.RULESET_LENGTH)]
        public string NeverPayReasonCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN303, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Default Reason 1st Code", Ruleset = Constant.RULESET_LENGTH)]
        public string DfltReason1stCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN304, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Default Reason 2nd Code", Ruleset = Constant.RULESET_LENGTH)]
        public string DfltReason2ndCd { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Hud Termination Reason Code", Ruleset = Constant.RULESET_LENGTH)]
        public string HudTerminationReasonCd { get; set; }

        public DateTime HudTerminationDt { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN305, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Hud Outcome code", Ruleset = Constant.RULESET_LENGTH)]
        public string HudOutcomeCd { get; set; }

        public int AmiPercentage { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN306, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Counseling Duration Code", Ruleset = Constant.RULESET_LENGTH)]
        public string CounselingDurationCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN307, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "Gender Code", Ruleset = Constant.RULESET_LENGTH)]
        public string GenderCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR105, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, "Borrower First Name", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerFname { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR106, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, "Borrower Last Name", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerLname { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Borrower M Name", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerMname { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Mother Maiden Last Name", Ruleset = Constant.RULESET_LENGTH)]
        public string MotherMaidenLname { get; set; }

        [NullableOrStringLengthValidator(true, 9, "Borrower SSN", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerSsn { get; set; }

        [NullableOrStringLengthValidator(true, 4, "Borrower Last 4 SSN", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerLast4Ssn { get; set; }
        
        [DateTimeRangeValidator("0001-01-02T00:00:00", "9999-12-31T00:00:00", Tag = ErrorMessages.WARN308, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        public DateTime BorrowerDob { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Co-Borrower F Name",  Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerFname { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Co-Borrwer L Name", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerLname { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Co-Borrower M Name", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerMname { get; set; }

        [NullableOrStringLengthValidator(true, 9, "Co-Borrower SSN", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerSsn { get; set; }

        [NullableOrStringLengthValidator(true, 4, "Co-Borrower Last 4 SSN", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerLast4Ssn { get; set; }

        public DateTime CoBorrowerDob { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR107, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 20, "Primary Contact Number", Ruleset = Constant.RULESET_LENGTH)]
        public string PrimaryContactNo { get; set; }

        [NullableOrStringLengthValidator(true, 20, "Second Contact Number", Ruleset = Constant.RULESET_LENGTH)]
        public string SecondContactNo { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Email 1", Ruleset = Constant.RULESET_LENGTH)]
        public string Email1 { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Email 2", Ruleset = Constant.RULESET_LENGTH)]
        public string Email2 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR108, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 50, "Contact Address 1", Ruleset = Constant.RULESET_LENGTH)]
        public string ContactAddr1 { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Contact Address 2", Ruleset = Constant.RULESET_LENGTH)]
        public string ContactAddr2 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR109, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, "Contact City", Ruleset = Constant.RULESET_LENGTH)]
        public string ContactCity { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR110, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 15, "Contact State Code", Ruleset = Constant.RULESET_LENGTH)]
        public string ContactStateCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR111, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 5, "Contact Zip", Ruleset = Constant.RULESET_LENGTH)]
        public string ContactZip { get; set; }

        [NullableOrStringLengthValidator(true, 4, "Contact Zip Plus 4", Ruleset = Constant.RULESET_LENGTH)]
        public string ContactZipPlus4 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR112, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 50, "Prop Address 1", Ruleset = Constant.RULESET_LENGTH)]
        public string PropAddr1 { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Prop Address 2", Ruleset = Constant.RULESET_LENGTH)]
        public string PropAddr2 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR113, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, "Prop City", Ruleset = Constant.RULESET_LENGTH)]
        public string PropCity { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR114, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 15, "Prop State Code", Ruleset = Constant.RULESET_LENGTH)]
        public string PropStateCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR115, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 5, "Prop Zip", Ruleset = Constant.RULESET_LENGTH)]
        public string PropZip { get; set; }

        [NullableOrStringLengthValidator(true, 4, "Prop Zip Plus 4", Ruleset = Constant.RULESET_LENGTH)]
        public string PropZipPlus4 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN309, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string BankruptcyInd { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Bankruptcy Attorney", Ruleset = Constant.RULESET_LENGTH)]
        public string BankruptcyAttorney { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string BankruptcyPmtCurrentInd { get; set; }

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

        [StringRequiredValidator(Tag = ErrorMessages.WARN310, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string HispanicInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string DuplicateInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN311, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string FcNoticeReceiveInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR116, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string FundingConsentInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR117, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string ServicerConsentInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string AgencyMediaInterestInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string HpfMediaCandidateInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string HpfSuccessStoryInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string AgencySuccessStoryInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerDisabledInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerDisabledInd { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Summary Sent Other Code", Ruleset = Constant.RULESET_LENGTH)]
        public string SummarySentOtherCd { get; set; }

        public DateTime SummarySentOtherDt { get; set; }

        public DateTime SummarySentDt { get; set; }

        [NotNullValidator(Tag = ErrorMessages.WARN312, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        public byte OccupantNum { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN313, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 8000, "Loan Default Reason Notes", Ruleset = Constant.RULESET_LENGTH)]
        public string LoanDfltReasonNotes { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN314, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 8000, "Action Items Notes", Ruleset = Constant.RULESET_LENGTH)]
        public string ActionItemsNotes { get; set; }

        [NullableOrStringLengthValidator(true, 8000, "Followup Notes", Ruleset = Constant.RULESET_LENGTH)]
        public string FollowupNotes { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "PrimResEstMktValue must be numeric(15,2)")]
        public double PrimResEstMktValue { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR118, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, "Assigned Counselor Id Referrence", Ruleset = Constant.RULESET_LENGTH)]
        public string AssignedCounselorIdRef { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR119, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, "Counselor F Name", Ruleset = Constant.RULESET_LENGTH)]
        public string CounselorFname { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR120, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, "Counselor L name", Ruleset = Constant.RULESET_LENGTH)]
        public string CounselorLname { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR121, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 50, "Counselor Email", Ruleset = Constant.RULESET_LENGTH)]
        public string CounselorEmail { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR122, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 20, "Counselor Phone", Ruleset = Constant.RULESET_LENGTH)]
        public string CounselorPhone { get; set; }

        [NullableOrStringLengthValidator(true, 20, "Counselor Ext", Ruleset = Constant.RULESET_LENGTH)]
        public string CounselorExt { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN315, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string DiscussedSolutionWithSrvcrInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN316, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string WorkedWithAnotherAgencyInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN317, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string ContactedSrvcrRecentlyInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN318, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string HasWorkoutPlanInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string SrvcrWorkoutPlanCurrentInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string OptOutNewsletterInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string OptOutSurveyInd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string DoNotCallInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR123, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string OwnerOccupiedInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR124, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string PrimaryResidenceInd { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Realty Company", Ruleset = Constant.RULESET_LENGTH)]
        public string RealtyCompany { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Property Code", Ruleset = Constant.RULESET_LENGTH)]
        public string PropertyCd { get; set; }

        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string ForSaleInd { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "HomeSalePrice must be numeric(15,2)")]
        public double HomeSalePrice { get; set; }

        public int HomePurchaseYear { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "HomePurchasePrice must be numeric(15,2)")]
        public double HomePurchasePrice { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "HomeCurrentMarketValue must be numeric(15,2)")]
        public double HomeCurrentMarketValue { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Military Service Cd", Ruleset = Constant.RULESET_LENGTH)]
        public string MilitaryServiceCd { get; set; }

        [NotNullValidator(Tag = ErrorMessages.WARN319, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "HouseholdGrossAnnualIncomeAmt must be numeric(15,2)")]
        public double HouseholdGrossAnnualIncomeAmt { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Loan List", Ruleset = Constant.RULESET_LENGTH)]
        public string LoanList { get; set; }

        [NullableOrStringLengthValidator(true, 4, "Intake Credit Score", Ruleset = Constant.RULESET_LENGTH)]
        public string IntakeCreditScore { get; set; }

        [NullableOrStringLengthValidator(true, 15, "Intake Credit Bureau Code", Ruleset = Constant.RULESET_LENGTH)]
        public string IntakeCreditBureauCd { get; set; }

        public DateTime FcSaleDate { get; set; }
    }
}
