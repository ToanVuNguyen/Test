using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class ForeclosureCaseSetDTO
    {
        public ForeclosureCaseDTO ForeclosureCase { get; set; }
       
        public CaseLoanDTOCollection CaseLoans { get; set; }

        public BudgetSetDTO BudgetSet { get; set; }

        public BudgetItemDTOCollection BudgetItems { get; set; }

        public BudgetAssetDTOCollection BudgetAssets { get; set; }

        public OutcomeItemDTOCollection Outcome { get; set; }

        public ActivityLogDTOCollection ActivityLog { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR0125, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(false, 30, "Working User ID", Ruleset = Constant.RULESET_LENGTH)]
        public string WorkingUserID { get; set; }
    }
}
