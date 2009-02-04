using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.Utils.DataValidator
{
    public class NullableOrInRangeNumberValidatorAttribute : ValidatorAttribute
    {
        string _lowerBound;
        string _upperBound;
        bool _nullable;

        /// <summary>
        /// Attribute for validator
        /// </summary>
        /// <param name="nullable">String to validate is allowed to be null or not</param>
        /// <param name="numberOfDigits">Length of the String to validate</param>
        public NullableOrInRangeNumberValidatorAttribute(bool nullable, string lowerBound, string upperBound)
        {
            _nullable = nullable;
            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }
        protected override Validator DoCreateValidator(Type targetType)
        {
            return new NullableOrInRangeNumberValidator(_nullable, _lowerBound, _upperBound);
        }
    }
}
