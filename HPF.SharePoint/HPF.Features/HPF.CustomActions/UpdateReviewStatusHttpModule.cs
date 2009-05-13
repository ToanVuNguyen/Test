using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.SharePoint;

namespace HPF.CustomActions
{
    /// <summary>
    /// <add name="HPFHttpModule" type="HPF.CustomActions.UpdateReviewStatusHttpModule, HPF.CustomActions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=886bf919f6a90bbd" />
    /// </summary>
    public class UpdateReviewStatusHttpModule : IHttpModule
    {
        #region IHttpModule Members

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.PostAuthorizeRequest += new EventHandler(context_PostAuthorizeRequest);
        }

        void context_PostAuthorizeRequest(object sender, EventArgs e)
        {
            UpdateReviewStatus();
        }

        #endregion

        private void UpdateReviewStatus()
        {
            try
            {
                string documentUrl = HttpContext.Current.Request.RawUrl;
                SPWeb web = SPContext.Current.Web;
                SPFile file = web.GetFile(documentUrl);                
                if (file != null && file.InDocumentLibrary)
                {
                    SPListItem item = file.Item;                    
                    if (item != null &&
                        DownloadAppSettings.RenderForDocumentLibrary.Contains(item.ParentList.Title))
                    {
                        web.AllowUnsafeUpdates = true;
                        item[DownloadAppSettings.ReviewStatusField] =
                            DownloadAppSettings.ReviewStatusDownloadValue;
                        item.SystemUpdate();
                        web.AllowUnsafeUpdates = false;
                    }
                }                
            }
            catch { }
        }
    }
}
