﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class PPBudgetSetDTO : BaseDTO
    {
        [XmlIgnore]
        public int? PPBudgetSetId { get; set; }

        [XmlIgnore]
        public int? PPCaseId { get; set; }

        [XmlIgnore]
        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH)]
        public double? TotalIncome { get; set; }

        [XmlIgnore]
        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH)]
        public double? TotalExpenses { get; set; }

        [XmlIgnore]
        [RangeValidator(-9999999999999.99, RangeBoundaryType.Inclusive, 9999999999999.99, RangeBoundaryType.Inclusive, Ruleset = Constant.RULESET_LENGTH)]
        public double? TotalAssets { get; set; }

        [XmlIgnore]
        public DateTime? PPBudgetSetDt { get; set; }

        [XmlIgnore]
        public double? TotalSurplus { get; set; }
    }
}
