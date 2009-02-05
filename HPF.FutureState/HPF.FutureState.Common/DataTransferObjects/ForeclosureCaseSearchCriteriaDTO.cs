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
    public class ForeclosureCaseSearchCriteriaDTO
    {
        [NullableOrInRangeValidator(true, "[a-zA-Z0-9]", MessageTemplate = "Agency Case Number: Only alpha-numeric characters allowed", Ruleset = "Default")]
        public string AgencyCaseNumber
        {
            get;
            set;

        }

        string _firstName = null;        
        [NullableOrStringLengthValidator(true, 30, "First Name", Ruleset="Default")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value != null)
                {
                    _firstName = value;
                    _firstName = ProcessSQLSpecialCharacter(_firstName);
                    Regex exp = new Regex(@"[*]");
                    MatchCollection matches = exp.Matches(_firstName);
                    foreach (Match item in matches)
                    {
                        _firstName = _firstName.Replace(item.Value, "%");
                    }
                    
                }
            }
        }

        string _lastName = null;        
        [NullableOrStringLengthValidator(true, 30, "Last Name", Ruleset="Default")]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (value != null)
                {
                    _lastName = value;
                    _lastName = ProcessSQLSpecialCharacter(_lastName);
                    Regex exp = new Regex(@"[*]");
                    MatchCollection matches = exp.Matches(_lastName);
                    foreach (Match item in matches)
                    {
                        _lastName = _lastName.Replace(item.Value, "%");
                    }

                }
            }
        }


        [NullableOrInRangeValidator(true, "[a-zA-Z0-9]", MessageTemplate = "Loan Number: Only alpha-numeric characters allowed", Ruleset = "Default")]
        public string LoanNumber
        {
            get;
            set;
        }

        [NullableOrDigitsRequriedValidator(true, 5, "Property Zip",  Ruleset = "Default")]
        public string PropertyZip { get; set; }

        [NullableOrDigitsRequriedValidator(true, 4, "Last 4 SSN" , Ruleset = "Default")]
        public string Last4_SSN { get; set; }

        private string ProcessSQLSpecialCharacter(string sInput)
        {
            sInput = sInput.Replace("%", "/%");
            sInput = sInput.Replace("_", "/_");
            sInput = sInput.Replace("[", "/[");
            return sInput;
        }
    }
}