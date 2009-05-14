using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class ServicerDTO : BaseDTO
    {
        public int? ServicerID { get; set; }
        public string ServicerName { get; set; }
        public string ContactFName { get; set; }
        public string ContactLName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        /// <summary>
        /// FundingAgreementInd
        /// </summary>
        public string FundingAgreement { get; set; }
        /// <summary>
        /// SecureDeliveryMethodCd
        /// </summary>
        public string SummaryDeliveryMethod { get; set; }
        [XmlIgnore]       
        public string ContactEmail { get; set; }
        [XmlIgnore]
        public string ActiveInd { get; set; }
        [XmlIgnore]
        public string CouselingSumFormatCd { get; set; }
        [XmlIgnore]
        public string HudServicerNum { get; set; }
        [XmlIgnore]
        public string SPFolderName { get; set; }                
    }
}
