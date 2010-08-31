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
using HPF.FutureState.Web.HPFWebControls;

namespace HPF.FutureState.Web.QCSelectionCaseDetail
{
    public partial class AgencyAudit : System.Web.UI.UserControl
    {
        private List<string> questionOrders=null;
        private string[] answers = new string[] { CaseEvaluationBL.EvaluationYesNoAnswer.YES,CaseEvaluationBL.EvaluationYesNoAnswer.NO,CaseEvaluationBL.EvaluationYesNoAnswer.NA};
        private CaseEvalDetailDTOCollection caseEvalDetailDraftCollection=null;
        private CaseEvalHeaderDTO evalHeader = null;
        private bool isHPFUser;
        private bool isFirstTime;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int caseId = int.Parse(Request.QueryString["CaseID"].ToString());
                evalHeader = CaseEvaluationBL.Instance.GetCaseEvalHeaderByCaseId(caseId);
                isHPFUser = (HPFWebSecurity.CurrentIdentity.UserType==Constant.USER_TYPE_HPF?true:false);
                isFirstTime = (evalHeader.EvalStatus == CaseEvaluationBL.EvaluationStatus.AGENCY_INPUT_REQUIRED);
                if (evalHeader != null)
                {
                    InitControlStatus();
                    RenderHTML(evalHeader.CaseEvalHeaderId);
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void InitControlStatus()
        {
            btnUpdate.Enabled = false;
            btnSaveNew.Enabled = false;
            txtEvaluationDate.Visible = !isHPFUser;
            lblEvaluationDate.Visible = !isHPFUser;
            txtEvaluationDate.Enabled = ((isFirstTime) && (!isHPFUser));
            btnUpdate.Visible = ((isHPFUser) && (string.Compare(evalHeader.EvalStatus,CaseEvaluationBL.EvaluationStatus.CLOSED)!=0));
            btnCloseAudit.Visible = (((isHPFUser) && (string.Compare(evalHeader.EvalStatus, CaseEvaluationBL.EvaluationStatus.RESULT_WITHIN_RANGE) == 0))
                                    || ((isHPFUser) && (string.Compare(evalHeader.EvalStatus, CaseEvaluationBL.EvaluationStatus.HPF_INPUT_REQUIRED) == 0)
                                                  && (string.Compare(evalHeader.EvalType, CaseEvaluationBL.EvaluationType.ONSITE)==0)));

            btnCalculate.Visible = btnSaveNew.Visible = (string.Compare(evalHeader.EvalStatus, CaseEvaluationBL.EvaluationStatus.CLOSED) != 0);
        }
        private void RenderHTML(int? caseEvalHeaderId)
        {
            string hpfAuditInd = (isHPFUser ? Constant.INDICATOR_YES : Constant.INDICATOR_NO);
            CaseEvalSetDTO caseEvalSetLatest = CaseEvaluationBL.Instance.GetCaseEvalLatest(caseEvalHeaderId, hpfAuditInd);
            if (caseEvalSetLatest == null)
                RenderEvalSetNotExist(evalHeader.EvalTemplateId);
            else
                RenderEvalSetExist(caseEvalSetLatest);
        }
        /// <summary>
        /// Render html page with evaluation set is exist
        /// </summary>
        /// <param name="caseEvalSetLatest"></param>
        private void RenderEvalSetExist(CaseEvalSetDTO caseEvalSetLatest)
        {
            questionOrders = new List<string>();
            caseEvalDetailDraftCollection = caseEvalSetLatest.CaseEvalDetails;
            string prevSectionName="";
            foreach (CaseEvalDetailDTO evalDetail in caseEvalSetLatest.CaseEvalDetails)
            {
                //Render Section row
                if (string.Compare(prevSectionName, evalDetail.SectionName)!=0)
                    placeHolder.Controls.Add(RenderSectionRow(evalDetail.SectionName));
                //Render Question row
                placeHolder.Controls.Add(RenderQuestionRow(evalDetail.CaseEvalDetailId, evalDetail.QuestionOrder, evalDetail.EvalQuestion, evalDetail.QuestionExample, evalDetail.EvalAnswer,evalDetail.Comments));
                prevSectionName = evalDetail.SectionName;
            }
            lblAuditorName.Text = caseEvalSetLatest.AuditorName;
            DateTime evaluationDt = (DateTime)caseEvalSetLatest.EvaluationDt;
            txtEvaluationDate.Text = evaluationDt.ToShortDateString();
            txtComments.Text = caseEvalSetLatest.Comments;
            chkFatalError.Checked = (caseEvalSetLatest.FatalErrorInd == Constant.INDICATOR_YES ? true : false);
            //Render score, level, percent, ... again
            CalculateScore(caseEvalSetLatest);
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
                    if (q.EvalQuestionId == 7) Response.Write("");
                    placeHolder.Controls.Add(RenderQuestionRow(q.EvalQuestionId, sq.QuestionOrder, q.Question, q.QuestionExample,"",""));
                    //Save question data to draft list, use to insert new after
                    CaseEvalDetailDTO caseEvalDetailDraft = new CaseEvalDetailDTO();
                    caseEvalDetailDraft.EvalSectionId = ts.EvalSectionId;
                    caseEvalDetailDraft.SectionName = ts.EvalSection.SectionName;
                    caseEvalDetailDraft.SectionOrder = ts.SectionOrder;
                    caseEvalDetailDraft.EvalQuestionId = q.EvalQuestionId;
                    caseEvalDetailDraft.EvalQuestion = q.Question;
                    caseEvalDetailDraft.QuestionExample = q.QuestionExample;
                    caseEvalDetailDraft.QuestionOrder = sq.QuestionOrder;
                    caseEvalDetailDraft.QuestionScore = q.QuestionScore;
                    caseEvalDetailDraftCollection.Add(caseEvalDetailDraft);
                }
            }
            lblAuditorName.Text = HPFWebSecurity.CurrentIdentity.DisplayName;
            txtEvaluationDate.Text = DateTime.Now.ToShortDateString();
            btnCloseAudit.Visible = false;
        }
        /// <summary>
        /// Render html section row
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        private TableRow RenderSectionRow(string sectionName)
        {
            TableRow tr = new TableRow();
            tr.Attributes.Add("style","background:#CDC9C9");
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
        private TableRow RenderQuestionRow(int? id,int? order, string question, string questionExample, string answer,string comments)
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
                tc.Attributes.Add("align", "center");
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
            txtBox.Columns = 50;
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
                CaseEvaluationBL.Instance.SaveCaseEvalSet(evalHeader, caseEvalSetDraft, isHPFUser, HPFWebSecurity.CurrentIdentity.LoginName);
                lblErrorMessage.Text = "New Case Evaluation was inserted successfully!!!";
                SetEvaluationCaseStatus();
                if (string.Compare(evalHeader.EvalStatus, CaseEvaluationBL.EvaluationStatus.AGENCY_UPLOAD_REQUIRED)==0)
                    Response.Redirect(Request.Url.ToString());
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
            if (caseEvalDetailDraftCollection.Count > 0) result.CaseEvalSetId = caseEvalDetailDraftCollection[0].CaseEvalSetId;
            result.CaseEvalHeaderId = evalHeader.CaseEvalHeaderId;
            result.AuditorName = HPFWebSecurity.CurrentIdentity.DisplayName;
            result.HpfAuditInd = (string.Compare(HPFWebSecurity.CurrentIdentity.UserType, Constant.USER_TYPE_HPF) == 0 ? Constant.INDICATOR_YES : Constant.INDICATOR_NO);
            result.EvaluationDt = (((isFirstTime) && (!isHPFUser)) ? ConvertToDateTime(txtEvaluationDate.Text):DateTime.Now);
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
                //Do not permit update when HPF auditor audit for the first time
                btnUpdate.Enabled = (string.Compare(evalHeader.EvalStatus, CaseEvaluationBL.EvaluationStatus.HPF_INPUT_REQUIRED) != 0);
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
            int totalYesAnswer = 0;
            int totalNoAnswer = 0;
            int totalNAAnswer = 0;
            caseEvalSetDraft.CaseEvalDetails = CaseEvaluationBL.Instance.AssignAllQuestionScores(caseEvalSetDraft.CaseEvalDetails,ref totalYesAnswer,ref totalNoAnswer,ref totalNAAnswer);
            caseEvalSetDraft = CaseEvaluationBL.Instance.CalculateCaseTotalScore(caseEvalSetDraft,ref totalNoScore,ref totalNAScore);
            decimal percent = Math.Round((decimal)((decimal)caseEvalSetDraft.TotalAuditScore /(decimal)caseEvalSetDraft.TotalPossibleScore), 4);
            //Bind to layout
            lblYesScore.InnerText = caseEvalSetDraft.TotalAuditScore.ToString();
            lblNoScore.InnerText = totalNoScore.ToString();
            lblNAScore.InnerText = totalNAScore.ToString();
            lblLevelPercent.InnerText = percent.ToString("0.0%");
            lblLevelName.InnerText = caseEvalSetDraft.ResultLevel;
            lblYesTotal.InnerText = totalYesAnswer.ToString();
            lblNoTotal.InnerText = totalNoAnswer.ToString();
            lblNATotal.InnerText = totalNAAnswer.ToString();
            return caseEvalSetDraft;
        }
        private DateTime ConvertToDateTime(object obj)
        {
            DateTime dt;
            if (DateTime.TryParse(obj.ToString().Trim(), out dt))
                return dt;
            return DateTime.MinValue;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchQCSelectionCase.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                CaseEvalSetDTO caseEvalSetDraft = DraftCaseEvalSet();
                caseEvalSetDraft = CalculateScore(caseEvalSetDraft);
                CaseEvaluationBL.Instance.UpdateCaseEvalSet(evalHeader, caseEvalSetDraft, HPFWebSecurity.CurrentIdentity.LoginName);
                lblErrorMessage.Text = "Case Evaluation was updated successfully!!!";
                SetEvaluationCaseStatus();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }

        protected void btnCloseAudit_Click(object sender, EventArgs e)
        {
            try
            {
                evalHeader.EvalStatus = CaseEvaluationBL.EvaluationStatus.CLOSED;
                evalHeader.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                CaseEvaluationBL.Instance.UpdateCaseEvalHeader(evalHeader);
                lblErrorMessage.Text = "Evaluation Case was closed successfully!!!";
                SetEvaluationCaseStatus();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void SetEvaluationCaseStatus()
        {
            //Set status of evaluation case
            Label lblEvaluationStatus = this.Parent.FindControl("lblEvaluationStatus") as Label;
            if (lblEvaluationStatus != null)
                lblEvaluationStatus.Text = evalHeader.EvalStatus;
        }
        
    }
}