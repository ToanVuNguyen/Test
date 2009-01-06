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
        /// <param name="id"></param>
        /// <param name="permission"></param>
        public void AddMenuItemSecurity(string id, char permission)
        {
            MenuItemSecurityList.Add(new MenuItemSecurity {Id = id, Permission = permission});
        }

        /// <summary>
        /// Check current user read permission
        /// </summary>
        /// <param name="menuItemId"></param>
        /// <returns></returns>
        public bool CanRead(string menuItemId)
        {
            var item = GetMenuItem(menuItemId);
            if (item != null)
            {
                return item.Permission == 'R';
            }
            return false;
        }

        /// <summary>
        /// Check current user update permission
        /// </summary>
        /// <param name="menuItemId"></param>
        /// <returns></returns>
        public bool CanUpdate(string menuItemId)
        {
            var item = GetMenuItem(menuItemId);
            if (item != null)
            {
                return item.Permission == 'U';
            }
            return false;
        }

        /// <summary>
        /// Get MenuItemSecurity by menuItemId
        /// </summary>
        /// <param name="menuItemId">MenuItem Id</param>
        /// <returns></returns>
        private MenuItemSecurity GetMenuItem(string menuItemId)
        {
            return MenuItemSecurityList.GetMenuItem(menuItemId);
        }
    }
}
