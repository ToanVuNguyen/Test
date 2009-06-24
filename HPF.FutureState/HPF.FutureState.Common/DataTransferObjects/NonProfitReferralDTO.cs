using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class NonProfitReferralDTO :BaseDTO
    {
        public string Id { get; set; }
        public string ReferralOrgName { get; set; }
        public string ReferralOrgState { get; set; }
        public string ReferralOrgZip { get; set; }
        public string ReferralContactEmail { get; set; }
    }
}
