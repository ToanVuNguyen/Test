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
    public class YesNoIndicatorValidator:Validator<string>
    {
        const string _YesIndicator = "Y";
        const string _NoIndicator = "N";
        public YesNoIndicatorValidator()
            : base(null, null)
        {
            
        }

        public YesNoIndicatorValidator(string messageTemplate, string tag)
            : base(messageTemplate, tag)
        {

        }

        protected override string DefaultMessageTemplate
        {
            get { return "InTake day is incorrect."; }
        }

        protected override void DoValidate(String objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            bool isValid = false;
            isValid = (objectToValidate.ToUpper().Equals(_YesIndicator) || objectToValidate.ToUpper().Equals(_NoIndicator));
            if (!isValid)
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);

            
        }
    }
}
