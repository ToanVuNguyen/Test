using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public class ExceptionMessageCollection : Collection<ExceptionMessage>
    {
        public void AddExceptionMessage(string exceptionId, string message)
        {
            Add(new ExceptionMessage
                         {
                             ExceptionId = exceptionId,
                             Message = message
                         });
        }

        public void AddExceptionMessage(int exceptionId, string message)
        {
            Add(new ExceptionMessage
            {
                ExceptionId = exceptionId.ToString(),
                Message = message
            });
        }

        public void AddExceptionMessage(string message)
        {
            Add(new ExceptionMessage
            {                
                Message = message
            });
        }
    }
}
