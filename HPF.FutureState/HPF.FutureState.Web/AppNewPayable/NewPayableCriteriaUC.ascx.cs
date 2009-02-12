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
                if (Convert.ToInt16(Request.QueryString["indicator"]) == 1)
                    ChkInclude.Checked = true;
                else ChkInclude.Checked = false;
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
                if (Request.QueryString["agency"] != null)
                {
                    if (Request.QueryString["agency"] == "-1")
                        ddlAgency.SelectedIndex = 0 ;
                    else
                    ddlAgency.SelectedValue = Request.QueryString["agency"].ToString();
                }
                    ddlAgency.DataSource = agencyCollection;
                ddlAgency.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
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
                txtPeriodStart.Text = priormonth + "/" + 1 + "/" + year;
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
                Response.Redirect("NewPayableSelection.aspx" + query);
            }
            catch (DataValidationException ex)
            {
                for (int i = 0; i < ex.ExceptionMessages.Count; i++)
                {
                    lblMessage.Text += ex.ExceptionMessages[i].Message;
                }
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
                
            }
        }
        //get all criterias pass to next page.
        private string GetQueryString()
        {
            AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria = new AgencyPayableSearchCriteriaDTO();
            agencyPayableSearchCriteria.AgencyId = int.Parse(ddlAgency.SelectedValue);
            agencyPayableSearchCriteria.CaseComplete = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), ddlCaseCompleted.SelectedValue.ToString());
            agencyPayableSearchCriteria.PeriodStartDate = Convert.ToDateTime(txtPeriodStart.Text.Trim());
            agencyPayableSearchCriteria.PeriodEndDate = Convert.ToDateTime(txtPeriodEnd.Text.Trim());
            if (ChkInclude.Checked)// return true or false
                agencyPayableSearchCriteria.Indicator = 1;
            else agencyPayableSearchCriteria.Indicator = 0;
            string query = "?agencyid=" + agencyPayableSearchCriteria.AgencyId + "&casecomplete=" + agencyPayableSearchCriteria.CaseComplete
                + "&periodenddate=" + agencyPayableSearchCriteria.PeriodEndDate.ToShortDateString() + "&periodstartdate=" + agencyPayableSearchCriteria.PeriodStartDate.ToShortDateString() 
                + "&indicator=" + agencyPayableSearchCriteria.Indicator;
            return query;
        }

    }
}