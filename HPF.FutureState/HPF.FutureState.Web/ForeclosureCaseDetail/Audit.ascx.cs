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
                    ViewState["CaseID"] = Request.QueryString["CaseID"];
                    BindingDataToGrdvCaseAudit();

                    BindDataToIndicatorDropDownLists();
                    BindDataToReviewedByDDL();
                    BindDataToAuditTypeDDL();
                    BindDataToAuditFailureReasonDDL();
                                        
                    btnCancel.Attributes.Add("onclick", "return ConfirmToSave('" + msgWARN0450 + "','-2');");
                    btnNew.Attributes.Add("onclick", "return ConfirmToSave('" + msgWARN0450 + "','-1');");                    
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
            ClearPage();
        }
        
        private void btnEdit_Click(object sender, EventArgs e)
        {
            Button btnEdit = sender as Button;
         
            int index = int.Parse(btnEdit.CommandArgument.ToString());
            grdvCaseAudit.SelectedIndex = index;
            CaseAuditDTO caseAudit = ((CaseAuditDTOCollection)grdvCaseAudit.DataSource)[index];
            Session[Constant.SS_CASE_AUDIT_OBJECT] = caseAudit;
            CaseAuditDTOToForm(caseAudit);
            hfAction.Value = ACTION_UPDATE;
            lblFormTitle.Text = "Audit detail - Editing";
            txtAuditDate.Focus();
        }
        
        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearPage();
            hfAction.Value = ACTION_INSERT;
            lblFormTitle.Text = "Audit detail - Inserting";
            txtAuditDate.Text = DateTime.Today.ToShortDateString();
            txtAuditDate.Focus();
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
                    btnEdit.Attributes.Add("onclick", "return ConfirmToSave('" + msgWARN0450 + "','" + e.Row.DataItemIndex +"');");
                    btnEdit.Click += new EventHandler(btnEdit_Click);
                    btnEdit.CommandArgument = e.Row.RowIndex.ToString();
                }                
            }            
        }

        private RefCodeItemDTO GetRefCode(RefCodeItemDTOCollection col, string code)
        {
            foreach (RefCodeItemDTO refCode in col)
                if (refCode.Code == code)
                    return refCode;
            return new RefCodeItemDTO();
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            DoSaving();
            UpdateUI();
        }
        protected void btnNo_Click(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (selRow.Value == "-1")
            {
                hfAction.Value = ACTION_INSERT;
                grdvCaseAudit.SelectedIndex = -1;
                lblFormTitle.Text = "Audit detail - Inserting";
                txtAuditDate.Text = DateTime.Today.ToShortDateString();
                txtAuditDate.Focus();
            }
            else if (selRow.Value == "-2")
            {
                grdvCaseAudit.SelectedIndex = -1;
                ClearPage();
            }
            else if (selRow.Value != string.Empty && selRow.Value.ToUpper() != "UNDEFINED")
            {
                grdvCaseAudit.SelectedIndex = int.Parse(selRow.Value);
            }

            selRow.Value = string.Empty;
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
            int.TryParse(ViewState["CaseID"].ToString(), out caseID);
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
            ddl.Items.Add(new ListItem(string.Empty, null));
            ddl.Items.Add(new ListItem(Constant.INDICATOR_YES_FULL));
            ddl.Items.Add(new ListItem(Constant.INDICATOR_NO_FULL));
        }

        private void BindDataToAuditTypeDDL()
        {
            ddlAuditType.Items.Clear();
            
            RefCodeItemDTOCollection auditTypeCodes = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_AUDIT_TYPE_CODE);
            ddlAuditType.DataValueField = "Code";
            //ddlAuditType.DataValueField = "CodeDesc";
            ddlAuditType.DataTextField = "CodeDesc";
            ddlAuditType.DataSource = auditTypeCodes;
            ddlAuditType.DataBind();
            ddlAuditType.Items.Insert(0, new ListItem(string.Empty, null));
            

        }

        private void BindDataToReviewedByDDL()
        {
            ddlReviewedBy.Items.Clear();            

            HPFUserDTOCollection hpfUsers = LookupDataBL.Instance.GetHpfUsers();
            ddlReviewedBy.DataValueField = "UserLoginId";
            //ddlReviewedBy.DataValueField = "FullName";
            ddlReviewedBy.DataTextField = "FullName";
            ddlReviewedBy.DataSource = hpfUsers;
            ddlReviewedBy.DataBind();

            ddlReviewedBy.Items.Insert(0, new ListItem(string.Empty, null));
            ListItem all = ddlReviewedBy.Items.FindByValue("ALL");
            if (all != null)
            {
                ddlReviewedBy.Items.Remove(all);
                ddlReviewedBy.Items.Insert(1, all);
            }
        }

        private void BindDataToAuditFailureReasonDDL()
        {
            //the failure reason codes are not provided from client
            //not implemented yet
            ddlAuditFailureReason.Items.Clear();

            RefCodeItemDTOCollection failureReasonCodes = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_AUDIT_FAILURE_REASON_CODE);
            ddlAuditFailureReason.DataValueField = "Code";
            //ddlAuditFailureReason.DataValueField = "CodeDesc";
            ddlAuditFailureReason.DataTextField = "CodeDesc";
            ddlAuditFailureReason.DataSource = failureReasonCodes;
            ddlAuditFailureReason.DataBind();
            ddlAuditFailureReason.Items.Insert(0, new ListItem(string.Empty, null));
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

            hfAction.Value = null;
        }

        private CaseAuditDTO FormToCaseAuditDTO()
        {
            int? id = null;
            if (Session[Constant.SS_CASE_AUDIT_OBJECT] != null)
                id = ((CaseAuditDTO)Session[Constant.SS_CASE_AUDIT_OBJECT]).CaseAuditId;
            CaseAuditDTO caseAudit = new CaseAuditDTO();

            caseAudit.CaseAuditId = id;
            caseAudit.AppropriateOutcomeInd = GetIndicatorShortValue(ddlAppropriateOutcome.SelectedValue);
            caseAudit.AuditComments = ConvertToString(txtAuditComment.Text);// string.IsNullOrEmpty(txtAuditComment.Text.Trim()) ? null : txtAuditComment.Text,
            caseAudit.AuditDt = ConvertToDateTime(txtAuditDate.Text.Trim());
            caseAudit.AuditFailureReasonCode = ConvertToString(ddlAuditFailureReason.SelectedValue);
            caseAudit.AuditTypeCode = ConvertToString(ddlAuditType.SelectedValue);
            caseAudit.BudgetCompletedInd = GetIndicatorShortValue(ddlBudgetCompleted.SelectedValue);
            caseAudit.ClientActionPlanInd = GetIndicatorShortValue(ddlClientActionPlan.SelectedValue);
            caseAudit.CompliantInd = GetIndicatorShortValue(ddlCompliant.SelectedValue);
            caseAudit.FcId = int.Parse(ViewState["CaseID"].ToString());
            caseAudit.ReasonForDefaultInd = GetIndicatorShortValue(ddlReasonForDefault.SelectedValue);
            caseAudit.ReviewedBy = ConvertToString(ddlReviewedBy.SelectedValue);
            caseAudit.VerbalPrivacyConsentInd = GetIndicatorShortValue(ddlVerbalPrivacyConsent.SelectedValue);
            caseAudit.WrittenActionConsentInd = GetIndicatorShortValue(ddlWrittenPrivacyConsent.SelectedValue);

            return caseAudit;
        }

        private void CaseAuditDTOToForm(CaseAuditDTO caseAudit)
        {         
            ddlAppropriateOutcome.Text = GetIndicatorLongValue(caseAudit.AppropriateOutcomeInd);
            txtAuditComment.Text = caseAudit.AuditComments;
            txtAuditDate.Text = (caseAudit.AuditDt.HasValue) ? caseAudit.AuditDt.Value.Date.ToShortDateString() : string.Empty;
            //ddlAuditFailureReason.SelectedIndex = ddlAuditFailureReason.Items.IndexOf(ddlAuditFailureReason.Items.FindByText(caseAudit.AuditFailureReasonCode));
            ddlAuditType.SelectedIndex = ddlAuditType.Items.IndexOf(ddlAuditType.Items.FindByValue(caseAudit.AuditTypeCode));
            ddlBudgetCompleted.Text = GetIndicatorLongValue(caseAudit.BudgetCompletedInd);
            ddlClientActionPlan.Text = GetIndicatorLongValue(caseAudit.ClientActionPlanInd);
            ddlCompliant.Text = GetIndicatorLongValue(caseAudit.CompliantInd);
            ddlReasonForDefault.Text = GetIndicatorLongValue(caseAudit.ReasonForDefaultInd);
            ddlReviewedBy.SelectedIndex = ddlReviewedBy.Items.IndexOf(ddlReviewedBy.Items.FindByValue(caseAudit.ReviewedBy));
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

        private string ConvertToString(object obj)
        {
            if (obj == null)
                return null;
            if (string.IsNullOrEmpty(obj.ToString())) return null;
            return obj.ToString();
        }

        public string msgWARN0450
        {
            get
            {
                return ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0450);                
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

                txtAuditDate.Enabled = false;
                txtAuditComment.Enabled = false;
                ddlAppropriateOutcome.Enabled = false;
                ddlAuditFailureReason.Enabled = false;
                ddlAuditType.Enabled = false;
                ddlBudgetCompleted.Enabled = false;
                ddlClientActionPlan.Enabled = false;
                ddlCompliant.Enabled = false;
                ddlReasonForDefault.Enabled = false;
                ddlReviewedBy.Enabled = false;
                ddlVerbalPrivacyConsent.Enabled = false;
                ddlWrittenPrivacyConsent.Enabled = false;
            }            
        }
        #endregion
       
    }
}