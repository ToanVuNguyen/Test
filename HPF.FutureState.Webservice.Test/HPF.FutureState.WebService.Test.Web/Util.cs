using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Collections.Generic;
using HPF.Webservice.Agency;

namespace HPF.FutureState.WebService.Test.Web
{
    public class Util
    {
        public static DateTime ConvertToDateTime(Object obj)
        {
            DateTime temp = DateTime.Now;
            DateTime.TryParse(obj.ToString(), out temp);
            return temp;
        }

        public static int ConvertToInt(Object obj)
        {
            int temp = 0;
            int.TryParse(obj.ToString(), out temp);
            return temp;
        }

        public static decimal ConvertToDecimal(Object obj)
        {
            decimal temp = 0;
            decimal.TryParse(obj.ToString(), out temp);
            return temp;
        }

        public static double ConvertToDouble(Object obj)
        {
            double temp = 0;
            double.TryParse(obj.ToString(), out temp);
            return temp;
        }

        public static byte ConvertToByte(Object obj)
        {
            byte temp;
            byte.TryParse(obj.ToString(), out temp);
            return temp;
        }

        public static string ConvertToString(object obj)
        {
            return obj.ToString();   
        }

        public static List<CaseLoanDTO> ParseCaseLoanDTO(XDocument xdoc)
        {
            try
            {
                var objs = from obj in xdoc.Descendants("CaseLoan")
                           select new CaseLoanDTO
                           {
                               AcctNum = obj.Element("AcctNum").Value,
                               ArmLoanInd = obj.Element("ArmLoanInd").Value,
                               ArmResetInd = obj.Element("ArmResetInd").Value,
                               CaseLoanId = ConvertToInt(obj.Element("CaseLoanId").Value),
                               CurrentLoanBalanceAmt = ConvertToDecimal(obj.Element("CurrentLoanBalanceAmt").Value),
                               CurrentServicerNameTbd = obj.Element("CurrentServicerNameTbd").Value,
                               FdicNcusNumCurrentServicerTbd = obj.Element("FdicNcusNumCurrentServicerTbd").Value,
                               FreddieLoanNum = obj.Element("FreddieLoanNum").Value,
                               FcId = ConvertToInt(obj.Element("FcId").Value),
                               InterestRate = ConvertToDouble(obj.Element("InterestRate").Value),
                               Loan1st2nd = obj.Element("Loan1st2nd").Value,
                               LoanDelinqStatusCd = obj.Element("LoanDelinqStatusCd").Value,
                               MortgageTypeCd = obj.Element("MortgageTypeCd").Value,
                               OrginalLoanNum = obj.Element("OrginalLoanNum").Value,
                               OriginatingLenderName = obj.Element("OriginatingLenderName").Value,
                               OrigLoanAmt = ConvertToDecimal(obj.Element("OrigLoanAmt").Value),
                               OrigMortgageCoFdicNcusNum = obj.Element("OrigMortgageCoFdicNcusNum").Value,
                               OrigMortgageCoName = obj.Element("OrigMortgageCoName").Value,
                               OtherServicerName = obj.Element("OtherServicerName").Value,
                               ServicerId = ConvertToInt(obj.Element("ServicerId").Value),
                               TermLengthCd = obj.Element("TermLengthCd").Value
                           };
                return objs.ToList<CaseLoanDTO>();
            }
            catch (NullReferenceException ex)
            {
                return null;
            }

        }

