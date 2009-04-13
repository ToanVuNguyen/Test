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
        public static string HPF_INVOICE_EXPORT_FIS_HEADER_REPORT
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_INVOICE_EXPORT_FIS_HEADER_REPORT"];
            }

        }

        public static string HPF_INVOICE_EXPORT_FIS_DETAIL_REPORT
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_INVOICE_EXPORT_FIS_DETAIL_REPORT"];
            }

        }

        public static string HPF_INVOICE_EXPORT_HPFSTD_REPORT
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_INVOICE_EXPORT_HPFSTD_REPORT"];
            }

        }
        public static string HPF_INVOICE_EXPORT_HSBC_REPORT
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_INVOICE_EXPORT_HSBC_REPORT"];
            }

        }
        public static string HPF_INVOICE_EXPORT_ICLEAR_REPORT
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_INVOICE_EXPORT_ICLEAR_REPORT"];
            }

        }
        public static string HPF_INVOICE_EXPORT_NFMC_REPORT
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_INVOICE_EXPORT_NFMC_REPORT"];
            }

        }
        public static string HPF_INVOICE_EXPORT_HUD_REPORT
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_INVOICE_EXPORT_HUD_REPORT"];
            }

        }
        public static string HPF_INVOICE_EXPORT_INVOICE_SUMMARY_REPORT
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_INVOICE_EXPORT_INVOICE_SUMMARY_REPORT"];
            }

        }    
   
        public static string MapReportPath(string virtualPath)
        {
            return SHAREPOINT_REPORT_LIBRARY + virtualPath;
        }
        //report path
        public static string HPF_NEW_AGENCY_PAYABLE_REPORT
        {
            get {
                return ConfigurationManager.AppSettings["HPF_NEW_AGENCY_PAYABLE_REPORT"];
            }
        }
        public static string HPF_VIEW_EDIT_AGENCY_PAYABLE_REPORT
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_VIEW_EDIT_AGENCY_PAYABLE_REPORT"];
            }
        }
        public static string HPF_AGENCY_PAYABLE_REPORT
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_AGENCY_PAYABLE_REPORT"];
            }
        }
        public static string HPF_INVOICE_PORTAL_URL
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_INVOICE_PORTAL_URL"];
            }
        }
        public static string HPF_PAYABLE_PORTAL_URL
        {
            get {
                return ConfigurationManager.AppSettings["HPF_PAYABLE_PORTAL_URL"];
            }
        }

        public static string HPF_EXPORT_FORMATS
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_EXPORT_FORMATS"];
            }
        }
    }
}
