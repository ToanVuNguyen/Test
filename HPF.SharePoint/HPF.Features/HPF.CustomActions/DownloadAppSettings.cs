using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace HPF.CustomActions
{
    public static class DownloadAppSettings
    {
        #region "const for settings in web.config file"
        private const string ReviewStatusFieldKey = "ReviewStatusField";
        private const string ReviewStatusDownloadValueKey = "ReviewStatusDownloadValue";
        private const string ArchiveListNameKey = "ArchiveListName";
        private const string RenderForDocumentLibraryKey = "RenderForDocumentLibrary";
        private const string TotalFilesAllowKey = "TotalFilesAllow";
        #endregion

        #region "const for progress bar"
        public const string UPDATE_META_DATA = "Updating Meta Data";
        public const string ZIPPING_FILES = "Zipping files";
        public const string ARCHIVING_ZIPPED_FILE = "Archiving zipped file";
        public const string CLEANING_UP_FILES = "Cleaning up files";
        #endregion

        #region "const for general settings"
        public const int DeleteBatchSize = 1000;
        #endregion

        #region "settings in web.config file"
        public static string ReviewStatusField
        {
            get
            {
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings[ReviewStatusFieldKey]))
                {
                    return ConfigurationManager.AppSettings[ReviewStatusFieldKey];
                }
                return "Review Status";
            }
        }

        public static string ReviewStatusDownloadValue
        {
            get
            {
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings[ReviewStatusDownloadValueKey]))
                {
                    return ConfigurationManager.AppSettings[ReviewStatusDownloadValueKey];
                }
                return "Processed";
            }
        }

        public static string ArchiveListName
        {
            get
            {
                if(!String.IsNullOrEmpty(ConfigurationManager.AppSettings[ArchiveListNameKey]))
                {
                    return ConfigurationManager.AppSettings[ArchiveListNameKey];
                }
                return "{0} Archive";
            }
        }

        public static IList<string> RenderForDocumentLibrary
        {
            get
            {
                List<string> documentLibraryNames = new List<string>();
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings[RenderForDocumentLibraryKey]))
                {
                    documentLibraryNames.AddRange(ConfigurationManager.AppSettings[RenderForDocumentLibraryKey].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                }
                return documentLibraryNames;
            }
        }

        public static int TotalFilesAllow
        {
            get
            {
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings[TotalFilesAllowKey]))
                {
                    return int.Parse(ConfigurationManager.AppSettings[TotalFilesAllowKey]);
                }
                return -1;
            }
        }
        #endregion

        #region "errors"
        public const string SIZE_EXCEED_2G = "Can not archive zipped file because the size of the file exceed 2 GB";
        public const string TOTAL_FILES_EXCEED = "Can not perform this action because the total files on current view exceed {0}";
        #endregion
    }
}
