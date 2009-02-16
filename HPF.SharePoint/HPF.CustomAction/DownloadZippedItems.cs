using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.WebControls;
using System.IO;
using System.Web.UI;
using Microsoft.SharePoint;
using HPF.SharePointAPI;
using System.Web;

namespace HPF.CustomAction
{
    public class DownloadZippedItems : WebControl, INamingContainer
    {
        protected override void CreateChildControls()
        {
            if (!base.ChildControlsCreated)
            {
                base.CreateChildControls();

                PostBackMenuItemTemplate downloadMenu = new PostBackMenuItemTemplate();
                downloadMenu.ID = "downloadMenu";
                downloadMenu.Text = "Zip and download selected document(s)";                
                
                downloadMenu.ClientOnClickPostBackConfirmation = "You will download selected document(s), depending on the item count and size it might be take sometime, are you sure ?";                
                
                downloadMenu.OnPostBack += new EventHandler<EventArgs>(downloadMenu_OnPostBack);                
                this.Controls.Add(downloadMenu);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            EnsureChildControls();
            base.OnLoad(e);

            this.Page.ClientScript.RegisterClientScriptInclude("DownLoadZipItem", "/_layouts/1033/DownLoadZipItem.js");

            if (!this.Page.IsPostBack)
            {
                this.Page.ClientScript.RegisterHiddenField("hiddenFieldSelectItemId", "");                
            }
        }

        void downloadMenu_OnPostBack(object sender, EventArgs e)
        {
            EnsureChildControls();
            string itemIds = this.Page.Request.Form["hiddenFieldSelectItemId"];
            if (itemIds.Length > 0)
            {
                string[] ids = itemIds.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                List<SPListItem> zippedFiles = new List<SPListItem>();
                SPFile file;
                SPWeb web = Microsoft.SharePoint.SPContext.Current.Web;
                if (web != null)
                {
                    SPList list = web.Lists["ReportDocumentLibrary"];
                    SPDocumentLibrary historyList = (SPDocumentLibrary)web.Lists["DownloadHistoryDocumentLibrary"];
                    if (list != null)
                    {
                        SPListItem listItem;
                        foreach (string id in ids)
                        {
                            listItem = list.GetItemById(int.Parse(id));
                            if (listItem != null)
                            {
                                zippedFiles.Add(listItem);
                            }
                        }

                        if (zippedFiles.Count > 0)
                        {
                            ResultInfo<SPFile> result = DocumentLibraryHelper.CompressSPFiles(zippedFiles, historyList);
                            if (result.Successful)
                            {
                                file = result.BizObject;
                                string script = String.Format("openDownloadWindow('{0}');", file.ServerRelativeUrl);
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "download", script, true);
                            }
                        }
                    }
                }
            }
        }



    }
}
