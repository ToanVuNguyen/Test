using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CaseEvalDetailDTO:BaseDTO
    {
        public int? CaseEvalDetailId { get; set; }
        public int? CaseEvalSetId { get; set; }

        public int? EvalSectionId { get; set; }
        private string _sectionName;
        public string SectionName
        {
            get { return _sectionName; }
            set { _sectionName = string.IsNullOrEmpty(value) ? null : value; }
        }
        public int? SectionOrder { get; set; }

        public int? EvalQuestionId { get; set; }
        private string _evalQuestion;
        public string EvalQuestion
        {
            get { return _evalQuestion; }
            set { _evalQuestion = string.IsNullOrEmpty(value) ? null : value; }
        }
        public int? QuestionOrder { get; set; }

        private string _evalAnswer;
        public string EvalAnswer
        {
            get { return _evalAnswer; }
            set { _evalAnswer = string.IsNullOrEmpty(value) ? null : value; }
        }

        public int? QuestionScore { get; set; }
        public int? AuditScore { get; set; }

        private string _comments;
        public string Comments
        {
            get { return _comments; }
            set { _comments = string.IsNullOrEmpty(value) ? null : value; }
        }
    }
}
