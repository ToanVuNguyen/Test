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

        internal string FunctionName { get; set; }
        internal string FcId { get; set; }
        internal string BatchJobId { get; set; } 

        public HPFException()
        {
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                ApplicationName = HPFConfigurationSettings.HPF_APPLICATION_NAME;
                AgencyId = string.Empty;
                CallCenterId = string.Empty;
                UserName = string.Empty;                
            }
            catch
            { 
            }
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

        public string GetBatchJobId()
        {
            return BatchJobId;
        }
    }
}
