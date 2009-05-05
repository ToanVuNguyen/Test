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
using System.Web.UI;
using System.Threading;

namespace HPF.CustomActions
{
    public class DownloadZippedItems : WebControl, ICallbackEventHandler
    {
        #region "Consts"
        public const string IN_PROGRESS = "InProgress";
        public const string PROGRESS_PERCENTAGE = "ProcessPercentage";
        public const string PROGRESS_ACTION = "ProgressAction";
        public const string HAS_ERROR = "HasError";
        public const string ERROR_MESSAGE = "ErrorMessage";
        #endregion

        #region "Fields"
        ProgressContext _progressContext = ProgressContext.Current;
        uint _rowLimit = 50;
        #endregion

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
                    templateCurrentView.ID = "menuDownloadCurrentView";

                    templateCurrentView.Attributes.Add("onclick", "InvokeProgressViaServerSide(this);");
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

            String cbReference = Page.ClientScript.GetCallbackEventReference(this,
                "arg", "ReceiveProgressDataFromServer", "context");

            String callbackScript = "function CallProgressData(arg, context)" +
                "{ " + cbReference + ";}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                "CallServer", callbackScript, true);

            Page.ClientScript.RegisterClientScriptInclude("DownloadZippedItems", "/_layouts/1033/DownloadZippedItems.js?rev=" + DateTime.Now.ToFileTime());
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
            _progressContext[IN_PROGRESS] = "true";

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
                //_rowLimit = view.RowLimit - 1;
                query.ViewAttributes = " Scope=\"Recursive\"";
                if (folder != null) { query.Folder = folder; }
                items = list.GetItems(query);
            }
            else
            {
                items = list.Items;
            }

            /******************************************************/
            //Update progress bar: Updating meta data
            /******************************************************/
            int index = 0;
            _progressContext[PROGRESS_ACTION] = "Updating Meta Data";
            _progressContext[PROGRESS_PERCENTAGE] = "0";
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

