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

        public string AgencyClientNum { get; set; }

        [DateTimeRangeValidator("0001-01-02T00:00:00", "9999-12-31T00:00:00", Tag = "ERR103", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public DateTime IntakeDt { get; set; }

        [NotNullValidator(Tag = "WARN300", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string IncomeEarnersCd { get; set; }

        [NotNullValidator(Tag = "ERR104", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string CaseSourceCd{ get; set; }

        [NotNullValidator(Tag = "WARN301", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string RaceCd { get; set; }

        [NotNullValidator(Tag = "WARN302", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string HouseholdCd { get; set; }

        public string NeverBillReasonCd { get; set; }

        public string NeverPayReasonCd { get; set; }

        [NotNullValidator(Tag = "WARN303", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string DfltReason1stCd { get; set; }

        [NotNullValidator(Tag = "WARN304", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string DfltReason2ndCd { get; set; }

        public string HudTerminationReasonCd { get; set; }

        public DateTime HudTerminationDt { get; set; }

        [NotNullValidator(Tag = "WARN305", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string HudOutcomeCd { get; set; }

        public int AmiPercentage { get; set; }

        [NotNullValidator(Tag = "WARN306", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string CounselingDurationCd { get; set; }

        [NotNullValidator(Tag = "WARN307", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string GenderCd { get; set; }

        [NotNullValidator(Tag = "ERR105", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string BorrowerFname { get; set; }

        [NotNullValidator(Tag = "ERR106", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string BorrowerLname { get; set; }

        public string BorrowerMname { get; set; }

        public string MotherMaidenLname { get; set; }

        public string BorrowerSsn { get; set; }

        public string BorrowerLast4Ssn { get; set; }

        [NotNullValidator(Tag = "WARN308", Ruleset = "Complete", MessageTemplate = "Required!")]
        public DateTime BorrowerDob { get; set; }

        public string CoBorrowerFname { get; set; }

        public string CoBorrowerLname { get; set; }

        public string CoBorrowerMname { get; set; }

        public string CoBorrowerSsn { get; set; }
        
        public string CoBorrowerLast4Ssn { get; set; }

        public DateTime CoBorrowerDob { get; set; }

        [NotNullValidator(Tag = "ERR107", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string PrimaryContactNo { get; set; }

        public string SecondContactNo { get; set; }

        public string Email1 { get; set; }        

        public string Email2 { get; set; }

        [NotNullValidator(Tag = "ERR108", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string ContactAddr1 { get; set; }

        public string ContactAddr2 { get; set; }

        [NotNullValidator(Tag = "ERR109", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string ContactCity { get; set; }

        [NotNullValidator(Tag = "ERR110", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string ContactStateCd { get; set; }

        [NotNullValidator(Tag = "ERR111", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string ContactZip { get; set; }

        public string ContactZipPlus4 { get; set; }

        [NotNullValidator(Tag = "ERR112", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string PropAddr1 { get; set; }

        public string PropAddr2 { get; set; }

        [NotNullValidator(Tag = "ERR113", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string PropCity { get; set; }

        [NotNullValidator(Tag = "ERR114", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string PropStateCd { get; set; }

        [NotNullValidator(Tag = "ERR115", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string PropZip { get; set; }

        public string PropZipPlus4 { get; set; }

        [NotNullValidator(Tag = "WARN309", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string BankruptcyInd { get; set; }

        public string BankruptcyAttorney { get; set; }

        public string BankruptcyPmtCurrentInd { get; set; }

        public string BorrowerEducLevelCompletedCd { get; set; }

        public string BorrowerMaritalStatusCd { get; set; }

        public string BorrowerPreferredLangCd { get; set; }

        public string BorrowerOccupationCd { get; set; }

        public string CoBorrowerOccupationCd { get; set; }

        [NotNullValidator(Tag = "WARN310", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string HispanicInd { get; set; }

        public string DuplicateInd { get; set; }

        [NotNullValidator(Tag = "WARN311", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string FcNoticeReceiveInd { get; set; }

        public string CaseCompleteInd { get; set; }

        [NotNullValidator(Tag = "ERR116", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string FundingConsentInd { get; set; }

        [NotNullValidator(Tag = "ERR117", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string ServicerConsentInd { get; set; }

        public string AgencyMediaConsentInd { get; set; }

        public string HpfMediaCandidateInd { get; set; }

        public string HpfNetworkCandidateInd { get; set; }

        public string HpfSuccessStoryInd { get; set; }

        public string AgencySuccessStoryInd { get; set; }

        public string BorrowerDisabledInd { get; set; }

        public string CoBorrowerDisabledInd { get; set; }

        public string SummarySentOtherCd { get; set; }

        public DateTime SummarySentOtherDt { get; set; }

        public DateTime SummarySentDt { get; set; }

        [NotNullValidator(Tag = "WARN312", Ruleset = "Complete", MessageTemplate = "Required!")]
        public byte OccupantNum { get; set; }

        [NotNullValidator(Tag = "WARN313", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string LoanDfltReasonNotes { get; set; }

        [NotNullValidator(Tag = "WARN314", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string ActionItemsNotes { get; set; }

        public string FollowupNotes { get; set; }
        
        public decimal PrimResEstMktValue { get; set; }

        [NotNullValidator(Tag = "ERR118", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string AssignedCounselorIdRef{ get; set; }

        [NotNullValidator(Tag = "ERR119", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string CounselorFname { get; set; }

        [NotNullValidator(Tag = "ERR120", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string CounselorLname { get; set; }

        [NotNullValidator(Tag = "ERR121", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string CounselorEmail { get; set; }

        [NotNullValidator(Tag = "ERR122", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string CounselorPhone { get; set; }
                
        public string CounselorExt { get; set; }

        [NotNullValidator(Tag = "WARN315", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string DiscussedSolutionWithSrvcrInd { get; set; }

        [NotNullValidator(Tag = "WARN316", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string WorkedWithAnotherAgencyInd { get; set; }

        [NotNullValidator(Tag = "WARN317", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string ContactedSrvcrRecentlyInd { get; set; }

        [NotNullValidator(Tag = "WARN318", Ruleset = "Complete", MessageTemplate = "Required!")]
        public string HasWorkoutPlanInd { get; set; }

        public string SrvcrWorkoutPlanCurrentInd { get; set; }
        
        public string OptOutNewsletterInd { get; set; }

        public string OptOutSurveyInd { get; set; }
        
        public string DoNotCallInd { get; set; }

        [NotNullValidator(Tag = "ERR123", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string OwnerOccupiedInd { get; set; }

        [NotNullValidator(Tag = "ERR124", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string PrimaryResidenceInd { get; set; }
        
        public string RealtyCompany { get; set; }

        public string PropertyCd { get; set; }

        public string ForSaleInd { get; set; }

        public decimal HomeSalePrice { get; set; }

        public int HomePurchaseYear { get; set; }        
        
        public decimal HomePurchasePrice { get; set; }

        public decimal HomeCurrentMarketValue { get; set; }

        public string MilitaryServiceCd { get; set; }

        [NotNullValidator(Tag = "WARN319", Ruleset = "Complete", MessageTemplate = "Required!")]
        public decimal HouseholdGrossAnnualIncomeAmt{ get; set; }

        public string LoanList { get; set; }

        public string IntakeCreditScore { get; set; }

        public string IntakeCreditBureauCd { get; set; }
        public DateTime FcSaleDate { get; set; }
    }
}
