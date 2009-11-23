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
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common;


namespace HPF.FutureState.Web.AppNewInvoice
{
    public partial class AppNewInvoice : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMessages();
                ApplySecurity();
                if (!IsPostBack)
                {
                    FundingSourceDatabind();
                    ProgramDatabind();
                    GenderDatabind();
                    RaceDatabind();
                    HouseholdDatabind();
                    StateDatabind();
                    SetDefaultPeriodStartEnd();
                    AddBlankToYesNoDropDownList();
                    if (Session["IvoiceCaseSearchCriteria"] != null)
                        RestoreSearchCriterial((InvoiceCaseSearchCriteriaDTO)Session["IvoiceCaseSearchCriteria"]);
                    else
                        SetDefaultValueForDropDownList();
                }
                dropFundingSource.SelectedIndexChanged += new EventHandler(dropFundingSource_SelectedIndexChanged1);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        /// <summary>
        /// Only the user with Accouting Edit permission can view this page
        /// </summary>
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_FUNDING_SOURCE_INVOICE))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
        }
        /// <summary>
        /// Restore search Criteria from Session when user click cancel Invoice 
        /// </summary>
        /// <param name="searchCriteria"></param>
        private void RestoreSearchCriterial(InvoiceCaseSearchCriteriaDTO searchCriteria)
        {
            dropFundingSource.Items.FindByValue(searchCriteria.FundingSourceId.ToString()).Selected = true;
            GetServicerList();
            dropProgram.Items.FindByValue(searchCriteria.ProgramId.ToString()).Selected = true;
            txtPeriodStart.Text = searchCriteria.PeriodStart.ToShortDateString();
            txtPeriodEnd.Text = searchCriteria.PeriodEnd.ToShortDateString();
            
            dropGender.Items.FindByValue(searchCriteria.Gender).Selected = true;
            dropRace.Items.FindByValue(searchCriteria.Race).Selected = true;
            
            txtAgeMin.Text = searchCriteria.AgeMin == -1 ? "" : searchCriteria.AgeMin.ToString();
            txtAgeMax.Text = searchCriteria.AgeMax == -1 ? "" : searchCriteria.AgeMax.ToString();
            txtIncomeMin.Text = searchCriteria.HouseholdGrossAnnualIncomeMin == -1 ? "" : searchCriteria.HouseholdGrossAnnualIncomeMin.ToString();
            txtIncomeMax.Text = searchCriteria.HouseholdGrossAnnualIncomeMax == -1 ? "" : searchCriteria.HouseholdGrossAnnualIncomeMax.ToString();
            dropHispanic.Items.FindByValue(((int)searchCriteria.Hispanic).ToString()).Selected = true;
            dropDuplicates.Items.FindByValue(((int)searchCriteria.Duplicate).ToString()).Selected = true; ;
            dropCaseCompleted.Items.FindByValue(((int)searchCriteria.Completed).ToString()).Selected = true; ;
            dropAlreadyBilled.Items.FindByValue(((int)searchCriteria.AlreadyBill).ToString()).Selected = true; ;
            if(searchCriteria.IgnoreFundingConsent!=CustomBoolean.None)
                dropFundingConsent.Items.FindByValue(((int)searchCriteria.IgnoreFundingConsent).ToString()).Selected = true; ;
            txtMaxNumberofCases.Text = searchCriteria.MaxNumOfCases == -1 ? "" : searchCriteria.MaxNumOfCases.ToString();
            dropHouseholdCode.Items.FindByValue(searchCriteria.HouseholdCode).Selected = true;
            txtCity.Text = searchCriteria.City;
            dropState.Items.FindByValue(searchCriteria.State).Selected = true;
            //Restore nonservicer 
            if (lst_FundingSourceGroup.Enabled == true)
            {
                chkFundingAgreement.Checked = searchCriteria.SelectAllServicer;
                chkNeighborworksRejected.Checked = searchCriteria.NeighborworkRejected;
                chkServicerFreddie.Checked = searchCriteria.ServicerRejectedFreddie;
                chkServicerRejected.Checked = searchCriteria.ServicerRejected;
                chkUnfunded.Checked = searchCriteria.SelectUnfunded;
                if (chkUnfunded.Checked)
                    Set4NonServicerOptions(false);
            }
            chkUnableToLocateLoanInPortfolio.Checked = searchCriteria.UnableToLocateLoanInPortfolio;
            chkInvalidCounselingDocument.Checked = searchCriteria.InvalidCounselingDocument;
        }
        /// <summary>
        /// Follow the business rule on the use-case ,Period Start = now - 1 month,Period End = now 
        /// </summary>
        protected void SetDefaultPeriodStartEnd()
        {
            DateTime today = DateTime.Today;
            DateTime startDate = today.AddMonths(-1);
            startDate = startDate.AddDays(-startDate.Day + 1);
            txtPeriodStart.Text = startDate.ToShortDateString();
            txtPeriodEnd.Text = startDate.AddDays(DateTime.DaysInMonth(startDate.Year,startDate.Month)-1).ToShortDateString();
        }
        /// <summary>
        /// Set default Value if there's no searchCriteria store in the session
        /// </summary>
        private void SetDefaultValueForDropDownList()
        {
            //set default value for DDLB
            dropDuplicates.SelectedIndex = 0;
            dropCaseCompleted.SelectedIndex = 0;
            dropAlreadyBilled.SelectedIndex = 1;
            dropFundingConsent.SelectedIndex = 1;
            dropGender.SelectedIndex = 0;
            dropRace.SelectedIndex = 0;
            dropHispanic.SelectedIndex = 0;
            dropHouseholdCode.SelectedIndex = 0;
            dropState.SelectedIndex = 0;
        }
        /// <summary>
        /// Add Blank to DDLB for yes/no/nochoice DDLB
        /// </summary>
        private void AddBlankToYesNoDropDownList()
        {
            AddBlankToDDLB(dropHispanic);
        }
        /// <summary>
        /// insert a blank to dropdowlist at index 0
        /// </summary>
        /// <param name="temp"></param>
        private void AddBlankToDDLB(DropDownList temp)
        {
            temp.Items.Insert(0,new ListItem(" ", "-1"));
        }
        private void FundingSourceDatabind()
        {
            FundingSourceDTOCollection fundingSourceCollection = null;
            try
            {
                fundingSourceCollection = LookupDataBL.Instance.GetFundingSources();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
            /*dropFundingSource.DataValueField = "FundingSourceID";
            dropFundingSource.DataTextField = "FundingSourceName";
            dropFundingSource.DataSource = fundingSourceCollection;
            dropFundingSource.DataBind();*/
            foreach (FundingSourceDTO fsource in fundingSourceCollection)
            {
                if (fsource.ActiveIndicator != Constant.DUPLICATE_YES) continue;
                dropFundingSource.Items.Add(new ListItem(fsource.FundingSourceName, fsource.FundingSourceID.ToString()));
            }            

            dropFundingSource.Items.Remove(dropFundingSource.Items.FindByValue("-1"));
            dropFundingSource.Items.Insert(0, new ListItem(" ", "-1"));
            //first time
            if (Session["fundingSourceId"] != null && Session["IvoiceCaseSearchCriteria"] == null)
            {
                string fundingSourceId = Session["fundingSourceId"].ToString();
                dropFundingSource.Items.FindByValue(fundingSourceId).Selected = true;
                GetServicerList();
            }
        }
        private void Set5NonServicerOptions(bool flag)
        {
            if (flag == false)
            {
                chkFundingAgreement.Checked = false;
                chkNeighborworksRejected.Checked = false;
                chkServicerFreddie.Checked = false;
                chkServicerRejected.Checked = false;
                chkUnfunded.Checked = false;
            }
            else
            {
                chkUnableToLocateLoanInPortfolio.Checked = false;
                chkInvalidCounselingDocument.Checked = false;
            }
            
            chkFundingAgreement.Enabled = flag;
            chkNeighborworksRejected.Enabled = flag;
            chkServicerFreddie.Enabled = flag;
            chkServicerRejected.Enabled = flag;
            chkUnfunded.Enabled = flag;
            dropFundingConsent.Enabled = flag;
            
            chkUnableToLocateLoanInPortfolio.Enabled = !flag;
            chkInvalidCounselingDocument.Enabled = !flag;

            if (flag == true && chkUnfunded.Checked == true)
                Set4NonServicerOptions(false);
            

        }
        #region DataBind
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
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
            dropProgram.DataValueField = "ProgramID";
            dropProgram.DataTextField = "ProgramName";
            dropProgram.DataSource = programCollection;
            dropProgram.DataBind();
            dropProgram.Items.Remove(dropProgram.Items.FindByValue("-1"));
            dropProgram.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        private void GenderDatabind()
        {
            RefCodeItemDTOCollection genderCollection = null;
            try
            {
                genderCollection = LookupDataBL.Instance.GetRefCodes(Constant.REF_CODE_SET_GENDER_CODE);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
            
            dropGender.DataValueField = "CodeValue";
            dropGender.DataTextField = "CodeDescription";
            dropGender.DataSource = genderCollection;
            dropGender.DataBind();
            dropGender.Items.Insert(0, new ListItem(" ", "-1"));
        }
        private void RaceDatabind()
        {
            RefCodeItemDTOCollection raceCollection = null;
            try
            {
                raceCollection = LookupDataBL.Instance.GetRefCodes(Constant.REF_CODE_SET_RACE_CODE);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
            dropRace.DataValueField = "CodeValue";
            dropRace.DataTextField = "CodeDescription";
            dropRace.DataSource = raceCollection;
            dropRace.DataBind();
            dropRace.Items.Insert(0, new ListItem(" ", "-1"));
        }
        
        private void HouseholdDatabind()
        {
            RefCodeItemDTOCollection householdCollection = null;
            try
            {
                householdCollection = LookupDataBL.Instance.GetRefCodes(Constant.REF_CODE_SET_HOUSEHOLD_CODE);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
            dropHouseholdCode.DataValueField = "CodeValue";
            dropHouseholdCode.DataTextField = "CodeDescription";
            dropHouseholdCode.DataSource = householdCollection;
            dropHouseholdCode.DataBind();
            dropHouseholdCode.Items.Insert(0, new ListItem(" ", "-1"));
        }
        private void StateDatabind()
        {
            RefCodeItemDTOCollection stateCollection = null;
            try
            {
                stateCollection = LookupDataBL.Instance.GetRefCodes(Constant.REF_CODE_SET_STATE_CODE);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
            dropState.DataValueField = "CodeValue";
            dropState.DataTextField = "CodeDescription";
            dropState.DataSource = stateCollection;
            dropState.DataBind();
            dropState.Items.Insert(0, new ListItem(" ", "-1"));
        }
        #endregion
        protected void dropFundingSource_SelectedIndexChanged1(object sender, EventArgs e)
        {
            GetServicerList();
        }

        private void GetServicerList()
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
                foreach(var i in servicers)
                    if (i.ServicerName == "ALL")
                    {
                        servicers.Remove(i);
                        break;
                    }
                //Non-Servicer
                Set5NonServicerOptions(servicers.Count==0);
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
                return;
            }
            lst_FundingSourceGroup.DataSource = servicers;
            lst_FundingSourceGroup.DataBind();
            
        }
        private void AddToErrorMessage(string s)
        {
            lblErrorMessage.Items.Add(new ListItem(s));
        }
        private void ClearErrorMessages()
        {
            lblErrorMessage.Items.Clear();
        }
        private ExceptionMessage GetExceptionMessage(string errorCode)
        {
            var exMes = new ExceptionMessage();
            exMes.ErrorCode = errorCode;
            exMes.Message = ErrorMessages.GetExceptionMessageCombined(errorCode);
            return exMes;
        }
        private ExceptionMessage GetExceptionMessageWithoutCode(string errorCode)
        {
            var exMes = new ExceptionMessage();
            exMes.ErrorCode = errorCode;
            exMes.Message = ErrorMessages.GetExceptionMessage(errorCode);
            return exMes;
        }
        DateTime SetToStartDay(DateTime t)
        {
            t=t.AddHours(-t.Hour);
            t=t.AddMinutes(-t.Minute);
            t=t.AddSeconds(-t.Second);
            t=t.AddMilliseconds(-t.Millisecond);
            return t;
        }
        DateTime SetToEndDay(DateTime t)
        {
            t = SetToStartDay(t);
            t=t.AddDays(1);
            t = t.AddSeconds(-1);
            return t;
        }
        /// <summary>
        /// Collect Invoice Criteria , if  not success the throw DataValidation Exception
        /// </summary>
        /// <returns></returns>
        private InvoiceCaseSearchCriteriaDTO GetInvoiceCaseSearchCriteria()
        {
            DataValidationException ex = new DataValidationException();
            InvoiceCaseSearchCriteriaDTO searchCriteria = new InvoiceCaseSearchCriteriaDTO();
            searchCriteria.ServicerConsentQty = 1;
            searchCriteria.PeriodStart = ConvertToDateTime(txtPeriodStart.Text);
            if(searchCriteria.PeriodStart!=DateTime.MinValue)
                searchCriteria.PeriodStart = SetToStartDay(searchCriteria.PeriodStart);
            
            searchCriteria.PeriodEnd = ConvertToDateTime(txtPeriodEnd.Text);
            if (searchCriteria.PeriodEnd != DateTime.MinValue)
                searchCriteria.PeriodEnd = SetToEndDay(searchCriteria.PeriodEnd);
            
            //a program is require
            searchCriteria.ProgramId = dropProgram.SelectedValue;
            searchCriteria.FundingSourceId = ConvertToInt(dropFundingSource.SelectedValue);

            searchCriteria.UnableToLocateLoanInPortfolio = chkUnableToLocateLoanInPortfolio.Checked;
            searchCriteria.InvalidCounselingDocument = chkInvalidCounselingDocument.Checked;

            searchCriteria.Duplicate = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropDuplicates.SelectedValue);
            searchCriteria.Gender = dropGender.SelectedValue;
            searchCriteria.Race = dropRace.SelectedValue;
            searchCriteria.Hispanic = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropHispanic.SelectedValue);
            searchCriteria.AgeMin = ConvertToInt(txtAgeMin.Text);
            
            searchCriteria.AgeMax = ConvertToInt(txtAgeMax.Text);
            
            searchCriteria.HouseholdGrossAnnualIncomeMin = ConvertToDouble(txtIncomeMin.Text);
            
            searchCriteria.HouseholdGrossAnnualIncomeMax = ConvertToDouble(txtIncomeMax.Text);
            
            searchCriteria.Completed = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropCaseCompleted.SelectedValue);
            searchCriteria.AlreadyBill = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropAlreadyBilled.SelectedValue);
            searchCriteria.IgnoreFundingConsent= (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropFundingConsent.SelectedValue);
            if (dropFundingConsent.Enabled == false)
                searchCriteria.IgnoreFundingConsent = CustomBoolean.None;
            searchCriteria.MaxNumOfCases = ConvertToInt(txtMaxNumberofCases.Text);
            if (searchCriteria.MaxNumOfCases == 0)
                searchCriteria.MaxNumOfCases = int.MinValue;
            
            searchCriteria.HouseholdCode = dropHouseholdCode.SelectedValue;
            searchCriteria.City = txtCity.Text.Trim();
            searchCriteria.State = dropState.SelectedValue;
            if (chkUnfunded.Enabled == false)
                SetNonServicerToFalse(searchCriteria);
            else
                if (dropFundingSource.SelectedValue!="-1")
                    GetNonServicer(searchCriteria);
            return searchCriteria;
        }

        private void GetNonServicer(InvoiceCaseSearchCriteriaDTO searchCriteria)
        {
            searchCriteria.ServicerRejected = chkServicerRejected.Checked;
            searchCriteria.ServicerRejectedFreddie = chkServicerFreddie.Checked;
            searchCriteria.NeighborworkRejected = chkNeighborworksRejected.Checked;
            searchCriteria.SelectAllServicer = chkFundingAgreement.Checked;
            searchCriteria.SelectUnfunded = chkUnfunded.Checked;
            searchCriteria.ServicerConsentQty = 0;
        }

        private static void SetNonServicerToFalse(InvoiceCaseSearchCriteriaDTO searchCriteria)
        {
            searchCriteria.ServicerRejected = false;
            searchCriteria.ServicerRejectedFreddie = false;
            searchCriteria.NeighborworkRejected = false;
            searchCriteria.SelectAllServicer = false;
            searchCriteria.SelectUnfunded = false;
        }
        
        /// <summary>
        /// Draft new Invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DraftNewInvoice_Click(object sender, EventArgs e)
        {
            ClearErrorMessages();
            try
            {
                InvoiceCaseSearchCriteriaDTO searchCriteria = GetInvoiceCaseSearchCriteria();
                InvoiceBL.Instance.ValidateInvoiceCaseSearchCriteria(searchCriteria);
                Session["IvoiceCaseSearchCriteria"] = searchCriteria;
                Session["fundingSource"] = dropFundingSource.SelectedItem.Text;
            }
            catch (DataValidationException ex)
            {
                lblErrorMessage.DataSource = ex.ExceptionMessages;
                lblErrorMessage.DataBind();
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                return;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem( ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                return;
            }

            Response.Redirect("NewInvoiceSelection.aspx");
        }
        private DateTime ConvertToDateTime(object obj)
        {
            DateTime dt;
            if (DateTime.TryParse(obj.ToString().Trim(), out dt))
                return dt;
            return DateTime.MinValue;
        }
        private int ConvertToInt(object obj)
        {
            if (string.IsNullOrEmpty(obj.ToString().Trim()))
                return -1;
            int value;
            if (int.TryParse(obj.ToString().Trim().Replace(",",""), out value))
                if(value>=0)
                    return value;
            return int.MinValue;
        }
        private double ConvertToDouble(object obj)
        {
            if (string.IsNullOrEmpty(obj.ToString().Trim()))
                return -1;
            double value;
            if (double.TryParse(obj.ToString().Trim(), out value))
                if(value>=0)
                    return value;
            return double.MinValue;
        }

        protected void chkUnfunded_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUnfunded.Checked)
            {
                chkFundingAgreement.Checked = false;
                chkNeighborworksRejected.Checked = false;
                chkServicerFreddie.Checked = false;
                chkServicerRejected.Checked = false;
                Set4NonServicerOptions(false);
            }
            else
            {
                Set4NonServicerOptions(true);
            }
        }

        private void Set4NonServicerOptions(bool value)
        {
            chkFundingAgreement.Enabled = value;
            chkNeighborworksRejected.Enabled = value;
            chkServicerFreddie.Enabled = value;
            chkServicerRejected.Enabled = value;
        }
    }
}