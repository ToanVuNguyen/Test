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

        protected AppForeclosureCaseSearchCriteriaDTO appForeclosureCaseSearchCriteriaDTO = new AppForeclosureCaseSearchCriteriaDTO();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindDDLProgram();
                BindDDLState();
                BindDDLAgency();
            }
        }
        protected void BindDDLState()
        {
            StateDTOCollection stateCollection = ForeclosureCaseSetBL.Instance.GetState();
            ddlPropertyState.DataValueField = "StateName";
            ddlPropertyState.DataTextField = "StateName";
            ListItem item = new ListItem("ALL", null);
            ddlPropertyState.Items.Add(item);
            ddlPropertyState.Items.FindByText("ALL").Selected = true;
            ddlPropertyState.DataSource = stateCollection;
            ddlPropertyState.DataBind();
        }
        protected void BindDDLAgency()
        {
            AgencyDTOCollection agencyCollection = ForeclosureCaseSetBL.Instance.GetAgency();
            ddlAgency.DataValueField = "AgencyID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.Items.FindByText("ALL").Selected = true;
            ddlAgency.DataSource = agencyCollection;
            ddlAgency.DataBind();
        }
        protected void BindDDLProgram()
        {
            ProgramDTOCollection programCollection = ForeclosureCaseSetBL.Instance.GetProgram();
            ddlProgram.DataValueField = "ProgramID";
            ddlProgram.DataTextField = "ProgramName";
            ListItem item = new ListItem("ALL", "-1");
            ddlProgram.Items.FindByText("ALL").Selected = true;
            ddlProgram.DataSource = programCollection;
            ddlProgram.DataBind();
        }

        protected void BindGrvForeClosureCaseSearch()
        {
            
            //grvForeClosureCaseSearch.ColumsName = new List<string> { "Case ID", "Agency Case ID", "Counseled", "Case Date", "Borrower Name", "SSN", "Co_Borrower", "SSN", "Property Address", "Property City", "Property State", "PropertyZip", "Agency Name", "Agent Name", "Agent Phone", "Agent Extension", "Agent Extension", "Agent Email", "Case Complete Date", "Days Deliquent", "Bankruptcy Indicator", "Foreclosre Notice Received Indicator", "Loan List" };
            //grvForeClosureCaseSearch.ColumsWidth = new List<int> { 100, 500, 300, 200, 300, 100,300,300, 300, 300, 300, 300, 300, 100, 300, 300, 300, 300, 300, 300, 300, 300, 300 };
            //grvForeClosureCaseSearch.HeaderCssClass = "header";

            try
            {
            appForeclosureCaseSearchCriteriaDTO.Last4SSN = txtSSN.Text == string.Empty ? null : txtSSN.Text;
            appForeclosureCaseSearchCriteriaDTO.LastName = txtLastName.Text == string.Empty ? null : txtLastName.Text;
            appForeclosureCaseSearchCriteriaDTO.FirstName = txtFirstName.Text == string.Empty ? null : txtFirstName.Text;
            //appForeclosureCaseSearchCriteriaDTO.ForeclosureCaseID = 676996;
            appForeclosureCaseSearchCriteriaDTO.ForeclosureCaseID = txtForeclosureCaseID.Text == string.Empty ? -1 : int.Parse(txtForeclosureCaseID.Text);
            appForeclosureCaseSearchCriteriaDTO.AgencyCaseID = txtAgencyCaseID.Text == string.Empty ?null : txtAgencyCaseID.Text;
            appForeclosureCaseSearchCriteriaDTO.LoanNumber = txtLoanNum.Text == string.Empty ? null : txtLoanNum.Text;
            appForeclosureCaseSearchCriteriaDTO.PropertyZip = txtPropertyZip.Text == string.Empty ? null : txtPropertyZip.Text;
            appForeclosureCaseSearchCriteriaDTO.PropertyState = ddlPropertyState.SelectedValue == string.Empty ? null : ddlPropertyState.SelectedValue;
            appForeclosureCaseSearchCriteriaDTO.Duplicates = ddlDup.SelectedValue.ToString() == string.Empty ? null : ddlDup.SelectedValue.ToString();
            appForeclosureCaseSearchCriteriaDTO.Agency = int.Parse(ddlAgency.SelectedValue);
            appForeclosureCaseSearchCriteriaDTO.Program = int.Parse(ddlProgram.SelectedValue);
            var temp = ForeclosureCaseSetBL.Instance.AppSearchforeClosureCase(appForeclosureCaseSearchCriteriaDTO);
            grvForeClosureCaseSearch.DataSource = temp;
            grvForeClosureCaseSearch.DataBind();
            }
            catch (ProcessingException ex)
            {

                throw ex;
            } 
        }
        protected void BindDDL(DataSet ds, DropDownList ddl,string ValueField,string TextField,string InitValue,string InitText)
        {
            ddl.DataSource = ds;
            ddl.DataTextField = TextField;
            ddl.DataValueField = ValueField;
            ddl.DataBind();
            if (InitText != "")
            {
                ListItem item = new ListItem(InitText, InitValue);
                ddl.Items.Add(item);
            }
            if (InitText == ""||InitText==null) InitText = "ALL";
            ddl.Items.FindByText(InitText).Selected = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrvForeClosureCaseSearch();
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


        
    }
}