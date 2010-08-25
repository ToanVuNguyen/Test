using System.Security.Principal;
using System.Web;
using HPF.FutureState.BusinessLogic;

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
            return SecurityBL.Instance.WebUserLogin(userName, password);            
        }

        public UserPrincipal CreateUserPrincipal(IIdentity identity)
        {
            var uId = CreateUserIdentity(identity);
            AssignUserInformation(uId);
            AssignMenuSecurity(uId);            
            return new UserPrincipal(uId);
        }

        private static UserIdentity CreateUserIdentity(IIdentity identity)
        {
            return new UserIdentity
                       {
                           LoginName = identity.Name,
                           IsAuthenticated = identity.IsAuthenticated,
                           AuthenticationType = identity.AuthenticationType
                       };
        }

        private static void AssignMenuSecurity(UserIdentity uId)
        {
            var menuSecurityList = MenuSecurityBL.Instance.GetMenuSecurityList(uId.UserId);
            if (menuSecurityList != null)
                foreach (var menuSecurity in menuSecurityList)
                    uId.AddMenuItemSecurity(menuSecurity.Target, menuSecurity.Permission);
        }

        private static void AssignUserInformation(UserIdentity uId)
        {
            var user = SecurityBL.Instance.GetWebUser(uId.LoginName);
            uId.Roles = user.UserRole;
            uId.DisplayName = user.FirstName + " " + user.LastName;
            //if (user.FirstName == "" || user.LastName == "")
            //    uId.DisplayName= uId.DisplayName.Replace(",", " ");

            uId.Email = user.Email;
            uId.UserId = user.HPFUserId.Value;
            uId.UserType = user.UserType;
            uId.AgencyId = user.AgencyId;
        }
    }
}
