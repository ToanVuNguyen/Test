using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation;
namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class ForeclosureCaseDTO : BaseDTO
    {        
        private const string RULE_SET_LENGTH = "Length";
        public int FcId { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Tag = "ERR100", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public int AgencyId { get; set; }

        public DateTime CompletedDt { get; set; }

        public int CallId { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Tag = "ERR101", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public int ProgramId { get; set; }

        [StringRequiredValidator(Tag = "ERR102", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        [NullableOrStringLengthValidator(false , 30, Ruleset = RULE_SET_LENGTH)]
        public string AgencyCaseNum { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = RULE_SET_LENGTH)]
        public string AgencyClientNum { get; set; }

        [DateTimeRangeValidator("0001-01-02T00:00:00", "9999-12-31T00:00:00", Tag = "ERR103", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public DateTime IntakeDt { get; set; }

        [StringRequiredValidator(Tag = "WARN300", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string IncomeEarnersCd { get; set; }

        [StringRequiredValidator(Tag = "ERR104", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 15, Ruleset = RULE_SET_LENGTH)]
        public string CaseSourceCd{ get; set; }

        [StringRequiredValidator(Tag = "WARN301", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string RaceCd { get; set; }

        [StringRequiredValidator(Tag = "WARN302", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string HouseholdCd { get; set; }

        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string NeverBillReasonCd { get; set; }

        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string NeverPayReasonCd { get; set; }

        [StringRequiredValidator(Tag = "WARN303", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string DfltReason1stCd { get; set; }

        [StringRequiredValidator(Tag = "WARN304", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string DfltReason2ndCd { get; set; }

        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string HudTerminationReasonCd { get; set; }
        
        public DateTime HudTerminationDt { get; set; }

        [StringRequiredValidator(Tag = "WARN305", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string HudOutcomeCd { get; set; }

        public int AmiPercentage { get; set; }

        [StringRequiredValidator(Tag = "WARN306", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string CounselingDurationCd { get; set; }

        [StringRequiredValidator(Tag = "WARN307", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string GenderCd { get; set; }

        [StringRequiredValidator(Tag = "ERR105", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        [NullableOrStringLengthValidator(false, 30, Ruleset = RULE_SET_LENGTH)]        
        public string BorrowerFname { get; set; }

        [StringRequiredValidator(Tag = "ERR106", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = RULE_SET_LENGTH)]
        public string BorrowerLname { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = RULE_SET_LENGTH)]
        public string BorrowerMname { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = RULE_SET_LENGTH)]
        public string MotherMaidenLname { get; set; }

        [NullableOrStringLengthValidator(true, 9, Ruleset = RULE_SET_LENGTH)]
        public string BorrowerSsn { get; set; }

        [NullableOrStringLengthValidator(true, 4, Ruleset = RULE_SET_LENGTH)]
        public string BorrowerLast4Ssn { get; set; }

        [StringRequiredValidator(Tag = "WARN308", Ruleset = "Complete", MessageTemplate = "Required!")]
        
        public DateTime BorrowerDob { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = RULE_SET_LENGTH)]
        public string CoBorrowerFname { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = RULE_SET_LENGTH)]
        public string CoBorrowerLname { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = RULE_SET_LENGTH)]
        public string CoBorrowerMname { get; set; }
        
        [NullableOrStringLengthValidator(true, 9, Ruleset = RULE_SET_LENGTH)]
        public string CoBorrowerSsn { get; set; }
        
        [NullableOrStringLengthValidator(true, 4, Ruleset = RULE_SET_LENGTH)]
        public string CoBorrowerLast4Ssn { get; set; }

        public DateTime CoBorrowerDob { get; set; }

        [StringRequiredValidator(Tag = "ERR107", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 20, Ruleset = RULE_SET_LENGTH)]
        public string PrimaryContactNo { get; set; }
        
        [NullableOrStringLengthValidator(true, 20, Ruleset = RULE_SET_LENGTH)]
        public string SecondContactNo { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string Email1 { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string Email2 { get; set; }

        [StringRequiredValidator(Tag = "ERR108", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 50, Ruleset = RULE_SET_LENGTH)]
        public string ContactAddr1 { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string ContactAddr2 { get; set; }

        [StringRequiredValidator(Tag = "ERR109", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = RULE_SET_LENGTH)]
        public string ContactCity { get; set; }

        [StringRequiredValidator(Tag = "ERR110", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 15, Ruleset = RULE_SET_LENGTH)]
        public string ContactStateCd { get; set; }

        [StringRequiredValidator(Tag = "ERR111", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 5, Ruleset = RULE_SET_LENGTH)]
        public string ContactZip { get; set; }

        [NullableOrStringLengthValidator(true, 4, Ruleset = RULE_SET_LENGTH)]
        public string ContactZipPlus4 { get; set; }

        [StringRequiredValidator(Tag = "ERR112", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 50, Ruleset = RULE_SET_LENGTH)]
        public string PropAddr1 { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string PropAddr2 { get; set; }

        [StringRequiredValidator(Tag = "ERR113", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = RULE_SET_LENGTH)]
        public string PropCity { get; set; }

        [StringRequiredValidator(Tag = "ERR114", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 15, Ruleset = RULE_SET_LENGTH)]
        public string PropStateCd { get; set; }

        [StringRequiredValidator(Tag = "ERR115", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 5, Ruleset = RULE_SET_LENGTH)]
        public string PropZip { get; set; }

        [NullableOrStringLengthValidator(true, 4, Ruleset = RULE_SET_LENGTH)]
        public string PropZipPlus4 { get; set; }

        [StringRequiredValidator(Tag = "WARN309", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string BankruptcyInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string BankruptcyAttorney { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string BankruptcyPmtCurrentInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string BorrowerEducLevelCompletedCd { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string BorrowerMaritalStatusCd { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string BorrowerPreferredLangCd { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string BorrowerOccupationCd { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string CoBorrowerOccupationCd { get; set; }

        [StringRequiredValidator(Tag = "WARN310", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string HispanicInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string DuplicateInd { get; set; }

        [StringRequiredValidator(Tag = "WARN311", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string FcNoticeReceiveInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string CaseCompleteInd { get; set; }

        [StringRequiredValidator(Tag = "ERR116", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 1, Ruleset = RULE_SET_LENGTH)]
        public string FundingConsentInd { get; set; }

        [StringRequiredValidator(Tag = "ERR117", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 1, Ruleset = RULE_SET_LENGTH)]
        public string ServicerConsentInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string AgencyMediaConsentInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string HpfMediaCandidateInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string HpfNetworkCandidateInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string HpfSuccessStoryInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string AgencySuccessStoryInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string BorrowerDisabledInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string CoBorrowerDisabledInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string SummarySentOtherCd { get; set; }

        public DateTime SummarySentOtherDt { get; set; }

        public DateTime SummarySentDt { get; set; }

        [StringRequiredValidator(Tag = "WARN312", Ruleset = "Complete", MessageTemplate = "Required!")]
        public byte OccupantNum { get; set; }

        [StringRequiredValidator(Tag = "WARN313", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 8000, Ruleset = RULE_SET_LENGTH)]
        public string LoanDfltReasonNotes { get; set; }

        [StringRequiredValidator(Tag = "WARN314", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 8000, Ruleset = RULE_SET_LENGTH)]
        public string ActionItemsNotes { get; set; }
        
        [NullableOrStringLengthValidator(true, 8000, Ruleset = RULE_SET_LENGTH)]
        public string FollowupNotes { get; set; }
        
        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = RULE_SET_LENGTH)]
        public double PrimResEstMktValue { get; set; }

        [StringRequiredValidator(Tag = "ERR118", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = RULE_SET_LENGTH)]
        public string AssignedCounselorIdRef{ get; set; }

        [StringRequiredValidator(Tag = "ERR119", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = RULE_SET_LENGTH)]
        public string CounselorFname { get; set; }

        [StringRequiredValidator(Tag = "ERR120", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = RULE_SET_LENGTH)]
        public string CounselorLname { get; set; }

        [StringRequiredValidator(Tag = "ERR121", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 50, Ruleset = RULE_SET_LENGTH)]
        public string CounselorEmail { get; set; }

        [StringRequiredValidator(Tag = "ERR122", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 20, Ruleset = RULE_SET_LENGTH)]        
        public string CounselorPhone { get; set; }
        
        [NullableOrStringLengthValidator(true, 20, Ruleset = RULE_SET_LENGTH)]                
        public string CounselorExt { get; set; }

        [StringRequiredValidator(Tag = "WARN315", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string DiscussedSolutionWithSrvcrInd { get; set; }

        [StringRequiredValidator(Tag = "WARN316", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string WorkedWithAnotherAgencyInd { get; set; }

        [StringRequiredValidator(Tag = "WARN317", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string ContactedSrvcrRecentlyInd { get; set; }

        [StringRequiredValidator(Tag = "WARN318", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string HasWorkoutPlanInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string SrvcrWorkoutPlanCurrentInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string OptOutNewsletterInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string OptOutSurveyInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string DoNotCallInd { get; set; }

        [StringRequiredValidator(Tag = "ERR123", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 1, Ruleset = RULE_SET_LENGTH)]
        public string OwnerOccupiedInd { get; set; }

        [StringRequiredValidator(Tag = "ERR124", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        [NullableOrStringLengthValidator(false, 1, Ruleset = RULE_SET_LENGTH)]
        public string PrimaryResidenceInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string RealtyCompany { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string PropertyCd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string ForSaleInd { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = RULE_SET_LENGTH)]
        public double HomeSalePrice { get; set; }

        public int HomePurchaseYear { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = RULE_SET_LENGTH)]
        public double HomePurchasePrice { get; set; }

        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = RULE_SET_LENGTH)]
        public double HomeCurrentMarketValue { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string MilitaryServiceCd { get; set; }

        [NotNullValidator(Tag = "WARN319", Ruleset = "Complete", MessageTemplate = "Required!")]
        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = RULE_SET_LENGTH)]
        public double HouseholdGrossAnnualIncomeAmt{ get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string LoanList { get; set; }
        
        [NullableOrStringLengthValidator(true, 4, Ruleset = RULE_SET_LENGTH)]
        public string IntakeCreditScore { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string IntakeCreditBureauCd { get; set; }
        
        public DateTime FcSaleDate { get; set; }
    }
}
