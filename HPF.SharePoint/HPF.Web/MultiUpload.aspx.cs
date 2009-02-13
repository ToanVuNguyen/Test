using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;
using HPF.SharePointAPI;

namespace HPF.Web
{
    public partial class MultiUpload : LayoutsPageBase
    {
        #region "members"
        private string Source
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Source"]))
                {
                    return Request.QueryString["Source"];
                }
                else
                {
                    return Web.Url;
                }
            }
        }

        private SPDocumentLibrary UploadLibrary
        {
            get
            {
                SPDocumentLibrary uploadLibrary = null;
                if(!String.IsNullOrEmpty(Request.QueryString["List"]))
                {
                    Guid listId = new Guid(Request.QueryString["List"]);
                    uploadLibrary = (SPDocumentLibrary)Web.Lists[listId];
                }
                return uploadLibrary;
            }
        }

        private string RootFolder
        {
            get
            {
                string rootFolder = "RootFolder=";
                string returnUrl = string.Empty;
                int rootFolderIndex = Source.IndexOf(rootFolder);
                if (rootFolderIndex >= 0)
                {
                    int andIndex = Source.IndexOf(@"&", rootFolderIndex);
                    if (andIndex < 0) { andIndex = Source.Length; }

                    int startIndex = rootFolderIndex + rootFolder.Length;
                    int length = andIndex - startIndex;
                    returnUrl = Source.Substring(startIndex, length);
                }
                return returnUrl;
            }
        }
        
        #endregion

        #region "events"
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.ButtonUpload.Click += new EventHandler(ButtonUpload_Click);
            this.ButtonCancel.Click += new EventHandler(ButtonCancel_Click);
        }

        void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Source);
        }

        void ButtonUpload_Click(object sender, EventArgs e)
        {
            UploadFiles();
            Response.Redirect(Source);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        #endregion

        #region "helper"
        private void UploadFiles()
        {
            if (Request.Files != null && Request.Files.Count > 0)
            {
                UploadFileInfo uploadFile;
                List<UploadFileInfo> uploadFileList = new List<UploadFileInfo>();
                HttpPostedFile file = null;
                foreach (string key in Request.Files.Keys)
                {
                    file = Request.Files[key];
                    if (file != null && !string.IsNullOrEmpty(file.FileName))
                    {
                        uploadFile = new UploadFileInfo(file, string.Empty, true);
                        uploadFileList.Add(uploadFile);
                    }
                }

                if (uploadFileList.Count > 0)
                {
                    IList<SPFile> uploadedFiles = null;
                    if (RootFolder.Length == 0)
                    {
                        uploadedFiles = DocumentLibraryHelper.UploadFiles(uploadFileList, UploadLibrary.RootFolder);
                    }
                    else
                    {
                        uploadedFiles = DocumentLibraryHelper.UploadFiles(uploadFileList, RootFolder);
                    }
                }

                Response.Redirect(Source);
            }
        }
        #endregion
    }
}
