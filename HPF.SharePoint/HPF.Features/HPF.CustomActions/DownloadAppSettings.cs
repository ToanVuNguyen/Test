using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace HPF.CustomActions
{
    public static class DownloadAppSettings
    {
        public const string ReviewStatusFieldKey = "ReviewStatusField";
        public const string ReviewStatusDownloadValueKey = "ReviewStatusDownloadValue";
        public const string ArchiveListNameKey = "ArchiveListName";
        public const string RenderForDocumentLibraryKey = "RenderForDocumentLibrary";
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
    }
}
