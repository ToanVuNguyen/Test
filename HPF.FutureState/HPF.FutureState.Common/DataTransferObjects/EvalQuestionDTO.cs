using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class EvalQuestionDTO:BaseDTO
    {
        #region property
        public int? EvalQuestionId { get; set; }

        private string _question;
        public string Question
        {
            get { return _question; }
            set { _question = string.IsNullOrEmpty(value) ? null : value; }
        }

        private string _questionDescription;
        public string QuestionDescription
        {
            get { return _questionDescription; }
            set { _questionDescription = string.IsNullOrEmpty(value) ? null : value; }
        }

        private string _questionExample;
        public string QuestionExample
        {
            get { return _questionExample; }
            set { _questionExample = string.IsNullOrEmpty(value) ? null : value; }
        }

        private string _questionType;
        public string QuestionType
        {
            get { return _questionType; }
            set { _questionType = string.IsNullOrEmpty(value) ? null : value; }
        }
        private string _activeInd;
        public string ActiveInd
        {
            get { return _activeInd; }
            set { _activeInd = string.IsNullOrEmpty(value) ? null : value; }
        }
        public int? QuestionScore;
        #endregion
    }
}
