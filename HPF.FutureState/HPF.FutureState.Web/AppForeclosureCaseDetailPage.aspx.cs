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
        string UCLOCATION = "ForeclosureCaseDetail\\";
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
                tabControl.SelectedTab = "caseDetail";
                UserControlLoader.LoadUserControl(UCLOCATION + "CaseDetail.ascx", "ucCaseDetail");
            }
        }

        void tabControl_TabClick(object sender, HPF.FutureState.Web.HPFWebControls.TabControlEventArgs e)
        {
            switch (e.SelectedTabID)
            { 
                case "caseDetail":
                    UserControlLoader.LoadUserControl(UCLOCATION+"CaseDetail.ascx", "ucCaseDetail");
                    break;
                case "caseLoan":
                    UserControlLoader.LoadUserControl(UCLOCATION+"CaseLoan.ascx", "ucCaseLoan");
                    break;
                case "budget":
                    UserControlLoader.LoadUserControl(UCLOCATION + "Budget.ascx", "ucBudget");
                    break;
                case "outcome":
                    UserControlLoader.LoadUserControl(UCLOCATION + "Outcome.ascx", "ucOutcome");
                    break;
                case "accounting":
                    UserControlLoader.LoadUserControl(UCLOCATION + "Accounting.ascx", "ucAccounting");
                    break;
                case "activityLog":
                    UserControlLoader.LoadUserControl(UCLOCATION + "ActivityLog.ascx", "ucActivityLog");
                    break;
                case "caseFollowUp":
                    UserControlLoader.LoadUserControl(UCLOCATION + "CaseFollowUp.ascx", "ucCaseFollowUp");
                    break;
                case "audit":
                    UserControlLoader.LoadUserControl(UCLOCATION + "Audit.ascx", "ucAudit");
                    break;

            }
            
        }
    }
}
