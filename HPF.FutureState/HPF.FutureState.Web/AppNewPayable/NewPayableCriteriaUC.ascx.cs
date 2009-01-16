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
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;


namespace HPF.FutureState.Web.AppNewPayable
{
    public partial class NewPayableCriteriaUC : System.Web.UI.UserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDDLAgency();
                GetDefaultPeriodStartEnd();
            }
        }
        protected void BindDDLAgency()
        {
            try
            {
                AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgency();
                AgencyDTO item = agencyCollection[0];
                agencyCollection.Remove(item);
                ddlAgency.DataTextField = "AgencyName";
                ddlAgency.DataValueField = "AgencyID";
                ddlAgency.DataSource = agencyCollection;
                if (Request.QueryString["agency"].ToString() != "-1")
                    ddlAgency.SelectedValue = Request.QueryString["agency"].ToString();
                ddlAgency.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }
        }
        /// <summary>
        /// get default periodstart:1st/priormonth/year
        /// get default periodend:last day/priormonth/year.
        /// </summary>
        protected void GetDefaultPeriodStartEnd()
        {
            DateTime today=DateTime.Today;
            int priormonth=today.AddMonths(-1).Month;
            int year = today.AddMonths(-1).Year;
            txtPeriodStart.Text = priormonth + "/" + 1 + "/" + year;
            int daysinmonth = DateTime.DaysInMonth(year, priormonth);
            txtPeriodEnd.Text = priormonth + "/" + daysinmonth + "/" + year;

        }
        /// <summary>
        /// create draftNewPayable data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDraftNewPayable_Click(object sender, EventArgs e)
        {
            try
            {
                string query = GetQueryString();
                Response.Redirect("NewAgencyPayableResults.aspx" + query);
            }
            catch (DataValidationException ex)
            {
                for (int i = 0; i < ex.ExceptionMessages.Count; i++)
                {
                    lblMessage.Text += ex.ExceptionMessages[i].Message;
                }
                ExceptionProcessor.HandleException(ex);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
                
            }
        }
        //get all criterias pass to next page.
        private string GetQueryString()
        {
            AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria = new AgencyPayableSearchCriteriaDTO();
            agencyPayableSearchCriteria.AgencyId = int.Parse(ddlAgency.SelectedValue);
            agencyPayableSearchCriteria.CaseComplete = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), ddlCaseCompleted.SelectedValue.ToString());
            agencyPayableSearchCriteria.PeriodStartDate = Convert.ToDateTime(txtPeriodStart.Text.Trim());
            agencyPayableSearchCriteria.ServicerConsent = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), ddlServicerConsent.SelectedValue.ToString());
            agencyPayableSearchCriteria.PeriodEndDate = Convert.ToDateTime(txtPeriodEnd.Text.Trim());
            agencyPayableSearchCriteria.FundingConsent = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), ddlFundingConsent.SelectedValue.ToString());
            if (txtMaxNumberCase.Text != string.Empty)
                agencyPayableSearchCriteria.MaxNumberOfCase = int.Parse(txtMaxNumberCase.Text.Trim());
            else agencyPayableSearchCriteria.MaxNumberOfCase = 500;
            agencyPayableSearchCriteria.LoanIndicator = ddlIndicator.SelectedValue;
            string query = "?agencyid=" + agencyPayableSearchCriteria.AgencyId + "&casecomplete=" + agencyPayableSearchCriteria.CaseComplete
                + "&periodenddate=" + agencyPayableSearchCriteria.PeriodEndDate + "&servicerconsent=" + agencyPayableSearchCriteria.ServicerConsent
                + "&periodstartdate=" + agencyPayableSearchCriteria.PeriodStartDate + "&fundingconsent=" + agencyPayableSearchCriteria.FundingConsent
                + "&maxnumbercase=" + agencyPayableSearchCriteria.MaxNumberOfCase + "&indicator=" + agencyPayableSearchCriteria.LoanIndicator;
            return query;
        }
       

    }
}