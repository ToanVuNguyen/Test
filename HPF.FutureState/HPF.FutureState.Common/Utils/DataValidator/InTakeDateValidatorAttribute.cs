using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.Utils.DataValidator
{
    public class InTakeDateValidatorAttribute : ValidatorAttribute
    {
        private readonly int _PastDateDiff;

        public InTakeDateValidatorAttribute(int pastDateDiff)
        {
            _PastDateDiff = pastDateDiff;
        }
        protected override Validator DoCreateValidator(Type targetType)
        {
            return new InTakeDateValidator(_PastDateDiff);
        }
    }
}
