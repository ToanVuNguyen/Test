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
            bool isValid = false;
            if (objectToValidate.GetType() == typeof(DateTime))
            {
                isValid = true;
                if (objectToValidate == null)
                {
                    isValid = false;
                    MessageTemplate = key + " is required";
                }
                else 
                {
                    DateTime dt;
                    DateTime.TryParse(objectToValidate.ToString() ,out dt);
                    if (dt == DateTime.MinValue)
                    {
                        isValid = false;
                        MessageTemplate = key + " is invalid";
                    }
                }
            }
            if (!isValid)
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
        }        
    }
}
