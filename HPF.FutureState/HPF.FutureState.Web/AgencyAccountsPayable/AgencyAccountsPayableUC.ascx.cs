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
        int rownum
        {
            get { return (int)ViewState["rownum"]; }
            set { ViewState["rownum"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDllAgency();
                SetDefaultPeriodStartEnd();
               
            }
        }
        protected void BindDllAgency()
        { 
         AgencyDTOCollection agencyCollection= LookupDataBL.Instance.GetAgency();
         ddlAgency.DataSource = agencyCollection;
         ddlAgency.DataTextField = "AgencyName";
         ddlAgency.DataValueField = "AgencyID";
         ddlAgency.DataBind();
        }
        
        protected void BindGrvInvoiceList()
        {
            AgencyPayableSearchCriteriaDTO searchCriteria = new AgencyPayableSearchCriteriaDTO();
            AgencyPayableDTOCollection agency = new AgencyPayableDTOCollection();
            try
            {
                searchCriteria.AgencyId = int.Parse(ddlAgency.SelectedValue);
                searchCriteria.PeriodEndDate = DateTime.Parse(txtPeriodEnd.Text);
                searchCriteria.PeriodStartDate = DateTime.Parse(txtPeriodStart.Text);
                agency = AgencyPayableBL.Instance.SearchAgencyPayable(searchCriteria);
                grvInvoiceList.DataSource = agency;
                grvInvoiceList.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                //ExceptionProcessor.HandleException(ex);
            }
        }
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
            BindGrvInvoiceList();
        }

        protected void btnNewPayable_Click(object sender, EventArgs e)
        {
            string query="?agency="+ddlAgency.SelectedValue;
            Response.Redirect("NewPayableCriteria.aspx"+query);
        }

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
                //ExceptionProcessor.HandleException(ex);
            }
        }

        protected void grvInvoiceList_DataBound(object sender, EventArgs e)
        {

        }
    }
}