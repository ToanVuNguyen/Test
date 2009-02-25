using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class ReconciliationDTOCollection:BaseDTOCollection<ReconciliationDTO>
    {
        public string FundingSourceId { get; set; }
    }
}
