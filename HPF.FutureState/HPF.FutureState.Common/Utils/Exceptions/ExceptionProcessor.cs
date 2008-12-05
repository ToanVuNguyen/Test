using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public static class ExceptionProcessor
    {
        private const string policyName = "HPF Exception Policy";

        /// <summary>
        /// Handle an Exception
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>True: Rethrow, False: do nothing</returns>
        public static bool HandleException(Exception exception)
        {
            return ExceptionPolicy.HandleException(exception, policyName);
        }

        /// <summary>
        ///Wrap an Exception to a T exception
        /// </summary>
        /// <typeparam name="T">Exception type</typeparam>
        /// <param name="exception">an Exception</param>
        /// <returns></returns>
        public static T Wrap<T>(Exception exception)
        {
            return (T)ExceptionFactory.CreateException(typeof (T).Name, exception);
        }
    }

    

}
