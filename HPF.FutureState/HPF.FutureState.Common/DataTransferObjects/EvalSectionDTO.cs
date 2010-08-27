using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class EvalSectionDTO:BaseDTO
    {
        #region property
        public int? EvalSectionId { get; set; }

        private string _sectionName;
        //[NullableOrStringLengthValidator(false, 50, "SectionName", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR1107)]
        public string SectionName
        {
            get { return _sectionName; }
            set { _sectionName = string.IsNullOrEmpty(value) ? null : value; }
        }

        private string _sectionDescription;
        //[NullableOrStringLengthValidator(true, 50, "SectionDescription", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR1108)]
        public string SectionDescription
        {
            get { return _sectionDescription; }
            set { _sectionDescription = string.IsNullOrEmpty(value) ? null : value; }
        }
        private string _activeInd;
        public string ActiveInd
        {
            get { return _activeInd; }
            set { _activeInd = string.IsNullOrEmpty(value) ? null : value; }
        }
        public EvalSectionQuestionDTOCollection EvalSectionQuestions { get; set; }
        public EvalSectionDTO()
        {
            EvalSectionQuestions = new EvalSectionQuestionDTOCollection();
        }
        public bool IsInUse { get; set; }
        #endregion
    }
}
