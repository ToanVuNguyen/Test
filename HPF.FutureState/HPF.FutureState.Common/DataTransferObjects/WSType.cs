using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public enum WSType
    {
        Agency = 0,
        CallCenter = 1,
        Any = 2
    }

    public enum ReportFormat 
    { 
        NONE = 0, 
        PDF = 1, 
        EXCEL = 2, 
        CVS = 3 
    };
}
