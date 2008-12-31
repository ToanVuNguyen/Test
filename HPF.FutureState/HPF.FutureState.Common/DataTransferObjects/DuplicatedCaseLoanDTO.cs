using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class DuplicatedCaseLoanDTO:BaseDTO
    {
        public string LoanNumber { get; set; }
        public int AgencyID { get; set; }
        public int FcID { get; set; }
        public int ServicerID { get; set; }
        public string ServicerName { get; set; }
        public string BorrowerFirstName { get; set; }
        public string BorrowerLastName { get; set; }
        public string AgencyName { get; set; }
        public string AgencyCaseNumber { get; set; }
        public string CounselorFullName { get; set; }
        public string CounselorPhone { get; set; }
        public string CounselorEmail { get; set; }
    }
}
