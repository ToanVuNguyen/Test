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
                BindDataToDDLB();
            }
        }
        private void BindDataToDDLB()
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
            try
            {
                InvoiceSearchResultDTOCollection searchResult = InvoiceBL.Instance.SearchInvoice(searchCriteria);
                grvFundingSourceInvoices.DataSource = searchResult;
                grvFundingSourceInvoices.DataBind();
            }
            catch (DataValidationException ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblErrorMessage.Visible = true;
            }
            catch (DataException ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblErrorMessage.Visible = true;
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

        protected void grvFundingSourceInvoices_DataBound(object sender, EventArgs e)
        {
            panForeClosureCaseSearch.Visible = true;
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
    }
}