using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class ICTForeclosureCaseSearchRequest : BaseRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LoanNumber { get; set; }

        public string PropertyZip { get; set; }
        
        public string Last4_SSN { get; set; }
    }
}
