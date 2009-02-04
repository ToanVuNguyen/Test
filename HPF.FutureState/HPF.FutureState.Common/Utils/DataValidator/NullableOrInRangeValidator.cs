using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Configuration;

namespace HPF.FutureState.Common.Utils.DataValidator
{
    [ConfigurationElementType(typeof(CustomValidatorData))]
    public class NullableOrInRangeValidator : Validator
    {

        string _range;
        bool _nullable;
        public NullableOrInRangeValidator(bool nullable, string range)
            : base(null, null)
        {
            _nullable = nullable;
            _range = range;
        }

        protected override string DefaultMessageTemplate
        {
            get { return "Field is invalid."; }
        }

        

        protected override void DoValidate(object objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            bool isValid = false;
            string stringToValidate;
            stringToValidate = string.Empty;

            if (objectToValidate != null)
                if (objectToValidate.ToString() != "-1")
                    stringToValidate = objectToValidate.ToString();

            if (stringToValidate == null || stringToValidate == string.Empty)
                isValid = _nullable;
            else
            {
                //the InRange pattern is supposed to be [...]
                _range = _range.Insert(1, @"^");
                Regex exp = new Regex(_range);
                _range = _range.Remove(1, 1);

                isValid = !exp.IsMatch(stringToValidate); //exp.Match(objectToValidate).Success;
            }
            if (!isValid)
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
        }


    }

}
