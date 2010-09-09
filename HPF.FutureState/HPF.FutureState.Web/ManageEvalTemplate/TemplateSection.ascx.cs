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
    public partial class TemplateSection : System.Web.UI.UserControl
    {
        private int? selectedEvalTemplateId
        {
            get { return (Session["evalTemplateId"] != null ? (int?)Session["evalTemplateId"] : 0); }
            set { Session["evalTemplateId"] = value; }
        }
        private EvalTemplateSectionDTOCollection sectionCollection;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ManageEvalTemplate page = (ManageEvalTemplate)this.Page;
                page.selectChangeHandle += new ManageEvalTemplate.OnSelectedChange(BindData);
                BindData();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void BindData()
        {
            btnUpdate.Enabled = (selectedEvalTemplateId != -1 ? true : false);
            sectionCollection = EvalTemplateBL.Instance.RetriveAllEvalSectionsByTemplateId(selectedEvalTemplateId);
            //Clear html
            placeHolder.Controls.Clear();
            foreach (EvalTemplateSectionDTO section in sectionCollection)
                RenderRow(section);
        }
        private void RenderRow(EvalTemplateSectionDTO section)
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            //Add check box
            tc.Attributes.Add("Align", "Center");
            CheckBox chkBox = new CheckBox();
            chkBox.Enabled = (!section.IsInUse);
            chkBox.Checked = (section.EvalTemplateId != -1?true:false);
            chkBox.ID = "chk" + section.EvalSectionId;
            tc.Controls.Add(chkBox);
            tr.Controls.Add(tc);
            //Add Section Name
            tc = new TableCell();
            tc.Attributes.Add("align", "left");
            tc.Attributes.Add("class", "Text");
            Label lbl = new Label();
            lbl.Text = section.EvalSection.SectionName;
            tc.Controls.Add(lbl);
            tr.Controls.Add(tc);
            //Add order
            tc = new TableCell();
            tc.Attributes.Add("align", "center");
            TextBox txtBox = new TextBox();
            txtBox.Text = (section.SectionOrder != -1 ? section.SectionOrder.ToString() : "");
            txtBox.ID = "txt" + section.EvalSectionId;
            tc.Controls.Add(txtBox);
            tr.Controls.Add(tc);
            placeHolder.Controls.Add(tr);
        }
        private EvalTemplateSectionDTOCollection DraftSectionCollection()
        {
            EvalTemplateSectionDTOCollection result = new EvalTemplateSectionDTOCollection();
            foreach (EvalTemplateSectionDTO section in sectionCollection)
            {
                CheckBox chkBox = placeHolder.FindControl("chk" + section.EvalSectionId) as CheckBox;
                TextBox txtBox = placeHolder.FindControl("txt" + section.EvalSectionId) as TextBox;
                if ((section.EvalTemplateId == -1) && (chkBox.Checked))
                {
                    section.StatusChanged = (byte)EvalTemplateBL.StatusChanged.Insert;
                    section.EvalTemplateId = selectedEvalTemplateId;
                    section.SectionOrder = ConvertToInt(txtBox.Text.Trim());
                    section.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                    result.Add(section);
                }
                else if (section.EvalTemplateId>0)
                {
                    section.StatusChanged = (byte)(chkBox.Checked ? EvalTemplateBL.StatusChanged.Update : EvalTemplateBL.StatusChanged.Remove);
                    section.SectionOrder = ConvertToInt(txtBox.Text.Trim());
                    section.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                    result.Add(section);
                }
            }
            return result;
        }
        protected static int? ConvertToInt(object obj)
        {
            int returnValue = 0;

            if (obj == null || !int.TryParse(obj.ToString(), out returnValue))
                return null;
            return returnValue;
        }
        private void ClearErrorMessages()
        {
            lblErrorMessage.Items.Clear();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                EvalTemplateSectionDTOCollection evalTemplateSectionCollection = DraftSectionCollection();
                EvalTemplateBL.Instance.ManageEvalTemplateSection(evalTemplateSectionCollection);
                lblErrorMessage.Items.Add(new ListItem("Update Successfully"));
                BindData();
                //Direct user to manage template question tab if new template is added
                if (Session["newTemplate"] != null)
                {
                    Session.Remove("newTemplate");
                    TabControl tabControl = this.Parent.FindControl("tabControl") as TabControl;
                    if (tabControl != null)
                        tabControl.ChangeSelectedTab("templateQuestion");
                }
            }
            catch (DataValidationException ex)
            {
                ClearErrorMessages();
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
    }
}