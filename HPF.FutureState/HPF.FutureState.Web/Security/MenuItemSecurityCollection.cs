using System;
using System.Collections.Generic;
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
    public class MenuItemSecurityCollection : List<MenuItemSecurity>
    {
        /// <summary>
        /// Get MenuItemSecurity by menuId
        /// </summary>
        /// <param name="target">MenuItem Id</param>
        /// <returns></returns>
        public MenuItemSecurity GetMenuItem(string target)
        {
            return this.SingleOrDefault(item => item.Target == target);
        }
    }
}
