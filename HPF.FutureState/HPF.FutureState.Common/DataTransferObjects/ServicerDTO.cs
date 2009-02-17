using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class ServicerDTO : BaseDTO
    {
        public int? ServicerID { get; set; }

        public string ServicerName { get; set; }

        public string SecureDeliveryMethodCd { get; set; }
    }
}
