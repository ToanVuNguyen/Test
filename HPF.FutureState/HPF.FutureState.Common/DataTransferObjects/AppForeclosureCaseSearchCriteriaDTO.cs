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
        [NullableOrInRangeValidator(true, "[a-zA-Z0-9]", MessageTemplate = "Agency Case Number: Only alpha-numeric characters allowed", Ruleset = "CriteriaValidation")]
        [NotNullValidator(Ruleset = "AppSearchRequireCriteria")]
        public string AgencyCaseID { get; set; }

        [NotNullValidator(Ruleset = "AppSearchRequireCriteria")]
        public string FirstName { get; set; }

        [NotNullValidator(Ruleset = "AppSearchRequireCriteria")]
        public string LastName { get; set; }

        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = "AppSearchRequireCriteria")]
        [NullableOrInRangeValidator(true, "[0-9]", MessageTemplate = "Foreclosure Case ID: Only numeric characters allowed", Ruleset = "CriteriaValidation")]
        public int ForeclosureCaseID { get; set; }

        [NotNullValidator(Ruleset = "AppSearchRequireCriteria")]
        [NullableOrInRangeValidator(true, "[a-zA-Z0-9]", MessageTemplate = "Loan Number: Only alpha-numeric characters allowed", Ruleset = "CriteriaValidation")]
        public string LoanNumber
        {
            get;
            set;

        }
        [NotNullValidator(Ruleset = "AppSearchRequireCriteria")]
        [NullableOrDigitsRequriedValidator(true, 5, MessageTemplate = "Property Zip: Only 5 numeric characters allowed", Ruleset = "CriteriaValidation")]
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
        [NullableOrDigitsRequriedValidator(true, 4, MessageTemplate = "Last4SSN: Only 4 numeric characters allowed", Ruleset = "CriteriaValidation")]
        public string Last4SSN { get; set; }

        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public int TotalRowNum { get; set; }
    }
}
