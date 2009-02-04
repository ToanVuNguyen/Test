using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Configuration;


namespace HPF.FutureState.Common.Utils.DataValidator
{
    [ConfigurationElementType(typeof(CustomValidatorData))]
    public class StringRequiredValidator : Validator<string>
    {
        public StringRequiredValidator() : base(null, null)
        {
            
        }
        public StringRequiredValidator(string messageTemplate, string tag) : base(messageTemplate, tag)
        {

        }

        protected override string DefaultMessageTemplate
        {
            get { return "Required!"; }
        }

        protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            if (objectToValidate == null)
            {
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);                
            }
            else
            {
                if ((objectToValidate == string.Empty) || (objectToValidate.Trim().Length == 0))
                {
                    LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
                }

            }
        }
    }
}
