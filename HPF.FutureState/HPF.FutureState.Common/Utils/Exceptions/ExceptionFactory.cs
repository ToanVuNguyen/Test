using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public static class ExceptionFactory
    {
        public static object CreateException(string toExceptionTypeName, Exception originalException)
        {
            switch (toExceptionTypeName)
            {
                case "AuthenticationException":
                    return new AuthenticationException(originalException.Message, originalException);
                case "DataAccessException":
                    return new DataAccessException(originalException.Message, originalException);
                case "DuplicateException":
                    return new DuplicateException(originalException.Message, originalException);
                case "DataValidationException":
                    return new DataValidationException(originalException.Message, originalException);
                default: throw new Exception("ExceptionTypeName is not supported.");
            }                        
        }
    }
}
