using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public class DataValidationException : Exception
    {

        public ExceptionMessageCollection ExceptionMessages { get; set; }

        public DataValidationException()
        {
            Init();
        }       

        public DataValidationException(string message)
            : base(message)
        {
            Init();
        }

        public DataValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Init();
        }

        public DataValidationException(string message, Exception innerException)
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
