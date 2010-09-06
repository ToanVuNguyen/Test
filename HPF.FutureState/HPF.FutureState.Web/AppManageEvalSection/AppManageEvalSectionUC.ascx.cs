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

namespace HPF.FutureState.Web.AppManageEvalSection
{
    public partial class AppManageEvalSectionUC : System.Web.UI.UserControl
    {
        public EvalSectionCollectionDTO evalSectionCollection
        {
            get { return (EvalSectionCollectionDTO)ViewState["EvalSections"]; }
            set { ViewState["EvalSections"] = value; }
        }
        public int? selectedEvalSectionId
        {
            get { return (int?)ViewState["evalSectionId"]; }
            set { ViewState["evalSectionId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    evalSectionCollection = EvalTemplateBL.Instance.RetriveAllEvalSection();
                    BindDropDownList();
                    btnUpdate.Enabled = false;
                    ClearData();
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
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void BindDropDownList()
        {
            ddlSection.DataValueField = "EvalSectionId";
            ddlSection.DataTextField = "SectionName";
            ddlSection.DataSource = evalSectionCollection;
            ddlSection.DataBind();
            ddlSection.Items.Insert(0, new ListItem("New Section", "-1"));
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedEvalSectionId = ConvertToInt(ddlSection.SelectedValue);
            EvalSectionDTO evalSection = evalSectionCollection.FirstOrDefault(o => o.EvalSectionId == selectedEvalSectionId);
            txtSectionName.Text = (evalSection != null ? evalSection.SectionName : "");
            txtSectionDescription.Text = (evalSection != null ? evalSection.SectionDescription : "");
            if (evalSection == null)
                chkActive.Checked = true;
            else 
                chkActive.Checked = ((evalSection.ActiveInd == Constant.INDICATOR_YES) ? true : false);
            btnUpdate.Enabled = (evalSection == null ? false : true);
            btnAddNew.Enabled = (evalSection != null ? false : true);
        }
        private int ConvertToInt(object obj)
        {
            int value;
            if (int.TryParse(obj.ToString().Trim(), out value))
                return value;
            return int.MinValue;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                EvalSectionDTO evalSection = evalSectionCollection.FirstOrDefault(o => o.EvalSectionId == selectedEvalSectionId);
                if (evalSection != null)
                {
                    evalSection.SectionName = txtSectionName.Text;
                    evalSection.SectionDescription = txtSectionDescription.Text;
                    evalSection.ActiveInd = (chkActive.Checked ? Constant.INDICATOR_YES : Constant.INDICATOR_NO);
                    evalSection.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                    EvalTemplateBL.Instance.UpdateEvalSection(evalSection);
                    lblErrorMessage.Items.Add(new ListItem("Update Successfull !!!"));
                    BindDropDownList();
                    ddlSection.Items.FindByValue(selectedEvalSectionId.ToString()).Selected=true;
                }
            }
            catch (DataValidationException ex)
            {
                lblErrorMessage.DataSource = ex.ExceptionMessages;
                lblErrorMessage.DataBind();
                //Set activeInd check box be check again
                foreach (ExceptionMessage ex1 in ex.ExceptionMessages)
                    if (ex1.ErrorCode == ErrorMessages.ERR1108)
                        chkActive.Checked = true;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);

            }
            catch (Exception ex)
            {
                ClearErrorMessages();
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                EvalSectionDTO evalSection = new EvalSectionDTO();
                evalSection.SectionName = txtSectionName.Text;
                evalSection.SectionDescription = txtSectionDescription.Text;
                evalSection.ActiveInd = (chkActive.Checked ? Constant.INDICATOR_YES : Constant.INDICATOR_NO);
                evalSection.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                evalSection.EvalSectionId = EvalTemplateBL.Instance.InsertEvalSection(evalSection);
                evalSectionCollection.Add(evalSection);
                BindDropDownList();
                lblErrorMessage.Items.Add(new ListItem("Insert new section successfull !!!"));
                ClearData();
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
        private void ClearData()
        {
            txtSectionName.Text = "";
            txtSectionDescription.Text = "";
            chkActive.Checked = true;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }
        private void ClearErrorMessages()
        {
            lblErrorMessage.Items.Clear();
        }
    }
}