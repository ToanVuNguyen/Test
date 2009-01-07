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
            if (Request.QueryString["CaseID"] == null)
                return;
            int caseid = int.Parse(Request.QueryString["CaseID"].ToString());
            ForeclosureCase = ForeclosureCaseBL.Instance.GetForeclosureCase(caseid);

            if (ForeclosureCase == null)
                return;
            //Top area
            

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
            ddlDuplicate.SelectedValue = ForeclosureCase.DuplicateInd;
            if (ForeclosureCase.DuplicateInd == "Y")
                ddlDuplicate.SelectedItem.Text = "Yes";
            else ddlDuplicate.SelectedItem.Text = "No";
            ddlAgency.SelectedValue = ForeclosureCase.AgencyId.ToString();
            lblAgencyCase.Text = ForeclosureCase.AgencyCaseNum;
            lblAgencyClient.Text = ForeclosureCase.AgencyClientNum;
            lblCounselor.Text = ForeclosureCase.CounselorFname + " " + ForeclosureCase.CounselorLname;
            lblPhoneExt.Text = ForeclosureCase.CounselorPhone + " " + ForeclosureCase.CounselorExt;
            lblCounselorEmail.Text = ForeclosureCase.CounselorEmail;
            lblProgram.Text = ForeclosureCase.ProgramId.ToString();
            lblIntakeDate.Text = ForeclosureCase.IntakeDt.ToShortDateString();
            lblCompleteDate.Text = ForeclosureCase.CompletedDt.ToShortDateString();
            lblCounsellingDuration.Text = ForeclosureCase.CounselingDurationCd.ToString();
            lblSourceCode.Text = ForeclosureCase.CaseSourceCd;
            //case summary
            lblSentDate.Text = ForeclosureCase.SummarySentDt.ToShortDateString();
            lblSentOrther.Text = ForeclosureCase.SummarySentOtherCd;
            lblOtherDate.Text = ForeclosureCase.SummarySentOtherDt.ToShortDateString();
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
            lblTerminationDate.Text = ForeclosureCase.HudTerminationDt.ToShortDateString();
            lblHUDOutcome.Text = ForeclosureCase.HudOutcomeCd;
            //couselor notes
            txtReasonNote.Text= ForeclosureCase.DfltReason1stCd;
            txtItemNotes.Text = ForeclosureCase.ActionItemsNotes;
            txtFollowUpNotes.Text = ForeclosureCase.FollowupNotes;
            //Opt In/Out
            ddlNotCall.SelectedValue = ForeclosureCase.DoNotCallInd;
            if (ForeclosureCase.DoNotCallInd == "Y")
                ddlNotCall.SelectedItem.Text = "Yes";
            else ddlNotCall.SelectedItem.Text = "No";
            ddlNewsLetter.SelectedValue = ForeclosureCase.OptOutNewsletterInd;
            if (ForeclosureCase.OptOutNewsletterInd == "Y")
                ddlNewsLetter.SelectedItem.Text = "Yes";
            else ddlNewsLetter.SelectedItem.Text = "No";
            ddlServey.SelectedValue = ForeclosureCase.OptOutSurveyInd;
            if (ForeclosureCase.OptOutSurveyInd == "Y")
                ddlServey.SelectedItem.Text = "Yes";
            else ddlServey.SelectedItem.Text = "No";
            //media cadidate
            lblMediaInterest.Text = ForeclosureCase.AgencyMediaConsentInd;
            ddlMediaCondirmation.SelectedValue = ForeclosureCase.HpfMediaCandidateInd;
            if (ForeclosureCase.HpfMediaCandidateInd == "Y")
                ddlMediaCondirmation.SelectedItem.Text = "Yes";
            else ddlMediaCondirmation.SelectedItem.Text = "No";
            //success story
            lblSuccessStory.Text = ForeclosureCase.AgencySuccessStoryInd;
            ddlSuccessStory.SelectedItem.Value = ForeclosureCase.HpfSuccessStoryInd;
            BindDDLAgency(ForeclosureCase.AgencyId.ToString());

        }
        protected override void OnLoad(EventArgs e)
        {

            BindData();

        }
        protected void BindDDLAgency(string agencyname)
        {
            AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgency();
            AgencyDTO item=agencyCollection[0];
            agencyCollection.Remove(item);
            ddlAgency.DataValueField = "AgencyID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = agencyCollection;
            ddlAgency.DataBind();
            ddlAgency.SelectedValue = agencyname;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {

        }

    }
}