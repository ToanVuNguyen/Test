using System;
using System.Collections.Generic;
using System.Text;

namespace HPF.SharePointAPI.BusinessEntity
{
    public class CounselorInfo: BaseObject
    {
        public string Title { get; set; }
        public string AgencyName { get; set; }
        public string CounselorLastName { get; set; }
        public string counselorFirstName { get; set; }
        public string CounselorEmail { get; set; }
        public string CounselorPhone { get; set; }
        public string CounselorExt { get; set; }
    }
}
