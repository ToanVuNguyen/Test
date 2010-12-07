using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class PrePurchaseCaseSaveResponse:BaseResponse
    {
        public int? PpcId { get; set; }
        public int? ApplicantId { get; set; }
        public DateTime UploadDt { get; set; }
        public int AgencyId { get; set; }
    }
}
