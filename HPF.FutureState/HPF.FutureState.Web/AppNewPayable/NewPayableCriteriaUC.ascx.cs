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
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common;


namespace HPF.FutureState.Web.AppNewPayable
{
    public partial class NewPayableCriteriaUC : System.Web.UI.UserControl
    {

        
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplySecurity();
            if (!IsPostBack)
            {
                
                BindDDLAgency();
                GetDefaultPeriodStartEnd();
                CancelDisplayCriteria();
            }
           
        }
        /// <summary>
        /// 
        /// </summary>
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_AGENCY_ACCOUNT_PAYABLE))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR999");
            }
        }
        /// <summary>
        protected void CancelDisplayCriteria()
        {
            if (Request.QueryString["periodenddate"]!=null)
            {
                txtPeriodEnd.Text = Request.QueryString["periodenddate"].ToString();
                txtPeriodStart.Text = Request.QueryString["periodstartdate"].ToString();
                ddlCaseCompleted.SelectedValue = Request.QueryString["casecomplete"].ToString();
                ddlServicerConsent.SelectedValue = Request.QueryString["servicerconsent"].ToString();
                ddlFundingConsent.SelectedValue = Request.QueryString["fundingconsent"].ToString();
                txtMaxNumberCase.Text = Request.QueryString["maxnumbercase"].ToString();
                ddlIndicator.SelectedValue = Request.QueryString["indicator"].ToString();
                ddlAgency.SelectedValue = Request.QueryString["agencyid"].ToString();
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
            if (Request.QueryString["periodenddate"]==null)
            {
                DateTime today = DateTime.Today;
                int priormonth = today.AddMonths(-1).Month;
                int year = today.AddMonths(-1).Year;
                txtPeriodStart.Text = today.AddMonths(-1).ToShortDateString();
                int daysinmonth = DateTime.DaysInMonth(year, priormonth);
                txtPeriodEnd.Text = priormonth + "/" + daysinmonth + "/" + year;
            }

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