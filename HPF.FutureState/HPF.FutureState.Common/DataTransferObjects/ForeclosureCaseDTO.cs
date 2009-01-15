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
        [NullableOrStringLengthValidator(false , 30, Ruleset = Constant.RULESET_LENGTH)]
        public string AgencyCaseNum { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string AgencyClientNum { get; set; }

        [DateTimeRangeValidator("0001-01-02T00:00:00", "9999-12-31T00:00:00", Tag = ErrorMessages.ERR103, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]        
        public DateTime IntakeDt { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN300, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string IncomeEarnersCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR104, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string CaseSourceCd{ get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN301, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string RaceCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN302, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string HouseholdCd { get; set; }

        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string NeverBillReasonCd { get; set; }

        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string NeverPayReasonCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN303, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string DfltReason1stCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN304, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string DfltReason2ndCd { get; set; }

        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string HudTerminationReasonCd { get; set; }
        
        public DateTime HudTerminationDt { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN305, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string HudOutcomeCd { get; set; }

        public int AmiPercentage { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN306, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string CounselingDurationCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN307, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string GenderCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR105, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]        
        [NullableOrStringLengthValidator(false, 30, Ruleset = Constant.RULESET_LENGTH)]        
        public string BorrowerFname { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR106, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerLname { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerMname { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string MotherMaidenLname { get; set; }

        [NullableOrStringLengthValidator(true, 9, Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerSsn { get; set; }

        [NullableOrStringLengthValidator(true, 4, Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerLast4Ssn { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN308, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]        
        public DateTime BorrowerDob { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerFname { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerLname { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerMname { get; set; }
        
        [NullableOrStringLengthValidator(true, 9, Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerSsn { get; set; }
        
        [NullableOrStringLengthValidator(true, 4, Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerLast4Ssn { get; set; }

        public DateTime CoBorrowerDob { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR107, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 20, Ruleset = Constant.RULESET_LENGTH)]
        public string PrimaryContactNo { get; set; }
        
        [NullableOrStringLengthValidator(true, 20, Ruleset = Constant.RULESET_LENGTH)]
        public string SecondContactNo { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = Constant.RULESET_LENGTH)]
        public string Email1 { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = Constant.RULESET_LENGTH)]
        public string Email2 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR108, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 50, Ruleset = Constant.RULESET_LENGTH)]
        public string ContactAddr1 { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = Constant.RULESET_LENGTH)]
        public string ContactAddr2 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR109, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string ContactCity { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR110, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string ContactStateCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR111, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 5, Ruleset = Constant.RULESET_LENGTH)]
        public string ContactZip { get; set; }

        [NullableOrStringLengthValidator(true, 4, Ruleset = Constant.RULESET_LENGTH)]
        public string ContactZipPlus4 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR112, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 50, Ruleset = Constant.RULESET_LENGTH)]
        public string PropAddr1 { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = Constant.RULESET_LENGTH)]
        public string PropAddr2 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR113, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string PropCity { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR114, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string PropStateCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR115, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 5, Ruleset = Constant.RULESET_LENGTH)]
        public string PropZip { get; set; }

        [NullableOrStringLengthValidator(true, 4, Ruleset = Constant.RULESET_LENGTH)]
        public string PropZipPlus4 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN309, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string BankruptcyInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = Constant.RULESET_LENGTH)]
        public string BankruptcyAttorney { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string BankruptcyPmtCurrentInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerEducLevelCompletedCd { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerMaritalStatusCd { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerPreferredLangCd { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerOccupationCd { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerOccupationCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN310, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string HispanicInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string DuplicateInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN311, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string FcNoticeReceiveInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string CaseCompleteInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR116, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string FundingConsentInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR117, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string ServicerConsentInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string AgencyMediaConsentInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string HpfMediaCandidateInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string HpfNetworkCandidateInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string HpfSuccessStoryInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string AgencySuccessStoryInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerDisabledInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerDisabledInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string SummarySentOtherCd { get; set; }

        public DateTime SummarySentOtherDt { get; set; }

        public DateTime SummarySentDt { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN312, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        public byte OccupantNum { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN313, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 8000, Ruleset = Constant.RULESET_LENGTH)]
        public string LoanDfltReasonNotes { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN314, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 8000, Ruleset = Constant.RULESET_LENGTH)]
        public string ActionItemsNotes { get; set; }
        
        [NullableOrStringLengthValidator(true, 8000, Ruleset = Constant.RULESET_LENGTH)]
        public string FollowupNotes { get; set; }
        
        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH)]
        public double PrimResEstMktValue { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR118, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string AssignedCounselorIdRef{ get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR119, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string CounselorFname { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR120, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = Constant.RULESET_LENGTH)]
        public string CounselorLname { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR121, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 50, Ruleset = Constant.RULESET_LENGTH)]
        public string CounselorEmail { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR122, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 20, Ruleset = Constant.RULESET_LENGTH)]        
        public string CounselorPhone { get; set; }
        
        [NullableOrStringLengthValidator(true, 20, Ruleset = Constant.RULESET_LENGTH)]                
        public string CounselorExt { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN315, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string DiscussedSolutionWithSrvcrInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN316, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string WorkedWithAnotherAgencyInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN317, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string ContactedSrvcrRecentlyInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.WARN318, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string HasWorkoutPlanInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string SrvcrWorkoutPlanCurrentInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string OptOutNewsletterInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string OptOutSurveyInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string DoNotCallInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR123, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string OwnerOccupiedInd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR124, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]        
        [NullableOrStringLengthValidator(false, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string PrimaryResidenceInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = Constant.RULESET_LENGTH)]
        public string RealtyCompany { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string PropertyCd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = Constant.RULESET_LENGTH)]
        public string ForSaleInd { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH)]
        public double HomeSalePrice { get; set; }

        public int HomePurchaseYear { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH)]
        public double HomePurchasePrice { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH)]
        public double HomeCurrentMarketValue { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string MilitaryServiceCd { get; set; }

        [NotNullValidator(Tag = ErrorMessages.WARN319, Ruleset = Constant.RULESET_COMPLETE, MessageTemplate = "Required!")]
        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH)]
        public double HouseholdGrossAnnualIncomeAmt{ get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = Constant.RULESET_LENGTH)]
        public string LoanList { get; set; }
        
        [NullableOrStringLengthValidator(true, 4, Ruleset = Constant.RULESET_LENGTH)]
        public string IntakeCreditScore { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = Constant.RULESET_LENGTH)]
        public string IntakeCreditBureauCd { get; set; }
        
        public DateTime FcSaleDate { get; set; }
    }
}
