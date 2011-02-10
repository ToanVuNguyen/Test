using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Collections.Generic;

using System.Xml;
using System.Xml.XPath;
using HPF.Webservice.Agency;
using System.Collections.ObjectModel;


namespace HPF.FutureState.WebService.Test.Web
{
    public partial class SaveForeclosureCase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadDefaultFcCase();
            }
            else
            {
                if (!(String.IsNullOrEmpty(txtPassword.Text.Trim())))
                {
                    txtPassword.Attributes["value"] = txtPassword.Text;
                }
            }
        }
        private void LoadDefaultFcCase()
        {
            string filename = MapPath(ConfigurationManager.AppSettings["ForeclosureCaseSetXML"]);
            XDocument xdoc = GetXmlDocument(filename);
            BindToForm(xdoc);
        }

        private void BindToForm(XDocument xdoc)
        {
            Session[SessionVariables.CASE_LOAN_COLLECTION] = AgencyHelper.ParseCaseLoanDTO(xdoc);
            Session[SessionVariables.BUDGET_ASSET_COLLECTION] = AgencyHelper.ParseBudgetAssetDTO(xdoc);
            Session[SessionVariables.BUDGET_ITEM_COLLECTION] = AgencyHelper.ParseBudgetItemDTO(xdoc);
            //Session[SessionVariables.ACTIVITY_LOG_COLLECTION] = Util.ParseActivityLogDTO(xdoc);
            Session[SessionVariables.OUTCOME_ITEM_COLLECTION] = AgencyHelper.ParseOutcomeItemDTO(xdoc);
            Session[SessionVariables.FORECLOSURE_CASE] = AgencyHelper.ParseForeclosureCaseDTO(xdoc);
            Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION] = AgencyHelper.ParseProposedBudgetItemDTO(xdoc);

            grdvCaseLoanBinding();
            grdvOutcomeItemBinding();
            grdvBudgetItemBinding();
            grdvProposedBudgetItemBinding();
            grdvBudgetAssetBinding();
            //grdvActivityLogBinding();
            ForeclosureCaseToForm((ForeclosureCaseDTO)Session[SessionVariables.FORECLOSURE_CASE]);
        }

        private ForeclosureCaseDTO FormToForeclosureCase()
        {
            ForeclosureCaseDTO fcCase = new ForeclosureCaseDTO();

            fcCase.ActionItemsNotes = txtActionItemsNotes.Text.Trim();
            fcCase.AgencyCaseNum = txtAgencyCaseNumber.Text.Trim();
            fcCase.AgencyClientNum = txtAgencyClientNumber.Text.Trim();
            fcCase.AgencyId = Util.ConvertToInt(txtAgencyID.Text.Trim());
            fcCase.AgencyMediaInterestInd = txtAgencyMediaConsentInd.Text.Trim();
            fcCase.AgencySuccessStoryInd = txtAgencySuccessStory.Text.Trim();
            //fcCase.AmiPercentage = Util.ConvertToInt(txtAMIPercentage.Text.Trim());
            fcCase.AssignedCounselorIdRef = txtAssignedCounselorIDRef.Text.Trim();
            fcCase.BankruptcyAttorney = txtBankruptcyAttorney.Text.Trim();
            fcCase.BankruptcyInd = txtBankruptcyInd.Text.Trim();
            fcCase.BankruptcyPmtCurrentInd = txtBankruptcyPmtCurrentInd.Text.Trim();
            fcCase.BorrowerDisabledInd = txtBorrowerDisabledInd.Text.Trim();
            fcCase.BorrowerDob = Util.ConvertToDateTime(txtBorrowerDOB.Text.Trim());
            fcCase.BorrowerEducLevelCompletedCd = txtBorrowerEducLevelCompletedInd.Text.Trim();
            fcCase.BorrowerFname = txtBorrowerFName.Text.Trim();
            //fcCase.BorrowerLast4Ssn = txtBorrowerLast4SSN.Text.Trim();
            fcCase.BorrowerLname = txtBorrowerLName.Text.Trim();
            fcCase.BorrowerMaritalStatusCd = txtBorrowerMaritalStatusCd.Text.Trim();
            fcCase.BorrowerMname = txtBorrowerMName.Text.Trim();
            fcCase.BorrowerOccupation = txtBorrowerOccupationCd.Text.Trim();
            fcCase.BorrowerPreferredLangCd = txtBorrowerPreferedLangCd.Text.Trim();
            fcCase.BorrowerSsn = txtBorrowerSSN.Text.Trim();
            fcCase.CallId = txtCallID.Text.Trim();
            //fcCase.CaseCompleteInd = txtCaseCompleteInd.Text.Trim();
            fcCase.CaseSourceCd = txtCaseSourceCd.Text.Trim();
            fcCase.CoBorrowerDisabledInd = txtCoBorrowerDisabledInd.Text.Trim();
            fcCase.CoBorrowerDob = Util.ConvertToDateTime(txtCoBorrowerDOB.Text.Trim());
            fcCase.CoBorrowerFname = txtCoBorrowerFName.Text.Trim();
            //fcCase.CoBorrowerLast4Ssn = txtCoBorrowerLast4SSN.Text.Trim();
            fcCase.CoBorrowerLname = txtCoBorrowerLName.Text.Trim();
            fcCase.CoBorrowerMname = txtCoBorrowerMName.Text.Trim();
            fcCase.CoBorrowerOccupation = txtCoBorrowerOccupationCd.Text.Trim();
            fcCase.CoBorrowerSsn = txtCoBorrowerSSN.Text.Trim();
            //fcCase.CompletedDt = Util.ConvertToDateTime(txtCompletedDt.Text.Trim());
            fcCase.ContactAddr1 = txtContactAddress1.Text.Trim();
            fcCase.ContactAddr2 = txtContactAddress2.Text.Trim();
            fcCase.ContactCity = txtContactCity.Text.Trim();
            fcCase.ContactedSrvcrRecentlyInd = txtContactedSrvcrRecentlyInd.Text.Trim();
            fcCase.ContactStateCd = txtContactStateCd.Text.Trim();
            fcCase.ContactZip = txtContactZip.Text.Trim();
            fcCase.ContactZipPlus4 = txtContactZipPlus4.Text.Trim();
            fcCase.CounselingDurationCd = txtCounselingDurationCd.Text.Trim();
            fcCase.CounselorEmail = txtCounselorEmail.Text.Trim();
            fcCase.CounselorExt = txtCounselorExt.Text.Trim();
            fcCase.CounselorFname = txtCounselorFirstName.Text.Trim();
            fcCase.CounselorLname = txtCounselorLastName.Text.Trim();
            fcCase.CounselorPhone = txtCounselorPhone.Text.Trim();
            fcCase.DfltReason1stCd = txtDfltReason1stCd.Text.Trim();
            fcCase.DfltReason2ndCd = txtDfltReason2ndCd.Text.Trim();
            fcCase.DiscussedSolutionWithSrvcrInd = txtDiscussedSolutionWithSrvcrInd.Text.Trim();
            //fcCase.DoNotCallInd = txtDoNotCallInd.Text.Trim();
            //fcCase.DuplicateInd = txtDuplicateInd.Text.Trim();
            fcCase.Email1 = txtEmail1.Text.Trim();
            fcCase.Email2 = txtEmail2.Text.Trim();
            fcCase.FcId = Util.ConvertToInt(txtFcID.Text.Trim());
            fcCase.FcNoticeReceiveInd = txtFcNoticeReceivedInd.Text.Trim();
            //fcCase.FcSaleDateSetInd = txtFcSaleDateSetInd.Text.Trim();
            fcCase.FollowupNotes = txtFollowupNotes.Text.Trim();
            fcCase.ForSaleInd = txtForSaleInd.Text.Trim();
            fcCase.FundingConsentInd = txtFundingConsentInd.Text.Trim();
            fcCase.GenderCd = txtGenderCd.Text.Trim();
            fcCase.HasWorkoutPlanInd = txtHasWorkoutPlanInd.Text.Trim();
            fcCase.HispanicInd = txtHispanicInd.Text.Trim();
            fcCase.HomeCurrentMarketValue = Util.ConvertToDouble(txtHomeCurMktValue.Text.Trim());
            fcCase.HomePurchasePrice = Util.ConvertToDouble(txtHomePurchasePrice.Text.Trim());
            fcCase.HomePurchaseYear = Util.ConvertToInt(txtHomePurchaseYear.Text.Trim());
            fcCase.HomeSalePrice = Util.ConvertToDouble(txtHomeSalePrice.Text.Trim());
            fcCase.HouseholdCd = txtHouseholdCd.Text.Trim();
            fcCase.HouseholdGrossAnnualIncomeAmt = Util.ConvertToDouble(txtHousholdGrossAnnualIncomeAmt.Text.Trim());
            //fcCase.HpfMediaCandidateInd = txtHpfMediaCandidateInd.Text.Trim();
            //fcCase.HpfNetworkCandidateInd = txtHpfNetworkCandidateInd.Text.Trim();
            //fcCase.HpfSuccessStoryInd = txtHpfSuccessStoryInd.Text.Trim();
            fcCase.HudOutcomeCd = txtHudOutcomeCd.Text.Trim();
            fcCase.HudTerminationDt = Util.ConvertToDateTime(txtHudTerminationDt.Text.Trim());
            fcCase.HudTerminationReasonCd = txtHudTerminationReasonCd.Text.Trim();
            fcCase.IncomeEarnersCd = txtIncomeEarnersCd.Text.Trim();
            fcCase.IntakeCreditBureauCd = txtIntakeCreditBureauCd.Text.Trim();
            fcCase.IntakeCreditScore = txtIntakeCreditScore.Text.Trim();
            fcCase.IntakeDt = Util.ConvertToDateTime(txtIntakeDt.Text.Trim());
            fcCase.LoanDfltReasonNotes = txtLoanDfltReasonNotes.Text.Trim();
            //fcCase.LoanList = txtLoanList.Text.Trim();
            fcCase.MilitaryServiceCd = txtMilitaryServiceCd.Text.Trim();
            fcCase.MotherMaidenLname = txtMotherMaidenLName.Text.Trim();
            //fcCase.NeverBillReasonCd = txtNeverBillReasonCd.Text.Trim();
            //fcCase.NeverPayReasonCd = txtNeverPayReasonCd.Text.Trim();
            fcCase.OccupantNum = Util.ConvertToByte(txtOccupantNum.Text.Trim());
            //fcCase.OptOutNewsletterInd = txtOptOutNewsletterInd.Text.Trim();
            //fcCase.OptOutSurveyInd = txtOptOutSurveyInd.Text.Trim();
            fcCase.OwnerOccupiedInd = txtOwnerOccupiedInd.Text.Trim();
            fcCase.PrimaryContactNo = txtPrimaryContactNo.Text.Trim();
            fcCase.PrimaryResidenceInd = txtPrimaryResidenceInd.Text.Trim();
            fcCase.PrimResEstMktValue = Util.ConvertToDouble(txtPrimResEstMktValue.Text.Trim());
            fcCase.ProgramId = Util.ConvertToInt(txtProgramID.Text.Trim());
            fcCase.PropAddr1 = txtPropertyAddress1.Text.Trim();
            fcCase.PropAddr2 = txtPropertyAddress2.Text.Trim();
            fcCase.PropCity = txtPropertyCity.Text.Trim();
            fcCase.PropertyCd = txtPropertyCd.Text.Trim();
            fcCase.PropStateCd = txtPropertyStateCd.Text.Trim();
            fcCase.PropZip = txtPropertyZip.Text.Trim();
            fcCase.PropZipPlus4 = txtPropertyZipPlus4.Text.Trim();
            fcCase.RaceCd = txtRaceCd.Text.Trim();
            fcCase.RealtyCompany = txtRealtyCompany.Text.Trim();
            fcCase.SecondContactNo = txtSecondContactNo.Text.Trim();
            fcCase.ServicerConsentInd = txtServicerConsentInd.Text.Trim();
            fcCase.SrvcrWorkoutPlanCurrentInd = txtSrvcrWorkoutPlanInd.Text.Trim();
            //fcCase.SummarySentDt = Util.ConvertToDateTime(txtSummarySentDt.Text.Trim());
            fcCase.SummarySentOtherCd = txtSummarySentOtherCd.Text.Trim();
            fcCase.SummarySentOtherDt = Util.ConvertToDateTime(txtSummarySentOtherDt.Text.Trim());
            fcCase.WorkedWithAnotherAgencyInd = txtWorkedWithAnotherAgencyInd.Text.Trim();
            fcCase.FcSaleDate = Util.ConvertToDateTime(txtFcSaleDate.Text.Trim());
            //fcCase.CreateUserId = txtCreateUserID.Text.Trim();
            //fcCase.ChangeLastUserId = txtChangeLastUserID.Text.Trim();

            fcCase.VipInd = txtVipInd.Text; 
            fcCase.VipReason = txtVipReason.Text;
            fcCase.CounseledLanguageCd = txtCounseledLanguageCd.Text;
            fcCase.ErcpOutcomeCd = txtErcpOutcomeCd.Text;
            fcCase.CounselorContactedSrvcrInd = txtCounselorContactedSrvcrInd.Text;
            fcCase.NumberOfUnits = Util.ConvertToInt(txtNumberOfUnits.Text);
            fcCase.VacantOrCondemedInd = txtVacantOrCondemedInd.Text;
            fcCase.MortgagePmtRatio = Util.ConvertToDouble(txtMortgagePmtRatio.Text);
            fcCase.CertificateId = txtCertififateID.Text;
            fcCase.ReferralClientNum = txtReferralNum.Text;
            fcCase.CampaignId = Util.ConvertToInt(txtCampaignId.Text);
            fcCase.SponsorId = Util.ConvertToInt(txtSponsorId.Text);
            fcCase.CounselorAttemptedSrvcrContactInd = Util.ConvertToString(txtCounselorAttemptedSrvcrContactInd.Text);
            fcCase.DependentNum = Util.ConvertToByte(txtDependentNum.Text);
            fcCase.PrimaryContactNoTypeCd = Util.ConvertToString(txtPrimaryContactNoTypeCd.Text);
            fcCase.SecondContactNoTypeCd = Util.ConvertToString(txtSecondContactNoTypeCd.Text);
            fcCase.PreferredContactTime = Util.ConvertToString(txtPreferredContactTime.Text);

            Session[SessionVariables.FORECLOSURE_CASE] = fcCase;

            return fcCase;


        }

        private void ForeclosureCaseToForm(ForeclosureCaseDTO fcCase)
        {
            if (fcCase != null)
            {
                if (fcCase.FcId != null)
                    txtFcID.Text = fcCase.FcId.ToString();
                txtActionItemsNotes.Text = fcCase.ActionItemsNotes;
                txtAgencyCaseNumber.Text = fcCase.AgencyCaseNum;
                txtAgencyClientNumber.Text = fcCase.AgencyClientNum;
                txtAgencyID.Text = Util.ConvertToString(fcCase.AgencyId);
                txtAgencyMediaConsentInd.Text = fcCase.AgencyMediaInterestInd;
                txtAgencySuccessStory.Text = fcCase.AgencySuccessStoryInd;
                //txtAMIPercentage.Text = fcCase.AmiPercentage;
                txtAssignedCounselorIDRef.Text = fcCase.AssignedCounselorIdRef;
                txtBankruptcyAttorney.Text = fcCase.BankruptcyAttorney;
                txtBankruptcyInd.Text = fcCase.BankruptcyInd;
                txtBankruptcyPmtCurrentInd.Text = fcCase.BankruptcyPmtCurrentInd;
                txtBorrowerDisabledInd.Text = fcCase.BorrowerDisabledInd;
                txtBorrowerDOB.Text = Util.ConvertToString(fcCase.BorrowerDob);
                txtBorrowerEducLevelCompletedInd.Text = fcCase.BorrowerEducLevelCompletedCd;
                txtBorrowerFName.Text = fcCase.BorrowerFname;
                //txtBorrowerLast4SSN.Text = fcCase.BorrowerLast4Ssn;
                txtBorrowerLName.Text = fcCase.BorrowerLname;
                txtBorrowerMaritalStatusCd.Text = fcCase.BorrowerMaritalStatusCd;
                txtBorrowerMName.Text = fcCase.BorrowerMname;
                txtBorrowerOccupationCd.Text = fcCase.BorrowerOccupation;
                txtBorrowerPreferedLangCd.Text = fcCase.BorrowerPreferredLangCd;
                txtBorrowerSSN.Text = fcCase.BorrowerSsn;
                txtCallID.Text = fcCase.CallId;
                //txtCaseCompleteInd.Text = fcCase.CaseCompleteInd;
                txtCaseSourceCd.Text = fcCase.CaseSourceCd;
                txtCoBorrowerDisabledInd.Text = fcCase.CoBorrowerDisabledInd;
                txtCoBorrowerDOB.Text = Util.ConvertToString(fcCase.CoBorrowerDob);
                txtCoBorrowerFName.Text = fcCase.CoBorrowerFname;
                //txtCoBorrowerLast4SSN.Text = fcCase.CoBorrowerLast4Ssn;
                txtCoBorrowerLName.Text = fcCase.CoBorrowerLname;
                txtCoBorrowerMName.Text = fcCase.CoBorrowerMname;
                txtCoBorrowerOccupationCd.Text = fcCase.CoBorrowerOccupation;
                txtCoBorrowerSSN.Text = fcCase.CoBorrowerSsn;
                //txtCompletedDt.Text = fcCase.CompletedDt;
                txtContactAddress1.Text = fcCase.ContactAddr1;
                txtContactAddress2.Text = fcCase.ContactAddr2;
                txtContactCity.Text = fcCase.ContactCity;
                txtContactedSrvcrRecentlyInd.Text = fcCase.ContactedSrvcrRecentlyInd;
                txtContactStateCd.Text = fcCase.ContactStateCd;
                txtContactZip.Text = fcCase.ContactZip;
                txtContactZipPlus4.Text = fcCase.ContactZipPlus4;
                txtCounselingDurationCd.Text = fcCase.CounselingDurationCd;
                txtCounselorEmail.Text = fcCase.CounselorEmail;
                txtCounselorExt.Text = fcCase.CounselorExt;
                txtCounselorFirstName.Text = fcCase.CounselorFname;
                txtCounselorLastName.Text = fcCase.CounselorLname;
                txtCounselorPhone.Text = fcCase.CounselorPhone;
                txtDfltReason1stCd.Text = fcCase.DfltReason1stCd;
                txtDfltReason2ndCd.Text = fcCase.DfltReason2ndCd;
                txtDiscussedSolutionWithSrvcrInd.Text = fcCase.DiscussedSolutionWithSrvcrInd;
                //txtDoNotCallInd.Text = fcCase.DoNotCallInd;
                //txtDuplicateInd.Text = fcCase.DuplicateInd;
                txtEmail1.Text = fcCase.Email1;
                txtEmail2.Text = fcCase.Email2;
                //txtFcID.Text = fcCase.FcId;
                txtFcNoticeReceivedInd.Text = fcCase.FcNoticeReceiveInd;
                //txtFcSaleDateSetInd.Text = fcCase.FcSaleDateSetInd;
                txtFollowupNotes.Text = fcCase.FollowupNotes;
                txtForSaleInd.Text = fcCase.ForSaleInd;
                txtFundingConsentInd.Text = fcCase.FundingConsentInd;
                txtGenderCd.Text = fcCase.GenderCd;
                txtHasWorkoutPlanInd.Text = fcCase.HasWorkoutPlanInd;
                txtHispanicInd.Text = fcCase.HispanicInd;
                txtHomeCurMktValue.Text = Util.ConvertToString(fcCase.HomeCurrentMarketValue);
                txtHomePurchasePrice.Text = Util.ConvertToString(fcCase.HomePurchasePrice);
                txtHomePurchaseYear.Text = Util.ConvertToString(fcCase.HomePurchaseYear);
                txtHomeSalePrice.Text = Util.ConvertToString(fcCase.HomeSalePrice);
                txtHouseholdCd.Text = fcCase.HouseholdCd;
                txtHousholdGrossAnnualIncomeAmt.Text = Util.ConvertToString(fcCase.HouseholdGrossAnnualIncomeAmt);
                //txtHpfMediaCandidateInd.Text = fcCase.HpfMediaCandidateInd;
                //txtHpfNetworkCandidateInd.Text = fcCase.HpfNetworkCandidateInd;
                //txtHpfSuccessStoryInd.Text = fcCase.HpfSuccessStoryInd;
                txtHudOutcomeCd.Text = fcCase.HudOutcomeCd;
                txtHudTerminationDt.Text = Util.ConvertToString(fcCase.HudTerminationDt);
                txtHudTerminationReasonCd.Text = fcCase.HudTerminationReasonCd;
                txtIncomeEarnersCd.Text = fcCase.IncomeEarnersCd;
                txtIntakeCreditBureauCd.Text = fcCase.IntakeCreditBureauCd;
                txtIntakeCreditScore.Text = fcCase.IntakeCreditScore;
                txtIntakeDt.Text = Util.ConvertToString(fcCase.IntakeDt);
                txtLoanDfltReasonNotes.Text = fcCase.LoanDfltReasonNotes;
                //txtLoanList.Text = fcCase.LoanList;
                txtMilitaryServiceCd.Text = fcCase.MilitaryServiceCd;
                txtMotherMaidenLName.Text = fcCase.MotherMaidenLname;
                //txtNeverBillReasonCd.Text = fcCase.NeverBillReasonCd;
                //txtNeverPayReasonCd.Text = fcCase.NeverPayReasonCd;
                txtOccupantNum.Text = Util.ConvertToString(fcCase.OccupantNum);
                //txtOptOutNewsletterInd.Text = fcCase.OptOutNewsletterInd;
                //txtOptOutSurveyInd.Text = fcCase.OptOutSurveyInd;
                txtOwnerOccupiedInd.Text = fcCase.OwnerOccupiedInd;
                txtPrimaryContactNo.Text = fcCase.PrimaryContactNo;
                txtPrimaryResidenceInd.Text = fcCase.PrimaryResidenceInd;
                txtPrimResEstMktValue.Text = Util.ConvertToString(fcCase.PrimResEstMktValue);
                txtProgramID.Text = Util.ConvertToString(fcCase.ProgramId);
                txtPropertyAddress1.Text = fcCase.PropAddr1;
                txtPropertyAddress2.Text = fcCase.PropAddr2;
                txtPropertyCity.Text = fcCase.PropCity;
                txtPropertyCd.Text = fcCase.PropertyCd;
                txtPropertyStateCd.Text = fcCase.PropStateCd;
                txtPropertyZip.Text = fcCase.PropZip;
                txtPropertyZipPlus4.Text = fcCase.PropZipPlus4;
                txtRaceCd.Text = fcCase.RaceCd;
                txtRealtyCompany.Text = fcCase.RealtyCompany;
                txtSecondContactNo.Text = fcCase.SecondContactNo;
                txtServicerConsentInd.Text = fcCase.ServicerConsentInd;
                txtSrvcrWorkoutPlanInd.Text = fcCase.SrvcrWorkoutPlanCurrentInd;
                //txtSummarySentDt.Text = fcCase.SummarySentDt;
                txtSummarySentOtherCd.Text = fcCase.SummarySentOtherCd;
                txtSummarySentOtherDt.Text = Util.ConvertToString(fcCase.SummarySentOtherDt);
                txtWorkedWithAnotherAgencyInd.Text = fcCase.WorkedWithAnotherAgencyInd;
                txtFcSaleDate.Text = Util.ConvertToString(fcCase.FcSaleDate);
                txtWorkingUserID.Text = fcCase.ChgLstUserId;
                //txtCreateUserID.Text = fcCase.CreateUserId.ToString();
                //txtChangeLastUserID.Text = fcCase.ChangeLastUserId.ToString();
                txtVipInd.Text = fcCase.VipInd;
                txtVipReason.Text = fcCase.VipReason;
                txtCounseledLanguageCd.Text = fcCase.CounseledLanguageCd;
                txtErcpOutcomeCd.Text = fcCase.ErcpOutcomeCd;
                txtCounselorContactedSrvcrInd.Text = fcCase.CounselorContactedSrvcrInd;
                txtNumberOfUnits.Text = (fcCase.NumberOfUnits.HasValue)?fcCase.NumberOfUnits.Value.ToString():"";
                txtVacantOrCondemedInd.Text = fcCase.VacantOrCondemedInd;
                txtMortgagePmtRatio.Text = (fcCase.MortgagePmtRatio.HasValue)?fcCase.MortgagePmtRatio.Value.ToString():"";
                txtCertififateID.Text = fcCase.CertificateId;
                txtSponsorId.Text = Util.ConvertToString(fcCase.SponsorId);
                txtCampaignId.Text = Util.ConvertToString(fcCase.CampaignId);
                txtReferralNum.Text = fcCase.ReferralClientNum;
                txtCounselorAttemptedSrvcrContactInd.Text = fcCase.CounselorAttemptedSrvcrContactInd;
                txtDependentNum.Text = Util.ConvertToString(fcCase.DependentNum);
                txtPrimaryContactNoTypeCd.Text = fcCase.PrimaryContactNoTypeCd;
                txtSecondContactNoTypeCd.Text = fcCase.SecondContactNoTypeCd;
                txtPreferredContactTime.Text = fcCase.PreferredContactTime;
            }
        }
       

        #region Case Loan
        private void grdvCaseLoanBinding()
        {
            List<CaseLoanDTO_App> caseLoans;
            if (Session[SessionVariables.CASE_LOAN_COLLECTION] != null)
            {
                caseLoans = (List<CaseLoanDTO_App>)Session[SessionVariables.CASE_LOAN_COLLECTION];
                grdvCaseLoan.DataSource = caseLoans;
                grdvCaseLoan.DataBind();
            }
            else
            {
                caseLoans = new List<CaseLoanDTO_App>();
                caseLoans.Add(new CaseLoanDTO_App());
                grdvCaseLoan.DataSource = caseLoans;
                grdvCaseLoan.DataBind();

                int TotalColumns = grdvCaseLoan.Rows[0].Cells.Count;
                grdvCaseLoan.Rows[0].Cells.Clear();
                grdvCaseLoan.Rows[0].Cells.Add(new TableCell());
                grdvCaseLoan.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grdvCaseLoan.Rows[0].Cells[0].Text = "No Records Found";
            }    
        }

        protected void grdvCaseLoan_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvCaseLoan.EditIndex = e.NewEditIndex;
            
            //grdvCaseLoanBinding();
            RefreshAllGrids();
             
        }

        protected void grdvCaseLoan_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            grdvCaseLoan.EditIndex = -1;
            //grdvCaseLoanBinding();
            RefreshAllGrids();
        }

        protected void grdvCaseLoan_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            CaseLoanDTO_App caseLoan = RowToCaseLoanDTO_App(grdvCaseLoan.Rows[e.RowIndex]);
            if (Session[SessionVariables.CASE_LOAN_COLLECTION] != null)
            {
                List<CaseLoanDTO_App> caseLoans = (List<CaseLoanDTO_App>)Session[SessionVariables.CASE_LOAN_COLLECTION];
                int index = caseLoans.FindIndex(item => item.CaseLoanId == caseLoan.CaseLoanId);

                if (index < 0)
                {
                    //can not update List
                }
                else
                {
                    caseLoans[index] = caseLoan;
                    Session[SessionVariables.CASE_LOAN_COLLECTION] = caseLoans;
                }


                grdvCaseLoan.EditIndex = -1;
                //grdvCaseLoanBinding();
                RefreshAllGrids();
            }
            else
            {
                //can not update List
            }
        }

        protected void grdvCaseLoan_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int? caseLoanId = Util.ConvertToInt(((Label)grdvCaseLoan.Rows[e.RowIndex].FindControl("lblCaseLoanId")).Text);
            if (Session[SessionVariables.CASE_LOAN_COLLECTION] != null)
            {
                List<CaseLoanDTO_App> caseLoans = (List<CaseLoanDTO_App>)Session[SessionVariables.CASE_LOAN_COLLECTION];
                int index = caseLoans.FindIndex(item => item.CaseLoanId == caseLoanId);

                if (index < 0)
                {
                    //can not Delete item
                }
                else
                {
                    caseLoans.RemoveAt(index);
                    if (caseLoans.Count > 0)
                        Session[SessionVariables.CASE_LOAN_COLLECTION] = caseLoans;
                    else
                        Session[SessionVariables.CASE_LOAN_COLLECTION] = null;
                }
                    
                grdvCaseLoan.EditIndex = -1;
                RefreshAllGrids();
                //grdvCaseLoanBinding();
            }
            else
            {
                //can not Delete item
            }
        }

        protected void grdvCaseLoan_RowCreated(object sender, GridViewRowEventArgs e)
        {           
        }

        protected void grdvCaseLoan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                CaseLoanDTO_App caseLoan = RowToCaseLoanDTO_App(grdvCaseLoan.FooterRow);
    
                
                List<CaseLoanDTO_App> caseLoans = new List<CaseLoanDTO_App>();
                if (Session[SessionVariables.CASE_LOAN_COLLECTION] != null)
                {
                    caseLoans = (List<CaseLoanDTO_App>)Session[SessionVariables.CASE_LOAN_COLLECTION];
                    int? caseLoanId = caseLoans.Max(item => item.CaseLoanId);
                    caseLoan.CaseLoanId = caseLoanId + 1;
                }
                else
                {
                    caseLoan.CaseLoanId = 1;
                }

                caseLoans.Add(caseLoan);
                Session[SessionVariables.CASE_LOAN_COLLECTION] = caseLoans;
                //grdvCaseLoanBinding();
                RefreshAllGrids();
            } 

        }
       

        private CaseLoanDTO_App RowToCaseLoanDTO_App(GridViewRow row)
        {
            CaseLoanDTO_App caseLoan = new CaseLoanDTO_App();

            #region retrieve controls from gridview
            TextBox txtAccNum = (TextBox)row.FindControl("txtAccNum");
            //TextBox txtArmLoanInd = (TextBox)row.FindControl("txtArmLoanInd");
            TextBox txtArmResetInd = (TextBox)row.FindControl("txtArmResetInd");
            //TextBox txtCaseLoanId = (TextBox)row.FindControl("txtCaseLoanId");
            TextBox txtCurrentLoanBalanceAmt = (TextBox)row.FindControl("txtCurrentLoanBalanceAmt");
            TextBox txtCurrentServiceNameTBD = (TextBox)row.FindControl("txtCurrentServiceNameTBD");
            TextBox txtFDICNCUANum = (TextBox)row.FindControl("txtFDICNCUANum");
            //TextBox txtFreddieLoanNum = (TextBox)row.FindControl("txtFreddieLoanNum");
            //TextBox txtFcId = (TextBox)row.FindControl("txtFcId");
            TextBox txtInterestRate = (TextBox)row.FindControl("txtInterestRate");
            TextBox txtLoan1st2nd = (TextBox)row.FindControl("txtLoan1st2nd");
            TextBox txtLoanDelinqCd = (TextBox)row.FindControl("txtLoanDelinqCd");
            TextBox txtMorgageTypeCode = (TextBox)row.FindControl("txtMorgageTypeCode");
            TextBox txtOrigLoanNum = (TextBox)row.FindControl("txtOrigLoanNum");
            TextBox txtOrigLenderName = (TextBox)row.FindControl("txtOrigLenderName");
            TextBox txtOrigLoanAmt = (TextBox)row.FindControl("txtOrigLoanAmt");
            TextBox txtOrigMorgageNum = (TextBox)row.FindControl("txtOrigMorgageNum");
            TextBox txtOrigMotgateConName = (TextBox)row.FindControl("txtOrigMotgateConName");
            TextBox txtOtherServiceName = (TextBox)row.FindControl("txtOtherServiceName");
            TextBox txtServicerId = (TextBox)row.FindControl("txtServicerId");
            TextBox txtTermLengthCd = (TextBox)row.FindControl("txtTermLengthCd");
            TextBox txtMortgageProgramCd = (TextBox)row.FindControl("txtMortgageProgramCd");
            //TextBox txtInvestorName = (TextBox)row.FindControl("txtInvestorName");
            //TextBox txtInvestorNum = (TextBox)row.FindControl("txtInvestorNum");
            Label lblCaseLoanId = (Label)row.FindControl("lblCaseLoanId");

            TextBox txtArmRateAdjustDt = (TextBox)row.FindControl("txtArmRateAdjustDt");
            TextBox txtArmLockDuration = (TextBox)row.FindControl("txtArmLockDuration");
            TextBox txtLoanLookupCd = (TextBox)row.FindControl("txtLoanLookupCd");
            TextBox txtThirtyDaysLatePastYrInd = (TextBox)row.FindControl("txtThirtyDaysLatePastYrInd");
            TextBox txtPmtMissLessOneYrLoanInd = (TextBox)row.FindControl("txtPmtMissLessOneYrLoanInd");
            TextBox txtSufficientIncomeInd = (TextBox)row.FindControl("txtSufficientIncomeInd");
            TextBox txtLongTermAffordInd = (TextBox)row.FindControl("txtLongTermAffordInd");
            TextBox txtHarpEligibleInd = (TextBox)row.FindControl("txtHarpEligibleInd");
            TextBox txtOrigPriorTo2009Ind = (TextBox)row.FindControl("txtOrigPriorTo2009Ind");
            TextBox txtPriorHampInd = (TextBox)row.FindControl("txtPriorHampInd");
            TextBox txtPrinBalWithinLimitInd = (TextBox)row.FindControl("txtPrinBalWithinLimitInd");
            TextBox txtHampEligibleInd = (TextBox)row.FindControl("txtHampEligibleInd");
            TextBox txtLossMitStatusCd = (TextBox)row.FindControl("txtLossMitStatusCd");

            #endregion            
            caseLoan.AcctNum = txtAccNum.Text.Trim();
            caseLoan.ArmResetInd = txtArmResetInd.Text.Trim();
            caseLoan.CaseLoanId = Util.ConvertToInt(lblCaseLoanId.Text.Trim());
            caseLoan.CurrentLoanBalanceAmt = Util.ConvertToDouble(txtCurrentLoanBalanceAmt.Text.Trim());
            caseLoan.CurrentServicerFdicNcuaNum = txtFDICNCUANum.Text.Trim();
            caseLoan.InterestRate = Util.ConvertToDouble(txtInterestRate.Text.Trim());
            caseLoan.Loan1st2nd = txtLoan1st2nd.Text.Trim();
            caseLoan.LoanDelinqStatusCd = txtLoanDelinqCd.Text.Trim();
            caseLoan.MortgageTypeCd = txtMorgageTypeCode.Text.Trim();
            caseLoan.OrginalLoanNum = txtOrigLoanNum.Text.Trim();
            caseLoan.OriginatingLenderName = txtOrigLenderName.Text.Trim();
            caseLoan.OrigLoanAmt = Util.ConvertToDouble(txtOrigLoanAmt.Text.Trim());
            caseLoan.OrigMortgageCoFdicNcusNum = txtOrigMorgageNum.Text.Trim();
            caseLoan.OrigMortgageCoName = txtOrigMotgateConName.Text.Trim();
            caseLoan.OtherServicerName = txtOtherServiceName.Text.Trim();
            caseLoan.ServicerId = Util.ConvertToInt(txtServicerId.Text.Trim());
            caseLoan.TermLengthCd = txtTermLengthCd.Text.Trim();
            caseLoan.MortgageProgramCd = txtMortgageProgramCd.Text.Trim();
            //comment because of hidden fields
            //caseLoan.InvestorName = txtInvestorName.Text.Trim();
            //caseLoan.InvestorNum = txtInvestorNum.Text.Trim();

            caseLoan.ArmRateAdjustDt = Util.ConvertToDateTime(txtArmRateAdjustDt.Text);
            caseLoan.ArmLockDuration = Util.ConvertToInt(txtArmLockDuration.Text);
            caseLoan.LoanLookupCd = txtLoanLookupCd.Text;
            caseLoan.ThirtyDaysLatePastYrInd = txtThirtyDaysLatePastYrInd.Text;
            caseLoan.PmtMissLessOneYrLoanInd = txtPmtMissLessOneYrLoanInd.Text;
            caseLoan.SufficientIncomeInd = txtSufficientIncomeInd.Text;
            caseLoan.LongTermAffordInd = txtLongTermAffordInd.Text;
            caseLoan.HarpEligibleInd = txtHarpEligibleInd.Text;
            caseLoan.OrigPriorTo2009Ind = txtOrigPriorTo2009Ind.Text;
            caseLoan.PriorHampInd = txtPriorHampInd.Text;
            caseLoan.PrinBalWithinLimitInd = txtPrinBalWithinLimitInd.Text;
            caseLoan.HampEligibleInd = txtHampEligibleInd.Text;
            caseLoan.LossMitStatusCd = txtLossMitStatusCd.Text;

            return caseLoan;

        }
        #endregion

        #region Budget Item
        private void grdvBudgetItemBinding()
        {
            List<BudgetItemDTO_App> budgetItems;
            if (Session[SessionVariables.BUDGET_ITEM_COLLECTION] != null)
            {
                budgetItems = (List<BudgetItemDTO_App>)Session[SessionVariables.BUDGET_ITEM_COLLECTION];
                grdvBudgetItem.DataSource = budgetItems;
                grdvBudgetItem.DataBind();
            }
            else
            {
                budgetItems = new List<BudgetItemDTO_App>();
                budgetItems.Add(new BudgetItemDTO_App());
                grdvBudgetItem.DataSource = budgetItems;
                grdvBudgetItem.DataBind();

                int TotalColumns = grdvBudgetItem.Rows[0].Cells.Count;
                grdvBudgetItem.Rows[0].Cells.Clear();
                grdvBudgetItem.Rows[0].Cells.Add(new TableCell());
                grdvBudgetItem.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grdvBudgetItem.Rows[0].Cells[0].Text = "No Records Found";
            }    
        }
        private BudgetItemDTO_App RowToBudgetItemDTO(GridViewRow row)
        {
            TextBox txtBudgetItemAmt = (TextBox)row.FindControl("txtBudgetItemAmt");
            TextBox txtBudgetNote = (TextBox)row.FindControl("txtBudgetNote");
            TextBox txtBudgetSubcategoryId = (TextBox)row.FindControl("txtBudgetSubcategoryId");
            //TextBox txtBudgetItemId = (TextBox)row.FindControl("txtBudgetItemId");
            Label lblBudgetItemId = (Label)row.FindControl("lblBudgetItemId");
            //TextBox txtBudgetItemSetId = (TextBox)row.FindControl("txtBudgetItemSetId");

            BudgetItemDTO_App budgetItem = new BudgetItemDTO_App();
            budgetItem.BudgetItemAmt = Util.ConvertToDouble(txtBudgetItemAmt.Text.Trim());
            budgetItem.BudgetNote = txtBudgetNote.Text.Trim();
            budgetItem.BudgetSubcategoryId = Util.ConvertToInt(txtBudgetSubcategoryId.Text.Trim());

            //budgetItem.CreateUserId = txtCreateUserID.Text.Trim();
            //budgetItem.ChangeLastUserId = txtChangeLastUserID.Text.Trim();
            budgetItem.BudgetItemId = Util.ConvertToInt(lblBudgetItemId.Text.Trim());
            //budgetItem.BudgetSetId = Util.ConvertToInt(txtBudgetItemSetId.Text.Trim());
            return budgetItem;
        }
        
        protected void grdvBudgetItem_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            grdvBudgetItem.EditIndex = -1;
            //grdvBudgetItemBinding();
            RefreshAllGrids();

        }

        protected void grdvBudgetItemRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                BudgetItemDTO_App budgetItem = RowToBudgetItemDTO(grdvBudgetItem.FooterRow);

                List<BudgetItemDTO_App> budgetItems = new List<BudgetItemDTO_App>();
                if (Session[SessionVariables.BUDGET_ITEM_COLLECTION] != null)
                {
                    budgetItems = (List<BudgetItemDTO_App>)Session[SessionVariables.BUDGET_ITEM_COLLECTION];
                    int? budgetItemId = budgetItems.Max(item => item.BudgetItemId);
                    budgetItem.BudgetItemId = budgetItemId + 1;
                }
                else
                {
                    budgetItem.BudgetItemId = 1;
                }

                budgetItems.Add(budgetItem);
                Session[SessionVariables.BUDGET_ITEM_COLLECTION] = budgetItems;
                //grdvBudgetItemBinding();
                RefreshAllGrids();
            } 

        }

        protected void grdvBudgetItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int? budgetItemId = Util.ConvertToInt(((Label)grdvBudgetItem.Rows[e.RowIndex].FindControl("lblBudgetItemId")).Text);
            if (Session[SessionVariables.BUDGET_ITEM_COLLECTION] != null)
            {
                List<BudgetItemDTO_App> budgetItems = (List<BudgetItemDTO_App>)Session[SessionVariables.BUDGET_ITEM_COLLECTION];
                int index = budgetItems.FindIndex(item => item.BudgetItemId == budgetItemId);

                if (index < 0)
                {
                    //can not Delete item
                }
                else
                {
                    budgetItems.RemoveAt(index);
                    if (budgetItems.Count > 0)
                        Session[SessionVariables.BUDGET_ITEM_COLLECTION] = budgetItems;
                    else
                        Session[SessionVariables.BUDGET_ITEM_COLLECTION] = null;
                }

                grdvBudgetItem.EditIndex = -1;
                RefreshAllGrids();
                //grdvBudgetItemBinding();
            }
            else
            {
                //can not Delete item
            }
        }

        protected void grdvBudgetItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvBudgetItem.EditIndex = e.NewEditIndex;
            //grdvBudgetItemBinding();
            RefreshAllGrids();

        }

        protected void grdvBudgetItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            BudgetItemDTO_App budgetItem = RowToBudgetItemDTO(grdvBudgetItem.Rows[e.RowIndex]);
            if (Session[SessionVariables.BUDGET_ITEM_COLLECTION] != null)
            {
                List<BudgetItemDTO_App> budgetItems = (List<BudgetItemDTO_App>)Session[SessionVariables.BUDGET_ITEM_COLLECTION];
                int index = budgetItems.FindIndex(item => item.BudgetItemId == budgetItem.BudgetItemId);

                if (index < 0)
                {
                    //can not update List
                }
                else
                {
                    budgetItems[index] = budgetItem;
                    Session[SessionVariables.BUDGET_ITEM_COLLECTION] = budgetItems;
                }


                grdvBudgetItem.EditIndex = -1;
                //grdvBudgetItemBinding();
                RefreshAllGrids();
            }
            else
            {
                //can not update List
            }
        }
        #endregion


        #region PROPOSED BUDGET ITEMS
        private void grdvProposedBudgetItemBinding()
        {
            List<BudgetItemDTO_App> budgetItems;
            if (Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION] != null)
            {
                budgetItems = (List<BudgetItemDTO_App>)Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION];
                grdvProposedBudgetItem.DataSource = budgetItems;
                grdvProposedBudgetItem.DataBind();
            }
            else
            {
                budgetItems = new List<BudgetItemDTO_App>();
                budgetItems.Add(new BudgetItemDTO_App());
                grdvProposedBudgetItem.DataSource = budgetItems;
                grdvProposedBudgetItem.DataBind();

                int TotalColumns = grdvProposedBudgetItem.Rows[0].Cells.Count;
                grdvProposedBudgetItem.Rows[0].Cells.Clear();
                grdvProposedBudgetItem.Rows[0].Cells.Add(new TableCell());
                grdvProposedBudgetItem.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grdvProposedBudgetItem.Rows[0].Cells[0].Text = "No Records Found";
            }
        }
        #endregion
        #region Budget Asset
        private void grdvBudgetAssetBinding()
        {

            List<BudgetAssetDTO_App> budgetAssets;
            if (Session[SessionVariables.BUDGET_ASSET_COLLECTION] != null)
            {
                budgetAssets = (List<BudgetAssetDTO_App>)Session[SessionVariables.BUDGET_ASSET_COLLECTION];
                grdvBudgetAsset.DataSource = budgetAssets;
                grdvBudgetAsset.DataBind();
            }
            else
            {
                budgetAssets = new List<BudgetAssetDTO_App>();
                budgetAssets.Add(new BudgetAssetDTO_App());
                grdvBudgetAsset.DataSource = budgetAssets;
                grdvBudgetAsset.DataBind();

                //grdvBudgetAsset.Rows[0].Visible = false;
                int TotalColumns = grdvBudgetAsset.Rows[0].Cells.Count;
                grdvBudgetAsset.Rows[0].Cells.Clear();
                grdvBudgetAsset.Rows[0].Cells.Add(new TableCell());
                grdvBudgetAsset.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grdvBudgetAsset.Rows[0].Cells[0].Text = "No Records Found";
            }    
        }

        private BudgetAssetDTO_App RowToBudgetAssetDTO(GridViewRow row)
        {
            TextBox txtAssetName = (TextBox)row.FindControl("txtAssetName");
            TextBox txtAssetValue = (TextBox)row.FindControl("txtAssetValue");
            //TextBox txtBudgetAssetID = (TextBox)row.FindControl("txtBudgetAssetID");
            //TextBox txtBudgetID = (TextBox)row.FindControl("txtBudgetID");
            Label lblBudgetAssetID = (Label)row.FindControl("lblBudgetAssetID");

            BudgetAssetDTO_App budgetAsset = new BudgetAssetDTO_App();
            budgetAsset.AssetName = txtAssetName.Text.Trim();
            budgetAsset.AssetValue = Util.ConvertToDouble(txtAssetValue.Text.Trim());
            //budgetAsset.CreateUserId = txtCreateUserID.Text.Trim();
            //budgetAsset.ChangeLastUserId = txtChangeLastUserID.Text.Trim();
            budgetAsset.BudgetAssetId = Util.ConvertToInt(lblBudgetAssetID.Text.Trim());
            //budgetAsset.BudgetSetId = Util.ConvertToInt(txtBudgetSetID.Text.Trim());
            return budgetAsset;
        }
        protected void grdvBudgetAsset_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            grdvBudgetAsset.EditIndex = -1;
            //grdvBudgetAssetBinding();
            RefreshAllGrids();

        }

        protected void grdvBudgetAssetRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                BudgetAssetDTO_App budgetAsset = RowToBudgetAssetDTO(grdvBudgetAsset.FooterRow);

                List<BudgetAssetDTO_App> budgetAssets = new List<BudgetAssetDTO_App>();
                if (Session[SessionVariables.BUDGET_ASSET_COLLECTION] != null)
                {
                    budgetAssets = (List<BudgetAssetDTO_App>)Session[SessionVariables.BUDGET_ASSET_COLLECTION];
                    int? budgetAssetId = budgetAssets.Max(item => item.BudgetAssetId);
                    budgetAsset.BudgetAssetId = budgetAssetId + 1;
                }
                else
                {
                    budgetAsset.BudgetAssetId = 1;
                }

                budgetAssets.Add(budgetAsset);
                Session[SessionVariables.BUDGET_ASSET_COLLECTION] = budgetAssets;
                //grdvBudgetAssetBinding();
                RefreshAllGrids();
            } 
        }

        protected void grdvBudgetAsset_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int? budgetAssetId = Util.ConvertToInt(((Label)grdvBudgetAsset.Rows[e.RowIndex].FindControl("lblBudgetAssetId")).Text);
            if (Session[SessionVariables.BUDGET_ASSET_COLLECTION] != null)
            {
                List<BudgetAssetDTO_App> budgetAssets = (List<BudgetAssetDTO_App>)Session[SessionVariables.BUDGET_ASSET_COLLECTION];
                int index = budgetAssets.FindIndex(item => item.BudgetAssetId == budgetAssetId);

                if (index < 0)
                {
                    //can not Delete item
                }
                else
                {
                    budgetAssets.RemoveAt(index);
                    if (budgetAssets.Count > 0)
                        Session[SessionVariables.BUDGET_ASSET_COLLECTION] = budgetAssets;
                    else
                        Session[SessionVariables.BUDGET_ASSET_COLLECTION] = null;
                }

                grdvBudgetAsset.EditIndex = -1;
                RefreshAllGrids();
                //grdvBudgetAssetBinding();
            }
            else
            {
                //can not Delete item
            }
        }

        protected void grdvBudgetAsset_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvBudgetAsset.EditIndex = e.NewEditIndex;
            //grdvBudgetAssetBinding();
            RefreshAllGrids();
        }

        protected void grdvBudgetAsset_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            BudgetAssetDTO_App budgetAsset = RowToBudgetAssetDTO(grdvBudgetAsset.Rows[e.RowIndex]);
            if (Session[SessionVariables.BUDGET_ASSET_COLLECTION] != null)
            {
                List<BudgetAssetDTO_App> budgetAssets = (List<BudgetAssetDTO_App>)Session[SessionVariables.BUDGET_ASSET_COLLECTION];
                int index = budgetAssets.FindIndex(item => item.BudgetAssetId == budgetAsset.BudgetAssetId);

                if (index < 0)
                {
                    //can not update List
                }
                else
                {
                    budgetAssets[index] = budgetAsset;
                    Session[SessionVariables.BUDGET_ASSET_COLLECTION] = budgetAssets;
                }


                grdvBudgetAsset.EditIndex = -1;
                //grdvBudgetAssetBinding();
                RefreshAllGrids();
            }
            else
            {
                //can not update List
            }
        }
        #endregion

        //#region Activity Log

        //private void grdvActivityLogBinding()
        //{
        //    List<ActivityLogDTO> activityLogs;
        //    if (Session[SessionVariables.ACTIVITY_LOG_COLLECTION] != null)
        //    {
        //        activityLogs = (List<ActivityLogDTO>)Session[SessionVariables.ACTIVITY_LOG_COLLECTION];
        //        grdvActivityLog.DataSource = activityLogs;
        //        grdvActivityLog.DataBind();
        //    }
        //    else
        //    {
        //        activityLogs = new List<ActivityLogDTO>();
        //        activityLogs.Add(new ActivityLogDTO());
        //        grdvActivityLog.DataSource = activityLogs;
        //        grdvActivityLog.DataBind();

        //        int TotalColumns = grdvActivityLog.Rows[0].Cells.Count;
        //        grdvActivityLog.Rows[0].Cells.Clear();
        //        grdvActivityLog.Rows[0].Cells.Add(new TableCell());
        //        grdvActivityLog.Rows[0].Cells[0].ColumnSpan = TotalColumns;
        //        grdvActivityLog.Rows[0].Cells[0].Text = "No Records Found";
        //    }           
        //}
        //private ActivityLogDTO RowToActivityLogDTO(GridViewRow row)
        //{
        //    TextBox txtFcId = (TextBox)row.FindControl("txtFcId");
        //    TextBox txtActivityCode = (TextBox)row.FindControl("txtActivityCode");
        //    TextBox txtActivityDt = (TextBox)row.FindControl("txtActivityDt");
        //    TextBox txtActivityLogId = (TextBox)row.FindControl("txtActivityLogId");
        //    TextBox txtActivityNote = (TextBox)row.FindControl("txtActivityNote");

        //    ActivityLogDTO activityLog = new ActivityLogDTO();
        //    activityLog.FcId = Util.ConvertToInt(txtFcId.Text.Trim());
        //    activityLog.ActivityCd = txtActivityCode.Text.Trim();
        //    activityLog.ActivityDt = Util.ConvertToDateTime(txtActivityDt.Text.Trim());
        //    activityLog.ActivityLogId = Util.ConvertToInt(txtActivityLogId.Text.Trim());
        //    activityLog.ActivityNote = txtActivityNote.Text.Trim();
        //    return activityLog;
        //}

        //protected void grdvActivityLog_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        //{
        //    grdvActivityLog.EditIndex = -1;
        //    grdvActivityLogBinding();
        //}

        //protected void grdvActivityLogRowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName.Equals("AddNew"))
        //    {
        //        ActivityLogDTO activityLog = RowToActivityLogDTO(grdvActivityLog.FooterRow);

        //        List<ActivityLogDTO> activityLogs = new List<ActivityLogDTO>();
        //        if (Session[SessionVariables.ACTIVITY_LOG_COLLECTION] != null)
        //        {
        //            activityLogs = (List<ActivityLogDTO>)Session[SessionVariables.ACTIVITY_LOG_COLLECTION];
        //            int activityLogId = activityLogs.Max(item => item.ActivityLogId);
        //            activityLog.ActivityLogId = activityLogId + 1;
        //        }
        //        else
        //        {
        //            activityLog.ActivityLogId = 1;
        //        }

        //        activityLogs.Add(activityLog);
        //        Session[SessionVariables.ACTIVITY_LOG_COLLECTION] = activityLogs;
        //        grdvActivityLogBinding();
        //    } 
        //}

        //protected void grdvActivityLog_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    int activityLogId = Util.ConvertToInt(((Label)grdvActivityLog.Rows[e.RowIndex].FindControl("lblActivityLogId")).Text);
        //    if (Session[SessionVariables.ACTIVITY_LOG_COLLECTION] != null)
        //    {
        //        List<ActivityLogDTO> activityLogs = (List<ActivityLogDTO>)Session[SessionVariables.ACTIVITY_LOG_COLLECTION];
        //        int index = activityLogs.FindIndex(item => item.ActivityLogId == activityLogId);

        //        if (index < 0)
        //        {
        //            //can not Delete item
        //        }
        //        else
        //        {
        //            activityLogs.RemoveAt(index);
        //            Session[SessionVariables.ACTIVITY_LOG_COLLECTION] = activityLogs;
        //        }

        //        grdvActivityLog.EditIndex = -1;
        //        grdvActivityLogBinding();
        //    }
        //    else
        //    {
        //        //can not Delete item
        //    }
        //}

        //protected void grdvActivityLog_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    grdvActivityLog.EditIndex = e.NewEditIndex;
        //    grdvActivityLogBinding();
        //}

        //protected void grdvActivityLog_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    ActivityLogDTO activityLog = RowToActivityLogDTO(grdvActivityLog.Rows[e.RowIndex]);
        //    if (Session[SessionVariables.ACTIVITY_LOG_COLLECTION] != null)
        //    {
        //        List<ActivityLogDTO> activityLogs = (List<ActivityLogDTO>)Session[SessionVariables.ACTIVITY_LOG_COLLECTION];
        //        int index = activityLogs.FindIndex(item => item.ActivityLogId == activityLog.ActivityLogId);

        //        if (index < 0)
        //        {
        //            //can not update List
        //        }
        //        else
        //        {
        //            activityLogs[index] = activityLog;
        //            Session[SessionVariables.ACTIVITY_LOG_COLLECTION] = activityLogs;
        //        }


        //        grdvActivityLog.EditIndex = -1;
        //        grdvActivityLogBinding();
        //    }
        //    else
        //    {
        //        //can not update List
        //    }
        //}
        //#endregion

        #region Outcome 
        private void grdvOutcomeItemBinding()
        {
            List<OutcomeItemDTO_App> outcomeItems;
            if (Session[SessionVariables.OUTCOME_ITEM_COLLECTION] != null)
            {
                outcomeItems = (List<OutcomeItemDTO_App>)Session[SessionVariables.OUTCOME_ITEM_COLLECTION];
                grdvOutcomeItem.DataSource = outcomeItems;
                grdvOutcomeItem.DataBind();
            }
            else                    
            {   
                outcomeItems = new List<OutcomeItemDTO_App>();
                outcomeItems.Add(new OutcomeItemDTO_App());
                grdvOutcomeItem.DataSource = outcomeItems;
                grdvOutcomeItem.DataBind();

                int TotalColumns = grdvOutcomeItem.Rows[0].Cells.Count;
                grdvOutcomeItem.Rows[0].Cells.Clear();
                grdvOutcomeItem.Rows[0].Cells.Add(new TableCell());
                grdvOutcomeItem.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grdvOutcomeItem.Rows[0].Cells[0].Text = "No Records Found";
            }
        }

        private OutcomeItemDTO_App RowToOutcomeItemDTO(GridViewRow row)
        {
            TextBox txtExtRefOtherName = (TextBox)row.FindControl("txtExtRefOtherName");
            //TextBox txtFcId = (TextBox)row.FindControl("txtFcId");
            TextBox txtNonprofitreferralKeyNum = (TextBox)row.FindControl("txtNonprofitreferralKeyNum");
            //TextBox txtOutcomeDeletedDt = (TextBox)row.FindControl("txtOutcomeDeletedDt");
            //TextBox txtOutcomeDt = (TextBox)row.FindControl("txtOutcomeDt");
            //TextBox txtOutcomeItemId = (TextBox)row.FindControl("txtOutcomeItemId");
            //TextBox txtOutcomeSetId = (TextBox)row.FindControl("txtOutcomeSetId");
            TextBox txtOutcomeTypeId = (TextBox)row.FindControl("txtOutcomeTypeId");
            Label lblOutcomeItemId = (Label)row.FindControl("lblOutcomeItemId");

            OutcomeItemDTO_App outcomeItem = new OutcomeItemDTO_App();
            outcomeItem.ExtRefOtherName = txtExtRefOtherName.Text.Trim();
            //outcomeItem.FcId = Util.ConvertToInt(txtFcId.Text.Trim());
            outcomeItem.NonprofitreferralKeyNum = txtNonprofitreferralKeyNum.Text.Trim();

            //outcomeItem.CreateUserId = txtCreateUserID.Text.Trim();
            //outcomeItem.ChangeLastUserId = txtChangeLastUserID.Text.Trim();
            //outcomeItem.OutcomeDeletedDt = Util.ConvertToDateTime(txtOutcomeDeletedDt.Text.Trim());
            //outcomeItem.OutcomeDt = Util.ConvertToDateTime(txtOutcomeDt.Text.Trim());
            
            //outcomeItem.OutcomeSetId = Util.ConvertToInt(txtOutcomeSetId.Text.Trim());
            outcomeItem.OutcomeTypeId = Util.ConvertToInt(txtOutcomeTypeId.Text.Trim());
            outcomeItem.OutcomeItemId = Util.ConvertToInt(lblOutcomeItemId.Text.Trim());
            
            return outcomeItem;
        }
       
        protected void grdvOutcomeItem_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            grdvOutcomeItem.EditIndex = -1;
            //grdvOutcomeItemBinding();
            RefreshAllGrids();
        }

        protected void grdvOutcomeItemRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                OutcomeItemDTO_App outcomeItem = RowToOutcomeItemDTO(grdvOutcomeItem.FooterRow);

                List<OutcomeItemDTO_App> outcomeItems = new List<OutcomeItemDTO_App>();
                if (Session[SessionVariables.OUTCOME_ITEM_COLLECTION] != null)
                {
                    outcomeItems = (List<OutcomeItemDTO_App>)Session[SessionVariables.OUTCOME_ITEM_COLLECTION];
                    int? outcomeItemId = outcomeItems.Max(item => item.OutcomeItemId);
                    outcomeItem.OutcomeItemId = outcomeItemId + 1;
                }
                else
                {
                    outcomeItem.OutcomeItemId = 1;
                }

                outcomeItems.Add(outcomeItem);
                Session[SessionVariables.OUTCOME_ITEM_COLLECTION] = outcomeItems;
                //grdvOutcomeItemBinding();
                RefreshAllGrids();
            } 
        }

        protected void grdvOutcomeItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int? outcomeItemId = Util.ConvertToInt(((Label)grdvOutcomeItem.Rows[e.RowIndex].FindControl("lblOutcomeItemId")).Text);
            if (Session[SessionVariables.OUTCOME_ITEM_COLLECTION] != null)
            {
                List<OutcomeItemDTO_App> outcomeItems = (List<OutcomeItemDTO_App>)Session[SessionVariables.OUTCOME_ITEM_COLLECTION];
                int index = outcomeItems.FindIndex(item => item.OutcomeItemId == outcomeItemId);

                if (index < 0)
                {
                    //can not Delete item
                }
                else
                {
                    outcomeItems.RemoveAt(index);
                    if (outcomeItems.Count != 0)
                        Session[SessionVariables.OUTCOME_ITEM_COLLECTION] = outcomeItems;
                    else
                        Session[SessionVariables.OUTCOME_ITEM_COLLECTION] = null;
                }

                grdvOutcomeItem.EditIndex = -1;
                RefreshAllGrids();
                //grdvOutcomeItemBinding();
            }
            else
            {
                //can not Delete item
            }
        }

        protected void grdvOutcomeItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvOutcomeItem.EditIndex = e.NewEditIndex;
            //grdvOutcomeItemBinding();
            RefreshAllGrids();
        }

        protected void grdvOutcomeItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            OutcomeItemDTO_App outcomeItem = RowToOutcomeItemDTO(grdvOutcomeItem.Rows[e.RowIndex]);
            if (Session[SessionVariables.OUTCOME_ITEM_COLLECTION] != null)
            {
                List<OutcomeItemDTO_App> outcomeItems = (List<OutcomeItemDTO_App>)Session[SessionVariables.OUTCOME_ITEM_COLLECTION];
                int index = outcomeItems.FindIndex(item => item.OutcomeItemId == outcomeItem.OutcomeItemId);

                if (index < 0)
                {
                    //can not update List
                }
                else
                {
                    outcomeItems[index] = outcomeItem;
                    Session[SessionVariables.OUTCOME_ITEM_COLLECTION] = outcomeItems;
                }


                grdvOutcomeItem.EditIndex = -1;
                //grdvOutcomeItemBinding();
                RefreshAllGrids();
            }
            else
            {
                //can not update List
            }
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ForeclosureCaseSaveRequest request = CreateForeclosureCaseSaveRequest();
            ForeclosureCaseSaveResponse response;

            AuthenticationInfo ai = new AuthenticationInfo();
            ai.UserName = txtUsername.Text.Trim();
            ai.Password = txtPassword.Text.Trim();
            AgencyWebService proxy = new AgencyWebService();
            proxy.AuthenticationInfoValue = ai;

            response = proxy.SaveForeclosureCase(request);
            if (response.Status != ResponseStatus.Success)
            {
                if (response.Status == ResponseStatus.Warning)
                {
                    lblMessage.Text = "Congratulation - FcId is " + response.FcId;
                    if (response.CompletedDt.HasValue)
                        lblMessage.Text += "<br> Completed Date: " + response.CompletedDt.Value.ToString();
                }
                else
                    lblMessage.Text = "Error Message: ";                    
                grdvMessages.Visible = true;
                grdvMessages.DataSource = response.Messages;
                grdvMessages.DataBind();
            }
            else
            {                
                    grdvMessages.Visible = false;
                    lblMessage.Text = "Congratulation - FcId is " + response.FcId;
                    if (response.CompletedDt.HasValue)
                        lblMessage.Text += "<br> Completed Date: " + response.CompletedDt.Value.ToString();
            }
        }

        private ForeclosureCaseSaveRequest CreateForeclosureCaseSaveRequest()
        {
            ForeclosureCaseSaveRequest request = new ForeclosureCaseSaveRequest();
            ForeclosureCaseSetDTO fcCaseSet = new ForeclosureCaseSetDTO();
            //fcCaseSet.WorkingUserID = txtWorkingUserID.Text.Trim();
            
            fcCaseSet.ForeclosureCase = FormToForeclosureCase();
            fcCaseSet.ForeclosureCase.ChgLstUserId = txtWorkingUserID.Text.Trim();
            //fcCaseSet.ActivityLog = ((List<ActivityLogDTO>)Session[SessionVariables.ACTIVITY_LOG_COLLECTION]).ToArray();
            if (Session[SessionVariables.BUDGET_ASSET_COLLECTION] != null)
            {
                var list = new List<BudgetAssetDTO>();
                var list_app = ((List<BudgetAssetDTO_App>)Session[SessionVariables.BUDGET_ASSET_COLLECTION]);
                foreach (var item in list_app)
                    list.Add(item.ConvertToBase());
                fcCaseSet.BudgetAssets = list.ToArray();
            }
                //fcCaseSet.BudgetAssets = ((List<BudgetAssetDTO>)Session[SessionVariables.BUDGET_ASSET_COLLECTION]).ToArray();
            else
                fcCaseSet.BudgetAssets = null;

            if (Session[SessionVariables.BUDGET_ITEM_COLLECTION] != null)
            {
                var list = new List<BudgetItemDTO>();
                var list_app = ((List<BudgetItemDTO_App>)Session[SessionVariables.BUDGET_ITEM_COLLECTION]);
                foreach (var item in list_app)
                    list.Add(item.ConvertToBase());
                fcCaseSet.BudgetItems = list.ToArray();
            }
                //fcCaseSet.BudgetItems = ((List<BudgetItemDTO_App>)Session[SessionVariables.BUDGET_ITEM_COLLECTION]).ToArray();
            else
                fcCaseSet.BudgetItems = null;

            if (Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION] != null)
            {
                var list = new List<BudgetItemDTO>();
                var list_app = ((List<BudgetItemDTO_App>)Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION]);
                foreach (var item in list_app)
                    list.Add(item.ConvertToBase());
                fcCaseSet.ProposedBudgetItems = list.ToArray();
            }            
            else
                fcCaseSet.ProposedBudgetItems = null;

            if (Session[SessionVariables.CASE_LOAN_COLLECTION] != null)
            {
                var list = new List<CaseLoanDTO>();
                var list_app = ((List<CaseLoanDTO_App>)Session[SessionVariables.CASE_LOAN_COLLECTION]);
                foreach (var item in list_app)
                    list.Add(item.ConvertToBase());
                fcCaseSet.CaseLoans = list.ToArray();
            }
                //fcCaseSet.CaseLoans = ((List<CaseLoanDTO>)Session[SessionVariables.CASE_LOAN_COLLECTION]).ToArray();
            else
                fcCaseSet.CaseLoans = null;

            if (Session[SessionVariables.OUTCOME_ITEM_COLLECTION] != null)
            {
                var list = new List<OutcomeItemDTO>();
                var list_app = ((List<OutcomeItemDTO_App>)Session[SessionVariables.OUTCOME_ITEM_COLLECTION]);
                foreach (var item in list_app)
                    list.Add(item.ConvertToBase());
                fcCaseSet.Outcome = list.ToArray();
            }
                //fcCaseSet.Outcome = ((List<OutcomeItemDTO_App>)Session[SessionVariables.OUTCOME_ITEM_COLLECTION]).ToArray();
            else
                fcCaseSet.Outcome = null;
                     
            request.ForeclosureCaseSet = fcCaseSet;
            return request;
        }        
        private static XDocument GetXmlDocument(string filename)
        {
            XDocument xdoc = XDocument.Load(filename);
            return xdoc;
        }

        private static XDocument GetXmlDocument(XmlReader xmlreader)
        {
            XDocument xdoc = XDocument.Load(xmlreader);
            return xdoc;
        }

        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            grdvMessages.Visible = false;
            try
            {
                if (fileUpload.HasFile)
                {
                    XmlReader xmlReader = new XmlTextReader(fileUpload.FileContent);
                    XDocument xdoc = GetXmlDocument(xmlReader);
                    BindToForm(xdoc);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception ex)
            {
                ExceptionMessage em = new ExceptionMessage();
                List<ExceptionMessage> exList = new List<ExceptionMessage>();
                em.Message = "Invalid XML format:" + ex.Message;
                exList.Add(em);
                em = new ExceptionMessage();
                em.Message = "Default fcCase is loaded";
                exList.Add(em);
                grdvMessages.DataSource = exList;
                grdvMessages.DataBind();
                grdvMessages.Visible = true;

                //ClearControls();

                LoadDefaultFcCase();

            }

        }

        private void ClearControls()
        {
            foreach (Control item in Page.Controls)
            {
                if (item.GetType() == typeof(TextBox))
                    ((TextBox)item).Text = "";
            }
        }

        private void RefreshAllGrids()
        {
            //if (Session[SessionVariables.BUDGET_ASSET_COLLECTION] == null)
                grdvBudgetAssetBinding();
            //if (Session[SessionVariables.BUDGET_ITEM_COLLECTION] == null)
                grdvBudgetItemBinding();
            //if (Session[SessionVariables.CASE_LOAN_COLLECTION] == null)
                grdvCaseLoanBinding();
            //if (Session[SessionVariables.OUTCOME_ITEM_COLLECTION] == null)
                grdvOutcomeItemBinding();

                grdvProposedBudgetItemBinding();
        }



        private BudgetItemDTO_App RowToProposedBudgetItemDTO(GridViewRow row)
        {
            TextBox txtBudgetItemAmt = (TextBox)row.FindControl("txtBudgetItemAmt1");
            TextBox txtBudgetNote = (TextBox)row.FindControl("txtBudgetNote1");
            TextBox txtBudgetSubcategoryId = (TextBox)row.FindControl("txtBudgetSubcategoryId1");
            Label lblBudgetItemId = (Label)row.FindControl("lblBudgetItemId1");            

            BudgetItemDTO_App budgetItem = new BudgetItemDTO_App();
            budgetItem.BudgetItemAmt = Util.ConvertToDouble(txtBudgetItemAmt.Text.Trim());
            budgetItem.BudgetNote = txtBudgetNote.Text.Trim();
            budgetItem.BudgetSubcategoryId = Util.ConvertToInt(txtBudgetSubcategoryId.Text.Trim());
            
            budgetItem.BudgetItemId = Util.ConvertToInt(lblBudgetItemId.Text.Trim());
            return budgetItem;
        }

        protected void grdvProposedBudgetItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                BudgetItemDTO_App budgetItem = RowToProposedBudgetItemDTO(grdvProposedBudgetItem.FooterRow);

                List<BudgetItemDTO_App> budgetItems = new List<BudgetItemDTO_App>();
                if (Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION] != null)
                {
                    budgetItems = (List<BudgetItemDTO_App>)Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION];
                    int? budgetItemId = budgetItems.Max(item => item.BudgetItemId);
                    budgetItem.BudgetItemId = budgetItemId + 1;
                }
                else
                {
                    budgetItem.BudgetItemId = 1;
                }

                budgetItems.Add(budgetItem);
                Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION] = budgetItems;
                //grdvBudgetItemBinding();
                RefreshAllGrids();
            } 
        }

        protected void grdvProposedBudgetItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int? budgetItemId = Util.ConvertToInt(((Label)grdvProposedBudgetItem.Rows[e.RowIndex].FindControl("lblBudgetItemId1")).Text);
            if (Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION] != null)
            {
                List<BudgetItemDTO_App> budgetItems = (List<BudgetItemDTO_App>)Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION];
                int index = budgetItems.FindIndex(item => item.BudgetItemId == budgetItemId);

                if (index < 0)
                {
                    //can not Delete item
                }
                else
                {
                    budgetItems.RemoveAt(index);
                    if (budgetItems.Count > 0)
                        Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION] = budgetItems;
                    else
                        Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION] = null;
                }

                grdvProposedBudgetItem.EditIndex = -1;
                RefreshAllGrids();
                //grdvBudgetItemBinding();
            }
            else
            {
                //can not Delete item
            }
        }

        protected void grdvProposedBudgetItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvProposedBudgetItem.EditIndex = e.NewEditIndex;            
            RefreshAllGrids();

        }

        protected void grdvProposedBudgetItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            BudgetItemDTO_App budgetItem = RowToProposedBudgetItemDTO(grdvProposedBudgetItem.Rows[e.RowIndex]);
            if (Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION] != null)
            {
                List<BudgetItemDTO_App> budgetItems = (List<BudgetItemDTO_App>)Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION];
                int index = budgetItems.FindIndex(item => item.BudgetItemId == budgetItem.BudgetItemId);

                if (index < 0)
                {
                    //can not update List
                }
                else
                {
                    budgetItems[index] = budgetItem;
                    Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION] = budgetItems;
                }


                grdvProposedBudgetItem.EditIndex = -1;
                //grdvBudgetItemBinding();
                RefreshAllGrids();
            }
            else
            {
                //can not update List
            }
        }

        protected void grdvProposedBudgetItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvProposedBudgetItem.EditIndex = -1;
            //grdvBudgetItemBinding();
            RefreshAllGrids();
        }

        protected void btnRetrieve_Click(object sender, EventArgs e)
        {
            try
            {
                int fcid = 0;
                AgencyWebService proxy = new AgencyWebService();
                SummaryRetrieveRequest request = new SummaryRetrieveRequest();
                HPF.Webservice.Agency.AuthenticationInfo ai = new HPF.Webservice.Agency.AuthenticationInfo();
                ai.UserName = txtUsername.Text.Trim();
                ai.Password = txtPassword.Text.Trim();
                proxy.AuthenticationInfoValue = ai;
                grdvMessages.Visible = false;
                lblMessage.Text = "Messsage: Success";
                if (Int32.TryParse(txtFcIdInput.Text.Trim(), out fcid))
                    request.FCId = fcid;
                SummaryRetrieveResponse response = proxy.RetrieveSummary(request);
                if (response.Status != ResponseStatus.Success)
                {
                    grdvMessages.Visible = true;
                    grdvMessages.DataSource = response.Messages;
                    grdvMessages.DataBind();
                }
                else
                {
                    #region Update UI
                    int i = 1;
                    List<BudgetAssetDTO_App> budgetAssetApps = null;
                    List<BudgetItemDTO_App> budgetItemApps = null;
                    List<CaseLoanDTO_App> caseLoanApps = null;
                    List<OutcomeItemDTO_App> outcomeApps = null;
                    List<BudgetItemDTO_App> proposedBudgetItemApps = null;
                    if (response.ForeclosureCaseSet.BudgetAssets != null && response.ForeclosureCaseSet.BudgetAssets.Length>0)
                    {
                        budgetAssetApps = new List<BudgetAssetDTO_App>();
                        for (i = 1; i <= response.ForeclosureCaseSet.BudgetAssets.Length; i++)
                        {
                            BudgetAssetDTO_App item = new BudgetAssetDTO_App();
                            item = item.ConvertFromBase(response.ForeclosureCaseSet.BudgetAssets[i - 1]);
                            item.BudgetAssetId = i;
                            budgetAssetApps.Add(item);
                        }
                    }
                    if (response.ForeclosureCaseSet.BudgetItems != null && response.ForeclosureCaseSet.BudgetItems.Length > 0)
                    {
                        budgetItemApps = new List<BudgetItemDTO_App>();
                        for (i = 1; i <= response.ForeclosureCaseSet.BudgetItems.Length; i++)
                        {
                            BudgetItemDTO_App item = new BudgetItemDTO_App();
                            item = item.ConvertFromBase(response.ForeclosureCaseSet.BudgetItems[i - 1]);
                            item.BudgetItemId = i;
                            budgetItemApps.Add(item);
                        }
                    }
                    if (response.ForeclosureCaseSet.ProposedBudgetItems != null && response.ForeclosureCaseSet.ProposedBudgetItems.Length > 0)
                    {
                        proposedBudgetItemApps = new List<BudgetItemDTO_App>();
                        for (i = 1; i <= response.ForeclosureCaseSet.ProposedBudgetItems.Length; i++)
                        {
                            BudgetItemDTO_App item = new BudgetItemDTO_App();
                            item = item.ConvertFromBase(response.ForeclosureCaseSet.ProposedBudgetItems[i - 1]);
                            item.BudgetItemId = i;
                            proposedBudgetItemApps.Add(item);
                        }
                    }
                    if (response.ForeclosureCaseSet.CaseLoans != null && response.ForeclosureCaseSet.CaseLoans.Length > 0)
                    {
                        caseLoanApps = new List<CaseLoanDTO_App>();
                        for (i = 1; i <= response.ForeclosureCaseSet.CaseLoans.Length; i++)
                        {
                            CaseLoanDTO_App item = new CaseLoanDTO_App();
                            item = item.ConvertFromBase(response.ForeclosureCaseSet.CaseLoans[i - 1]);
                            item.CaseLoanId = i;
                            caseLoanApps.Add(item);
                        }
                    }
                    if (response.ForeclosureCaseSet.Outcome != null && response.ForeclosureCaseSet.Outcome.Length > 0)
                    {
                        outcomeApps = new List<OutcomeItemDTO_App>();
                        for (i = 1; i <= response.ForeclosureCaseSet.Outcome.Length; i++)
                        {
                            OutcomeItemDTO_App item = new OutcomeItemDTO_App();
                            item = item.ConvertFromBase(response.ForeclosureCaseSet.Outcome[i - 1]);
                            item.OutcomeItemId = i;
                            outcomeApps.Add(item);
                        }
                    }
                    
                    Session[SessionVariables.CASE_LOAN_COLLECTION] = caseLoanApps;
                    Session[SessionVariables.BUDGET_ASSET_COLLECTION] = budgetAssetApps;
                    Session[SessionVariables.BUDGET_ITEM_COLLECTION] = budgetItemApps;
                    Session[SessionVariables.OUTCOME_ITEM_COLLECTION] = outcomeApps;
                    Session[SessionVariables.FORECLOSURE_CASE] = response.ForeclosureCaseSet.ForeclosureCase;
                    Session[SessionVariables.PROPOSED_BUDGET_ITEM_COLLECTION] = proposedBudgetItemApps;
                    grdvCaseLoanBinding();
                    grdvOutcomeItemBinding();
                    grdvBudgetItemBinding();
                    grdvProposedBudgetItemBinding();
                    grdvBudgetAssetBinding();
                    ForeclosureCaseToForm((ForeclosureCaseDTO)Session[SessionVariables.FORECLOSURE_CASE]);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Messsage:" + ex.Message;
            }
        }
    }
}
