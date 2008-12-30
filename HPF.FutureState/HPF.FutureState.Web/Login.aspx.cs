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
using HPF.FutureState.Web.Security;

namespace HPF.FutureState.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            if (HPFWebSecurity.IsAuthenticated(txt_username.Text, txt_password.Text))
                FormsAuthentication.RedirectFromLoginPage(txt_username.Text, false);
            else
                lb_message.Text = "Login failed.";
        }
    }
}
