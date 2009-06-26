using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{    
    public class SummaryRetrieveRequest: BaseRequest
    {
        /// <summary>
        /// Foreclosure case id
        /// </summary>
        public int? FCId { get; set; }
        /// <summary>
        /// If ReportOutput= None then output is ForeclosureSet
        /// Otherwise the output is report summary buffer
        /// </summary>
        public string OutputFormat { get; set; }
    }
}
