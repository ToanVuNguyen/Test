using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class ForeclosureCaseWSDTO : BaseDTO
    {
        
        public int FcId { get; set; }
        public DateTime IntakeDt { get; set; }
        public string BorrowerFname { get; set; }
        public string BorrowerLname { get; set; }
        public string BorrowerLast4SSN { get; set; }
        public string CoBorrowerFname { get; set; }
        public string CoBorrowerLname { get; set; }
        public string CoBorrowerLast4SSN { get; set; }
        public string PropAddr1 { get; set; }
        public string PropAddr2 { get; set; }
        public string PropCity { get; set; }
        public string PropStateCd { get; set; }
		public string PropZip { get; set; }		
		public string CounselorFullName { get; set; }
        public string CounselorPhone { get; set; }
        public string CounselorExt { get; set; }
        public string CounselorEmail { get; set; }
		public DateTime CompletedDt { get; set; }
		//-- ref_code_itemcode_desc as delinquent_dt { get; set; } -- table ref_code_item
        public string BankruptcyInd { get; set; }
        public string FcNoticeReceivedInd { get; set; }
        public string AgencyCaseNum { get; set; }

        public string AgencyName { get; set; }
        public string Counseled { get; set; }
        public DateTime DelinquentDt { get; set; }
        public string LoanNumber { get; set; }
        public string LoanServicer { get; set; }
        public string CaseLoanID {get; set;}


    }
}
