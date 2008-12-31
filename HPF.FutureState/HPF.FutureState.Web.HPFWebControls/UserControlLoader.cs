using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPF.FutureState.Web.HPFWebControls
{
    [ToolboxData("<{0}:UserControlLoader runat=server></{0}:UserControlLoader>")]
    public class UserControlLoader : Control
    {
        public string UserControlVirtualPath
        {
            get
            {
                var s = ViewState["UserControlURL"] as string;
                return (s ?? string.Empty);
            }
            set
            {
                ViewState["UserControlURL"] = value;
            }
        }

        public string UserControlID
        {
            get
            {
                var s = ViewState["UserControlID"] as string;
                return (s ?? string.Empty);
            }
            set
            {
                ViewState["UserControlID"] = value;
            }
        }
        /// <summary>
        /// Load UserControl by control virtualPath and control id 
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <param name="id"></param>
        public void LoadUserControl(string virtualPath, string id)
        {
            if (virtualPath == string.Empty || id == string.Empty)
                return;
            var uc = Page.LoadControl(virtualPath);
            uc.ID = id;
            Controls.Clear();
            Controls.Add(uc);
            UserControlVirtualPath = virtualPath;
            UserControlID = id;
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadUserControl(UserControlVirtualPath, UserControlID);
        }
        protected override void Render(HtmlTextWriter writer)
        {
            if (DesignMode)
                return;
            base.Render(writer);
        }
        
        
    }
}
