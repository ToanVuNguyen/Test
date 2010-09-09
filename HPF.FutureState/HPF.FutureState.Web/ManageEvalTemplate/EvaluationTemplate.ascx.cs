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
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common;
using HPF.FutureState.Web.HPFWebControls;

namespace HPF.FutureState.Web.ManageEvalTemplateTab
{
    public partial class EvaluationTemplate : System.Web.UI.UserControl
    {
        private int? selectedEvalTemplateId
        {
            get { return (Session["evalTemplateId"] != null ? (int?)Session["evalTemplateId"] : 0); }
            set { Session["evalTemplateId"] = value; }
        }
        private EvalTemplateDTOCollection evalTemplateCollection
        {
            get { return (EvalTemplateDTOCollection)Session["evalTemplateCollection"]; }
            set { Session["evalTemplateCollection"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                ManageEvalTemplate page = (ManageEvalTemplate)this.Page;
                page.selectChangeHandle += new ManageEvalTemplate.OnSelectedChange(BindData);
                BindData();
            }
            catch (DataValidationException ex)
            {
                lblErrorMessage.DataSource = ex.ExceptionMessages;
                lblErrorMessage.DataBind();
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);

            }
            catch (Exception ex)
            {
                ClearErrorMessages();
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void BindData()
        {
            if (evalTemplateCollection != null)
            {
                EvalTemplateDTO evalTemplate = evalTemplateCollection.FirstOrDefault(o => o.EvalTemplateId == selectedEvalTemplateId);
                txtTemplateName.Text = (evalTemplate != null ? evalTemplate.TemplateName : "");
                txtTemplateDescription.Text = (evalTemplate != null ? evalTemplate.TemplateDescription : "");
                if (evalTemplate == null)
                    chkActive.Checked = true;
                else 
                    chkActive.Checked = (evalTemplate.ActiveInd == Constant.INDICATOR_YES ? true : false);
                btnUpdate.Enabled = (evalTemplate != null ? true : false);
            }
            else
                chkActive.Checked = true;
        }
        private void ClearErrorMessage()
        {
            lblErrorMessage.Items.Clear();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                EvalTemplateDTO evalTemplate = evalTemplateCollection.FirstOrDefault(o => o.EvalTemplateId == selectedEvalTemplateId);
                if (evalTemplate != null)
                {
                    evalTemplate.TemplateName = txtTemplateName.Text;
                    evalTemplate.TemplateDescription = txtTemplateDescription.Text;
                    evalTemplate.ActiveInd = (chkActive.Checked ? Constant.INDICATOR_YES : Constant.INDICATOR_NO);
                    evalTemplate.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                    EvalTemplateBL.Instance.UpdateEvalTemplate(evalTemplate);
                    lblErrorMessage.Items.Add(new ListItem("Update Successfull !!!"));
                    BindTemplateDropDownList();
                }
            }
            catch (DataValidationException ex)
            {
                lblErrorMessage.DataSource = ex.ExceptionMessages;
                lblErrorMessage.DataBind();
                //Set activeInd check box be check again
                foreach (ExceptionMessage ex1 in ex.ExceptionMessages)
                    if (ex1.ErrorCode == ErrorMessages.ERR1113)
                    {
                        chkActive.Checked = true;
                        break;
                    }
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);

            }
            catch (Exception ex)
            {
                ClearErrorMessages();
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void BindTemplateDropDownList()
        {
            DropDownList ddlTemplate = this.Parent.FindControl("ddlTemplate") as DropDownList;
            if (ddlTemplate != null)
            {
                ddlTemplate.DataSource = evalTemplateCollection;
                ddlTemplate.DataBind();
                ddlTemplate.Items.Insert(0, new ListItem("New Template", "-1"));
                ddlTemplate.Items.FindByValue(selectedEvalTemplateId.ToString()).Selected = true;
            }
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                EvalTemplateDTO evalTemplate = new EvalTemplateDTO();
                evalTemplate.TemplateName = txtTemplateName.Text;
                evalTemplate.TemplateDescription = txtTemplateDescription.Text;
                evalTemplate.ActiveInd = (chkActive.Checked ? Constant.INDICATOR_YES : Constant.INDICATOR_NO);
                evalTemplate.TotalScore = 0;
                evalTemplate.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                EvalTemplateBL.Instance.InsertEvalTemplate(evalTemplate);
                lblErrorMessage.Items.Add(new ListItem("Add new template successfull !!!"));
                evalTemplateCollection.Add(evalTemplate);
                selectedEvalTemplateId = evalTemplate.EvalTemplateId;
                BindTemplateDropDownList();
                //Direct user to manage template section tab
                TabControl tabControl = this.Parent.FindControl("tabControl") as TabControl;
                if (tabControl != null)
                {
                    Session["newTemplate"] = Constant.INDICATOR_YES;
                    tabControl.ChangeSelectedTab("templateSection");
                }
            }
            catch (DataValidationException ex)
            {
                lblErrorMessage.DataSource = ex.ExceptionMessages;
                lblErrorMessage.DataBind();
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);

            }
            catch (Exception ex)
            {
                ClearErrorMessages();
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Session.Remove("evalTemplateCollection");
            Session.Remove("evalTemplateId");
            Response.Redirect("default.aspx");
        }
        private void ClearErrorMessages()
        {
            lblErrorMessage.Items.Clear();
        }
    }
}