using System;
using System.Collections.Generic;
using System.Text;

namespace HPF.SharePointAPI.BusinessEntity
{
    public class FannieMaeInfo : BaseObject
    {
        public string FileName { get; set; }
        public DateTime? StartDt { get; set; }
        public DateTime? EndDt { get; set; }
    }
}
