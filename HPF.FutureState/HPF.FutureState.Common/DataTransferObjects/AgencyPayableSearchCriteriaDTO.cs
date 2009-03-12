using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using HPF.FutureState.Common.Utils.DataValidator;
namespace HPF.FutureState.Common.DataTransferObjects
{
    public enum CustomBoolean { None = -1, Y = 0, N = 1 }        

    [Serializable]
    public class AgencyPayableSearchCriteriaDTO
    {        
        public int AgencyId { get; set; }
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }
        public string CaseComplete { get; set; }
        //public CustomBoolean ServicerConsent { get; set; }
        //public CustomBoolean FundingConsent { get; set; }
        //[NullableOrInRangeValidator(true, "[0-9]", MessageTemplate = "Max Number Of Case: Only numeric characters allowed", Ruleset = "CriteriaValidation")]
        //public int MaxNumberOfCase { get; set; }
        public int Indicator { get; set; }
    }
}
