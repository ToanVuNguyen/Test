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
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common;

namespace HPF.FutureState.Web.ForeclosureCaseDetail
{
    public partial class ForeclosureCaseDetail : System.Web.UI.UserControl
    {
        protected override void OnLoad(EventArgs e)
        {
            bulMessage.Items.Clear();
            hidChkAgencyActive.Value = "";
            try
            {
                ApplySecurity();
                if (Request.QueryString["CaseID"] != null)
                {
                    int caseid = int.Parse(Request.QueryString["CaseID"].ToString());
                    ViewState["CaseID"] = caseid.ToString();
                    BindDetailCaseData(caseid);
                }
            }
            catch (Exception ex)
            {
                bulMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                btn_Save.Enabled = false;
                btn_Save0.Enabled = false;
                ddlAgency.Enabled = false;
                ddlDuplicate.Enabled = false;
                ddlMediaCondirmation.Enabled = false;
                ddlNewsLetter.Enabled = false;
                ddlNotCall.Enabled = false;
                ddlServey.Enabled = false;
                ddlSuccessStory.Enabled = false;
            }
        }
        private void BindDetailCaseData(int? caseid)
        {
            bulMessage.Items.Clear();
            ForeclosureCaseDTO foreclosureCase = ForeclosureCaseBL.Instance.GetForeclosureCase(caseid);
            if (foreclosureCase == null)
                return;
            BindForeclosureCaseDetail(foreclosureCase);
        }
        /// <summary>
        /// help to return Y: Yes, N: No and Null: ""
        /// </summary>
        /// <param name="Ind"></param>
        /// <returns></returns>
        private string DisplayInd(string Ind)
        {
            if (String.IsNullOrEmpty(Ind)) return "";
            Ind = Ind.Trim();
            if (Ind == "Y")
                return "Yes";
            if (Ind == "N")
                return "No";
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agencyname"></param>
        protected void BindDDLAgency(string agencyid)
        {
            AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgency();
            ddlAgency.DataValueField = "AgencyID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = agencyCollection;
            ddlAgency.DataBind();
            ////check agency active or not
            foreach (var agency in agencyCollection)
                if (agency.AgencyID == agencyid)
                    hidChkAgencyActive.Value = "1";

            if (hidChkAgencyActive.Value == "1")
            {
                ddlAgency.SelectedValue = agencyid;
            }
            else
            {
                ddlAgency.Items.Insert(0, new ListItem("", "inactive"));
                ddlAgency.SelectedValue = "inactive";
            }
            ddlAgency.Items.RemoveAt(ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue("-1")));
        }

