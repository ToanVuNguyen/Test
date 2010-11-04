using System;
using System.Collections.Generic;
using System.Text;

namespace HPF.SharePointAPI.BusinessEntity
{
    public class MHAHelpInfo: BaseObject
    {
        public DateTime? ItemCreatedDate { get; set; }
        public string ItemCreatedUser { get; set; }
        public DateTime? ItemModifiedDate { get; set; }
        public string ItemModifiedUser { get; set; }
        public int? ItemId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Servicer { get; set; }
        public string LoanNumber { get; set; }
        public string CounselorName { get; set; }
        public string CounselorEmail { get; set; }
        public string CallSource { get; set; }
        public DateTime? VoicemailDate { get; set; }
        public string MHAHelpReason { get; set; }
        public string Comments { get; set; }
        public string MHAConversionCampainFields { get; set; }
        public string PrivacyConsent { get; set; }
        public string BorrowerInTrialMod { get; set; }
        public string TrialModStartedBeforeNov1 { get; set; }
        public string CurrentOnPayments { get; set; }
        public string WageEarner { get; set; }
        public string IfWageEarnerWereTwoPayStubsSentIn { get; set; }
        public string AllDocumentsSubmitted { get; set; }
        public string DocumentsSubmitted { get; set; }
        public string Phone { get; set; }
        public string BestTimeToReach { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string MHAHelpResolution { get; set; }
        public string Title { get; set; }
        public string FinalResolutionNotes { get; set; }
        public string MMICaseId { get; set; }

        public int? HandleTimeHrs { get; set; }
        public int? HandleTimeMins { get; set; }
    }
}
