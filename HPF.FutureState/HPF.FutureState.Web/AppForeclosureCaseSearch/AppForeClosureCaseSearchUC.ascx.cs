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
namespace HPF.FutureState.Web.AppForeClosureCaseSearch
{

    public partial class AppForeClosureCaseSearchUC : System.Web.UI.UserControl
    {
        protected AppForeclosureCaseSearchCriteriaDTO SearchCriteria
        {
            get
            {
                return (AppForeclosureCaseSearchCriteriaDTO)Session["searchcriteria"];
            }
            set { AppForeclosureCaseSearchCriteriaDTO searchcriteria = value; }
        }
        protected int PageSize
        {
            get { return (int.Parse(ConfigurationManager.AppSettings["pagesize"])); }
        }
        protected double TotalRowNum
        {
            get { return Convert.ToDouble(ViewState["totalrownum"]); }
            set { ViewState["totalrownum"] = value; }
        }
        protected int PageNum
        {
            get { return Convert.ToInt16(ViewState["pagenum"]); }
            set { ViewState["pagenum"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProgramDropdownlist();
                BindStateDropdownlist();
                BindAgencyDropdownlist();
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
        protected void ReBindSearchCriteria()
        {
            if (Session["searchcriteria"] != null)
            {
                AppForeclosureCaseSearchCriteriaDTO searchcriteria = (AppForeclosureCaseSearchCriteriaDTO)Session["searchcriteria"];
                txtLastName.Text = searchcriteria.LastName;
                txtFirstName.Text = searchcriteria.FirstName;
                txtAgencyCaseID.Text = searchcriteria.AgencyCaseID;
                if (searchcriteria.ForeclosureCaseID.ToString() == "-1")
                    txtForeclosureCaseID.Text = "";
                txtForeclosureCaseID.Text = searchcriteria.ForeclosureCaseID.ToString();
                txtLoanNum.Text = searchcriteria.LoanNumber;
                txtPropertyZip.Text = searchcriteria.PropertyZip;
                txtSSN.Text = searchcriteria.Last4SSN;
                ddlAgency.SelectedValue = searchcriteria.Agency.ToString();
                ddlDup.SelectedValue = searchcriteria.Duplicates;
                ddlProgram.SelectedValue = searchcriteria.Program.ToString();
                ddlPropertyState.SelectedValue = searchcriteria.PropertyState;
            }
        }
        protected void BindStateDropdownlist()
        {
            StateDTOCollection stateCollection = LookupDataBL.Instance.GetState();
            //Bind data
            ddlPropertyState.DataValueField = "StateName";
            ddlPropertyState.DataTextField = "StateName";
            ddlPropertyState.DataSource = stateCollection;
            ddlPropertyState.DataBind();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PageNum"></param>
        protected void BindGrvForeClosureCaseSearch(int PageNum)
        {
            try
            {
                panForeClosureCaseSearch.Visible = true;
                ManageControls(false);
                //get search criteria
                var appForeclosureCaseSearchCriteriaDTO = GetAppForeclosureCaseSearchCriteriaDTO(PageNum);
                //get search info match search criteria
                var temp = ForeclosureCaseBL.Instance.AppSearchforeClosureCase(appForeclosureCaseSearchCriteriaDTO);
                //Bind search result
                grvForeClosureCaseSearch.DataSource = temp;
                grvForeClosureCaseSearch.DataBind();
                
                //selected gridview row
                //for(int i=0;i<grvForeClosureCaseSearch.Rows.Count;i++)
                //{
                //grvForeClosureCaseSearch.Rows[i].Attributes.Add("onclick", ClientScript.GetPostBackEventReference(grvForeClosureCaseSearch,"Select$"+1));
                //}
                    this.TotalRowNum = temp.SearchResultCount;
                //

                if (this.TotalRowNum != 0)
                {
                    ManageControls(true);
                    grvForeClosureCaseSearch.Visible = true;
                    lbtnFirst.Enabled = false;
                    lbtnPrev.Enabled = false;
                    int MinRow = (this.PageSize * (PageNum - 1) + 1);
                    int MaxRow = PageNum * this.PageSize;
                    lblTotalRowNum.Text = this.TotalRowNum.ToString();
                    lblMinRow.Text = MinRow.ToString();
                    lblMaxRow.Text = MaxRow.ToString();
                    if (MaxRow > this.TotalRowNum)
                        lblMaxRow.Text = this.TotalRowNum.ToString();
                    else lblMaxRow.Text = MaxRow.ToString();
                    //lblTemp.Text = "1";
                    double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
                    if (totalpage == 1)
                    {
                        lbtnFirst.Enabled = false;
                        lbtnLast.Enabled = false;
                        lbtnPrev.Enabled = false;
                        lbtnNext.Enabled = false;
                    }
                    GeneratePages(totalpage);
                    lblTemp.Text = "1";
                }
                else
                {
                    ManageControls(false);
                }
            }
            catch (DataValidationException ex)
            {
                //lblErrorMessage.Text += ex.Message;
                for (int i = 0; i < ex.ExceptionMessages.Count; i++)
                {
                    panForeClosureCaseSearch.Visible = false;
                    lblErrorMessage.Text += ex.ExceptionMessages[i].Message;
                    lblErrorMessage.Text += " <br>";
                }
                this.TotalRowNum = 0;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
            catch (Exception ex)
            {
                panForeClosureCaseSearch.Visible = false;
                lblErrorMessage.Text += ex.Message;
                lblErrorMessage.Text += " <br>";
                this.TotalRowNum = 0;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }

        private AppForeclosureCaseSearchCriteriaDTO GetAppForeclosureCaseSearchCriteriaDTO(int PageNum)
        {
            var appForeclosureCaseSearchCriteriaDTO = new AppForeclosureCaseSearchCriteriaDTO();

            string textchangeFirstName = "";
            string textchangeLastName = "";
            appForeclosureCaseSearchCriteriaDTO.Last4SSN = txtSSN.Text == string.Empty ? null : txtSSN.Text;
            if (txtFirstName.Text != string.Empty)
            {
                textchangeFirstName = Replace1Char(txtFirstName.Text, "*", "%");
                textchangeFirstName = Replace1Char(textchangeFirstName, "*", "%");
            }
            if (txtLastName.Text != string.Empty)
            {
                textchangeLastName = Replace1Char(txtLastName.Text, "*", "%");
                textchangeLastName = Replace1Char(textchangeLastName, "*", "%");
            }
            appForeclosureCaseSearchCriteriaDTO.LastName = txtLastName.Text == string.Empty ? null : textchangeLastName;
            appForeclosureCaseSearchCriteriaDTO.FirstName = txtFirstName.Text == string.Empty ? null : textchangeFirstName;
            appForeclosureCaseSearchCriteriaDTO.ForeclosureCaseID = txtForeclosureCaseID.Text == string.Empty ? -1 : int.Parse(txtForeclosureCaseID.Text.Trim());
            appForeclosureCaseSearchCriteriaDTO.AgencyCaseID = txtAgencyCaseID.Text == string.Empty ? null : txtAgencyCaseID.Text.Trim();
            appForeclosureCaseSearchCriteriaDTO.LoanNumber = txtLoanNum.Text == string.Empty ? null : txtLoanNum.Text.Trim();
            appForeclosureCaseSearchCriteriaDTO.PropertyZip = txtPropertyZip.Text == string.Empty ? null : txtPropertyZip.Text.Trim();
            appForeclosureCaseSearchCriteriaDTO.PropertyState = ddlPropertyState.SelectedValue == "ALL" ? null : ddlPropertyState.SelectedValue.Trim();
            appForeclosureCaseSearchCriteriaDTO.Duplicates = ddlDup.SelectedValue.ToString() == string.Empty ? null : ddlDup.SelectedValue.ToString();
            appForeclosureCaseSearchCriteriaDTO.Agency = int.Parse(ddlAgency.SelectedValue);
            appForeclosureCaseSearchCriteriaDTO.Program = int.Parse(ddlProgram.SelectedValue);
            appForeclosureCaseSearchCriteriaDTO.PageNum = PageNum;
            appForeclosureCaseSearchCriteriaDTO.PageSize = PageSize;
            appForeclosureCaseSearchCriteriaDTO.TotalRowNum = 1;
            return appForeclosureCaseSearchCriteriaDTO;
        }

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
            Session["searchcriteria"] = GetAppForeclosureCaseSearchCriteriaDTO(1);
            lblErrorMessage.Text = "";
            BindGrvForeClosureCaseSearch(1);
            double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
            if (totalpage > 10) lblErrorMessage.Text = @"There are greater than 500 case results are found based on 
                        the search criteria,Please defined search criteria and resubmitted";
            //
            GeneratePages(totalpage);
        }

        protected void grvForeClosureCaseSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblcounseled = e.Row.FindControl("lblCounseled") as Label;
                DateTime datecounseled = DateTime.Parse(lblcounseled.Text);
                DateTime datecompare = DateTime.Today.AddYears(1);
                int result = DateTime.Compare(datecompare, datecounseled);

                if (result > 0) lblcounseled.Text = ">1 yr";
                else lblcounseled.Text = "<1 yr";

            }
        }
        protected void lbtnNavigate_Click(object sender, CommandEventArgs e)
        {
            double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
            switch (e.CommandName)
            {
                case "First":
                    this.PageNum = 1;
                    lbtnFirst.Enabled = false;
                    lbtnPrev.Enabled = false;
                    break;
                case "Last":
                    this.PageNum = Convert.ToInt16(totalpage);
                    lbtnLast.Enabled = false;
                    lbtnNext.Enabled = false;
                    if (totalpage > 10) totalpage = 10;
                    break;
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
            BindGrvForeClosureCaseSearch(this.PageNum);
        }

        void GeneratePages(double totalpage)
        {
            if (totalpage > 10) totalpage = 10;
            phPages.Controls.Clear();
            for (int i = 1; i <= totalpage; i++)
            {
                LinkButton myLinkBtn = new LinkButton();
                myLinkBtn.ID = i.ToString();
                myLinkBtn.Text = i.ToString();

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
                Literal lit = new Literal();
                lit.Text = "&nbsp;&nbsp;";

                phPages.Controls.Add(lit);
            }
        }

        void myLinkBtn_Command(object sender, CommandEventArgs e)
        {
            double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
            int pagenum = int.Parse(e.CommandName);
            this.PageNum = pagenum;

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

            BindGrvForeClosureCaseSearch(pagenum);

        }
        string Replace1Char(string mystring, string oldchar, string newchar)
        {
            int StartIndex = mystring.IndexOf(oldchar);
            if (StartIndex == -1) return mystring;
            string mystring1 = null;
            string mystring2 = null;
            mystring1 = mystring.Substring(0, StartIndex);

            if (StartIndex + 1 == mystring.Length)
                return mystring1 + "%";
            else
            {
                mystring2 = mystring.Substring(StartIndex + 1, mystring.Length - 1);
                if (StartIndex == 0) return ("%" + mystring2);
                return (mystring1 + "%" + mystring2);
            }
        }
        protected void grvForeClosureCaseSearch_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick", "this.className='SelectedRowStyle'");
                
                if (e.Row.RowState == DataControlRowState.Alternate)
                {
                    e.Row.Attributes.Add("ondblclick", "this.className='AlternatingRowStyle'");
                }
                else
                {
                    e.Row.Attributes.Add("ondblclick", "this.className='RowStyle'");
                }

            }
        }

        protected void grvForeClosureCaseSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}