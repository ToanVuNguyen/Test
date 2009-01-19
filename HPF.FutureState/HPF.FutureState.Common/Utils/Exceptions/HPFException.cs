using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HPF.FutureState.Common.Utils.Exceptions
{
    public class HPFException : Exception
    {
        internal string ApplicationName { get; set; }

        internal string AgencyId { get; set; }

        internal string CallCenterId { get; set; }

        internal string UserName { get; set; }

        public HPFException()
        {
            Initialize();
        }

        private void Initialize()
        {
            ApplicationName = ConfigurationManager.AppSettings["HPFApplicationName"];
            AgencyId = string.Empty;
            CallCenterId = string.Empty;
            UserName = string.Empty;
        }

        public HPFException(string message)
            : base(message)
        {
            Initialize();
        }

        public HPFException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Initialize();
        }

        public HPFException(string message, Exception innerException)
            : base(message,innerException)
        {
            Initialize();
        }
    }
}
