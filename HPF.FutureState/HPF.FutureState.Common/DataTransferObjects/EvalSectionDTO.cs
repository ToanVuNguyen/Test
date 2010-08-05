using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class EvalSectionDTO:BaseDTO
    {
        #region property
        public int? EvalSectionId { get; set; }

        private string _sectionName;
        public string SectionName
        {
            get { return _sectionName; }
            set { _sectionName = string.IsNullOrEmpty(value) ? null : value; }
        }

        private string _sectionDescription;
        public string SectionDescription
        {
            get { return _sectionDescription; }
            set { _sectionDescription = string.IsNullOrEmpty(value) ? null : value; }
        }

        public EvalSectionQuestionDTOCollection EvalSectionQuestions { get; set; }
        #endregion
    }
}
