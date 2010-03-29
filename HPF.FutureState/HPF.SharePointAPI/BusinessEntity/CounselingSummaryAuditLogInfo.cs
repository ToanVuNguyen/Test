using System;
using System.Collections.Generic;
using System.Text;

namespace HPF.SharePointAPI.BusinessEntity
{
    public class CounselingSummaryAuditLogInfo: BaseObject
    {
        public string CounselingSummaryFile { get; set; }
        public DateTime? OccurredDate { get; set; }
        public string Servicer { get; set; }
        public string LoanNumber { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? ItemCreatedDate { get; set; }
        public string UserId { get; set; }
        public string ArchiveName { get; set; }
    }
}
