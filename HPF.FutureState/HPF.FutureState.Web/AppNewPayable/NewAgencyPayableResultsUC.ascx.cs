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
        AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria = new AgencyPayableSearchCriteriaDTO();
        protected void Page_Load(object sender, EventArgs e)
        {
            int agencyid=int.Parse(Request.QueryString["agencyid"].ToString());
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
            if(fundingconsent=="Y")
            agencyPayableSearchCriteria.FundingConsent = CustomBoolean.Y;
            if (fundingconsent == "Y")
                agencyPayableSearchCriteria.FundingConsent = CustomBoolean.N;
            else
                agencyPayableSearchCriteria.FundingConsent = CustomBoolean.None;

            agencyPayableSearchCriteria.MaxNumberOfCase = maxnumbercase;
            agencyPayableSearchCriteria.LoanIndicator = indicator;
            DisplayNewAgencyPayableResult(agencyPayableSearchCriteria);
        }
        protected void DisplayNewAgencyPayableResult(AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria)
        {
            AgencyPayableDraftDTO agencyPayableDraftDTO = new AgencyPayableDraftDTO();
            try
            {
                agencyPayableDraftDTO = AgencyPayableBL.Instance.CreateDraftAgencyPayable(agencyPayableSearchCriteria);
                grvInvoiceItems.DataSource = agencyPayableDraftDTO.ForclosureCaseDrafts;
                grvInvoiceItems.DataBind();
                lblAgency.Text = agencyPayableSearchCriteria.AgencyId.ToString();
                lblPeriodStart.Text = agencyPayableSearchCriteria.PeriodStartDate.ToShortDateString();
                lblPeriodEnd.Text = agencyPayableSearchCriteria.PeriodEndDate.ToShortDateString();
                lblTotalAmount.Text = agencyPayableDraftDTO.TotalAmount.ToString();
                lblTotalCases.Text = agencyPayableDraftDTO.TotalCases.ToString();
                
            }
            catch(Exception ex)
            {
            
            }
        }

        protected void btnGeneratePayable_Click(object sender, EventArgs e)
        {

        }
    }
}