        private void BindForeclosureCaseDetail(ForeclosureCaseDTO foreclosureCase)
        {
            if (foreclosureCase.AgencyId != null)
            {
                //Top area
                //Property
                lblAddress1.Text = foreclosureCase.PropAddr1;
                lblAddress2.Text = foreclosureCase.PropAddr2;
                lblCity.Text = foreclosureCase.PropCity;
                if (String.IsNullOrEmpty(foreclosureCase.PropZipPlus4))
                    lblStateZip.Text = foreclosureCase.PropStateCd + ", " + foreclosureCase.PropZip;
                else
                    lblStateZip.Text = foreclosureCase.PropStateCd + ", " + foreclosureCase.PropZip + " - " + foreclosureCase.PropZipPlus4;
                lblPrimaryResidence.Text = DisplayInd(foreclosureCase.PrimaryResidenceInd);
                lblOwnerOccupied.Text = DisplayInd(foreclosureCase.OwnerOccupiedInd);
                lblPropertyCode.Text = foreclosureCase.PropertyDesc;
                lblNumberOfOccupants.Text = foreclosureCase.OccupantNum.ToString();
                lblPurchaseYear.Text = foreclosureCase.HomePurchaseYear.ToString();
                lblPurchasePrice.Text = foreclosureCase.HomePurchasePrice.HasValue ? foreclosureCase.HomePurchasePrice.Value.ToString("C"):null;
                lblCurrentMarketValue.Text = foreclosureCase.HomeCurrentMarketValue.HasValue?foreclosureCase.HomeCurrentMarketValue.Value.ToString("C"):null;
                lblForSaleIndicator.Text = DisplayInd(foreclosureCase.ForSaleInd);
                lblRealtyCompany.Text = foreclosureCase.RealtyCompany;
                lblHomeAskingPrice.Text = foreclosureCase.HomeSalePrice.HasValue?foreclosureCase.HomeSalePrice.Value.ToString("C"):null;
                lblPrimaryRes.Text = foreclosureCase.PrimResEstMktValue.HasValue?foreclosureCase.PrimResEstMktValue.Value.ToString("C"):null;
                //Borrower
                lblFirstName.Text = foreclosureCase.BorrowerFname;
                lblMidName.Text = foreclosureCase.BorrowerMname;
                lblLastName.Text = foreclosureCase.BorrowerLname;
                lblDOB.Text = foreclosureCase.BorrowerDob == null ? "" : foreclosureCase.BorrowerDob.Value.ToShortDateString();
                if (String.IsNullOrEmpty(foreclosureCase.BorrowerLast4Ssn))
                    lblLast4SSN.Text = "";
                else lblLast4SSN.Text = "XXX-XX-" + foreclosureCase.BorrowerLast4Ssn;
                lblPrimaryContact.Text = foreclosureCase.PrimaryContactNo;
                lblSecondaryContact.Text = foreclosureCase.SecondContactNo;
                lblPrimaryEmail.Text = foreclosureCase.Email1;
                lblSecondaryEmail.Text = foreclosureCase.Email2;
                lblGender.Text = foreclosureCase.GenderDesc;
                lblMother.Text = foreclosureCase.MotherMaidenLname;
                lblDisabled.Text = DisplayInd(foreclosureCase.BorrowerDisabledInd);
                lblRace.Text = foreclosureCase.RaceDesc;
                lblEthnicity.Text = DisplayInd(foreclosureCase.HispanicInd);
                lblPreferedLanguage.Text = foreclosureCase.LanguageDesc;
                lblEducationLevel.Text = foreclosureCase.EducationDesc;
                lblMaritalStatus.Text = foreclosureCase.MaritalDesc;
                lblOccupation.Text = foreclosureCase.BorrowerOccupation;
                lblMilitaryService.Text = foreclosureCase.MilitaryDesc;
                //Co-Borrower
                lblCoFirstName.Text = foreclosureCase.CoBorrowerFname;
                lblCoMidName.Text = foreclosureCase.CoBorrowerMname;
                lblCoLastName.Text = foreclosureCase.CoBorrowerLname;
                //DisplayDt(foreclosureCase.CoBorrowerDob.Value, lblCoDOB);
                lblCoDOB.Text = foreclosureCase.CoBorrowerDob == null ? "" : foreclosureCase.CoBorrowerDob.Value.ToShortDateString();
                if (String.IsNullOrEmpty(foreclosureCase.CoBorrowerLast4Ssn))
                    lblCoLast4SSN.Text = "";
                else lblCoLast4SSN.Text = "XXX-XX-" + foreclosureCase.CoBorrowerLast4Ssn;
                lblCoDisabled.Text = DisplayInd(foreclosureCase.CoBorrowerDisabledInd);
                lblCoOccupation.Text = foreclosureCase.CoBorrowerOccupation;
                //Contact Address
                lblContactAdd1.Text = foreclosureCase.ContactAddr1;
                lblContactAdd2.Text = foreclosureCase.ContactAddr2;
                lblContactCity.Text = foreclosureCase.ContactCity;
                if (String.IsNullOrEmpty(foreclosureCase.ContactZipPlus4))
                    lblContactStateZip.Text = foreclosureCase.ContactStateCd + ", " + foreclosureCase.ContactZip;
                else lblContactStateZip.Text = foreclosureCase.ContactStateCd + ", " + foreclosureCase.ContactZip + " - " + foreclosureCase.ContactZipPlus4;

                //case status
                ddlDuplicate.SelectedValue = foreclosureCase.DuplicateInd;
                if (foreclosureCase.DuplicateInd == "Y")
                    ddlDuplicate.SelectedIndex = 0;
                else ddlDuplicate.SelectedIndex = 1;
                lblAgencyCase.Text = foreclosureCase.AgencyCaseNum;
                lblAgencyClient.Text = foreclosureCase.AgencyClientNum;
                lblCounselor.Text = foreclosureCase.CounselorFname + " " + foreclosureCase.CounselorLname;
                if (string.IsNullOrEmpty(foreclosureCase.CounselorExt))
                    lblPhoneExt.Text = foreclosureCase.CounselorPhone;
                else
                    lblPhoneExt.Text = foreclosureCase.CounselorPhone + " (ext:"+ foreclosureCase.CounselorExt + ")";
                lblCounselorEmail.Text = foreclosureCase.CounselorEmail;
                lblProgram.Text = foreclosureCase.ProgramName.ToString();
                lblIntakeDate.Text = foreclosureCase.IntakeDt == null ? "" : foreclosureCase.IntakeDt.Value.ToShortDateString();
                lblCompleteDate.Text = foreclosureCase.CompletedDt == null ? "" : foreclosureCase.CompletedDt.Value.ToShortDateString();
                lblCounsellingDuration.Text = foreclosureCase.CounselingDesc;
                lblSourceCode.Text = foreclosureCase.CaseSourceDesc;
                //case summary
                lblSentDate.Text = foreclosureCase.SummarySentDt == null ? "" : foreclosureCase.SummarySentDt.Value.ToShortDateString();
                lblSentOrther.Text = foreclosureCase.SummaryDesc;
                lblOtherDate.Text = foreclosureCase.SummarySentOtherDt == null ? "" : foreclosureCase.SummarySentOtherDt.Value.ToShortDateString();
                //consent
                lblServicerConsent.Text = DisplayInd(foreclosureCase.ServicerConsentInd);
                lblFundingConsent.Text = DisplayInd(foreclosureCase.FundingConsentInd);
                //default reason
                lblDefaultReason.Text = foreclosureCase.DefaultReason1Desc;
                lblSDefaultReason.Text = foreclosureCase.DefaultReason2Desc;
                //case financial
                lblHouseholdType.Text = foreclosureCase.HouseholdDesc;
                lblAnnualIncome.Text = foreclosureCase.HouseholdGrossAnnualIncomeAmt.HasValue?foreclosureCase.HouseholdGrossAnnualIncomeAmt.Value.ToString("C"):null;
                lblEarnerCode.Text = foreclosureCase.IncomeDesc;
                if(foreclosureCase.AmiPercentage.HasValue)
                    lblAMIPercentage.Text = (foreclosureCase.AmiPercentage.Value).ToString()+"%";
                lblWServicer.Text = DisplayInd(foreclosureCase.DiscussedSolutionWithSrvcrInd);
                lblAnotherAgency.Text = DisplayInd(foreclosureCase.WorkedWithAnotherAgencyInd);
                lblSevicerRecently.Text = DisplayInd(foreclosureCase.ContactedSrvcrRecentlyInd);
                lblWorkoutPlan.Text = DisplayInd(foreclosureCase.HasWorkoutPlanInd);
                lblPlanCurrent.Text = DisplayInd(foreclosureCase.SrvcrWorkoutPlanCurrentInd);
                lblCreditScores.Text = foreclosureCase.IntakeCreditScore;
                lblCreditBureau.Text = foreclosureCase.CreditDesc;
                //foreclosure notice

                lblNoticeReceived.Text = foreclosureCase.FcSaleDate == null ? "" : foreclosureCase.FcSaleDate.Value.ToShortDateString();
                lblDateSet.Text = foreclosureCase.FcNoticeReceiveInd;
                //bankcruptcy
                lblBankruptcy.Text = DisplayInd(foreclosureCase.BankruptcyInd);
                lblBankruptcyAttomey.Text = foreclosureCase.BankruptcyAttorney;
                lblCurrentIndicator.Text = DisplayInd(foreclosureCase.BankruptcyPmtCurrentInd);
                //HUD
                lblTerminationReason.Text = foreclosureCase.HudTerminationDesc;
                lblTerminationDate.Text = foreclosureCase.HudTerminationDt == null ? "" : foreclosureCase.HudTerminationDt.Value.ToShortDateString();
                lblHUDOutcome.Text = foreclosureCase.HudOutcomeDesc;
                //couselor notes
                txtReasonNote.Text = foreclosureCase.LoanDfltReasonNotes;
                txtItemNotes.Text = foreclosureCase.ActionItemsNotes;
                txtFollowUpNotes.Text = foreclosureCase.FollowupNotes;
                //Opt In/Out
                ddlNotCall.SelectedValue = foreclosureCase.DoNotCallInd;
                ddlNotCall.SelectedItem.Text = DisplayInd(foreclosureCase.DoNotCallInd);
                ddlNewsLetter.SelectedValue = foreclosureCase.OptOutNewsletterInd;
                ddlNewsLetter.SelectedItem.Text = DisplayInd(foreclosureCase.OptOutNewsletterInd);
                ddlServey.SelectedValue = foreclosureCase.OptOutSurveyInd;
                ddlServey.SelectedItem.Text = DisplayInd(foreclosureCase.OptOutSurveyInd);
                //media cadidate
                lblMediaInterest.Text = DisplayInd(foreclosureCase.AgencyMediaInterestInd);
                ddlMediaCondirmation.SelectedValue = foreclosureCase.HpfMediaCandidateInd;
                if (foreclosureCase.HpfMediaCandidateInd == "Y")
                    ddlMediaCondirmation.SelectedIndex = 1;//yes
                else
                    if (foreclosureCase.HpfMediaCandidateInd == "N")
                        ddlMediaCondirmation.SelectedIndex = 2;//no
                    else ddlMediaCondirmation.SelectedIndex = 0;//blank
                //success story
                lblSuccessStory.Text = DisplayInd(foreclosureCase.AgencySuccessStoryInd);
                ddlSuccessStory.SelectedValue = DisplayInd(foreclosureCase.HpfSuccessStoryInd);
                if (foreclosureCase.HpfSuccessStoryInd == "Y")
                    ddlSuccessStory.SelectedIndex = 1;//yes
                else
                    if (foreclosureCase.HpfSuccessStoryInd == "N")
                        ddlSuccessStory.SelectedIndex = 2;//no
                    else ddlSuccessStory.SelectedIndex = 0;//blank
                //if (ddlAgency.SelectedIndex == -1)
                BindDDLAgency(foreclosureCase.AgencyId.ToString());
            }
            else
                bulMessage.Items.Add(new ListItem("There is no data with this agency"));
        }
        protected bool UpdateForecloseCase()
        {
            try
            {
                bulMessage.Items.Clear();
                hidSaveIsYes.Value = "";
                //get update datacollection from UI
                ForeclosureCaseDTO foreclosureCase = null;
                if (ddlAgency.SelectedValue != "inactive")
                {
                    foreclosureCase = GetUpdateInfo();


                    if (foreclosureCase != null)
                    {
                        foreclosureCase.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
                        int? fcid = ForeclosureCaseBL.Instance.UpdateForeclosureCase(foreclosureCase);
                        bulMessage.Items.Add(new ListItem("Save foreclosure case succesfully"));
                        return true;
                    }
                    else                    
                        bulMessage.Items.Add(new ListItem("Agency null value, can't save"));                        
                }
                else
                    bulMessage.Items.Add(new ListItem("Can't Save foreclosure case because agency inactive."));                
            }
            catch (Exception ex)
            {
                bulMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }

            return false;
        }
        /// <summary>
        /// get update info from UI
        /// </summary>
        /// <returns></returns>
        private ForeclosureCaseDTO GetUpdateInfo()
        {
            ForeclosureCaseDTO foreclosureCase = new ForeclosureCaseDTO();
            //case status
            foreclosureCase.FcId = int.Parse(ViewState["CaseID"].ToString());
            foreclosureCase.DuplicateInd = ddlDuplicate.SelectedItem.Value;
            foreclosureCase.AgencyId = int.Parse(ddlAgency.SelectedItem.Value);
            //couselor notes
            foreclosureCase.LoanDfltReasonNotes = txtReasonNote.Text;
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
            int caseid = Int32.Parse(Request.QueryString["CaseID"]);
            Response.Redirect("ForeclosureCaseInfo.aspx?CaseID=" + caseid);
        }

        public string msgWARN0450
        {
            get
            {
                return ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0450);
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            if (!UpdateForecloseCase())
                selTabCtrl.Value = string.Empty;
        }
    }
}