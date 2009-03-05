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
        public int? ActivityLogId { get; set; }

        public int? FcId{ get; set; }
        
        public string ActivityCd{ get; set; }

        public string ActivityCdDesc { get; set; }
        
        public DateTime? ActivityDt{ get; set; }

        public string ActivityNote { get; set; }
    }
}
