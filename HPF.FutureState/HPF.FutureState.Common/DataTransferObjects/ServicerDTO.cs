using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class ServicerDTO : BaseDTO
    {
        public int? ServicerID { get; set; }

        public string ServicerName { get; set; }

        public string SecureDeliveryMethodCd { get; set; }

        public string ContactFName { get; set; }

        public string ContactLName { get; set; }

        public string ContactEmail { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string ActiveInd { get; set; }

        public string FundingAgreementInd { get; set; }

        public string CouselingSumFormatCd { get; set; }

        public string HudServicerNum { get; set; }

        public string SPFolderName { get; set; }    
            
    }
}
