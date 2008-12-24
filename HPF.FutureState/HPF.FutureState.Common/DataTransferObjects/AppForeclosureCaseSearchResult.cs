using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class AppForeclosureCaseSearchResult:BaseDTOCollection<AppForeclosureCaseSearchResult>
    {
        public string CaseID { get; set; }
        public string AgencyCaseID { get; set; }
        public string Counseled { get; set; }
        public string CaseDate { get; set; }
        public string BorrowerFirstName { get; set; }
        public string BorrowerLastName { get; set; }
        public string Last4SSN { get; set; }
        public string CoborrowerFirstName { get; set; }
        public string CoborrowerLastName { get; set; }
        public string CoborrowerLast4SSN { get; set; }
        public string PropertyAddress { get; set; }
        public string PropertyCity { get; set; }
        public string PropertyState { get; set; }
        public string PropertyZip { get; set; }
        public string AgencyName { get; set; }
        public string AgentName { get; set; }
        public string AgentPhone { get; set; }
        public string AgentExtension { get; set; }
        public string AgentEmail { get; set; }
        public string CaseCompleteDate { get; set; }
        public string DaysDelinquent { get; set; }
        public string BankruptcyIndicator { get; set; }
        public string ForeclosureNoticeReceivedIndicator { get; set; }
        public string LoanList { get; set; }
    }
}
