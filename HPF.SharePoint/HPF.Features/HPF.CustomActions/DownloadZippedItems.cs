using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.IO;
using System.Web;
using DSOFile;

namespace HPF.CustomActions
{
    public class DownloadZippedItems : WebControl
    {
        #region "Overwrites"
        protected override void CreateChildControls()
        {
            SPList spList = SPContext.Current.List;
            if ((DownloadAppSettings.RenderForDocumentLibrary.Count == 0) ||
                (DownloadAppSettings.RenderForDocumentLibrary.Count > 0 && DownloadAppSettings.RenderForDocumentLibrary.Contains(spList.Title)))
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

        #region "Properties"
        public SPView MetaDataView
        {
            get
            {
                SPView view = SPContext.Current.ViewContext.View;
                return view;
            }
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
                SPFolder folder = null;
                if (!String.IsNullOrEmpty(Page.Request.QueryString["RootFolder"]))
                {
                    string rootFolder = Page.Request.QueryString["RootFolder"];
                    folder = SPContext.Current.Web.GetFolder(rootFolder);
                }
                SPView view = SPContext.Current.ViewContext.View;
                SPQuery query = new SPQuery();
                query.Query = view.Query;
                query.ViewAttributes = " Scope=\"Recursive\"";
                if (folder != null) { query.Folder = folder; }
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

                    UpdateReviewStatus(file.Item);

                    WriteToFileAndUpdateMetaData(bytFile, qualifiedFileName, file.Item);
                }
            }
            string outputPathAndFile = randomFileName + ".zip";
            ZipUtilities.ZipFiles(path, outputPathAndFile, string.Empty);

            string[] names = HttpContext.Current.User.Identity.Name.Split(new char[] { '\\', ':' });
            string loginName = HttpContext.Current.User.Identity.Name;
            if (names.Length > 1)
            {
                loginName = names[1];
            }

            //DocumentLibraryTitle-USERNAME_MMDDYYYY
            string newFileName = String.Format("{0}-{1}_{2}.zip",
                list.Title, loginName,
                DateTime.Now.ToString("MMddyyyy"));

            ArchiveFiles(path + outputPathAndFile, newFileName, string.Format(DownloadAppSettings.ArchiveListName, list.Title));

            DeleteSPFiles(items);

            PushFileToDownload(path + outputPathAndFile, newFileName);
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

        private bool WriteToFileAndUpdateMetaData(byte[] bytFile, string QualifiedFileName, SPListItem listItem)
        {
            FileStream stream = new FileStream(QualifiedFileName, FileMode.OpenOrCreate, FileAccess.Write);
            stream.Write(bytFile, 0, bytFile.Length);
            stream.Close();


            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                WriteMetaData(QualifiedFileName, listItem);
            });
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
            if (!Page.Response.IsClientConnected) return;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                FileStream zipFileStream = null;
                try
                {
                    SPWeb sourceWeb = SPContext.Current.Web;
                    SPDocumentLibrary spArchiveLibrary = (SPDocumentLibrary)sourceWeb.Lists[archiveListPath];

                    if (spArchiveLibrary != null)
                    {
                        SPFolder folder = spArchiveLibrary.RootFolder;
                        if (!String.IsNullOrEmpty(Page.Request.QueryString["RootFolder"]))
                        {
                            string archiveFolder = Page.Request.QueryString["RootFolder"].Replace(SPContext.Current.List.RootFolder.ServerRelativeUrl,
                                spArchiveLibrary.RootFolder.ServerRelativeUrl);
                            folder = EnsureSPFolder(sourceWeb, archiveFolder);
                        }

                        zipFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        try
                        {
                            if (folder.Files[fileName] != null) { folder.Files[fileName].Delete(); }
                        }
                        catch { }

                        SPFile returnSPFile = folder.Files.Add(
                            fileName, zipFileStream);
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

        /// <summary>
        /// Write metadata to file and delete it
        /// </summary>
        /// <param name="qualifiedFileName"></param>
        /// <param name="item"></param>
        private void WriteMetaData(string qualifiedFileName, SPListItem spListItem)
        {
            try
            {
                OleDocumentPropertiesClass document = new OleDocumentPropertiesClass();
                document.Open(qualifiedFileName, false, dsoFileOpenOptions.dsoOptionOpenReadOnlyIfNoWriteAccess);

                object value;
                foreach (string field in this.MetaDataView.ViewFields)
                {
                    try
                    {
                        value = Convert.ToString(spListItem[field]);
                        try
                        {
                            if (document.CustomProperties[field.Replace("_x0020_", "")] != null)
                            {
                                document.CustomProperties[field.Replace("_x0020_", "")].set_Value(ref value);
                            }
                            else
                            {
                                document.CustomProperties.Add(field.Replace("_x0020_", ""), ref value);
                            }
                        }
                        catch
                        {
                            document.CustomProperties.Add(field.Replace("_x0020_", ""), ref value);
                        }


                    }
                    catch { }
                }
                try
                {
                    value = spListItem.ContentType.Name;
                    document.CustomProperties.Add("ContentType", ref value);
                }
                catch { }

                document.Save();
                document.Close(true);
            }
            catch { }
        }

        private void DeleteSPFiles(SPListItemCollection items)
        {
            if (Page.Response.IsClientConnected)
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    List<int> deletedIds = new List<int>();

                    foreach (SPListItem spListItem in items)
                    {
                        //check if review status was updated, delete it
                        try
                        {
                            deletedIds.Add(spListItem.ID);
                        }
                        catch { }
                    }

                    deletedIds.ForEach(delegate(int id)
                    {
                        items.DeleteItemById(id);
                    });
                });
            }
        }

        /// <summary>
        /// Ensure SharePoint folder if exists
        /// </summary>
        /// <param name="sourceWeb"></param>
        /// <param name="docUrl"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        private SPFolder EnsureSPFolder(SPWeb sourceWeb, string folderPath)
        {
            SPFolder folder = sourceWeb.GetFolder(folderPath);

            if (folder.Exists) { return folder; }
            string tmpPath = "";
            string[] folders = folderPath.Split(new Char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < folders.Length; i++)
            {
                tmpPath += folders[i] + "/";

                if (!sourceWeb.GetFolder(tmpPath).Exists)
                {
                    folder = sourceWeb.Folders.Add(tmpPath);
                }
            }
            return folder;
        }
        #endregion
    }
}
