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
    public class PrePurchaseCaseSetDTO
    {
        public PrePurchaseCaseDTO PrePurchaseCase { get; set; }
        [XmlIgnore]
        public PPBudgetSetDTO PPBudgetSet { get; set; }

        public PPBudgetItemDTOCollection PPBudgetItems { get; set; }

        public PPBudgetAssetDTOCollection PPBudgetAssets { get; set; }

        public PPPBudgetItemDTOCollection ProposedPPBudgetItems { get; set; }
        [XmlIgnore]
        public PPPBudgetSetDTO ProposedPPBudgetSet { get; set; }
        public ApplicantDTO Applicant { get; set; }
    }
}
