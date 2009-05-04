using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;

namespace HPF.CustomActions
{
    internal class PostBackMenuItemTemplate : MenuItemTemplate, IPostBackEventHandler
    {
        // Events
        public event EventHandler<EventArgs> OnPostBack;

        //public PostBackMenuItemTemplate(string text, string imageUrl, string clientOnClickScript) :
        //    base(text, imageUrl, clientOnClickScript) { }

        // Methods
        protected override void EnsureChildControls()
        {
            if (!base.ChildControlsCreated)
            {
                base.EnsureChildControls();
                if (string.IsNullOrEmpty(base.ClientOnClickUsingPostBackEvent))
                {
                    base.ClientOnClickUsingPostBackEventFromControl(this, "%ListId%");
                }
            }
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            EventHandler<EventArgs> onPostBack = this.OnPostBack;
            if (onPostBack != null)
            {
                onPostBack(this, new EventArgs());
            }
        }
    }
}
