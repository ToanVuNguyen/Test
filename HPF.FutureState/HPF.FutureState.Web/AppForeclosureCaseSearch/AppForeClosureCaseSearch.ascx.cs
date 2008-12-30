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
namespace HPF.FutureState.Web.BillingAdmin
{

    public partial class AppForeClosureCaseSearch : System.Web.UI.UserControl
    {
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
                BindDDLProgram();
                BindDDLState();
                BindDDLAgency();

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
        protected void BindDDLState()
        {
            StateDTOCollection stateCollection = ForeclosureCaseSetBL.Instance.GetState();
            ddlPropertyState.DataValueField = "StateName";
            ddlPropertyState.DataTextField = "StateName";
            ddlPropertyState.DataSource = stateCollection;
            ddlPropertyState.DataBind();
            ddlPropertyState.Items.FindByText("ALL").Selected = true;
        }
        protected void BindDDLAgency()
        {
            AgencyDTOCollection agencyCollection = ForeclosureCaseSetBL.Instance.GetAgency();
            ddlAgency.DataValueField = "AgencyID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = agencyCollection;
            ddlAgency.DataBind();
            ddlAgency.Items.FindByText("ALL").Selected = true;
        }
        protected void BindDDLProgram()
        {
            ProgramDTOCollection programCollection = ForeclosureCaseSetBL.Instance.GetProgram();
            ddlProgram.DataValueField = "ProgramID";
            ddlProgram.DataTextField = "ProgramName";
            ddlProgram.DataSource = programCollection;
            ddlProgram.DataBind();
            ddlProgram.Items.FindByText("ALL").Selected = true;
        }

        protected void BindGrvForeClosureCaseSearch(int PageNum)
        {


            AppForeclosureCaseSearchCriteriaDTO appForeclosureCaseSearchCriteriaDTO = new AppForeclosureCaseSearchCriteriaDTO();
            try
            {
                appForeclosureCaseSearchCriteriaDTO.Last4SSN = txtSSN.Text == string.Empty ? null : txtSSN.Text;
                appForeclosureCaseSearchCriteriaDTO.LastName = txtLastName.Text == string.Empty ? null : txtLastName.Text;
                appForeclosureCaseSearchCriteriaDTO.FirstName = txtFirstName.Text == string.Empty ? null : txtFirstName.Text;
                appForeclosureCaseSearchCriteriaDTO.ForeclosureCaseID = txtForeclosureCaseID.Text == string.Empty ? -1 : int.Parse(txtForeclosureCaseID.Text);
                appForeclosureCaseSearchCriteriaDTO.AgencyCaseID = txtAgencyCaseID.Text == string.Empty ? null : txtAgencyCaseID.Text;
                appForeclosureCaseSearchCriteriaDTO.LoanNumber = txtLoanNum.Text == string.Empty ? null : txtLoanNum.Text;
                appForeclosureCaseSearchCriteriaDTO.PropertyZip = txtPropertyZip.Text == string.Empty ? null : txtPropertyZip.Text;
                appForeclosureCaseSearchCriteriaDTO.PropertyState = ddlPropertyState.SelectedValue == "ALL" ? null : ddlPropertyState.SelectedValue;
                appForeclosureCaseSearchCriteriaDTO.Duplicates = ddlDup.SelectedValue.ToString() == string.Empty ? null : ddlDup.SelectedValue.ToString();
                appForeclosureCaseSearchCriteriaDTO.Agency = int.Parse(ddlAgency.SelectedValue);
                appForeclosureCaseSearchCriteriaDTO.Program = int.Parse(ddlProgram.SelectedValue);
                appForeclosureCaseSearchCriteriaDTO.PageNum = 1;
                appForeclosureCaseSearchCriteriaDTO.PageSize = PageSize;
                appForeclosureCaseSearchCriteriaDTO.TotalRowNum = 1;
                var temp = ForeclosureCaseSetBL.Instance.AppSearchforeClosureCase(appForeclosureCaseSearchCriteriaDTO);
                grvForeClosureCaseSearch.DataSource = temp;
                grvForeClosureCaseSearch.DataBind();
                this.TotalRowNum = temp.SearchResultCount;
                if (this.TotalRowNum != 0)
                {
                    lbl1.Visible = true;
                    lbl2.Visible = true;
                    lblMaxRow.Visible = true;
                    lblMinRow.Visible = true;
                    lblTotalRowNum.Visible = true;
                    lbtnFirst.Visible = true;
                    lbtnLast.Visible = true;
                    lbtnNext.Visible = true;
                    lbtnPrev.Visible = true;

                    int MinRow = (this.PageSize * (PageNum - 1) + 1);
                    int MaxRow = PageNum * this.PageSize;
                    lblTotalRowNum.Text = this.TotalRowNum.ToString();
                    lblMinRow.Text = MinRow.ToString();
                    lblMaxRow.Text = MaxRow.ToString();
                    if (MaxRow > this.TotalRowNum)
                        lblMaxRow.Text = this.TotalRowNum.ToString();
                    else lblMaxRow.Text = MaxRow.ToString();
                    lblTemp.Text = "1";
                }
                else
                {
                    lbl1.Visible = false;
                    lbl2.Visible = false;
                    lblMaxRow.Visible = false;
                    lblMinRow.Visible = false;
                    lblTotalRowNum.Visible = false;
                    lbtnFirst.Visible = false;
                    lbtnLast.Visible = false;
                    lbtnNext.Visible = false;
                    lbtnPrev.Visible = false;

                }


            }
            catch (Exception ex)
            {
            }


        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrvForeClosureCaseSearch(1);
            double totalpage = Math.Ceiling(this.TotalRowNum / this.PageSize);
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
                    break;
                case "Last":
                    this.PageNum = Convert.ToInt16(totalpage);
                    if (totalpage > 10) totalpage = 10;
                    break;
                case "Next":
                    this.PageNum = Convert.ToInt16(this.PageNum) + 1;
                    if (this.PageNum > 10) this.PageNum = 10;
                    break;
                case "Prev":
                    this.PageNum = Convert.ToInt16(this.PageNum) - 1;
                    if (this.PageNum < 1) this.PageNum = 1;
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
                myLinkBtn.CssClass = "UnderLine";
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
            int pagenum = int.Parse(e.CommandName);
            BindGrvForeClosureCaseSearch(pagenum);

        }


    }
}