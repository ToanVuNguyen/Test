using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class ForeclosureCaseSaveRequest : BaseRequest
    {
        public ForeclosureCaseSetDTO ForeclosureCaseSet { get; set; }
    }
}
