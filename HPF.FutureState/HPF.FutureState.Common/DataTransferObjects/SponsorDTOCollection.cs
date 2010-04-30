using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class SponsorDTOCollection: BaseDTOCollection<SponsorDTO>
    {
        public SponsorDTO GetSponsor(int sponsorId)
        {
            return this.SingleOrDefault(i => i.SponsorId == sponsorId);
        }
    }
}
