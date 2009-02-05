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
    public class NullableOrInRangeNumberValidator : Validator
    {
        bool _nullable;        
        string _lowerBound;
        string _upperBound;

        public NullableOrInRangeNumberValidator(bool nullable, string lowerBound, string upperBound)
            : base(null, null)
        {
            _nullable = nullable;
            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        protected override string DefaultMessageTemplate
        {
            get { return "Field is invalid."; }
        }

        protected override void DoValidate(object objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            bool isValid = false;
            if ((objectToValidate == null) || (string.IsNullOrEmpty(objectToValidate.ToString())))
            {
                isValid = _nullable;
                if (!isValid)
                    MessageTemplate = key + " is required";
            }
            else
            {
                if (objectToValidate.GetType() == typeof(decimal))
                {
                    decimal value;
                    decimal lowerValue;
                    decimal upperValue;
                    if (!decimal.TryParse(_lowerBound, out lowerValue))
                        lowerValue = decimal.MinValue;
                    if (!decimal.TryParse(_upperBound, out upperValue))
                        upperValue = decimal.MaxValue;
                    if (!decimal.TryParse(objectToValidate.ToString(), out value))
                    {
                        isValid = false;
                        //MessageTemplate = key + " is invalid";
                    }
                    else
                    {
                        if (lowerValue <= value && upperValue >= value)
                            isValid = true;
                        else
                        {
                            isValid = false;
                            //MessageTemplate = string.Format("{0} is out of allowed range ", key);
                        }

                    }
                }

                if (objectToValidate.GetType() == typeof(double))
                {
                    double value;
                    double lowerValue;
                    double upperValue;
                    if (!double.TryParse(_lowerBound, out lowerValue))
                        lowerValue = double.MinValue;
                    if (!double.TryParse(_upperBound, out upperValue))
                        upperValue = double.MaxValue;
                    if (!double.TryParse(objectToValidate.ToString(), out value))
                    {
                        isValid = false;
                        //MessageTemplate = key + " is invalid";
                    }
                    else
                    {
                        if (lowerValue <= value && upperValue >= value)
                            isValid = true;
                        else
                        {
                            isValid = false;
                            //MessageTemplate = string.Format("{0} is out of allowed range ", key);
                        }

                    }
                }


                if (objectToValidate.GetType() == typeof(int))
                {
                    int value;
                    int lowerValue;
                    int upperValue;
                    if (!int.TryParse(_lowerBound, out lowerValue))
                        lowerValue = int.MinValue;
                    if (!int.TryParse(_upperBound, out upperValue))
                        upperValue = int.MaxValue;
                    if (!int.TryParse(objectToValidate.ToString(), out value))
                    {
                        isValid = false;
                        //MessageTemplate = key + " is invalid";
                    }
                    else
                    {
                        if (lowerValue <= value && upperValue >= value)
                            isValid = true;
                        else
                        {
                            isValid = false;
                            //MessageTemplate = string.Format("{0} is out of allowed range ", key);
                        }

                    }
                }

                                
            }
            if (!isValid)
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
        }
    }
}
