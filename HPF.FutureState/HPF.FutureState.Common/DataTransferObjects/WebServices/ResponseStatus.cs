using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public enum ResponseStatus
    {
        Success = 0,
        Fail = 1,
        Warning = 2,
        AuthenticationFail = 4,
    }
}
