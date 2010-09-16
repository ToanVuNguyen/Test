using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class CaseEvalSearchResultDTO:BaseDTO
    {
        public CaseEvalSetDTOCollection CaseEvalSets { get; set; }
        public int? CaseEvalHeaderId { get; set; }
        public string EvalStatus { get; set; }
        public string EvalType { get; set; }
        public string EvaluationYearMonth { get; set; }
        #region Information of ForeclosureCase
        public int? FcId { get; set; }
        public string AgencyCaseNum { get; set; }
        public string AgencyName { get; set; }
        public string CounselorName { get; set; }
        public string HomeowenerFirstName { get; set; }
        public string HomeowenerLastName { get; set; }
        public string ZipCode { get; set; }
        public string LoanNumber { get; set; }
        public string ServicerName { get; set; }
        public DateTime? CallDate { get; set; }
        #endregion
        public CaseEvalSearchResultDTO()
        {
            CaseEvalSets = new CaseEvalSetDTOCollection();
        }
    }
}
