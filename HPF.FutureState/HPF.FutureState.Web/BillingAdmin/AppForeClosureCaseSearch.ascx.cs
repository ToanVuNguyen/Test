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
//using HPF.FutureState.BusinessLogic.BillingAdmin;
//using HPF.FutureState.Common.DataTransferObjects.BillingAdmin;

namespace HPF.FutureState.Web.BillingAdmin

{

    public partial class AppForeClosureCaseSearch : System.Web.UI.UserControl
    {
        
        protected AppForeclosureCaseSearchCriteriaDTO appForeclosureCaseSearchCriteriaDTO = new AppForeclosureCaseSearchCriteriaDTO();
        protected void Page_Load(object sender, EventArgs e)
        {
          
            BindDdlProgram();
            BindDdlState();
            BindDdlAgency();
            BindGrvForeClosureCaseSearch();

        }
        protected void BindDdlState()
        {
            //DataSet dsProgram = AppForeclosureCaseBL.Instance.GetState();
            //string ValueField = dsProgram.Tables[0].Columns["prop_state_cd"].ToString();
            //string TextField = dsProgram.Tables[0].Columns["prop_state_cd"].ToString();
            //BindDll(dsProgram, ddlPropertyState, ValueField, TextField, "", "All");
        }
        protected void BindDdlAgency()
        {
            //DataSet dsProgram = AppForeclosureCaseBL.Instance.GetAgency();
            //string ValueField = dsProgram.Tables[0].Columns["agency_id"].ToString();
            //string TextField = dsProgram.Tables[0].Columns["agency_name"].ToString();
            //BindDll(dsProgram, ddlAgency, ValueField, TextField, "", "");
        }
        protected void BindDdlProgram()
        {
            //DataSet dsProgram = AppForeclosureCaseBL.Instance.GetProgram();
            //string ValueField = dsProgram.Tables[0].Columns["program_id"].ToString();
            //string TextField = dsProgram.Tables[0].Columns["program_name"].ToString();
            //BindDll(dsProgram,ddlProgram,ValueField,TextField,"-1","ALL");
        }

        protected void BindGrvForeClosureCaseSearch()
        {
            //grvForeClosureCaseSearch.ColumsName = new List<string> { "Case ID", "Agency Case ID", "Counseled", "Case Date", "Borrower Name", "SSN", "Co_Borrower", "SSN", "Property Address", "Property City", "Property State", "PropertyZip", "Agency Name","Agent Name","Agent Phone","Agent Extension","Agent Extension","Agent Email","Case Complete Date","Days Deliquent","Bankruptcy Indicator","Foreclosre Notice Received Indicator","Loan List" };
            //grvForeClosureCaseSearch.ColumsWidth = new List<int> { 100, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 100, 300, 300, 300, 300, 300, 300, 300, 300, 300};
            //grvForeClosureCaseSearch.HeaderCssClass = "header";

            //appForeclosureCaseSearchCriteriaDTO.Last4SSN=txtSSN.Text==string.Empty?null:txtSSN.Text;
            //appForeclosureCaseSearchCriteriaDTO.LastName =  txtLastName.Text==string.Empty?null:txtLastName.Text;
            //appForeclosureCaseSearchCriteriaDTO.FirstName = txtFirstName.Text==string.Empty?null:txtFirstName.Text;
            //appForeclosureCaseSearchCriteriaDTO.ForeclosureCaseID = 676996;
            //    //txtForeclosureCaseID.Text==""?-1:int.Parse(txtForeclosureCaseID.Text);
            //appForeclosureCaseSearchCriteriaDTO.AgencyCaseID =txtAgencyCaseID.Text==""?-1:int.Parse(txtAgencyCaseID.Text);
            //appForeclosureCaseSearchCriteriaDTO.LoanNumber = txtLoanNum.Text==string.Empty?null:txtLoanNum.Text;
            //appForeclosureCaseSearchCriteriaDTO.PropertyZip = txtPropertyZip.Text==string.Empty?null:txtPropertyZip.Text;
            //appForeclosureCaseSearchCriteriaDTO.PropertyState = ddlPropertyState.SelectedValue == string.Empty ? null : ddlPropertyState.SelectedValue;
            //appForeclosureCaseSearchCriteriaDTO.Duplicates = ddlDup.SelectedValue.ToString() == string.Empty ? null : ddlDup.SelectedValue.ToString();
            //appForeclosureCaseSearchCriteriaDTO.Agency = int.Parse(ddlAgency.SelectedValue);
            //appForeclosureCaseSearchCriteriaDTO.Program = int.Parse(ddlProgram.SelectedValue);
            //var temp = AppForeclosureCaseBL.Instance.AppSearchforeClosureCase(appForeclosureCaseSearchCriteriaDTO);
            //grvForeClosureCaseSearch.DataSource = temp;
            

            
        }
        protected void BindDll(DataSet ds, DropDownList ddl,string ValueField,string TextField,string InitValue,string InitText)
        {
            ddl.DataSource = ds;
            ddl.DataTextField = TextField;
            ddl.DataValueField = ValueField;
            ddl.DataBind();
            if (InitText != "" )
            {
                ListItem item = new ListItem(InitText, InitValue);
                ddl.Items.Add(item);
            }
            if (InitText == "") InitText = "ALL";
            ddl.Items.FindByText(InitText).Selected = true;
        }


        
    }
}