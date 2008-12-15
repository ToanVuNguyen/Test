using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class CallLogInsertResult
    {
        public string CallLogID { get; set; }
        public DataValidationException Messages { get; set; }
    }
}
