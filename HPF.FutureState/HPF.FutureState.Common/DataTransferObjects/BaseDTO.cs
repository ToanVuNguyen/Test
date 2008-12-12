using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BaseDTO
    {
        
        public DateTime CreateDate { get; set; }

        public string CreateUserId { get; set; }

        public string CreateAppName { get; set; }

        public DateTime ChangeLastDate { get; set; }

        public string ChangeLastUserId { get; set; }

        public string ChangeLastAppName { get; set; }
    }
}
