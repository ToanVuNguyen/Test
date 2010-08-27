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
    public partial class EvaluationTemplate : System.Web.UI.UserControl
    {
        private int? selectedEvalTeplateId
        {
            get { return (Session["evalTemplateId"] != null ? (int?)Session["evalTemplateId"] : 0); }
            set { Session["evalTemplateId"] = value; }
        }
        private EvalTemplateDTOCollection evalTemplateCollection
        {
            get { return (EvalTemplateDTOCollection)Session["evalTemplateCollection"]; }
            set { Session["evalTemplateCollection"] = value; }
        }
        public delegate void OnButtonClick();
        public event OnButtonClick updateHandler;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                ManageEvalTemplate page = (ManageEvalTemplate)this.Page;
                page.selectChangeHandle += new ManageEvalTemplate.OnSelectedChange(BindData);
                if (!IsPostBack)
                    btnUpdate.Enabled = false;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void BindData()
        {
            if (evalTemplateCollection != null)
            {
                EvalTemplateDTO evalTemplate = evalTemplateCollection.FirstOrDefault(o => o.EvalTemplateId == selectedEvalTeplateId);
                txtTemplateName.Text = (evalTemplate != null ? evalTemplate.TemplateName : "");
                txtTemplateDescription.Text = (evalTemplate != null ? evalTemplate.TemplateDescription : "");
                chkActive.Checked = ((evalTemplate != null && evalTemplate.ActiveInd == Constant.INDICATOR_YES) ? true : false);
                btnUpdate.Enabled = (evalTemplate != null ? true : false);
            }
        }
        private void ClearErrorMessage()
        {
            lblErrorMessage.Items.Clear();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                EvalTemplateDTO evalTemplate = evalTemplateCollection.FirstOrDefault(o => o.EvalTemplateId == selectedEvalTeplateId);
                if (evalTemplate != null)
                {
                    evalTemplate.TemplateName = txtTemplateName.Text;
                    evalTemplate.TemplateDescription = txtTemplateDescription.Text;
                    evalTemplate.ActiveInd = (chkActive.Checked ? Constant.INDICATOR_YES : Constant.INDICATOR_NO);
                    evalTemplate.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                    EvalTemplateBL.Instance.UpdateEvalTemplate(evalTemplate);
                    lblErrorMessage.Items.Add(new ListItem("Update Successfull !!!"));
                    if (updateHandler != null)
                        updateHandler();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
    }
}