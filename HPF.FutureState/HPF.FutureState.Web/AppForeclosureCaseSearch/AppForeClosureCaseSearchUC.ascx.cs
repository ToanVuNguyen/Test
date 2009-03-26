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
using System.Collections.Generic;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common;
namespace HPF.FutureState.Web.AppForeClosureCaseSearch
{

    public partial class AppForeClosureCaseSearchUC : System.Web.UI.UserControl
    {
        #region Properties
        //session store search criteria
        protected AppForeclosureCaseSearchCriteriaDTO SearchCriteria
        {
            get
            {
                return (AppForeclosureCaseSearchCriteriaDTO)Session["searchcriteria"];
            }
            set { AppForeclosureCaseSearchCriteriaDTO searchcriteria = value; }
        }
        //total records in one page, get this info from web config
        protected int PageSize
        {
            get { return (int.Parse(HPFConfigurationSettings.APP_FORECLOSURECASE_PAGE_SIZE)); }
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
            get { return Convert.ToInt16(ViewState["pagenum"]); }
            set { ViewState["pagenum"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            lblResult.Visible = false;
            myPannel.Visible = false;
            if (!IsPostBack)
            {
                ApplySecurity();
                //Bind data to dropdownlist.
                BindProgramDropdownlist();
                BindStateDropdownlist();
                BindAgencyDropdownlist();
                BindServicerDropDownList();
                //redisplay search criteria when you click on menu item.
                this.PageNum = 1;
                BindSearchCriteria();                
            }
            else
            {
                if (lblTemp.Text == "1")
                {
                    double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
                    GeneratePages(totalpage);
                }
            }
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
        }
        /// <summary>
        /// Redisplay search criteria.
        /// </summary>

        #region Binding Data
        protected void BindStateDropdownlist()
        {
            RefCodeItemDTOCollection stateCol = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_STATE_CODE);
            //Bind data
            ddlPropertyState.DataValueField = "Code";
            ddlPropertyState.DataTextField = "CodeDesc";
            ddlPropertyState.DataSource = stateCol;
            ddlPropertyState.DataBind();
            ddlPropertyState.Items.Insert(0, new ListItem("ALL", "ALL"));
        }
        protected void BindAgencyDropdownlist()
        {
            AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgency();
            ddlAgency.DataValueField = "AgencyID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = agencyCollection;
            ddlAgency.DataBind();
            ddlAgency.Items.RemoveAt(ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue("-1")));
            ddlAgency.Items.Insert(0, new ListItem("ALL", "-1"));
            ddlAgency.Items.FindByText("ALL").Selected = true;
        }
        protected void BindProgramDropdownlist()
        {
            ProgramDTOCollection programCollection = LookupDataBL.Instance.GetProgram();
            ddlProgram.DataValueField = "ProgramID";
            ddlProgram.DataTextField = "ProgramName";
            ddlProgram.DataSource = programCollection;
            ddlProgram.DataBind();
            //selected item of drop down list : all
            ddlProgram.Items.RemoveAt(ddlProgram.Items.IndexOf(ddlProgram.Items.FindByValue("-1")));
            ddlProgram.Items.Insert(0, new ListItem("ALL", "-1"));
            ddlProgram.Items.FindByText("ALL").Selected = true;
        }
        protected void BindServicerDropDownList()
        {
            ServicerDTOCollection servicerCollection = LookupDataBL.Instance.GetServicer();
            ddlServicer.DataValueField = "ServicerID";
            ddlServicer.DataTextField = "ServicerName";
            ddlServicer.DataSource = servicerCollection;
            ddlServicer.DataBind();
            ddlServicer.Items.RemoveAt(ddlServicer.Items.IndexOf(ddlServicer.Items.FindByValue("-1")));
            ddlServicer.Items.Insert(0, new ListItem("ALL", "-1"));
            ddlServicer.Items.FindByText("ALL").Selected = true;
        }
        protected void BindSearchCriteria()
        {
            try
            {
                if (Session["searchcriteria"] != null)
                {
                    AppForeclosureCaseSearchCriteriaDTO searchcriteria = (AppForeclosureCaseSearchCriteriaDTO)Session["searchcriteria"];
                    //if (currentUserID == searchcriteria.UserID)
                    {
                        //set search control the values from session searchcriteria.               

                        //bind criteria
                        txtLastName.Text = searchcriteria.LastName;
                        txtFirstName.Text = searchcriteria.FirstName;
                        txtAgencyCaseID.Text = searchcriteria.AgencyCaseID;
                        if (searchcriteria.ForeclosureCaseID == -1)
                            txtForeclosureCaseID.Text = "";
                        else txtForeclosureCaseID.Text = searchcriteria.ForeclosureCaseID.ToString();
                        txtLoanNum.Text = searchcriteria.LoanNumber;
                        txtPropertyZip.Text = searchcriteria.PropertyZip;
                        txtSSN.Text = searchcriteria.Last4SSN;
                        ddlAgency.SelectedValue = searchcriteria.Agency.ToString();
                        ddlDup.SelectedValue = searchcriteria.Duplicates;
                        ddlProgram.SelectedValue = searchcriteria.Program.ToString();
                        ddlPropertyState.SelectedValue = searchcriteria.PropertyState;
                        ddlServicer.SelectedValue = searchcriteria.Servicer.ToString();

                        SearchForeclosureCase(searchcriteria);
                    }
                }
            }
            catch (DataValidationException Exdata)
            {
                bulErrorMessage.DataSource = Exdata.ExceptionMessages;
                bulErrorMessage.DataBind();
            }
            catch (Exception ex)
            {
                bulErrorMessage.Items.Add(ex.Message);
            }
        }

