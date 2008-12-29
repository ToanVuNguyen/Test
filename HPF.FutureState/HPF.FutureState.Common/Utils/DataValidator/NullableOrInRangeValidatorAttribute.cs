using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.Utils.DataValidator
{
    public class NullableOrInRangeValidatorAttribute : ValidatorAttribute
    {
        string _range;
        bool _nullable;

        /// <summary>
        /// Attribute for validator
        /// </summary>
        /// <param name="nullable">String to validate is allowed to be null or not</param>
        /// <param name="numberOfDigits">Length of the String to validate</param>
        public NullableOrInRangeValidatorAttribute(bool nullable, string range)
        {
            _nullable = nullable;
            _range = range;
        }
        protected override Validator DoCreateValidator(Type targetType)
        {
            return new NullableOrInRangeValidator(_nullable, _range);
        }
    }
}
