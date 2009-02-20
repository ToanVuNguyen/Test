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
        //[NullableOrInRangeValidator(true, "[a-zA-Z0-9]", MessageTemplate = "Agency Case Number: Only alpha-numeric characters allowed", Ruleset = Constant.RULESET_CRITERIAVALID)]
        [NotNullValidator(Ruleset = Constant.RULESET_APPSEARCH)]
        public string AgencyCaseID { get; set; }

        [NotNullValidator(Ruleset = Constant.RULESET_APPSEARCH)]
        public string FirstName { get; set; }

        [NotNullValidator(Ruleset = Constant.RULESET_APPSEARCH)]
        public string LastName { get; set; }

        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_APPSEARCH)]
        [NullableOrInRangeValidator(true, "[0-9]", MessageTemplate = ErrorMessages.ERR0503, Ruleset = Constant.RULESET_CRITERIAVALID)]
        public int ForeclosureCaseID { get; set; }

        [NotNullValidator(Ruleset = Constant.RULESET_APPSEARCH)]
        //[NullableOrInRangeValidator(true, "[a-zA-Z0-9]", MessageTemplate = "Loan Number: Only alpha-numeric characters allowed", Ruleset = Constant.RULESET_CRITERIAVALID)]
        public string LoanNumber
        {
            get;
            set;

        }
        [NotNullValidator(Ruleset = Constant.RULESET_APPSEARCH)]
        [NullableOrDigitsRequriedValidator(true, 5, "Property Zip", Ruleset = Constant.RULESET_CRITERIAVALID)]
        public string PropertyZip { get; set; }

        [NotNullValidator(Ruleset = Constant.RULESET_APPSEARCH)]
        public string PropertyState { get; set; }

        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_APPSEARCH)]
        public int Agency { get; set; }

        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_APPSEARCH)]
        public int Program { get; set; }

        [NotNullValidator(Ruleset = Constant.RULESET_APPSEARCH)]
        public string Duplicates { get; set; }

        [NotNullValidator(Ruleset = Constant.RULESET_APPSEARCH)]
        [NullableOrDigitsRequriedValidator(true, 4, "Last 4 SSN", Ruleset = Constant.RULESET_CRITERIAVALID)]
        public string Last4SSN { get; set; }

        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_APPSEARCH)]
        public int Servicer { get; set; }

        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public int TotalRowNum { get; set; }
    }
}
