using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class RefCodeItemDTO : BaseDTO
    {        
        public int RefCodeItemId { get; set; }

        public string RefCodeSetName { get; set; }

        public string Code { get; set; }

        public string CodeDesc { get; set; }

        public string CodeComment { get; set; }

        public int SortOrder { get; set; }

        public string ActiveInd { get; set; }           
    }
}
