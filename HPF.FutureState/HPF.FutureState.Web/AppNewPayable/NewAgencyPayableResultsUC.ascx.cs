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

namespace HPF.FutureState.Web.AppNewPayable
{
    public partial class NewAgencyPayableResultsUC : System.Web.UI.UserControl
    {

        AgencyPayableDraftDTO agencyPayableDraftDTO = new AgencyPayableDraftDTO();
        //ForeclosureCaseDraftDTOCollection fcDraftCol;
        AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria = new AgencyPayableSearchCriteriaDTO();
        ForeclosureCaseDraftDTOCollection FCDraftCol
        {
            get
            { return (ForeclosureCaseDraftDTOCollection)ViewState["fcDraftCol"]; }
            set
            {
                ViewState["fcDraftCol"] = value;  
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            
            agencyPayableSearchCriteria = GetCriteria();
            if (!IsPostBack)
            {
                DisplayNewAgencyPayableResult(agencyPayableSearchCriteria);
            }
           

        }
        protected AgencyPayableSearchCriteriaDTO GetCriteria()
        {
            AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria = new AgencyPayableSearchCriteriaDTO();
            int agencyid = int.Parse(Request.QueryString["agencyid"].ToString());
            string casecomplete = Request.QueryString["casecomplete"].ToString();

            DateTime periodenddate = Convert.ToDateTime(Request.QueryString["periodenddate"].ToString());

            string servicerconsent = Request.QueryString["servicerconsent"].ToString();

            DateTime periodstartdate = Convert.ToDateTime(Request.QueryString["periodstartdate"].ToString());

            string fundingconsent = Request.QueryString["fundingconsent"].ToString();
            int maxnumbercase = int.Parse(Request.QueryString["maxnumbercase"].ToString());
            string indicator = Request.QueryString["indicator"].ToString();

            agencyPayableSearchCriteria.AgencyId = agencyid;
            if (casecomplete == "Y")
                agencyPayableSearchCriteria.CaseComplete = CustomBoolean.Y;
            if (casecomplete == "N")
                agencyPayableSearchCriteria.CaseComplete = CustomBoolean.N;
            else agencyPayableSearchCriteria.CaseComplete = CustomBoolean.None;

            agencyPayableSearchCriteria.PeriodStartDate = periodstartdate;
            if (servicerconsent == "Y")
                agencyPayableSearchCriteria.ServicerConsent = CustomBoolean.Y;
            if (servicerconsent == "N")
                agencyPayableSearchCriteria.ServicerConsent = CustomBoolean.N;
            else
                agencyPayableSearchCriteria.ServicerConsent = CustomBoolean.None;
            agencyPayableSearchCriteria.PeriodEndDate = periodenddate;
            if (fundingconsent == "Y")
                agencyPayableSearchCriteria.FundingConsent = CustomBoolean.Y;
            if (fundingconsent == "Y")
                agencyPayableSearchCriteria.FundingConsent = CustomBoolean.N;
            else
                agencyPayableSearchCriteria.FundingConsent = CustomBoolean.None;

            agencyPayableSearchCriteria.MaxNumberOfCase = maxnumbercase;
            agencyPayableSearchCriteria.LoanIndicator = indicator;
            return agencyPayableSearchCriteria;
        }
        protected void BindGridView()
        {
            grvInvoiceItems.DataSource = this.FCDraftCol;
            grvInvoiceItems.DataBind();
            decimal total = 0;
            foreach (var item in this.FCDraftCol)
            {
                total = +item.Amount;
            }
            lblInvoiceTotalFooter.Text = total.ToString();
            lblTotalCasesFooter.Text = this.FCDraftCol.Count.ToString();

        }
        protected void DisplayNewAgencyPayableResult(AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria)
        {
            try
            {
                agencyPayableDraftDTO = AgencyPayableBL.Instance.CreateDraftAgencyPayable(agencyPayableSearchCriteria);
                grvInvoiceItems.DataSource = agencyPayableDraftDTO.ForclosureCaseDrafts;
                this.FCDraftCol = agencyPayableDraftDTO.ForclosureCaseDrafts;
                grvInvoiceItems.DataBind();

                lblAgency.Text = agencyPayableSearchCriteria.AgencyId.ToString();
                lblPeriodStart.Text = agencyPayableSearchCriteria.PeriodStartDate.ToShortDateString();
                lblPeriodEnd.Text = agencyPayableSearchCriteria.PeriodEndDate.ToShortDateString();
                lblTotalAmount.Text = agencyPayableDraftDTO.TotalAmount.ToString();
                lblTotalCases.Text = agencyPayableDraftDTO.TotalCases.ToString();
                lblTotalCasesFooter.Text = agencyPayableDraftDTO.ForclosureCaseDrafts.Count.ToString();
                decimal total = 0;
                foreach (var item in agencyPayableDraftDTO.ForclosureCaseDrafts)
                {
                    total = +item.Amount;
                }
                lblInvoiceTotalFooter.Text = total.ToString();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void chkHeaderCaseIDCheck(object sender, EventArgs e)
        {
            CheckBox chkdelall = (CheckBox)grvInvoiceItems.HeaderRow.FindControl("chkHeaderCaseID");
            foreach (GridViewRow row in grvInvoiceItems.Rows)
            {
                CheckBox chkdel = (CheckBox)row.FindControl("chkCaseID");
                chkdel.Checked = chkdelall.Checked;
            }
        }
        protected void btnGeneratePayable_Click(object sender, EventArgs e)
        {
            try
            {
                agencyPayableSearchCriteria = GetCriteria();
                agencyPayableDraftDTO = AgencyPayableBL.Instance.CreateDraftAgencyPayable(agencyPayableSearchCriteria);
                agencyPayableDraftDTO.ForclosureCaseDrafts = this.FCDraftCol;
                AgencyPayableBL.Instance.InsertAgencyPayable(agencyPayableDraftDTO);
                lblMessage.Text = "Generate succesfull.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void btnRemoveMarkedCases_Click(object sender, EventArgs e)
        {
            int rownum;
            foreach (GridViewRow row in grvInvoiceItems.Rows)
            {
                CheckBox chkdel = (CheckBox)row.FindControl("chkCaseID");
                if (chkdel.Checked == true)
                {
                    rownum = row.RowIndex;
                    ForeclosureCaseDraftDTO agency = this.FCDraftCol[rownum];
                    this.FCDraftCol.Remove(agency);
                }
            }
            BindGridView();

        }

        protected void btnCancelPayable_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewPayableCriteria.aspx");
        }


    }
}