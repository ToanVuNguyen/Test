using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    [Serializable]
    public class BaseResponse
    {
        private ResponseStatus status = ResponseStatus.Fail;
        /// <summary>
        /// Status
        /// </summary>
        public ResponseStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        /// <summary>
        /// List of exception message.
        /// </summary>
        public ExceptionMessageCollection Messages; 
      
        public BaseResponse()
        {
            Messages = new ExceptionMessageCollection();
        }
    }
}
