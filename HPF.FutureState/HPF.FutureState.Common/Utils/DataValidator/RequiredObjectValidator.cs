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
            bool isValid = true;
            if (objectToValidate != null  && (string.IsNullOrEmpty(objectToValidate.ToString())))
            {
                if (objectToValidate.GetType() == typeof(DateTime))
                {
                    DateTime dt;
                    DateTime.TryParse(objectToValidate.ToString(), out dt);
                    if (dt == DateTime.MinValue)
                    {
                        isValid = false;
                        MessageTemplate = key + " is invalid";
                    }
                }

                if (objectToValidate.GetType() == typeof(decimal))
                {
                    decimal value;
                    decimal.TryParse(objectToValidate.ToString(), out value);
                    if (value == decimal.MinValue)
                    {
                        isValid = false;
                        MessageTemplate = key + " is invalid";
                    }
                }

                if (objectToValidate.GetType() == typeof(double))
                {
                    double value;
                    double.TryParse(objectToValidate.ToString(), out value);
                    if (value == double.MinValue)
                    {
                        isValid = false;
                        MessageTemplate = key + " is invalid";
                    }
                }

                if (objectToValidate.GetType() == typeof(int))
                {
                    int value;
                    int.TryParse(objectToValidate.ToString(), out value);
                    if (value == int.MinValue)
                    {
                        isValid = false;
                        MessageTemplate = key + " is invalid";
                    }
                }

                if (objectToValidate.GetType() == typeof(byte))
                {
                    byte value;
                    byte.TryParse(objectToValidate.ToString(), out value);
                    if (value == byte.MinValue)
                    {
                        isValid = false;
                        MessageTemplate = key + " is invalid";
                    }
                }
            }
            else
            {
                isValid = false;
                MessageTemplate = key + " is required";
            }
            if (!isValid)
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
        }        

    }
}
