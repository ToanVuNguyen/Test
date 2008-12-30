using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace HPF.FutureState.Web.Security
{
    public sealed class UserPrincipal : IPrincipal
    {
        private readonly UserIdentity userIdentity;

        public UserPrincipal(UserIdentity userIdentity)
        {
            this.userIdentity = userIdentity;
        }

        #region IPrincipal Members

        public IIdentity Identity
        {
            get { return userIdentity; }
        }

        public bool IsInRole(string role)
        {
            return userIdentity.IsInRole(role);
        }

        #endregion
    }
}
