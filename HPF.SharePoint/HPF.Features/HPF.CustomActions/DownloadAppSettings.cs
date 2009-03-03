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
        public static string ReviewStatusField
        {
            get
            {
                //return ConfigurationManager.AppSettings[ReviewStatusFieldKey];
                return "Review Status";
            }
        }

        public static string ReviewStatusDownloadValue
        {
            get
            {
                //return ConfigurationManager.AppSettings[ReviewStatusDownloadValueKey];
                return "Processed";
            }
        }
    }
}
