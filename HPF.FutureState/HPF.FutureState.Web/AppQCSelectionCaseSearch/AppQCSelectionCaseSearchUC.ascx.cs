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
using System.Text;
using HPF.FutureState.Common;

namespace HPF.FutureState.Web.ApphQCSelectionCaseSearch
{
    public partial class AppQCSelectionCaseSearchUC : System.Web.UI.UserControl
    {
        #region Properties
        private const int MONTHS = 24;
        protected CaseEvalSearchResultDTOCollection searchResult
        {
            get { return (CaseEvalSearchResultDTOCollection)ViewState["SearchResult"]; }
            set { ViewState["SearchResult"] = value; }
        }
        //total records in one page, get this info from web config
        protected int PageSize
        {
            get { return (int.Parse(HPFConfigurationSettings.APP_EVALUATIONCASE_PAGE_SIZE)); }
        }
        //total rows of search data
        protected double TotalRowNum
        {
            get { return Convert.ToDouble(ViewState["totalrownum"]); }
            set { ViewState["totalrownum"] = value; }
        }
        //current page
        protected int PageNum
        {
            get { return grvCaseEvalList.PageIndex; }
            set { grvCaseEvalList.PageIndex = value; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearErrorMessages();
            grvCaseEvalList.PageSize = this.PageSize;
            if (!IsPostBack)
            {
                BindMonthYearDropDownList();
                BindAgencyDropDownList();
                BindEvalStatusDropDownList();
                BindEvalTypeDropDownList();
            }
            else
            {
                double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
                GeneratePages(totalpage);
            }
        }
        /// <summary>
        ///Display data in dropdownlist 
        /// </summary>
        protected void BindAgencyDropDownList()
        {
            try
            {
                AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgencies();
                ddlAgency.DataValueField = "AgencyID";
                ddlAgency.DataTextField = "AgencyName";
                if (string.Compare(HPFWebSecurity.CurrentIdentity.UserType, Constant.USER_TYPE_HPF) == 0)
                {
                    ddlAgency.DataSource = agencyCollection;
                    ddlAgency.DataBind();
                    ddlAgency.Items.RemoveAt(ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue("-1")));
                    ddlAgency.Items.Insert(0, new ListItem("All Agencies", "-1"));
                    ddlAgency.Items.FindByText("All Agencies").Selected = true;
                }
                else if (string.Compare(HPFWebSecurity.CurrentIdentity.UserType, Constant.USER_TYPE_AGENCY) == 0)
                {
                    AgencyDTO agency = agencyCollection.FirstOrDefault(o => o.AgencyID == HPFWebSecurity.CurrentIdentity.AgencyId.ToString());
                    agencyCollection = new AgencyDTOCollection();
                    agencyCollection.Add(agency);
                    ddlAgency.DataSource = agencyCollection;
                    ddlAgency.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void BindEvalStatusDropDownList()
        {
            ddlEvaluationStatus.Items.Add(new ListItem("All","-1"));
            ddlEvaluationStatus.Items.Add(new ListItem(CaseEvaluationBL.EvaluationStatus.AGENCY_INPUT_REQUIRED, CaseEvaluationBL.EvaluationStatus.AGENCY_INPUT_REQUIRED));
            ddlEvaluationStatus.Items.Add(new ListItem(CaseEvaluationBL.EvaluationStatus.AGENCY_UPLOAD_REQUIRED, CaseEvaluationBL.EvaluationStatus.AGENCY_UPLOAD_REQUIRED));
            ddlEvaluationStatus.Items.Add(new ListItem(CaseEvaluationBL.EvaluationStatus.HPF_INPUT_REQUIRED, CaseEvaluationBL.EvaluationStatus.HPF_INPUT_REQUIRED));
            ddlEvaluationStatus.Items.Add(new ListItem(CaseEvaluationBL.EvaluationStatus.RECON_REQUIRED_AGENCY_INPUT, CaseEvaluationBL.EvaluationStatus.RECON_REQUIRED_AGENCY_INPUT));
            ddlEvaluationStatus.Items.Add(new ListItem(CaseEvaluationBL.EvaluationStatus.RECON_REQUIRED_HPF_INPUT, CaseEvaluationBL.EvaluationStatus.RECON_REQUIRED_HPF_INPUT));
            ddlEvaluationStatus.Items.Add(new ListItem(CaseEvaluationBL.EvaluationStatus.RESULT_WITHIN_RANGE, CaseEvaluationBL.EvaluationStatus.RESULT_WITHIN_RANGE));
            ddlEvaluationStatus.Items.Add(new ListItem(CaseEvaluationBL.EvaluationStatus.CLOSED, CaseEvaluationBL.EvaluationStatus.CLOSED));
        }
        private void BindEvalTypeDropDownList()
        {
            ddlEvaluationType.Items.Add(new ListItem("All", "-1"));
            ddlEvaluationType.Items.Add(new ListItem(CaseEvaluationBL.EvaluationType.ONSITE, CaseEvaluationBL.EvaluationType.ONSITE));
            ddlEvaluationType.Items.Add(new ListItem(CaseEvaluationBL.EvaluationType.DESKTOP, CaseEvaluationBL.EvaluationType.DESKTOP));
        }
        /// <summary>
        /// Display MonthYear dropdownlist for From and To
        /// </summary>
        private void BindMonthYearDropDownList()
        {
            DateTime dt;
            StringBuilder text;
            StringBuilder value;
            for (int i = 0; i < MONTHS; i++)
            {
                text = new StringBuilder();
                value = new StringBuilder();
                dt = DateTime.Now.AddMonths(0-i);
                text.AppendFormat("{0}-{1}",dt.ToString("MM"),dt.ToString("yyyy"));
                value.AppendFormat("{0}{1}",dt.ToString("yyyy"),dt.ToString("MM"));
                ddlYearMonthFrom.Items.Add(new ListItem(text.ToString(),value.ToString()));
                ddlYearMonthTo.Items.Add(new ListItem(text.ToString(), value.ToString()));
            }
            string prevMonth = DateTime.Now.AddMonths(-1).ToString("MM") + "-" + DateTime.Now.AddMonths(-1).ToString("yyyy");
            ddlYearMonthFrom.Items.FindByText(prevMonth).Selected = true;
        }
        private int ConvertToInt(object obj)
        {
            int value;
            if (int.TryParse(obj.ToString().Trim(), out value))
                return value;
            return int.MinValue;
        }
        private void ClearErrorMessages()
        {
            lblErrorMessage.Items.Clear();
        }
        private CaseEvalSearchCriteriaDTO GetSearchCriteria()
        {
            CaseEvalSearchCriteriaDTO caseEvalCriteria = new CaseEvalSearchCriteriaDTO();
            caseEvalCriteria.AgencyId = ConvertToInt(ddlAgency.SelectedValue);
            caseEvalCriteria.YearMonthFrom = ddlYearMonthFrom.SelectedValue;
            caseEvalCriteria.YearMonthTo = ddlYearMonthTo.SelectedValue;
            caseEvalCriteria.EvaluationStatus = ddlEvaluationStatus.SelectedValue;
            caseEvalCriteria.EvaluationType = ddlEvaluationType.SelectedValue;
            return caseEvalCriteria;
        }
        private void CaseEvalSearch(CaseEvalSearchCriteriaDTO searchCriteria)
        {
            searchResult = CaseEvaluationBL.Instance.SearchCaseEval(searchCriteria);
            grvCaseEvalList.DataSource = searchResult;
            grvCaseEvalList.DataBind();
            if (searchResult.Count > 0)
            {
                
                btnEditCase.Visible = true;
                CalculatePaging(searchResult.Count);
            }
            else
            {
                btnEditCase.Visible = false;
                ShowHidePagingControl(false);
                lblErrorMessage.Items.Add(new ListItem("No case evaluation found !!!"));
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMessages();
                CaseEvalSearchCriteriaDTO searchCriteria = GetSearchCriteria();
                CaseEvalSearch(searchCriteria);
            }
            catch (DataValidationException ex)
            {
                lblErrorMessage.DataSource = ex.ExceptionMessages;
                lblErrorMessage.DataBind();
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                grvCaseEvalList.DataSource = null;
                grvCaseEvalList.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                grvCaseEvalList.DataSource = null;
                grvCaseEvalList.DataBind();
            }
        }

        protected void btnEditCase_Click(object sender, EventArgs e)
        {
            ClearErrorMessages();
            if (grvCaseEvalList.SelectedValue == null)
            {
                //lblErrorMessage.Items.Add(new ListItem(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0568)));
                return;
            }
            int fcId = (int)grvCaseEvalList.SelectedValue;
            Response.Redirect("QCSelectionCaseInfo.aspx?caseId=" + fcId.ToString());
        }
        #region Paging
        private void CalculatePaging(double searchResultCount)
        {
            this.TotalRowNum = searchResultCount;
            double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
            if (totalpage > 1)
            {
                GeneratePages(totalpage);
                lblTemp.Text = "1";
                ShowHidePagingControl(true);
                int MinRow = this.PageSize * PageNum + 1;
                int MaxRow = (PageNum + 1) * this.PageSize;
                lblTotalRowNum.Text = this.TotalRowNum.ToString();
                lblMinRow.Text = MinRow.ToString();
                lblMaxRow.Text = MaxRow.ToString();
                if (MaxRow > this.TotalRowNum)
                    lblMaxRow.Text = this.TotalRowNum.ToString();
                else lblMaxRow.Text = MaxRow.ToString();
            }
            else
                ShowHidePagingControl(false);
        }

        private void GeneratePages(double totalpage)
        {
            phPages.Controls.Clear();
            for (int i = 1; i <= totalpage; i++)
            {
                LinkButton myLinkBtn = new LinkButton();
                myLinkBtn.ID = i.ToString();
                myLinkBtn.Text = i.ToString();
                //the first time you click searh button or choosen page. disable this page.
                if (i == this.PageNum + 1)
                {
                    myLinkBtn.CssClass = "PageChoose";
                    myLinkBtn.Enabled = false;
                }
                else
                {
                    myLinkBtn.CssClass = "UnderLine";
                }
                myLinkBtn.CommandName = i.ToString();
                myLinkBtn.Command += new CommandEventHandler(myLinkBtn_Command);
                myLinkBtn.Attributes.Add("onclick", "ShowWaitPanel();");
                phPages.Controls.Add(myLinkBtn);
                //add spaces beetween pages link button.
                Literal lit = new Literal();
                lit.Text = "&nbsp;&nbsp;";
                phPages.Controls.Add(lit);
            }

            if (totalpage == 1)
            {
                lbtnLast.Enabled = false;
                lbtnNext.Enabled = false;
            }
            else
            {
                if (this.PageNum < totalpage - 1)
                {
                    lbtnLast.Enabled = true;
                    lbtnNext.Enabled = true;
                }
            }
        }

        protected void ShowHidePagingControl(bool isEnable)
        {
            lbl1.Visible = isEnable;
            lbl2.Visible = isEnable;
            lblMaxRow.Visible = isEnable;
            lblMinRow.Visible = isEnable;
            lblTotalRowNum.Visible = isEnable;
            lbtnFirst.Visible = isEnable;
            lbtnLast.Visible = isEnable;
            lbtnNext.Visible = isEnable;
            lbtnPrev.Visible = isEnable;
            phPages.Visible = isEnable;
        }
        /// <summary>
        /// when click on button:  << < > >>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnNavigate_Click(object sender, CommandEventArgs e)
        {
            double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
            switch (e.CommandName)
            {
                // button: <<
                case "First":
                    this.PageNum = 0;
                    lbtnFirst.Enabled = false;
                    lbtnPrev.Enabled = false;
                    break;
                // button: >>
                case "Last":
                    lbtnLast.Enabled = false;
                    lbtnNext.Enabled = false;
                    lbtnFirst.Enabled = true;
                    lbtnPrev.Enabled = true;
                    this.PageNum = (int)totalpage - 1;
                    break;
                // button: >
                case "Next":
                    this.PageNum++;
                    lbtnFirst.Enabled = true;
                    lbtnLast.Enabled = true;
                    lbtnPrev.Enabled = true;

                    if (this.PageNum == totalpage - 1)
                    {
                        lbtnNext.Enabled = false;
                        lbtnLast.Enabled = false;
                    }

                    break;
                // button: <
                case "Prev":
                    this.PageNum--;
                    lbtnFirst.Enabled = true;
                    lbtnLast.Enabled = true;
                    lbtnNext.Enabled = true;
                    if (this.PageNum == 0)
                    {
                        lbtnPrev.Enabled = false;
                        lbtnFirst.Enabled = false;
                    }
                    break;
            }

            ShowHidePagingControl(true);

            grvCaseEvalList.DataSource = (CaseEvalSearchResultDTOCollection)ViewState["SearchResult"];
            grvCaseEvalList.DataBind();
            CalculatePaging(this.TotalRowNum);
        }

        void myLinkBtn_Command(object sender, CommandEventArgs e)
        {
            double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
            int pagenum = int.Parse(e.CommandName);
            this.PageNum = pagenum - 1;

            lbtnFirst.Enabled = true;
            lbtnLast.Enabled = true;
            lbtnNext.Enabled = true;
            lbtnPrev.Enabled = true;

            if (pagenum == 1)
            {
                lbtnFirst.Enabled = false;
                lbtnPrev.Enabled = false;
            }
            if (pagenum == totalpage)
            {
                lbtnLast.Enabled = false;
                lbtnNext.Enabled = false;
            }

            ShowHidePagingControl(true);

            grvCaseEvalList.DataSource = (CaseEvalSearchResultDTOCollection)ViewState["SearchResult"];
            grvCaseEvalList.DataBind();
            CalculatePaging(this.TotalRowNum);
        }
        #endregion
        
    }
}