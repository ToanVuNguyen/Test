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
    public class UserControlLoader :Control
    {
        public string UserControlURL
        {
            get
            {
                string s = ViewState["UserControlURL"] as string;
                return ((s == null) ? string.Empty : s);
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
                string s = ViewState["UserControlID"] as string;
                return ((s == null) ? string.Empty : s);
            }
            set
            {
                ViewState["UserControlID"] = value;
            }
        }
        /// <summary>
        /// Load UserControl by control URL and control ID 
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="ID"></param>
        public void LoadUC(string URL,string ID)
        {
            this.Controls.Clear();
            if (URL == string.Empty || ID == string.Empty)
                return;
            var uc = Page.LoadControl(URL);
            uc.ID = ID;
            this.Controls.Add(uc);
            if (UserControlURL == string.Empty)
            {
                UserControlURL = URL;
                UserControlID = ID;
            }
            
        }
        
        protected override void OnLoad(EventArgs e)
        {
            LoadUC(UserControlURL, UserControlID);
        }
        protected override void Render(HtmlTextWriter writer)
        {
            if (DesignMode)
                return;
            base.Render(writer);
        }
        
        
    }
}
