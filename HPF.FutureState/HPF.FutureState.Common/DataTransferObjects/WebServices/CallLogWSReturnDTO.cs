using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class CallLogWSReturnDTO
    {
        #region property
        public string CallId { get; set; }

        [XmlElement(IsNullable = true)]
        public DateTime? StartDate { get; set; }

        [XmlElement(IsNullable = true)]
        public DateTime? EndDate { get; set; }                

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

        public string FinalDispoCd { get; set; }                

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


        public string DelinqInd { get; set; }
        public string PropStreetAddr { get; set; }
        public string PrimResInd { get; set; }
        public string LoanAmtInd { get; set; }
        public string CustPhone { get; set; }
        public string LoanLookupCd { get; set; }
        public string OrigdateInd { get; set; }
        public double? Payment { get; set; }
        public double? GrossIncome { get; set; }
        public string DTIIndicator { get; set; }
        public int? ServicerCA { get; set; }
        public DateTime? LastSCA { get; set; }
        public int? ServicerIdCA { get; set; }
        public string ServicerOtherNameCA { get; set; }
        public string MHAInfoShare { get; set; }
        public string ICTCallId { get; set; }
        public string MHAEligibilityCd { get; set; }

        #endregion           
    }
}
