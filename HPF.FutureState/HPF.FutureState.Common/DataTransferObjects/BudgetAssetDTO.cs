using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BudgetAssetDTO : BaseDTO
    {
        [XmlIgnore]
        public int? BudgetAssetId { get; set; }

        [XmlIgnore]
        public int? BudgetSetId { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Asset Name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0071)]
        public string AssetName { get; set; }

        [XmlElement(IsNullable = true)]
        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH,MessageTemplate = "AssetValue must be numeric(15,2)")]
        public double? AssetValue { get; set; }          
    }
}
