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
using HPF.FutureState.Web.Security;
using System.Collections.Generic;
using HPF.FutureState.Common;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.Web.QCSelectionCaseDetail
{
    public partial class CompareResult : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int caseId = int.Parse(Request.QueryString["CaseID"].ToString());
                RenderData(caseId);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void RenderData(int caseId)
        {
            CaseEvalSetDTOCollection caseEvalLatestSets = CaseEvaluationBL.Instance.GetCaseEvalLatestAll(caseId);
            if (caseEvalLatestSets.Count == 2)
            {
                CaseEvalSetDTO caseEvalHPF = caseEvalLatestSets[0];
                CaseEvalSetDTO caseEvalAgency = caseEvalLatestSets[1];
                string prevSectionName = "";
                int i = 0;
                foreach (CaseEvalDetailDTO evalDetail in caseEvalAgency.CaseEvalDetails)
                {
                    //Render Section row
                    if (string.Compare(prevSectionName, evalDetail.SectionName) != 0)
                        placeHolder.Controls.Add(RenderSectionRow(evalDetail.SectionName));
                    //Render Question row
                    placeHolder.Controls.Add(RenderQuestionRow(evalDetail.QuestionOrder, evalDetail.EvalQuestion, evalDetail.QuestionExample, evalDetail.EvalAnswer, caseEvalHPF.CaseEvalDetails[i].EvalAnswer,evalDetail.Comments,caseEvalHPF.CaseEvalDetails[i].Comments));
                    prevSectionName = evalDetail.SectionName;
                    i++;
                }
                #region Render Section Comments
                placeHolder.Controls.Add(RenderSectionRow("Reviewer Comment"));
                placeHolder.Controls.Add(RenderQuestionRow(-1, "", "", "", "", caseEvalAgency.Comments, caseEvalHPF.Comments));
                #endregion
                lblAgencyScore.InnerText = caseEvalAgency.TotalAuditScore.ToString();
                lblAgencyCasePossibleScore.InnerText= caseEvalAgency.TotalPossibleScore.ToString();
                decimal percent = Math.Round((decimal)((decimal)caseEvalAgency.TotalAuditScore / (decimal)caseEvalAgency.TotalPossibleScore), 4);
                lblAgencyLevelPercent.InnerText = percent.ToString("0.0%");
                lblHPFScore.InnerText = caseEvalHPF.TotalAuditScore.ToString();
                lblHPFCasePossibleScore.InnerText = caseEvalHPF.TotalPossibleScore.ToString();
                percent = Math.Round((decimal)((decimal)caseEvalHPF.TotalAuditScore / (decimal)caseEvalHPF.TotalPossibleScore), 4);
                lblHPFLevelPercent.InnerText = percent.ToString("0.0%");
                lblAgencyLevel.InnerText = caseEvalAgency.ResultLevel;
                lblHPFLevel.InnerText = caseEvalHPF.ResultLevel;
                lblAgencyFatalError.InnerText = caseEvalAgency.FatalErrorInd;
                lblHPFFatalError.InnerText = caseEvalHPF.FatalErrorInd;
            }
        }
        /// <summary>
        /// Render html section row
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        private TableRow RenderSectionRow(string sectionName)
        {
            TableRow tr = new TableRow();
            tr.Attributes.Add("style", "background:#CDC9C9");
            for (int i = 0; i < 5; i++)
            {
                TableCell tc = new TableCell();
                if (i == 0)
                {
                    tc.Attributes.Add("class", "sidelinks");
                    tc.Attributes.Add("align", "left");
                    Label lbl = new Label();
                    lbl.Text = sectionName;
                    tc.Controls.Add(lbl);
                }
                tr.Controls.Add(tc);
            }
            return tr;
        }
        /// <summary>
        /// Render HTML question row
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="question"></param>
        /// <param name="questionExample"></param>
        /// <param name="questionDescription"></param>
        /// <returns></returns>
        private TableRow RenderQuestionRow(int? order, string question, string questionExample, string answerAgency, string answerHPF,string commentAgency,string commentHPF)
        {
            TableRow result = new TableRow();
            //First Cell
            TableCell tc = new TableCell();
            Table tbChild = new Table();
            TableRow trChild = new TableRow();
            TableCell tcChild = new TableCell();
            Label lblOrder = new Label();
            if(order>0)
                lblOrder.Text = order.ToString();
            tcChild.Attributes.Add("align", "center");
            tcChild.Attributes.Add("class", "sidelinks");
            tcChild.Attributes.Add("width", "10px");
            tcChild.Controls.Add(lblOrder);
            trChild.Controls.Add(tcChild);
            tcChild = new TableCell();
            Label lblQuestion = new Label();
            lblQuestion.Text = question;
            if (!string.IsNullOrEmpty(questionExample))
                lblQuestion.Text += "<br/>(ex:" + questionExample + ")";
            tcChild.Attributes.Add("align", "left");
            tcChild.Attributes.Add("class", "Text");
            tcChild.Controls.Add(lblQuestion);
            trChild.Controls.Add(tcChild);
            tbChild.Controls.Add(trChild);
            tc.Controls.Add(tbChild);
            result.Controls.Add(tc);
            //The next agency answer cell
            tc = new TableCell();
            tc.Attributes.Add("align", "center");
            Label lblAgencyAnswer = new Label();
            lblAgencyAnswer.Text = answerAgency;
            tc.Controls.Add(lblAgencyAnswer);
            result.Controls.Add(tc);
            //The next agency comment cell
            tc = new TableCell();
            tc.Attributes.Add("align", "justify");
            Label lblAgencyComment = new Label();
            lblAgencyComment.Text = commentAgency;
            tc.Controls.Add(lblAgencyComment);
            result.Controls.Add(tc);
            //The next HPF answer cell
            tc = new TableCell();
            tc.Attributes.Add("align", "center");
            Label lblHPFAnswer = new Label();
            lblHPFAnswer.Text = answerHPF;
            tc.Controls.Add(lblHPFAnswer);
            result.Controls.Add(tc);
            //The next HPF comment cell
            tc = new TableCell();
            tc.Attributes.Add("align", "justify");
            Label lblHPFComment = new Label();
            lblHPFComment.Text = commentHPF;
            tc.Controls.Add(lblHPFComment);
            result.Controls.Add(tc);

            return result;
        }

        protected void btnPrintReport_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Print QC Report", "<script language='javascript'>window.open('PrintQCReport.aspx?ReportType=" + Constant.QC_AUDIT_CASE_REPORT_TYPE + "&CaseId=" + Request.QueryString["CaseID"].ToString() + "','','menu=no,scrollbars=no,resizable=yes,top=0,left=0,width=1010px,height=900px')</script>");
        }
    }
}