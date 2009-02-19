using System;
using System.Collections.Generic;
using System.Text;

namespace HPF.SharePointAPI.Enum
{
    public enum Delinquency
    {
        LessThan30,
        From30To59,
        From60To89,
        From90To119,
        GreaterThan120
    }

    public enum ReviewStatus
    {
        PendingReview,
        Reviewed
    }
}
