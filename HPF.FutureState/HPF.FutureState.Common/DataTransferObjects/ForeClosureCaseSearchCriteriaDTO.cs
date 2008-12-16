using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class ForeClosureCaseSearchCriteriaDTO
    {
        string _loanNumber = null; 
        
        //[ValidatorComposition(CompositionType.Or, Ruleset="Default")]
        //[IgnoreNulls()]
        //[RegexValidator("[a-zA-Z0-9]", MessageTemplate = "Agency Case Number: Only alpha-numeric characters allowed")]
        [NullableOrPatternMatchedValidator(true, "[a-zA-Z0-9]", MessageTemplate = "Agency Case Number: Only alpha-numeric characters allowed", Ruleset="Default")]
        public string AgencyCaseNumber 
        { 
            get; 
            set;
            
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string LoanNumber 
        {
            get { return _loanNumber; }
            set
            {
                if (value != null)
                {
                    _loanNumber = value;
                    Regex exp = new Regex(@"[^a-zA-Z0-9*]");
                    MatchCollection matches = exp.Matches(_loanNumber);                                        
                    foreach (Match item in matches)
                    {
                        _loanNumber = _loanNumber.Replace(item.Value, string.Empty);
                    }

                    exp = new Regex(@"[^a-zA-Z0-9]");
                    matches = exp.Matches(_loanNumber);
                    foreach (Match item in matches)
                    {
                        _loanNumber = _loanNumber.Replace(item.Value, "%");
                    }
                }
            }
        }

        [NullableOrDigitsRequriedValidator(false, 5, MessageTemplate = "Property Zip: is not null and must be 5 alpha - numeric characters", Ruleset = "Default")]
        public string PropertyZip { get; set; }
        
        [NullableOrDigitsRequriedValidator(true, 4, MessageTemplate = "Last 4 SSN must be 4 alpha - numeric characters", Ruleset = "Default")]
        //[ValidatorComposition(CompositionType.And, Ruleset = "Default")]
        //[IgnoreNulls()]
        //[RegexValidator("^\\d{4}$", MessageTemplate = "Last 4 SSN must be 4 alpha - numeric characters")]
        public string Last4_SSN { get; set; }

    }
}
