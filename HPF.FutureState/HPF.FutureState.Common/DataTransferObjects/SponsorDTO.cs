using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class SponsorDTO: BaseDTO
    {
        public int SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string SponsorComment { get; set; }
        public DateTime? EffDt { get; set; }
        public DateTime? ExpDt { get; set; }
    }
}
