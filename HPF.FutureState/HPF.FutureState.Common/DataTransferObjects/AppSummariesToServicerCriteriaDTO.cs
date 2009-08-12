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
        [RequiredObjectValidator(Ruleset = "Default", MessageTemplate = "A Servicer is required to process.")]
        public int? ServicerId { get; set; }
        [RequiredObjectValidator(Ruleset = "Default", MessageTemplate = "A Start Date is required to process.")]
        public DateTime? StartDt { get; set; }
        [RequiredObjectValidator(Ruleset = "Default", MessageTemplate = "An End Date is required to process.")]
        public DateTime? EndDt { get; set; }        
    }
}
