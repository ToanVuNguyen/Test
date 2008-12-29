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
        [XmlIgnore]
        public DateTime CreateDate { get; set; }

        [XmlIgnore]
        public string CreateUserId { get; set; }

        [XmlIgnore]
        public string CreateAppName { get; set; }

        [XmlIgnore]
        public DateTime ChangeLastDate { get; set; }

        [XmlIgnore]
        public string ChangeLastUserId { get; set; }

        [XmlIgnore]
        public string ChangeLastAppName { get; set; }
    }
}
