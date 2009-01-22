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
            try
            {
                ApplySecurity();
                if (!IsPostBack)
                {
                    GetFundingSourceList();
                    SetUpDefaultValue();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblErrorMessage.Visible = true;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_FUNDING_SOURCE_INVOICE))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR999");
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
            defaultSearchCriteria.PeriodEnd = DateTime.Today;
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
                dropFundingSource.Items.FindByText("ALL").Selected = true;
            
        }
        protected void btnRefreshList_Click(object sender, EventArgs e)
        {
            InvoiceSearchCriteriaDTO searchCriteria = new InvoiceSearchCriteriaDTO();
            searchCriteria.FundingSourceId = int.Parse(dropFundingSource.SelectedValue);
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
            InvoiceSearch(searchCriteria);
            
        }

        private void InvoiceSearch(InvoiceSearchCriteriaDTO searchCriteria)
        {
            try
            {
                InvoiceDTOCollection searchResult = InvoiceBL.Instance.InvoiceSearch(searchCriteria);
                Session["searchResult"] = searchResult;
                Session["searchCriteria"] = searchCriteria;
                grvFundingSourceInvoices.DataSource = searchResult;
                grvFundingSourceInvoices.DataBind();
            }
            catch (DataValidationException ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblErrorMessage.Visible = true;
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
            catch (DataException ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblErrorMessage.Visible = true;
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }

        /// <summary>
        /// Show the gridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvFundingSourceInvoices_DataBound(object sender, EventArgs e)
        {
            
            lblInvoiceList.Visible = true;
            lblErrorMessage.Visible = false;
        }

        protected void grvFundingSourceInvoices_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lblInvoice = e.Row.FindControl("lblInvoiceDate") as Label;
            if (lblInvoice != null)
            {
                string date = (e.Row.DataItem as InvoiceDTO).InvoiceDate.ToShortDateString();
                lblInvoice.Text = date;
                if (date == "1/1/0001")
                    lblInvoice.Text = "N/A";
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
            Response.Redirect("AppNewInvoiceCriteriaPage.aspx");
        }

        protected void btnCancelInvoice_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
            if (grvFundingSourceInvoices.SelectedIndex== -1)
                return;
            if (Session["searchResult"] == null)
                return;
            InvoiceDTOCollection searchResult = Session["searchResult"] as InvoiceDTOCollection;
            InvoiceDTO invoice = searchResult[grvFundingSourceInvoices.SelectedRow.DataItemIndex];
            if(invoice.InvoicePaymentAmount>0)
            {
                lblErrorMessage.Text = ErrorMessages.GetExceptionMessage("WARN0550");
                lblErrorMessage.Visible = true;
                return;
            }
            try
            {
                string cancelCode = LookupDataBL.Instance.GetRefCode("invoice status code")[1].Code;
                if (invoice.StatusCode == cancelCode)
                {
                    lblErrorMessage.Text = "This Invoice has been Cancel";
                    lblErrorMessage.Visible = true;
                    return;
                }
                //Update to Database
                invoice.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
                invoice.StatusCode = cancelCode;
            
                InvoiceBL.Instance.UpdateInvoice(invoice);
                if (Session["searchCriteria"] == null)
                    return;
                InvoiceSearchCriteriaDTO searchCriteria = Session["searchCriteria"] as InvoiceSearchCriteriaDTO;
                InvoiceSearch(searchCriteria);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblErrorMessage.Visible = true;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
    }
}