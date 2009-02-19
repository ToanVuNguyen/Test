using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common
{
    public static class HPFConfigurationSettting
    {
        //
        public static string SHAREPOINT_REPORT_LIBRARY
        {
            get
            {
                return ConfigurationManager.AppSettings["SHAREPOINT_REPORT_LIBRARY"];
            }
        }

        public static string REPORTSERVER_URL
        {
            get
            {
                return ConfigurationManager.AppSettings["REPORTSERVER_URL"];
            }
        }

        public static string REPORTSERVER_LOGINNAME
        {
            get
            {
                return ConfigurationManager.AppSettings["REPORTSERVER_LOGINNAME"];
            }
        }

        public static string REPORTSERVER_PASSWORD
        {
            get
            {
                return ConfigurationManager.AppSettings["REPORTSERVER_PASSWORD"];
            }
        }     

        
    }
}
