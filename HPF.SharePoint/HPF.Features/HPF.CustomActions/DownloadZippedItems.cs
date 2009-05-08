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
using Microsoft.Office.Server.Diagnostics;

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
        ProgressContext _progressContext;
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
                    //SubMenuTemplate child = new SubMenuTemplate();
                    //child.Text = "Zip List Items";
                    //child.ImageUrl = "/_layouts/1033/images/hpf_zip.gif";
                    //child.Description = "Zip and download List Items";

                    PostBackMenuItemTemplate templateCurrentView = new PostBackMenuItemTemplate();
                    templateCurrentView.Text = "Zip Items In Current View";
                    templateCurrentView.Description = "Zip and Download All Items";
                    templateCurrentView.ID = "menuDownloadCurrentView";
                    templateCurrentView.ImageUrl = "/_layouts/1033/images/hpf_zip.gif";

                    templateCurrentView.Attributes.Add("onclick", "InvokeProgressViaServerSide(this);");
                    templateCurrentView.OnPostBack += new EventHandler<EventArgs>(this.mnuListItemCurrentView_OnPostBack);

                    //child.Controls.Add(templateCurrentView);
                    //this.Controls.Add(child);
                    this.Controls.Add(templateCurrentView);
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

            Page.RegisterHiddenField("_ProgressContext", Guid.NewGuid().ToString());
        }
        #endregion

        #region "Event Handlers"
        private void mnuListItemCurrentView_OnPostBack(object sender, EventArgs e)
        {
            _progressContext = ProgressContext.GetCurrentProgressContext(
                Page.Request["_ProgressContext"]);

            this.GetListItems(SPContext.Current.List.ID);
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
        private void GetListItems(Guid listId)
        {
            _progressContext[IN_PROGRESS] = "true";
            string tempPath = Path.GetTempPath();
            string randomFileName = Path.GetRandomFileName();
            string path = tempPath + randomFileName + @"\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            SPList list = SPContext.Current.Web.Lists[listId];
            SPListItemCollection items = GetItemsOnCurrentViewAndFolder(list);

            if (DownloadAppSettings.TotalFilesAllow > 0 &&
                items.Count > DownloadAppSettings.TotalFilesAllow)
            {
                Alert(String.Format(DownloadAppSettings.TOTAL_FILES_EXCEED, DownloadAppSettings.TotalFilesAllow));
                _progressContext.RemoveProgressContext();
                return;
            }

            /******************************************************/
            //Update progress bar: Updating meta data
            /******************************************************/
            int index = 0;
            _progressContext[PROGRESS_ACTION] = DownloadAppSettings.UPDATE_META_DATA;
            _progressContext[PROGRESS_PERCENTAGE] = "0";
            foreach (SPListItem item in items)
            {
                SPFile file = item.File;
                if (file != null && Page.Response.IsClientConnected)
                {
                    byte[] bytFile = file.OpenBinary();
                    string serverRelativeUrl = file.ServerRelativeUrl.Remove(0, 1);
                    string fileUrl = serverRelativeUrl.Substring(serverRelativeUrl.IndexOf("/") + 1).Replace(file.Name, "").Replace("/", @"\");
                    if (!string.IsNullOrEmpty(fileUrl))
                    {
                        Directory.CreateDirectory(path + fileUrl);
                    }
                    string qualifiedFileName = path + fileUrl + file.Name;

                    if (!Page.Response.IsClientConnected) return;
                    UpdateReviewStatus(file.Item);
                    try
                    {
                        WriteToFileAndUpdateMetaData(bytFile, qualifiedFileName, file.Item);
                    }
                    catch { }

                    /******************************************************/
                    //Update progress bar: updating meta data
                    /******************************************************/
                    if (++index % _rowLimit == 0) Thread.Sleep(200);
                    updateProgressAction((double)20 / items.Count);
                }
            }
            string outputPathAndFile = randomFileName + ".zip";

            /******************************************************/
            //Update progress bar: zipping files
            /******************************************************/
            _progressContext[PROGRESS_ACTION] = DownloadAppSettings.ZIPPING_FILES;            

            long length = ZipUtilities.ZipFiles(path, outputPathAndFile, string.Empty, _rowLimit, updateProgressAction);

            //The size of the file that is uploaded cannot exceed 2 GB.
            //http://msdn.microsoft.com/en-us/library/ms454491.aspx
            if (ConvertToGigabytes((ulong)length) > 2)
            {
                Alert(DownloadAppSettings.SIZE_EXCEED_2G);
                _progressContext.RemoveProgressContext();
                return;
            }
            
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
            _progressContext[PROGRESS_ACTION] = DownloadAppSettings.ARCHIVING_ZIPPED_FILE;
            Thread.Sleep(500);


            SPFile spFile = null;
            bool archiveSuccess = ArchiveFiles(path + outputPathAndFile, newFileName,
                string.Format(DownloadAppSettings.ArchiveListName, list.Title), ref spFile);
            if (archiveSuccess)
            {
                updateProgressAction(20);
                _progressContext[PROGRESS_ACTION] = DownloadAppSettings.CLEANING_UP_FILES;

                DeleteSPFiles(items);
                _progressContext[PROGRESS_PERCENTAGE] = "100";
                Thread.Sleep(500);
            }

            /******************************************************/
            //Update progress bar: Done!
            /******************************************************/
            _progressContext[IN_PROGRESS] = "false";

            //If zipped file was archived successfully, 
            //push file to download from SPFile
            if (archiveSuccess && spFile != null)
            {
                try
                {                    
                    Directory.Delete(path, true);
                }
                catch (Exception error)
                {
                    PortalLog.LogString("[HPF] Exception Occurred: {0} || {1}",
                        error.Message, error.StackTrace);
                }
                PushFileToDownload(spFile);
            }
            else
            {
                PushFileToDownload(path + outputPathAndFile, newFileName);
            }
        }

        /// <summary>
        /// Get SPListItems on current view and folder
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private SPListItemCollection GetItemsOnCurrentViewAndFolder(SPList list)
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
            SPListItemCollection items = list.GetItems(query);

            return items;
        }

        /// <summary>
        /// Put file to download from temp folder
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

        /// <summary>
        /// Put file to download from archived file
        /// </summary>
        /// <param name="file"></param>
        private void PushFileToDownload(SPFile file)
        {            
            HttpContext.Current.Response.ContentType = "application/x-download";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + file.Name);
            HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());

            byte[] bytes = file.OpenBinary();
            HttpContext.Current.Response.BinaryWrite(bytes);
        }
        
        /// <summary>
        /// Write file to temp folder and update its meta-data
        /// </summary>
        /// <param name="bytFile"></param>
        /// <param name="QualifiedFileName"></param>
        /// <param name="listItem"></param>
        /// <returns></returns>
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
                        spListItem.SystemUpdate();
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
        private bool ArchiveFiles(string filePath, string fileName, string archiveListPath, ref SPFile file)
        {
            SPFile retSPFile = null;
            bool archiveSuccess = false;
            if (!Page.Response.IsClientConnected)
            {
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

                        if (Page.Response.IsClientConnected)
                        {
                            SPFile spFile = folder.Files.Add(
                                fileName, zipFileStream);
                            retSPFile = spFile;
                            archiveSuccess = true;
                        }
                    }
                }
                catch (Exception error)
                {
                    _progressContext[ERROR_MESSAGE] = error.Message;
                    archiveSuccess = false;
                    PortalLog.LogString("[HPF]Exception Occurred: {0} || {1}",
                        error.Message, error.StackTrace);
                }
                finally
                {
                    if (zipFileStream != null) { zipFileStream.Close(); }
                }
            });
            file = retSPFile;
            return archiveSuccess;
        }

        /// <summary>
        /// Write metadata to file and delete it
        /// </summary>
        /// <param name="qualifiedFileName"></param>
        /// <param name="item"></param>
        private void WriteMetaData(string qualifiedFileName, SPListItem spListItem)
        {
            OleDocumentPropertiesClass document = new OleDocumentPropertiesClass();
            try
            {   
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
            }
            catch { }
            finally { document.Close(true); }
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
                /******************************************************/
                /***************Delete a batch of items****************/
                /******************************************************/
                List<KeyValuePair<int, string>> deletedIds = new List<KeyValuePair<int, string>>();
                foreach (SPListItem spListItem in items)
                {
                    deletedIds.Add(new KeyValuePair<int, string>(spListItem.ID, spListItem.File.ServerRelativeUrl));
                }

                SPContext.Current.Web.AllowUnsafeUpdates = true;
                int batchSize = DownloadAppSettings.DeleteBatchSize;
                StringBuilder sbDelete = new StringBuilder();
                sbDelete.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Batch>");
                string listguid = SPContext.Current.List.ID.ToString();
                int bcount = 0;

                for (int i = 0; i < deletedIds.Count; i++)
                {
                    if (bcount > batchSize)
                    {
                        sbDelete.Append("</Batch>");
                        try
                        {
                            if (!Page.Response.IsClientConnected) return;
                            string processMessage = SPContext.Current.Web.ProcessBatchData(sbDelete.ToString());
                            PortalLog.LogString("[HPF] Deleted {0} file(s)\n {1}", bcount, processMessage);
                        }
                        catch (Exception error)
                        {
                            PortalLog.LogString("[HPF] Exception Occurred: {0} || {1}",
                                error.Message, error.StackTrace);
                        }
                        Thread.Sleep(500);
                        updateProgressAction((double)20 * batchSize / total);
                        sbDelete = new StringBuilder();
                        sbDelete.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Batch>");
                        bcount = 0;

                    }
                    bcount++;
                    sbDelete.Append("<Method>");
                    sbDelete.Append("<SetList Scope=\"Request\">" + listguid + "</SetList>");
                    sbDelete.Append("<SetVar Name=\"ID\">" + deletedIds[i].Key.ToString() + "</SetVar>");
                    sbDelete.Append("<SetVar Name=\"Cmd\">Delete</SetVar>");
                    sbDelete.Append("<SetVar Name=\"owsfileref\">" + deletedIds[i].Value + "</SetVar>");
                    sbDelete.Append("</Method>");
                }

                sbDelete.Append("</Batch>");
                try
                {
                    if (!Page.Response.IsClientConnected) return;
                    string processMessage = SPContext.Current.Web.ProcessBatchData(sbDelete.ToString());
                    Thread.Sleep(500);
                    updateProgressAction((double)20 / total);
                    PortalLog.LogString("[HPF] Deleted {0} file(s)\n {1}", bcount, processMessage);
                }
                catch (Exception error) 
                {
                    PortalLog.LogString("[HPF] Exception Occurred: {0} || {1}", 
                        error.Message, error.StackTrace);
                }
                /******************************************************/

                /******************************************************/
                /*Get all items again and try delete one by one if any*/
                /******************************************************/                
                Guid listId = SPContext.Current.List.ID;
                SPList list = SPContext.Current.Web.Lists[listId];
                items = GetItemsOnCurrentViewAndFolder(list);

                if (items.Count > 0)
                {                    
                    PortalLog.LogString("[HPF] Remain {0} file(s)", items.Count);
                    List<int> ids = new List<int>();
                    foreach (SPListItem item in items) { ids.Add(item.ID); }
                    ids.ForEach(delegate(int id)
                    {
                        try
                        {
                            if (!Page.Response.IsClientConnected) return;
                            items.DeleteItemById(id);
                            updateProgressAction((double)10 / total);
                            Thread.Sleep(200);
                        }
                        catch (Exception error)
                        {
                            PortalLog.LogString("[HPF] Exception Occurred: {0} || {1}",
                                error.Message, error.StackTrace);
                        }
                    });
                }
                /*********************************************/
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

        /// <summary>
        /// Convert bytes to gigabytes for validating zipped file
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        static decimal ConvertToGigabytes(ulong bytes)
        {
            return ((decimal)bytes / 1024M / 1024M / 1024M);
        }
        #endregion

        #region ICallbackEventHandler Members

        public string GetCallbackResult()
        {
            string result = _progressContext.SerializeToJSonObject();
            //Remove Progress Context if Download done
            if (_progressContext[IN_PROGRESS] == "false")
            {
                _progressContext.RemoveProgressContext();
            }
            return result;
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            _progressContext = ProgressContext.GetCurrentProgressContext(eventArgument);            
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

        void Alert(string message)
        {
            string script = String.Format("alert('{0}');", message);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }
        #endregion
    }
}
