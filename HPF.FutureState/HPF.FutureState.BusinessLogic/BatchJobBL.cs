using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;

namespace HPF.FutureState.BusinessLogic
{
    public class BatchJobBL
    {
        private static readonly BatchJobBL instance = new BatchJobBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static BatchJobBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected BatchJobBL()
        {            
        }

        /// <summary>
        /// Main function: Read job table and process all jobs and write email + log when error orcur
        /// </summary>
        public void ProcessBatchJobs()
        {            
            BatchJobDTOCollection batchJobs = BatchJobDAO.Instance.ReadBatchJobs();
            int rowCount = 0;
            foreach (BatchJobDTO job in batchJobs)
            {
                if (!DetermineTodayBatchJob(job)) continue;
                
                if(!job.OutputFormat.Equals("XML"))
                    throw ExceptionProcessor.GetHpfExceptionForBatchJob(new Exception("Error: Repport format [" + job.OutputFormat + "] is not supported in batch job now." + job.JobName), job.BatchJobId.ToString(), "ProcessBatchJobs");

                if (job.JobName.Equals(Constant.SERVICER_DAILY_SUMMARY))
                    rowCount = GenerateServicerDailySummary(job);
                else if (job.JobName.Equals(Constant.FANNIE_MAE_WEEKLY_REPORT))
                    rowCount = GenerateFannieMaeWeeklyReport(job);
                else 
                    throw ExceptionProcessor.GetHpfExceptionForBatchJob(new Exception("Error: Invalid job name for [" + job.JobName + "]"), job.BatchJobId.ToString(), "ProcessBatchJobs");
                
                InsertBatchJobLog(job, rowCount, Status.SUCCESS);
                UpdateBatchJobStartAndLastRunDates(job);                
            }
        }

        private bool DetermineTodayBatchJob(BatchJobDTO batchJobs)
        {            
            return DateTime.Today.Year.Equals(batchJobs.JobStartDate.Year) &&
                DateTime.Today.Month.Equals(batchJobs.JobStartDate.Month) &&
                DateTime.Today.Day.Equals(batchJobs.JobStartDate.Day);
        }
        /// <summary>
        /// Generate Summary report with batch job input
        /// </summary>
        private int GenerateServicerDailySummary(BatchJobDTO batchjob)
        {
            try
            {
                string xmlbuffer = "<DailySummaryReport>";

                //Seach all marched fc with start date and requestor id
                int[] fcIds = BatchJobDAO.Instance.SearchDailySummaryForeclosureCase(batchjob);

                foreach (int fcId in fcIds)
                    xmlbuffer += GenerateDailySummaryXMLOutputReportXML(fcId);

                xmlbuffer += "</DailySummaryReport>";
                SendDailySummaryToHPFPortal(xmlbuffer, batchjob);                

                return fcIds.Length;
            }
            catch (Exception ex)
            {
                InsertBatchJobLog(batchjob, 0, Status.FAIL);
                throw ExceptionProcessor.GetHpfExceptionForBatchJob(ex, batchjob.BatchJobId.ToString(), "GenerateServicerDailySummary");
            }                       
        }


        private string GenerateDailySummaryXMLOutputReportXML(int fcId)
        {
            string xmlString = "<ForeclosureCaseSummary>";            
            xmlString += BatchJobDAO.Instance.GenerateForclosureCaseXML(fcId);
            xmlString += BatchJobDAO.Instance.GenerateCaseLoanXML(fcId);
            xmlString += BatchJobDAO.Instance.GenerateBudgetXML(fcId);
            xmlString += BatchJobDAO.Instance.GenerateBudgetAssetXML(fcId);
            xmlString += BatchJobDAO.Instance.GenerateOutcomeXML(fcId);
            xmlString += "</ForeclosureCaseSummary>";

            return xmlString;
        }

