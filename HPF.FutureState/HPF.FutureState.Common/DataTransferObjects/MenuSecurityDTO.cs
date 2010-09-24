using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class MenuSecurityDTO : BaseDTO
    {
        public string Target { get; set; }
        public char Permission { get; set; }
        public int? MenuSecurityId { get; set; }
        public int? MenuItemId { get; set; }
        public string MenuName { get; set; }
        public int? HpfUserId { get; set; }
        public byte StatusChanged { get; set; }
    }
}
