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
using HPF.FutureState.Common.Utils.Exceptions;



namespace HPF.FutureState.Web.ForeclosureCaseDetail
{
    public partial class ForeclosureCaseDetail : System.Web.UI.UserControl
    {
        protected override void OnLoad(EventArgs e)
        {

            try
            {
                int caseid = int.Parse(Request.QueryString["CaseID"].ToString());
                BindDetailCaseData(caseid);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }

        }
        private void BindDetailCaseData(int caseid)
        {
            try
            {
            ForeclosureCaseDTO  foreclosureCase = ForeclosureCaseBL.Instance.GetForeclosureCase(caseid);
            if (foreclosureCase == null)
                return;
            BindForeclosureCaseDetail(foreclosureCase);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }

        }
        private void BindForeclosureCaseDetail(ForeclosureCaseDTO foreclosureCase)
        {
            //Top area
            //Property
            lblAddress1.Text = foreclosureCase.PropAddr1;
            lblAddress2.Text=foreclosureCase.PropAddr2;
            lblCity.Text=foreclosureCase.PropCity;
            if (foreclosureCase.PropZipPlus4 != null)
                lblStateZip.Text = foreclosureCase.PropStateCd + " - " + foreclosureCase.PropZip + " - " + foreclosureCase.PropZipPlus4;
            else
                lblStateZip.Text = foreclosureCase.PropStateCd + " - " + foreclosureCase.PropZip;    
            lblPrimaryResidence.Text = foreclosureCase.PrimaryResidenceInd;
            lblOwnerOccupied.Text = foreclosureCase.OwnerOccupiedInd;
            lblPropertyCode.Text = foreclosureCase.PropertyCd;
            lblNumberOfOccupants.Text = foreclosureCase.OccupantNum.ToString();
            lblPurchaseYear.Text = foreclosureCase.HomePurchaseYear.ToString();
            lblPurchasePrice.Text = foreclosureCase.HomePurchasePrice.ToString();
            lblCurrentMarketValue.Text = foreclosureCase.HomeCurrentMarketValue.ToString();
            lblForSaleIndicator.Text = foreclosureCase.ForSaleInd;
            lblRealtyCompany.Text = foreclosureCase.RealtyCompany;
            lblHomeAskingPrice.Text = foreclosureCase.HomeSalePrice.ToString();
            lblPrimaryResidence.Text = foreclosureCase.PrimResEstMktValue.ToString();
            //Borrower
            lblFirstName.Text = foreclosureCase.BorrowerFname;
            lblMidName.Text = foreclosureCase.BorrowerMname;
            lblLastName.Text = foreclosureCase.BorrowerLname;
            lblDOB.Text = foreclosureCase.BorrowerDob.ToShortDateString();
            if (foreclosureCase.BorrowerLast4Ssn != null)
                lblLast4SSN.Text = "XXX-XX-" + foreclosureCase.BorrowerLast4Ssn;
            else lblLast4SSN.Text = "";
            lblPrimaryContact.Text = foreclosureCase.PrimaryContactNo;
            lblSecondaryContact.Text = foreclosureCase.SecondContactNo;
            lblPrimaryEmail.Text = foreclosureCase.Email1;
            lblSecondaryEmail.Text = foreclosureCase.Email2;
            lblGender.Text = foreclosureCase.GenderCd;
            lblMother.Text = foreclosureCase.MotherMaidenLname;
            lblDisabled.Text = foreclosureCase.BorrowerDisabledInd;
            lblRace.Text = foreclosureCase.RaceCd;
            lblEthnicity.Text = foreclosureCase.HispanicInd;
            lblPreferedLanguage.Text = foreclosureCase.BorrowerPreferredLangCd;
            lblEducationLevel.Text = foreclosureCase.BorrowerEducLevelCompletedCd;
            lblMaritalStatus.Text = foreclosureCase.BorrowerMaritalStatusCd;
            lblOccupation.Text = foreclosureCase.BorrowerOccupationCd;
            lblMilitaryService.Text = foreclosureCase.MilitaryServiceCd;
            //Co-Borrower
            lblCoFirstName.Text = foreclosureCase.CoBorrowerFname;
            lblCoMidName.Text = foreclosureCase.CoBorrowerMname;
            lblCoLastName.Text = foreclosureCase.CoBorrowerLname;
            lblCoDOB.Text = foreclosureCase.CoBorrowerDob.ToShortDateString();
            if (foreclosureCase.CoBorrowerLast4Ssn != null)
                lblCoLast4SSN.Text = "XXX-XX-" + foreclosureCase.CoBorrowerLast4Ssn;
            else lblCoLast4SSN.Text = "";
            lblCoDisabled.Text = foreclosureCase.CoBorrowerDisabledInd;
            lblCoOccupation.Text = foreclosureCase.CoBorrowerOccupationCd;
            //Contact Address
            lblContactAdd1.Text = foreclosureCase.ContactAddr1;
            lblContactAdd2.Text = foreclosureCase.ContactAddr2;
            lblContactCity.Text = foreclosureCase.ContactCity;
            lblContactStateZip.Text = foreclosureCase.ContactStateCd +","+ foreclosureCase.ContactZip;
            //case status
            ddlDuplicate.SelectedValue = foreclosureCase.DuplicateInd;
            if (foreclosureCase.DuplicateInd == "Y")
                ddlDuplicate.SelectedIndex = 0;
            else ddlDuplicate.SelectedIndex = 1;
            ddlAgency.SelectedValue = foreclosureCase.AgencyId.ToString();
            lblAgencyCase.Text = foreclosureCase.AgencyCaseNum;
            lblAgencyClient.Text = foreclosureCase.AgencyClientNum;
            lblCounselor.Text = foreclosureCase.CounselorFname + " " + foreclosureCase.CounselorLname;
            lblPhoneExt.Text = foreclosureCase.CounselorPhone + " " + foreclosureCase.CounselorExt;
            lblCounselorEmail.Text = foreclosureCase.CounselorEmail;
            lblProgram.Text = foreclosureCase.ProgramId.ToString();
            lblIntakeDate.Text = foreclosureCase.IntakeDt.ToShortDateString();
            lblCompleteDate.Text = foreclosureCase.CompletedDt.ToShortDateString();
            lblCounsellingDuration.Text = foreclosureCase.CounselingDurationCd.ToString();
            lblSourceCode.Text = foreclosureCase.CaseSourceCd;
            //case summary
            lblSentDate.Text = foreclosureCase.SummarySentDt.ToShortDateString();
            lblSentOrther.Text = foreclosureCase.SummarySentOtherCd;
            lblOtherDate.Text = foreclosureCase.SummarySentOtherDt.ToShortDateString();
            //consent
            lblServicerConsent.Text = foreclosureCase.ServicerConsentInd;
            lblFundingConsent.Text = foreclosureCase.FundingConsentInd;
            //default reason
            lblDefaultReason.Text = foreclosureCase.DfltReason1stCd;
            lblSDefaultReason.Text = foreclosureCase.DfltReason2ndCd;
            //case financial
            lblHouseholdType.Text = foreclosureCase.HouseholdCd;
            lblAnnualIncome.Text = foreclosureCase.HouseholdGrossAnnualIncomeAmt.ToString();
            lblEarnerCode.Text = foreclosureCase.IncomeEarnersCd;
            lblAMIPercentage.Text = foreclosureCase.AmiPercentage.ToString();
            lblWServicer.Text = foreclosureCase.DiscussedSolutionWithSrvcrInd;
            lblAnotherAgency.Text = foreclosureCase.WorkedWithAnotherAgencyInd;
            lblSevicerRecently.Text = foreclosureCase.ContactedSrvcrRecentlyInd;
            lblWorkoutPlan.Text = foreclosureCase.WorkedWithAnotherAgencyInd;
            lblPlanCurrent.Text = foreclosureCase.SrvcrWorkoutPlanCurrentInd;
            lblCreditScores.Text = foreclosureCase.IntakeCreditScore;
            lblCreditBureau.Text = foreclosureCase.IntakeCreditBureauCd;
            //foreclosure notice

            lblNoticeReceived.Text = foreclosureCase.FcSaleDate.ToShortDateString();            
            //bankcruptcy
            lblBankruptcy.Text = foreclosureCase.BankruptcyInd;
            lblBankruptcyAttomey.Text = foreclosureCase.BankruptcyAttorney;
            lblCurrentIndicator.Text = foreclosureCase.BankruptcyPmtCurrentInd;
            //HUD
            lblTerminationReason.Text = foreclosureCase.HudOutcomeCd;
            lblTerminationDate.Text = foreclosureCase.HudTerminationDt.ToShortDateString();
            lblHUDOutcome.Text = foreclosureCase.HudOutcomeCd;
            //couselor notes
            txtReasonNote.Text = foreclosureCase.LoanDfltReasonNotes;
            txtItemNotes.Text = foreclosureCase.ActionItemsNotes;
            txtFollowUpNotes.Text = foreclosureCase.FollowupNotes;
            //Opt In/Out
            ddlNotCall.SelectedValue = foreclosureCase.DoNotCallInd;
            if (foreclosureCase.DoNotCallInd == "Y")
                ddlNotCall.SelectedItem.Text = "Yes";
            else ddlNotCall.SelectedItem.Text = "No";
            ddlNewsLetter.SelectedValue = foreclosureCase.OptOutNewsletterInd;
            if (foreclosureCase.OptOutNewsletterInd == "Y")
                ddlNewsLetter.SelectedItem.Text = "Yes";
            else ddlNewsLetter.SelectedItem.Text = "No";
            ddlServey.SelectedValue = foreclosureCase.OptOutSurveyInd;
            if (foreclosureCase.OptOutSurveyInd == "Y")
                ddlServey.SelectedItem.Text = "Yes";
            else ddlServey.SelectedItem.Text = "No";
            //media cadidate
            lblMediaInterest.Text = foreclosureCase.AgencyMediaInterestInd;
            
            ddlMediaCondirmation.SelectedValue = foreclosureCase.HpfMediaCandidateInd;
            if (foreclosureCase.HpfMediaCandidateInd == "Y")
                ddlMediaCondirmation.SelectedIndex = 1;//yes
            else
                if (foreclosureCase.HpfMediaCandidateInd == "N")
                    ddlMediaCondirmation.SelectedIndex = 2;//no
                else ddlMediaCondirmation.SelectedIndex = 0;//blank
            //success story
            lblSuccessStory.Text = foreclosureCase.AgencySuccessStoryInd;
            
            ddlSuccessStory.SelectedValue = foreclosureCase.HpfSuccessStoryInd;
            if (foreclosureCase.HpfSuccessStoryInd == "Y")
                ddlSuccessStory.SelectedIndex = 1;//yes
            else
                if (foreclosureCase.HpfSuccessStoryInd == "N")
                    ddlSuccessStory.SelectedIndex = 2;//no
                else ddlSuccessStory.SelectedIndex = 0;//blank
            BindAgencyDropdownlist(foreclosureCase.AgencyId.ToString());
        }

     
        protected void BindAgencyDropdownlist(string agencyname)
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

