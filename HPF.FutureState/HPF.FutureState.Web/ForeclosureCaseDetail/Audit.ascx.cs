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


using System.Collections.ObjectModel;
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;

using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.Web.ForeclosureCaseDetail
{
    public partial class Audit : System.Web.UI.UserControl
    {
        
        private const string ACTION_UPDATE = "update";
        private const string ACTION_INSERT = "insert";

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {                               
                ApplySecurity();
                if (IsPostBack)
                {
                    BindingDataToGrdvCaseAudit();

                    BindDataToIndicatorDropDownLists();
                    BindDataToReviewedByDDL();
                    BindDataToAuditTypeDDL();
                    BindDataToAuditFailureReasonDDL();
                    
                    hfAction.Value = ACTION_INSERT;
                    btnCancel.Attributes.Add("onclick", "return ConfirmToCancel();");
                }
                
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex);
            }
        }
       
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DoSaving();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (hfDoSaving.Value == Constant.INDICATOR_YES_FULL)
                DoSaving();

            ClearPage();
        }
        
        private void btnEdit_Click(object sender, EventArgs e)
        {
            Button btnEdit = sender as Button;

            if (hfDoSaving.Value == Constant.INDICATOR_YES_FULL)
                DoSaving();

            int index = int.Parse(btnEdit.CommandArgument.ToString());
            grdvCaseAudit.SelectedIndex = index;
            CaseAuditDTO caseAudit = ((CaseAuditDTOCollection)grdvCaseAudit.DataSource)[index];
            Session[Constant.SS_CASE_AUDIT_OBJECT] = caseAudit;
            CaseAuditDTOToForm(caseAudit);
            hfAction.Value = ACTION_UPDATE;
            lblFormTitle.Text = "Audit detail - Editing";

        }
        
        protected void btnNew_Click(object sender, EventArgs e)
        {            
            ClearPage();
            hfAction.Value = ACTION_INSERT;
            lblFormTitle.Text = "Audit detail - Inserting";
        }
        
        protected void grdvCaseAudit_RowCreated(object sender, GridViewRowEventArgs e)
        {
            int idxIdColumn = 0;
            e.Row.Cells[idxIdColumn].Visible = false;

            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // View/Edit button on GridRow
                int idxCommandFieldColumn = 8;
                Button btnEdit = e.Row.Cells[idxCommandFieldColumn].FindControl("btnEdit") as Button;
                if (btnEdit != null)
                {
                    btnEdit.Attributes.Add("onclick", "return ConfirmToCancel();");
                    btnEdit.Click += new EventHandler(btnEdit_Click);
                    btnEdit.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }
        
        
        #region helper

        private void DoSaving()
        {
            try
            {
                CaseAuditDTO caseAudit = FormToCaseAuditDTO();
                if (!string.IsNullOrEmpty(hfAction.Value))
                {
                    if (hfAction.Value == ACTION_UPDATE)
                        CaseAuditBL.Instance.SaveCaseAudit(caseAudit, HPFWebSecurity.CurrentIdentity.LoginName, true);
                    else
                        CaseAuditBL.Instance.SaveCaseAudit(caseAudit, HPFWebSecurity.CurrentIdentity.LoginName, false);

                    BindingDataToGrdvCaseAudit();
                    ClearPage();
                }
            }
            catch (DataValidationException ex)
            {
                errorList.DataSource = ex.ExceptionMessages;
                errorList.DataBind();
            }            
        }

        private CaseAuditDTOCollection RetrieveCaseAudits(int fcid)
        {
            return CaseAuditBL.Instance.RetrieveCaseAudits(fcid);
        }

        private void BindingDataToGrdvCaseAudit()
        {
            int caseID = 0;
            int.TryParse(Request.QueryString["CaseID"].ToString(), out caseID);
            CaseAuditDTOCollection caseAudits = RetrieveCaseAudits(caseID);
            if (caseAudits.Count > 0)
            {
                grdvCaseAudit.DataSource = caseAudits;
                grdvCaseAudit.DataBind();
            }
            else
            {                
                caseAudits.Add(new CaseAuditDTO());

                grdvCaseAudit.DataSource = caseAudits;
                grdvCaseAudit.DataBind();

                int TotalColumns = grdvCaseAudit.Rows[0].Cells.Count;
                grdvCaseAudit.Rows[0].Cells.Clear();
                grdvCaseAudit.Rows[0].Cells.Add(new TableCell());
                grdvCaseAudit.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grdvCaseAudit.Rows[0].Cells[0].Text = "No Records Found";
            }
        }

        private void BindDataToIndicatorDropDownLists()
        {
            BindIndicatorToDDL(ddlAppropriateOutcome);
            BindIndicatorToDDL(ddlCompliant);
            BindIndicatorToDDL(ddlReasonForDefault);
            BindIndicatorToDDL(ddlBudgetCompleted);
            BindIndicatorToDDL(ddlClientActionPlan);
            BindIndicatorToDDL(ddlVerbalPrivacyConsent);
            BindIndicatorToDDL(ddlWrittenPrivacyConsent);
        }
        
        private void BindIndicatorToDDL(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem(string.Empty));
            ddl.Items.Add(new ListItem(Constant.INDICATOR_YES_FULL));
            ddl.Items.Add(new ListItem(Constant.INDICATOR_NO_FULL));
        }

        private void BindDataToAuditTypeDDL()
        {
            ddlAuditType.Items.Clear();
            
            RefCodeItemDTOCollection auditTypeCodes = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_AUDIT_TYPE_CODE);
            //ddlAuditType.DataValueField = "Code";
            ddlAuditType.DataValueField = "CodeDesc";
            ddlAuditType.DataTextField = "CodeDesc";
            ddlAuditType.DataSource = auditTypeCodes;
            ddlAuditType.DataBind();
            ddlAuditType.Items.Insert(0, new ListItem(string.Empty));
            

        }

        private void BindDataToReviewedByDDL()
        {
            ddlReviewedBy.Items.Clear();
            ddlReviewedBy.Items.Add(new ListItem(string.Empty));

            HPFUserDTOCollection hpfUsers = LookupDataBL.Instance.GetHpfUsers();
            //ddlReviewedBy.DataValueField = "HpfUserId";
            ddlReviewedBy.DataValueField = "FullName";
            ddlReviewedBy.DataTextField = "FullName";
            ddlReviewedBy.DataSource = hpfUsers;
            ddlReviewedBy.DataBind();

            ddlReviewedBy.Items.Insert(0, new ListItem(string.Empty));
        }

        private void BindDataToAuditFailureReasonDDL()
        {
            //the failure reason codes are not provided from client
            //not implemented yet
            ddlAuditFailureReason.Items.Clear();

            RefCodeItemDTOCollection failureReasonCodes = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_AUDIT_FAILURE_REASON_CODE);
            //ddlAuditFailureReason.DataValueField = "Code";
            ddlAuditFailureReason.DataValueField = "CodeDesc";
            ddlAuditFailureReason.DataTextField = "CodeDesc";
            ddlAuditFailureReason.DataSource = failureReasonCodes;
            ddlAuditFailureReason.DataBind();
            ddlAuditFailureReason.Items.Insert(0, new ListItem(string.Empty));
        }

        private void ClearPage()
        {
            //reset session
            Session[Constant.SS_CASE_AUDIT_OBJECT] = null;

            //clear controls
            foreach (DropDownList ddl in this.Controls.OfType<DropDownList>())
                ddl.Text = string.Empty;
            txtAuditDate.Text = null;
            txtAuditComment.Text = null;
            grdvCaseAudit.SelectedIndex = -1;

            hfAction.Value = ACTION_INSERT;
        }

        private CaseAuditDTO FormToCaseAuditDTO()
        {
            int? id = null;
            if (Session[Constant.SS_CASE_AUDIT_OBJECT] != null)
                id = ((CaseAuditDTO)Session[Constant.SS_CASE_AUDIT_OBJECT]).CaseAuditId;
            return new CaseAuditDTO(){
                CaseAuditId = id,
                AppropriateOutcomeInd = GetIndicatorShortValue(ddlAppropriateOutcome.SelectedValue),
                AuditComments = string.IsNullOrEmpty(txtAuditComment.Text.Trim()) ? null : txtAuditComment.Text,
                AuditDt = ConvertToDateTime(txtAuditDate.Text.Trim()),
                AuditFailureReasonCode = ddlAuditFailureReason.SelectedValue,
                AuditTypeCode = ddlAuditType.SelectedValue,
                BudgetCompletedInd = GetIndicatorShortValue(ddlBudgetCompleted.SelectedValue),
                ClientActionPlanInd = GetIndicatorShortValue(ddlClientActionPlan.SelectedValue),
                CompliantInd = GetIndicatorShortValue(ddlCompliant.SelectedValue),
                FcId = int.Parse(Request.QueryString["CaseID"].ToString()),
                ReasonForDefaultInd = GetIndicatorShortValue(ddlReasonForDefault.SelectedValue),
                ReviewedBy = ddlReviewedBy.SelectedValue,
                VerbalPrivacyConsentInd = GetIndicatorShortValue(ddlVerbalPrivacyConsent.SelectedValue),
                WrittenActionConsentInd = GetIndicatorShortValue(ddlWrittenPrivacyConsent.SelectedValue)           
            };
        }

        private void CaseAuditDTOToForm(CaseAuditDTO caseAudit)
        {         
            ddlAppropriateOutcome.Text = GetIndicatorLongValue(caseAudit.AppropriateOutcomeInd);
            txtAuditComment.Text = caseAudit.AuditComments;
            txtAuditDate.Text = (caseAudit.AuditDt.HasValue) ? caseAudit.AuditDt.Value.Date.ToString() : string.Empty;
            //ddlAuditFailureReason.SelectedIndex = ddlAuditFailureReason.Items.IndexOf(ddlAuditFailureReason.Items.FindByText(caseAudit.AuditFailureReasonCode));
            ddlAuditType.SelectedIndex = ddlAuditType.Items.IndexOf(ddlAuditType.Items.FindByText(caseAudit.AuditTypeCode));
            ddlBudgetCompleted.Text = GetIndicatorLongValue(caseAudit.BudgetCompletedInd);
            ddlClientActionPlan.Text = GetIndicatorLongValue(caseAudit.ClientActionPlanInd);
            ddlCompliant.Text = GetIndicatorLongValue(caseAudit.CompliantInd);
            ddlReasonForDefault.Text = GetIndicatorLongValue(caseAudit.ReasonForDefaultInd);
            ddlReviewedBy.SelectedIndex = ddlReviewedBy.Items.IndexOf(ddlReviewedBy.Items.FindByText(caseAudit.ReviewedBy));
            ddlVerbalPrivacyConsent.Text = GetIndicatorLongValue(caseAudit.VerbalPrivacyConsentInd);
            ddlWrittenPrivacyConsent.Text = GetIndicatorLongValue(caseAudit.WrittenActionConsentInd);
        }

        private string GetIndicatorShortValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            if (value.ToUpper() == Constant.INDICATOR_YES_FULL.ToUpper())
                return Constant.INDICATOR_YES;
            return Constant.INDICATOR_NO;
        }

        private string GetIndicatorLongValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            if (value.ToUpper() == Constant.INDICATOR_YES.ToUpper())
                return Constant.INDICATOR_YES_FULL;
            return Constant.INDICATOR_NO_FULL;
        }

        private DateTime? ConvertToDateTime(object obj)
        {
            DateTime dt;
            if (DateTime.TryParse(obj.ToString(), out dt))
                return dt;
            return null;
        }
        public string msgWARN0450
        {
            get
            {
                Object msg = ViewState["msgWARN0450"];
                return (msg == null) ? ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0450) : (string)msg;
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
                btnNew.Enabled = false;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
            }
            else
            {
                btnNew.Enabled = true;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
        }
        #endregion
    }
}