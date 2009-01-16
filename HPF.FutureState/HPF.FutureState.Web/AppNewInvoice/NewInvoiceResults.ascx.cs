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

namespace HPF.FutureState.Web.AppNewInvoice
{
    public partial class NewInvoiceResults : System.Web.UI.UserControl
    {
        InvoiceDraftDTO invoiceDraft =null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["searchCriteria"] == null)
                    return;
                InvoiceCaseSearchCriteriaDTO searchCriteria = Session["searchCriteria"] as InvoiceCaseSearchCriteriaDTO;
                //Get the Invoice Case Draft
                try
                {
                    invoiceDraft= InvoiceBL.Instance.CreateInvoiceDraft(searchCriteria);
                }
                catch (DataException ex)
                {
                    lblErrorMessage.Text = ex.Message;
                    lblErrorMessage.Visible = true;
                    ExceptionProcessor.HandleException(ex);
                }
                Session["invoiceDraft"] = invoiceDraft;
                InvoiceDraftDataBind();
            }
            else
                if(Session["invoiceDraft"]!=null)
                    invoiceDraft = (InvoiceDraftDTO)Session["invoiceDraft"];
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
            lblTotalAmount.Text = "$" + invoiceDraft.TotalAmount;
            grvNewInvoiceResults.DataSource = invoiceDraft.ForeclosureCaseDrafts;
            grvNewInvoiceResults.DataBind();
            lblErrorMessage.Visible = false;
        }

        protected void grvNewInvoiceResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lblComplete = e.Row.FindControl("lblCompleteDate") as Label;
            if (lblComplete != null)
            {
                string date = (e.Row.DataItem as ForeclosureCaseDraftDTO).CompletedDate.ToShortDateString();
                lblComplete.Text = date;
                if (date == "1/1/0001")
                    lblComplete.Text = "N/A";
            }

        }

        protected void grvNewInvoiceResults_DataBound(object sender, EventArgs e)
        {
            grvNewInvoiceResults.Visible = true;
        }

        protected void btnRemoveMarkedCases_Click(object sender, EventArgs e)
        {
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

        protected void btnGenerateInvoice_Click(object sender, EventArgs e)
        {
            if (invoiceDraft.ForeclosureCaseDrafts==null||invoiceDraft.ForeclosureCaseDrafts.Count == 0)
            {
                lblErrorMessage.Text = "There must be at least one Invoice Item to generate an invoice.";
                lblErrorMessage.Visible = true;
                return;
            }
            try
            {
                invoiceDraft.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
                //insert invoice to the database
                InvoiceBL.Instance.InsertInvoice(invoiceDraft);
                lblErrorMessage.Text = "Insert Invoice successful.";
                lblErrorMessage.Visible = true;
            }
            catch(Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AppNewInvoiceCriteriaPage.aspx");
        }
    }
}