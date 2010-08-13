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
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Web.Security;
using System.Text;

namespace HPF.FutureState.Web.ApphQCSelectionCaseSearch
{
    public partial class AppQCSelectionCaseSearchUC : System.Web.UI.UserControl
    {
        private const int MONTHS = 24;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMonthYearDropDownList();
                BindAgencyDropDownList();
            }
        }
        /// <summary>
        ///Display data in dropdownlist 
        /// </summary>
        protected void BindAgencyDropDownList()
        {
            try
            {
                AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgencies();
                ddlAgency.DataValueField = "AgencyID";
                ddlAgency.DataTextField = "AgencyName";
                ddlAgency.DataSource = agencyCollection;
                ddlAgency.DataBind();
                ddlAgency.Items.RemoveAt(ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue("-1")));
                ddlAgency.Items.Insert(0, new ListItem("All Agencies", "-1"));
                ddlAgency.Items.FindByText("All Agencies").Selected = true;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        /// <summary>
        /// Display MonthYear dropdownlist for From and To
        /// </summary>
        private void BindMonthYearDropDownList()
        {
            DateTime dt;
            StringBuilder text;
            StringBuilder value;
            for (int i = 0; i < MONTHS; i++)
            {
                text = new StringBuilder();
                value = new StringBuilder();
                dt = DateTime.Now.AddMonths(0-i);
                text.AppendFormat("{0}-{1}",dt.ToString("MM"),dt.ToString("yyyy"));
                value.AppendFormat("{0}{1}",dt.ToString("yyyy"),dt.ToString("MM"));
                ddlFromYearMonth.Items.Add(new ListItem(text.ToString(),value.ToString()));
                ddlToYearMonth.Items.Add(new ListItem(text.ToString(), value.ToString()));
            }
        }
    }
}