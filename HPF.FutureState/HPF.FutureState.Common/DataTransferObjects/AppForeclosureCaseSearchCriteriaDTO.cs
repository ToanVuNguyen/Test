using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class AppForeclosureCaseSearchCriteriaDTO
    {
        [NotNullValidator(Ruleset = "AppSearchRequireCriteria")]
        public string AgencyCaseID { get; set; }

        [NotNullValidator(Ruleset = "AppSearchRequireCriteria")]
        public string FirstName { get; set; }

        [NotNullValidator(Ruleset = "AppSearchRequireCriteria")]
        public string LastName { get; set; }

        


        

        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = "AppSearchRequireCriteria")]
        public int ForeclosureCaseID { get; set; }

        [NotNullValidator(Ruleset = "AppSearchRequireCriteria")]
        public string LoanNumber
        {
            get;
            set;

        }

        [NotNullValidator(Ruleset = "AppSearchRequireCriteria")]
        public string PropertyZip { get; set; }

        [NotNullValidator(Ruleset = "AppSearchRequireCriteria")]
        public string PropertyState { get; set; }

        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = "AppSearchRequireCriteria")]
        public int Agency { get; set; }

        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = "AppSearchRequireCriteria")]
        public int Program { get; set; }

        [NotNullValidator(Ruleset = "AppSearchRequireCriteria")]
        public string Duplicates { get; set; }

        [NotNullValidator(Ruleset = "AppSearchRequireCriteria")]
        public string Last4SSN { get; set; }
    }
}
