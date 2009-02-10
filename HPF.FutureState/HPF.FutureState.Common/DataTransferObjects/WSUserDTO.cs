using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class WSUserDTO
    {
        public int? WsUserId { get; set; }
        public int? AgencyId { get; set; }
        public int? CallCenterId { get; set; }
        public string LoginUsername { get; set; }
        public string LoginPassword { get; set; }
        public string ActiveInd { get; set; }
        public DateTime? CreateDt { get; set; }
        public string CreateUserId { get; set; }
        public string CreateLstAppName { get; set; }
        public DateTime? ChgLstDt { get; set; }
        public string ChgLstUserId { get; set; }
        public string ChgLstAppName { get; set; }
    }
}
