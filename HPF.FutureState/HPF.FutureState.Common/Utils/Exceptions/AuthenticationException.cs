using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public class AuthenticationException : HPFException
    {
        public AuthenticationException()
        {
            
        }

        public AuthenticationException(string message)
            : base(message)
        {

        }

        public AuthenticationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public AuthenticationException(string message, Exception innerException)
            : base(message,innerException)
        {

        }
    }
}
