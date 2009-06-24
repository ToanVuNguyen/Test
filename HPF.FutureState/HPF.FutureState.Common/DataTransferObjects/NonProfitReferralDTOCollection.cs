using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class NonProfitReferralDTOCollection : BaseDTOCollection<NonProfitReferralDTO>
    {
        public NonProfitReferralDTO GetNonProfitReferral(string Id)
        {
            return this.SingleOrDefault(i => i.Id == Id);
        }
    }
}
