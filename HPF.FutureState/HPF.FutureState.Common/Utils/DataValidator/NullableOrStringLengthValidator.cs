using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Configuration;

using System.Text;
namespace HPF.FutureState.Common.Utils.DataValidator
{
    [ConfigurationElementType(typeof(CustomValidatorData))]
    class NullableOrStringLengthValidator:Validator<string>
    {
        int _length;
        bool _nullable;
        public NullableOrStringLengthValidator(bool nullable, int length)
            : base(null, null)
        {
            _nullable = nullable;
            _length = length;            
        }

        protected override string DefaultMessageTemplate
        {
            get { return "Field is invalid."; }
        }

        protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            bool isValid = false;
            
            if (objectToValidate == null || objectToValidate.Trim() == string.Empty)
            {
                isValid = _nullable;
                if (!isValid) MessageTemplate = key + " is required";
            }
            else
            {
                isValid = (objectToValidate.Trim().Length <= _length);
                if (!isValid) MessageTemplate = key + "max length is " + _length.ToString();
            }
            if (!isValid)
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
        }

    }
}
