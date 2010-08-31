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
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common;

namespace HPF.FutureState.Web.AppManageEvalQuestion
{
    public partial class AppManageEvalQuestionUC : System.Web.UI.UserControl
    {
        public EvalQuestionDTOCollection evalQuestionCollection
        {
            get { return (EvalQuestionDTOCollection)ViewState["EvalSections"]; }
            set { ViewState["EvalSections"] = value; }
        }
        public int? selectedEvalQuestionId
        {
            get { return (int?)ViewState["evalQuestionId"]; }
            set { ViewState["evalQuestionId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    evalQuestionCollection = EvalTemplateBL.Instance.RetriveAllQuestion();
                    BindQuestionDropDownList();
                    BindScoreDropDownList();
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
        private void BindQuestionDropDownList()
        {
            ddlQuestion.DataValueField = "EvalQuestionId";
            ddlQuestion.DataTextField = "Question";
            ddlQuestion.DataSource = evalQuestionCollection;
            ddlQuestion.DataBind();
            ddlQuestion.Items.Insert(0, new ListItem("New Question", "-1"));
        }
        private void BindScoreDropDownList()
        {
            for (int i = 1; i <= 5; i++)
                ddlQuestionScore.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }

        protected void ddlQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedEvalQuestionId = ConvertToInt(ddlQuestion.SelectedValue);
            EvalQuestionDTO evalQuestion = evalQuestionCollection.FirstOrDefault(o => o.EvalQuestionId == selectedEvalQuestionId);
            txtQuestion.Text = (evalQuestion != null ? evalQuestion.Question : "");
            txtQuestionDescription.Text = (evalQuestion != null ? evalQuestion.QuestionDescription : "");
            txtQuestionExample.Text = (evalQuestion != null ? evalQuestion.QuestionExample : "");
            txtQuestionType.Text = (evalQuestion != null ? evalQuestion.QuestionType : "YesNo");
            ddlQuestionScore.SelectedValue =(evalQuestion!=null?evalQuestion.QuestionScore.ToString():"1");
            chkActive.Checked = (((evalQuestion != null) && (evalQuestion.ActiveInd == Constant.INDICATOR_YES)) ? true : false);
            btnUpdate.Enabled = (evalQuestion == null ? false : true);
            btnAddNew.Enabled = (evalQuestion != null ? false : true);
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
                EvalQuestionDTO evalQuestion = evalQuestionCollection.FirstOrDefault(o => o.EvalQuestionId == selectedEvalQuestionId);
                if (evalQuestion != null)
                {
                    evalQuestion.Question = txtQuestion.Text;
                    evalQuestion.QuestionDescription = txtQuestionDescription.Text;
                    evalQuestion.QuestionExample = txtQuestionExample.Text;
                    evalQuestion.QuestionType = txtQuestionType.Text;
                    evalQuestion.QuestionScore = ConvertToInt(ddlQuestionScore.SelectedValue);
                    evalQuestion.ActiveInd = (chkActive.Checked ? Constant.INDICATOR_YES : Constant.INDICATOR_NO);
                    evalQuestion.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                    EvalTemplateBL.Instance.UpdateEvalQuestion(evalQuestion);
                    BindQuestionDropDownList();
                    ddlQuestion.Items.FindByValue(selectedEvalQuestionId.ToString()).Selected = true;

                    ClearErrorMessages();
                    lblErrorMessage.Items.Add(new ListItem("Update Successfull !!!"));
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
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                EvalQuestionDTO evalQuestion = new EvalQuestionDTO();

                evalQuestion.Question = txtQuestion.Text;
                evalQuestion.QuestionDescription = txtQuestionDescription.Text;
                evalQuestion.QuestionExample = txtQuestionExample.Text;
                evalQuestion.QuestionType = txtQuestionType.Text;
                evalQuestion.QuestionScore = ConvertToInt(ddlQuestionScore.SelectedValue);
                evalQuestion.ActiveInd = (chkActive.Checked ? Constant.INDICATOR_YES : Constant.INDICATOR_NO);
                evalQuestion.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                evalQuestion.EvalQuestionId = EvalTemplateBL.Instance.InsertEvalQuestion(evalQuestion);

                evalQuestionCollection.Add(evalQuestion);
                BindQuestionDropDownList();
                ClearErrorMessages();
                lblErrorMessage.Items.Add(new ListItem("Add New Question Successfull !!!"));
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
            txtQuestion.Text = "";
            txtQuestionDescription.Text = "";
            txtQuestionExample.Text = "";
            txtQuestionType.Text = "YesNo";
            ddlQuestionScore.SelectedValue = "1";
            chkActive.Checked = true;
        }
        private void ClearErrorMessages()
        {
            lblErrorMessage.Items.Clear();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }
    }
}