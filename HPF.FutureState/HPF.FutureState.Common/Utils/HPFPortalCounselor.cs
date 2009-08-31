using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.Common.Utils
{
    public class HPFPortalCounselor
    {
        public string SPFolderName { get; set; }
        public CounselorDTOCollection Counselors { get; set; }
    }
}
