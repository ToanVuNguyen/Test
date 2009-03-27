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
    {   [RequiredObjectValidator(Tag=ErrorMessages.ERR0579,Ruleset=Constant.RULESET_AGENCY_PAYABLE_SEARCH)] 
        public int? AgencyId { get; set; }
        [NullableOrInRangeNumberValidator(false,"1-1-1753","12-31-9999",Tag=ErrorMessages.ERR0580,Ruleset=Constant.RULESET_AGENCY_PAYABLE_SEARCH)]
        public DateTime PeriodStartDate { get; set; }
        [NullableOrInRangeNumberValidator(false,"1-1-1753","12-31-9999",Tag=ErrorMessages.ERR0581,Ruleset=Constant.RULESET_AGENCY_PAYABLE_SEARCH)]
        public DateTime PeriodEndDate { get; set; }
        public string CaseComplete { get; set; }
        public int Indicator { get; set; }
        //public string UserID { get; set; }
    }
}
