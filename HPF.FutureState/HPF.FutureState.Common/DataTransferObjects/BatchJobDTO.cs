using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public enum RequestorType{Servicer, FundingSource, Agency, CallCenter};
    public enum JobFrequency { Daily = 1, Weekly = 7};

    [Serializable]
    public class BatchJobDTO:BaseDTO
    {
        public int BatchJobId { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public JobFrequency JobFrequency { get; set; }
        DateTime jobStartDate;
        public DateTime JobStartDate 
        {
            get { return jobStartDate; }
            set { jobStartDate = new DateTime(value.Year, value.Month, value.Day); }
        }
        DateTime jobLastStartDt;
        public DateTime JobLastStartDt 
        {
            get { return jobLastStartDt; }
            set { jobLastStartDt = new DateTime(value.Year, value.Month, value.Day); }
        }
        public RequestorType RequestorType { get; set; }
        public int RequestorId { get; set; }
        string outputFormat;
        public string OutputFormat 
        {
            get { return outputFormat; }
            set { outputFormat = string.IsNullOrEmpty(value) ? null : value.Trim(); }
        }
        public string OutputDestination { get; set; }
    }
}
