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


namespace HPF.FutureState.Web.AppFundingSourceInvoices
{
    public partial class AppFundingSourceInvoices : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetFundingSourceList();
                SetUpDefaultValue();    
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
                InvoiceSearchResultDTOCollection searchResult = InvoiceBL.Instance.InvoiceSearch(searchCriteria);
                grvFundingSourceInvoices.DataSource = searchResult;
                grvFundingSourceInvoices.DataBind();
            }
            catch (DataValidationException ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblErrorMessage.Visible = true;
                ExceptionProcessor.HandleException(ex);
            }
            catch (DataException ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblErrorMessage.Visible = true;
                ExceptionProcessor.HandleException(ex);
            }
        }

        protected void grvFundingSourceInvoices_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick", "this.className='SelectedRowStyle'");
                if (e.Row.RowState == DataControlRowState.Alternate)
                {
                    e.Row.Attributes.Add("ondblclick", "this.className='AlternatingRowStyle'");
                }
                else
                {
                    e.Row.Attributes.Add("ondblclick", "this.className='RowStyle'");
                }

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
                string date = (e.Row.DataItem as InvoiceSearchResultDTO).InvoiceDate.ToShortDateString();
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
    }
}