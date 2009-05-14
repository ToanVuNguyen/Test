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
using System.Text;
using System.Globalization;


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
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
        }
        /// <summary>
        protected void CancelDisplayCriteria()
        {
            if (Request.QueryString["periodenddate"] != null)
            {
                DateTime periodend=DateTime.Parse(Request.QueryString["periodenddate"]);
                txtPeriodEnd.Text = periodend.ToShortDateString();
                //Sinh 
                
                DateTime periodstart = DateTime.Parse(Request.QueryString["periodstartdate"]);
                txtPeriodStart.Text = periodstart.ToShortDateString();
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
                AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgencies();

                ddlAgency.DataTextField = "AgencyName";
                ddlAgency.DataValueField = "AgencyID";
                ddlAgency.DataSource = agencyCollection;
                ddlAgency.DataBind();
                ddlAgency.Items.RemoveAt(ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue("-1")));
                if (Request.QueryString["agency"] != null)
                {
                    if (Request.QueryString["agency"] == "-1")
                    {
                        ddlAgency.Items.Insert(0, new ListItem("", ""));
                        ddlAgency.SelectedIndex = 0;
                    }
                    else
                        ddlAgency.SelectedValue = Request.QueryString["agency"].ToString();
                }
            }
            catch (Exception ex)
            {
                bulMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        /// <summary>
        /// get default periodstart:1st/priormonth/year
        /// get default periodend:last day/priormonth/year.
        /// </summary>
        protected void GetDefaultPeriodStartEnd()
        {
            if (Request.QueryString["periodenddate"] == null)
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
            bulMessage.Items.Clear();
            bool error = true;
            string query = "";

            try
            {
                AgencyPayableSearchCriteriaDTO agencyPayableCriteria = BuildSearchAgencyPayableCriteria();
                AgencyPayableBL.Instance.CheckSeachAgencyPayable(agencyPayableCriteria);
                query = BuildQueryString(agencyPayableCriteria);
                error = false;
            }
            catch (DataValidationException ex)
            {
                foreach (var mes in ex.ExceptionMessages)
                {
                    bulMessage.Items.Add(new ListItem(mes.Message));
                }
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);                
            }
            catch (Exception ex)
            {                
                bulMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
            if(!error)
                Response.Redirect("NewPayableSelection.aspx" + query);
        }
        private string BuildQueryString(AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria)
        {
            StringBuilder query = new StringBuilder();
            query.Append("?agencyid=");
            query.Append(agencyPayableSearchCriteria.AgencyId);
            query.Append("&casecomplete=");
            query.Append(agencyPayableSearchCriteria.CaseComplete);
            query.Append("&periodenddate=");
            query.Append(agencyPayableSearchCriteria.PeriodEndDate);
            query.Append("&periodstartdate=");
            query.Append(agencyPayableSearchCriteria.PeriodStartDate);
            query.Append("&indicator=");
            query.Append(agencyPayableSearchCriteria.Indicator);
            return query.ToString();
        }
        private DateTime ConvertToDateTime(string obj)
        {
            DateTime dt;
            //CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            //DateTimeStyles styles = DateTimeStyles.None;
            if (DateTime.TryParse(obj, out dt))
                return dt;
            return dt;
        }
        private AgencyPayableSearchCriteriaDTO BuildSearchAgencyPayableCriteria()
        {
            AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria = new AgencyPayableSearchCriteriaDTO();
            int agencyid;
            if (Int32.TryParse(ddlAgency.SelectedValue, out agencyid))
                agencyPayableSearchCriteria.AgencyId = agencyid;
            else agencyPayableSearchCriteria.AgencyId = null;
            
            agencyPayableSearchCriteria.PeriodStartDate = ConvertToDateTime(txtPeriodStart.Text.Trim());            
            agencyPayableSearchCriteria.PeriodEndDate = ConvertToDateTime(txtPeriodEnd.Text.Trim());
            
            agencyPayableSearchCriteria.CaseComplete = ddlCaseCompleted.SelectedValue.ToString();
            
            if (ChkInclude.Checked)// return true or false
                agencyPayableSearchCriteria.Indicator = 1;
            else agencyPayableSearchCriteria.Indicator = 0;
            return agencyPayableSearchCriteria;
        }   
    }
}