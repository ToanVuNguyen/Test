using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class MenuGroupDTO:BaseDTO
    {
        public int? GroupId {get;set;}
        public string GroupName { get; set; }
        public int? GroupSortOrder { get; set; }
        public string GroupTarget { get; set; }

        public MenuItemDTOCollection MenuItemList { get; private set; }

        public MenuGroupDTO()
        {
            MenuItemList = new MenuItemDTOCollection();
        }
    }
    
}
