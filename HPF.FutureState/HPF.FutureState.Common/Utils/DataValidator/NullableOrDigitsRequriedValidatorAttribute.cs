using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.Utils.DataValidator
{
    public class NullableOrDigitsRequriedValidatorAttribute : ValidatorAttribute
    {
        int _numberOfDigits;
        bool _nullable;
        string _fieldName;

        /// <summary>
        /// Attribute for validator
        /// </summary>
        /// <param name="nullable">String to validate is allowed to be null or not</param>
        /// <param name="numberOfDigits">Length of the String to validate</param>
        public NullableOrDigitsRequriedValidatorAttribute(bool nullable, int numberOfDigits, string fieldName)            
        {
            _nullable = nullable;
            _numberOfDigits = numberOfDigits;
            _fieldName = fieldName;
        }
        protected override Validator DoCreateValidator(Type targetType)
        {
            return new NullableOrDigitsRequriedValidator(_nullable, _numberOfDigits, _fieldName);
        }
    }
}
