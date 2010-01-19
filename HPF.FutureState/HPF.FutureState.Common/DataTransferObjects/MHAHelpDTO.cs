using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class MHAHelpDTO: BaseDTO
    {
        public DateTime? ItemCreatedDt { get; set; }
        public string ItemCreatedUser { get; set; }
        public DateTime? ItemModifiedDt { get; set; }
        public string ItemModifiedUser { get; set; }
        public string BorrowerFName { get; set; }
        public string BorrowerLName { get; set; }
        public string Servicer { get; set; }
        public string AcctNum { get; set; }
        public string CounselorName { get; set; }
        public string CounselorEmail { get; set; }
        public string CallSource { get; set; }
        public DateTime? VoicemailDt { get; set; }
        public string MHAHelpReason { get; set; }
        public string Comments { get; set; }
        public string MHAConversionCampainFields { get; set; }
        public string PrivacyConsent { get; set; }
        public string BorrowerInTrialMod { get; set; }
        public string TrialModStartedBeforeStept1 { get; set; }
        public string CurrentOnPayments { get; set; }
        public string WageEarner { get; set; }
        public string IfWageEarnerWereTwoPayStubsSentIn { get; set; }
        public string AllDocumentsSubmitted { get; set; }
        public string DocumentsSubmitted { get; set; }
        public string BorrowerPhone { get; set; }
        public string BestTimeToReach { get; set; }
        public string BorrowerEmail { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string MHAHelpResolution { get; set; }
        public string Title { get; set; }
        public string FinalResolutionNotes { get; set; }
        public string MMICaseId { get; set; }
    }
}
