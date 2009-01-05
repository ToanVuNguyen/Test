using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace HPF.FutureState.Web
{
    public partial class AppForeclosureCaseDetailPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            tabControl.TabClick += new HPF.FutureState.Web.HPFWebControls.TabControlEventHandler(tabControl_TabClick);
            if (!IsPostBack)
            {
                tabControl.AddTab("caseDetail", "Case Detail");
                tabControl.AddTab("caseLoan", "Case Loan(s)");
                tabControl.AddTab("budget", "Budget(s)");
                tabControl.AddTab("outcome", "Outcome(s)");
                tabControl.AddTab("accounting", "Accounting");
                tabControl.AddTab("activityLog", "Activity Log");
                tabControl.AddTab("caseFollowUp", "Case Follow-Up");
                tabControl.AddTab("audit", "Audit");
            }
        }

        void tabControl_TabClick(object sender, HPF.FutureState.Web.HPFWebControls.TabControlEventArgs e)
        {
            lbl_selected.Text = e.SelectedTabID;
        }
    }
}