        protected void grvForeClosureCaseSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblcounseled = e.Row.FindControl("lblCounseled") as Label;
                if (lblcounseled.Text == string.Empty)
                    lblcounseled.Text = "<1 yr";
                else
                {
                    DateTime datecounseled = DateTime.Parse(lblcounseled.Text);
                    //datecompare is early than today 1 year.
                    DateTime datecompare = DateTime.Today.AddYears(-1);
                    int result = DateTime.Compare(datecompare, datecounseled);
                    //datecompare is later than datecounseled--> counseled <1 yr
                    if (result <= 0)
                        lblcounseled.Text = "<1 yr";
                    //datecompare is earlier than datecounseled--> counseled >1 yr
                    else
                        lblcounseled.Text = ">1 yr";
                }
            }
        }
        #endregion

        private void SearchForeclosureCase(AppForeclosureCaseSearchCriteriaDTO appFCSearchCriteriaDTO)
        {
            var searchResult = ForeclosureCaseBL.Instance.AppSearchforeClosureCase(appFCSearchCriteriaDTO);
            bulErrorMessage.Items.Clear();

            if (searchResult.Count > 0)
            {
                lblResult.Visible = true;
                myPannel.Visible = true;
                grvForeClosureCaseSearch.DataSource = searchResult;
                grvForeClosureCaseSearch.DataBind();

                CalculatePaging(searchResult.SearchResultCount);                
            }
            else
            {
                bulErrorMessage.Items.Add(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0504));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {            
            try
            {                                
                AppForeclosureCaseSearchCriteriaDTO fcDTO = GetAppForeclosureCaseSearchCriteriaDTO(1);
                Session["searchcriteria"] = fcDTO;

                bulErrorMessage.Items.Clear();                
                lblTemp.Text = " ";

                SearchForeclosureCase(fcDTO);
            }
            catch (DataValidationException Exdata)
            {
                bulErrorMessage.DataSource = Exdata.ExceptionMessages;
                bulErrorMessage.DataBind();
            }
            catch (Exception ex)
            {
                bulErrorMessage.Items.Add(ex.Message);                
            }
        }

        private AppForeclosureCaseSearchCriteriaDTO GetAppForeclosureCaseSearchCriteriaDTO(int PageNum)
        {
            var appForeclosureCaseSearchCriteriaDTO = new AppForeclosureCaseSearchCriteriaDTO();
            
            string textchangeFirstName = "";
            string textchangeLastName = "";

            //replace * by % to search like in fristname
            if (txtFirstName.Text != string.Empty)           
                textchangeFirstName = Replace1Char(txtFirstName.Text, "*", "%");
            
            //replace * by % to search like in fristname
            if (txtLastName.Text != string.Empty)           
                textchangeLastName = Replace1Char(txtLastName.Text, "*", "%");
            
            appForeclosureCaseSearchCriteriaDTO.LastName = txtLastName.Text.Trim() == string.Empty ? null : AddToSearchSpecialChar(textchangeLastName.Trim());
            appForeclosureCaseSearchCriteriaDTO.FirstName = txtFirstName.Text.Trim() == string.Empty ? null : AddToSearchSpecialChar(textchangeFirstName.Trim());            
            int temp = -1;
            if (txtForeclosureCaseID.Text.Trim() != string.Empty)
                int.TryParse(txtForeclosureCaseID.Text.Trim(), out temp);
            if (temp == 0) temp = -2;
            appForeclosureCaseSearchCriteriaDTO.ForeclosureCaseID = temp;         
            appForeclosureCaseSearchCriteriaDTO.AgencyCaseID = txtAgencyCaseID.Text.Trim() == string.Empty ? null : txtAgencyCaseID.Text.Trim();
            appForeclosureCaseSearchCriteriaDTO.LoanNumber = DeleteSpecialChar(txtLoanNum.Text.Trim()) == string.Empty ? null : DeleteSpecialChar(txtLoanNum.Text.Trim());
            appForeclosureCaseSearchCriteriaDTO.PropertyZip = txtPropertyZip.Text.Trim() == string.Empty ? null : txtPropertyZip.Text.Trim();
            appForeclosureCaseSearchCriteriaDTO.Last4SSN = txtSSN.Text.Trim() == string.Empty ? null : txtSSN.Text.Trim();
            appForeclosureCaseSearchCriteriaDTO.PropertyState = ddlPropertyState.SelectedValue == "ALL" ? null : ddlPropertyState.SelectedValue.Trim();
            appForeclosureCaseSearchCriteriaDTO.Duplicates = ddlDup.SelectedValue.ToString() == string.Empty ? null : ddlDup.SelectedValue.ToString();
            appForeclosureCaseSearchCriteriaDTO.Agency = int.Parse(ddlAgency.SelectedValue);
            appForeclosureCaseSearchCriteriaDTO.Program = int.Parse(ddlProgram.SelectedValue);
            appForeclosureCaseSearchCriteriaDTO.Servicer = int.Parse(ddlServicer.SelectedValue);
            appForeclosureCaseSearchCriteriaDTO.PageNum = PageNum;
            appForeclosureCaseSearchCriteriaDTO.PageSize = PageSize;
            appForeclosureCaseSearchCriteriaDTO.TotalRowNum = 1;
            txtLoanNum.Text = DeleteSpecialChar(txtLoanNum.Text);
            appForeclosureCaseSearchCriteriaDTO.UserID = HPFWebSecurity.CurrentIdentity.UserId.ToString();
            
            return appForeclosureCaseSearchCriteriaDTO;
        }

        private void CalculatePaging(double searchResultCount)
        {
            this.TotalRowNum = searchResultCount;
            double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
            ShowHidePagingControl(false);

            if (totalpage > 10)
                bulErrorMessage.Items.Add(new ListItem(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0500)).ToString().Replace("*", this.TotalRowNum.ToString()));

            if (totalpage > 1)
            {
                GeneratePages(totalpage);
                lblTemp.Text = "1";
                ShowHidePagingControl(true);
                int MinRow = (this.PageSize * (PageNum - 1) + 1);
                int MaxRow = PageNum * this.PageSize;
                lblTotalRowNum.Text = this.TotalRowNum.ToString();
                lblMinRow.Text = MinRow.ToString();
                lblMaxRow.Text = MaxRow.ToString();
                if (MaxRow > this.TotalRowNum)
                    lblMaxRow.Text = this.TotalRowNum.ToString();
                else lblMaxRow.Text = MaxRow.ToString();
            }
        }

        private void GeneratePages(double totalpage)
        {
            if (totalpage > 10) totalpage = 10;
            phPages.Controls.Clear();
            for (int i = 1; i <= totalpage; i++)
            {
                LinkButton myLinkBtn = new LinkButton();
                myLinkBtn.ID = i.ToString();
                myLinkBtn.Text = i.ToString();
                //the first time you click searh button or choosen page. disable this page.
                if (i == this.PageNum || (i == 1 && this.PageNum == 0))
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
                phPages.Controls.Add(myLinkBtn);
                //add spaces beetween pages link button.
                Literal lit = new Literal();
                lit.Text = "&nbsp;&nbsp;";
                phPages.Controls.Add(lit);
                if (totalpage == 1)
                {
                    lbtnLast.Enabled = false;
                    lbtnNext.Enabled = false;
                }
                else
                {
                    if (this.PageNum != totalpage)
                    {
                        lbtnLast.Enabled = true;
                        lbtnNext.Enabled = true;
                    }
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
                    this.PageNum = 1;
                    lbtnFirst.Enabled = false;
                    lbtnPrev.Enabled = false;
                    break;
                // button: >>
                case "Last":
                    this.PageNum = Convert.ToInt16(totalpage);
                    lbtnLast.Enabled = false;
                    lbtnNext.Enabled = false;
                    lbtnFirst.Enabled = true;
                    lbtnPrev.Enabled = true;
                    if (totalpage > 10)
                    {
                        totalpage = 10;
                        this.PageNum = 10;
                    }
                    break;
                // button: >
                case "Next":
                    this.PageNum = Convert.ToInt16(this.PageNum) + 1;
                    lbtnFirst.Enabled = true;
                    lbtnLast.Enabled = true;
                    lbtnPrev.Enabled = true;
                    if (this.PageNum > 10) this.PageNum = 10;
                    if (this.PageNum == totalpage || this.PageNum == 10)
                    {
                        lbtnNext.Enabled = false;
                        lbtnLast.Enabled = false;
                    }

                    break;
                // button: <
                case "Prev":
                    this.PageNum = Convert.ToInt16(this.PageNum) - 1;
                    lbtnFirst.Enabled = true;
                    lbtnLast.Enabled = true;
                    lbtnNext.Enabled = true;
                    if (this.PageNum < 1 || this.PageNum == 1)
                    {
                        this.PageNum = 1;
                        lbtnPrev.Enabled = false;
                        lbtnFirst.Enabled = false;
                    }
                    break;
            }

           AppForeclosureCaseSearchCriteriaDTO appFCCriteria = GetAppForeclosureCaseSearchCriteriaDTO(PageNum);
           SearchForeclosureCase(appFCCriteria);
        }

        void myLinkBtn_Command(object sender, CommandEventArgs e)
        {
            double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
            int pagenum = int.Parse(e.CommandName);
            this.PageNum = pagenum;

            AppForeclosureCaseSearchCriteriaDTO appFCCriteria = GetAppForeclosureCaseSearchCriteriaDTO(PageNum);
            SearchForeclosureCase(appFCCriteria);

            lbtnFirst.Enabled = true;
            lbtnLast.Enabled = true;
            lbtnNext.Enabled = true;
            lbtnPrev.Enabled = true;

            if (pagenum == 1)
            {
                lbtnFirst.Enabled = false;
                lbtnPrev.Enabled = false;
            }
            if (totalpage > 10) totalpage = 10;
            if (pagenum == totalpage)
            {
                lbtnLast.Enabled = false;
                lbtnNext.Enabled = false;
            }
        }
        #region Ultility
        private string AddToSearchSpecialChar(string mystring)
        {
            string result = mystring;
            if (mystring != null)
            {
                for (int i = mystring.Length - 1; i >= 0; i--)
                {
                    if (mystring[i] == '[' || mystring[i] == ']' || mystring[i] == '\\')
                    {
                        result = result.Insert(i, "/");
                    }
                }
            }
            return result;
        }	
        string Replace1Char(string mystring, string oldchar, string newchar)
        {
            if (mystring == "" || oldchar == "" || newchar == "")
                return mystring;
            var mysubstring = mystring.Split('*');
            string result = null;
            for (int i = 0; i < mysubstring.Count(); i++)
            {
                result += mysubstring[i] + newchar;
            }
            result = result.Substring(0, result.Length - 1);
            return result;

        }

        private string DeleteSpecialChar(string mystring)
        {
            string result = "";
            if (mystring != null)
            {
                mystring = mystring.Trim();
                char[] specialchar = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '+', '=', '-', '[', ']', ';', ',', '.', '/', '{', '}', '|', ':', '<', '>', '?', ' ' };
                var DigitChar = mystring.Split(specialchar);
                for (int i = 0; i < DigitChar.Count(); i++)
                {
                    result += DigitChar[i];
                }
            }
            return result;
        }
        #endregion
    }
}