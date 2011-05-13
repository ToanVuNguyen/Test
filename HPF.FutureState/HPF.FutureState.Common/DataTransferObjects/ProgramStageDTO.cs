using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using HPF.FutureState.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class ProgramStageDTO:BaseDTO
    {
        public int? ProgramStageId { get; set; }
        public int? ProgramId { get; set; }

        [NullableOrStringLengthValidator(true, 50, "Stage Name", Ruleset = Constant.RULESET_LENGTH)]
        public string StageName { get; set; }

        [NullableOrStringLengthValidator(true, 300, "Stage Comment", Ruleset = Constant.RULESET_LENGTH)]
        public string StageComment { get; set; }

        [NullableOrStringLengthValidator(true, 4, "Stage Desciption", Ruleset = Constant.RULESET_LENGTH)]
        public string StageDesc { get; set; }

        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "StartDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? StartDt { get; set; }
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "EndDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? EndDt { get; set; }
    }
}
