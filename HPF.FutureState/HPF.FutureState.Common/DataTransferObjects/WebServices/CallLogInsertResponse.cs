﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{    
    [Serializable]    
    public class CallLogInsertResponse : BaseResponse
    {
        //public CallLogWSDTO CallLog { get; set; }
        public string CallLogID { get; set; }
    }
}
