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
            this.RadioButtonFromClient.CheckedChanged += new EventHandler(RadioButtonFromClient_CheckedChanged);
            this.RadioButtonFromServer.CheckedChanged += new EventHandler(RadioButtonFromServer_CheckedChanged);
        }

        void RadioButtonFromServer_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButtonFromServer.Checked)
            {
                PanelUploadFromServer.Visible = true;
                PanelUploadFromClient.Visible = false;
            }
        }

        void RadioButtonFromClient_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButtonFromClient.Checked)
            {
                PanelUploadFromClient.Visible = true;
                PanelUploadFromServer.Visible = false;
            }
        }
        
        void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Source);
        }

        void ButtonUpload_Click(object sender, EventArgs e)
        {
            if (this.RadioButtonFromClient.Checked)
            {
                UploadFilesFromClient();
            }
            else
            {
                UploadFileFromServer();
            }
            Response.Redirect(Source);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        #endregion

        #region "helper"
        private void UploadFilesFromClient()
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

                IList<ResultInfo<SPFile>> results = null;
                if (uploadFileList.Count > 0)
                {                    
                    if (RootFolder.Length == 0)
                    {
                        results = DocumentLibraryHelper.UploadFiles(uploadFileList, UploadLibrary.RootFolder);
                    }
                    else
                    {
                        results = DocumentLibraryHelper.UploadFiles(uploadFileList, RootFolder);
                    }
                }

                if (uploadFileList != null && uploadFileList.Count > 0)
                {
                    foreach (ResultInfo<SPFile> f in results)
                    {
                        //todo: handle error
                    }
                }
            }
        }

        private void UploadFileFromServer()
        {
            if (RootFolder.Length == 0)
            {
                DocumentLibraryHelper.UploadFiles(this.TextBoxServerLocation.Text, UploadLibrary.RootFolder);
            }
            else
            {
                DocumentLibraryHelper.UploadFiles(this.TextBoxServerLocation.Text, RootFolder);
            }
            
        }
        #endregion
    }
}
