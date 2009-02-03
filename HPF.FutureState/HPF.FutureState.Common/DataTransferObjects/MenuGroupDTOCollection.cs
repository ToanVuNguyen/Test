using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class MenuGroupDTOCollection:BaseDTOCollection<MenuGroupDTO>
    {
        public MenuGroupDTO FindMenuGroupDTO(int groupId)
        {
            return this.SingleOrDefault(item => item.GroupId == groupId);
        }
    }
}
