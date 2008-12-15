using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.Utils.DataValidator
{
    public class NullableOrPatternMatchedValidatorAttribute : ValidatorAttribute
    {
        string _pattern;
        bool _nullable;

        /// <summary>
        /// Attribute for validator
        /// </summary>
        /// <param name="nullable">String to validate is allowed to be null or not</param>
        /// <param name="numberOfDigits">Length of the String to validate</param>
        public NullableOrPatternMatchedValidatorAttribute(bool nullable, string pattern)
        {
            _nullable = nullable;
            _pattern = pattern;
        }
        protected override Validator DoCreateValidator(Type targetType)
        {
            return new NullableOrPatternMatchedValidator(_nullable, _pattern);
        }
    }
}
