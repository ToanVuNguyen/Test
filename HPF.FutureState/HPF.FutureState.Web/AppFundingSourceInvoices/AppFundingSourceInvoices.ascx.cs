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
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common;


namespace HPF.FutureState.Web.AppFundingSourceInvoices
{
    public partial class AppFundingSourceInvoices : System.Web.UI.UserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (grvFundingSourceInvoices.SelectedValue == null)
                SelectedRowIndex.Value = "";
            lblPortal.PostBackUrl = HPFConfigurationSettings.HPF_INVOICE_PORTAL_URL;
            ClearErrorMessages();
            try
            {
                ApplySecurity();
                if (!IsPostBack)
                {
                    GetFundingSourceList();
                    SetUpDefaultValue();
                    btnCancelInvoice.Attributes.Add("onclick", " return CancelClientClick();");
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem( ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_FUNDING_SOURCE_INVOICE))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_FUNDING_SOURCE_INVOICE))
            {
                btnNewInvoice.Enabled = false;
                btnCancelInvoice.Enabled = false;
            }
        }
        /// <summary>
        /// Default page display "All" funding sources and start from today - 6 months and now
        /// </summary>
        private void SetUpDefaultValue()
        {
            InvoiceSearchCriteriaDTO defaultSearchCriteria = new InvoiceSearchCriteriaDTO();
            defaultSearchCriteria.FundingSourceId = -1;
            defaultSearchCriteria.PeriodStart = DateTime.Today.AddMonths(-6);
            defaultSearchCriteria.PeriodEnd = SetToEndDay(DateTime.Today);
            txtPeriodStart.Text = defaultSearchCriteria.PeriodStart.ToShortDateString();
            txtPeriodEnd.Text = defaultSearchCriteria.PeriodEnd.ToShortDateString();
            InvoiceSearch(defaultSearchCriteria);
        }
        private void GetFundingSourceList()
        {
                FundingSourceDTOCollection fundingSourceCollection = LookupDataBL.Instance.GetFundingSource();
                dropFundingSource.DataValueField = "FundingSourceID";
                dropFundingSource.DataTextField = "FundingSourceName";
                dropFundingSource.DataSource = fundingSourceCollection;
                dropFundingSource.DataBind();
                dropFundingSource.Items.FindByText(" ALL").Selected = true;
            
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
            t = t.AddHours(-t.Hour);
            t = t.AddMinutes(-t.Minute);
            t = t.AddSeconds(-t.Second);
            t = t.AddMilliseconds(-t.Millisecond);
            return t;
        }
        DateTime SetToEndDay(DateTime t)
        {
            t = SetToStartDay(t);
            t = t.AddDays(1);
            t = t.AddSeconds(-1);
            return t;
        }
        private InvoiceSearchCriteriaDTO GetInvoiceSearchCriterial()
        {
            DataValidationException ex = new DataValidationException();
            InvoiceSearchCriteriaDTO searchCriteria = new InvoiceSearchCriteriaDTO();
            searchCriteria.FundingSourceId = int.Parse(dropFundingSource.SelectedValue);
            txtPeriodStart.Text = txtPeriodStart.Text.Trim();
            txtPeriodEnd.Text = txtPeriodEnd.Text.Trim();
            if (txtPeriodStart.Text == string.Empty)
            {
                ExceptionMessage exMes = GetExceptionMessage(ErrorMessages.ERR0562);
                ex.ExceptionMessages.Add(exMes);
            }
            else
            {
                try
                {
                    searchCriteria.PeriodStart = DateTime.Parse(txtPeriodStart.Text);
                    searchCriteria.PeriodStart = SetToStartDay(searchCriteria.PeriodStart);
                    if (searchCriteria.PeriodStart.Year < 1753)
                        throw (new Exception());
                }
                catch
                {
                    ExceptionMessage exMes = GetExceptionMessageWithoutCode(ErrorMessages.ERR0997);
                    ex.ExceptionMessages.Add(exMes);
                }
            }
            if (txtPeriodEnd.Text == string.Empty)
            {
                ExceptionMessage exMes = GetExceptionMessage(ErrorMessages.ERR0563);
                ex.ExceptionMessages.Add(exMes);
            }
            else
            {
                try
                {
                    searchCriteria.PeriodEnd = DateTime.Parse(txtPeriodEnd.Text);
                    searchCriteria.PeriodEnd = SetToEndDay(searchCriteria.PeriodEnd);
                    if (searchCriteria.PeriodEnd.Year < 1753)
                        throw (new Exception());
                }
                catch
                {
                    ExceptionMessage exMes = GetExceptionMessageWithoutCode(ErrorMessages.ERR0996);
                    ex.ExceptionMessages.Add(exMes);
                }
            }
            if (ex.ExceptionMessages.Count > 0)
                throw (ex);
            return searchCriteria;

        }
        private void ClearErrorMessages()
        {
            lblErrorMessage.Items.Clear();
        }
        protected void btnRefreshList_Click(object sender, EventArgs e)
        {
            ClearErrorMessages();
            InvoiceSearchCriteriaDTO searchCriteria = null;
            try
            {
                searchCriteria = GetInvoiceSearchCriterial();
            }
            catch (DataValidationException ex)
            {
                foreach (var mes in ex.ExceptionMessages)
                    lblErrorMessage.Items.Add(new ListItem(mes.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                return;
            }
            InvoiceSearch(searchCriteria);
        }

        private void InvoiceSearch(InvoiceSearchCriteriaDTO searchCriteria)
        {
            try
            {
                InvoiceDTOCollection searchResult = InvoiceBL.Instance.InvoiceSearch(searchCriteria);
                Session["searchResult"] = searchResult;
                Session["invoiceSearchCriteria"] = searchCriteria;
                grvFundingSourceInvoices.DataSource = searchResult;
                grvFundingSourceInvoices.DataBind();
                if (searchResult == null)
                {
                    DataValidationException ex = new DataValidationException(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0566));
                    throw ex;
                }
            }
            
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }

        /// <summary>
        /// Show the gridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name0.="e"></param>
        protected void grvFundingSourceInvoices_DataBound(object sender, EventArgs e)
        {
            lblInvoiceList.Visible = true;
        }

        protected void grvFundingSourceInvoices_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lblInvoice = e.Row.FindControl("lblInvoiceDate") as Label;
            if (lblInvoice != null)
            {
                string date = (e.Row.DataItem as InvoiceDTO).InvoiceDate == null ? "" : (e.Row.DataItem as InvoiceDTO).InvoiceDate.Value.ToShortDateString();
                lblInvoice.Text = date;
            }
        }
        /// <summary>
        /// forward fundingSourceId to next Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewInvoice_Click(object sender, EventArgs e)
        {
            Session["fundingSourceId"] = dropFundingSource.SelectedValue;
            Session["IvoiceCaseSearchCriteria"] = null;
            Response.Redirect("CreateNewInvoice.aspx");
        }

        protected void btnCancelInvoice_Click(object sender, EventArgs e)
        {
            ClearErrorMessages();
            if (grvFundingSourceInvoices.SelectedValue == null)
            {
                lblErrorMessage.Items.Add(new ListItem(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0567)));
                return;
            }
            if (Session["searchResult"] == null)
                return;
            InvoiceDTOCollection searchResult = Session["searchResult"] as InvoiceDTOCollection;
            InvoiceDTO invoice = searchResult[grvFundingSourceInvoices.SelectedRow.DataItemIndex];
            if (invoice.InvoicePaymentAmount > 0)
            {
                lblErrorMessage.Items.Add(new ListItem(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0550)));
                return;
            }
            try
            {
                string cancelCode = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_INVOICE_STATUS_CODE)[1].Code;
                //Update to Database
                invoice.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
                invoice.StatusCode = cancelCode;

                InvoiceBL.Instance.UpdateInvoice(invoice);
                if (Session["invoiceSearchCriteria"] == null)
                    return;
                //search again to refresh the grid
                InvoiceSearchCriteriaDTO searchCriteria = Session["invoiceSearchCriteria"] as InvoiceSearchCriteriaDTO;
                InvoiceSearch(searchCriteria);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }

        protected void btnViewEditInvoice_Click(object sender, EventArgs e)
        {
            ClearErrorMessages();
            if (grvFundingSourceInvoices.SelectedValue == null)
            {
                lblErrorMessage.Items.Add(new ListItem(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0568)));
                return;
            }
            int invoiceId = (int)grvFundingSourceInvoices.SelectedValue;
            Response.Redirect("InvoiceInfo.aspx?id=" + invoiceId.ToString());
        }

        protected void grvFundingSourceInvoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearErrorMessages();
            if (grvFundingSourceInvoices.SelectedValue != null)
                SelectedRowIndex.Value = grvFundingSourceInvoices.SelectedValue.ToString();
            
        }
    }
}