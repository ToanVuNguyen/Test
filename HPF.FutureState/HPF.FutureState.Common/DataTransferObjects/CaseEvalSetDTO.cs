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
        public int? TotalPossibleScore { get; set; }

        private string _resultLevel;
        public string ResultLevel
        {
            get { return _resultLevel; }
            set { _resultLevel = string.IsNullOrEmpty(value) ? null : value; }
        }

        public bool? FatalErrorInd { get; set; }
        public bool? HpfAuditInd { get; set; }

        private string _comments;
        public string Comments
        {
            get { return _comments; }
            set { _comments = string.IsNullOrEmpty(value) ? null : value; }
        }
        public EvalSectionCollectionDTO EvalSections { get; set; }

        public CaseEvalSetDTO()
        {
            EvalSections = new EvalSectionCollectionDTO();
        }
    }
}
