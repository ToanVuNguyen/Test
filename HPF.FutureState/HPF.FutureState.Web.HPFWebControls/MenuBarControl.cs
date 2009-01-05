using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPF.FutureState.Web.HPFWebControls
{    
    [ToolboxData("<{0}:MenuBarControl runat=server></{0}:MenuBarControl>")]
    public class MenuBarControl : Control
    {
        public List<string> _disabledMenus;

        public List<string> _enabledMenus;

        public string XmlMenuFile
        {
            get
            {
                var s = ViewState["XmlMenuFile"] as string;
                return (s ?? string.Empty);
            }
            set
            {
                ViewState["XmlMenuFile"] = value;
            }
        }

        public MenuBarControl()
        {
            _disabledMenus = new List<string>();
            _enabledMenus = new List<string>();
        }
        /// <summary>
        /// Enabled Menu by menu id
        /// </summary>
        /// <param name="id"></param>
        public void EnabledMenu(string id)
        {
            _enabledMenus.Add(id);
        }

        /// <summary>
        /// Disable Menu by menu id
        /// </summary>
        /// <param name="id"></param>
        public void DisabledMenu(string id)
        {
            _disabledMenus.Add(id);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            var menuBar = GetMenuBar();
            //Render Menu
            foreach (var menu in menuBar)
            {
                
            }
        }

        private MenuBar GetMenuBar()
        {
            var menuBar = new MenuBar();
            menuBar.Load(Page.MapPath(XmlMenuFile));
            //Disabled menu
            foreach (var disabledMenuId in _disabledMenus)
            {
                menuBar.DisabledMenu(disabledMenuId);
            }
            //Enabled menu
            foreach (var enabledMenuId in _enabledMenus)
            {
                menuBar.EnabledMenu(enabledMenuId);
            }
            return menuBar;
        }
    }
}
