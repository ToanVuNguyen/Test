using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.Utils.DataValidator
{
    public class NullableOrStringLengthValidatorAttribute : ValidatorAttribute
    {
        int _length;
        bool _nullable;
        string _fieldName;

        /// <summary>
        /// Attribute for validator
        /// </summary>
        /// <param name="nullable">String to validate is allowed to be null or not</param>
        /// <param name="numberOfDigits">Length of the String to validate</param>
        public NullableOrStringLengthValidatorAttribute(bool nullable, int length, string fieldName)            
        {
            _nullable = nullable;
            _length = length;
            _fieldName = fieldName;
        }
        protected override Validator DoCreateValidator(Type targetType)
        {
            return new NullableOrStringLengthValidator(_nullable, _length, _fieldName);
        }
    }
}
