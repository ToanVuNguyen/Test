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

        public static bool HandleExceptionPolicyName(Exception exception, string policyName)
        {
            return ExceptionPolicy.HandleException(exception, policyName);
        }

        public static bool HandleException(Exception exception, string userName, string agencyId, string callCenterId)
        {
            var hpfException = GetHpfException(exception, userName, agencyId, callCenterId);
            return ExceptionPolicy.HandleException(hpfException, policyName);
        }

        public static bool HandleException(Exception exception, string userName)
        {
            var hpfException = GetHpfException(exception, userName, "", "");
            return ExceptionPolicy.HandleException(hpfException, policyName);
        }

        private static HPFException GetHpfException(Exception exception, string userName, string agencyId, string callCenterId)
        {
            var hpfException = GetHpfException(exception);
            hpfException.UserName = userName;
            hpfException.AgencyId = agencyId;
            hpfException.CallCenterId = callCenterId;
            return hpfException;
        }

        public static HPFException GetHpfException(Exception exception, string fcId, string fucntionName)
        {
            var hpfException = GetHpfException(exception);
            hpfException.FcId = fcId;
            hpfException.FunctionName = fucntionName;
            
            return hpfException;
        }
        private static HPFException GetHpfException(Exception exception)
        {
            HPFException hpfException;
            if (exception is HPFException)
            {
                hpfException = (HPFException)exception;                
            }
            else
            {
                hpfException = Wrap<HPFException>(exception);                
            }
            return hpfException;
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

        public static string FormatException(Exception exception)
        {
            return exception.Message;
        }
    }

    

}
