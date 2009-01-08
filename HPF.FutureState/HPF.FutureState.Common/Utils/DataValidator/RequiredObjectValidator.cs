using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Configuration;

namespace HPF.FutureState.Common.Utils.DataValidator
{
    [ConfigurationElementType(typeof(CustomValidatorData))]
    public class RequiredObjectValidator : Validator
    {
        public RequiredObjectValidator()
            : base(null, null)
        {            
        }

        protected override string DefaultMessageTemplate
        {
            get { return "Required Field"; }
        }

        protected override void DoValidate(object objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            bool isValid = (objectToValidate == null);
            if (!isValid)
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
        }
        //protected override void DoValidate // (string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        //{
        //    bool isValid = false;
        //    if (objectToValidate == null || objectToValidate == string.Empty)
        //        isValid = _nullable;
        //    else
        //    {
        //        string pattern = "^\\d{" + _numberOfDigits + "}$";
        //        Regex exp = new Regex(pattern);

        //        isValid = exp.Match(objectToValidate).Success;
        //    }
        //    if (!isValid)
        //        LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
        //}
    }
}
