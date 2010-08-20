﻿using System;
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
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common;

namespace HPF.FutureState.Web
{
    public partial class QCSelectionCaseInfo : System.Web.UI.Page
    {
        string UCLOCATION = "QCSelectionCaseDetail\\";
        private CaseEvalSearchResultDTO selectionCase = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMessage();
                tabControl2.TabClick += new HPF.FutureState.Web.HPFWebControls.TabControlEventHandler(tabControl_TabClick);
                BindData();
                if (!IsPostBack)
                {
                    tabControl2.AddTab("agencyAudit", "Agency Audit");
                    tabControl2.AddTab("hpfAudit", "HPF Audit");
                    tabControl2.AddTab("compareResult", "Compare Result");
                    tabControl2.AddTab("fileUploads", "File Upload");
                }
                LoadDefaultTab();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(ex.Message);
            }
        }
        private void LoadDefaultTab()
        {
            if (string.Compare(HPFWebSecurity.CurrentIdentity.UserType, Constant.USER_TYPE_AGENCY) == 0)
            {
                if (string.Compare(selectionCase.EvalStatus, CaseEvaluationBL.EvaluationStatus.AGENCY_UPLOAD_REQUIRED)==0)
                {
                    tabControl2.SelectedTab = "fileUploads";
                    UserControlLoader2.LoadUserControl(UCLOCATION + "FileUploads.ascx", "ucFileUploads");
                    return;
                }
                tabControl2.SelectedTab = "agencyAudit";
                UserControlLoader2.LoadUserControl(UCLOCATION + "AgencyAudit.ascx", "ucAgencyAudit");
                return;
            }
            else if ((string.Compare(HPFWebSecurity.CurrentIdentity.UserType, Constant.USER_TYPE_HPF) == 0)
                     && (string.Compare(selectionCase.EvalStatus, CaseEvaluationBL.EvaluationStatus.AGENCY_INPUT_REQUIRED) != 0)
                     && (string.Compare(selectionCase.EvalStatus, CaseEvaluationBL.EvaluationStatus.AGENCY_UPLOAD_REQUIRED) != 0))
            {
                tabControl2.SelectedTab = "hpfAudit";
                UserControlLoader2.LoadUserControl(UCLOCATION + "AgencyAudit.ascx", "ucHpfAudit");
            }
        }
        private void BindData()
        {
            ClearErrorMessage();
            if (Request.QueryString["caseId"] == null)
                return;
            int caseId = int.Parse(Request.QueryString["caseId"].ToString());
            selectionCase = GetSelectionCase(caseId);
            BindSelectionCaseToUI(selectionCase);

        }
        private CaseEvalSearchResultDTO GetSelectionCase(int caseId)
        {
            return CaseEvaluationBL.Instance.SearchCaseEvalByFcId(caseId);
        }
        private void BindSelectionCaseToUI(CaseEvalSearchResultDTO caseSelection)
        {
            lblHpfID.Text = caseSelection.FcId.ToString();
            lblAgencyName.Text = caseSelection.AgencyName;
            lblHomeOwner.Text = caseSelection.HomeowenerFirstName + " " + caseSelection.HomeowenerLastName;
            lblEvaluationStatus.Text = caseSelection.EvalStatus;
            lblLoanNumber.Text = caseSelection.LoanNumber;
            lblServicerName.Text = caseSelection.ServicerName;
            lblZipCode.Text = caseSelection.ZipCode;
            lblCallDate.Text = caseSelection.CallDate.ToString();
            lblCounselor.Text = caseSelection.CounselorName;
        }
        private void AddErrorMessage(string errMes)
        {
            lblErrorMessage.Items.Add(new ListItem(errMes));
        }
        private void ClearErrorMessage()
        {
            lblErrorMessage.Items.Clear();
        }
        //display message when you click out casedetail tab
        void tabControl_TabClick(object sender, HPF.FutureState.Web.HPFWebControls.TabControlEventArgs e)
        {
            try
            {
                bool enableAgencyAuditTab = ((string.Compare(selectionCase.EvalStatus, CaseEvaluationBL.EvaluationStatus.AGENCY_UPLOAD_REQUIRED) != 0) 
                                            && (string.Compare(HPFWebSecurity.CurrentIdentity.UserType, Constant.USER_TYPE_AGENCY) == 0));
                bool enableHPFAuditTab = ((string.Compare(selectionCase.EvalStatus, CaseEvaluationBL.EvaluationStatus.AGENCY_INPUT_REQUIRED) != 0)
                                            && (string.Compare(HPFWebSecurity.CurrentIdentity.UserType, Constant.USER_TYPE_HPF) == 0));
                bool enableCompareResultTab = ((string.Compare(selectionCase.EvalStatus, CaseEvaluationBL.EvaluationStatus.RESULT_WITHIN_RANGE) == 0)
                                            || (string.Compare(selectionCase.EvalStatus, CaseEvaluationBL.EvaluationStatus.RECON_REQUIRED_AGENCY_INPUT) == 0)
                                            || (string.Compare(selectionCase.EvalStatus, CaseEvaluationBL.EvaluationStatus.RECON_REQUIRED_HPF_INPUT) == 0)
                                            || ((string.Compare(selectionCase.EvalStatus, CaseEvaluationBL.EvaluationStatus.CLOSED) == 0)
                                                && (string.Compare(HPFWebSecurity.CurrentIdentity.UserType, Constant.USER_TYPE_HPF) == 0)));
                bool enableFileUploadsTab = ((string.Compare(selectionCase.EvalStatus, CaseEvaluationBL.EvaluationStatus.AGENCY_INPUT_REQUIRED) != 0)
                                            || ((string.Compare(selectionCase.EvalStatus, CaseEvaluationBL.EvaluationStatus.AGENCY_UPLOAD_REQUIRED) == 0)
                                                && (string.Compare(HPFWebSecurity.CurrentIdentity.UserType, Constant.USER_TYPE_AGENCY) == 0))
                                            || ((string.Compare(selectionCase.EvalStatus, CaseEvaluationBL.EvaluationStatus.HPF_INPUT_REQUIRED) == 0)
                                                && (string.Compare(HPFWebSecurity.CurrentIdentity.UserType, Constant.USER_TYPE_AGENCY) == 0))
                                            || ((string.Compare(selectionCase.EvalStatus, CaseEvaluationBL.EvaluationStatus.RESULT_WITHIN_RANGE) == 0)
                                                && (string.Compare(HPFWebSecurity.CurrentIdentity.UserType, Constant.USER_TYPE_AGENCY) == 0)));
                                            
                switch (e.SelectedTabID)
                {
                    case "agencyAudit":
                        if (enableAgencyAuditTab)
                            UserControlLoader2.LoadUserControl(UCLOCATION + "AgencyAudit.ascx", "ucAgencyAudit");
                        break;
                    case "hpfAudit":
                        if (enableHPFAuditTab)
                            UserControlLoader2.LoadUserControl(UCLOCATION + "AgencyAudit.ascx", "ucHpfAudit");
                        break;
                    case "compareResult":
                        if (enableCompareResultTab)
                            UserControlLoader2.LoadUserControl(UCLOCATION + "CompareResult.ascx", "ucCompareResult");
                        break;
                    case "fileUploads":
                        if (enableFileUploadsTab)
                            UserControlLoader2.LoadUserControl(UCLOCATION + "FileUploads.ascx", "ucFileUploads");
                        break;
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }

        }
    }
}
