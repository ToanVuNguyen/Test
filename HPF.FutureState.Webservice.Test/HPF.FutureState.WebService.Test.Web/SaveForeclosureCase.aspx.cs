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

            grdvCaseLoanBinding();
            grdvOutcomeItemBinding();
            grdvBudgetItemBinding();
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
            fcCase.BorrowerOccupationCd = txtBorrowerOccupationCd.Text.Trim();
            fcCase.BorrowerPreferredLangCd = txtBorrowerPreferedLangCd.Text.Trim();
            fcCase.BorrowerSsn = txtBorrowerSSN.Text.Trim();
            fcCase.CallId = Util.ConvertToInt(txtCallID.Text.Trim());
            //fcCase.CaseCompleteInd = txtCaseCompleteInd.Text.Trim();
            fcCase.CaseSourceCd = txtCaseSourceCd.Text.Trim();
            fcCase.CoBorrowerDisabledInd = txtCoBorrowerDisabledInd.Text.Trim();
            fcCase.CoBorrowerDob = Util.ConvertToDateTime(txtCoBorrowerDOB.Text.Trim());
            fcCase.CoBorrowerFname = txtCoBorrowerFName.Text.Trim();
            //fcCase.CoBorrowerLast4Ssn = txtCoBorrowerLast4SSN.Text.Trim();
            fcCase.CoBorrowerLname = txtCoBorrowerLName.Text.Trim();
            fcCase.CoBorrowerMname = txtCoBorrowerMName.Text.Trim();
            fcCase.CoBorrowerOccupationCd = txtCoBorrowerOccupationCd.Text.Trim();
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

            Session[SessionVariables.FORECLOSURE_CASE] = fcCase;

            return fcCase;


        }

        private void ForeclosureCaseToForm(ForeclosureCaseDTO fcCase)
        {
            if (fcCase != null)
            {
                txtActionItemsNotes.Text = fcCase.ActionItemsNotes.ToString();
                txtAgencyCaseNumber.Text = fcCase.AgencyCaseNum.ToString();
                txtAgencyClientNumber.Text = fcCase.AgencyClientNum.ToString();
                txtAgencyID.Text = fcCase.AgencyId.ToString();
                txtAgencyMediaConsentInd.Text = fcCase.AgencyMediaInterestInd.ToString();
                txtAgencySuccessStory.Text = fcCase.AgencySuccessStoryInd.ToString();
                //txtAMIPercentage.Text = fcCase.AmiPercentage.ToString();
                txtAssignedCounselorIDRef.Text = fcCase.AssignedCounselorIdRef.ToString();
                txtBankruptcyAttorney.Text = fcCase.BankruptcyAttorney.ToString();
                txtBankruptcyInd.Text = fcCase.BankruptcyInd.ToString();
                txtBankruptcyPmtCurrentInd.Text = fcCase.BankruptcyPmtCurrentInd.ToString();
                txtBorrowerDisabledInd.Text = fcCase.BorrowerDisabledInd.ToString();
                txtBorrowerDOB.Text = fcCase.BorrowerDob.ToString();
                txtBorrowerEducLevelCompletedInd.Text = fcCase.BorrowerEducLevelCompletedCd.ToString();
                txtBorrowerFName.Text = fcCase.BorrowerFname.ToString();
                //txtBorrowerLast4SSN.Text = fcCase.BorrowerLast4Ssn.ToString();
                txtBorrowerLName.Text = fcCase.BorrowerLname.ToString();
                txtBorrowerMaritalStatusCd.Text = fcCase.BorrowerMaritalStatusCd.ToString();
                txtBorrowerMName.Text = fcCase.BorrowerMname.ToString();
                txtBorrowerOccupationCd.Text = fcCase.BorrowerOccupationCd.ToString();
                txtBorrowerPreferedLangCd.Text = fcCase.BorrowerPreferredLangCd.ToString();
                txtBorrowerSSN.Text = fcCase.BorrowerSsn.ToString();
                txtCallID.Text = fcCase.CallId.ToString();
                //txtCaseCompleteInd.Text = fcCase.CaseCompleteInd.ToString();
                txtCaseSourceCd.Text = fcCase.CaseSourceCd.ToString();
                txtCoBorrowerDisabledInd.Text = fcCase.CoBorrowerDisabledInd.ToString();
                txtCoBorrowerDOB.Text = fcCase.CoBorrowerDob.ToString();
                txtCoBorrowerFName.Text = fcCase.CoBorrowerFname.ToString();
                //txtCoBorrowerLast4SSN.Text = fcCase.CoBorrowerLast4Ssn.ToString();
                txtCoBorrowerLName.Text = fcCase.CoBorrowerLname.ToString();
                txtCoBorrowerMName.Text = fcCase.CoBorrowerMname.ToString();
                txtCoBorrowerOccupationCd.Text = fcCase.CoBorrowerOccupationCd.ToString();
                txtCoBorrowerSSN.Text = fcCase.CoBorrowerSsn.ToString();
                //txtCompletedDt.Text = fcCase.CompletedDt.ToString();
                txtContactAddress1.Text = fcCase.ContactAddr1.ToString();
                txtContactAddress2.Text = fcCase.ContactAddr2.ToString();
                txtContactCity.Text = fcCase.ContactCity.ToString();
                txtContactedSrvcrRecentlyInd.Text = fcCase.ContactedSrvcrRecentlyInd.ToString();
                txtContactStateCd.Text = fcCase.ContactStateCd.ToString();
                txtContactZip.Text = fcCase.ContactZip.ToString();
                txtContactZipPlus4.Text = fcCase.ContactZipPlus4.ToString();
                txtCounselingDurationCd.Text = fcCase.CounselingDurationCd.ToString();
                txtCounselorEmail.Text = fcCase.CounselorEmail.ToString();
                txtCounselorExt.Text = fcCase.CounselorExt.ToString();
                txtCounselorFirstName.Text = fcCase.CounselorFname.ToString();
                txtCounselorLastName.Text = fcCase.CounselorLname.ToString();
                txtCounselorPhone.Text = fcCase.CounselorPhone.ToString();
                txtDfltReason1stCd.Text = fcCase.DfltReason1stCd.ToString();
                txtDfltReason2ndCd.Text = fcCase.DfltReason2ndCd.ToString();
                txtDiscussedSolutionWithSrvcrInd.Text = fcCase.DiscussedSolutionWithSrvcrInd.ToString();
                //txtDoNotCallInd.Text = fcCase.DoNotCallInd.ToString();
                //txtDuplicateInd.Text = fcCase.DuplicateInd.ToString();
                txtEmail1.Text = fcCase.Email1.ToString();
                txtEmail2.Text = fcCase.Email2.ToString();
                //txtFcID.Text = fcCase.FcId.ToString();
                txtFcNoticeReceivedInd.Text = fcCase.FcNoticeReceiveInd.ToString();
                //txtFcSaleDateSetInd.Text = fcCase.FcSaleDateSetInd.ToString();
                txtFollowupNotes.Text = fcCase.FollowupNotes.ToString();
                txtForSaleInd.Text = fcCase.ForSaleInd.ToString();
                txtFundingConsentInd.Text = fcCase.FundingConsentInd.ToString();
                txtGenderCd.Text = fcCase.GenderCd.ToString();
                txtHasWorkoutPlanInd.Text = fcCase.HasWorkoutPlanInd.ToString();
                txtHispanicInd.Text = fcCase.HispanicInd.ToString();
                txtHomeCurMktValue.Text = fcCase.HomeCurrentMarketValue.ToString();
                txtHomePurchasePrice.Text = fcCase.HomePurchasePrice.ToString();
                txtHomePurchaseYear.Text = fcCase.HomePurchaseYear.ToString();
                txtHomeSalePrice.Text = fcCase.HomeSalePrice.ToString();
                txtHouseholdCd.Text = fcCase.HouseholdCd.ToString();
                txtHousholdGrossAnnualIncomeAmt.Text = fcCase.HouseholdGrossAnnualIncomeAmt.ToString();
                //txtHpfMediaCandidateInd.Text = fcCase.HpfMediaCandidateInd.ToString();
                //txtHpfNetworkCandidateInd.Text = fcCase.HpfNetworkCandidateInd.ToString();
                //txtHpfSuccessStoryInd.Text = fcCase.HpfSuccessStoryInd.ToString();
                txtHudOutcomeCd.Text = fcCase.HudOutcomeCd.ToString();
                txtHudTerminationDt.Text = fcCase.HudTerminationDt.ToString();
                txtHudTerminationReasonCd.Text = fcCase.HudTerminationReasonCd.ToString();
                txtIncomeEarnersCd.Text = fcCase.IncomeEarnersCd.ToString();
                txtIntakeCreditBureauCd.Text = fcCase.IntakeCreditBureauCd.ToString();
                txtIntakeCreditScore.Text = fcCase.IntakeCreditScore.ToString();
                txtIntakeDt.Text = fcCase.IntakeDt.ToString();
                txtLoanDfltReasonNotes.Text = fcCase.LoanDfltReasonNotes.ToString();
                //txtLoanList.Text = fcCase.LoanList.ToString();
                txtMilitaryServiceCd.Text = fcCase.MilitaryServiceCd.ToString();
                txtMotherMaidenLName.Text = fcCase.MotherMaidenLname.ToString();
                //txtNeverBillReasonCd.Text = fcCase.NeverBillReasonCd.ToString();
                //txtNeverPayReasonCd.Text = fcCase.NeverPayReasonCd.ToString();
                txtOccupantNum.Text = fcCase.OccupantNum.ToString();
                //txtOptOutNewsletterInd.Text = fcCase.OptOutNewsletterInd.ToString();
                //txtOptOutSurveyInd.Text = fcCase.OptOutSurveyInd.ToString();
                txtOwnerOccupiedInd.Text = fcCase.OwnerOccupiedInd.ToString();
                txtPrimaryContactNo.Text = fcCase.PrimaryContactNo.ToString();
                txtPrimaryResidenceInd.Text = fcCase.PrimaryResidenceInd.ToString();
                txtPrimResEstMktValue.Text = fcCase.PrimResEstMktValue.ToString();
                txtProgramID.Text = fcCase.ProgramId.ToString();
                txtPropertyAddress1.Text = fcCase.PropAddr1.ToString();
                txtPropertyAddress2.Text = fcCase.PropAddr2.ToString();
                txtPropertyCity.Text = fcCase.PropCity.ToString();
                txtPropertyCd.Text = fcCase.PropertyCd.ToString();
                txtPropertyStateCd.Text = fcCase.PropStateCd.ToString();
                txtPropertyZip.Text = fcCase.PropZip.ToString();
                txtPropertyZipPlus4.Text = fcCase.PropZipPlus4.ToString();
                txtRaceCd.Text = fcCase.RaceCd.ToString();
                txtRealtyCompany.Text = fcCase.RealtyCompany.ToString();
                txtSecondContactNo.Text = fcCase.SecondContactNo.ToString();
                txtServicerConsentInd.Text = fcCase.ServicerConsentInd.ToString();
                txtSrvcrWorkoutPlanInd.Text = fcCase.SrvcrWorkoutPlanCurrentInd.ToString();
                //txtSummarySentDt.Text = fcCase.SummarySentDt.ToString();
                txtSummarySentOtherCd.Text = fcCase.SummarySentOtherCd.ToString();
                txtSummarySentOtherDt.Text = fcCase.SummarySentOtherDt.ToString();
                txtWorkedWithAnotherAgencyInd.Text = fcCase.WorkedWithAnotherAgencyInd.ToString();
                txtFcSaleDate.Text = fcCase.FcSaleDate.ToString();
                txtWorkingUserID.Text = fcCase.CreateUserId.ToString();
                //txtCreateUserID.Text = fcCase.CreateUserId.ToString();
                //txtChangeLastUserID.Text = fcCase.ChangeLastUserId.ToString();
            }
        }
       

        #region Case Loan
        private void grdvCaseLoanBinding()
        {
            List<CaseLoanDTO> caseLoans;
            if (Session[SessionVariables.CASE_LOAN_COLLECTION] != null)
            {
                caseLoans = (List<CaseLoanDTO>)Session[SessionVariables.CASE_LOAN_COLLECTION];
                grdvCaseLoan.DataSource = caseLoans;
                grdvCaseLoan.DataBind();
            }
            else
            {
                caseLoans = new List<CaseLoanDTO>();
                caseLoans.Add(new CaseLoanDTO());
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
            CaseLoanDTO caseLoan = RowToCaseLoanDTO(grdvCaseLoan.Rows[e.RowIndex]);
            if (Session[SessionVariables.CASE_LOAN_COLLECTION] != null)
            {
                List<CaseLoanDTO> caseLoans = (List<CaseLoanDTO>)Session[SessionVariables.CASE_LOAN_COLLECTION];
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
            int caseLoanId = Util.ConvertToInt(((Label)grdvCaseLoan.Rows[e.RowIndex].FindControl("lblCaseLoanId")).Text);
            if (Session[SessionVariables.CASE_LOAN_COLLECTION] != null)
            {
                List<CaseLoanDTO> caseLoans = (List<CaseLoanDTO>)Session[SessionVariables.CASE_LOAN_COLLECTION];
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

        protected void grdvCaseLoan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                CaseLoanDTO caseLoan = RowToCaseLoanDTO(grdvCaseLoan.FooterRow);

                List<CaseLoanDTO> caseLoans = new List<CaseLoanDTO>();
                if (Session[SessionVariables.CASE_LOAN_COLLECTION] != null)
                {
                     caseLoans = (List<CaseLoanDTO>)Session[SessionVariables.CASE_LOAN_COLLECTION];
                    int caseLoanId = caseLoans.Max(item => item.CaseLoanId);
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
       

        private CaseLoanDTO RowToCaseLoanDTO(GridViewRow row)
        {
            CaseLoanDTO caseLoan = new CaseLoanDTO();

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
            Label lblCaseLoanId = (Label)row.FindControl("lblCaseLoanId");
            #endregion

            caseLoan.AcctNum = txtAccNum.Text.Trim();
            //caseLoan.ArmLoanInd = txtArmLoanInd.Text.Trim();
            caseLoan.ArmResetInd = txtArmResetInd.Text.Trim();
            caseLoan.CaseLoanId = Util.ConvertToInt(lblCaseLoanId.Text.Trim());
            caseLoan.CurrentLoanBalanceAmt = Util.ConvertToDouble(txtCurrentLoanBalanceAmt.Text.Trim());
            caseLoan.CurrentServicerNameTbd = txtCurrentServiceNameTBD.Text.Trim();
            caseLoan.FdicNcusNumCurrentServicerTbd = txtFDICNCUANum.Text.Trim();
            //caseLoan.InvestorLoanNum = txtFreddieLoanNum.Text.Trim();
            //caseLoan.FcId = Util.ConvertToInt(txtFcId.Text.Trim());
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

            //caseLoan.CreateUserId = txtCreateUserID.Text.Trim();
            //caseLoan.ChangeLastUserId = txtChangeLastUserID.Text.Trim();
            return caseLoan;

        }
        #endregion

        #region Budget Item
        private void grdvBudgetItemBinding()
        {
            List<BudgetItemDTO> budgetItems;
            if (Session[SessionVariables.BUDGET_ITEM_COLLECTION] != null)
            {
                budgetItems = (List<BudgetItemDTO>)Session[SessionVariables.BUDGET_ITEM_COLLECTION];
                grdvBudgetItem.DataSource = budgetItems;
                grdvBudgetItem.DataBind();
            }
            else
            {
                budgetItems = new List<BudgetItemDTO>();
                budgetItems.Add(new BudgetItemDTO());
                grdvBudgetItem.DataSource = budgetItems;
                grdvBudgetItem.DataBind();

                int TotalColumns = grdvBudgetItem.Rows[0].Cells.Count;
                grdvBudgetItem.Rows[0].Cells.Clear();
                grdvBudgetItem.Rows[0].Cells.Add(new TableCell());
                grdvBudgetItem.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grdvBudgetItem.Rows[0].Cells[0].Text = "No Records Found";
            }    
        }
        private BudgetItemDTO RowToBudgetItemDTO(GridViewRow row)
        {
            TextBox txtBudgetItemAmt = (TextBox)row.FindControl("txtBudgetItemAmt");
            TextBox txtBudgetNote = (TextBox)row.FindControl("txtBudgetNote");
            TextBox txtBudgetSubcategoryId = (TextBox)row.FindControl("txtBudgetSubcategoryId");
            //TextBox txtBudgetItemId = (TextBox)row.FindControl("txtBudgetItemId");
            Label lblBudgetItemId = (Label)row.FindControl("lblBudgetItemId");
            //TextBox txtBudgetItemSetId = (TextBox)row.FindControl("txtBudgetItemSetId");

            BudgetItemDTO budgetItem = new BudgetItemDTO();
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
                BudgetItemDTO budgetItem = RowToBudgetItemDTO(grdvBudgetItem.FooterRow);

                List<BudgetItemDTO> budgetItems = new List<BudgetItemDTO>();
                if (Session[SessionVariables.BUDGET_ITEM_COLLECTION] != null)
                {
                    budgetItems = (List<BudgetItemDTO>)Session[SessionVariables.BUDGET_ITEM_COLLECTION];
                    int budgetItemId = budgetItems.Max(item => item.BudgetItemId);
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
            int budgetItemId = Util.ConvertToInt(((Label)grdvBudgetItem.Rows[e.RowIndex].FindControl("lblBudgetItemId")).Text);
            if (Session[SessionVariables.BUDGET_ITEM_COLLECTION] != null)
            {
                List<BudgetItemDTO> budgetItems = (List<BudgetItemDTO>)Session[SessionVariables.BUDGET_ITEM_COLLECTION];
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
            BudgetItemDTO budgetItem = RowToBudgetItemDTO(grdvBudgetItem.Rows[e.RowIndex]);
            if (Session[SessionVariables.BUDGET_ITEM_COLLECTION] != null)
            {
                List<BudgetItemDTO> budgetItems = (List<BudgetItemDTO>)Session[SessionVariables.BUDGET_ITEM_COLLECTION];
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

        #region Budget Asset
        private void grdvBudgetAssetBinding()
        {

            List<BudgetAssetDTO> budgetAssets;
            if (Session[SessionVariables.BUDGET_ASSET_COLLECTION] != null)
            {
                budgetAssets = (List<BudgetAssetDTO>)Session[SessionVariables.BUDGET_ASSET_COLLECTION];
                grdvBudgetAsset.DataSource = budgetAssets;
                grdvBudgetAsset.DataBind();
            }
            else
            {
                budgetAssets = new List<BudgetAssetDTO>();
                budgetAssets.Add(new BudgetAssetDTO());
                grdvBudgetAsset.DataSource = budgetAssets;
                grdvBudgetAsset.DataBind();

                int TotalColumns = grdvBudgetAsset.Rows[0].Cells.Count;
                grdvBudgetAsset.Rows[0].Cells.Clear();
                grdvBudgetAsset.Rows[0].Cells.Add(new TableCell());
                grdvBudgetAsset.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grdvBudgetAsset.Rows[0].Cells[0].Text = "No Records Found";
            }    
        }

        private BudgetAssetDTO RowToBudgetAssetDTO(GridViewRow row)
        {
            TextBox txtAssetName = (TextBox)row.FindControl("txtAssetName");
            TextBox txtAssetValue = (TextBox)row.FindControl("txtAssetValue");
            //TextBox txtBudgetAssetID = (TextBox)row.FindControl("txtBudgetAssetID");
            //TextBox txtBudgetID = (TextBox)row.FindControl("txtBudgetID");
            Label lblBudgetAssetID = (Label)row.FindControl("lblBudgetAssetID");

            BudgetAssetDTO budgetAsset = new BudgetAssetDTO();
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
                BudgetAssetDTO budgetAsset = RowToBudgetAssetDTO(grdvBudgetAsset.FooterRow);

                List<BudgetAssetDTO> budgetAssets = new List<BudgetAssetDTO>();
                if (Session[SessionVariables.BUDGET_ASSET_COLLECTION] != null)
                {
                    budgetAssets = (List<BudgetAssetDTO>)Session[SessionVariables.BUDGET_ASSET_COLLECTION];
                    int budgetAssetId = budgetAssets.Max(item => item.BudgetAssetId);
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
            int budgetAssetId = Util.ConvertToInt(((Label)grdvBudgetAsset.Rows[e.RowIndex].FindControl("lblBudgetAssetId")).Text);
            if (Session[SessionVariables.BUDGET_ASSET_COLLECTION] != null)
            {
                List<BudgetAssetDTO> budgetAssets = (List<BudgetAssetDTO>)Session[SessionVariables.BUDGET_ASSET_COLLECTION];
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
            BudgetAssetDTO budgetAsset = RowToBudgetAssetDTO(grdvBudgetAsset.Rows[e.RowIndex]);
            if (Session[SessionVariables.BUDGET_ASSET_COLLECTION] != null)
            {
                List<BudgetAssetDTO> budgetAssets = (List<BudgetAssetDTO>)Session[SessionVariables.BUDGET_ASSET_COLLECTION];
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

        #region Activity Log

        private void grdvActivityLogBinding()
        {
            List<ActivityLogDTO> activityLogs;
            if (Session[SessionVariables.ACTIVITY_LOG_COLLECTION] != null)
            {
                activityLogs = (List<ActivityLogDTO>)Session[SessionVariables.ACTIVITY_LOG_COLLECTION];
                grdvActivityLog.DataSource = activityLogs;
                grdvActivityLog.DataBind();
            }
            else
            {
                activityLogs = new List<ActivityLogDTO>();
                activityLogs.Add(new ActivityLogDTO());
                grdvActivityLog.DataSource = activityLogs;
                grdvActivityLog.DataBind();

                int TotalColumns = grdvActivityLog.Rows[0].Cells.Count;
                grdvActivityLog.Rows[0].Cells.Clear();
                grdvActivityLog.Rows[0].Cells.Add(new TableCell());
                grdvActivityLog.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grdvActivityLog.Rows[0].Cells[0].Text = "No Records Found";
            }           
        }
        private ActivityLogDTO RowToActivityLogDTO(GridViewRow row)
        {
            TextBox txtFcId = (TextBox)row.FindControl("txtFcId");
            TextBox txtActivityCode = (TextBox)row.FindControl("txtActivityCode");
            TextBox txtActivityDt = (TextBox)row.FindControl("txtActivityDt");
            TextBox txtActivityLogId = (TextBox)row.FindControl("txtActivityLogId");
            TextBox txtActivityNote = (TextBox)row.FindControl("txtActivityNote");

            ActivityLogDTO activityLog = new ActivityLogDTO();
            activityLog.FcId = Util.ConvertToInt(txtFcId.Text.Trim());
            activityLog.ActivityCd = txtActivityCode.Text.Trim();
            activityLog.ActivityDt = Util.ConvertToDateTime(txtActivityDt.Text.Trim());
            activityLog.ActivityLogId = Util.ConvertToInt(txtActivityLogId.Text.Trim());
            activityLog.ActivityNote = txtActivityNote.Text.Trim();
            return activityLog;
        }

        protected void grdvActivityLog_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            grdvActivityLog.EditIndex = -1;
            grdvActivityLogBinding();
        }

        protected void grdvActivityLogRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                ActivityLogDTO activityLog = RowToActivityLogDTO(grdvActivityLog.FooterRow);

                List<ActivityLogDTO> activityLogs = new List<ActivityLogDTO>();
                if (Session[SessionVariables.ACTIVITY_LOG_COLLECTION] != null)
                {
                    activityLogs = (List<ActivityLogDTO>)Session[SessionVariables.ACTIVITY_LOG_COLLECTION];
                    int activityLogId = activityLogs.Max(item => item.ActivityLogId);
                    activityLog.ActivityLogId = activityLogId + 1;
                }
                else
                {
                    activityLog.ActivityLogId = 1;
                }

                activityLogs.Add(activityLog);
                Session[SessionVariables.ACTIVITY_LOG_COLLECTION] = activityLogs;
                grdvActivityLogBinding();
            } 
        }

        protected void grdvActivityLog_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int activityLogId = Util.ConvertToInt(((Label)grdvActivityLog.Rows[e.RowIndex].FindControl("lblActivityLogId")).Text);
            if (Session[SessionVariables.ACTIVITY_LOG_COLLECTION] != null)
            {
                List<ActivityLogDTO> activityLogs = (List<ActivityLogDTO>)Session[SessionVariables.ACTIVITY_LOG_COLLECTION];
                int index = activityLogs.FindIndex(item => item.ActivityLogId == activityLogId);

                if (index < 0)
                {
                    //can not Delete item
                }
                else
                {
                    activityLogs.RemoveAt(index);
                    Session[SessionVariables.ACTIVITY_LOG_COLLECTION] = activityLogs;
                }

                grdvActivityLog.EditIndex = -1;
                grdvActivityLogBinding();
            }
            else
            {
                //can not Delete item
            }
        }

        protected void grdvActivityLog_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvActivityLog.EditIndex = e.NewEditIndex;
            grdvActivityLogBinding();
        }

        protected void grdvActivityLog_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ActivityLogDTO activityLog = RowToActivityLogDTO(grdvActivityLog.Rows[e.RowIndex]);
            if (Session[SessionVariables.ACTIVITY_LOG_COLLECTION] != null)
            {
                List<ActivityLogDTO> activityLogs = (List<ActivityLogDTO>)Session[SessionVariables.ACTIVITY_LOG_COLLECTION];
                int index = activityLogs.FindIndex(item => item.ActivityLogId == activityLog.ActivityLogId);

                if (index < 0)
                {
                    //can not update List
                }
                else
                {
                    activityLogs[index] = activityLog;
                    Session[SessionVariables.ACTIVITY_LOG_COLLECTION] = activityLogs;
                }


                grdvActivityLog.EditIndex = -1;
                grdvActivityLogBinding();
            }
            else
            {
                //can not update List
            }
        }
        #endregion

        #region Outcome 
        private void grdvOutcomeItemBinding()
        {
            List<OutcomeItemDTO> outcomeItems;
            if (Session[SessionVariables.OUTCOME_ITEM_COLLECTION] != null)
            {
                outcomeItems = (List<OutcomeItemDTO>)Session[SessionVariables.OUTCOME_ITEM_COLLECTION];
                grdvOutcomeItem.DataSource = outcomeItems;
                grdvOutcomeItem.DataBind();
            }
            else                    
            {   
                outcomeItems = new List<OutcomeItemDTO>();
                outcomeItems.Add(new OutcomeItemDTO());
                grdvOutcomeItem.DataSource = outcomeItems;
                grdvOutcomeItem.DataBind();

                int TotalColumns = grdvOutcomeItem.Rows[0].Cells.Count;
                grdvOutcomeItem.Rows[0].Cells.Clear();
                grdvOutcomeItem.Rows[0].Cells.Add(new TableCell());
                grdvOutcomeItem.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grdvOutcomeItem.Rows[0].Cells[0].Text = "No Records Found";
            }
        }
        private OutcomeItemDTO RowToOutcomeItemDTO(GridViewRow row)
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

            OutcomeItemDTO outcomeItem = new OutcomeItemDTO();
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
                OutcomeItemDTO outcomeItem = RowToOutcomeItemDTO(grdvOutcomeItem.FooterRow);

                List<OutcomeItemDTO> outcomeItems = new List<OutcomeItemDTO>();
                if (Session[SessionVariables.OUTCOME_ITEM_COLLECTION] != null)
                {
                    outcomeItems = (List<OutcomeItemDTO>)Session[SessionVariables.OUTCOME_ITEM_COLLECTION];
                    int outcomeItemId = outcomeItems.Max(item => item.OutcomeItemId);
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
            int outcomeItemId = Util.ConvertToInt(((Label)grdvOutcomeItem.Rows[e.RowIndex].FindControl("lblOutcomeItemId")).Text);
            if (Session[SessionVariables.OUTCOME_ITEM_COLLECTION] != null)
            {
                List<OutcomeItemDTO> outcomeItems = (List<OutcomeItemDTO>)Session[SessionVariables.OUTCOME_ITEM_COLLECTION];
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
            OutcomeItemDTO outcomeItem = RowToOutcomeItemDTO(grdvOutcomeItem.Rows[e.RowIndex]);
            if (Session[SessionVariables.OUTCOME_ITEM_COLLECTION] != null)
            {
                List<OutcomeItemDTO> outcomeItems = (List<OutcomeItemDTO>)Session[SessionVariables.OUTCOME_ITEM_COLLECTION];
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
                    lblMessage.Text = "Congratulation - New FcId is " + response.FcId;                    
                    grdvMessages.Visible = true;
                    grdvMessages.DataSource = response.Messages;
                    grdvMessages.DataBind();
                }
                else
                {
                    lblMessage.Text = "Error Message: ";
                    grdvMessages.Visible = true;
                    grdvMessages.DataSource = response.Messages;
                    grdvMessages.DataBind();
                }
            }
            else
            {                
                    grdvMessages.Visible = false;
                    lblMessage.Text = "Congratulation - New FcId is " + response.FcId;                
            }
        }

        private void SetTrackingInformation(ForeclosureCaseSetDTO fcCaseSet)
        {
            string createUserId = txtWorkingUserID.Text.Trim();  //txtCreateUserID.Text.Trim();
            string changeLastUserId = txtWorkingUserID.Text.Trim();// txtChangeLastUserID.Text.Trim();
            fcCaseSet.ForeclosureCase.CreateUserId = createUserId;
            fcCaseSet.ForeclosureCase.ChangeLastUserId = changeLastUserId;

            foreach (BudgetAssetDTO obj in fcCaseSet.BudgetAssets)
            {
                obj.CreateUserId = createUserId;
                obj.ChangeLastUserId = changeLastUserId;
            }
            foreach (BudgetItemDTO obj in fcCaseSet.BudgetItems)
            {
                obj.CreateUserId = createUserId;
                obj.ChangeLastUserId = changeLastUserId;
            }
            foreach (OutcomeItemDTO obj in fcCaseSet.Outcome)
            {
                obj.CreateUserId = createUserId;
                obj.ChangeLastUserId = changeLastUserId;
            }            
            foreach (CaseLoanDTO obj in fcCaseSet.CaseLoans)
            {
                obj.CreateUserId = createUserId;
                obj.ChangeLastUserId = changeLastUserId;
            }
            
        }
        private ForeclosureCaseSaveRequest CreateForeclosureCaseSaveRequest()
        {
            ForeclosureCaseSaveRequest request = new ForeclosureCaseSaveRequest();
            ForeclosureCaseSetDTO fcCaseSet = new ForeclosureCaseSetDTO();
            fcCaseSet.WorkingUserID = txtWorkingUserID.Text.Trim();

            fcCaseSet.ForeclosureCase = FormToForeclosureCase();
            //fcCaseSet.ActivityLog = ((List<ActivityLogDTO>)Session[SessionVariables.ACTIVITY_LOG_COLLECTION]).ToArray();
            if (Session[SessionVariables.BUDGET_ASSET_COLLECTION] != null)
                fcCaseSet.BudgetAssets = ((List<BudgetAssetDTO>)Session[SessionVariables.BUDGET_ASSET_COLLECTION]).ToArray();
            else
                fcCaseSet.BudgetAssets = null;

            if (Session[SessionVariables.BUDGET_ITEM_COLLECTION] != null)
                fcCaseSet.BudgetItems = ((List<BudgetItemDTO>)Session[SessionVariables.BUDGET_ITEM_COLLECTION]).ToArray();
            else
                fcCaseSet.BudgetItems = null;

            if (Session[SessionVariables.CASE_LOAN_COLLECTION] != null)
                fcCaseSet.CaseLoans = ((List<CaseLoanDTO>)Session[SessionVariables.CASE_LOAN_COLLECTION]).ToArray();
            else
                fcCaseSet.CaseLoans = null;

            if (Session[SessionVariables.OUTCOME_ITEM_COLLECTION] != null)
                fcCaseSet.Outcome = ((List<OutcomeItemDTO>)Session[SessionVariables.OUTCOME_ITEM_COLLECTION]).ToArray();
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
            catch
            {
                ExceptionMessage em = new ExceptionMessage();
                List<ExceptionMessage> exList = new List<ExceptionMessage>();
                em.Message = "Invalid XML format";
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
        }
    }
}
