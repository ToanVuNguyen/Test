using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class CampaignSponsorProgramDTOCollection: BaseDTOCollection<CampaignSponsorProgramDTO>
    {
        public CampaignSponsorProgramDTO GetCampaignSponsorProgram(int campaignId, int sponsorId, int counseledProgramId)
        {
            return this.SingleOrDefault(i => i.CampaignId == campaignId && i.SponsorId == sponsorId && i.CounseledProgramId == counseledProgramId);
        }

        public CampaignSponsorProgramDTO GetCampaignSponsorProgram(int campaignId)
        {
            return this.SingleOrDefault(i => i.CampaignId == campaignId);
        }
    }
}
