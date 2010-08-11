using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class EvalTemplateDTO:BaseDTO
    {
        #region property
        public int? EvalTemplateId{get;set;}
        private string _templateName;
        public string TemplateName
        {
            get { return _templateName; }
            set { _templateName = string.IsNullOrEmpty(value) ? null : value; }
        }
        private string _templateDescription;
        public string TemplateDescription
        {
            get { return _templateDescription; }
            set { _templateDescription = string.IsNullOrEmpty(value) ? null : value; }
        }
        public int? TotalScore { get; set; }

        public EvalTemplateSectionDTOCollection EvalTemplateSections { get; set; }
        public EvalTemplateDTO()
        {
            EvalTemplateSections = new EvalTemplateSectionDTOCollection();
        }
        #endregion
    }
}
