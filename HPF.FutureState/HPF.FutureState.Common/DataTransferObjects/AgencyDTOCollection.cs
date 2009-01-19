using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class AgencyDTOCollection:BaseDTOCollection<AgencyDTO>
    {
        public String GetAgencyName(int agencyID)
        {
            foreach (AgencyDTO agency in Items)
            {
                if (agency.AgencyID == agencyID.ToString())
                    return agency.AgencyName;
            }

            return string.Empty;
        }
    }
}
