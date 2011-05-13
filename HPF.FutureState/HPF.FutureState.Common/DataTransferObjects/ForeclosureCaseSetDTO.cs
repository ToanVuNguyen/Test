using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using HPF.FutureState.Common.Utils.DataValidator;
using System.Xml.Serialization;
namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class ForeclosureCaseSetDTO
    {
        public ForeclosureCaseDTO ForeclosureCase { get; set; }
       
        public CaseLoanDTOCollection CaseLoans { get; set; }

        [XmlIgnore]
        public BudgetSetDTO BudgetSet { get; set; }

        public BudgetItemDTOCollection BudgetItems { get; set; }

        public BudgetAssetDTOCollection BudgetAssets { get; set; }

        public OutcomeItemDTOCollection Outcome { get; set; }

        public CreditReportDTOCollection CreditReport { get; set; }

        [XmlIgnore]
        public ActivityLogDTOCollection ActivityLog { get; set; }

        public BudgetItemDTOCollection ProposedBudgetItems { get; set; }
        [XmlIgnore]
        public BudgetSetDTO ProposedBudgetSet { get; set; }                
    }
}
