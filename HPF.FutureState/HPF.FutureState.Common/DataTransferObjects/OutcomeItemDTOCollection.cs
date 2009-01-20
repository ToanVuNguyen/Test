using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class OutcomeItemDTOCollection : BaseDTOCollection<OutcomeItemDTO>
    {
        public OutcomeItemDTO ItemAt(int outcomeItemId)
        {
            foreach (OutcomeItemDTO item in this)
            {
                if (item.OutcomeItemId == outcomeItemId)
                    return item;
            }
            return null;
        }
    }
}
