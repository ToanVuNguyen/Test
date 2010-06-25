using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class CallingDataHeaderDTO : BaseDTO
    {
        public const int FILE_LENGTH_LEN = 9;
        public const int SUBSCRIBER_ID_LEN = 16;
        public const int SUBACCOUNT_NAME_LEN = 8;
        public const int LONGIN_ID_LEN = 8;
        public const int NUM_ASSIGNED_SERVICE_TYPE_LEN = 2;
        public const int SERVICER_TYPE_LEN = 4;
        public const int RESERVED_LEN = 3;
        public const int FILE_CREATION_DATE_LEN = 14;
        public const int DATE_LEN = 8;
        public const int TIME_LEN = 5;        
        public const int CALL_RECORD_COUNT_LEN = 6;
        public const int CUSTOMER_PROVIDED_HEADER_LEN = 20;
        public const int DOWNLOAD_TYPE_LEN = 8;
        public const int REPORT_FORM_LEN = 8;

        public float FileLength { get; set; }
        public string SubscriberId { get; set; }
        public string SubaccountName { get; set; }
        public string LoginId { get; set; }
        public string NumAssignedServiceType { get; set; }
        public string ServiceType { get; set; }
        public string Reserved { get; set; }
        public DateTime FileCreationDt { get; set; }
        public DateTime StartDt { get; set; }
        public DateTime EndDt { get; set; }
        public int CallRecordCount { get; set; }
        public string CustomerProvidedHeader { get; set; }
        public string DownloadType { get; set; }
        public string ReportForm { get; set; }

        public static int Length
        {
            get
            {
                return  FILE_LENGTH_LEN +
                        SUBSCRIBER_ID_LEN +
                        SUBACCOUNT_NAME_LEN +
                        LONGIN_ID_LEN +
                        NUM_ASSIGNED_SERVICE_TYPE_LEN +
                        SERVICER_TYPE_LEN +
                        RESERVED_LEN +
                        FILE_CREATION_DATE_LEN +
                        DATE_LEN +
                        TIME_LEN +
                        DATE_LEN +
                        TIME_LEN +
                        CALL_RECORD_COUNT_LEN +
                        CUSTOMER_PROVIDED_HEADER_LEN +
                        DOWNLOAD_TYPE_LEN +
                        REPORT_FORM_LEN; 
            }
        }
    }
}
