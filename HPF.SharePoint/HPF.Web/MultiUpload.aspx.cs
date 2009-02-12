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
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.ButtonUpload.Click += new EventHandler(ButtonUpload_Click);
            this.ButtonCancel.Click += new EventHandler(ButtonCancel_Click);
        }

        void ButtonCancel_Click(object sender, EventArgs e)
        {
            
        }

        void ButtonUpload_Click(object sender, EventArgs e)
        {
            UploadFiles();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);            
        }

        private void UploadFiles()
        {
            if (Request.Files != null && Request.Files.Count > 0)
            {
                UploadFileInfo uploadFile;
                List<UploadFileInfo> uploadFileList = new List<UploadFileInfo>();
                foreach (HttpPostedFile file in Request.Files)
                {
                    uploadFile = new UploadFileInfo(file, string.Empty, true);
                }


                SPDocumentLibrary uploadLibrary = (SPDocumentLibrary)Web.Lists["ReportDocumentLibrary"];

                //List<SPFile> uploadedFiles = DocumentLibraryHelper.UploadFiles(uploadFileList, uploadLibrary.RootFolder);
            }
        }
    }
}
