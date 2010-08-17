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
using System.Text;
using HPF.FutureState.Web.Security;

namespace HPF.FutureState.Web.QCSelectionCaseDetail
{
    public partial class AgencyAudit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int caseId = int.Parse(Request.QueryString["CaseID"].ToString());
                CaseEvalHeaderDTO evalHeader = CaseEvaluationBL.Instance.GetCaseEvalHeaderByCaseId(caseId);
                if (evalHeader != null)
                {
                    RenderQuestionFirstTime(evalHeader.EvalTemplateId);
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void RenderQuestionFirstTime(int? evalTemplateId)
        {
            EvalTemplateDTO evalTemplate = EvalTemplateBL.Instance.RetriveTemplate(evalTemplateId);
            StringBuilder sectionHTML = new StringBuilder();
            EvalQuestionDTO q;
            foreach (EvalTemplateSectionDTO ts in evalTemplate.EvalTemplateSections)
            {
                sectionHTML.AppendFormat("<tr style='background:gray'>"
                    + "<td class='sidelinks' align='left'>{0}</td>"
                    + "<td></td><td></td><td></td><td></td></tr>", ts.EvalSection.SectionName);
                
                foreach (EvalSectionQuestionDTO sq in ts.EvalSection.EvalSectionQuestions)
                {
                    q = sq.EvalQuestion;
                    sectionHTML.AppendFormat("<tr>"
                    + "<td>"
                    + "<table width='100%'>"
                    + "<tr><td class='sidelinks' align='center' width='10px'>{0}</td>"
                    + "<td class='Text' align='left'>{1}", sq.QuestionOrder, q.Question);
                    if (!string.IsNullOrEmpty(q.QuestionExample))
                        sectionHTML.AppendFormat("<br/>(ex:{0})", q.QuestionExample);
                    if (!string.IsNullOrEmpty(q.QuestionDescription))
                        sectionHTML.AppendFormat("<br/>({0})", q.QuestionDescription);
                    sectionHTML.AppendFormat("</td></tr></table>"
                    + "</td>");
                    sectionHTML.AppendFormat("<td align='center'>"
                        + "<input type='radio' ID='q{0}_Y' GroupName='q{0}' /></td>", q.EvalQuestionId);
                    sectionHTML.AppendFormat("<td align='center'>"
                        + "<input type='radio' ID='q{0}_N' GroupName='q{0}' /></td>", q.EvalQuestionId);
                    sectionHTML.AppendFormat("<td align='center'>"
                        + "<input type='radio' ID='q{0}_YN' GroupName='q{0}' /></td>", q.EvalQuestionId);
                    sectionHTML.AppendFormat("<td align='center'></td></tr>");
                }
            }
            
            lblSectionQuestion.Text = sectionHTML.ToString();
        }

        protected void btnSaveNew_Click(object sender, EventArgs e)
        {
            
        }
        
    }
}