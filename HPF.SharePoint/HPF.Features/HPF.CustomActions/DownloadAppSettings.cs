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
    }
}
