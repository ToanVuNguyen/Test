using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public class MiscProcessingException : Exception
    {
        public MiscProcessingException()
        {
            
        }

        public MiscProcessingException(string message)
            : base(message)
        {

        }

        public MiscProcessingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public MiscProcessingException(string message, Exception innerException)
            : base(message,innerException)
        {

        }
    }
}
