using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.IO;
using System.Web;

namespace HPF.CustomActions
{
    public class DownloadZippedItems : WebControl
    {
        #region "Overwrites"
        protected override void CreateChildControls()
        {
            if (!base.ChildControlsCreated)
            {
                base.CreateChildControls();
                SubMenuTemplate child = new SubMenuTemplate();
                child.Text = "Zip List Items";
                child.ImageUrl = "/_layouts/images/TBSPRSHT.GIF";
                child.Description = "Zip and download List Items";
                
                PostBackMenuItemTemplate templateCurrentView = new PostBackMenuItemTemplate();
                templateCurrentView.Text = "Items In Current View";
                templateCurrentView.Description = "Zip and Download All Items";
                templateCurrentView.ID = "menuCurrentView";
                templateCurrentView.OnPostBack += new EventHandler<EventArgs>(this.mnuListItemCurrentView_OnPostBack);                
                
                child.Controls.Add(templateCurrentView);                
                this.Controls.Add(child);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.EnsureChildControls();
            base.OnLoad(e);
        }
        #endregion

        #region "Event Handlers"
        private void mnuListItemCurrentView_OnPostBack(object sender, EventArgs e)
        {
            this.GetListItems(SPContext.Current.List.ID, false, true);
        }
        #endregion

        #region "Helpers"
        /// <summary>
        /// Zip all items in current view and put to download and archive them
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="IncludeVersions"></param>
        /// <param name="isCurrentView"></param>
        private void GetListItems(Guid listId, bool IncludeVersions, bool isCurrentView)
        {
            SPListItemCollection items;
            string tempPath = Path.GetTempPath();
            string randomFileName = Path.GetRandomFileName();
            string path = tempPath + randomFileName + @"\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            SPList list = SPContext.Current.Web.Lists[listId];
            if (isCurrentView)
            {
                SPView view = SPContext.Current.ViewContext.View;
                SPQuery query = new SPQuery();
                query.Query = view.Query;
                query.ViewAttributes = " Scope=\"Recursive\"";
                items = list.GetItems(query);
            }
            else
            {
                items = list.Items;
            }
            foreach (SPListItem item in items)
            {
                SPFile file = item.File;
                if (file != null)
                {
                    byte[] bytFile = file.OpenBinary();                    
                    string serverRelativeUrl = file.ServerRelativeUrl.Remove(0, 1);
                    string fileUrl = serverRelativeUrl.Substring(serverRelativeUrl.IndexOf("/") + 1).Replace(file.Name, "").Replace("/", @"\");
                    if (!string.IsNullOrEmpty(fileUrl))
                    {
                        Directory.CreateDirectory(path + fileUrl);
                    }
                    string qualifiedFileName = path + fileUrl + file.Name;
                    this.WriteToFile(bytFile, qualifiedFileName);
                    if (IncludeVersions && list.EnableVersioning)
                    {
                        foreach (SPFileVersion version in file.Versions)
                        {
                            if (!version.IsCurrentVersion)
                            {
                                byte[] bufferForVersion = version.OpenBinary();
                                string qualifiedName = qualifiedFileName.Substring(0, qualifiedFileName.LastIndexOf("."));
                                string ver = qualifiedFileName.Substring(qualifiedFileName.LastIndexOf(".") + 1);
                                string finalFileName = qualifiedName + "(" + version.VersionLabel + ")." + ver;
                                this.WriteToFile(bufferForVersion, finalFileName);
                            }
                        }
                        continue;
                    }

                    UpdateReviewStatus(file.Item);
                }
            }
            string outputPathAndFile = randomFileName + ".zip";
            ZipUtilities.ZipFiles(path, outputPathAndFile, string.Empty);
            string fileName = String.Format("{0}_{1}.zip", list.Title, DateTime.Now.ToFileTime());

            ArchiveFiles(path + outputPathAndFile, fileName, string.Format(DownloadAppSettings.ArchiveListName, list.Title));

            PushFileToDownload(path + outputPathAndFile, fileName);
        }        
        
        /// <summary>
        /// Put file to download
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="FileName"></param>
        private void PushFileToDownload(string FilePath, string FileName)
        {
            FileInfo info = new FileInfo(FilePath);
            HttpContext.Current.Response.ContentType = "application/x-download";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
            HttpContext.Current.Response.AddHeader("Content-Length", info.Length.ToString());
            HttpContext.Current.Response.WriteFile(FilePath);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        private bool WriteToFile(byte[] bytFile, string QualifiedFileName)
        {
            FileStream stream = new FileStream(QualifiedFileName, FileMode.OpenOrCreate, FileAccess.Write);
            stream.Write(bytFile, 0, bytFile.Length);
            stream.Close();
            return true;
        }

        /// <summary>
        /// Update Review Status field
        /// </summary>
        /// <param name="spListItem"></param>
        private void UpdateReviewStatus(SPListItem spListItem)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                try
                {
                    SPField reviewStatusField = spListItem.Fields.GetField(DownloadAppSettings.ReviewStatusField);
                    if (reviewStatusField != null)
                    {
                        spListItem[reviewStatusField.Id] = DownloadAppSettings.ReviewStatusDownloadValue;
                        spListItem.Update();
                    }
                }
                catch { }
            });
        }

        /// <summary>
        /// Archive downloaded items to archive list
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="archiveListPath"></param>
        private void ArchiveFiles(string filePath, string fileName, string archiveListPath)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                FileStream zipFileStream = null;
                try
                {
                    SPDocumentLibrary spDocLib = (SPDocumentLibrary)SPContext.Current.Web.Lists[archiveListPath];

                    if (spDocLib != null)
                    {
                        zipFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        SPFile returnSPFile = spDocLib.RootFolder.Files.Add(spDocLib.RootFolder.ServerRelativeUrl + @"\" + fileName, zipFileStream);
                    }
                }
                catch { }
                finally
                {
                    if (zipFileStream != null) { zipFileStream.Close(); }
                    //File.Delete(tempPath + randomFileName);
                }
            });
        }
        #endregion
    }    
}
