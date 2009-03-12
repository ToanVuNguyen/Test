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
using HPF.FutureState.Common.Utils;
using System.Text;

namespace HPF.FutureState.Web.AppNewInvoice
{
    public partial class NewInvoiceResults : System.Web.UI.UserControl
    {
        InvoiceDraftDTO invoiceDraft =null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ApplySecurity();
                ClearErrorMessages();
                if (!IsPostBack)
                {
                    if (Session["IvoiceCaseSearchCriteria"] == null)
                        Response.Redirect("CreateNewInvoice.aspx");
                    InvoiceCaseSearchCriteriaDTO searchCriteria = Session["IvoiceCaseSearchCriteria"] as InvoiceCaseSearchCriteriaDTO;
                    //Get the Invoice Case Draft
                    try
                    {
                        invoiceDraft = InvoiceBL.Instance.CreateInvoiceDraft(searchCriteria);
                        Session["invoiceDraft"] = invoiceDraft;
                        InvoiceDraftDataBind();
                    }
                    catch (DataException ex)
                    {
                        lblErrorMessage.Items.Add(new ListItem(ex.Message));
                        ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
                    }
                }
                else
                    if (Session["invoiceDraft"] != null)
                        invoiceDraft = (InvoiceDraftDTO)Session["invoiceDraft"];
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
                Response.Redirect("ErrorPage.aspx?CODE=ERR999");
            }
        }
        protected void chkHeaderCaseIDCheck(object sender, EventArgs e)
        {
            CheckBox headerCheckbox = (CheckBox)sender;
            foreach (GridViewRow row in grvNewInvoiceResults.Rows)
            {
                CheckBox chkSelected = (CheckBox)row.FindControl("chkCaseSelected");
                if (chkSelected != null)
                    chkSelected.Checked = headerCheckbox.Checked;
            }
        }
        private void InvoiceDraftDataBind()
        {
            if (Session["fundingSource"] != null)
                lblFundingSource.Text = Session["fundingSource"].ToString();
            lblPeriodStart.Text = invoiceDraft.PeriodStartDate.ToShortDateString();
            lblPeriodEnd.Text = invoiceDraft.PeriodEndDate.ToShortDateString();
            lblTotalCases.Text = invoiceDraft.TotalCases.ToString();
            lblTotalAmount.Text = invoiceDraft.TotalAmount.ToString("C");
            grvNewInvoiceResults.DataSource = invoiceDraft.ForeclosureCaseDrafts;
            grvNewInvoiceResults.DataBind();
            if (invoiceDraft.ForeclosureCaseDrafts == null)
            {
                btnGenerateInvoice.Enabled = false;
                lblErrorMessage.Items.Add(new ListItem(ErrorMessages.GetExceptionMessage(ErrorMessages.ERR0987)));
            }
        }

        protected void grvNewInvoiceResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lblComplete = e.Row.FindControl("lblCompleteDate") as Label;
            if (lblComplete != null)
            {
                string date = (e.Row.DataItem as ForeclosureCaseDraftDTO).CompletedDate == null ? "" : (e.Row.DataItem as ForeclosureCaseDraftDTO).CompletedDate.Value.ToShortDateString();
                lblComplete.Text = date;
            }

        }

        protected void grvNewInvoiceResults_DataBound(object sender, EventArgs e)
        {
            grvNewInvoiceResults.Visible = true;
        }

        protected void btnRemoveMarkedCases_Click(object sender, EventArgs e)
        {
            ClearErrorMessages();
            if (invoiceDraft == null)
                return;
            for (int i = grvNewInvoiceResults.Rows.Count - 1; i >= 0; i--)
            {
                if (grvNewInvoiceResults.Rows[i] is GridViewRow)
                {
                    GridViewRow row = grvNewInvoiceResults.Rows[i];
                    CheckBox chkSelected = (CheckBox)row.FindControl("chkCaseSelected");
                    if (chkSelected != null)
                        if (chkSelected.Checked == true)
                        {
                            //remove from the grid and remove from the collection 
                            invoiceDraft.ForeclosureCaseDrafts.RemoveAt(row.RowIndex);
                        }
                }
            }
            Session["invoiceDraft"] = invoiceDraft;
            InvoiceDraftDataBind();
        }
        private void ClearErrorMessages()
        {
            lblErrorMessage.Items.Clear();
        }
        protected void btnGenerateInvoice_Click(object sender, EventArgs e)
        {
            ClearErrorMessages();
            SaveAndSendInvoiceReport();
        }

        private void SaveAndSendInvoiceReport()
        {

            FundingSourceDTO fundingSource = null;
            InvoiceDTO invoice = null;
            byte[] excelFile1 = null;
            byte[] excelFile2 = null;
            byte[] pdfFile = null;
            try
            {
                //Insert Invoice to Database
                invoice = InsertInvoice();
                fundingSource = GetFundingSource(invoice.FundingSourceId.Value);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                btnGenerateInvoice.Enabled = false;
                return;
            }
            try
            {
                //Generate Report
                pdfFile = GeneratePDFReport(invoice.InvoiceId);
                excelFile1 = GenerateExcelReport(invoice, fundingSource.ExportFormatCd,false);
                //If ExportFormatCd = FIS so we will get the detail 
                if (fundingSource.ExportFormatCd == Constant.REF_CODE_SET_BILLING_EXPORT_FIS_CODE)
                    excelFile2 = GenerateExcelReport(invoice, fundingSource.ExportFormatCd, true);

            }
            catch (Exception ex)
            {
                //Insert Successful
                string exMes = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR0985);
                lblErrorMessage.Items.Add(exMes);
                //Generate file fail.
                exMes = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR0984)+ex.Message;
                lblErrorMessage.Items.Add(exMes);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                btnGenerateInvoice.Enabled = false;
                return;
            }
            try
            {
                //Upload Report
                UploadPDFReport(invoice,pdfFile,fundingSource);
                UploadExcelReport(invoice, excelFile1, fundingSource);
                if (fundingSource.ExportFormatCd == Constant.REF_CODE_SET_BILLING_EXPORT_FIS_CODE)
                    UploadExcelReport(invoice, excelFile2, fundingSource);
            }
            catch (Exception ex)
            {
                //Insert Successful
                string exMes = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR0985);
                lblErrorMessage.Items.Add(exMes);
                //Generate file successful.
                exMes = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR0983);
                lblErrorMessage.Items.Add(exMes);
                //Deliver fail.
                exMes = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR0982);
                lblErrorMessage.Items.Add(exMes);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                btnGenerateInvoice.Enabled = false;
                return;
            }
            //if all successful , redirect to fudning source invoice page
            Response.Redirect("FundingSourceInvoice.aspx");
            
        }
        #region InsertInvoice/CreateReport/SendReport
        private void UploadExcelReport(InvoiceDTO invoice, byte[] excelFile,FundingSourceDTO fundingSource)
        {
            string fileName = GetExcelFileName(fundingSource, invoice);
            HPFPortalInvoice portalInvoice = GetPortalInvoice(invoice, excelFile, fundingSource, fileName);
            HPFPortalGateway.SendInvoiceReportFile(portalInvoice);
        }
        private void UploadPDFReport(InvoiceDTO invoice, byte[] pdfFile, FundingSourceDTO fundingSource)
        {
            string fileName = GetPDFFileName(fundingSource, invoice);
            HPFPortalInvoice portalInvoice = GetPortalInvoice(invoice, pdfFile, fundingSource, fileName);
            HPFPortalGateway.SendInvoiceReportFile(portalInvoice);
        }
        /// <summary>
        /// Call REport Bl
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="fundingSourceFormatCode"></param>
        /// <param name="getFISDetail"></param>
        /// <returns></returns>
        private byte[] GenerateExcelReport(InvoiceDTO invoice, string fundingSourceFormatCode, bool getFISDetail)
        {
            return ReportBL.Instance.InvoiceExcelReport(invoice.InvoiceId.Value,fundingSourceFormatCode,getFISDetail,HPFWebSecurity.CurrentIdentity.LoginName);
        }
        /// <summary>
        /// Call REport Bl
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="fundingSourceFormatCode"></param>
        /// <param name="getFISDetail"></param>
        /// <returns></returns>
        private byte[] GeneratePDFReport(int? invoiceId)
        {
            return ReportBL.Instance.InvoicePDFReport(invoiceId.Value);
        }

        private InvoiceDTO InsertInvoice()
        {
                //insert invoice to the database
            invoiceDraft.InvoiceComment = txtComment.Text;
            invoiceDraft.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
            return InvoiceBL.Instance.InsertInvoice(invoiceDraft);
        }
        #endregion
        #region GetMethods
        string GetExcelFileName(FundingSourceDTO fundingSource, InvoiceDTO invoice)
        {
            StringBuilder result = new StringBuilder();
            result.Append(fundingSource.FundingSourceAbbrev);
            result.Append("_");
            result.Append(string.Format("{0:yyyymmdd}", invoice.PeriodStartDate.Value));
            result.Append("_");
            result.Append(string.Format("{0:yyyymmdd}", invoice.PeriodEndDate.Value));
            //result.Append("_HPF_INV#");
            //todo: invalid charater when uploading to SharePoint (#)
            result.Append("_HPF_INV-");
            result.Append(invoice.InvoiceId.ToString());
            result.Append("_DETAIL.xls");
            return result.ToString();
        }
        string GetPDFFileName(FundingSourceDTO fundingSource, InvoiceDTO invoice)
        {
            StringBuilder result = new StringBuilder();
            result.Append(fundingSource.FundingSourceAbbrev);
            result.Append("_");
            result.Append(string.Format("{0:yyyymmdd}", invoice.PeriodStartDate.Value));
            result.Append("_");
            result.Append(string.Format("{0:yyyymmdd}", invoice.PeriodEndDate.Value));
            //result.Append("_HPF_INV#");
            //todo: invalid charater when uploading to SharePoint (#)
            result.Append("_HPF_INV-");
            result.Append(invoice.InvoiceId.ToString());
            result.Append("_DETAIL.pdf");
            return result.ToString();
        }
        FundingSourceDTO GetFundingSource(int fundingSourceId)
        {
            var fundingSourceCollection= LookupDataBL.Instance.GetFundingSource();
            foreach (var i in fundingSourceCollection)
                if (i.FundingSourceID == fundingSourceId)
                    return i;
            return null;
        }
        HPFPortalInvoice GetPortalInvoice(InvoiceDTO invoice,byte[] excelFile,FundingSourceDTO fundingSource,string fileName)
        {
            HPFPortalInvoice result = new   HPFPortalInvoice 
                                            {
                                                File = excelFile,
                                                FundingSource = fundingSource.FundingSourceName,
                                                InvoiceDate = invoice.InvoiceDate,
                                                InvoiceFolderName =  fundingSource.SharePointFolder,
                                                InvoiceNumber = invoice.InvoiceId.ToString(),
                                                Month = string.Format("{0:MMM}",invoice.InvoiceDate.Value),
                                                Year = invoice.InvoiceDate.Value.Year,
                                                FileName = fileName
                                            };
            return result;
        }
        #endregion
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateNewInvoice.aspx");
        }
    }
}