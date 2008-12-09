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
    public class InTakeDateValidator : Validator<DateTime>
    {
        private readonly int _PastDateDiff;

        public InTakeDateValidator(int pastDateDiff)
            : base(null, null)
        {
            _PastDateDiff = pastDateDiff;
        }

        public InTakeDateValidator(string messageTemplate, string tag) : base(messageTemplate, tag)
        {

        }

        protected override string DefaultMessageTemplate
        {
            get { return "InTake day is incorrect."; }
        }

        protected override void DoValidate(DateTime objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {                   
            if (objectToValidate < DateTime.Today.AddDays((-1) * _PastDateDiff))
            {
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
            }
        }
    }
}
