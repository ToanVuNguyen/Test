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
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Web.HPFWebControls;



namespace HPF.FutureState.Web
{
    public partial class Default : System.Web.UI.Page
    {       
        protected void Page_Load(object sender, EventArgs e)
        {

            TabControl1.AddTab("caseDetail","Case Detail");
            TabControl1.AddTab("caseLoan","Case Loan(s)");
            TabControl1.AddTab("budget","Budget(s)");
            TabControl1.AddTab("outcome","Outcome(s)");
            TabControl1.AddTab("accounting","Accounting");
            TabControl1.AddTab("activityLog","Activity Log");
            TabControl1.AddTab("caseFollowUp","Case Follow-Up");
            TabControl1.AddTab( "audit","Audit");
            TabControl1.TabClick += new TabControlEventHandler(TabControl1_TabClick);
            if (!IsPostBack)
            {
                //UserControlLoader1.UserControlVirtualPath="AppForeclosureCaseSearch//AppForeClosureCaseSearch.ascx";
                //UserControlLoader1.UserControlID="abc";
                
            }

            //if(!IsPostBack)
            //    UserControlLoader1.LoadUC("AppForeclosureCaseSearch//AppForeClosureCaseSearch.ascx", "abc");            

        }

        void TabControl1_TabClick(object sender, TabControlEventArgs e)
        {
            txt_Tab.Text = e.SelectedTabID+ " selected";
        }

        

        protected void Button1_Click1(object sender, EventArgs e)
        {
            
        }              
    }   
}
