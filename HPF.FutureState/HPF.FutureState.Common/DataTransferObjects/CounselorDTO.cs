using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CounselorDTO: BaseDTO
    {
        public string AgencyName { get; set; }
        public string CounselorLastName { get; set; }
        public string counselorFirstName { get; set; }
        public string CounselorEmail { get; set; }
        public string CounselorPhone { get; set; }
        public string CounselorExt { get; set; }
    }
}
