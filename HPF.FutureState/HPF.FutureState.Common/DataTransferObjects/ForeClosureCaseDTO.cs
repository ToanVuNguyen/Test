using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class ForeClosureCaseDTO : BaseDTO
    {
        public int FcId { get; set; }
        public int AgencyId { get; set; }
        public int CallId { get; set; }
        public int ProgramId { get; set; }
        public string AgencyCaseNum { get; set; }
        public string AgencyClientNum { get; set; }
        public DateTime IntakeDt { get; set; }
        public string IncomeEarnersCd { get; set; }
        public string RaceCd { get; set; }
        public string HouseholdCd { get; set; }
        public string NeverBillReasonCd { get; set; }
        public string NeverPayReasonCd { get; set; }
        public string DfltReason1stCd { get; set; }
        public string DfltReason2ndCd { get; set; }
        public string HudTerminationReasonCd { get; set; }
        public DateTime HudTerminationDt { get; set; }
        public string HudOutcomeCd { get; set; }
        public int AmiPercentage { get; set; }
        public string CounselingDurationCd { get; set; }
        public string GenderCd { get; set; }
        public string BorrowerFname { get; set; }
        public string BorrowerLname { get; set; }
        public string BorrowerMname { get; set; }
        public string MotherMaidenLname { get; set; }
        public string BorrowerSsn { get; set; }
        public string BorrowerLast4Ssn { get; set; }
        public DateTime BorrowerDob { get; set; }
        public string CoBorrowerFname { get; set; }
        public string CoBorrowerLname { get; set; }
        public string CoBorrowerMname { get; set; }
        public string CoBorrowerSsn { get; set; }
        public string CoBorrowerLast4Ssn { get; set; }
        public DateTime CoBorrowerDob { get; set; }
        public string PrimaryContactNo { get; set; }
        public string SecondContactNo { get; set; }
        public string Email1 { get; set; }
        public string ContactZipPlus4 { get; set; }
        public string Email2 { get; set; }
        public string ContactAddr1 { get; set; }
        public string ContactAddr2 { get; set; }
        public string ContactCity { get; set; }
        public string ContactState { get; set; }
        public string ContactZip { get; set; }
        public string PropAddr1 { get; set; }
        public string PropAddr2 { get; set; }
        public string PropCity { get; set; }
        public string PropState { get; set; }
        public string PropZip { get; set; }
        public string PropZipPlus4 { get; set; }
        public string BankruptcyInd { get; set; }
        public string BankruptcyAttorney { get; set; }
        public string BankruptcyPmtCurrentInd { get; set; }
        public string BorrowerEducLevelCompletedCd { get; set; }
        public string BorrowerMaritalStatusCd { get; set; }
        public string BorrowerPreferredLangCd { get; set; }
        public string BorrowerOccupation { get; set; }
        public string CoBorrowerOccupation { get; set; }
        public string HispanicInd { get; set; }
        public string DuplicateInd { get; set; }
        public string FcNoticeReceiveInd { get; set; }
        public string CaseCompleteInd { get; set; }
        public DateTime CompletedDt { get; set; }
        public string FundingConsentInd { get; set; }
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
        public byte OccupantNum { get; set; }
        public string LoanDfltReasonNotes { get; set; }
        public string ActionItemsNotes { get; set; }
        public string FollowupNotes { get; set; }
        public decimal PrimResEstMktValue { get; set; }
        public string CounselorIdRef { get; set; }
        public string CounselorFullName { get; set; }
        public string CounselorEmail { get; set; }
        public string CounselorPhone { get; set; }
        public string CounselorExt { get; set; }
        public string DiscussedSolutionWithSrvcrInd { get; set; }
        public string WorkedWithAnotherAgencyInd { get; set; }
        public string ContactedSrvcrRecentlyInd { get; set; }
        public string HasWorkoutPlanInd { get; set; }
        public string SrvcrWorkoutPlanCurrentInd { get; set; }
        public string FcSaleDateSetInd { get; set; }
        public string OptOutNewsletterInd { get; set; }
        public string OptOutSurveyInd { get; set; }
        public string DoNotCallInd { get; set; }
        public string OwnerOccupiedInd { get; set; }
        public string PrimaryResidenceInd { get; set; }
        public string RealtyCompany { get; set; }
        public string PropertyCd { get; set; }
        public string ForSaleInd { get; set; }
        public decimal HomeSalePrice { get; set; }
        public int HomePurchaseYear { get; set; }
        public decimal HomePurchasePrice { get; set; }
        public decimal HomeCurrentMarketValue { get; set; }
        public string MilitaryServiceCd { get; set; }                
    }
}
