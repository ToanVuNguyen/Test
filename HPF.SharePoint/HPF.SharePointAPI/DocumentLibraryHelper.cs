using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.IO.Compression;

using Microsoft.SharePoint;
using HPF.SharePointAPI.Validators;
using ICSharpCode.SharpZipLib.Zip;

namespace HPF.SharePointAPI
{
    public static class DocumentLibraryHelper
    {
        #region "Upload Methods"
        /// <summary>
        /// Upload files to SharePoint folder
        /// </summary>
        /// <param name="files">A collection of file info</param>
        /// <param name="spFolder">Target SharePoint folder</param>
        /// <returns>A collection of SharePoint files which are uploaded</returns>
        public static IList<SPFile> UploadFiles(IList<UploadFileInfo> files, SPFolder spFolder)
        {
            //Validate arguments
            CommonValidator.ArgumentNotNull(files, "files");
            CommonValidator.ArgumentNotNull(spFolder, "spFolder");
            if (!spFolder.Exists)
            {
                string errorMessage = String.Format("{0} does not exist.", spFolder.Name);
                throw new SPException(errorMessage);
            }

            List<SPFile> spFiles = new List<SPFile>();
            string fileUrl;
            SPFile spFile;
            SPUser checkedOutBy;
            SPWeb web = spFolder.ParentWeb;
            SPDocumentLibrary currentList = (SPDocumentLibrary)web.Lists[spFolder.ContainingDocumentLibrary];

            foreach (UploadFileInfo file in files)
            {
                //check if this file exists
                fileUrl = String.Format("{0}/{1}", spFolder.ServerRelativeUrl, file.FileName);
                spFile = web.GetFile(fileUrl);
                if (spFile == null || !spFile.Exists)
                {
                    //if not exists, this file will be added to spFolder
                    spFile = spFolder.Files.Add(file.FileName, file.InputStream, file.OverwriteIfExists, file.CheckInComment, true);
                    spFiles.Add(spFile);
                    continue;
                }

                //if exists and not allow to overwrite, throws exception
                if (!file.OverwriteIfExists)
                {
                    throw new SPException(SPResource.GetString("FileAlreadyExistsError", new object[] { spFile.Name, spFile.ModifiedBy, spFile.TimeLastModified }));
                }

                //if exists and allow overwrite
                if (spFile != null)
                {
                    //try to check out
                    checkedOutBy = spFile.CheckedOutBy;
                    if (checkedOutBy == null)
                    {
                        if (currentList.ForceCheckout)
                            spFile.CheckOut();
                    }
                    else if(checkedOutBy.ID != web.CurrentUser.ID)
                    {
                        //user don't have permission on this file
                        throw new SPException(SPResource.GetString("FileAlreadyCheckedOutError", new object[] { spFile.Name, checkedOutBy, spFile.CheckedOutDate }));
                    }

                    //try to save file and check in
                    try
                    {
                        spFile.SaveBinary(file.InputStream, true);
                        spFile.CheckIn(file.CheckInComment);
                    }
                    catch (Exception error)
                    {
                        spFile.UndoCheckOut();
                        throw error;
                    }

                    spFiles.Add(spFile);
                }
            }
            return spFiles;
        }

        /// <summary>
        /// Upload files to SharePoint folder based on folder's url
        /// </summary>
        /// <param name="files">A collection of file info</param>
        /// <param name="spFolder">Target url of SharePoint folder</param>
        /// <returns>A collection of SharePoint files which are uploaded</returns>
        public static IList<SPFile> UploadFiles(IList<UploadFileInfo> files, string folderUrl)
        {
            SPFolder spFolder = SPContext.Current.Web.GetFolder(folderUrl);
            return UploadFiles(files, spFolder);
        }

