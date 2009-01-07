using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public class DuplicateException : Exception
    {
        public ExceptionMessageCollection ExceptionMessages { get; set; }
        public DuplicateException()
        {
            
        }

        public DuplicateException(string message)
            : base(message)
        {
            Init();
        }

        public DuplicateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Init();
        }

        public DuplicateException(string message, Exception innerException)
            : base(message,innerException)
        {
            Init();
        }
        private void Init()
        {
            ExceptionMessages = new ExceptionMessageCollection();
        }
    }
}
