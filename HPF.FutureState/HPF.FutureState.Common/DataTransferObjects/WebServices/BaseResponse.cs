using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    [Serializable]
    public class BaseResponse
    {
        private ResponseStatus status = ResponseStatus.Fail;

        public ResponseStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Message { get; set; }        
    }
}
