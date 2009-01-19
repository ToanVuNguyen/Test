using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public class DataAccessException : HPFException
    {
        public DataAccessException()
        {
            
        }

        public DataAccessException(string message)
            : base(message)
        {

        }

        public DataAccessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public DataAccessException(string message, Exception innerException)
            : base(message,innerException)
        {
            
        }
    }
}
