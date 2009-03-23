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

namespace HPF.FutureState.Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Admin
            MenuBarControl.UserId = HPFWebSecurity.CurrentIdentity.UserId;
            lblUserName.Text = HPFWebSecurity.CurrentIdentity.DisplayName;
            lblVersion.Text = HPFConfigurationSettings.HPF_VERSION;
            //Disable ForeclosureCaseInfo when user dont has neither read nor write permision 
            if(!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                MenuBarControl.DisableAGroupMenu("3");
            }
        }
    }
}
