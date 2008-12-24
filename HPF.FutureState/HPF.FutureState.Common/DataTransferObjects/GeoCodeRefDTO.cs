using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class GeoCodeRefDTO : BaseDTO
    {
        public int GeocodeRefId{ get; set; }

        public string ZipCode{ get; set; }

        public string ZipType{ get; set; }

        public string CityName{ get; set; }

        public string CityType{ get; set; }

        public string CountyName{ get; set; }

        public string CountyFIPS{ get; set; }

        public string StateName{ get; set; }

        public string StateAbbr{ get; set; }

        public string StateFIPS{ get; set; }

        public string MSACode{ get; set; }

        public string AreaCode{ get; set; }

        public string TimeZone{ get; set; }

        public decimal UTC{ get; set; }

        public string DST{ get; set; }

        public string Latitude{ get; set; }

        public string Longitude { get; set; }
    }
}
