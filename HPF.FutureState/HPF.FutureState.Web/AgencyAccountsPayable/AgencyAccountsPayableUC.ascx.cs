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

namespace HPF.FutureState.Web.AgencyAccountsPayable
{
    public partial class AgencyAccountsPayableUC : System.Web.UI.UserControl
    {
        //row number of selected row.
        protected int rownum
        {
            get { return (int)ViewState["rownum"]; }
            set { ViewState["rownum"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAgencyDropDownList();
                SetDefaultPeriodStartEnd();
                BindGrvInvoiceList(DateTime.Now, DateTime.Now.AddMonths(-6));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected void BindAgencyDropDownList()
        { 
         AgencyDTOCollection agencyCollection= LookupDataBL.Instance.GetAgency();
         ddlAgency.DataSource = agencyCollection;
         ddlAgency.DataTextField = "AgencyName";
         ddlAgency.DataValueField = "AgencyID";
         ddlAgency.DataBind();
        }
        /// <summary>
        /// Bind search data into gridview
        /// </summary>
        protected void BindGrvInvoiceList(DateTime periodStart,DateTime periodEnd)
        {
            AgencyPayableSearchCriteriaDTO searchCriteria = new AgencyPayableSearchCriteriaDTO();
            AgencyPayableDTOCollection agency = new AgencyPayableDTOCollection();
            try
            {
                //get search criteria to AgencyPayableSearchCriteriaDTO
                searchCriteria.AgencyId = int.Parse(ddlAgency.SelectedValue);
                searchCriteria.PeriodEndDate = periodEnd;
                searchCriteria.PeriodStartDate = periodStart;
                //get search data match that search collection
                agency = AgencyPayableBL.Instance.SearchAgencyPayable(searchCriteria);
                //bind search data to gridview
                grvInvoiceList.DataSource = agency;
                grvInvoiceList.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }
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
            BindGrvInvoiceList(periodStart,periodEnd);
        }
        /// <summary>
        /// go to New payable criteria.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewPayable_Click(object sender, EventArgs e)
        {
            string query="?agency="+ddlAgency.SelectedValue;
            Response.Redirect("NewPayableCriteria.aspx"+query);
        }
        /// <summary>
        ///click: highlight selected row
        ///dblclick: un-highlight selected row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected  void grvInvoiceList_RowDataBound(object sender, GridViewRowEventArgs e)
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
        /// update statuscode, payment comment.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelPayable_Click(object sender, EventArgs e)
        {
            AgencyPayableSearchCriteriaDTO searchCriteria = new AgencyPayableSearchCriteriaDTO();
            AgencyPayableDTOCollection agency = new AgencyPayableDTOCollection();
            try
            {
                searchCriteria.AgencyId = int.Parse(ddlAgency.SelectedValue);
                searchCriteria.PeriodEndDate = DateTime.Parse(txtPeriodEnd.Text);
                searchCriteria.PeriodStartDate = DateTime.Parse(txtPeriodStart.Text);
                agency = AgencyPayableBL.Instance.SearchAgencyPayable(searchCriteria);
                agency[rownum].StatusCode = "Cancel";
                agency[rownum].PaymentComment = "Agency didn't complete";
                grvInvoiceList.DataSource = agency;
                grvInvoiceList.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }
        }

        protected void grvInvoiceList_DataBound(object sender, EventArgs e)
        {

        }
    }
}