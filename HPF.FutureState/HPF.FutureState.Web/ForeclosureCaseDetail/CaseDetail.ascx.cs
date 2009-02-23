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
            try
            {
               // ApplySecurity();
                int caseid = int.Parse(Request.QueryString["CaseID"].ToString());
                if (Request.QueryString["CaseID"]!=null)
                    BindDetailCaseData(caseid);
                else
                    bulMessage.Items.Add(new ListItem("There is no data with this fc_id"));
            }
            catch (Exception ex)
            {
                bulMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }

        }
        //private void ApplySecurity()
        //{
        //    if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
        //    {
        //        Response.Redirect("ErrorPage.aspx?CODE=ERR999");
        //    }
        //    if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
        //    {
        //        btn_Save.Enabled = false;
        //    }
        //}
        private void BindDetailCaseData(int? caseid)
        {
            bulMessage.Items.Clear();
            try
            {
                ForeclosureCaseDTO foreclosureCase = ForeclosureCaseBL.Instance.GetForeclosureCase(caseid);
                if (foreclosureCase == null)
                    return;
                BindForeclosureCaseDetail(foreclosureCase);
            }
            catch (Exception ex)
            {
                bulMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        /// <summary>
        /// help to return Y: Yes, N: No and Null: ""
        /// </summary>
        /// <param name="Ind"></param>
        /// <returns></returns>
        private string DisplayInd(string Ind)
        {
            if (Ind == null) return "";
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
        protected void BindDDLAgency(string agencyname)
        {
            AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgency();
            AgencyDTO item = agencyCollection[0];
            agencyCollection.Remove(item);
            ddlAgency.DataValueField = "AgencyID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = agencyCollection;
            ddlAgency.DataBind();
            ddlAgency.SelectedValue = agencyname;
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
                    lblStateZip.Text = foreclosureCase.PropStateCd + " - " + foreclosureCase.PropZip;
                else
                    lblStateZip.Text = foreclosureCase.PropStateCd + " - " + foreclosureCase.PropZip + " - " + foreclosureCase.PropZipPlus4;
                lblPrimaryResidence.Text = DisplayInd(foreclosureCase.PrimaryResidenceInd);
                lblOwnerOccupied.Text=DisplayInd(foreclosureCase.OwnerOccupiedInd);
                lblPropertyCode.Text = foreclosureCase.PropertyCd;
                lblNumberOfOccupants.Text = foreclosureCase.OccupantNum.ToString();
                lblPurchaseYear.Text = foreclosureCase.HomePurchaseYear.ToString();
                lblPurchasePrice.Text = foreclosureCase.HomePurchasePrice.ToString();
                lblCurrentMarketValue.Text = foreclosureCase.HomeCurrentMarketValue.ToString();
                lblForSaleIndicator.Text = DisplayInd(foreclosureCase.ForSaleInd);
                lblRealtyCompany.Text = foreclosureCase.RealtyCompany;
                lblHomeAskingPrice.Text = foreclosureCase.HomeSalePrice.ToString();
                lblPrimaryRes.Text = foreclosureCase.PrimResEstMktValue.ToString();
                //Borrower
                lblFirstName.Text = foreclosureCase.BorrowerFname;
                lblMidName.Text = foreclosureCase.BorrowerMname;
                lblLastName.Text = foreclosureCase.BorrowerLname;
                lblDOB.Text = foreclosureCase.BorrowerDob == null ? "" : foreclosureCase.BorrowerDob.Value.ToShortDateString();
                if(String.IsNullOrEmpty(foreclosureCase.BorrowerLast4Ssn))
                    lblLast4SSN.Text = "";
                else lblLast4SSN.Text = "XXX-XX-" + foreclosureCase.BorrowerLast4Ssn;
                lblPrimaryContact.Text = foreclosureCase.PrimaryContactNo;
                lblSecondaryContact.Text = foreclosureCase.SecondContactNo;
                lblPrimaryEmail.Text = foreclosureCase.Email1;
                lblSecondaryEmail.Text = foreclosureCase.Email2;
                lblGender.Text = foreclosureCase.GenderCd;
                lblMother.Text = foreclosureCase.MotherMaidenLname;
                lblDisabled.Text = DisplayInd(foreclosureCase.BorrowerDisabledInd);
                lblRace.Text = foreclosureCase.RaceCd;
                lblEthnicity.Text = DisplayInd(foreclosureCase.HispanicInd);
                lblPreferedLanguage.Text = foreclosureCase.BorrowerPreferredLangCd;
                lblEducationLevel.Text = foreclosureCase.BorrowerEducLevelCompletedCd;
                lblMaritalStatus.Text = foreclosureCase.BorrowerMaritalStatusCd;
                lblOccupation.Text = foreclosureCase.BorrowerOccupation;
                lblMilitaryService.Text = foreclosureCase.MilitaryServiceCd;
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
                lblContactStateZip.Text = foreclosureCase.ContactStateCd + " , " + foreclosureCase.ContactZip+" , "+foreclosureCase.ContactZipPlus4;
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
                lblIntakeDate.Text = foreclosureCase.IntakeDt == null ? "" : foreclosureCase.IntakeDt.Value.ToShortDateString();
                lblCompleteDate.Text = foreclosureCase.CompletedDt == null ? "" : foreclosureCase.CompletedDt.Value.ToShortDateString();
                lblCounsellingDuration.Text = foreclosureCase.CounselingDurationCd.ToString();
                lblSourceCode.Text = foreclosureCase.CaseSourceCd;
                //case summary
                lblSentDate.Text = foreclosureCase.SummarySentDt == null ? "" : foreclosureCase.SummarySentDt.Value.ToShortDateString();
                lblSentOrther.Text = foreclosureCase.SummarySentOtherCd;
                lblOtherDate.Text = foreclosureCase.SummarySentOtherDt == null ? "" : foreclosureCase.SummarySentOtherDt.Value.ToShortDateString();
                //consent
                lblServicerConsent.Text = DisplayInd(foreclosureCase.ServicerConsentInd);
                lblFundingConsent.Text = DisplayInd(foreclosureCase.FundingConsentInd);
                //default reason
                lblDefaultReason.Text = foreclosureCase.DfltReason1stCd;
                lblSDefaultReason.Text = foreclosureCase.DfltReason2ndCd;
                //case financial
                lblHouseholdType.Text = foreclosureCase.HouseholdCd;
                lblAnnualIncome.Text = foreclosureCase.HouseholdGrossAnnualIncomeAmt.ToString();
                lblEarnerCode.Text = foreclosureCase.IncomeEarnersCd;
                lblAMIPercentage.Text = foreclosureCase.AmiPercentage.ToString();
                lblWServicer.Text = DisplayInd(foreclosureCase.DiscussedSolutionWithSrvcrInd);
                lblAnotherAgency.Text = DisplayInd(foreclosureCase.WorkedWithAnotherAgencyInd);
                lblSevicerRecently.Text = DisplayInd(foreclosureCase.ContactedSrvcrRecentlyInd);
                lblWorkoutPlan.Text = DisplayInd(foreclosureCase.HasWorkoutPlanInd);
                lblPlanCurrent.Text = DisplayInd(foreclosureCase.SrvcrWorkoutPlanCurrentInd);
                lblCreditScores.Text = foreclosureCase.IntakeCreditScore;
                lblCreditBureau.Text = foreclosureCase.IntakeCreditBureauCd;
                //foreclosure notice

                lblNoticeReceived.Text = foreclosureCase.FcSaleDate == null ? "" : foreclosureCase.FcSaleDate.Value.ToShortDateString();
                //bankcruptcy
                lblBankruptcy.Text = DisplayInd(foreclosureCase.BankruptcyInd);
                lblBankruptcyAttomey.Text = foreclosureCase.BankruptcyAttorney;
                lblCurrentIndicator.Text = DisplayInd(foreclosureCase.BankruptcyPmtCurrentInd);
                //HUD
                lblTerminationReason.Text = foreclosureCase.HudOutcomeCd;
                lblTerminationDate.Text = foreclosureCase.HudTerminationDt == null ? "" : foreclosureCase.HudTerminationDt.Value.ToShortDateString();
                lblHUDOutcome.Text = foreclosureCase.HudOutcomeCd;
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
                ddlSuccessStory.SelectedValue =DisplayInd(foreclosureCase.HpfSuccessStoryInd);
                if (foreclosureCase.HpfSuccessStoryInd == "Y")
                    ddlSuccessStory.SelectedIndex = 1;//yes
                else
                    if (foreclosureCase.HpfSuccessStoryInd == "N")
                        ddlSuccessStory.SelectedIndex = 2;//no
                    else ddlSuccessStory.SelectedIndex = 0;//blank
                BindDDLAgency(foreclosureCase.AgencyId.ToString());
            }
            else
                bulMessage.Items.Add(new ListItem("There is no data with this agency"));
        }
        protected void UpdateForecloseCase()
        {
            //get update datacollection from UI
            ForeclosureCaseDTO foreclosureCase=null;
            if(ddlAgency.SelectedValue!="") 
            foreclosureCase = GetUpdateInfo();
            
            try
            {
                if (foreclosureCase != null)
                {
                    foreclosureCase.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
                    int? fcid = ForeclosureCaseBL.Instance.UpdateForeclosureCase(foreclosureCase);
                    BindDetailCaseData(fcid);
                    bulMessage.Items.Add(new ListItem("Save foreclosure case succesfull"));
                }
                else bulMessage.Items.Add(new ListItem("Agency null value, can't save"));
            }
            catch (Exception ex)
            {
                bulMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
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
                foreclosureCase.FcId = int.Parse(Request.QueryString["CaseID"].ToString());
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
        }

        
    }
}