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
    public class HPFWebSecurity
    {
        /// <summary>
        /// Current UserIdentity
        /// </summary>
        public static UserIdentity CurrentIdentity
        {
            get
            {
                var userIdentity = HttpContext.Current.User.Identity as UserIdentity;
                return userIdentity ?? new UserIdentity();
            }
        }

        public static bool IsAuthenticated(string userName, string password)
        {
            //var userManager = new XmlFileUserManager();
            //var user = userManager.GetUser(userName, password);
            //return user != null;
            return false;
        }

        public static UserPrincipal CreateUserPrincipal(IIdentity identity)
        {
            var uId = new UserIdentity
            {
                UserID = identity.Name,
                IsAuthenticated = identity.IsAuthenticated,
                AuthenticationType = identity.AuthenticationType
            };
            //var userManager = new XmlFileUserManager();
            //var user = userManager.GetUser(uId.UserID);
            //uId.Roles = user.Roles;
            //uId.DisplayName = user.DisplayName;
            //uId.Email = user.Email;
            return new UserPrincipal(uId);
        }
    }
}
