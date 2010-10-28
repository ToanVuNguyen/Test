using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class PrePurchaseSaveResponse:BaseResponse
    {
        public int? PPCaseId { get; set; }
        public int? PPBorrowerId { get; set; }
        public DateTime UploadDt { get; set; }
    }
}