        /// <summary>
        /// Upload files and create SharePoint folder from physical location on server
        /// </summary>
        /// <param name="physicalFolderPath">Physical folder path</param>
        /// <param name="spFolder">Target SharePoint folder</param>
        /// <returns>A collection of SharePoint files which are uploaded</returns>
        public static IList<SPFile> UploadFiles(string physicalFolderPath, SPFolder spFolder)
        {
            List<SPFile> uploadedFiles = new List<SPFile>();
            //check if physical folder exists
            if (!Directory.Exists(physicalFolderPath))
            {
                throw new IOException(String.Format("{0} does not exist.", physicalFolderPath));
            }

            //upload files in current folder
            UploadFileInfo fileInfo = null;
            List<UploadFileInfo> fileInfoList = new List<UploadFileInfo>();
            foreach (string fileName in Directory.GetFiles(physicalFolderPath))
            {
                fileInfo = new UploadFileInfo(fileName, "", true);
                fileInfoList.Add(fileInfo);
            }

            uploadedFiles.AddRange(UploadFiles(fileInfoList, spFolder));

            //recursive loop into subfolders and upload files
            foreach (string folder in Directory.GetDirectories(physicalFolderPath))
            {
                //create SPFolder
                SPFolder createdSPFolder = spFolder.SubFolders.Add(folder);
                uploadedFiles.AddRange(UploadFiles(folder, createdSPFolder));
            }
            return uploadedFiles;
        }

        /// <summary>
        /// Upload files and create SharePoint folder from physical location on server
        /// </summary>
        /// <param name="physicalFolderPath">Physical folder path</param>
        /// <param name="spFolder">Target url of SharePoint folder</param>
        /// <returns>A collection of SharePoint files which are uploaded</returns>
        public static IList<SPFile> UploadFiles(string physicalFolderPath, string folderUrl)
        {
            SPFolder spFolder = SPContext.Current.Web.GetFolder(folderUrl);
            return UploadFiles(physicalFolderPath, spFolder);
        }
        #endregion

        #region "Zip Helper"        
        public static SPFile CompressSPFiles(IList<SPListItem> spListItems, SPDocumentLibrary spDocLib)
        {
            string tempPath = Path.GetTempPath();
            string randomFileName = Path.GetRandomFileName();
            string path = tempPath + randomFileName + @"\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (SPListItem item in spListItems)
            {
                SPFile file = item.File;
                if (file != null)
                {
                    byte[] bytFile = file.OpenBinary();                    
                    string qualifiedFileName = path + file.Name;
                    FlushBufferToFile(bytFile, qualifiedFileName);                    
                }
            }
            string outputPathAndFile = randomFileName + ".zip";

            ZipFolder(path, outputPathAndFile);

            FileStream zipFileStream = null;
            SPFile returnSPFile = null;
            try
            {
                zipFileStream = new FileStream(path + @"\" + outputPathAndFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                returnSPFile = spDocLib.RootFolder.Files.Add(spDocLib.RootFolder.ServerRelativeUrl + @"\" + outputPathAndFile, zipFileStream);

            }
            catch { }
            finally
            {
                if (zipFileStream != null) { zipFileStream.Close(); }
                //File.Delete(tempPath + randomFileName);
            }

            return returnSPFile;

        }
        
        private static bool FlushBufferToFile(byte[] bytFile, string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            stream.Write(bytFile, 0, bytFile.Length);
            stream.Close();
            return true;
        }

        private static void ZipFolder(string inputFolderPath, string outputPathAndFile)
        {
            IList<string> list = GetFileNameInFolder(inputFolderPath);
            int count = Directory.GetParent(inputFolderPath).ToString().Length + 1;
            ZipOutputStream zipStream = new ZipOutputStream(File.Create(inputFolderPath + @"\" + outputPathAndFile));
            
            zipStream.SetLevel(9);
            foreach (string name in list)
            {
                ZipEntry entry = new ZipEntry(name.Remove(0, count));
                zipStream.PutNextEntry(entry);
                if (!name.EndsWith("/"))
                {
                    FileStream stream = File.OpenRead(name);
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    zipStream.Write(buffer, 0, buffer.Length);
                }
            }
            zipStream.Finish();
            zipStream.Close();
        }        

        private static IList<string> GetFileNameInFolder(string dir)
        {
            List<string> list = new List<string>();
            bool flag = true;
            foreach (string str in Directory.GetFiles(dir))
            {
                list.Add(str);
                flag = false;
            }
            if (flag && (Directory.GetDirectories(dir).Length == 0))
            {
                list.Add(dir + "/");
            }
            foreach (string folder in Directory.GetDirectories(dir))
            {
                foreach (string item in GetFileNameInFolder(folder))
                {
                    list.Add(item);
                }
            }
            return list;
        }
        #endregion
    }
}
