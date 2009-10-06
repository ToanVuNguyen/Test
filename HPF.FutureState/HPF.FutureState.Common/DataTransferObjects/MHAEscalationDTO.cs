using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class MHAEscalationDTO : BaseDTO
    {
        public DateTime? CreatedDt { get; set; }
        public string BorrowerFname { get; set; }
        public string BorrowerLname { get; set; }
        public string AcctNum { get; set; }
        public string Servicer { get; set; }
        public int? ServicerId { get; set; }
        public string Escalation { get; set; }
        public string EscalationCd { get; set; }
        public string EscalationTeamNotes { get; set; }
        public string AgencyCaseNum { get; set; }
        public int? FcId { get; set; }
        public string GseLookup { get; set; }
        public string CounselorName { get; set; }
        public string CounselorEmail { get; set; }
        public string CounselorPhone { get; set; }
        public string EscalatedToHPF { get; set; }
        public string CurrentOwnerOfIssue { get; set; }
        public string FinalResolution { get; set; }
        public string FinalResolutionCd { get; set; }
        public DateTime? FinalResolutionDate { get; set; }
        public string FinalResolutionNotes { get; set; }
        public string ResolvedBy { get; set; }
        public string EscalatedToFannie { get; set; }
        public string EscalatedToFreddie { get; set; }
        public string HpfNotes { get; set; }
        public string GseNotes { get; set; }
        //public DateTime? ReplicateDt { get; set; }
    }
}
