using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException()
        {
            
        }

        public DuplicateException(string message)
            : base(message)
        {

        }

        public DuplicateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public DuplicateException(string message, Exception innerException)
            : base(message,innerException)
        {

        }
    }
}
