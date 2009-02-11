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
        private bool _isUpdating = false;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {               
                ApplySecurity();
                BindDataToIndicatorDropDownLists();
                BindDataToReviewedByDDL();
                BindDataToAuditTypeDDL();
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CaseAuditDTO caseAudit = FormToCaseAuditDTO();
            if (_isUpdating)
                CaseAuditBL.Instance.SaveCaseAudit(caseAudit, HPFWebSecurity.CurrentIdentity.LoginName, true);
            else
                CaseAuditBL.Instance.SaveCaseAudit(caseAudit, HPFWebSecurity.CurrentIdentity.LoginName, false);
            _isUpdating = false;
   
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            _isUpdating = false;
            ClearControls();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            _isUpdating = false;
            ClearControls();
        }


        private CaseAuditDTOCollection RetrieveCaseAudits(int fcid)
        {
            return CaseAuditBL.Instance.RetrieveCaseAudits(fcid);
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
            ddlAuditType.DataValueField = "Code";
            ddlAuditType.DataTextField = "CodeDesc";
            ddlAuditType.DataSource = auditTypeCodes;
            ddlAuditType.DataBind();
            ddlAuditType.Items.Insert(0, new ListItem(string.Empty));
            

        }

        private void BindDataToReviewedByDDL()
        {
            ddlReviewedBy.Items.Clear();
            ddlReviewedBy.Items.Add(new ListItem(string.Empty));
            ddlReviewedBy.Items.Add(new ListItem(HPFWebSecurity.CurrentIdentity.DisplayName));
        }

        private void BindDataToAuditFailureReasonDDL()
        {
            ddlAuditFailureReason.Items.Clear();
            ddlAuditFailureReason.Items.Add(new ListItem(string.Empty));
        }

        private void ClearControls()
        {
            foreach (DropDownList ddl in this.Controls)
                ddl.Text = string.Empty;
            txtAuditDate.Text = string.Empty;
            txtAuditComment.Text = string.Empty;
        }

        private CaseAuditDTO FormToCaseAuditDTO()
        {
            CaseAuditDTO caseAudit = new CaseAuditDTO()
            {
                AppropriateOutcomeInd = GetIndicatorShortValue(ddlAppropriateOutcome.SelectedValue),
                AuditComments = txtAuditComment.Text.Trim(),
                AuditDt = ConvertToDateTime(txtAuditDate.Text.Trim()),
                AuditFailureReasonCode = ddlAuditFailureReason.SelectedValue,
                AuditTypeCode = ddlAuditType.SelectedValue,
                BudgetCompletedInd = GetIndicatorShortValue(ddlBudgetCompleted.SelectedValue),
                ClientActionPlanInd = GetIndicatorShortValue(ddlClientActionPlan.SelectedValue),
                CompliantInd = GetIndicatorShortValue(ddlCompliant.SelectedValue),
                ReasonForDefaultInd = GetIndicatorShortValue(ddlReasonForDefault.SelectedValue),
                ReviewedBy = ddlReviewedBy.SelectedValue,
                VerbalPrivacyConsentInd = GetIndicatorShortValue(ddlVerbalPrivacyConsent.SelectedValue),
                WrittenActionConsentInd = GetIndicatorShortValue(ddlWrittenPrivacyConsent.SelectedValue)
            };
            return caseAudit;
        }

        private void CaseAuditDTOToForm(CaseAuditDTO caseAudit)
        {         
            ddlAppropriateOutcome.Text = GetIndicatorLongValue(caseAudit.AppropriateOutcomeInd);
            txtAuditComment.Text = caseAudit.AuditComments;
            txtAuditDate.Text = (caseAudit.AuditDt.HasValue) ? caseAudit.AuditDt.Value.Date.ToString() : string.Empty;
            ddlAuditFailureReason.Text = caseAudit.AuditFailureReasonCode;
            ddlAuditType.Text = caseAudit.AuditTypeCode;
            ddlBudgetCompleted.Text = GetIndicatorLongValue(caseAudit.BudgetCompletedInd);
            ddlClientActionPlan.Text = GetIndicatorLongValue(caseAudit.ClientActionPlanInd);
            ddlCompliant.Text = GetIndicatorLongValue(caseAudit.CompliantInd);
            ddlReasonForDefault.Text = GetIndicatorLongValue(caseAudit.ReasonForDefaultInd);            
            ddlReviewedBy.Text = GetIndicatorLongValue(caseAudit.ReviewedBy);
            ddlVerbalPrivacyConsent.Text = GetIndicatorLongValue(caseAudit.VerbalPrivacyConsentInd);
            ddlWrittenPrivacyConsent.Text = GetIndicatorLongValue(caseAudit.WrittenActionConsentInd);
        }

        private string GetIndicatorShortValue(string value)
        {
            if (value.ToUpper() == Constant.INDICATOR_YES_FULL.ToUpper())
                return Constant.INDICATOR_YES;
            return Constant.INDICATOR_NO;
        }

        private string GetIndicatorLongValue(string value)
        {
            if (value.ToUpper() == Constant.INDICATOR_YES.ToUpper())
                return Constant.INDICATOR_YES_FULL;
            return Constant.INDICATOR_NO_FULL;
        }

        private DateTime ConvertToDateTime(object obj)
        {
            DateTime dt;
            DateTime.TryParse(obj.ToString(), out dt);
            return dt;
        }

        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                //btnDelete.Enabled = false;
                //btnReinstate.Enabled = false;
            }
            else
            {
                //btnDelete.Enabled = true;
                //btnReinstate.Enabled = true;
            }
        }
    }
}