        /// <summary>
        /// Generate FannieMae Weekly Summary report with batch job input
        /// </summary>
        private int GenerateFannieMaeWeeklyReport(BatchJobDTO batchjob)
        {
            try
            {
                int callCount = 0;
                string xmlbuffer = BatchJobDAO.Instance.GenerateFannieMaeWeeklyXML(batchjob);
                if (string.IsNullOrEmpty(xmlbuffer))                
                    xmlbuffer = "<CallWeeklyReport></CallWeeklyReport> ";
                    
                //Send to sharepoint
                SendFannieMaeWeeklyToHPFPortal(xmlbuffer, batchjob);                

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlbuffer);
                XmlNodeList list = doc.GetElementsByTagName("Call");
                if (list != null)
                    callCount = list.Count;

                return callCount;
            }
            catch (Exception ex)
            {
                InsertBatchJobLog(batchjob, 0, Status.FAIL);
                throw ExceptionProcessor.GetHpfExceptionForBatchJob(ex, batchjob.BatchJobId.ToString(), "GenerateServicerDailySummary");
            }            
        }

        private void SendDailySummaryToHPFPortal(string xmlbuffer, BatchJobDTO job)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            ServicerDTO servicer = GetServer(job.RequestorId);
            HPFPortalCounselingSummary summary = new HPFPortalCounselingSummary();
            summary.SPFolderName = servicer.SPFolderName;
            if (!string.IsNullOrEmpty(job.OutputDestination))
                summary.SPFolderName += "/" + job.OutputDestination;
            summary.ReportFile = encoding.GetBytes(xmlbuffer);
            summary.ReportFileName = string.Format("DailySummaryReport_{0}_{1}-{2}-{3}.xml", servicer.ServicerName, job.LastJobEndDate.Month, job.LastJobEndDate.Day, job.LastJobEndDate.Year);
            HPFPortalGateway.SendSummary(summary);
        }

        private void SendFannieMaeWeeklyToHPFPortal(string xmlbuffer, BatchJobDTO job)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();            
            HPFPortalFannieMae fannieMae = new HPFPortalFannieMae();
            fannieMae.ReportFile = encoding.GetBytes(xmlbuffer);
            fannieMae.EndDt = job.LastJobEndDate.AddDays((int)job.JobFrequency);
            fannieMae.ReportFileName = string.Format("WeeklyCallReport_{0}-{1}-{2}.xml", job.LastJobEndDate.Month, job.LastJobEndDate.Day, job.LastJobEndDate.Year);
            fannieMae.SPFolderName = job.OutputDestination;
            fannieMae.StartDt = job.LastJobEndDate;

            HPFPortalGateway.SendFannieMaeReport(fannieMae);
        }

        /// <summary>
        /// Insert Batch job log after finish job
        /// </summary>
        /// <param name="batchJobLog"></param>
        private void InsertBatchJobLog(BatchJobDTO batchJob, int rowCount, Status status)
        {
            BatchJobLogDTO batchJobLog = new BatchJobLogDTO();
            batchJobLog.SetInsertTrackingInformation("System");
            batchJobLog.BatchJobId = batchJob.BatchJobId;
            batchJobLog.JobResult = status;
            batchJobLog.RecordCount = rowCount;

            batchJobLog.JobNotes = string.Format(batchJob.JobName + " is executed on {0} resulting {1} records for requestor {2} from date {3} to {4}",
                                                DateTime.Now, rowCount, batchJob.RequestorId, batchJob.LastJobEndDate, batchJob.LastJobEndDate.AddDays((int)batchJob.JobFrequency));

            BatchJobDAO.Instance.InsertBatchJobLog(batchJobLog);
        }

        /// <summary>
        /// Update start job date and last start date job after finish job
        /// </summary>
        /// <param name="batchJob"></param>
        private void UpdateBatchJobStartAndLastRunDates(BatchJobDTO batchJob)
        {
            batchJob.LastJobEndDate = batchJob.LastJobEndDate.AddDays((int)batchJob.JobFrequency);
            batchJob.JobStartDate = batchJob.JobStartDate.AddDays((int)batchJob.JobFrequency);  
            batchJob.SetUpdateTrackingInformation("System");
            BatchJobDAO.Instance.UpdateBatchJobStartAndLastRunDates(batchJob);
        }

        private ServicerDTO GetServer(int servicerId)
        {
            return LookupDataBL.Instance.GetServicers().GetServicerById(servicerId);
        }
    }
}
