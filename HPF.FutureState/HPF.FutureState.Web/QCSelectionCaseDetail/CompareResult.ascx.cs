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
            int caseId = int.Parse(Request.QueryString["CaseID"].ToString());
            RenderData(caseId);
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
                    placeHolder.Controls.Add(RenderQuestionRow(evalDetail.CaseEvalDetailId, evalDetail.QuestionOrder, evalDetail.EvalQuestion, evalDetail.QuestionExample, evalDetail.EvalAnswer, caseEvalHPF.CaseEvalDetails[i].EvalAnswer));
                    prevSectionName = evalDetail.SectionName;
                    i++;
                }
                lblAgencyScore.InnerText = caseEvalAgency.TotalAuditScore + "/" + caseEvalAgency.TotalPossibleScore;
                lblHPFScore.InnerText = caseEvalHPF.TotalAuditScore + "/" + caseEvalHPF.TotalPossibleScore;
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
        private TableRow RenderQuestionRow(int? id, int? order, string question, string questionExample, string answerAgency, string answerHPF)
        {
            TableRow result = new TableRow();
            //First Cell
            TableCell tc = new TableCell();
            Table tbChild = new Table();
            TableRow trChild = new TableRow();
            TableCell tcChild = new TableCell();
            Label lblOrder = new Label();
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
            //The next HPF answer cell
            tc = new TableCell();
            tc.Attributes.Add("align", "center");
            Label lblHPFAnswer = new Label();
            lblHPFAnswer.Text = answerHPF;
            tc.Controls.Add(lblHPFAnswer);
            result.Controls.Add(tc);

            return result;
        }
    }
}