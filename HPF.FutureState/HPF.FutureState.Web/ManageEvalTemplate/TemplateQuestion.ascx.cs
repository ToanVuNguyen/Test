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

namespace HPF.FutureState.Web.ManageEvalTemplateTab
{
    public partial class TemplateQuestion : System.Web.UI.UserControl
    {
        private int? selectedEvalTemplateId
        {
            get { return (Session["evalTemplateId"] != null ? (int?)Session["evalTemplateId"] : 0); }
            set { Session["evalTemplateId"] = value; }
        }
        private EvalSectionCollectionDTO evalSectionCollection;
        private EvalSectionQuestionDTOCollection questionCollection;
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
            evalSectionCollection = EvalTemplateBL.Instance.RetrivEvalSectionByTemplateId(selectedEvalTemplateId);
            questionCollection = EvalTemplateBL.Instance.RetriveAllEvalQuestionByTemplateId(selectedEvalTemplateId);
            //Clear html
            placeHolder.Controls.Clear();
            foreach (EvalSectionQuestionDTO question in questionCollection)
                RenderRow(question);
        }
        private void RenderRow(EvalSectionQuestionDTO question)
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            //Add check box
            tc.Attributes.Add("Align", "Center");
            CheckBox chkBox = new CheckBox();
            chkBox.Checked = (question.EvalTemplateId != -1 ? true : false);
            chkBox.ID = "chk" + question.EvalQuestionId;
            tc.Controls.Add(chkBox);
            tr.Controls.Add(tc);
            //Add Section Name
            tc = new TableCell();
            tc.Attributes.Add("align", "left");
            tc.Attributes.Add("class", "Text");
            Label lbl = new Label();
            lbl.Text = question.EvalQuestion.Question;
            tc.Controls.Add(lbl);
            tr.Controls.Add(tc);
            //Add section drop down list box
            tc = new TableCell();
            tc.Attributes.Add("align", "center");
            DropDownList ddl = new DropDownList();
            ddl.ID = "ddl" + question.EvalQuestionId;
            ddl.DataValueField = "EvalSectionId";
            ddl.DataTextField = "SectionName";
            ddl.DataSource = evalSectionCollection;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose Section", "-1"));
            ddl.Items.FindByValue(question.EvalSectionId.ToString()).Selected = true;
            tc.Controls.Add(ddl);
            tr.Controls.Add(tc);
            //Add order column
            tc = new TableCell();
            tc.Attributes.Add("align", "center");
            TextBox txtBox = new TextBox();
            txtBox.Text = (question.QuestionOrder != -1 ? question.QuestionOrder.ToString() : "");
            txtBox.ID = "txt" + question.EvalQuestionId;
            tc.Controls.Add(txtBox);
            tr.Controls.Add(tc);
            placeHolder.Controls.Add(tr);
        }
        private EvalSectionQuestionDTOCollection DraftQuestionCollection()
        {
            EvalSectionQuestionDTOCollection result = new EvalSectionQuestionDTOCollection();
            foreach (EvalSectionQuestionDTO question in questionCollection)
            {
                CheckBox chkBox = placeHolder.FindControl("chk" + question.EvalQuestionId) as CheckBox;
                TextBox txtBox = placeHolder.FindControl("txt" + question.EvalQuestionId) as TextBox;
                DropDownList ddl = placeHolder.FindControl("ddl"+question.EvalQuestionId) as DropDownList;
                if ((question.EvalTemplateId == -1) && (chkBox.Checked))
                {
                    question.StatusChanged = (byte)EvalTemplateBL.StatusChanged.Insert;
                    question.EvalTemplateId = selectedEvalTemplateId;
                    question.EvalSectionId = ConvertToInt(ddl.SelectedValue);
                    question.QuestionOrder = ConvertToInt(txtBox.Text);
                    question.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                    result.Add(question);
                }
                else if (question.EvalTemplateId > 0)
                {
                    question.StatusChanged = (byte)(chkBox.Checked ? EvalTemplateBL.StatusChanged.Update : EvalTemplateBL.StatusChanged.Remove);
                    question.EvalSectionId = ConvertToInt(ddl.SelectedValue);
                    question.QuestionOrder = ConvertToInt(txtBox.Text);
                    question.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                    result.Add(question);
                }
                
            }
            return result;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                EvalSectionQuestionDTOCollection evalTemplateSectionCollection = DraftQuestionCollection();
                EvalTemplateBL.Instance.ManageEvalSectionQuestion(evalTemplateSectionCollection,HPFWebSecurity.CurrentIdentity.LoginName);
                lblErrorMessage.Items.Add(new ListItem("Update Successfully"));
                BindData();
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

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Session.Remove("evalTemplateCollection");
            Session.Remove("evalTemplateId");
            Response.Redirect("default.aspx");
        }
    }
}