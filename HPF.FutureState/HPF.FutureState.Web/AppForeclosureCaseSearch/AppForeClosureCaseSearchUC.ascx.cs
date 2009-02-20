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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Bind data to dropdownlist.
                BindProgramDropdownlist();
                BindStateDropdownlist();
                BindAgencyDropdownlist();
                BindServicerDropDownList();
                //redisplay search criteria when you click on menu item.
                ReBindSearchCriteria();
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
        /// <summary>
        /// Redisplay search criteria.
        /// </summary>
        protected void ReBindSearchCriteria()
        {
            if (Session["searchcriteria"] != null)
            {
                //set search control the values from session searchcriteria.               
                AppForeclosureCaseSearchCriteriaDTO searchcriteria = (AppForeclosureCaseSearchCriteriaDTO)Session["searchcriteria"];
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
                //bind gridview
                BindGrvForeClosureCaseSearch(1);
            }
        }
        protected void BindStateDropdownlist()
        {
            RefCodeItemDTOCollection stateCol = LookupDataBL.Instance.GetRefCode("state");
            //Bind data
            ddlPropertyState.DataValueField = "code";
            ddlPropertyState.DataTextField = "code";
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
            ddlProgram.Items.FindByText("ALL").Selected = true;
        }
        protected void BindServicerDropDownList()
        {
            ServicerDTOCollection servicerCollection = LookupDataBL.Instance.GetServicer();
            ddlServicer.DataValueField = "ServicerID";
            ddlServicer.DataTextField = "ServicerName";
            ddlServicer.DataSource = servicerCollection;
            ddlServicer.DataBind();
            ddlServicer.Items.FindByText("ALL").Selected = true;
        }

        /// <summary>
        /// Bind data search result to gridview. Depend on that display pager controls.
        /// </summary>
        /// <param name="PageNum">initial pagenum =1</param>
        protected void BindGrvForeClosureCaseSearch(int PageNum)
        {
            try
            {
                //panForeClosureCaseSearch.Visible = true;
                ManageControls(false);
                //get search criteria
                var appForeclosureCaseSearchCriteriaDTO = GetAppForeclosureCaseSearchCriteriaDTO(PageNum);
                //get search info match search criteria
                var searchResult = ForeclosureCaseBL.Instance.AppSearchforeClosureCase(appForeclosureCaseSearchCriteriaDTO);
                //Bind data search result to gridview
                grvForeClosureCaseSearch.DataSource = searchResult;
                grvForeClosureCaseSearch.DataBind();
                //The first time you click search button, page 1 is choose, disable button: << <
                if (lblTemp.Text != "1")
                {
                    lbtnFirst.Enabled = false;
                    lbtnPrev.Enabled = false;
                }
                this.TotalRowNum = searchResult.SearchResultCount;
                //there have data search result
                if (this.TotalRowNum != 0)
                {
                   
                    //display pagers controls
                    ManageControls(true);
                    grvForeClosureCaseSearch.Visible = true;
                    int MinRow = (this.PageSize * (PageNum - 1) + 1);
                    int MaxRow = PageNum * this.PageSize;
                    lblTotalRowNum.Text = this.TotalRowNum.ToString();
                    lblMinRow.Text = MinRow.ToString();
                    lblMaxRow.Text = MaxRow.ToString();
                    if (MaxRow > this.TotalRowNum)
                        lblMaxRow.Text = this.TotalRowNum.ToString();
                    else lblMaxRow.Text = MaxRow.ToString();
                    double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
                    if (totalpage == 1)
                    {
                        lbtnFirst.Enabled = false;
                        lbtnLast.Enabled = false;
                        lbtnPrev.Enabled = false;
                        lbtnNext.Enabled = false;
                    }
                    //generate pages
                    GeneratePages(totalpage);
                    lblTemp.Text = "1";

                }
                //there is no data search result
                else
                {
                    //not display pagers control.
                    ManageControls(false);
                }
            }
            catch (DataValidationException ex)
            {
                //return exception message check input search criteria
                for (int i = 0; i < ex.ExceptionMessages.Count; i++)
                {
                    //panForeClosureCaseSearch.Visible = false;
                    bulErrorMessage.Items.Add(new ListItem(ex.ExceptionMessages[i].Message));
                   
                }
                this.TotalRowNum = 0;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
            catch (Exception ex)
            {
                //panForeClosureCaseSearch.Visible = false;
                bulErrorMessage.Items.Add(new ListItem(ex.Message));
                //lblErrorMessage.Text += ex.Message;
                //lblErrorMessage.Text += " <br>";
                this.TotalRowNum = 0;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        /// <summary>
        /// get all search criteria from controls put into AppForeclosureCaseSearchCriteriaDTO
        /// </summary>
        /// <param name="PageNum"></param>
        /// <returns></returns>
        private AppForeclosureCaseSearchCriteriaDTO GetAppForeclosureCaseSearchCriteriaDTO(int PageNum)
        {
            var appForeclosureCaseSearchCriteriaDTO = new AppForeclosureCaseSearchCriteriaDTO();

            string textchangeFirstName = "";
            string textchangeLastName = "";
            appForeclosureCaseSearchCriteriaDTO.Last4SSN = txtSSN.Text == string.Empty ? null : txtSSN.Text;
            //replace * by % to search like in fristname
            if (txtFirstName.Text != string.Empty)
            {
                textchangeFirstName = Replace1Char(txtFirstName.Text, "*", "%");
                //textchangeFirstName = Replace1Char(textchangeFirstName, "*", "%");
            }
            //replace * by % to search like in fristname
            if (txtLastName.Text != string.Empty)
            {
                textchangeLastName = Replace1Char(txtLastName.Text, "*", "%");
                //textchangeLastName = Replace1Char(textchangeLastName, "*", "%");
            }
            appForeclosureCaseSearchCriteriaDTO.LastName = txtLastName.Text == string.Empty ? null : textchangeLastName;
            appForeclosureCaseSearchCriteriaDTO.FirstName = txtFirstName.Text == string.Empty ? null : textchangeFirstName;
            try
            {
                appForeclosureCaseSearchCriteriaDTO.ForeclosureCaseID = txtForeclosureCaseID.Text == string.Empty ? -1 : int.Parse(txtForeclosureCaseID.Text.Trim());
            }
            catch
            {
                bulErrorMessage.Items.Add(ErrorMessages.ERR0503);
            }
            appForeclosureCaseSearchCriteriaDTO.AgencyCaseID = DeleteSpecialChar(txtAgencyCaseID.Text) == string.Empty ? null : DeleteSpecialChar(txtAgencyCaseID.Text);
            appForeclosureCaseSearchCriteriaDTO.LoanNumber = DeleteSpecialChar(txtLoanNum.Text) == string.Empty ? null : DeleteSpecialChar(txtLoanNum.Text);
            appForeclosureCaseSearchCriteriaDTO.PropertyZip = txtPropertyZip.Text == string.Empty ? null : txtPropertyZip.Text.Trim();
            appForeclosureCaseSearchCriteriaDTO.PropertyState = ddlPropertyState.SelectedValue == "ALL" ? null : ddlPropertyState.SelectedValue.Trim();
            appForeclosureCaseSearchCriteriaDTO.Duplicates = ddlDup.SelectedValue.ToString() == string.Empty ? null : ddlDup.SelectedValue.ToString();
            appForeclosureCaseSearchCriteriaDTO.Agency = int.Parse(ddlAgency.SelectedValue);
            appForeclosureCaseSearchCriteriaDTO.Program = int.Parse(ddlProgram.SelectedValue);
            appForeclosureCaseSearchCriteriaDTO.Servicer = int.Parse(ddlServicer.SelectedValue);
            appForeclosureCaseSearchCriteriaDTO.PageNum = PageNum;
            appForeclosureCaseSearchCriteriaDTO.PageSize = PageSize;
            appForeclosureCaseSearchCriteriaDTO.TotalRowNum = 1;
            txtAgencyCaseID.Text = DeleteSpecialChar(txtAgencyCaseID.Text);
            txtLoanNum.Text = DeleteSpecialChar(txtLoanNum.Text);
            return appForeclosureCaseSearchCriteriaDTO;
        }
        /// <summary>
        /// display or not display pager controls
        /// </summary>
        /// <param name="isEnable"></param>
        protected void ManageControls(bool isEnable)
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //store searchcriteria in session to keep searchcriteria when you click on menu item.
            Session["searchcriteria"] = GetAppForeclosureCaseSearchCriteriaDTO(1);
            //lblErrorMessage.Text = "";
            bulErrorMessage.Items.Clear();
            //Bind search data meet search criteria to gridview, display page 1.
            lblTemp.Text = " ";
            BindGrvForeClosureCaseSearch(1);
            //calculate totalpage from search data to display warning message if there are greater than 500 cases.
            double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
            if (totalpage > 10)
                //lblErrorMessage.Text = ErrorMessages.GetExceptionMessageCombined("WARN0500");
                bulErrorMessage.Items.Add(new ListItem(ErrorMessages.GetExceptionMessageCombined("WARN0500")));
            //every click search button, set 
            this.PageNum = 0;
            lbtnLast.Enabled = true;
            lbtnNext.Enabled = true;

            //
            GeneratePages(totalpage);
        }
        /// <summary>
        /// display 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvForeClosureCaseSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblcounseled = e.Row.FindControl("lblCounseled") as Label;
                if (lblcounseled.Text == string.Empty) return;
                DateTime datecounseled = DateTime.Parse(lblcounseled.Text);
                //datecompare is early than today 1 year.
                DateTime datecompare = DateTime.Today.AddYears(-1);
                int result = DateTime.Compare(datecompare, datecounseled);
                //datecompare is later than datecounseled--> counseled <1 yr
                if (result > 0 || datecounseled == null) lblcounseled.Text = ">1 yr";
                //datecompare is earlier than datecounseled--> counseled >1 yr
                else lblcounseled.Text = "<1 yr";

                //Case complete date
                Label lblcasecompletedate = e.Row.FindControl("lblCaseCompleteDate") as Label;
                string completedt = lblcasecompletedate.Text;
                string mindt = DateTime.MinValue.ToShortDateString();
                DateTime completedate = Convert.ToDateTime(lblcasecompletedate.Text);
                DateTime mindate = DateTime.MinValue;
                if (DateTime.Compare(completedate, mindate) == 0)
                    lblcasecompletedate.Text = "";


            }
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
                    if (totalpage > 10) totalpage = 10;
                    break;
                // button: >
                case "Next":
                    this.PageNum = Convert.ToInt16(this.PageNum) + 1;
                    lbtnFirst.Enabled = true;
                    lbtnLast.Enabled = true;
                    lbtnPrev.Enabled = true;
                    if (this.PageNum > 10) this.PageNum = 10;
                    if (this.PageNum == totalpage)
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
            //
            BindGrvForeClosureCaseSearch(this.PageNum);
        }
        /// <summary>
        /// generate pages
        /// </summary>
        /// <param name="totalpage"></param>
        void GeneratePages(double totalpage)
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
            }
        }
        /// <summary>
        /// when click pages link button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void myLinkBtn_Command(object sender, CommandEventArgs e)
        {
            double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
            int pagenum = int.Parse(e.CommandName);
            this.PageNum = pagenum;

            BindGrvForeClosureCaseSearch(pagenum);
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
        /// <summary>
        /// replace oldchar in mystring by newchar.
        /// </summary>
        /// <param name="mystring"></param>
        /// <param name="oldchar"></param>
        /// <param name="newchar"></param>
        /// <returns></returns>
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
        string DeleteSpecialChar(string mystring)
        {
            string result = "";
            if (mystring != null)
            {
                mystring = mystring.Trim();
                char[] specialchar = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '+', '=', '-', '[', ']', ';', ',', '.', '/', '{', '}', '|', ':', '<', '>', '?' };
                var DigitChar = mystring.Split(specialchar);
                for (int i = 0; i < DigitChar.Count(); i++)
                {
                    result += DigitChar[i];
                }
            }
            return result;
        }

    }
}