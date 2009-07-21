using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class ForeclosureCaseSaveResponse : BaseResponse
    {
        public int? FcId { get; set; }
        public DateTime? CompletedDt { get; set; }
    }
}
