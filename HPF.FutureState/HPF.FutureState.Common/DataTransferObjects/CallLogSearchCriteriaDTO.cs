using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CallLogSearchCriteriaDTO
    {
        string firstName;
        string lastName;
        string loanNumber;
        [RequiredObjectValidator(Ruleset = "Default", MessageTemplate="")]
        [NullableOrStringLengthValidator(true, 30, "First name", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0366)]        
        public string FirstName 
        {
            get { return firstName; }
            set 
            {                
                firstName = (string.IsNullOrEmpty(value) || value.Trim() == "")?null:value;
            }
        }
        [RequiredObjectValidator(Ruleset = "Default", MessageTemplate = "")]
        [NullableOrStringLengthValidator(true, 30, "LastName", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0367)]        
        public string LastName 
        {
            get { return lastName; }
            set
            { 
                lastName = (string.IsNullOrEmpty(value) || value.Trim() == "")?null:value;
            }
        }
        [RequiredObjectValidator(Ruleset = "Default", MessageTemplate = "")]
        [NullableOrStringLengthValidator(true, 30, "LoanNumber", Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR0365)]        
        public string LoanNumber 
        {
            get { return loanNumber; }
            set 
            {
                loanNumber = (string.IsNullOrEmpty(value) || value.Trim() == "") ? null : value;
            }
        }
    }
}
