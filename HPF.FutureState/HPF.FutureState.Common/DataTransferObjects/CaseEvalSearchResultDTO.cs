using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class CaseEvalSearchResultDTO:BaseDTO
    {
        public CaseEvalSetDTOCollection CaseEvalSets { get; set; }
        #region Information of ForeclosureCase
        public DateTime? CallDate { get; set; }
        public string AgencyName { get; set; }
        public string ZipCode { get; set; }
        public string CounselorName { get; set; }
        public string LoanNumber { get; set; }
        public string HomeowenerFirstName { get; set; }
        public string HomeownerLastName { get; set; }
        public string ServicerName { get; set; }
        #endregion
        public CaseEvalSearchResultDTO()
        {
            CaseEvalSets = new CaseEvalSetDTOCollection();
        }
    }
}
