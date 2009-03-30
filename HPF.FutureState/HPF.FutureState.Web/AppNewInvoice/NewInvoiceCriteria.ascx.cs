﻿using System;
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
            }
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
                fundingSourceCollection = LookupDataBL.Instance.GetFundingSource();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
            dropFundingSource.DataValueField = "FundingSourceID";
            dropFundingSource.DataTextField = "FundingSourceName";
            dropFundingSource.DataSource = fundingSourceCollection;
            dropFundingSource.DataBind();
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
        private void ServicerExists(bool flag)
        {
            chkFundingAgreement.Enabled = !flag;
            chkNeighborworksRejected.Enabled = !flag;
            chkServicerFreddie.Enabled = !flag;
            chkServicerRejected.Enabled = !flag;
            chkUnfunded.Enabled = !flag;

            dropFundingConsent.Enabled = !flag;

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
                genderCollection = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_GENDER_CODE);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
            
            dropGender.DataValueField = "Code";
            dropGender.DataTextField = "CodeDesc";
            dropGender.DataSource = genderCollection;
            dropGender.DataBind();
            dropGender.Items.Insert(0, new ListItem(" ", "-1"));
        }
        private void RaceDatabind()
        {
            RefCodeItemDTOCollection raceCollection = null;
            try
            {
                raceCollection = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_RACE_CODE);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
            dropRace.DataValueField = "Code";
            dropRace.DataTextField = "CodeDesc";
            dropRace.DataSource = raceCollection;
            dropRace.DataBind();
            dropRace.Items.Insert(0, new ListItem(" ", "-1"));
        }
        
        private void HouseholdDatabind()
        {
            RefCodeItemDTOCollection householdCollection = null;
            try
            {
                householdCollection = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_HOUSEHOLD_CODE);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
            dropHouseholdCode.DataValueField = "Code";
            dropHouseholdCode.DataTextField = "CodeDesc";
            dropHouseholdCode.DataSource = householdCollection;
            dropHouseholdCode.DataBind();
            dropHouseholdCode.Items.Insert(0, new ListItem(" ", "-1"));
        }
        private void StateDatabind()
        {
            RefCodeItemDTOCollection stateCollection = null;
            try
            {
                stateCollection = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_STATE_CODE);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
            dropState.DataValueField = "Code";
            dropState.DataTextField = "CodeDesc";
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
                ServicerExists(!(servicers.Count==0));
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
            //try
            //{
            //    searchCriteria.PeriodStart = DateTime.Parse(txtPeriodStart.Text);
            //    searchCriteria.PeriodStart = SetToStartDay(searchCriteria.PeriodStart);
            //    if (searchCriteria.PeriodStart.Year < 1753)
            //        throw (new Exception());
            //}
            //catch
            //{
            //    ExceptionMessage exMes = GetExceptionMessage(ErrorMessages.ERR0562);
            //    ex.ExceptionMessages.Add(exMes);
            //}
            searchCriteria.PeriodEnd = ConvertToDateTime(txtPeriodEnd.Text);
            if (searchCriteria.PeriodEnd != DateTime.MinValue)
                searchCriteria.PeriodEnd = SetToEndDay(searchCriteria.PeriodEnd);
            //try
            //{
            //    searchCriteria.PeriodEnd = DateTime.Parse(txtPeriodEnd.Text);
            //    searchCriteria.PeriodEnd = SetToEndDay(searchCriteria.PeriodEnd);
            //    if (searchCriteria.PeriodEnd.Year < 1753)
            //        throw (new Exception());
            //}
            //catch
            //{
            //    ExceptionMessage exMes = GetExceptionMessage(ErrorMessages.ERR0563);
            //    ex.ExceptionMessages.Add(exMes);
            //}
            
            //a program is require
            searchCriteria.ProgramId = dropProgram.SelectedValue;
            searchCriteria.FundingSourceId = ConvertToInt(dropFundingSource.SelectedValue);
            //if (searchCriteria.FundingSourceId == -1)
            //{ 
            //    ExceptionMessage exMes = GetExceptionMessage(ErrorMessages.ERR0561);
            //    ex.ExceptionMessages.Add(exMes);
            //}
            searchCriteria.Duplicate = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropDuplicates.SelectedValue);
            searchCriteria.Gender = dropGender.SelectedValue;
            searchCriteria.Race = dropRace.SelectedValue;
            searchCriteria.Hispanic = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropHispanic.SelectedValue);
            searchCriteria.AgeMin = ConvertToInt(txtAgeMin.Text);
            //try
            //{
            //    searchCriteria.AgeMin = (txtAgeMin.Text == "") ? -1 : int.Parse(txtAgeMin.Text);
            //    if(searchCriteria.AgeMin!=int.MinValue)
            //        if (searchCriteria.AgeMin > 200||searchCriteria.AgeMin<0)
            //            throw (new Exception());
            //}
            //catch
            //{
            //    ExceptionMessage exMes = GetExceptionMessage(ErrorMessages.ERR0570);
            //    ex.ExceptionMessages.Add(exMes);
            //}
            searchCriteria.AgeMax = ConvertToInt(txtAgeMax.Text);
            //try
            //{
            //    searchCriteria.AgeMax = (txtAgeMax.Text == "") ? -1 : int.Parse(txtAgeMax.Text);
            //    if (searchCriteria.AgeMax !=int.MinValue)
            //        if (searchCriteria.AgeMax > 200||searchCriteria.AgeMax<0)
            //            throw (new Exception());
            //}
            //catch
            //{
            //    ExceptionMessage exMes = GetExceptionMessage(ErrorMessages.ERR0571);
            //    ex.ExceptionMessages.Add(exMes);
            //}
            searchCriteria.HouseholdGrossAnnualIncomeMin = ConvertToDouble(txtIncomeMin.Text);
            //try
            //{
            //    searchCriteria.HouseholdGrossAnnualIncomeMin = (txtIncomeMin.Text == "") ? -1 : double.Parse(txtIncomeMin.Text);
            //    if(searchCriteria.HouseholdGrossAnnualIncomeMin!=double.MinValue)
            //        if (searchCriteria.HouseholdGrossAnnualIncomeMin >=  100000000||searchCriteria.HouseholdGrossAnnualIncomeMin<0)
            //            throw (new Exception());
            //}
            //catch
            //{
            //    ExceptionMessage exMes = GetExceptionMessage(ErrorMessages.ERR0572);
            //    ex.ExceptionMessages.Add(exMes);
            //}
            searchCriteria.HouseholdGrossAnnualIncomeMax = ConvertToDouble(txtIncomeMax.Text);
            //try
            //{
            //    searchCriteria.HouseholdGrossAnnualIncomeMax = (txtIncomeMax.Text == "") ? -1 : double.Parse(txtIncomeMax.Text);
            //    if (searchCriteria.HouseholdGrossAnnualIncomeMax !=double.MinValue)
            //        if (searchCriteria.HouseholdGrossAnnualIncomeMax >= 100000000||searchCriteria.HouseholdGrossAnnualIncomeMax<0)
            //            throw (new Exception());
            //}
            //catch
            //{
            //    ExceptionMessage exMes = GetExceptionMessage(ErrorMessages.ERR0573);
            //    ex.ExceptionMessages.Add(exMes);
            //}

            searchCriteria.Completed = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropCaseCompleted.SelectedValue);
            searchCriteria.AlreadyBill = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropAlreadyBilled.SelectedValue);
            searchCriteria.IgnoreFundingConsent= (CustomBoolean)Enum.Parse(typeof(CustomBoolean), dropFundingConsent.SelectedValue);
            if (dropFundingConsent.Enabled == false)
                searchCriteria.IgnoreFundingConsent = CustomBoolean.None;
            searchCriteria.MaxNumOfCases = ConvertToInt(txtMaxNumberofCases.Text);
            //Max Num of cases
            //try
            //{
            //    searchCriteria.MaxNumOfCases = txtMaxNumberofCases.Text == "" ? int.MinValue : int.Parse(txtMaxNumberofCases.Text.Replace(",",""));
            //    if(searchCriteria.MaxNumOfCases!=int.MinValue)
            //        if (searchCriteria.MaxNumOfCases > 65000||searchCriteria.MaxNumOfCases<=0)
            //            throw (new Exception());
            //}
            //catch
            //{
            //    ExceptionMessage exMes = GetExceptionMessage(ErrorMessages.ERR0569);
            //    ex.ExceptionMessages.Add(exMes);
            //}
            searchCriteria.HouseholdCode = dropHouseholdCode.SelectedValue;
            searchCriteria.City = txtCity.Text.Trim();
            //if (txtCity.Text.Length > 30)
            //{
            //    ExceptionMessage exMes = GetExceptionMessageWithoutCode(ErrorMessages.ERR0986);
            //    ex.ExceptionMessages.Add(exMes);
            //}
            //else
                
            searchCriteria.State = dropState.SelectedValue;
            if (chkUnfunded.Enabled == false)
                SetNonServicerToFalse(searchCriteria);
            else
                //Check for error 0564 here
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
                Response.Redirect("NewInvoiceSelection.aspx");
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
            }

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
            if (int.TryParse(obj.ToString().Trim(), out value))
                return value;
            return int.MinValue;
        }
        private double ConvertToDouble(object obj)
        {
            if (string.IsNullOrEmpty(obj.ToString().Trim()))
                return -1;
            double value;
            if (double.TryParse(obj.ToString().Trim(), out value))
                return value;
            return double.MinValue;
        }
    }
}