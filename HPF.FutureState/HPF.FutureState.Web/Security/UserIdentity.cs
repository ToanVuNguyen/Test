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
    public class UserIdentity : IIdentity
    {
        public string UserID { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        private string roles = string.Empty;

        public string Roles
        {
            get { return roles; }
            set { roles = value; }
        }

        #region IIdentity Members

        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public string Name
        {
            get { return UserID; }
        }

        #endregion

        public bool IsInRole(string role)
        {
            return Roles.ToLower().Split(',').Contains(role.ToLower());
        }

    }
}
