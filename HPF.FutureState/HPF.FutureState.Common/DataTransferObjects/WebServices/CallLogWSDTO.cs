using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class CallLogWSDTO : BaseDTO
    {
        public string CallId { get; set; }

        public string ExtCallNumber { get; set; }

        public string AgencyId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string DNIS { get; set; }

        public string CallCenter { get; set; }

        public string CallCenterCD { get; set; }

        public string CallResource { get; set; }

        public string ReasonToCall { get; set; }

        public string AccountNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CounselPastYRInd { get; set; }

        public string MtgProbInd { get; set; }

        public string PastDueInd { get; set; }

        public string PastDueSoonInd { get; set; }

        public int PastDueMonths { get; set; }

        public int ServicerId { get; set; }

        public string OtherServicerName { get; set; }

        public string PropZip { get; set; }

        public int PrevCounselorId { get; set; }

        public int PrevAgencyId { get; set; }

        public string SelectedAgencyId { get; set; }

        public string ScreenRout { get; set; }

        public int FinalDispo { get; set; }

        public string TransNumber { get; set; }

        public string OutOfNetworkReferralTBD { get; set; }
    }
}
