using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class CallLogWSDTO : BaseDTO
    {
        #region property
        //public string CallId { get; set; }

        //[XmlElement(IsNullable = true)]
        //public int? CallCenterID { get; set; }

        public string CcAgentIdKey { get; set; }

        [XmlElement(IsNullable = true)]
        public DateTime? StartDate { get; set; }

        [XmlElement(IsNullable = true)]
        public DateTime? EndDate { get; set; }

        public string DNIS { get; set; }

        public string CallCenter { get; set; }

        public string CallSourceCd { get; set; }

        public string ReasonForCall { get; set; }

        public string LoanAccountNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [XmlElement(IsNullable = true)]
        public int? ServicerId { get; set; }

        public string OtherServicerName { get; set; }

        public string PropZipFull9 { get; set; }

        [XmlElement(IsNullable = true)]
        public int? PrevAgencyId { get; set; }

        [XmlElement(IsNullable = true)]
        public int? SelectedAgencyId { get; set; }

        public string ScreenRout { get; set; }

        public string FinalDispoCd { get; set; }

        public string TransNumber { get; set; }

        public string CcCallKey { get; set; }

        public string LoanDelinqStatusCd { get; set; }

        public string SelectedCounselor { get; set; }

        public string HomeownerInd { get; set; }

        public string PowerOfAttorneyInd { get; set; }

        public string AuthorizedInd { get; set; }

        public string City { get; set; } 

        public string State { get; set; }

        public string NonprofitReferralKeyNum1 { get; set; }

        public string NonprofitReferralKeyNum2 { get; set; }

        public string NonprofitReferralKeyNum3 { get; set; }
        #endregion           
    }
}
