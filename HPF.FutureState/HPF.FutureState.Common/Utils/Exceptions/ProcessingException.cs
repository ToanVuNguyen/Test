using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public class ProcessingException:Exception
    {
        public ExceptionMessageCollection ExceptionMessages { get; set; }

        public ProcessingException()
        {
            Init();
        }

        public ProcessingException(string message)
            : base(message)
        {
            Init();
        }

        public ProcessingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Init();
        }

        public ProcessingException(string message, Exception innerException)
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
