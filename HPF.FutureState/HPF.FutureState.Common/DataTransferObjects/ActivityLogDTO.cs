using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class ActivityLogDTO : BaseDTO
    {
        public int ActivityLogId{ get; set; }

        public int FcId{ get; set; }

        [NotNullValidator(Ruleset = "Default", MessageTemplate = "Required!")]
        public string ActivityCd{ get; set; }

        [NotNullValidator(Ruleset = "Default", MessageTemplate = "Required!")]
        public DateTime ActivityDt{ get; set; }

        public string ActivityNote { get; set; }
    }
}
