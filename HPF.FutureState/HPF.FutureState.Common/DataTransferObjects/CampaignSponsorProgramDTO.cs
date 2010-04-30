using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class CampaignSponsorProgramDTO: BaseDTO
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public int SponsorId { get; set; }
        public int CounseledProgramId { get; set; }
        public int ProgramId { get; set; }
        public DateTime? EffDt { get; set; }
        public DateTime? ExpDt { get; set; }
    }
}
