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
        [NullableOrPatternMatchedValidator(true, "[a-zA-Z0-9]", MessageTemplate = "Agency Case Number: Only alpha-numeric characters allowed", Ruleset = "Default")]
        public string AgencyCaseNumber
        {
            get;
            set;

        }

        string _firstName = null;
        [IgnoreNulls(Ruleset = "Default")]
        [StringLengthValidator(0, 30, MessageTemplate = "First Name: Max Length is 30 characters", Ruleset = "Default")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value != null)
                {
                    _firstName = value;
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
        [IgnoreNulls(Ruleset = "Default")]
        [StringLengthValidator(0, 30, MessageTemplate = "Last Name: Max Length is 30 characters", Ruleset = "Default")]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (value != null)
                {
                    _lastName = value;
                    Regex exp = new Regex(@"[*]");
                    MatchCollection matches = exp.Matches(_lastName);
                    foreach (Match item in matches)
                    {
                        _lastName = _lastName.Replace(item.Value, "%");
                    }

                }
            }
        }

        string _loanNumber = null;
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

                    exp = new Regex(@"[*]");
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
        public string Last4_SSN { get; set; }

    }
}