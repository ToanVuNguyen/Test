using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
namespace HPF.FutureState.Common.Utils.DataValidator
{

    public class RequiredObjectValidatorAttribute : ValidatorAttribute
    {
        public RequiredObjectValidatorAttribute()
        {            
        }
        protected override Validator DoCreateValidator(Type targetType)
        {
            return new RequiredObjectValidator();
        }
    }
}
