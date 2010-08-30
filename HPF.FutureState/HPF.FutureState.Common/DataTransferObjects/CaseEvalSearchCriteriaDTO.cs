using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CaseEvalSearchCriteriaDTO:BaseDTO
    {
        public int? AgencyId { get; set; }
        public string YearMonthFrom { get; set; }
        public string YearMonthTo { get; set; }
        public string EvaluationStatus { get; set; }
        public string EvaluationType { get; set; }
    }
}
