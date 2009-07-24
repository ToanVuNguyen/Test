using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{    
    public class SummaryRetrieveRequest: BaseRequest
    {
        string outputReport;
        /// <summary>
        /// Foreclosure case id
        /// </summary>
        public int? FCId { get; set; }
        /// <summary>
        /// If ReportOutput= None then output is ForeclosureSet
        /// Otherwise the output is report summary buffer
        /// </summary>       /
        public string OutputFormat 
        {
            get { return outputReport; }
            set
            {
                outputReport = string.IsNullOrEmpty(value) ? null : value.ToUpper().Trim();
            }
        }
    }
}
