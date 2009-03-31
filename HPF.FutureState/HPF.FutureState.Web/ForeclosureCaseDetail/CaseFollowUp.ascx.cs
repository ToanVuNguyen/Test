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
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace HPF.FutureState.Web.ForeclosureCaseDetail
{
    public partial class CaseFollowUp : System.Web.UI.UserControl
    {
        private const string ACTION_UPDATE = "update";
        private const string ACTION_INSERT = "insert";
        CaseFollowUpDTOCollection caseFollowUps = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ApplySecurity();
                if (IsPostBack)
                {
                    ViewState["CaseID"] = Request.QueryString["CaseID"];
                    grdvCaseFollowUpBinding();

                    ddlFollowUpSourceBinding();
                    ddlCreditReportBureauBinding();
                    ddlOutcomeBinding();
                    ddlDelinquencyStatusBinding();
                    ddlStillInHomeBinding();                    

                    btn_Cancel.Attributes.Add("onclick", "return ConfirmEdit('" + msgWARN0450 + "','-2');");
                    btn_New.Attributes.Add("onclick", "return ConfirmEdit('" + msgWARN0450 + "','-1');");                                        
                }                
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex);
            }
        }

        #region BindDataToControl
        private void grdvCaseFollowUpBinding()
        {
            int caseID = int.Parse(ViewState["CaseID"].ToString());
            caseFollowUps = RetrieveCaseFollowUps(caseID);
            if (caseFollowUps.Count > 0)
            {
                grd_FollowUpList.DataSource = caseFollowUps;
                grd_FollowUpList.DataBind();
            }
            else
            {
                caseFollowUps.Add(new CaseFollowUpDTO());
                grd_FollowUpList.DataSource = caseFollowUps;
                grd_FollowUpList.DataBind();
                int TotalColumns = grd_FollowUpList.Rows[0].Cells.Count;
                grd_FollowUpList.Rows[0].Cells.Clear();
                grd_FollowUpList.Rows[0].Cells.Add(new TableCell());
                grd_FollowUpList.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grd_FollowUpList.Rows[0].Cells[0].Text = "No Records Found";
            }
        }

        //Bind data to follow-up source
        private void ddlFollowUpSourceBinding()
        {
            ddl_FollowUpSource.Items.Clear();
            RefCodeItemDTOCollection followUpTypeCodes = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_FOLLOW_UP_SOURCE_CODE);
            ddl_FollowUpSource.DataValueField = "Code";
            ddl_FollowUpSource.DataTextField = "CodeDesc";
            ddl_FollowUpSource.DataSource = followUpTypeCodes;
            ddl_FollowUpSource.DataBind();
            ddl_FollowUpSource.Items.Insert(0, new ListItem(string.Empty));
        }

        //Bind data to credir report bureau
        private void ddlCreditReportBureauBinding()
        {
            ddl_CreditReportBureau.Items.Clear();
            RefCodeItemDTOCollection followUpTypeCodes = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_CREDIR_BERREAU_CODE);
            ddl_CreditReportBureau.DataValueField = "Code";
            ddl_CreditReportBureau.DataTextField = "CodeDesc";
            ddl_CreditReportBureau.DataSource = followUpTypeCodes;
            ddl_CreditReportBureau.DataBind();
            ddl_CreditReportBureau.Items.Insert(0, new ListItem(string.Empty));
        }

        //Bind data to follow up outcome 
        private void ddlOutcomeBinding()
        {
            ddl_FollowUpOutcome.Items.Clear();
            OutcomeTypeDTOCollection followUpTypeCodes = CaseFollowUpBL.Instance.RetrieveOutcomeTypes();
            ddl_FollowUpOutcome.DataValueField = "OutcomeTypeID";
            ddl_FollowUpOutcome.DataTextField = "OutcomeTypeName";
            ddl_FollowUpOutcome.DataSource = followUpTypeCodes;
            ddl_FollowUpOutcome.DataBind();
            ddl_FollowUpOutcome.Items.Insert(0, new ListItem(string.Empty));
        }

        //Bind data to delinquncy status
        private void ddlDelinquencyStatusBinding()
        {
            ddl_DelinqencyStatus.Items.Clear();
            RefCodeItemDTOCollection followUpTypeCodes = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_LOAN_DELINQUENCY_CODE);
            ddl_DelinqencyStatus.DataValueField = "Code";
            ddl_DelinqencyStatus.DataTextField = "CodeDesc";
            ddl_DelinqencyStatus.DataSource = followUpTypeCodes;
            ddl_DelinqencyStatus.DataBind();
            ddl_DelinqencyStatus.Items.Insert(0, new ListItem(string.Empty));
        }

        //Bind data to still in home
        private void ddlStillInHomeBinding()
        {
            ddl_StillInHome.Items.Clear();
            ddl_StillInHome.Items.Add(new ListItem(string.Empty));
            ddl_StillInHome.Items.Add(new ListItem(Constant.INDICATOR_YES_FULL));
            ddl_StillInHome.Items.Add(new ListItem(Constant.INDICATOR_NO_FULL));
        }

        protected void grd_FollowUpList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            int idxIdColumn = 0;
            e.Row.Cells[idxIdColumn].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button _button = (Button)e.Row.FindControl("btn_Edit");
                if (_button != null)
                {
                    _button.Attributes.Add("onclick", "return ConfirmEdit('" + msgWARN0450 + "', '"+ e.Row.DataItemIndex+"');");
                    _button.Click += new EventHandler(btn_Edit_Click);
                    _button.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }        

        private CaseFollowUpDTOCollection RetrieveCaseFollowUps(int fcid)
        {
            return CaseFollowUpBL.Instance.RetrieveCaseFollowUps(fcid);
        }   
        #endregion
        
        #region ButtonClick
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            DoSaving();
        }
        
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {            
            grd_FollowUpList.SelectedIndex = -1;
            ClearControls();
        }        

        protected void btn_New_Click(object sender, EventArgs e)
        {
            hfAction.Value = ACTION_INSERT;
            grd_FollowUpList.SelectedIndex = -1;
            GenerateDefaultData();
            txt_FollowUpDt.Focus();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            Button btnEdit = sender as Button;            

            int index = int.Parse(btnEdit.CommandArgument.ToString());
            grd_FollowUpList.SelectedIndex = index;
            BindingSelectedData(index);
        }

        private void BindingSelectedData(int index)
        {
            CaseFollowUpDTO caseFollowUp = ((CaseFollowUpDTOCollection)grd_FollowUpList.DataSource)[index];
            Session[Constant.SS_CASE_FOLLOW_UP_OBJECT] = caseFollowUp;
            CaseFollowUpDTOToForm(caseFollowUp);
            hfAction.Value = ACTION_UPDATE;
            txt_FollowUpDt.Focus();
        }        

        private void ClearControls()
        {
            foreach (DropDownList ddl in this.Controls.OfType<DropDownList>())
                ddl.Text = string.Empty;
            txt_CreditReportDt.Text = string.Empty;
            txt_CreditScore.Text = string.Empty;
            txt_FollowUpComment.Text = string.Empty;
            txt_FollowUpDt.Text = string.Empty;
            hfAction.Value = null;
        }

        protected bool DoSaving()
        {
            if (string.IsNullOrEmpty(hfAction.Value))
            {
                return false;
            }
            var caseFollowUp = CreateCaseFollowUpDTO();
            var msgFollowUp = ValidateFollowUpDTO(caseFollowUp);
            if (msgFollowUp.Count == 0)
            {
                string logInName = HPFWebSecurity.CurrentIdentity.LoginName;
                if (hfAction.Value == ACTION_UPDATE)
                    CaseFollowUpBL.Instance.SaveCaseFollowUp(caseFollowUp, logInName, true);
                else
                    CaseFollowUpBL.Instance.SaveCaseFollowUp(caseFollowUp, logInName, false);
                grdvCaseFollowUpBinding();
                ClearControls();
            }
            else
            {
                ArrayList err = new ArrayList();
                foreach (ExceptionMessage item in msgFollowUp)
                {
                    err.Add(item.Message);
                }
                errorList.DataSource = err;
                errorList.DataBind();
                return false;
            }

            return true;
        }
        #endregion

        #region GenerateData
        private CaseFollowUpDTO CreateCaseFollowUpDTO()
        {
            int? id = null;
            if (Session[Constant.SS_CASE_FOLLOW_UP_OBJECT] != null)
                id = ((CaseFollowUpDTO)Session[Constant.SS_CASE_FOLLOW_UP_OBJECT]).CasePostCounselingStatusId;
            CaseFollowUpDTO caseFollowUp = new CaseFollowUpDTO();

            caseFollowUp.CasePostCounselingStatusId = id;

            if (ViewState["CaseID"] != null && !string.IsNullOrEmpty(ViewState["CaseID"].ToString()))
                caseFollowUp.FcId = int.Parse(ViewState["CaseID"].ToString());
            else
                caseFollowUp.FcId = null;

            if (!string.IsNullOrEmpty(ddl_FollowUpOutcome.SelectedValue))
                caseFollowUp.OutcomeTypeId = int.Parse(ddl_FollowUpOutcome.SelectedValue);
            else
                caseFollowUp.OutcomeTypeId = null;

            if (!string.IsNullOrEmpty(txt_FollowUpDt.Text))
                caseFollowUp.FollowUpDt = ConvertToDateTime(txt_FollowUpDt.Text);
            else
                caseFollowUp.FollowUpDt = null;

            caseFollowUp.FollowUpComment = txt_FollowUpComment.Text;
            caseFollowUp.FollowUpSourceCd = ddl_FollowUpSource.SelectedValue;
            caseFollowUp.LoanDelinqStatusCd = ddl_DelinqencyStatus.SelectedValue;
            caseFollowUp.StillInHouseInd = ddl_StillInHome.SelectedValue;
            caseFollowUp.CreditScore = txt_CreditScore.Text;
            caseFollowUp.CreditBureauCd = ddl_CreditReportBureau.SelectedValue;

            if (!string.IsNullOrEmpty(txt_CreditReportDt.Text))
                caseFollowUp.CreditReportDt = ConvertToDateTime(txt_CreditReportDt.Text);
            else
                caseFollowUp.CreditReportDt = null;

            return caseFollowUp;
        }
        
        private void CaseFollowUpDTOToForm(CaseFollowUpDTO caseFollowUp)
        {
            if (caseFollowUp != null)
            {
                txt_FollowUpDt.Text = (caseFollowUp.FollowUpDt.HasValue) ? caseFollowUp.FollowUpDt.Value.Date.ToString("MM/dd/yyyy") : string.Empty;
                txt_CreditScore.Text = caseFollowUp.CreditScore;
                ddl_FollowUpSource.SelectedIndex = ddl_FollowUpSource.Items.IndexOf(ddl_FollowUpSource.Items.FindByValue(caseFollowUp.FollowUpSourceCd));
                ddl_CreditReportBureau.SelectedIndex = ddl_CreditReportBureau.Items.IndexOf(ddl_CreditReportBureau.Items.FindByValue(caseFollowUp.CreditBureauCd));
                ddl_FollowUpOutcome.SelectedIndex = ddl_FollowUpOutcome.Items.IndexOf(ddl_FollowUpOutcome.Items.FindByValue(caseFollowUp.OutcomeTypeId.ToString()));
                txt_CreditReportDt.Text = (caseFollowUp.CreditReportDt.HasValue) ? caseFollowUp.CreditReportDt.Value.Date.ToString("MM/dd/yyyy") : string.Empty;
                ddl_DelinqencyStatus.SelectedIndex = ddl_DelinqencyStatus.Items.IndexOf(ddl_DelinqencyStatus.Items.FindByValue(caseFollowUp.LoanDelinqStatusCd));
                txt_FollowUpComment.Text = caseFollowUp.FollowUpComment;
                ddl_StillInHome.SelectedIndex = ddl_StillInHome.Items.IndexOf(ddl_StillInHome.Items.FindByText(GetIndicatorLongValue(caseFollowUp.StillInHouseInd)));
            }
        }

        private void GenerateDefaultData()
        {
            txt_FollowUpDt.Text = DateTime.Now.ToString("MM/dd/yyyy");
            ddl_FollowUpSource.SelectedIndex = ddl_FollowUpSource.Items.IndexOf(ddl_FollowUpSource.Items.FindByValue("HPF"));
            ddl_FollowUpOutcome.SelectedIndex = ddl_FollowUpOutcome.Items.IndexOf(ddl_FollowUpOutcome.Items.FindByText(string.Empty));
            ddl_DelinqencyStatus.SelectedIndex = ddl_DelinqencyStatus.Items.IndexOf(ddl_DelinqencyStatus.Items.FindByText(string.Empty));
            ddl_StillInHome.SelectedIndex = ddl_StillInHome.Items.IndexOf(ddl_StillInHome.Items.FindByText(string.Empty));
            txt_CreditScore.Text = string.Empty;
            ddl_CreditReportBureau.SelectedIndex = ddl_CreditReportBureau.Items.IndexOf(ddl_CreditReportBureau.Items.FindByText(string.Empty));
            txt_CreditReportDt.Text = string.Empty;
            txt_FollowUpComment.Text = string.Empty;
        }
        #endregion

        #region Sercurity
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                SetControlEnable(false);
            }
            else
            {
                SetControlEnable(true);
            }
        }

        private void SetControlEnable(bool status)
        {
            btn_New.Enabled = status;
            btn_Save.Enabled = status;
            btn_Cancel.Enabled = status;
            txt_FollowUpDt.Enabled = status;
            ddl_FollowUpSource.Enabled = status;
            ddl_FollowUpOutcome.Enabled = status;
            ddl_DelinqencyStatus.Enabled = status;
            ddl_StillInHome.Enabled = status;
            txt_CreditScore.Enabled = status;
            ddl_CreditReportBureau.Enabled = status;
            txt_CreditReportDt.Enabled = status;
            txt_FollowUpComment.Enabled = status;
        }
        #endregion

        protected void btnYes_Click(object sender, EventArgs e)
        {
            if (!DoSaving())
                selTabCtrl.Value = string.Empty;
            else
                UpdateUI();
        }
        protected void btnNo_Click(object sender, EventArgs e)
        {
            UpdateUI();
        }
        private void UpdateUI()
        {
            if (selRow.Value == "-1")
            {
                hfAction.Value = ACTION_INSERT;
                grd_FollowUpList.SelectedIndex = -1;
                GenerateDefaultData();
                txt_FollowUpDt.Focus();
            }
            else if (selRow.Value == "-2")
            {
                grd_FollowUpList.SelectedIndex = -1;
                ClearControls();                
            }
            else if (selRow.Value != string.Empty && selRow.Value.ToUpper() != "UNDEFINED")
            {
                grd_FollowUpList.SelectedIndex = int.Parse(selRow.Value);
                BindingSelectedData(grd_FollowUpList.SelectedIndex);
            }

            selRow.Value = string.Empty;
        }
        #region ValidateData
        private ExceptionMessageCollection ValidateFollowUpDTO(CaseFollowUpDTO followUp)
        {
            ExceptionMessageCollection msgFolowUp = new ExceptionMessageCollection();
            ValidationResults validResults = HPFValidator.Validate<CaseFollowUpDTO>(followUp, Constant.RULESET_FOLLOW_UP);
            DataValidationException dataValidExp = new DataValidationException();
            string errorCode = "";
            string errorMessage = "";
            if (!validResults.IsValid)
            {
                foreach (var validResult in validResults)
                {
                    errorCode = string.IsNullOrEmpty(validResult.Tag) ? "ERROR" : validResult.Tag;
                    errorMessage = string.IsNullOrEmpty(validResult.Tag) ? validResult.Message : ErrorMessages.GetExceptionMessageCombined(validResult.Tag);
                    //dataValidExp.ExceptionMessages.AddExceptionMessage(errorCode, errorMessage);
                    msgFolowUp.AddExceptionMessage(errorCode, errorMessage);
                }
            }
            
            //var msgFolowUp = new ExceptionMessageCollection { HPFValidator.ValidateToGetExceptionMessage(followUp, Constant.RULESET_FOLLOW_UP) };
            if (followUp.FollowUpSourceCd.ToUpper().Trim() == Constant.FOLLOW_UP_CD_CREDIT_REPORT)
            {
                if (string.IsNullOrEmpty(followUp.CreditScore) || string.IsNullOrEmpty(followUp.CreditBureauCd) || followUp.CreditReportDt == null)
                    msgFolowUp.AddExceptionMessage(ErrorMessages.ERR0700, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0700));
            }
            return msgFolowUp;
        }
        #endregion

        #region Utility
        private DateTime ConvertToDateTime(object obj)
        {
            DateTime dt;
            DateTime.TryParse(obj.ToString(), out dt);
            return dt;
        }

        private string GetIndicatorLongValue(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.ToUpper() == Constant.INDICATOR_YES.ToUpper())
                    return Constant.INDICATOR_YES_FULL;
                return Constant.INDICATOR_NO_FULL;
            }
            return string.Empty;
        }
        
        public string msgWARN0450
        {
            get
            {                
                return ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0450);                
            }            
        }                      
        #endregion
    }
}