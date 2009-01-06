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
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.DataTransferObjects;



namespace HPF.FutureState.Web.ForeclosureCaseDetail
{
    public partial class ForeclosureCaseDetail : System.Web.UI.UserControl
    {
        private ForeclosureCaseDTO _ForeclosureCase;
        public ForeclosureCaseDTO ForeclosureCase
        {

            get
            {
                return _ForeclosureCase;
            }
            set
            {
                _ForeclosureCase = value;
            }
        }
        private void BindData()
        {
            int caseid = int.Parse(Request.QueryString["CaseID"].ToString());
            ForeclosureCase = ForeclosureCaseBL.Instance.GetForeclosureCase(caseid);

            if (ForeclosureCase == null)
                return;
            //Property
            lblAddress1.Text = ForeclosureCase.PropAddr1;
            lblAddress2.Text=ForeclosureCase.PropAddr2;
            lblCity.Text=ForeclosureCase.PropCity;
            lblStateZip.Text=ForeclosureCase.PropStateCd+","+ForeclosureCase.PropZip;
            lblPrimaryResidence.Text = ForeclosureCase.PrimaryResidenceInd;
            lblOwnerOccupied.Text = ForeclosureCase.OwnerOccupiedInd;
            lblPropertyCode.Text = ForeclosureCase.PropertyCd;
            lblNumberOfOccupants.Text = ForeclosureCase.OccupantNum.ToString();
            lblPurchaseYear.Text = ForeclosureCase.HomePurchaseYear.ToString();
            lblPurchasePrice.Text = ForeclosureCase.HomePurchasePrice.ToString();
            lblCurrentMarketValue.Text = ForeclosureCase.HomeCurrentMarketValue.ToString();
            lblForSaleIndicator.Text = ForeclosureCase.ForSaleInd;
            lblRealtyCompany.Text = ForeclosureCase.RealtyCompany;
            lblHomeAskingPrice.Text = ForeclosureCase.HomeSalePrice.ToString();
            lblPrimaryResidence.Text = ForeclosureCase.PrimResEstMktValue.ToString();
            //Borrower
            lblFirstName.Text = ForeclosureCase.BorrowerFname;
            lblMidName.Text = ForeclosureCase.BorrowerMname;
            lblLastName.Text = ForeclosureCase.BorrowerLname;
            lblDOB.Text = ForeclosureCase.BorrowerDob.ToShortDateString();
            lblLast4SSN.Text = "XXX-XX-" + ForeclosureCase.BorrowerLast4Ssn;
            lblPrimaryContact.Text = ForeclosureCase.PrimaryContactNo;
            lblSecondaryContact.Text = ForeclosureCase.SecondContactNo;
            lblPrimaryEmail.Text = ForeclosureCase.Email1;
            lblSecondaryEmail.Text = ForeclosureCase.Email2;
            lblGender.Text = ForeclosureCase.GenderCd;
            lblMother.Text = ForeclosureCase.MotherMaidenLname;
            lblDisabled.Text = ForeclosureCase.BorrowerDisabledInd;
            lblRace.Text = ForeclosureCase.RaceCd;
            lblEthnicity.Text = ForeclosureCase.HispanicInd;
            lblPreferedLanguage.Text = ForeclosureCase.BorrowerPreferredLangCd;
            lblEducationLevel.Text = ForeclosureCase.BorrowerEducLevelCompletedCd;
            lblMaritalStatus.Text = ForeclosureCase.BorrowerMaritalStatusCd;
            lblOccupation.Text = ForeclosureCase.BorrowerOccupationCd;
            lblMilitaryService.Text = ForeclosureCase.MilitaryServiceCd;
            //Co-Borrower
            lblCoFirstName.Text = ForeclosureCase.CoBorrowerFname;
            lblCoMidName.Text = ForeclosureCase.CoBorrowerMname;
            lblCoLastName.Text = ForeclosureCase.CoBorrowerLname;
            lblCoDOB.Text = ForeclosureCase.CoBorrowerDob.ToShortDateString();
            lblCoLast4SSN.Text = "XXX-XX-" + ForeclosureCase.CoBorrowerLast4Ssn;
            lblCoDisabled.Text = ForeclosureCase.CoBorrowerDisabledInd;
            lblCoOccupation.Text = ForeclosureCase.CoBorrowerOccupationCd;
            //Contact Address
            lblContactAdd1.Text = ForeclosureCase.ContactAddr1;
            lblContactAdd2.Text = ForeclosureCase.ContactAddr2;
            lblContactCity.Text = ForeclosureCase.ContactCity;
            lblContactStateZip.Text = ForeclosureCase.ContactStateCd +","+ ForeclosureCase.ContactZip;
            //case status
            ddlDuplicate.SelectedItem.Value = ForeclosureCase.DuplicateInd;
            ddlAgency.SelectedItem.Value = ForeclosureCase.AgencyId.ToString();
            lblAgencyCase.Text = ForeclosureCase.AgencyCaseNum;
            lblAgencyClient.Text = ForeclosureCase.AgencyClientNum;
            lblCounselor.Text = ForeclosureCase.CounselorFname + " " + ForeclosureCase.CounselorLname;
            lblPhoneExt.Text = ForeclosureCase.CounselorPhone + " " + ForeclosureCase.CounselorExt;
            lblCounselorEmail.Text = ForeclosureCase.CounselorEmail;
            lblProgram.Text = ForeclosureCase.ProgramId.ToString();
            lblIntakeDate.Text = ForeclosureCase.IntakeDt.ToString();
            lblCompleteDate.Text = ForeclosureCase.CompletedDt.ToString();
            lblCounsellingDuration.Text = ForeclosureCase.CounselingDurationCd.ToString();
            lblSourceCode.Text = ForeclosureCase.CaseSourceCd;
            //case summary
            lblSentDate.Text = ForeclosureCase.SummarySentDt.ToString();
            lblSentOrther.Text = ForeclosureCase.SummarySentOtherCd;
            lblOtherDate.Text = ForeclosureCase.SummarySentOtherDt.ToString();
            //consent
            lblServicerConsent.Text = ForeclosureCase.ServicerConsentInd;
            lblFundingConsent.Text = ForeclosureCase.FundingConsentInd;
            //default reason
            lblDefaultReason.Text = ForeclosureCase.DfltReason1stCd;
            lblSDefaultReason.Text = ForeclosureCase.DfltReason2ndCd;
            //case financial
            lblHouseholdType.Text = ForeclosureCase.HouseholdCd;
            lblAnnualIncome.Text = ForeclosureCase.HouseholdGrossAnnualIncomeAmt.ToString();
            lblEarnerCode.Text = ForeclosureCase.IncomeEarnersCd;
            lblAMIPercentage.Text = ForeclosureCase.AmiPercentage.ToString();
            lblWServicer.Text = ForeclosureCase.DiscussedSolutionWithSrvcrInd;
            lblAnotherAgency.Text = ForeclosureCase.WorkedWithAnotherAgencyInd;
            lblSevicerRecently.Text = ForeclosureCase.ContactedSrvcrRecentlyInd;
            lblWorkoutPlan.Text = ForeclosureCase.WorkedWithAnotherAgencyInd;
            lblPlanCurrent.Text = ForeclosureCase.SrvcrWorkoutPlanCurrentInd;
            lblCreditScores.Text = ForeclosureCase.IntakeCreditScore;
            lblCreditBureau.Text = ForeclosureCase.IntakeCreditBureauCd;
            //foreclosure notice
            
            //lblNoticeReceived.Text=ForeclosureCase.
            lblDateSet.Text = ForeclosureCase.FcSaleDateSetInd;
            //bankcruptcy
            lblBankruptcy.Text = ForeclosureCase.BankruptcyInd;
            lblBankruptcyAttomey.Text = ForeclosureCase.BankruptcyAttorney;
            lblCurrentIndicator.Text = ForeclosureCase.BankruptcyPmtCurrentInd;
            //HUD
            lblTerminationReason.Text = ForeclosureCase.HudOutcomeCd;
            lblTerminationDate.Text = ForeclosureCase.HudTerminationDt.ToString();
            lblHUDOutcome.Text = ForeclosureCase.HudOutcomeCd;
            //couselor notes
            txtReasonNote.Text= ForeclosureCase.DfltReason1stCd;
            txtItemNotes.Text = ForeclosureCase.ActionItemsNotes;
            txtFollowUpNotes.Text = ForeclosureCase.FollowupNotes;
            //Opt In/Out
            ddlNotCall.SelectedItem.Value = ForeclosureCase.DoNotCallInd;
            ddlNewsLetter.SelectedItem.Value = ForeclosureCase.OptOutNewsletterInd;
            ddlServey.SelectedItem.Value = ForeclosureCase.OptOutSurveyInd;
            //media cadidate
            lblMediaInterest.Text = ForeclosureCase.AgencyMediaConsentInd;
            ddlMediaCondirmation.SelectedItem.Value = ForeclosureCase.HpfMediaCandidateInd;
            //success story
            lblSuccessStory.Text = ForeclosureCase.AgencySuccessStoryInd;
            ddlSuccessStory.SelectedItem.Value = ForeclosureCase.HpfSuccessStoryInd;
            


        }
        protected override void OnLoad(EventArgs e)
        {
            if(!IsPostBack)
                BindDDLAgency();
        }
        protected void BindDDLAgency()
        {
            AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgency();
            ddlAgency.DataValueField = "AgencyID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = agencyCollection;
            ddlAgency.DataBind();
            ddlAgency.Items.FindByText("ALL").Selected = true;
        }

    }
}