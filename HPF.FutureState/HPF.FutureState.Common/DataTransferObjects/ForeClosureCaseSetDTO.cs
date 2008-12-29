using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
