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
using System.Web.UI.MobileControls;
using System.Collections.Generic;

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
                lblErrorMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.DisplayName);
            }
        }

        /// <summary>
        /// Load Invoice and InvoiceCases to display
        /// </summary>
        private void LoadInvoiceSet()
        {
            InvoiceSetDTO invoiceSet= InvoiceBL.Instance.GetInvoiceSet(invoiceID);
            Session["invoiceSet"] = invoiceSet;
            BindInvoice(invoiceSet);
            BindInvoiceCases(invoiceSet);
        }
        private void BindInvoiceCases(InvoiceSetDTO invoiceSet)
        {
            InvoiceCaseDTOCollection invoiceCases = invoiceSet.InvoiceCases;
            grvViewEditInvoice.DataSource = invoiceCases;
            grvViewEditInvoice.DataBind();
        }
        /// <summary>
        /// Bind Invoice Info to labels
        /// </summary>
        /// <param name="invoiceSet"></param>
        private void BindInvoice(InvoiceSetDTO invoiceSet)
        {
            InvoiceDTO invoice = invoiceSet.Invoice;
            lblFundingSource.Text = invoice.FundingSourceName;
            lblInvoiceNumber.Text = invoice.InvoiceId.ToString();
            lblInvoiceTotal.Text = invoice.InvoiceBillAmount == null ? "" : invoice.InvoiceBillAmount.Value.ToString("C");
            lblPeriodEnd.Text = invoice.PeriodEndDate == null ? "" : invoice.PeriodEndDate.Value.ToShortDateString();
            lblPeriodStart.Text = invoice.PeriodStartDate == null ? "" : invoice.PeriodStartDate.Value.ToShortDateString();
            lblTotalCases.Text = invoiceSet.TotalCases.ToString() ;
            lblTotalPaid.Text = invoiceSet.Invoice.InvoicePaymentAmount == null ? "" : invoiceSet.Invoice.InvoicePaymentAmount.Value.ToString("C");
            lblTotalRejected.Text = invoiceSet.TotalRejected.ToString("C");
            txtInvoiceComments.Text = invoiceSet.Invoice.InvoiceComment;
            lblTotalCase1.Text = invoiceSet.TotalCases.ToString();
            lblTotalPaid1.Text = invoiceSet.TotalPaid.ToString("C");
            lblInvoiceTotal1.Text = invoice.InvoiceBillAmount == null ? "" : invoice.InvoiceBillAmount.Value.ToString("C");
        }
        /// <summary>
        /// only user with edit permision can access this page
        /// </summary>
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_FUNDING_SOURCE_INVOICE))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_FUNDING_SOURCE_INVOICE))
            {
                txtInvoiceComments.Enabled = false;
                txtPaymentID.Enabled = false;
                dropRejectReason.Enabled = false;
                btnPay.Enabled = false;
                btnReject.Enabled = false;
                btnUnpay.Enabled = false;
            }
        }
        /// <summary>
        /// Bind data to Reject Reason dropdown box
        /// </summary>
        private void BindDataToRejectReason()
        {
            try
            {
                RefCodeItemDTOCollection paymentRejectCode = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_PAYMENT_REJECT_REASON_CODE);
                dropRejectReason.DataValueField = "Code";
                dropRejectReason.DataTextField = "CodeDesc";
                dropRejectReason.DataSource = paymentRejectCode;
                dropRejectReason.DataBind();
                dropRejectReason.Items.Insert(0, new ListItem(" ", null));
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(ex.Message);
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
        /// <summary>
        /// Get selected InvoiceCase list
        /// </summary>
        /// <returns></returns>
        string GetSelectedRows(InvoiceCaseUpdateFlag updateFlag)
        {
            string result ="";
            InvoiceSetDTO invoiceSet=(InvoiceSetDTO) Session["invoiceSet"];
            if(invoiceSet==null)
                return null;
            foreach(GridViewRow row in grvViewEditInvoice.Rows)
            {
                CheckBox chkSelected = (CheckBox)row.FindControl("chkSelected");
                if (chkSelected != null)
                    if (chkSelected.Checked == true)
                    {
                        result += invoiceSet.InvoiceCases[row.DataItemIndex].InvoiceCaseId.ToString() + ",";
                        //if Reject then set invoiceCase payment to 0
                        if (updateFlag == InvoiceCaseUpdateFlag.Reject)
                            invoiceSet.InvoiceCases[row.DataItemIndex].InvoiceCasePaymentAmount = 0;
                        //if pay then set invoice case payment equal Invoice case bill 
                        else if (updateFlag == InvoiceCaseUpdateFlag.Pay)
                            invoiceSet.InvoiceCases[row.DataItemIndex].InvoiceCasePaymentAmount = invoiceSet.InvoiceCases[row.DataItemIndex].InvoiceCaseBillAmount;
                        else
                            invoiceSet.InvoiceCases[row.DataItemIndex].InvoiceCasePaymentAmount = 0;
                    }
            }
            if (result == "")
                return null;
            result= result.Remove(result.LastIndexOf(","),1);
            return result;
        }
         /// <summary>
         /// Reject Selected Invoice Case
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        protected void btnReject_Click(object sender, EventArgs e)
        {
            HideErrorMessage();
            RejectInvoiceCases();
        }
        /// <summary>
        /// Reject Invoice Cases
        /// </summary>
        
        private void RejectInvoiceCases()
        {
            string invoiceCaseIdCollection = GetSelectedRows(InvoiceCaseUpdateFlag.Reject);
            if (invoiceCaseIdCollection == null)
            {
                lblErrorMessage.Items.Add(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0553));
                return;
            }
            InvoiceSetDTO invoiceSet = (InvoiceSetDTO)Session["invoiceSet"];
            if (invoiceSet == null)
                return;
            invoiceSet.PaymentRejectReason = dropRejectReason.SelectedValue;
            //Update invoice amount
            invoiceSet.Invoice.InvoicePaymentAmount = invoiceSet.TotalPaid;
            invoiceSet.Invoice.InvoiceBillAmount = invoiceSet.InvoiceTotal;
            try
            {
                invoiceSet.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
                InvoiceBL.Instance.UpdateInvoiceCase(invoiceSet, invoiceCaseIdCollection, InvoiceCaseUpdateFlag.Reject);
                LoadInvoiceSet();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.DisplayName);
            }
            ////Refresh Data again
            
        }
        private void HideErrorMessage()
        {
            lblErrorMessage.Items.Clear();
        }
        protected void btnPay_Click(object sender, EventArgs e)
        {
            HideErrorMessage();
            PayInvoiceCases();
        }
        protected void btnUnpay_Click(object sender, EventArgs e)
        {
            HideErrorMessage();
            UnPayInvoiceCases();
        }
        private void ClearErrorMessage()
        {
            lblErrorMessage.Items.Clear();
        }
        private void PayInvoiceCases()
        {
            string invoiceCaseIdCollection = GetSelectedRows(InvoiceCaseUpdateFlag.Pay);
            if (invoiceCaseIdCollection == null)
            {
                lblErrorMessage.Items.Add(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0555));
                return;
            }
            InvoiceSetDTO invoiceSet = (InvoiceSetDTO)Session["invoiceSet"];
            if (invoiceSet == null)
                return;
            try
            {
                invoiceSet.InvoicePaymentId = int.Parse(txtPaymentID.Text);
                if (invoiceSet.InvoicePaymentId < 0)
                    throw (new Exception());
            }
            catch
            {
                lblErrorMessage.Items.Add( ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0558));
                return;
            }
            invoiceSet.Invoice.InvoicePaymentAmount = invoiceSet.TotalPaid;
            invoiceSet.Invoice.InvoiceBillAmount = invoiceSet.InvoiceTotal;
            bool result=false;
            try
            {
                invoiceSet.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
               result= InvoiceBL.Instance.UpdateInvoiceCase(invoiceSet, invoiceCaseIdCollection, InvoiceCaseUpdateFlag.Pay);
               if (result == false)
                   lblErrorMessage.Items.Add(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0558));
               else
                   LoadInvoiceSet();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.DisplayName);
            }
            
        }
        private void UnPayInvoiceCases()
        {
            string invoiceCaseIdCollection = GetSelectedRows(InvoiceCaseUpdateFlag.Unpay);
            if (invoiceCaseIdCollection == null)
            {
                lblErrorMessage.Items.Add(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0559));
                return;
            }
            InvoiceSetDTO invoiceSet = (InvoiceSetDTO)Session["invoiceSet"];
            if (invoiceSet == null)
                return;
            invoiceSet.Invoice.InvoicePaymentAmount = invoiceSet.TotalPaid;
            invoiceSet.Invoice.InvoiceBillAmount = invoiceSet.InvoiceTotal;
            bool result = false;
            try
            {
                invoiceSet.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
                result = InvoiceBL.Instance.UpdateInvoiceCase(invoiceSet, invoiceCaseIdCollection, InvoiceCaseUpdateFlag.Unpay);
                LoadInvoiceSet();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.DisplayName);
            }

        }

        
     
    }
}