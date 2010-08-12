using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CaseEvalHeaderDTO:BaseDTO
    {
        public int? CaseEvalHeaderId { get; set; }
        public int? FcId { get; set; }
        public int? AgencyId { get; set; }
        public int? EvalTemplateId { get; set; }

        public string EvaluationYearMonth { get; set; }

        private string _evalType;
        public string EvalType
        {
            get { return _evalType; }
            set { _evalType = string.IsNullOrEmpty(value) ? null : value; }
        }

        private string _evalStatus;
        public string EvalStatus
        {
            get { return _evalStatus; }
            set { _evalStatus = string.IsNullOrEmpty(value) ? null : value; }
        }
        public CaseEvalSetDTOCollection CaseEvalSets { get; set; }
        #region Information of ForeclosureCase
        public DateTime? CallDate { get; set; }
        public string AgencyName { get; set; }
        public string ZipCode { get; set; }
        public string CounselorName { get; set; }
        public string LoanNumber { get; set; }
        public string HomeowenerFirstName { get; set; }
        public string HomeownerLastName { get; set; }
        #endregion
        public CaseEvalHeaderDTO()
        {
            CaseEvalSets = new CaseEvalSetDTOCollection();
        }
    }
}
