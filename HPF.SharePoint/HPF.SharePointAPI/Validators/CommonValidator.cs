using System;
using System.Collections.Generic;
using System.Text;

namespace HPF.SharePointAPI.Validators
{
    public static class CommonValidator
    {
        public static void ArgumentNotNull(object value, string argumentName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void ArgumentNotNull(object[] values, string argumentName)
        {
            ArgumentNotNull(values, "values");
            if (values.Length < 1)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void ArgumentNotNullAndEmpty(string value, string argumentName)
        {
            ArgumentNotNull(value, argumentName);
            if (value.Length == 0)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
