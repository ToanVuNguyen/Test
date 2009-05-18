using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class SummaryRetrieveResponse: BaseResponse
    {
        /// <summary>
        /// The foreclosure case is existed when request with input ReportOutput = None
        /// </summary>
        public ForeclosureCaseSetDTO ForeclosureCaseSet { get; set; }
        /// <summary>
        /// The buffer file is exixed when request with reportOuput = PDF
        /// </summary>
        public byte[] ReportSummary { get; set; }
    }
}