        protected void UpdateForecloseCase()
        {
            //get update datacollection from UI
            ForeclosureCaseDTO foreclosureCase = GetUpdateInfo();
            try
            {
                int fcid=ForeclosureCaseBL.Instance.UpdateForeclosureCase(foreclosureCase);
                BindDetailCaseData(fcid);
                lblMessage.Text = "Save foreclosure case succesfull";
            }
            
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
                
            }
            
        }
        /// <summary>
        /// get update info from UI
        /// </summary>
        /// <returns></returns>
        private ForeclosureCaseDTO GetUpdateInfo()
        {
            ForeclosureCaseDTO foreclosureCase = new ForeclosureCaseDTO();
            //case status
            foreclosureCase.DuplicateInd = ddlDuplicate.SelectedItem.Value;
            foreclosureCase.AgencyId = int.Parse(ddlAgency.SelectedItem.Value);
            //couselor notes
            foreclosureCase.DfltReason1stCd = txtReasonNote.Text;
            foreclosureCase.ActionItemsNotes = txtItemNotes.Text;
            foreclosureCase.FollowupNotes = txtFollowUpNotes.Text;
            //Opt In/Out
            foreclosureCase.DoNotCallInd = ddlNotCall.SelectedItem.Value;
            foreclosureCase.OptOutNewsletterInd = ddlNewsLetter.SelectedItem.Value;
            foreclosureCase.OptOutSurveyInd = ddlServey.SelectedItem.Value;
            //media cadidate
            if (ddlMediaCondirmation.SelectedValue == "") foreclosureCase.HpfMediaCandidateInd = null;
            else
                foreclosureCase.HpfMediaCandidateInd = ddlMediaCondirmation.SelectedItem.Value;
            //success story
            if (ddlSuccessStory.SelectedValue == "") foreclosureCase.HpfSuccessStoryInd = null;
            else
                foreclosureCase.HpfSuccessStoryInd = ddlSuccessStory.SelectedItem.Value;
            return foreclosureCase;
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            UpdateForecloseCase();
        }

    }
}