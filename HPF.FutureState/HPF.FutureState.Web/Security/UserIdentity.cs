using System.Linq;
using System.Security.Principal;

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

        public MenuItemSecurityCollection MenuItemSecurityList { get; private set; }

        #region IIdentity Members

        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public string Name
        {
            get { return UserID; }
        }

        #endregion

        public UserIdentity()
        {
            MenuItemSecurityList = new MenuItemSecurityCollection();            
        }

        public bool IsInRole(string role)
        {
            return Roles.ToLower().Split(',').Contains(role.ToLower());
        }

        /// <summary>
        /// Add MenuItemSecurity
        /// </summary>
        /// <param name="target"></param>
        /// <param name="permission"></param>
        public void AddMenuItemSecurity(string target, char permission)
        {
            MenuItemSecurityList.Add(new MenuItemSecurity {Target = target, Permission = permission});
        }

        /// <summary>
        /// Check current user read permission
        /// </summary>
        /// <param name="menuItemTarget"></param>
        /// <returns></returns>
        public bool CanRead(string menuItemTarget)
        {
            var item = GetMenuItem(menuItemTarget);
            if (item != null)
            {
                return item.Permission == 'R';
            }
            return false;
        }

        /// <summary>
        /// Check current user update permission
        /// </summary>
        /// <param name="menuItemTarget"></param>
        /// <returns></returns>
        public bool CanUpdate(string menuItemTarget)
        {
            var item = GetMenuItem(menuItemTarget);
            if (item != null)
            {
                return item.Permission == 'U';
            }
            return false;
        }

        /// <summary>
        /// Get MenuItemSecurity by target
        /// </summary>
        /// <param name="target">MenuItem target</param>
        /// <returns></returns>
        private MenuItemSecurity GetMenuItem(string target)
        {
            return MenuItemSecurityList.GetMenuItem(target);
        }
    }
}
