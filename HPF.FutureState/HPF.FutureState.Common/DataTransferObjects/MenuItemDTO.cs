using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class MenuItemDTO:BaseDTO
    {
        public int ItemId { get; set; }
        public int GroupId { get; set; }
        public string ItemName { get; set; }
        public int ItemSearchOrder { get; set; }
        public string ItemTarget { get; set; }
        public char PermissionValue { get; set; }
        public bool Visible { get; set; }
    }
}
