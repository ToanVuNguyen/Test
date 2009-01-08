using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.ComponentModel;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class ForeclosureCaseDTO : BaseDTO
    {        
        public int FcId { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Tag = "P-WS-SFC-00100", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public int AgencyId { get; set; }

        public DateTime CompletedDt { get; set; }

        public int CallId { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Tag = "P-WS-SFC-00101", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public int ProgramId { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00102", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string AgencyCaseNum { get; set; }

        public string AgencyClientNum { get; set; }

        [DateTimeRangeValidator("0001-01-02T00:00:00", "9999-12-31T00:00:00", Tag = "P-WS-SFC-00103", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public DateTime IntakeDt { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string IncomeEarnersCd { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00104", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string CaseSourceCd{ get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string RaceCd { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string HouseholdCd { get; set; }

        public string NeverBillReasonCd { get; set; }

        public string NeverPayReasonCd { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string DfltReason1stCd { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string DfltReason2ndCd { get; set; }

        public string HudTerminationReasonCd { get; set; }

        public DateTime HudTerminationDt { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string HudOutcomeCd { get; set; }

        public int AmiPercentage { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string CounselingDurationCd { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string GenderCd { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00105", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string BorrowerFname { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00106", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string BorrowerLname { get; set; }

        public string BorrowerMname { get; set; }

        public string MotherMaidenLname { get; set; }

        public string BorrowerSsn { get; set; }

        public string BorrowerLast4Ssn { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public DateTime BorrowerDob { get; set; }

        public string CoBorrowerFname { get; set; }

        public string CoBorrowerLname { get; set; }

        public string CoBorrowerMname { get; set; }

        public string CoBorrowerSsn { get; set; }
        
        public string CoBorrowerLast4Ssn { get; set; }

        public DateTime CoBorrowerDob { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00107", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string PrimaryContactNo { get; set; }

        public string SecondContactNo { get; set; }

        public string Email1 { get; set; }        

        public string Email2 { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00108", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string ContactAddr1 { get; set; }

        public string ContactAddr2 { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00109", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string ContactCity { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00110", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string ContactStateCd { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00111", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string ContactZip { get; set; }

        public string ContactZipPlus4 { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00112", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string PropAddr1 { get; set; }

        public string PropAddr2 { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00113", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string PropCity { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00114", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string PropStateCd { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00115", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string PropZip { get; set; }

        public string PropZipPlus4 { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string BankruptcyInd { get; set; }

        public string BankruptcyAttorney { get; set; }

        public string BankruptcyPmtCurrentInd { get; set; }

        public string BorrowerEducLevelCompletedCd { get; set; }

        public string BorrowerMaritalStatusCd { get; set; }

        public string BorrowerPreferredLangCd { get; set; }

        public string BorrowerOccupationCd { get; set; }

        public string CoBorrowerOccupationCd { get; set; }        

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string HispanicInd { get; set; }

        public string DuplicateInd { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string FcNoticeReceiveInd { get; set; }

        public string CaseCompleteInd { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00116", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string FundingConsentInd { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00117", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
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

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public byte OccupantNum { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string LoanDfltReasonNotes { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string ActionItemsNotes { get; set; }

        public string FollowupNotes { get; set; }
        
        public decimal PrimResEstMktValue { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00118", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string AssignedCounselorIdRef{ get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00119", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string CounselorFname { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00120", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string CounselorLname { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00121", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string CounselorEmail { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00122", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string CounselorPhone { get; set; }
                
        public string CounselorExt { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string DiscussedSolutionWithSrvcrInd { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string WorkedWithAnotherAgencyInd { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string ContactedSrvcrRecentlyInd { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string HasWorkoutPlanInd { get; set; }

        public string SrvcrWorkoutPlanCurrentInd { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public string FcSaleDateSetInd { get; set; }
        
        public string OptOutNewsletterInd { get; set; }

        public string OptOutSurveyInd { get; set; }
        
        public string DoNotCallInd { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00123", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string OwnerOccupiedInd { get; set; }

        [NotNullValidator(Tag = "P-WS-SFC-00124", Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public string PrimaryResidenceInd { get; set; }
        
        public string RealtyCompany { get; set; }

        public string PropertyCd { get; set; }

        public string ForSaleInd { get; set; }

        public decimal HomeSalePrice { get; set; }

        public int HomePurchaseYear { get; set; }        
        
        public decimal HomePurchasePrice { get; set; }

        public decimal HomeCurrentMarketValue { get; set; }

        public string MilitaryServiceCd { get; set; }

        [NotNullValidator(Ruleset = "Complete", MessageTemplate = "Required!")]
        public decimal HouseholdGrossAnnualIncomeAmt{ get; set; }

        public string LoanList { get; set; }

        public string IntakeCreditScore { get; set; }

        public string IntakeCreditBureauCd { get; set; }
        public DateTime FcSaleDate { get; set; }
    }
}
