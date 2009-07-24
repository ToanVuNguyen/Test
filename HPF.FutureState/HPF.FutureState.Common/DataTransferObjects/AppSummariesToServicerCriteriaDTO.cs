using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class AppSummariesToServicerCriteriaDTO: BaseDTO
    {
        [RequiredObjectValidator(Ruleset = "Default", MessageTemplate = "ServicerId is required for operation")]
        public int? ServicerId { get; set; }
        [RequiredObjectValidator(Ruleset = "Default", MessageTemplate = "Start Date is required for operation")]
        public DateTime? StartDt { get; set; }
        [RequiredObjectValidator(Ruleset = "Default", MessageTemplate = "End Date is required for operation")]
        public DateTime? EndDt { get; set; }        
    }
}
