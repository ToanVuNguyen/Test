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
using HPF.FutureState.Web.ManageEvalTemplateTab;

namespace HPF.FutureState.Web
{
    public partial class ManageEvalTemplate : System.Web.UI.Page
    {
        string UCLOCATION = "ManageEvalTemplate\\";
        private EvalTemplateDTOCollection evalTemplateCollection
        {
            get { return (EvalTemplateDTOCollection)Session["evalTemplateCollection"]; }
            set { Session["evalTemplateCollection"] = value; }
        }
        public delegate void OnSelectedChange();
        public event OnSelectedChange selectChangeHandle;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                tabControl.TabClick += new HPF.FutureState.Web.HPFWebControls.TabControlEventHandler(tabControl_TabClick);
                if (!IsPostBack)
                {
                    tabControl.AddTab("evaluationTemplate", "Evaluation Template");
                    tabControl.AddTab("templateSection", "Template Section");
                    tabControl.AddTab("templateQuestion", "Template Question");
                    tabControl.SelectedTab = "evaluationTemplate";
                    UserControlLoader.LoadUserControl(UCLOCATION + "EvaluationTemplate.ascx", "ucEvaluationTemplate");

                    evalTemplateCollection = EvalTemplateBL.Instance.RetriveAllTemplate();
                    BindDropDownList();
                  
                }
                if (UserControlLoader.Controls.Count > 0)
                {
                    EvaluationTemplate evaluationTemplateControl = (EvaluationTemplate)UserControlLoader.Controls[0];
                    if (evaluationTemplateControl != null)
                        evaluationTemplateControl.updateHandler += new EvaluationTemplate.OnButtonClick(UpdateEvalTemplate);
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void BindDropDownList()
        {
            ddlTemplate.DataValueField = "EvalTemplateId";
            ddlTemplate.DataTextField = "TemplateName";
            ddlTemplate.DataSource = evalTemplateCollection;
            ddlTemplate.DataBind();
            ddlTemplate.Items.Insert(0, new ListItem("New Template", "-1"));
        }
        protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["evalTemplateId"] =ConvertToInt(ddlTemplate.SelectedValue);
            if (selectChangeHandle != null)
                selectChangeHandle();
        }
        private void UpdateEvalTemplate()
        {
            BindDropDownList();
        }
        void tabControl_TabClick(object sender, HPF.FutureState.Web.HPFWebControls.TabControlEventArgs e)
        {
            try
            {
                switch (e.SelectedTabID)
                {
                    case "evaluationTemplate":
                        UserControlLoader.LoadUserControl(UCLOCATION + "EvaluationTemplate.ascx", "ucEvaluationTemplate");
                        break;
                    case "templateSection":
                        UserControlLoader.LoadUserControl(UCLOCATION + "TemplateSection.ascx", "ucTemplateSection");
                        break;
                    case "templateQuestion":
                        UserControlLoader.LoadUserControl(UCLOCATION + "TemplateQuestion.ascx", "ucTemplateQuestion");
                        break;
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }

        }
        private int ConvertToInt(object obj)
        {
            int value;
            if (int.TryParse(obj.ToString().Trim(), out value))
                return value;
            return int.MinValue;
        }
    }
}
