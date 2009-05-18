using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class RefCodeItemDTO : BaseDTO
    {                
        public string Code { get; set; }
        public string CodeDesc { get; set; }
        public string RefCodeSetName { get; set; }
        public string CodeComment { get; set; }

        [XmlIgnore]
        public int? RefCodeItemId { get; set; }                                        
        [XmlIgnore]
        public int? SortOrder { get; set; }
        [XmlIgnore]
        public string ActiveInd { get; set; }
        //[XmlIgnore]
        //public string AgencyUsageInd { get; set; }
    }
}
