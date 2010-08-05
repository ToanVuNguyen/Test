using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CaseEvalSetDTO:BaseDTO
    {
        public int? CaseEvalSetId { get; set; }
        public int? CaseEvalHeaderId { get; set; }
        public DateTime? EvaluationDt { get; set; }

        private string _auditorName;
        public string AuditorName
        {
            get { return _auditorName; }
            set { _auditorName = string.IsNullOrEmpty(value) ? null : value; }
        }

        public int? TotalAuditScore { get; set; }

        private string _resultLevel;
        public string ResultLevel
        {
            get { return _resultLevel; }
            set { _resultLevel = string.IsNullOrEmpty(value) ? null : value; }
        }

        private string _fatalErrorInd;
        public string FatalErrorInd
        {
            get { return _fatalErrorInd; }
            set { _fatalErrorInd = string.IsNullOrEmpty(value) ? null : value.ToUpper(); }
        }

        private string _hpfAuditInd;
        public string HpfAuditInd
        {
            get { return _hpfAuditInd; }
            set { _hpfAuditInd = string.IsNullOrEmpty(value) ? null : value.ToUpper(); }
        }

        public CaseEvalDetailDTOCollection CaseEvalDetails { get; set; }
    }
}
