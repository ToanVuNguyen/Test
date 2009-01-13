﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class OutcomeItemDTO : BaseDTO
    {
        private const string RULE_SET_LENGTH = "Length";

        public int OutcomeItemId { get; set; }

        public int FcId { get; set; }  

        public int OutcomeSetId { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public int OutcomeTypeId { get; set; }  

        public DateTime OutcomeDt { get; set; }  

        public DateTime OutcomeDeletedDt { get; set; }

        [NullableOrStringLengthValidator(true, 10, Ruleset = RULE_SET_LENGTH)]
        public string NonprofitreferralKeyNum { get; set; }

        [NullableOrStringLengthValidator(true, 50, Ruleset = RULE_SET_LENGTH)]
        public string ExtRefOtherName { get; set; }  
    }
}
