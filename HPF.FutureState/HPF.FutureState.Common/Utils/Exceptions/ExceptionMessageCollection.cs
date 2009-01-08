using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public class ExceptionMessageCollection : Collection<ExceptionMessage>
    {      
        public void AddExceptionMessage(string message)
        {
            Add(new ExceptionMessage
            {                
                Message = message
            });
        }
    }
}
