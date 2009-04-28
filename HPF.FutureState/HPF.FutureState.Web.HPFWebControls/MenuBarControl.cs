using System;
using System.Collections.Generic;
using System.Web.UI;

namespace HPF.FutureState.Web.HPFWebControls
{
    [ToolboxData("<{0}:MenuBarControl runat=server></{0}:MenuBarControl>")]
    public class MenuBarControl : Control
    {
        private readonly List<string> _disabledMenus;

        private readonly List<string> _enabledMenus;

        private readonly List<string> _DisabledGroupMenus;

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
        public int UserId
        {
            get
            {
                if (ViewState["UserId"] != null)
                    return int.Parse(ViewState["UserId"].ToString());
                return -1;
            }
            set
            {
                ViewState["UserId"] = value;
            }
        }
        public MenuBarControl()
        {
            _disabledMenus = new List<string>();
            _enabledMenus = new List<string>();
            _DisabledGroupMenus = new List<string>();
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

        public void DisableAGroupMenu(string id)
        {
            _DisabledGroupMenus.Add(id);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (DesignMode)
                return;
            var menuBar = GetMenuBar();
            foreach (var menuGroup in menuBar)
            {
                if (_DisabledGroupMenus.Contains(menuGroup.Id))
                {
                    menuGroup.Enabled = false;
                }
            }
            //Render Menu
            writer.Write("<table id= 'sddm' cellpadding='0' cellspacing='0'>");
            writer.Write("<tr>");

            foreach (var menu in menuBar)
            {
                //dont have child
                if (menu.Count == 0)
                {
                    writer.Write("<td valign='middle' align='center' onmouseover=\"MenuMouseOver(this)\" onmouseout=\"MenuMouseLeave(this)\">");
                    if (menu.Enabled)
                        writer.Write("<a id='menuItem' href=\"{0}\" id=\"{1}\">{2}</a>", menu.Url, menu.Id, menu.Title);
                    else
                        //writer.Write("<a id=\"{0}\" style=\"display: block;margin: 0 1 0 0; padding: 6 6;background: transparent;color:Gray; cursor:pointer; text-align: center; text-decoration: none;width: auto;\">{1}</a>", menu.Id, menu.Title);
                        writer.Write("<font color=\"gray\">" + menu.Title + "</font>");
                }
                else
                {
                    writer.Write("<td onmouseover=\"mopen(this,'{0}', '{1}')\">", "m" + menu.Id, menu.Id);
                    if (menu.Enabled)
                    {
                        writer.Write("<a id='menuItem' href=\"{0}\" id=\"{1}\">{2}</a>", menu.Url, menu.Id, menu.Title);
                        //writer.Write("<div id=\"{0}\" style=\"display:none;\">", "m" + menu.Id);
                        writer.Write("<table id=\"{0}\" onmouseout=\"mclosetime()\" cellspacing='0'>", "m" + menu.Id);
                        foreach (var submenu in menu)
                        {
                            writer.Write("<tr>");
                            writer.Write("<td onmouseover=\"mcancelclosetime()\">");
                            if (submenu.Enabled)
                                writer.Write("<a id='subMenuItem' href=\"{0}\">&nbsp;{1}&nbsp;</a>", submenu.Url, submenu.Title);
                            else
                                //writer.Write("<a style=\"position: relative;display: block; margin: 0; padding: 5 10; width: auto; white-space: nowrap; text-align: left; text-decoration: none; background: #8FC4F6;  color:Gray; cursor:default; font: 11 Tahoma; height: 10;\">{0}</a>", submenu.Title); writer.Write("</td>");
                                writer.Write("<font color=\"gray\">" + menu.Title + "</font>");
                            writer.Write("</tr>");
                        }
                        writer.Write("</table>");
                        //writer.Write("</div>");
                    }
                    else
                        writer.Write("<a id=\"{0}\" style=\"display: block;margin: 0 1 0 0; padding: 6 6;background: transparent;color:Gray; cursor:pointer; text-align: center; text-decoration: none;width: auto;\">{1}</a>", menu.Id, menu.Title);
                    //writer.Write("<font color=\"gray\">" +menu.Title + "</font>");

                }
                writer.Write("</td>");

            }
            writer.Write("</tr>");
            writer.Write("</table>");
        }

        private MenuBar GetMenuBar()
        {
            var menuBar = new MenuBar();
            // menuBar.Load(Page.MapPath(XmlMenuFile));
            menuBar.LoadFromDatabase(UserId);
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
        protected override void OnLoad(System.EventArgs e)
        {
            if (DesignMode)
                return;
            //Add CSS to client page
            //string includeTemplate = "<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />";
            //string includeLocation = Page.ClientScript.GetWebResourceUrl(this.GetType(), "HPF.FutureState.Web.HPFWebControls.MenuBarControl.css");
            //LiteralControl include = new LiteralControl(String.Format(includeTemplate, includeLocation));
            //Page.Header.Controls.Add(include);

            ////Add JavaScript to client page
            //string resourceName = "HPF.FutureState.Web.HPFWebControls.MenuBarControl.js";
            //ClientScriptManager cs = this.Page.ClientScript;
            //cs.RegisterClientScriptResource(this.GetType(), resourceName);
        }
    }
}
