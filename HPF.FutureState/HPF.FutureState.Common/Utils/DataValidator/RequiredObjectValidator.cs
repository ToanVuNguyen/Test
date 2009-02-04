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
            if (objectToValidate != null  && (!string.IsNullOrEmpty(objectToValidate.ToString())))
            {
                if (objectToValidate.GetType() == typeof(DateTime))
                {
                    DateTime dt;
                    if (!DateTime.TryParse(objectToValidate.ToString(), out dt))
                    {
                        isValid = false;
                        MessageTemplate = key + " is invalid";
                    }
                }

                if (objectToValidate.GetType() == typeof(decimal))
                {
                    decimal value;
                    if(!decimal.TryParse(objectToValidate.ToString(), out value))
                    {
                        isValid = false;
                        MessageTemplate = key + " is invalid";
                    }
                }

                if (objectToValidate.GetType() == typeof(double))
                {
                    double value;
                    if(!double.TryParse(objectToValidate.ToString(), out value))
                    {
                        isValid = false;
                        MessageTemplate = key + " is invalid";
                    }
                }

                if (objectToValidate.GetType() == typeof(int))
                {
                    int value;
                    if (!int.TryParse(objectToValidate.ToString(), out value))
                    {
                        isValid = false;
                        MessageTemplate = key + " is invalid";
                    }
                }

                if (objectToValidate.GetType() == typeof(byte))
                {
                    byte value;
                    if (!byte.TryParse(objectToValidate.ToString(), out value))
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
