using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Configuration;

namespace HPF.FutureState.Common.Utils.DataValidator
{
    [ConfigurationElementType(typeof(CustomValidatorData))]
    public class NullableOrDigitsRequriedValidator : Validator<String>
    {
        int _numberOfDigits;
        bool _nullable;
        string _fieldName;
        public NullableOrDigitsRequriedValidator(bool nullable, int numberOfDigits, string fieldName)
            : base(null, null)
        {
            _nullable = nullable;
            _numberOfDigits = numberOfDigits;
            _fieldName = fieldName;
        }

        protected override string DefaultMessageTemplate
        {
            get { return "Field is invalid."; }
        }

        protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            bool isValid = false;
            if (objectToValidate == null || objectToValidate == string.Empty)
            {
                isValid = _nullable;
                if (!isValid) MessageTemplate = _fieldName + " is required";
            }
            else
            {
                string pattern = "^\\d{" + _numberOfDigits + "}$";
                Regex exp = new Regex(pattern);

                isValid = exp.Match(objectToValidate.Trim()).Success;
                if (!isValid) MessageTemplate = string.Format("{0} must be numeric and contain {1} digits", _fieldName, _numberOfDigits);
            }
            if (!isValid)
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
        }
    }
}
