using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HPF.FutureState.Common.Utils.DataValidator;
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

        [NotNullValidator(Tag = "ERR102", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        [NullableOrStringLengthValidator(false , 30, Ruleset = RULE_SET_LENGTH)]
        public string AgencyCaseNum { get; set; }

        [NullableOrStringLengthValidator(true, 30, Ruleset = RULE_SET_LENGTH)]
        public string AgencyClientNum { get; set; }

        [DateTimeRangeValidator("0001-01-02T00:00:00", "9999-12-31T00:00:00", Tag = "ERR103", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public DateTime IntakeDt { get; set; }

        [NotNullValidator(Tag = "WARN300", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string IncomeEarnersCd { get; set; }

        [NotNullValidator(Tag = "ERR104", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 15, Ruleset = RULE_SET_LENGTH)]
        public string CaseSourceCd{ get; set; }

        [NotNullValidator(Tag = "WARN301", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string RaceCd { get; set; }

        [NotNullValidator(Tag = "WARN302", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string HouseholdCd { get; set; }

        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string NeverBillReasonCd { get; set; }

        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string NeverPayReasonCd { get; set; }

        [NotNullValidator(Tag = "WARN303", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string DfltReason1stCd { get; set; }

        [NotNullValidator(Tag = "WARN304", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string DfltReason2ndCd { get; set; }

        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string HudTerminationReasonCd { get; set; }
        
        public DateTime HudTerminationDt { get; set; }

        [NotNullValidator(Tag = "WARN305", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string HudOutcomeCd { get; set; }

        public int AmiPercentage { get; set; }

        [NotNullValidator(Tag = "WARN306", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string CounselingDurationCd { get; set; }

        [NotNullValidator(Tag = "WARN307", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string GenderCd { get; set; }

        [NotNullValidator(Tag = "ERR105", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = RULE_SET_LENGTH)]
        public string BorrowerFname { get; set; }

        [NotNullValidator(Tag = "ERR106", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
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

        [NotNullValidator(Tag = "WARN308", Ruleset = "Complete", MessageTemplate = "Required!")]
        
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

        [NotNullValidator(Tag = "ERR107", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 20, Ruleset = RULE_SET_LENGTH)]
        public string PrimaryContactNo { get; set; }
        
        [NullableOrStringLengthValidator(true, 20, Ruleset = RULE_SET_LENGTH)]
        public string SecondContactNo { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string Email1 { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string Email2 { get; set; }

        [NotNullValidator(Tag = "ERR108", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 50, Ruleset = RULE_SET_LENGTH)]
        public string ContactAddr1 { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string ContactAddr2 { get; set; }

        [NotNullValidator(Tag = "ERR109", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = RULE_SET_LENGTH)]
        public string ContactCity { get; set; }

        [NotNullValidator(Tag = "ERR110", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 15, Ruleset = RULE_SET_LENGTH)]
        public string ContactStateCd { get; set; }

        [NotNullValidator(Tag = "ERR111", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 5, Ruleset = RULE_SET_LENGTH)]
        public string ContactZip { get; set; }

        [NullableOrStringLengthValidator(true, 4, Ruleset = RULE_SET_LENGTH)]
        public string ContactZipPlus4 { get; set; }

        [NotNullValidator(Tag = "ERR112", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 50, Ruleset = RULE_SET_LENGTH)]
        public string PropAddr1 { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string PropAddr2 { get; set; }

        [NotNullValidator(Tag = "ERR113", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = RULE_SET_LENGTH)]
        public string PropCity { get; set; }

        [NotNullValidator(Tag = "ERR114", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 15, Ruleset = RULE_SET_LENGTH)]
        public string PropStateCd { get; set; }

        [NotNullValidator(Tag = "ERR115", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 5, Ruleset = RULE_SET_LENGTH)]
        public string PropZip { get; set; }

        [NullableOrStringLengthValidator(true, 4, Ruleset = RULE_SET_LENGTH)]
        public string PropZipPlus4 { get; set; }

        [NotNullValidator(Tag = "WARN309", Ruleset = "Complete", MessageTemplate = "Required!")]
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

        [NotNullValidator(Tag = "WARN310", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string HispanicInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string DuplicateInd { get; set; }

        [NotNullValidator(Tag = "WARN311", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string FcNoticeReceiveInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string CaseCompleteInd { get; set; }

        [NotNullValidator(Tag = "ERR116", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 1, Ruleset = RULE_SET_LENGTH)]
        public string FundingConsentInd { get; set; }

        [NotNullValidator(Tag = "ERR117", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
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

        [NotNullValidator(Tag = "WARN312", Ruleset = "Complete", MessageTemplate = "Required!")]
        public byte OccupantNum { get; set; }

        [NotNullValidator(Tag = "WARN313", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 8000, Ruleset = RULE_SET_LENGTH)]
        public string LoanDfltReasonNotes { get; set; }

        [NotNullValidator(Tag = "WARN314", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 8000, Ruleset = RULE_SET_LENGTH)]
        public string ActionItemsNotes { get; set; }
        
        [NullableOrStringLengthValidator(true, 8000, Ruleset = RULE_SET_LENGTH)]
        public string FollowupNotes { get; set; }
        
        public decimal PrimResEstMktValue { get; set; }

        [NotNullValidator(Tag = "ERR118", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = RULE_SET_LENGTH)]
        public string AssignedCounselorIdRef{ get; set; }

        [NotNullValidator(Tag = "ERR119", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = RULE_SET_LENGTH)]
        public string CounselorFname { get; set; }

        [NotNullValidator(Tag = "ERR120", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 30, Ruleset = RULE_SET_LENGTH)]
        public string CounselorLname { get; set; }

        [NotNullValidator(Tag = "ERR121", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 50, Ruleset = RULE_SET_LENGTH)]
        public string CounselorEmail { get; set; }

        [NotNullValidator(Tag = "ERR122", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 20, Ruleset = RULE_SET_LENGTH)]        
        public string CounselorPhone { get; set; }
        
        [NullableOrStringLengthValidator(true, 20, Ruleset = RULE_SET_LENGTH)]                
        public string CounselorExt { get; set; }

        [NotNullValidator(Tag = "WARN315", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string DiscussedSolutionWithSrvcrInd { get; set; }

        [NotNullValidator(Tag = "WARN316", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string WorkedWithAnotherAgencyInd { get; set; }

        [NotNullValidator(Tag = "WARN317", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string ContactedSrvcrRecentlyInd { get; set; }

        [NotNullValidator(Tag = "WARN318", Ruleset = "Complete", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string HasWorkoutPlanInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string SrvcrWorkoutPlanCurrentInd { get; set; }
        
        [NullableOrStringLengthValidator(false, 1, Ruleset = RULE_SET_LENGTH)]
        public string OptOutNewsletterInd { get; set; }
        
        [NullableOrStringLengthValidator(false, 1, Ruleset = RULE_SET_LENGTH)]
        public string OptOutSurveyInd { get; set; }
        
        [NullableOrStringLengthValidator(false, 1, Ruleset = RULE_SET_LENGTH)]
        public string DoNotCallInd { get; set; }

        [NotNullValidator(Tag = "ERR123", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(false, 1, Ruleset = RULE_SET_LENGTH)]
        public string OwnerOccupiedInd { get; set; }

        [NotNullValidator(Tag = "ERR124", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        [NullableOrStringLengthValidator(false, 1, Ruleset = RULE_SET_LENGTH)]
        public string PrimaryResidenceInd { get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string RealtyCompany { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string PropertyCd { get; set; }
        
        [NullableOrStringLengthValidator(true, 1, Ruleset = RULE_SET_LENGTH)]
        public string ForSaleInd { get; set; }

        public decimal HomeSalePrice { get; set; }

        public int HomePurchaseYear { get; set; }        
        
        public decimal HomePurchasePrice { get; set; }

        public decimal HomeCurrentMarketValue { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string MilitaryServiceCd { get; set; }

        [NotNullValidator(Tag = "WARN319", Ruleset = "Complete", MessageTemplate = "Required!")]
        public decimal HouseholdGrossAnnualIncomeAmt{ get; set; }
        
        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string LoanList { get; set; }
        
        [NullableOrStringLengthValidator(true, 4, Ruleset = RULE_SET_LENGTH)]
        public string IntakeCreditScore { get; set; }
        
        [NullableOrStringLengthValidator(true, 15, Ruleset = RULE_SET_LENGTH)]
        public string IntakeCreditBureauCd { get; set; }
        
        public DateTime FcSaleDate { get; set; }
    }
}
