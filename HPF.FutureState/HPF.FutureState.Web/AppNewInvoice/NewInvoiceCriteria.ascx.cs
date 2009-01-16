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
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.BusinessLogic;


namespace HPF.FutureState.Web.AppNewInvoice
{
    public partial class AppNewInvoice : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FundingSourceDatabind();
                ProgramDatabind();
                GenderDatabind();
                RaceDatabind();
                IndicatorDatabind();
                HouseholdDatabind();
                StateDatabind();
                DDLBDataBind();
            }
        }
        private void DDLBDataBind()
        {
            AddBlankToDDLB(dropDuplicates);
            AddBlankToDDLB(dropHispanic);
            AddBlankToDDLB(dropCaseCompleted);
            AddBlankToDDLB(dropAlreadyBilled);
            AddBlankToDDLB(dropServicerConsent);
            AddBlankToDDLB(dropFundingConsent);
            //set default value for DDLB
            dropDuplicates.SelectedIndex = 2;
            dropCaseCompleted.SelectedIndex = 1;
            dropAlreadyBilled.SelectedIndex = 2;
            dropServicerConsent.SelectedIndex = 1;
            dropFundingConsent.SelectedIndex = 1;
            dropIndicators.SelectedIndex = 1;
            dropGender.SelectedIndex = 0;
            dropRace.SelectedIndex = 0;
            dropHispanic.SelectedIndex = 0;
            dropHouseholdCode.SelectedIndex = 0;
            dropState.SelectedIndex = 0;

        }
        private void AddBlankToDDLB(DropDownList temp)
        {
            temp.Items.Insert(0,new ListItem(" ", "0"));
        }
        private void FundingSourceDatabind()
        {
            FundingSourceDTOCollection fundingSourceCollection = null;
            try
            {
                fundingSourceCollection = LookupDataBL.Instance.GetFundingSource();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }
            dropFundingSource.DataValueField = "FundingSourceID";
            dropFundingSource.DataTextField = "FundingSourceName";
            dropFundingSource.DataSource = fundingSourceCollection;
            dropFundingSource.DataBind();
            dropFundingSource.Items.Remove(dropFundingSource.Items.FindByText("ALL"));
            dropFundingSource.Items.Insert(0, new ListItem(" ", "-1"));
            if (Session["fundingSourceId"] != null)
            {
                string fundingSourceId = Session["fundingSourceId"].ToString();
                dropFundingSource.Items.FindByValue(fundingSourceId).Selected = true;
                dropFundingSource_SelectedIndexChanged1(new object(),new EventArgs());
            }
        }
        private void ProgramDatabind()
        {
            ProgramDTOCollection programCollection = null;
            try
            {
                programCollection = LookupDataBL.Instance.GetProgram();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }
            dropProgram.DataValueField = "ProgramID";
            dropProgram.DataTextField = "ProgramName";
            dropProgram.DataSource = programCollection;
            dropProgram.DataBind();
            dropProgram.Items.FindByText("ALL").Selected = true;
        }
        private void GenderDatabind()
        {
            RefCodeItemDTOCollection genderCollection = null;
            try
            {
                genderCollection = LookupDataBL.Instance.GetRefCode("Gender code");
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }
            
            dropGender.DataValueField = "Code";
            dropGender.DataTextField = "CodeDesc";
            dropGender.DataSource = genderCollection;
            dropGender.DataBind();
            dropGender.Items.Insert(0, new ListItem(" ", "-1"));
            dropGender.SelectedIndex = 0;
        }
        private void RaceDatabind()
        {
            RefCodeItemDTOCollection raceCollection = null;
            try
            {
                raceCollection = LookupDataBL.Instance.GetRefCode("Race code");
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }
            dropRace.DataValueField = "Code";
            dropRace.DataTextField = "CodeDesc";
            dropRace.DataSource = raceCollection;
            dropRace.DataBind();
            dropRace.Items.Insert(0, new ListItem(" ", "-1"));
            dropRace.SelectedIndex = 0;
        }
        private void IndicatorDatabind()
        {
            RefCodeItemDTOCollection indicatorCollection = null;
            try
            {
                indicatorCollection = LookupDataBL.Instance.GetRefCode("Loan 1st 2nd");
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }
            dropIndicators.DataValueField = "Code";
            dropIndicators.DataTextField = "CodeDesc";
            dropIndicators.DataSource = indicatorCollection;
            dropIndicators.DataBind();
            dropIndicators.Items.Insert(0, new ListItem(" ", "-1"));
            dropIndicators.SelectedIndex = 1;
            
        }
        private void HouseholdDatabind()
        {
            RefCodeItemDTOCollection householdCollection = null;
            try
            {
                householdCollection = LookupDataBL.Instance.GetRefCode("Household code");
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }
            dropHouseholdCode.DataValueField = "Code";
            dropHouseholdCode.DataTextField = "CodeDesc";
            dropHouseholdCode.DataSource = householdCollection;
            dropHouseholdCode.DataBind();
            dropHouseholdCode.Items.Insert(0, new ListItem(" ", "-1"));
            dropHouseholdCode.SelectedIndex = 0;
        }
        private void StateDatabind()
        {
            RefCodeItemDTOCollection stateCollection = null;
            try
            {
                stateCollection = LookupDataBL.Instance.GetRefCode("State");
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }
            dropState.DataValueField = "Code";
            dropState.DataTextField = "CodeDesc";
            dropState.DataSource = stateCollection;
            dropState.DataBind();
            dropState.Items.Insert(0, new ListItem(" ", "-1"));
            dropState.SelectedIndex = 0;
        }
        protected void dropFundingSource_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (int.Parse(dropFundingSource.SelectedValue) == -1)
            {
                lst_FundingSourceGroup.DataSource = null;
                lst_FundingSourceGroup.DataBind();
                return;
            }
            ServicerDTOCollection servicers = null;
            try
            {
                servicers = LookupDataBL.Instance.GetServicerByFundingSourceId(int.Parse(dropFundingSource.SelectedValue));
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex);
            }
            lst_FundingSourceGroup.DataSource = servicers;
            lst_FundingSourceGroup.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            InvoiceCaseSearchCriteriaDTO searchCriteria = new InvoiceCaseSearchCriteriaDTO();
            
            try
            {
                searchCriteria.PeriodEnd = DateTime.Parse(txtPeriodEnd.Text);
            }
            catch
            {
                searchCriteria.PeriodEnd = DateTime.MinValue;
            }
            try
            {
                searchCriteria.PeriodStart = DateTime.Parse(txtPeriodStart.Text);
            }
            catch
            {
                searchCriteria.PeriodStart = DateTime.MinValue;
            }
            searchCriteria.FundingSourceId = dropFundingSource.SelectedValue;
            searchCriteria.ProgramId = int.Parse(dropProgram.SelectedValue);
            searchCriteria.Duplicate = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropDuplicates.SelectedValue);
            searchCriteria.Gender = dropGender.SelectedValue;
            searchCriteria.Race = dropRace.SelectedValue;
            searchCriteria.Hispanic = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropHispanic.SelectedValue);
            searchCriteria.Age.Min = (txtAgeMin.Text=="")?int.MinValue:int.Parse(txtAgeMin.Text);
            searchCriteria.Age.Max = (txtAgeMax.Text == "")?int.MinValue:int.Parse(txtAgeMax.Text);
            searchCriteria.HouseholdGrossAnnualIncome.Min = (txtIncomeMin.Text == "") ? double.MinValue: double.Parse(txtIncomeMin.Text);
            searchCriteria.HouseholdGrossAnnualIncome.Max = (txtIncomeMax.Text == "") ? double.MinValue : double.Parse(txtIncomeMax.Text);
            searchCriteria.CompleteCase = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropCaseCompleted.SelectedValue);
            searchCriteria.AlreadyBill = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropAlreadyBilled.SelectedValue);
            searchCriteria.ServicerConsent = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropServicerConsent.SelectedValue);
            searchCriteria.FundingConsent = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropFundingConsent.SelectedValue);
            searchCriteria.MaxNumOfCases = txtMaxNumberofCases.Text==""?int.MinValue:int.Parse(txtMaxNumberofCases.Text);
            searchCriteria.LoanIndicator = dropIndicators.SelectedValue;
            searchCriteria.HouseholdCode = dropHouseholdCode.SelectedValue;
            searchCriteria.City = txtCity.Text;
            searchCriteria.State = dropState.SelectedValue;
            string errorMessage;
            if(InvoiceBL.Instance.ValidateInvoiceCaseCriteria(searchCriteria,out errorMessage)==false)
            {
                lblErrorMessage.Text = errorMessage;
                return;
            }
            Session["searchCriteria"] = searchCriteria;
            Session["fundingSource"] = dropFundingSource.SelectedItem.Text;
            Response.Redirect("NewInvoiceResultPage.aspx");
        }
    }
}