        public static List<OutcomeItemDTO> ParseOutcomeItemDTO(XDocument xdoc)
        {
            try
            {
                var objs = from obj in xdoc.Descendants("OutcomeItem")
                           select new OutcomeItemDTO
                           {
                               OutcomeItemId = ConvertToInt(obj.Element("OutcomeItemId").Value),
                               ExtRefOtherName = obj.Element("ExtRefOtherName").Value,
                               FcId = ConvertToInt(obj.Element("FcId").Value),
                               NonprofitreferralKeyNum = obj.Element("NonprofitreferralKeyNum").Value,
                               OutcomeDeletedDt = ConvertToDateTime(obj.Element("OutcomeDeletedDt").Value),
                               OutcomeDt = ConvertToDateTime(obj.Element("OutcomeDt").Value),
                               OutcomeSetId = ConvertToInt(obj.Element("OutcomeSetId").Value),
                               OutcomeTypeId = ConvertToInt(obj.Element("OutcomeTypeId").Value)
                           };
                return objs.ToList<OutcomeItemDTO>();
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
        }

        public static List<BudgetAssetDTO> ParseBudgetAssetDTO(XDocument xdoc)
        {
            try
            {
                var objs = from obj in xdoc.Descendants("BudgetAsset")
                           select new BudgetAssetDTO
                           {
                               BudgetAssetId = ConvertToInt(obj.Element("BudgetAssetId").Value),
                               BudgetSetId = ConvertToInt(obj.Element("BudgetSetId").Value),
                               AssetName = obj.Element("AssetName").Value,
                               AssetValue = ConvertToDecimal(obj.Element("AssetValue").Value),
                           };
                return objs.ToList<BudgetAssetDTO>();
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
        }

        public static List<BudgetItemDTO> ParseBudgetItemDTO(XDocument xdoc)
        {
            try
            {
                var objs = from obj in xdoc.Descendants("BudgetItem")
                           select new BudgetItemDTO
                           {
                               BudgetItemAmt = ConvertToDouble(obj.Element("BudgetItemAmt").Value),
                               BudgetItemId = ConvertToInt(obj.Element("BudgetSetId").Value),
                               BudgetNote = obj.Element("BudgetNote").Value,
                               BudgetSetId = ConvertToInt(obj.Element("BudgetSetId").Value),
                               BudgetSubcategoryId = ConvertToInt(obj.Element("BudgetSubcategoryId").Value)
                           };
                return objs.ToList<BudgetItemDTO>();
            }
            catch (NullReferenceException ex)
            {
                return null;
            }

        }

        public static List<ActivityLogDTO> ParseActivityLogDTO(XDocument xdoc)
        {
            try
            {
                var objs = from obj in xdoc.Descendants("ActivityLog")
                           select new ActivityLogDTO
                           {
                               FcId = ConvertToInt(obj.Element("FcId").Value),
                               ActivityCd = obj.Element("ActivityCd").Value,
                               ActivityDt = ConvertToDateTime(obj.Element("ActivityDt").Value),
                               ActivityLogId = ConvertToInt(obj.Element("ActivityLogId").Value),
                               ActivityNote = obj.Element("ActivityNote").Value
                           };
                return objs.ToList<ActivityLogDTO>();
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
        }

        public static ForeclosureCaseDTO ParseForeclosureCaseDTO(XDocument xdoc)
        {
            try
            {
                var objs = from obj in xdoc.Descendants("ForeclosureCase")
                           select new ForeclosureCaseDTO
                           {
                               ActionItemsNotes = obj.Element("ActionItemsNotes").Value,
                               AgencyCaseNum = obj.Element("AgencyCaseNum").Value,
                               AgencyClientNum = obj.Element("AgencyClientNum").Value,
                               AgencyId = ConvertToInt(obj.Element("AgencyId").Value),
                               AgencyMediaConsentInd = obj.Element("AgencyMediaConsentInd").Value,
                               AgencySuccessStoryInd = obj.Element("AgencySuccessStoryInd").Value,
                               AmiPercentage = ConvertToInt(obj.Element("AmiPercentage").Value),
                               AssignedCounselorIdRef = obj.Element("AssignedCounselorIdRef").Value,
                               BankruptcyAttorney = obj.Element("BankruptcyAttorney").Value,
                               BankruptcyInd = obj.Element("BankruptcyInd").Value,
                               BankruptcyPmtCurrentInd = obj.Element("BankruptcyPmtCurrentInd").Value,
                               BorrowerDisabledInd = obj.Element("BorrowerDisabledInd").Value,
                               BorrowerDob = ConvertToDateTime(obj.Element("BorrowerDob").Value),
                               BorrowerEducLevelCompletedCd = obj.Element("BorrowerEducLevelCompletedCd").Value,
                               BorrowerFname = obj.Element("BorrowerFname").Value,
                               BorrowerLast4Ssn = obj.Element("BorrowerLast4Ssn").Value,
                               BorrowerLname = obj.Element("BorrowerLname").Value,
                               BorrowerMaritalStatusCd = obj.Element("BorrowerMaritalStatusCd").Value,
                               BorrowerMname = obj.Element("BorrowerMname").Value,
                               BorrowerOccupationCd = obj.Element("BorrowerOccupationCd").Value,
                               BorrowerPreferredLangCd = obj.Element("BorrowerPreferredLangCd").Value,
                               BorrowerSsn = obj.Element("BorrowerSsn").Value,
                               CallId = ConvertToInt(obj.Element("CallId").Value),
                               CaseCompleteInd = obj.Element("CaseCompleteInd").Value,
                               CaseSourceCd = obj.Element("CaseSourceCd").Value,
                               CoBorrowerDisabledInd = obj.Element("CoBorrowerDisabledInd").Value,
                               CoBorrowerDob = ConvertToDateTime(obj.Element("CoBorrowerDob").Value),
                               CoBorrowerFname = obj.Element("CoBorrowerFname").Value,
                               CoBorrowerLast4Ssn = obj.Element("CoBorrowerLast4Ssn").Value,
                               CoBorrowerLname = obj.Element("CoBorrowerLname").Value,
                               CoBorrowerMname = obj.Element("CoBorrowerMname").Value,
                               CoBorrowerOccupationCd = obj.Element("CoBorrowerOccupationCd").Value,
                               CoBorrowerSsn = obj.Element("CoBorrowerSsn").Value,
                               CompletedDt = ConvertToDateTime(obj.Element("CompletedDt").Value),
                               ContactAddr1 = obj.Element("ContactAddr1").Value,
                               ContactAddr2 = obj.Element("ContactAddr2").Value,
                               ContactCity = obj.Element("ContactCity").Value,
                               ContactedSrvcrRecentlyInd = obj.Element("ContactedSrvcrRecentlyInd").Value,
                               ContactStateCd = obj.Element("ContactStateCd").Value,
                               ContactZip = obj.Element("ContactZip").Value,
                               ContactZipPlus4 = obj.Element("ContactZipPlus4").Value,
                               CounselingDurationCd = obj.Element("CounselingDurationCd").Value,
                               CounselorEmail = obj.Element("CounselorEmail").Value,
                               CounselorExt = obj.Element("CounselorExt").Value,
                               CounselorFname = obj.Element("CounselorFname").Value,
                               CounselorLname = obj.Element("CounselorLname").Value,
                               CounselorPhone = obj.Element("CounselorPhone").Value,
                               DfltReason1stCd = obj.Element("DfltReason1stCd").Value,
                               DfltReason2ndCd = obj.Element("DfltReason2ndCd").Value,
                               DiscussedSolutionWithSrvcrInd = obj.Element("DiscussedSolutionWithSrvcrInd").Value,
                               DoNotCallInd = obj.Element("DoNotCallInd").Value,
                               DuplicateInd = obj.Element("DuplicateInd").Value,
                               Email1 = obj.Element("Email1").Value,
                               Email2 = obj.Element("Email2").Value,
                               FcId = ConvertToInt(obj.Element("FcId").Value),
                               FcNoticeReceiveInd = obj.Element("FcNoticeReceiveInd").Value,
                               FcSaleDateSetInd = obj.Element("FcSaleDateSetInd").Value,
                               FollowupNotes = obj.Element("FollowupNotes").Value,
                               ForSaleInd = obj.Element("ForSaleInd").Value,
                               FundingConsentInd = obj.Element("FundingConsentInd").Value,
                               GenderCd = obj.Element("GenderCd").Value,
                               HasWorkoutPlanInd = obj.Element("HasWorkoutPlanInd").Value,
                               HispanicInd = obj.Element("HispanicInd").Value,
                               HomeCurrentMarketValue = ConvertToDecimal(obj.Element("HomeCurrentMarketValue").Value),
                               HomePurchasePrice = ConvertToDecimal(obj.Element("HomePurchasePrice").Value),
                               HomePurchaseYear = ConvertToInt(obj.Element("HomePurchaseYear").Value),
                               HomeSalePrice = ConvertToDecimal(obj.Element("HomeSalePrice").Value),
                               HouseholdCd = obj.Element("HouseholdCd").Value,
                               HouseholdGrossAnnualIncomeAmt = ConvertToDouble(obj.Element("HouseholdGrossAnnualIncomeAmt").Value),
                               HpfMediaCandidateInd = obj.Element("HpfMediaCandidateInd").Value,
                               HpfNetworkCandidateInd = obj.Element("HpfNetworkCandidateInd").Value,
                               HpfSuccessStoryInd = obj.Element("HpfSuccessStoryInd").Value,
                               HudOutcomeCd = obj.Element("HudOutcomeCd").Value,
                               HudTerminationDt = ConvertToDateTime(obj.Element("HudTerminationDt").Value),
                               HudTerminationReasonCd = obj.Element("HudTerminationReasonCd").Value,
                               IncomeEarnersCd = obj.Element("IncomeEarnersCd").Value,
                               IntakeCreditBureauCd = obj.Element("IntakeCreditBureauCd").Value,
                               IntakeCreditScore = obj.Element("IntakeCreditScore").Value,
                               IntakeDt = ConvertToDateTime(obj.Element("IntakeDt").Value),
                               LoanDfltReasonNotes = obj.Element("LoanDfltReasonNotes").Value,
                               LoanList = obj.Element("LoanList").Value,
                               MilitaryServiceCd = obj.Element("MilitaryServiceCd").Value,
                               MotherMaidenLname = obj.Element("MotherMaidenLname").Value,
                               NeverBillReasonCd = obj.Element("NeverBillReasonCd").Value,
                               NeverPayReasonCd = obj.Element("NeverPayReasonCd").Value,
                               OccupantNum = ConvertToByte(obj.Element("OccupantNum").Value),
                               OptOutNewsletterInd = obj.Element("OptOutNewsletterInd").Value,
                               OptOutSurveyInd = obj.Element("OptOutSurveyInd").Value,
                               OwnerOccupiedInd = obj.Element("OwnerOccupiedInd").Value,
                               PrimaryContactNo = obj.Element("PrimaryContactNo").Value,
                               PrimaryResidenceInd = obj.Element("PrimaryResidenceInd").Value,
                               PrimResEstMktValue = ConvertToDecimal(obj.Element("PrimResEstMktValue").Value),
                               ProgramId = ConvertToInt(obj.Element("ProgramId").Value),
                               PropAddr1 = obj.Element("PropAddr1").Value,
                               PropAddr2 = obj.Element("PropAddr2").Value,
                               PropCity = obj.Element("PropCity").Value,
                               PropertyCd = obj.Element("PropertyCd").Value,
                               PropStateCd = obj.Element("PropStateCd").Value,
                               PropZip = obj.Element("PropZip").Value,
                               PropZipPlus4 = obj.Element("PropZipPlus4").Value,
                               RaceCd = obj.Element("RaceCd").Value,
                               RealtyCompany = obj.Element("RealtyCompany").Value,
                               SecondContactNo = obj.Element("SecondContactNo").Value,
                               ServicerConsentInd = obj.Element("ServicerConsentInd").Value,
                               SrvcrWorkoutPlanCurrentInd = obj.Element("SrvcrWorkoutPlanCurrentInd").Value,
                               SummarySentDt = ConvertToDateTime(obj.Element("SummarySentDt").Value),
                               SummarySentOtherCd = obj.Element("SummarySentOtherCd").Value,
                               SummarySentOtherDt = ConvertToDateTime(obj.Element("SummarySentOtherDt").Value),
                               WorkedWithAnotherAgencyInd = obj.Element("WorkedWithAnotherAgencyInd").Value,
                           };
                return objs.ToList<ForeclosureCaseDTO>()[0];
            }
            catch (NullReferenceException ex)
            {
                return null;
            }

        }

    }
}
