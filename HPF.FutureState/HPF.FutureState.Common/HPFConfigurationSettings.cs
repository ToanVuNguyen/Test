using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common
{
    public static class HPFConfigurationSettings
    {
        public static string HPF_DB_CONNECTION_STRING
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString;
            }
        }

        

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

        public static string HPF_APPLICATION_NAME
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_APPLICATION_NAME"];
            }

        }

        public static string HPF_SUPPORT_EMAIL
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_SUPPORT_EMAIL"];
            }

        }

        public static string APP_FORECLOSURECASE_PAGE_SIZE
        {
            get
            {
                return ConfigurationManager.AppSettings["APP_FORECLOSURECASE_PAGE_SIZE"];
            }

        }

        public static string TEMP_DIRECTORY
        {
            get
            {
                return ConfigurationManager.AppSettings["TEMP_DIRECTORY"];
            }

        }

        public static string HPF_VERSION
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_VERSION"];
            }

        }

        public static string WS_SEARCH_RESULT_MAXROW
        {
            get
            {
                return ConfigurationManager.AppSettings["WS_SEARCH_RESULT_MAXROW"];
            }

        }

        public static string HPF_COUNSELINGSUMMARY_REPORT
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_COUNSELINGSUMMARY_REPORT"];
            }

        }        
   
        public static string MapReportPath(string virtualPath)
        {
            return SHAREPOINT_REPORT_LIBRARY + virtualPath;
        }
    }
}