                    /******************************************************/
                    //Update progress bar: updating meta data
                    /******************************************************/
                    if (++index % _rowLimit == 0) Thread.Sleep(500);
                    updateProgressAction((double)20 / items.Count);
                }
            }
            string outputPathAndFile = randomFileName + ".zip";

            /******************************************************/
            //Update progress bar: zipping files
            /******************************************************/
            _progressContext[PROGRESS_ACTION] = "Zipping files";
            //_progressContext[PROGRESS_PERCENTAGE] = "20";

            long length = ZipUtilities.ZipFiles(path, outputPathAndFile, string.Empty, _rowLimit, updateProgressAction);

            //The size of the file that is uploaded cannot exceed 2 GB.
            //http://msdn.microsoft.com/en-us/library/ms454491.aspx
            if (ConvertToGigabytes((ulong)length) > 2)
            {
                _progressContext[PROGRESS_PERCENTAGE] = "100";
                _progressContext[IN_PROGRESS] = "false";
                _progressContext[HAS_ERROR] = "true";
                _progressContext[ERROR_MESSAGE] = "Can not archive zipped file because the size of the file exceed 2 GB";
                return;
            }

            /******************************************************/
            //update progress bar: Finish zipping files
            /******************************************************/
            //_progressContext[PROGRESS_PERCENTAGE] = "50";

            string[] names = HttpContext.Current.User.Identity.Name.Split(new char[] { '\\', ':' });
            string loginName = HttpContext.Current.User.Identity.Name;
            if (names.Length > 1)
            {
                loginName = names[1];
            }

            //DocumentLibraryTitle-USERNAME_MMDDYYYY
            string newFileName = String.Format("{0}-{1}_{2}.zip",
                list.Title, loginName,
                DateTime.Now.ToString("MMddyyyy hhmmsstt"));

            /******************************************************/
            //Update progress bar: Start archieving files
            /******************************************************/
            _progressContext[PROGRESS_ACTION] = "Archiving zipped file";
            Thread.Sleep(500);

            string archiveFileUrl = "";
            if (ArchiveFiles(path + outputPathAndFile, newFileName,
                string.Format(DownloadAppSettings.ArchiveListName, list.Title), out archiveFileUrl))
            {
                updateProgressAction(30);
                _progressContext[PROGRESS_ACTION] = "Cleaning up files";

                DeleteSPFiles(items);
                _progressContext[PROGRESS_PERCENTAGE] = "100";
                Thread.Sleep(500);

                /******************************************************/
                //Update progress bar: Done!
                /******************************************************/
                _progressContext[IN_PROGRESS] = "false";

                PushFileToDownload(path + outputPathAndFile, newFileName);
                //PushFileToDownload(archiveFileUrl);
            }
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

        //private void PushFileToDownload(string archiveFileUrl)
        //{
        //    string script = string.Format("downloadArchiveFile('{0}');", archiveFileUrl);
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "download", script, true);
        //}

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
        private bool ArchiveFiles(string filePath, string fileName, string archiveListPath, out string archiveFileUrl)
        {
            string returnArchiveUrl = "";
            bool archiveSuccess = false;
            if (!Page.Response.IsClientConnected)
            {
                archiveFileUrl = returnArchiveUrl;
                return archiveSuccess;
            }
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

                        returnArchiveUrl = Microsoft.SharePoint.Utilities.SPUtility.GetFullUrl(sourceWeb.Site, "/" + returnSPFile.Url);

                        archiveSuccess = true;
                    }
                }
                catch { archiveSuccess = false; }
                finally
                {
                    if (zipFileStream != null) { zipFileStream.Close(); }
                }
            });

            archiveFileUrl = returnArchiveUrl;
            return archiveSuccess;
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

        /// <summary>
        /// Delete Zipped SPListItem collection
        /// </summary>
        /// <param name="items"></param>
        private void DeleteSPFiles(SPListItemCollection items)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                int total = items.Count;
                //List<int> deletedIds = new List<int>();

                //foreach (SPListItem spListItem in items)
                //{
                //    deletedIds.Add(spListItem.ID);
                //}
                //int index = 0;
                //deletedIds.ForEach(delegate(int id)
                //{
                //    if (Page.Response.IsClientConnected)
                //    {
                //        try
                //        {
                //            //todo: rem for test
                //            //items.DeleteItemById(id);
                //            if (++index % _rowLimit == 0) Thread.Sleep(500);
                //            updateProgressAction((double)20 / total);
                //        }
                //        catch { }
                //    }
                //});

                /*****************************************/
                /*Delete a batch of items*/
                List<KeyValuePair<int, string>> deletedIds = new List<KeyValuePair<int, string>>();
                foreach (SPListItem spListItem in items)
                {
                    deletedIds.Add(new KeyValuePair<int, string>(spListItem.ID, spListItem.File.ServerRelativeUrl));
                }
                StringBuilder sbDelete = new StringBuilder();

                sbDelete.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Batch OnError='Return'>");

                int batchSize = 20;
                int count = 0, index = 0;
                SPContext.Current.Web.AllowUnsafeUpdates = true;
                while (index < total)
                {
                    count = index + 100 > total ? total - index : batchSize;
                    List<KeyValuePair<int, string>> itemIds = deletedIds.GetRange(index, count);
                    foreach (KeyValuePair<int, string> item in itemIds)
                    {
                        sbDelete.Append("<Method>");
                        sbDelete.Append("<SetList Scope=\"Request\">" +
                        SPContext.Current.List.ID + "</SetList>");
                        sbDelete.Append("<SetVar Name=\"ID\">" +
                        Convert.ToString(item.Key) + "</SetVar>");
                        sbDelete.Append("<SetVar Name=\"Cmd\">Delete</SetVar>");
                        sbDelete.Append("<SetVar Name=\"owsfileref\">" + item.Value + "</SetVar>");
                        sbDelete.Append("</Method>");
                    }
                    sbDelete.Append("</Batch>");
                    try
                    {
                        string processBatch = SPContext.Current.Web.ProcessBatchData(sbDelete.ToString());
                        Thread.Sleep(500);
                        updateProgressAction((double)20 * batchSize / total);
                    }
                    catch (Exception ex)
                    {
                        //Log Error
                    }
                    index += batchSize;
                }

                /*****************************************/
            });
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

        static decimal ConvertToGigabytes(ulong bytes)
        {            
            return ((decimal)bytes / 1024M / 1024M / 1024M);
        } 
        #endregion

        #region ICallbackEventHandler Members

        public string GetCallbackResult()
        {
            return _progressContext.SerializeToJSonObject();
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            //Remove Progress Context if Download done
            if (_progressContext[IN_PROGRESS] == "false")
            {
                _progressContext.RemoveProgressContext();
            }
        }

        #endregion

        #region "Progress Helpers"
        void updateProgressAction(double percentage)
        {
            if (string.IsNullOrEmpty(_progressContext[PROGRESS_PERCENTAGE]))
            {
                _progressContext[PROGRESS_PERCENTAGE] = "0";
            }
            double currentPercentage = double.Parse(_progressContext[PROGRESS_PERCENTAGE]) + percentage;
            _progressContext[PROGRESS_PERCENTAGE] = currentPercentage.ToString("N2");
        }
        #endregion
    }
}
