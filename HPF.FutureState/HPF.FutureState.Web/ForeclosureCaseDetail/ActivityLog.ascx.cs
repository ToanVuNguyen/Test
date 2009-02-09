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

using HPF.FutureState.Common;
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.Web.ForeclosureCaseDetail
{
    public partial class ActivityLog : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int fcId = Int32.Parse(Request.QueryString["CaseID"].ToString());
                ApplySecurity();
                grdvActivityLogs.DataSource = ActivityLogBL.Instance.GetActivityLog(fcId);
                grdvActivityLogs.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex);
            }
        }

        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))            
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");            
        }
    }
}