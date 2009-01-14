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
    public partial class NewInvoiceResults : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["searchCriteria"] == null)
                return;
            InvoiceCaseSearchCriteriaDTO searchCriteria = Session["searchCriteria"] as InvoiceCaseSearchCriteriaDTO;
            InvoiceDraftDTO invoiceDraft = new InvoiceDraftDTO();
            invoiceDraft.FundingSourceId = searchCriteria.FundingSourceId;
            invoiceDraft.PeriodEndDate = searchCriteria.PeriodEnd;
            invoiceDraft.PeriodStartDate = searchCriteria.PeriodStart;
            //perform case search
            invoiceDraft.ForeclosureCaseDrafts = InvoiceBL.Instance.InvoiceCaseSearch(searchCriteria);
            InvoiceDraftDataBind(invoiceDraft);
        }
        private void InvoiceDraftDataBind(InvoiceDraftDTO invoiceDraft)
        {
            if (Session["fundingSource"] != null)
                lblFundingSource.Text = Session["fundingSource"].ToString();
            lblPeriodStart.Text = invoiceDraft.PeriodStartDate.ToShortDateString();
            lblPeriodEnd.Text = invoiceDraft.PeriodEndDate.ToShortDateString();
            lblTotalCases.Text = invoiceDraft.TotalCases.ToString();
            lblTotalAmount.Text = "$" + invoiceDraft.TotalAmount;
            grvNewInvoiceResults.DataSource = invoiceDraft.ForeclosureCaseDrafts;
            grvNewInvoiceResults.DataBind();
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
    }
}