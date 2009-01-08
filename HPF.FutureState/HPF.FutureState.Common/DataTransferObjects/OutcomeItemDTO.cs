using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class OutcomeItemDTO : BaseDTO
    {      
        public int OutcomeItemId { get; set; }

        public int FcId { get; set; }  

        public int OutcomeSetId { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, Ruleset = "RequirePartialValidate", MessageTemplate = "Required!")]        
        public int OutcomeTypeId { get; set; }  

        public DateTime OutcomeDt { get; set; }  

        public DateTime OutcomeDeletedDt { get; set; }  

        public string NonprofitreferralKeyNum { get; set; }

        public string ExtRefOtherName { get; set; }  
    }
}
