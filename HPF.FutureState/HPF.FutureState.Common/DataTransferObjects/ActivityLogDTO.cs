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

        [NotNullValidator(Ruleset = "Min Request Validate", MessageTemplate = "Required!")]
        public string ActivityCd{ get; set; }

        [DateTimeRangeValidator("0001-01-02T00:00:00", "9999-12-31T00:00:00", Ruleset = "Min Request Validate", MessageTemplate = "Required!")]
        public DateTime ActivityDt{ get; set; }

        public string ActivityNote { get; set; }
    }
}
