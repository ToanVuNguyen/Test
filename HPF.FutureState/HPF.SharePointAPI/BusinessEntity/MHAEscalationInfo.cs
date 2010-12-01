using System;
using System.Collections.Generic;
using System.Text;

namespace HPF.SharePointAPI.BusinessEntity
{
    public class MHAEscalationInfo : BaseObject
    {
        public DateTime? ItemCreatedDate { get; set; }
        public string ItemCreatedUser { get; set; }
        public DateTime? ItemModifiedDate { get; set; }
        public string ItemModifiedUser { get; set; }
        public int? ItemId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string LoanNumber { get; set; }
        public string Servicer { get; set; }
        //public int? ServicerId { get; set; }
        public string Escalation { get; set; }
        //public string EscalationCd { get; set; }
        public string SupportingDocumentation { get; set; }
        public string EscalationTeamNotes { get; set; }
        public string MMICaseId { get; set; }
        //public int? FcId { get; set; }
        public string GSELookup { get; set; }
        public string CounselorName { get; set; }
        public string CounselorEmail { get; set; }
        public string CounselorPhone { get; set; }
        public string EscalatedToMMIMgmt { get; set; } //EscalatedToHPF 
        public string CurrentOwnerOfIssue { get; set; }
        public string FinalResolution { get; set; }
        public DateTime? FinalResolutionDate { get; set; }
        //public string FinalResolutionCd { get; set; }
        public string FinalResolutionNotes { get; set; }
        public string ResolvedBy { get; set; }
        public string EscalatedToFannie { get; set; }
        public string EscalatedToFreddie { get; set; }
        public string HpfNotes { get; set; }
        public string GseNotes { get; set; }
        //public DateTime? ReplicateDt { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Address { get; set; }

        public string BorrowerEmail { get; set; }
        public string BestTimetoReach { get; set; }
        public string BestNumberToCall { get; set; }
        public int? HandleTimeHrs { get; set; }
        public int? HandleTimeMins { get; set; }
        public DateTime? EscalatedToGSEDate { get; set; }
        public DateTime? GSENotesCompletedDate { get; set; }
        public DateTime? EscalatedToMMIMgmtDate { get; set; }

        public string Commitment { get; set; }
        public DateTime? FollowupDateTime { get; set; }
        public DateTime? CommitmentClosed { get; set; }
        public string EscalatedToHSC { get; set; }
    }
}
