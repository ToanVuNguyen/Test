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
using System.Collections.Generic;
using HPF.FutureState.Common;

namespace HPF.FutureState.Web.QCSelectionCaseDetail
{
    public partial class AgencyAudit : System.Web.UI.UserControl
    {
        private List<string> questionOrders=null;
        private string[] answers = new string[] { CaseEvaluationBL.EvaluationYesNoAnswer.YES,CaseEvaluationBL.EvaluationYesNoAnswer.NO,CaseEvaluationBL.EvaluationYesNoAnswer.NA};
        private CaseEvalDetailDTOCollection caseEvalDetailDraftCollection=null;
        private CaseEvalHeaderDTO evalHeader = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int caseId = int.Parse(Request.QueryString["CaseID"].ToString());
                evalHeader = CaseEvaluationBL.Instance.GetCaseEvalHeaderByCaseId(caseId);
                btnSaveNew.Enabled = false;
                if (evalHeader != null)
                {
                    if (evalHeader.EvalStatus == CaseEvaluationBL.EvaluationStatus.AGENCY_INPUT_REQUIRED)
                        RenderEvalSetNotExist(evalHeader.EvalTemplateId);
                    else
                        RenderEvalSetExist(evalHeader.CaseEvalHeaderId);
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }

        private void RenderEvalSetExist(int? caseEvalHeaderId)
        {
            CaseEvalSetDTO caseEvalSetLatest = CaseEvaluationBL.Instance.GetCaseEvalLatest(caseEvalHeaderId, Constant.INDICATOR_NO);
            questionOrders = new List<string>();
            string prevSectionName="";
            foreach (CaseEvalDetailDTO evalDetail in caseEvalSetLatest.CaseEvalDetails)
            {
                //Render Section row
                if (string.Compare(prevSectionName, evalDetail.SectionName)!=0)
                    placeHolder.Controls.Add(RenderSectionRow(evalDetail.SectionName));
                //Render Question row
                placeHolder.Controls.Add(RenderQuestionRow(evalDetail.CaseEvalDetailId, evalDetail.QuestionOrder, evalDetail.EvalQuestion, "", "",evalDetail.EvalAnswer,evalDetail.Comments));
                prevSectionName = evalDetail.SectionName;
            }
        }
        /// <summary>
        /// Render html page base on EvalTemplateId
        /// This function will be called when evaluation status is AgencyInputRequired
        /// </summary>
        /// <param name="evalTemplateId"></param>
        private void RenderEvalSetNotExist(int? evalTemplateId)
        {
            EvalTemplateDTO evalTemplate = EvalTemplateBL.Instance.RetriveTemplate(evalTemplateId);
            StringBuilder sectionHTML = new StringBuilder();
            EvalQuestionDTO q;
            questionOrders = new List<string>();
            caseEvalDetailDraftCollection = new CaseEvalDetailDTOCollection();
            foreach (EvalTemplateSectionDTO ts in evalTemplate.EvalTemplateSections)
            {
                placeHolder.Controls.Add(RenderSectionRow(ts.EvalSection.SectionName));
                foreach (EvalSectionQuestionDTO sq in ts.EvalSection.EvalSectionQuestions)
                {
                    q = sq.EvalQuestion;
                    placeHolder.Controls.Add(RenderQuestionRow(q.EvalQuestionId, sq.QuestionOrder, q.Question, q.QuestionExample, q.QuestionDescription,"",""));
                    //Save question data to list, use to insert new after
                    CaseEvalDetailDTO caseEvalDetailDraft = new CaseEvalDetailDTO();
                    caseEvalDetailDraft.EvalSectionId = ts.EvalSectionId;
                    caseEvalDetailDraft.SectionName = ts.EvalSection.SectionName;
                    caseEvalDetailDraft.SectionOrder = ts.SectionOrder;
                    caseEvalDetailDraft.EvalQuestionId = q.EvalQuestionId;
                    caseEvalDetailDraft.EvalQuestion = q.Question;
                    caseEvalDetailDraft.QuestionOrder = sq.QuestionOrder;
                    caseEvalDetailDraft.QuestionScore = q.QuestionScore;
                    caseEvalDetailDraftCollection.Add(caseEvalDetailDraft);
                }
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
            tr.Attributes.Add("style","background:gray");
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
        private TableRow RenderQuestionRow(int? id,int? order, string question, string questionExample, string questionDescription,string answer,string comments)
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
                lblQuestion.Text += "<br/>(ex:"+questionExample+")";
            if (!string.IsNullOrEmpty(questionDescription))
                lblQuestion.Text += "<br/>(" + questionDescription + ")";
            tcChild.Attributes.Add("align", "left");
            tcChild.Attributes.Add("class", "Text");
            tcChild.Controls.Add(lblQuestion);
            trChild.Controls.Add(tcChild);
            tbChild.Controls.Add(trChild);
            tc.Controls.Add(tbChild);
            result.Controls.Add(tc);
            //The next radio button cells
            for (int i = 0; i < answers.Length;i++ )
            {
                tc = new TableCell();
                RadioButton rbtn = new RadioButton();
                rbtn.ID = id.ToString() + answers[i];
                rbtn.GroupName = id.ToString();
                rbtn.Checked = (answers[i] == answer); 
                tc.Controls.Add(rbtn);
                result.Controls.Add(tc);
            }
            //Comment cell
            tc = new TableCell();
            tc.Attributes.Add("align", "center");
            TextBox txtBox = new TextBox();
            txtBox.ID ="txt"+id.ToString();
            txtBox.Columns = 40;
            txtBox.Rows = 1;
            txtBox.Attributes.Add("class", "Text");
            txtBox.Text = comments;
            tc.Controls.Add(txtBox);
            result.Controls.Add(tc);
            questionOrders.Add(id.ToString());
            return result;
        }
        protected void btnSaveNew_Click(object sender, EventArgs e)
        {
            try
            {
                CaseEvalSetDTO caseEvalSetDraft = DraftCaseEvalSet();
                caseEvalSetDraft = CalculateScore(caseEvalSetDraft);
                CaseEvaluationBL.Instance.SaveCaseEvalSet(evalHeader,caseEvalSetDraft,false,HPFWebSecurity.CurrentIdentity.LoginName);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        /// <summary>
        /// Draft CaseEvalSet, get data from user input screen
        /// </summary>
        /// <returns>CaseEvalSet</returns>
        private CaseEvalSetDTO DraftCaseEvalSet()
        {
            CaseEvalSetDTO result = new CaseEvalSetDTO();
            int i = 0;
            foreach (CaseEvalDetailDTO caseEvalDetailDraft in caseEvalDetailDraftCollection)
            {
                CaseEvalDetailDTO caseEvalDetailDraftTemp = GetQuestionAnswer(questionOrders[i], caseEvalDetailDraft);
                result.CaseEvalDetails.Add(caseEvalDetailDraftTemp);
                i++;
            }
            result.CaseEvalHeaderId = evalHeader.CaseEvalHeaderId;
            result.AuditorName = HPFWebSecurity.CurrentIdentity.LoginName;
            result.EvaluationDt = DateTime.Now;
            result.FatalErrorInd = (chkFatalError.Checked ? Constant.INDICATOR_YES : Constant.INDICATOR_NO);
            result.Comments =txtComments.Text;
            return result;
        }
        /// <summary>
        /// Insert question answer,audit score to caseEvalDetailDraft
        /// </summary>
        /// <param name="questionOrder">Id value of question control</param>
        /// <param name="caseEvalDetailDraft"></param>
        /// <returns>CaseEvalDetailDTO</returns>
        private CaseEvalDetailDTO GetQuestionAnswer(string questionOrder, CaseEvalDetailDTO caseEvalDetailDraft)
        {
            string answer = null;
            for (int i = 0; i < answers.Length; i++)
            {
                RadioButton rbtn = (RadioButton)placeHolder.FindControl(questionOrder + answers[i]);
                if (rbtn.Checked)
                {
                    answer = answers[i];
                    break;
                }
            }
            TextBox txtBox = (TextBox)placeHolder.FindControl("txt" + questionOrder);
            caseEvalDetailDraft.EvalAnswer = answer;
            caseEvalDetailDraft.Comments = txtBox.Text;
            return caseEvalDetailDraft;
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                CaseEvalSetDTO caseEvalSetDraft = DraftCaseEvalSet();
                caseEvalSetDraft = CalculateScore(caseEvalSetDraft);
                btnSaveNew.Enabled = true;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        /// <summary>
        /// Calculate Score
        /// Add data to caseEvalSetDraft: totalPosibleScore, totalScore, resultLevel,...
        /// </summary>
        /// <param name="caseEvalSetDraft">caseEvalSetDraft</param>
        /// <returns>CaseEvelSetDTO</returns>
        private CaseEvalSetDTO CalculateScore(CaseEvalSetDTO caseEvalSetDraft)
        {
            int totalNoScore=0;
            int totalNAScore=0;
            caseEvalSetDraft.CaseEvalDetails = CaseEvaluationBL.Instance.AssignAllQuestionScores(caseEvalSetDraft.CaseEvalDetails);
            caseEvalSetDraft = CaseEvaluationBL.Instance.CalculateCaseTotalScore(caseEvalSetDraft,ref totalNoScore,ref totalNAScore);
            decimal percent = Math.Round((decimal)((decimal)caseEvalSetDraft.TotalAuditScore /(decimal)caseEvalSetDraft.TotalPossibleScore), 4);
            //Bind to layout
            lblYesScore.InnerText = caseEvalSetDraft.TotalAuditScore.ToString();
            lblNoScore.InnerText = totalNoScore.ToString();
            lblNAScore.InnerText = totalNAScore.ToString();
            lblLevelPercent.InnerText = percent.ToString("0.0%");
            lblLevelName.InnerText = caseEvalSetDraft.ResultLevel;
            if ((!string.IsNullOrEmpty(caseEvalSetDraft.FatalErrorInd)) && (caseEvalSetDraft.FatalErrorInd ==Constant.INDICATOR_YES))
            {
                caseEvalSetDraft.ResultLevel = CaseEvaluationBL.ResultLevel.REMEDIATION;
            }
            lblLevelNameOverride.InnerText = caseEvalSetDraft.ResultLevel;
            return caseEvalSetDraft;
        }
    }
}