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
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common;

namespace HPF.FutureState.Web.ForeclosureCaseDetail
{
    public partial class QCSelection : System.Web.UI.UserControl
    {
        private int caseId;
        private CaseEvalHeaderDTO evalHeader;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BindQCTemplateName();
                caseId = int.Parse(Request.QueryString["CaseID"].ToString());
                evalHeader = CaseEvaluationBL.Instance.GetCaseEvalHeaderByCaseId(caseId);
                if (evalHeader != null)
                {
                    ddlEvalTemplate.Items.FindByValue(evalHeader.EvalTemplateId.ToString()).Selected = true;
                    rbtnOnSite.Checked = (evalHeader.EvalType == CaseEvaluationBL.EvaluationType.ONSITE);
                    btnSelectQC.Enabled = false;
                    btnRemoveQC.Attributes.Add("onclick", " return CancelClientClick();");
                }
                else btnRemoveQC.Enabled = false;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        /// <summary>
        /// Display data in dropdown list 
        /// </summary>
        private void BindQCTemplateName()
        {
            EvalTemplateDTOCollection templateCollection = EvalTemplateBL.Instance.RetriveAllTemplate();
            ddlEvalTemplate.DataValueField = "EvalTemplateId";
            ddlEvalTemplate.DataTextField = "TemplateName";
            ddlEvalTemplate.DataSource = templateCollection;
            ddlEvalTemplate.DataBind();
        }

        protected void btnSelectQC_Click(object sender, EventArgs e)
        {
            try
            {
                CaseEvalHeaderDTO evalHeader = new CaseEvalHeaderDTO();
                ForeclosureCaseDTO fc = ForeclosureCaseBL.Instance.GetForeclosureCase(caseId);
                evalHeader.FcId = caseId;
                evalHeader.EvalTemplateId = ConvertToInt(ddlEvalTemplate.SelectedValue);
                evalHeader.EvalType = (rbtnDesktop.Checked == true ? CaseEvaluationBL.EvaluationType.DESKTOP : CaseEvaluationBL.EvaluationType.ONSITE);
                evalHeader.AgencyId = fc.AgencyId;
                evalHeader.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName.ToString());
                CaseEvaluationBL.Instance.SaveSelectForQCEvalHeader(evalHeader);
                btnSelectQC.Enabled = false;
                btnRemoveQC.Enabled = true;
                //Send notify email to all agency auditor which case belong to
                CaseEvaluationBL.Instance.SendNotifyEmail("", evalHeader.FcId.Value,evalHeader.AgencyId.Value, evalHeader.EvalStatus, evalHeader.EvalType);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveFCFromQC();
                btnSelectQC.Enabled = true;
                btnRemoveQC.Enabled = false;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void RemoveFCFromQC()
        {
            CaseEvaluationBL.Instance.RemoveCaseEval(evalHeader, Server.MapPath(HPFConfigurationSettings.HPF_QC_FILE_UPLOAD_PATH));
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