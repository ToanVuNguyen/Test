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
    [Serializable]
    public class InvoiceSearchCriteriaDTO
    {
        [RangeValidator(-1,RangeBoundaryType.Inclusive,int.MaxValue,RangeBoundaryType.Inclusive,Ruleset=Constant.RULESET_FUNDINGSOURCEVALIDATION, Tag=ErrorMessages.ERR0561)]
        public int FundingSourceId { get; set; }        
        
        [NullableOrInRangeNumberValidator(false,"1-1-1753","12-31-9999",Ruleset=Constant.RULESET_FUNDINGSOURCEVALIDATION,Tag=ErrorMessages.ERR0562)]
        public DateTime PeriodStart { get; set; }
        [NullableOrInRangeNumberValidator(false, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_FUNDINGSOURCEVALIDATION, Tag = ErrorMessages.ERR0563)]
        public DateTime PeriodEnd { get; set; }        
    }
}
