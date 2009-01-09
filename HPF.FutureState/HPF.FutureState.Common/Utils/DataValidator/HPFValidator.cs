using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.Utils.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace HPF.FutureState.Common.Utils.DataValidator
{
    /// <summary>
    /// Used to produce Validator object
    /// </summary>
    public static class HPFValidator
    {
        /// <summary>
        /// Create Validator object
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <returns>a Validator</returns>
        private static Validator<T> CreateValidator<T>()
        {
            return ValidationFactory.CreateValidator<T>("Default");
        }

        private static Validator<T> CreateValidator<T>(string ruleSet)
        {
            return ValidationFactory.CreateValidator<T>(ruleSet);
        } 

        public static ValidationResults Validate<T>(T target)
        {
            return CreateValidator<T>().Validate(target);
        }

        public static ValidationResults Validate<T>(T target, string ruleSet)
        {
            return CreateValidator<T>(ruleSet).Validate(target);
        }

        public static ExceptionMessageCollection ValidateToGetExceptionMessage<T>(T target)
        {
            var results = CreateValidator<T>().Validate(target);
            return CreateFriendlyExceptionMessage(results);
        }

        public static ExceptionMessageCollection ValidateToGetExceptionMessage<T>(T target, string ruleSet)
        {
            var results = CreateValidator<T>(ruleSet).Validate(target);
            return CreateFriendlyExceptionMessage(results);
        }

        private static ExceptionMessageCollection CreateFriendlyExceptionMessage(ValidationResults results)
        {
            var exceptionMessages = new ExceptionMessageCollection();
            foreach (var result in results)
            {
                if (result.Tag != null)
                {
                    exceptionMessages.AddExceptionMessage(result.Tag, FriendlyMessageTranslate(result.Tag));
                    continue;
                }
                exceptionMessages.AddExceptionMessage(result.Message);                
            }
            return exceptionMessages;
        }

        /// <summary>
        /// Translate a validation errorCode to a friendly message
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        private static string FriendlyMessageTranslate(string errorCode)
        {
            return ErrorMessages.GetExceptionMessageCombined(errorCode);
        }

    }
}
