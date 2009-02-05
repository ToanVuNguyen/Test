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
    public class AgencyHelper
    {
        public static List<CaseLoanDTO_App> ParseCaseLoanDTO(XDocument xdoc)
        {            
            try
            {
                var objs = from obj in xdoc.Descendants("CaseLoan")
                           select new CaseLoanDTO_App
                           {                                 
                               AcctNum = obj.Element("AcctNum").Value,
                               ArmResetInd = obj.Element("ArmResetInd").Value,
                               CurrentLoanBalanceAmt = Util.ConvertToDouble(obj.Element("CurrentLoanBalanceAmt").Value),
                               CurrentServicerFdicNcuaNum = obj.Element("CurrentServicerFdicNcuaNum").Value,
                               InterestRate = Util.ConvertToDouble(obj.Element("InterestRate").Value),
                               Loan1st2nd = obj.Element("Loan1st2nd").Value,
                               LoanDelinqStatusCd = obj.Element("LoanDelinqStatusCd").Value,
                               MortgageTypeCd = obj.Element("MortgageTypeCd").Value,
                               OrginalLoanNum = obj.Element("OrginalLoanNum").Value,
                               OriginatingLenderName = obj.Element("OriginatingLenderName").Value,
                               OrigLoanAmt = Util.ConvertToDouble(obj.Element("OrigLoanAmt").Value),
                               OrigMortgageCoFdicNcusNum = obj.Element("OrigMortgageCoFdicNcusNum").Value,
                               OrigMortgageCoName = obj.Element("OrigMortgageCoName").Value,
                               OtherServicerName = obj.Element("OtherServicerName").Value,
                               ServicerId = Util.ConvertToInt(obj.Element("ServicerId").Value),
                               TermLengthCd = obj.Element("TermLengthCd").Value
                           };
                int i = 1;
                List<CaseLoanDTO_App> list = objs.ToList<CaseLoanDTO_App>();
                foreach (var item in list)
                {
                    item.CaseLoanId = i;
                    i++;
                }
                return list;
            }
            catch (NullReferenceException ex)
            {
                return null;
            }

        }

        public static List<OutcomeItemDTO_App> ParseOutcomeItemDTO(XDocument xdoc)
        {
            try
            {
                var objs = from obj in xdoc.Descendants("OutcomeItem")
                           select new OutcomeItemDTO_App
                           {
                               //OutcomeItemId = Util.ConvertToInt(obj.Element("OutcomeTypeId").Value),
                               ExtRefOtherName = obj.Element("ExtRefOtherName").Value,
                               //FcId = Util.ConvertToInt(obj.Element("FcId").Value),
                               NonprofitreferralKeyNum = obj.Element("NonprofitreferralKeyNum").Value,
                               //OutcomeDeletedDt = Util.ConvertToDateTime(obj.Element("OutcomeDeletedDt").Value),
                               //OutcomeDt = Util.ConvertToDateTime(obj.Element("OutcomeDt").Value),
                               //OutcomeSetId = Util.ConvertToInt(obj.Element("OutcomeSetId").Value),
                               OutcomeTypeId = Util.ConvertToInt(obj.Element("OutcomeTypeId").Value),
                               //ChangeLastUserId = obj.Element("ChangeLastUserId").Value,
                               //CreateUserId = obj.Element("CreateUserId").Value
                           };
                int i = 1;
                List<OutcomeItemDTO_App> list = objs.ToList<OutcomeItemDTO_App>();
                foreach (var item in list)
                {
                    item.OutcomeItemId = i;
                    i++;
                }
                return list;
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
        }

        public static List<BudgetAssetDTO_App> ParseBudgetAssetDTO(XDocument xdoc)
        {
            try
            {
                var objs = from obj in xdoc.Descendants("BudgetAsset")
                           select new BudgetAssetDTO_App
                           {
                               //BudgetAssetId = Util.ConvertToInt(obj.Element("BudgetAssetId").Value),
                               //BudgetSetId = Util.ConvertToInt(obj.Element("BudgetSetId").Value),
                               AssetName = obj.Element("AssetName").Value,
                               AssetValue = Util.ConvertToDouble(obj.Element("AssetValue").Value),
                               //ChangeLastUserId = obj.Element("ChangeLastUserId").Value,
                               //CreateUserId = obj.Element("CreateUserId").Value,
                           };
                int i = 1;
                List<BudgetAssetDTO_App> list = objs.ToList<BudgetAssetDTO_App>();
                foreach (var item in list)
                {
                    item.BudgetAssetId = i;
                    i++;
                }
                return list;
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
        }

        public static List<BudgetItemDTO_App> ParseBudgetItemDTO(XDocument xdoc)
        {
            try
            {
                var objs = from obj in xdoc.Descendants("BudgetItem")
                           select new BudgetItemDTO_App
                           {
                               BudgetItemAmt = Util.ConvertToDouble(obj.Element("BudgetItemAmt").Value),
                               //BudgetItemId = Util.ConvertToInt(obj.Element("BudgetItemId").Value),
                               BudgetNote = obj.Element("BudgetNote").Value,
                               //BudgetSetId = Util.ConvertToInt(obj.Element("BudgetSetId").Value),
                               BudgetSubcategoryId = Util.ConvertToInt(obj.Element("BudgetSubcategoryId").Value),
                               //ChangeLastUserId = obj.Element("ChangeLastUserId").Value,
                               //CreateUserId = obj.Element("CreateUserId").Value
                           };
                int i = 1;
                List<BudgetItemDTO_App> list = objs.ToList<BudgetItemDTO_App>();
                foreach (var item in list)
                {
                    item.BudgetItemId = i;
                    i++;
                }
                return list;
            }
            catch (NullReferenceException ex)
            {
                return null;
            }

        }

        //public static List<ActivityLogDTO> ParseActivityLogDTO(XDocument xdoc)
        //{
            //try
            //{
            //    var objs = from obj in xdoc.Descendants("ActivityLog")
            //               select new ActivityLogDTO
            //               {
            //                   FcId = Util.ConvertToInt(obj.Element("FcId").Value),
            //                   ActivityCd = obj.Element("ActivityCd").Value,
            //                   ActivityDt = Util.ConvertToDateTime(obj.Element("ActivityDt").Value),
            //                   ActivityLogId = Util.ConvertToInt(obj.Element("ActivityLogId").Value),
            //                   ActivityNote = obj.Element("ActivityNote").Value
            //               };
            //    return objs.ToList<ActivityLogDTO>();
            //}
            //catch (NullReferenceException ex)
            //{
            //    return null;
            //}
        //}

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
                               AgencyId = Util.ConvertToInt(obj.Element("AgencyId").Value),
                               AgencyMediaInterestInd = obj.Element("AgencyMediaConsentInd").Value,
                               AgencySuccessStoryInd = obj.Element("AgencySuccessStoryInd").Value,
                               //AmiPercentage = Util.ConvertToInt(obj.Element("AmiPercentage").Value),
                               AssignedCounselorIdRef = obj.Element("AssignedCounselorIdRef").Value,
                               BankruptcyAttorney = obj.Element("BankruptcyAttorney").Value,
                               BankruptcyInd = obj.Element("BankruptcyInd").Value,
                               BankruptcyPmtCurrentInd = obj.Element("BankruptcyPmtCurrentInd").Value,
                               BorrowerDisabledInd = obj.Element("BorrowerDisabledInd").Value,
                               BorrowerDob = Util.ConvertToDateTime(obj.Element("BorrowerDob").Value),
                               BorrowerEducLevelCompletedCd = obj.Element("BorrowerEducLevelCompletedCd").Value,
                               BorrowerFname = obj.Element("BorrowerFname").Value,
                               //BorrowerLast4Ssn = obj.Element("BorrowerLast4Ssn").Value,
                               BorrowerLname = obj.Element("BorrowerLname").Value,
                               BorrowerMaritalStatusCd = obj.Element("BorrowerMaritalStatusCd").Value,
                               BorrowerMname = obj.Element("BorrowerMname").Value,
                               BorrowerOccupationCd = obj.Element("BorrowerOccupationCd").Value,
                               BorrowerPreferredLangCd = obj.Element("BorrowerPreferredLangCd").Value,
                               BorrowerSsn = obj.Element("BorrowerSsn").Value,
                               CallId = obj.Element("CallId").Value,
                               //CaseCompleteInd = obj.Element("CaseCompleteInd").Value,
                               CaseSourceCd = obj.Element("CaseSourceCd").Value,
                               CoBorrowerDisabledInd = obj.Element("CoBorrowerDisabledInd").Value,
                               CoBorrowerDob = Util.ConvertToDateTime(obj.Element("CoBorrowerDob").Value),
                               CoBorrowerFname = obj.Element("CoBorrowerFname").Value,
                               //CoBorrowerLast4Ssn = obj.Element("CoBorrowerLast4Ssn").Value,
                               CoBorrowerLname = obj.Element("CoBorrowerLname").Value,
                               CoBorrowerMname = obj.Element("CoBorrowerMname").Value,
                               CoBorrowerOccupationCd = obj.Element("CoBorrowerOccupationCd").Value,
                               CoBorrowerSsn = obj.Element("CoBorrowerSsn").Value,
                               //CompletedDt = Util.ConvertToDateTime(obj.Element("CompletedDt").Value),
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
                               //DoNotCallInd = obj.Element("DoNotCallInd").Value,
                               //DuplicateInd = obj.Element("DuplicateInd").Value,
                               Email1 = obj.Element("Email1").Value,
                               Email2 = obj.Element("Email2").Value,
                               //FcId = Util.ConvertToInt(obj.Element("FcId").Value),
                               FcNoticeReceiveInd = obj.Element("FcNoticeReceiveInd").Value,
                               FcSaleDate = Util.ConvertToDateTime(obj.Element("FcSaleDate").Value),
                               
                               FollowupNotes = obj.Element("FollowupNotes").Value,
                               ForSaleInd = obj.Element("ForSaleInd").Value,
                               FundingConsentInd = obj.Element("FundingConsentInd").Value,
                               GenderCd = obj.Element("GenderCd").Value,
                               HasWorkoutPlanInd = obj.Element("HasWorkoutPlanInd").Value,
                               HispanicInd = obj.Element("HispanicInd").Value,
                               HomeCurrentMarketValue = Util.ConvertToDouble(obj.Element("HomeCurrentMarketValue").Value),
                               HomePurchasePrice = Util.ConvertToDouble(obj.Element("HomePurchasePrice").Value),
                               HomePurchaseYear = Util.ConvertToInt(obj.Element("HomePurchaseYear").Value),
                               HomeSalePrice = Util.ConvertToDouble(obj.Element("HomeSalePrice").Value),
                               HouseholdCd = obj.Element("HouseholdCd").Value,
                               HouseholdGrossAnnualIncomeAmt = Util.ConvertToDouble(obj.Element("HouseholdGrossAnnualIncomeAmt").Value),
                               //HpfMediaCandidateInd = obj.Element("HpfMediaCandidateInd").Value,
                               //HpfNetworkCandidateInd = obj.Element("HpfNetworkCandidateInd").Value,
                               //HpfSuccessStoryInd = obj.Element("HpfSuccessStoryInd").Value,
                               HudOutcomeCd = obj.Element("HudOutcomeCd").Value,
                               HudTerminationDt = Util.ConvertToDateTime(obj.Element("HudTerminationDt").Value),
                               HudTerminationReasonCd = obj.Element("HudTerminationReasonCd").Value,
                               IncomeEarnersCd = obj.Element("IncomeEarnersCd").Value,
                               IntakeCreditBureauCd = obj.Element("IntakeCreditBureauCd").Value,
                               IntakeCreditScore = obj.Element("IntakeCreditScore").Value,
                               IntakeDt = Util.ConvertToDateTime(obj.Element("IntakeDt").Value),
                               LoanDfltReasonNotes = obj.Element("LoanDfltReasonNotes").Value,
                               //LoanList = obj.Element("LoanList").Value,
                               MilitaryServiceCd = obj.Element("MilitaryServiceCd").Value,
                               MotherMaidenLname = obj.Element("MotherMaidenLname").Value,
                               //NeverBillReasonCd = obj.Element("NeverBillReasonCd").Value,
                               //NeverPayReasonCd = obj.Element("NeverPayReasonCd").Value,
                               OccupantNum = Util.ConvertToByte(obj.Element("OccupantNum").Value),
                               //OptOutNewsletterInd = obj.Element("OptOutNewsletterInd").Value,
                               //OptOutSurveyInd = obj.Element("OptOutSurveyInd").Value,
                               OwnerOccupiedInd = obj.Element("OwnerOccupiedInd").Value,
                               PrimaryContactNo = obj.Element("PrimaryContactNo").Value,
                               PrimaryResidenceInd = obj.Element("PrimaryResidenceInd").Value,
                               PrimResEstMktValue = Util.ConvertToDouble(obj.Element("PrimResEstMktValue").Value),
                               ProgramId = Util.ConvertToInt(obj.Element("ProgramId").Value),
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
                               //SummarySentDt = Util.ConvertToDateTime(obj.Element("SummarySentDt").Value),
                               SummarySentOtherCd = obj.Element("SummarySentOtherCd").Value,
                               SummarySentOtherDt = Util.ConvertToDateTime(obj.Element("SummarySentOtherDt").Value),
                               WorkedWithAnotherAgencyInd = obj.Element("WorkedWithAnotherAgencyInd").Value,
                               ChangeLastUserId = obj.Element("ChangeLastUserId").Value,                               
                               CreateUserId = obj.Element("CreateUserId").Value
                               
                           };
                ForeclosureCaseDTO fcCase = objs.ToList<ForeclosureCaseDTO>()[0];
                return fcCase;
            }
            catch (NullReferenceException ex)
            {
                return null;
            }

        }

        

    }

    public class CaseLoanDTO_App : CaseLoanDTO
    {
        public int? CaseLoanId { get; set; }
        public CaseLoanDTO ConvertToBase()
        {

            return new CaseLoanDTO()
            {

                AcctNum = this.AcctNum,
                ArmResetInd = this.ArmResetInd,
                CurrentLoanBalanceAmt = this.CurrentLoanBalanceAmt,
                CurrentServicerFdicNcuaNum = this.CurrentServicerFdicNcuaNum,
                InterestRate = this.InterestRate,
                InvestorName = this.InvestorName,
                InvestorNum = this.InvestorNum,
                Loan1st2nd = this.Loan1st2nd,
                LoanDelinqStatusCd = this.LoanDelinqStatusCd,
                MortgageTypeCd = this.MortgageTypeCd,
                OrginalLoanNum = this.OrginalLoanNum,
                OriginatingLenderName = this.OriginatingLenderName,
                OrigLoanAmt = this.OrigLoanAmt,
                OrigMortgageCoFdicNcusNum = this.OrigMortgageCoFdicNcusNum,
                OrigMortgageCoName = this.OrigMortgageCoName,
                OtherServicerName = this.OtherServicerName,
                ServicerId = this.ServicerId,
                TermLengthCd = this.TermLengthCd

            };
        }
    }

    public class OutcomeItemDTO_App : OutcomeItemDTO
    {
        public int? OutcomeItemId { get; set; }
        public OutcomeItemDTO ConvertToBase()
        {
            return new OutcomeItemDTO()
            {
                ExtRefOtherName = this.ExtRefOtherName,
                NonprofitreferralKeyNum = this.NonprofitreferralKeyNum,
                OutcomeTypeId = this.OutcomeTypeId
            };
        }
    }

    public class BudgetItemDTO_App : BudgetItemDTO
    {
        public int? BudgetItemId { get; set; }
        public BudgetItemDTO ConvertToBase()
        {
            return new BudgetItemDTO()
            {
                BudgetItemAmt = this.BudgetItemAmt,
                BudgetNote = this.BudgetNote,
                BudgetSubcategoryId = this.BudgetSubcategoryId
            };
        }
    }

    public class BudgetAssetDTO_App : BudgetAssetDTO
    {
        public int? BudgetAssetId { get; set; }
        public BudgetAssetDTO ConvertToBase()
        {
            return new BudgetAssetDTO()
            {
                AssetName = this.AssetName,
                AssetValue = this.AssetValue
               
            };
        }
    }
}
