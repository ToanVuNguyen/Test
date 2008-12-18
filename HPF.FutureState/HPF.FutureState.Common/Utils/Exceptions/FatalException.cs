using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public class FatalException : Exception
    {
        public FatalException()
        {
            
        }

        public FatalException(string message)
            : base(message)
        {

        }

        public FatalException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public FatalException(string message, Exception innerException)
            : base(message,innerException)
        {

        }
    }
}
