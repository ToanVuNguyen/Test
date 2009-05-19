using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.IO;

using HPF.Webservice.Agency;

namespace HPF.FutureState.WebService.Test.Web
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRetrieveSummary_Click(object sender, EventArgs e)
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
                Panel1.Visible = false;
                lblMessage.Text = "Messsage: Success";

                if (Int32.TryParse(txtFcID.Text.Trim(), out fcid))
                    request.ForeclosureId = fcid;
                request.ReportOutput = txtReportFormat.Text.Trim();

                SummaryRetrieveResponse response = proxy.RetrieveSummary(request);                

                if (response.Status != ResponseStatus.Success)
                {
                    grdvMessages.Visible = true;
                    grdvMessages.DataSource = response.Messages;
                    grdvMessages.DataBind();
                }
                else if (request.ReportOutput.ToUpper() != "PDF")
                {
                    #region Update UI
                    Panel1.Visible = true;
                    ForeclosureCaseToForm(response.ForeclosureCaseSet.ForeclosureCase);

                    grdvBudgetAsset.DataSource = response.ForeclosureCaseSet.BudgetAssets;
                    grdvBudgetAsset.DataBind();
                    grdvBudgetItem.DataSource = response.ForeclosureCaseSet.BudgetItems;
                    grdvBudgetItem.DataBind();
                    grdvCaseLoan.DataSource = response.ForeclosureCaseSet.CaseLoans;
                    grdvCaseLoan.DataBind();
                    grdvOutcomeItem.DataSource = response.ForeclosureCaseSet.Outcome;
                    grdvOutcomeItem.DataBind();
                    #endregion
                }
                else if (response.ReportSummary.Length > 0)
                {
                    #region Write PDF to file
                    //demo to write report to file
                    string ext = "PDF";
                    FileStream fs = File.Create(txtReportFolder.Text + "\\Report_" + txtReportFormat.Text + "_" + txtFcID.Text + "." + ext);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(response.ReportSummary);
                    lblMessage.Text = "Messsage: Please check the output!";
                    bw.Close();
                    fs.Close();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Messsage:" + ex.Message;
            }
        }

        private void ForeclosureCaseToForm(ForeclosureCaseDTO fcCase)
        {
            if (fcCase != null)
            {
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

                txtWorkingUserID.Text = fcCase.ChgLstUserId;
            }
        }
        
    }
}
