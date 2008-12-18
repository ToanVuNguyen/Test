using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public class ProcessingException:Exception
    {
        public ProcessingException()
        {
            
        }

        public ProcessingException(string message)
            : base(message)
        {

        }

        public ProcessingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public ProcessingException(string message, Exception innerException)
            : base(message,innerException)
        {

        }
    }
}
