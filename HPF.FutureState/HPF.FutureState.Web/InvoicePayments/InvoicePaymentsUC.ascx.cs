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
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Web.Security;

namespace HPF.FutureState.Web.InvoicePayments
{
    public partial class InvoicePaymentsUC : System.Web.UI.UserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //ApplySecurity();
            for (int i = 0; i < grvInvoicePaymentList.Rows.Count; i++)
            {
                grvInvoicePaymentList.Rows[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grvInvoicePaymentList, "Select$" + i));
            }
            if (grvInvoicePaymentList.SelectedIndex != -1)
                btnViewEditPayable.Attributes.Clear();
            if (!IsPostBack)
            {
                BindFundingSourceDropDownList();
                SetDefaultPeriodStartEnd();
                BindGrvInvoicePaymentList(DateTime.Now.AddMonths(-6), DateTime.Now);
            }
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_AGENCY_ACCOUNT_PAYABLE))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR999");
            }
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_AGENCY_ACCOUNT_PAYABLE))
            {
                btnNewPayable.Enabled = false;
                btnViewEditPayable.Enabled = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>

        protected void BindFundingSourceDropDownList()
        {
            FundingSourceDTOCollection fundingSourceCol = LookupDataBL.Instance.GetFundingSource();
            ddlFundingSource.DataSource = fundingSourceCol;
            ddlFundingSource.DataTextField = "FundingSourceName";
            ddlFundingSource.DataValueField = "FundingSourceID";
            ddlFundingSource.DataBind();
        }
        /// <summary>
        /// Bind search data into gridview
        /// </summary>
        protected void BindGrvInvoicePaymentList(DateTime periodStart, DateTime periodEnd)
        {
            
            try
            {
                InvoicePaymentDTOCollection invoicePayment = GetInvoicePaymentInfo(periodStart, periodEnd);
                //bind search data to gridview
               
                if (invoicePayment!= null)
                {
                    grvInvoicePaymentList.DataSource = invoicePayment;
                    ViewState["invoicePayment"] = invoicePayment;
                }
                else
                {
                    grvInvoicePaymentList.SelectedIndex = -1;
                    invoicePayment = null;
                }
                //
                grvInvoicePaymentList.DataBind();
                for (int i = 0; i < grvInvoicePaymentList.Rows.Count; i++)
                {
                    grvInvoicePaymentList.Rows[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grvInvoicePaymentList, "Select$" + i));
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }
        }
        protected InvoicePaymentDTOCollection GetInvoicePaymentInfo(DateTime periodStart, DateTime periodEnd)
        {
            InvoiceSearchCriteriaDTO searchCriteria = new InvoiceSearchCriteriaDTO();
            InvoicePaymentDTOCollection invoicePayment = new InvoicePaymentDTOCollection();
            try
            {
                //get search criteria to AgencyPayableSearchCriteriaDTO
                searchCriteria.FundingSourceId = int.Parse(ddlFundingSource.SelectedValue);
                searchCriteria.PeriodStart = periodStart;
                searchCriteria.PeriodEnd = periodEnd;
                //get search data match that search collection
                invoicePayment = InvoicePaymentBL.Instance.InvoicePaymentSearch(searchCriteria);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }
            return invoicePayment;
        }
        /// <summary>
        /// get default period start:1st\prior month\year
        /// get default period end: lastday\prior month\year
        /// </summary>
        protected void SetDefaultPeriodStartEnd()
        {
            DateTime today = DateTime.Today;
            int priormonth = today.AddMonths(-1).Month;
            int year = today.AddMonths(-1).Year;
            txtPeriodStart.Text = priormonth + "/" + 1 + "/" + year;
            int daysinmonth = DateTime.DaysInMonth(year, priormonth);
            txtPeriodEnd.Text = priormonth + "/" + daysinmonth + "/" + year;
        }

        protected void btnRefreshList_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            DateTime periodStart, periodEnd;
            periodStart = DateTime.Parse(txtPeriodStart.Text);
            periodEnd = DateTime.Parse(txtPeriodEnd.Text);
            BindGrvInvoicePaymentList(periodStart, periodEnd);
        }
       
        protected void grvInvoiceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvoicePaymentDTOCollection invoicePayment = new InvoicePaymentDTOCollection();
            invoicePayment = (InvoicePaymentDTOCollection)ViewState["invoicePayment"];
            if (invoicePayment != null)
            {
                
                Session["invoicePaymentInfo"] = invoicePayment[grvInvoicePaymentList.SelectedIndex];
            }
        }

        protected void btnViewPayable_Click(object sender, EventArgs e)
        {
            if (grvInvoicePaymentList.SelectedIndex != -1)
            {
                btnViewEditPayable.Attributes.Clear();
                Response.Redirect("AppViewEditInvoicePaymentPage.aspx?id="+grvInvoicePaymentList.SelectedValue.ToString());
            }
            else
                btnViewEditPayable.Attributes.Add("onclick", "alert('You have to choose a row in gridview!')");

        }

        protected void btnNewPayable_Click(object sender, EventArgs e)
        {
            Response.Redirect("AppViewEditInvoicePaymentPage.aspx");
        }
    }
}