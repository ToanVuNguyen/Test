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

namespace HPF.FutureState.Web.AppViewEditInvoice
{
    public partial class ViewEditInvoice : System.Web.UI.UserControl
    {
        int invoiceID=-1;
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplySecurity();
            try
            {
                invoiceID = int.Parse(Request.QueryString["id"]);
                if (!IsPostBack)
                {
                    BindDataToRejectReason();
                    if (invoiceID != -1)
                        LoadInvoiceSet();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.DisplayName);
            }
        }
        /// <summary>
        /// Load Invoice and InvoiceCases to display
        /// </summary>
        private void LoadInvoiceSet()
        {
            InvoiceSetDTO invoiceSet= InvoiceBL.Instance.GetInvoiceSet(invoiceID);
            BindInvoice(invoiceSet);
            BindInvoiceCases(invoiceSet);
        }
        private void BindInvoiceCases(InvoiceSetDTO invoiceSet)
        {
            InvoiceCaseDTOCollection invoiceCases = invoiceSet.InvoiceCases;
            grvViewEditInvoice.DataSource = invoiceCases;
            grvViewEditInvoice.DataBind();
        }
        private void BindInvoice(InvoiceSetDTO invoiceSet)
        {
            InvoiceDTO invoice = invoiceSet.Invoice;
            lblFundingSource.Text = invoice.FundingSourceName;
            lblInvoiceNumber.Text = invoice.InvoiceId.ToString();
            lblInvoiceTotal.Text = invoice.InvoiceBillAmount.ToString("C");
            lblPeriodEnd.Text = invoice.PeriodEndDate.ToShortDateString();
            lblPeriodStart.Text = invoice.PeriodStartDate.ToShortDateString();
            lblTotalCases.Text = invoiceSet.TotalCases.ToString() ;
            lblTotalPaid.Text = invoiceSet.TotalPaid.ToString("C");
            lblTotalRejected.Text = invoiceSet.TotalRejected.ToString("C");
        }
        /// <summary>
        /// only user with edit permision can access this page
        /// </summary>
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_FUNDING_SOURCE_INVOICE))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
        }
        /// <summary>
        /// Bind data to Reject Reason dropdown box
        /// </summary>
        private void BindDataToRejectReason()
        {
            try
            {
                RefCodeItemDTOCollection paymentRejectCode = LookupDataBL.Instance.GetRefCode("payment reject reason code");
                dropRejectReason.DataValueField = "Code";
                dropRejectReason.DataTextField = "CodeDesc";
                dropRejectReason.DataSource = paymentRejectCode;
                dropRejectReason.DataBind();
                dropRejectReason.Items.Insert(0, new ListItem(" ", "-1"));
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblErrorMessage.Visible = true;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.DisplayName);
            }
        }
        protected void chkCheckAllCheck(object sender, EventArgs e)
        {
            CheckBox headerCheckbox = (CheckBox)sender;
            foreach (GridViewRow row in grvViewEditInvoice.Rows)
            {
                CheckBox chkSelected = (CheckBox)row.FindControl("chkSelected");
                if (chkSelected != null)
                    chkSelected.Checked = headerCheckbox.Checked;
            }
        }
    }
}