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
                               AcctNum = Util.ConvertToString(obj.Element("AcctNum")),
                               ArmResetInd = Util.ConvertToString(obj.Element("ArmResetInd")),
                               CurrentLoanBalanceAmt = Util.ConvertToDouble(obj.Element("CurrentLoanBalanceAmt")),
                               CurrentServicerFdicNcuaNum = Util.ConvertToString(obj.Element("CurrentServicerFdicNcuaNum")),
                               InterestRate = Util.ConvertToDouble(obj.Element("InterestRate")),
                               Loan1st2nd = Util.ConvertToString(obj.Element("Loan1st2nd")),
                               LoanDelinqStatusCd = Util.ConvertToString(obj.Element("LoanDelinqStatusCd")),
                               MortgageTypeCd = Util.ConvertToString(obj.Element("MortgageTypeCd")),
                               OrginalLoanNum = Util.ConvertToString(obj.Element("OrginalLoanNum")),
                               OriginatingLenderName = Util.ConvertToString(obj.Element("OriginatingLenderName")),
                               OrigLoanAmt = Util.ConvertToDouble(obj.Element("OrigLoanAmt")),
                               OrigMortgageCoFdicNcusNum = Util.ConvertToString(obj.Element("OrigMortgageCoFdicNcusNum")),
                               OrigMortgageCoName = Util.ConvertToString(obj.Element("OrigMortgageCoName")),
                               OtherServicerName = Util.ConvertToString(obj.Element("OtherServicerName")),
                               ServicerId = Util.ConvertToInt(obj.Element("ServicerId")),
                               TermLengthCd = Util.ConvertToString(obj.Element("TermLengthCd")),
                               MortgageProgramCd = Util.ConvertToString(obj.Element("MortgageProgramCd")),                               
                               //InvestorName = Util.ConvertToString(obj.Element("InvestorName")),
                               //InvestorNum = Util.ConvertToString(obj.Element("InvestorNum"))                              
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
                               ExtRefOtherName = Util.ConvertToString(obj.Element("ExtRefOtherName")),
                               NonprofitreferralKeyNum = Util.ConvertToString(obj.Element("NonprofitreferralKeyNum")),
                               OutcomeTypeId = Util.ConvertToInt(obj.Element("OutcomeTypeId")),
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
                               //BudgetAssetId = Util.ConvertToInt(obj.Element("BudgetAssetId")),
                               //BudgetSetId = Util.ConvertToInt(obj.Element("BudgetSetId")),
                               AssetName = Util.ConvertToString(obj.Element("AssetName")),
                               AssetValue = Util.ConvertToDouble(obj.Element("AssetValue")),
                               //ChangeLastUserId = Util.ConvertToString(obj.Element("ChangeLastUserId")),
                               //CreateUserId = Util.ConvertToString(obj.Element("CreateUserId")),
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
                               BudgetItemAmt = Util.ConvertToDouble(obj.Element("BudgetItemAmt")),
                               //BudgetItemId = Util.ConvertToInt(obj.Element("BudgetItemId")),
                               BudgetNote = Util.ConvertToString(obj.Element("BudgetNote")),
                               //BudgetSetId = Util.ConvertToInt(obj.Element("BudgetSetId")),
                               BudgetSubcategoryId = Util.ConvertToInt(obj.Element("BudgetSubcategoryId")),
                               //ChangeLastUserId = Util.ConvertToString(obj.Element("ChangeLastUserId")),
                               //CreateUserId = Util.ConvertToString(obj.Element("CreateUserId")
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
            //                   FcId = Util.ConvertToInt(obj.Element("FcId")),
            //                   ActivityCd = Util.ConvertToString(obj.Element("ActivityCd")),
            //                   ActivityDt = Util.ConvertToDateTime(obj.Element("ActivityDt")),
            //                   ActivityLogId = Util.ConvertToInt(obj.Element("ActivityLogId")),
            //                   ActivityNote = Util.ConvertToString(obj.Element("ActivityNote")
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
                               ActionItemsNotes = Util.ConvertToString(obj.Element("ActionItemsNotes")),
                               AgencyCaseNum = Util.ConvertToString(obj.Element("AgencyCaseNum")),
                               AgencyClientNum = Util.ConvertToString(obj.Element("AgencyClientNum")),
                               AgencyId = Util.ConvertToInt(obj.Element("AgencyId")),
                               AgencyMediaInterestInd = Util.ConvertToString(obj.Element("AgencyMediaConsentInd")),
                               AgencySuccessStoryInd = Util.ConvertToString(obj.Element("AgencySuccessStoryInd")),
                               AssignedCounselorIdRef = Util.ConvertToString(obj.Element("AssignedCounselorIdRef")),
                               BankruptcyAttorney = Util.ConvertToString(obj.Element("BankruptcyAttorney")),
                               BankruptcyInd = Util.ConvertToString(obj.Element("BankruptcyInd")),
                               BankruptcyPmtCurrentInd = Util.ConvertToString(obj.Element("BankruptcyPmtCurrentInd")),
                               BorrowerDisabledInd = Util.ConvertToString(obj.Element("BorrowerDisabledInd")),
                               BorrowerDob = Util.ConvertToDateTime(obj.Element("BorrowerDob")),
                               BorrowerEducLevelCompletedCd = Util.ConvertToString(obj.Element("BorrowerEducLevelCompletedCd")),
                               BorrowerFname = Util.ConvertToString(obj.Element("BorrowerFname")),
                               BorrowerLname = Util.ConvertToString(obj.Element("BorrowerLname")),
                               BorrowerMaritalStatusCd = Util.ConvertToString(obj.Element("BorrowerMaritalStatusCd")),
                               BorrowerMname = Util.ConvertToString(obj.Element("BorrowerMname")),
                               BorrowerOccupation = Util.ConvertToString(obj.Element("BorrowerOccupationCd")),
                               BorrowerPreferredLangCd = Util.ConvertToString(obj.Element("BorrowerPreferredLangCd")),
                               BorrowerSsn = Util.ConvertToString(obj.Element("BorrowerSsn")),
                               CallId = Util.ConvertToString(obj.Element("CallId")),
                               CaseSourceCd = Util.ConvertToString(obj.Element("CaseSourceCd")),
                               CoBorrowerDisabledInd = Util.ConvertToString(obj.Element("CoBorrowerDisabledInd")),
                               CoBorrowerDob = Util.ConvertToDateTime(obj.Element("CoBorrowerDob")),
                               CoBorrowerFname = Util.ConvertToString(obj.Element("CoBorrowerFname")),
                               CoBorrowerLname = Util.ConvertToString(obj.Element("CoBorrowerLname")),
                               CoBorrowerMname = Util.ConvertToString(obj.Element("CoBorrowerMname")),
                               CoBorrowerOccupation = Util.ConvertToString(obj.Element("CoBorrowerOccupationCd")),
                               CoBorrowerSsn = Util.ConvertToString(obj.Element("CoBorrowerSsn")),
                               ContactAddr1 = Util.ConvertToString(obj.Element("ContactAddr1")),
                               ContactAddr2 = Util.ConvertToString(obj.Element("ContactAddr2")),
                               ContactCity = Util.ConvertToString(obj.Element("ContactCity")),
                               ContactedSrvcrRecentlyInd = Util.ConvertToString(obj.Element("ContactedSrvcrRecentlyInd")),
                               ContactStateCd = Util.ConvertToString(obj.Element("ContactStateCd")),
                               ContactZip = Util.ConvertToString(obj.Element("ContactZip")),
                               ContactZipPlus4 = Util.ConvertToString(obj.Element("ContactZipPlus4")),
                               CounselingDurationCd = Util.ConvertToString(obj.Element("CounselingDurationCd")),
                               CounselorEmail = Util.ConvertToString(obj.Element("CounselorEmail")),
                               CounselorExt = Util.ConvertToString(obj.Element("CounselorExt")),
                               CounselorFname = Util.ConvertToString(obj.Element("CounselorFname")),
                               CounselorLname = Util.ConvertToString(obj.Element("CounselorLname")),
                               CounselorPhone = Util.ConvertToString(obj.Element("CounselorPhone")),
                               DfltReason1stCd = Util.ConvertToString(obj.Element("DfltReason1stCd")),
                               DfltReason2ndCd = Util.ConvertToString(obj.Element("DfltReason2ndCd")),
                               DiscussedSolutionWithSrvcrInd = Util.ConvertToString(obj.Element("DiscussedSolutionWithSrvcrInd")),
                               Email1 = Util.ConvertToString(obj.Element("Email1")),
                               Email2 = Util.ConvertToString(obj.Element("Email2")),
                               FcNoticeReceiveInd = Util.ConvertToString(obj.Element("FcNoticeReceiveInd")),
                               FcSaleDate = Util.ConvertToDateTime(obj.Element("FcSaleDate")),

                               FollowupNotes = Util.ConvertToString(obj.Element("FollowupNotes")),
                               ForSaleInd = Util.ConvertToString(obj.Element("ForSaleInd")),
                               FundingConsentInd = Util.ConvertToString(obj.Element("FundingConsentInd")),
                               GenderCd = Util.ConvertToString(obj.Element("GenderCd")),
                               HasWorkoutPlanInd = Util.ConvertToString(obj.Element("HasWorkoutPlanInd")),
                               HispanicInd = Util.ConvertToString(obj.Element("HispanicInd")),
                               HomeCurrentMarketValue = Util.ConvertToDouble(obj.Element("HomeCurrentMarketValue")),
                               HomePurchasePrice = Util.ConvertToDouble(obj.Element("HomePurchasePrice")),
                               HomePurchaseYear = Util.ConvertToInt(obj.Element("HomePurchaseYear")),
                               HomeSalePrice = Util.ConvertToDouble(obj.Element("HomeSalePrice")),
                               HouseholdCd = Util.ConvertToString(obj.Element("HouseholdCd")),
                               HouseholdGrossAnnualIncomeAmt = Util.ConvertToDouble(obj.Element("HouseholdGrossAnnualIncomeAmt")),
                               HudOutcomeCd = Util.ConvertToString(obj.Element("HudOutcomeCd")),
                               HudTerminationDt = Util.ConvertToDateTime(obj.Element("HudTerminationDt")),
                               HudTerminationReasonCd = Util.ConvertToString(obj.Element("HudTerminationReasonCd")),
                               IncomeEarnersCd = Util.ConvertToString(obj.Element("IncomeEarnersCd")),
                               IntakeCreditBureauCd = Util.ConvertToString(obj.Element("IntakeCreditBureauCd")),
                               IntakeCreditScore = Util.ConvertToString(obj.Element("IntakeCreditScore")),
                               IntakeDt = Util.ConvertToDateTime(obj.Element("IntakeDt")),

                               LoanDfltReasonNotes = Util.ConvertToString(obj.Element("LoanDfltReasonNotes")),
                               MilitaryServiceCd = Util.ConvertToString(obj.Element("MilitaryServiceCd")),
                               MotherMaidenLname = Util.ConvertToString(obj.Element("MotherMaidenLname")),
                               OccupantNum = Util.ConvertToByte(obj.Element("OccupantNum")),
                               OwnerOccupiedInd = Util.ConvertToString(obj.Element("OwnerOccupiedInd")),
                               PrimaryContactNo = Util.ConvertToString(obj.Element("PrimaryContactNo")),
                               PrimaryResidenceInd = Util.ConvertToString(obj.Element("PrimaryResidenceInd")),
                               PrimResEstMktValue = Util.ConvertToDouble(obj.Element("PrimResEstMktValue")),
                               ProgramId = Util.ConvertToInt(obj.Element("ProgramId")),
                               PropAddr1 = Util.ConvertToString(obj.Element("PropAddr1")),
                               PropAddr2 = Util.ConvertToString(obj.Element("PropAddr2")),
                               PropCity = Util.ConvertToString(obj.Element("PropCity")),
                               PropertyCd = Util.ConvertToString(obj.Element("PropertyCd")),
                               PropStateCd = Util.ConvertToString(obj.Element("PropStateCd")),
                               PropZip = Util.ConvertToString(obj.Element("PropZip")),
                               PropZipPlus4 = Util.ConvertToString(obj.Element("PropZipPlus4")),
                               RaceCd = Util.ConvertToString(obj.Element("RaceCd")),
                               RealtyCompany = Util.ConvertToString(obj.Element("RealtyCompany")),
                               SecondContactNo = Util.ConvertToString(obj.Element("SecondContactNo")),
                               ServicerConsentInd = Util.ConvertToString(obj.Element("ServicerConsentInd")),
                               SrvcrWorkoutPlanCurrentInd = Util.ConvertToString(obj.Element("SrvcrWorkoutPlanCurrentInd")),
                               SummarySentOtherCd = Util.ConvertToString(obj.Element("SummarySentOtherCd")),
                               SummarySentOtherDt = Util.ConvertToDateTime(obj.Element("SummarySentOtherDt")),
                               WorkedWithAnotherAgencyInd = Util.ConvertToString(obj.Element("WorkedWithAnotherAgencyInd")),
                               ChgLstUserId = Util.ConvertToString(obj.Element("ChgLstUserId"))
                               
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
                //InvestorName = this.InvestorName,
                //InvestorNum = this.InvestorNum,
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
                TermLengthCd = this.TermLengthCd,
                MortgageProgramCd = this.MortgageProgramCd                

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
