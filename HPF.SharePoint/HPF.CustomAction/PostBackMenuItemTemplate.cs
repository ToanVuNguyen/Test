using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;

namespace HPF.CustomAction
{
    public class PostBackMenuItemTemplate:MenuItemTemplate, IPostBackEventHandler
    {
        public event EventHandler<EventArgs> OnPostBack = null;

        protected override void CreateChildControls()
        {
            if (!base.ChildControlsCreated)
            {
                base.EnsureChildControls();

                if (string.IsNullOrEmpty(base.ClientOnClickUsingPostBackEvent))
                {                    
                    base.ClientOnClickUsingPostBackEventFromControl(this);
                }
            }
        }
        #region IPostBackEventHandler Members

        public void RaisePostBackEvent(string eventArgument)
        {
            if (OnPostBack != null)
            {
                OnPostBack(this, new EventArgs());
            }
        }

        #endregion
    }
}
