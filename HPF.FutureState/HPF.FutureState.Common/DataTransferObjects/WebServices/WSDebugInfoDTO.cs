using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    [XmlRootAttribute("DebugInfo")]
    public class WSDebugInfoDTO
    {
        [XmlAttribute("fcid")]
        public string FcId { get; set; }
        [XmlElement("Request")]
        public ForeclosureCaseSetDTO FCaseSetRequest { get; set; }
        [XmlElement("Response")]
        public ForeclosureCaseSaveResponse Response { get; set; }
        [XmlElement("EventSaveRequest")]
        public EventSaveRequest EventRequest { get; set; }
        [XmlElement("EventSaveResponse")]
        public EventSaveResponse EventResponse {get;set;}
    }
}
