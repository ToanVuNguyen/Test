using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace HPF.FutureState.Web.Security
{
    public class HPFHttpModule : IHttpModule
    {
        private HttpApplication application;

        #region IHttpModule Members

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            application = context;            
            application.AuthenticateRequest += context_AuthenticateRequest;
        }

        protected void context_AuthenticateRequest(object sender, EventArgs e)
        {
            if (application.User != null)
            {
                application.Context.User = new HPFWebSecurity().CreateUserPrincipal(application.User.Identity);
            }
        }        

        #endregion
    }
}
