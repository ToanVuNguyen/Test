using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class CallLogRetrieveRequest
    {
        string _callLogId = null;
        [RegexValidator(@"^HPF\d", Ruleset = "Default", MessageTemplate="CallLogId is invalid")]
        public string callLogId 
        { 
            get{ return _callLogId;} 
            set
            {
                if(value != null)
                {
                    _callLogId = value;
                    _callLogId = _callLogId.ToUpper();
                }
            } 
        }
    }
}
