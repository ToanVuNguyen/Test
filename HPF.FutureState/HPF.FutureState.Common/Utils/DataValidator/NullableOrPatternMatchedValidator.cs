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
    public class NullableOrPatternMatchedValidator : Validator<String>
    {
        string _pattern;
        bool _nullable;
        public NullableOrPatternMatchedValidator(bool nullable, string pattern)
            : base(null, null)
        {
            _nullable = nullable;
            _pattern = pattern;
        }

        protected override string DefaultMessageTemplate
        {
            get { return "Field is invalid."; }
        }

        protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            bool isValid = false;
            if (objectToValidate == null || objectToValidate == string.Empty)
                isValid = _nullable;
            else
            {
                _pattern += @"^" + _pattern;
                Regex exp = new Regex(_pattern);

                isValid = !exp.Match(objectToValidate).Success;
            }
            if (!isValid)
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
        }
    }
}