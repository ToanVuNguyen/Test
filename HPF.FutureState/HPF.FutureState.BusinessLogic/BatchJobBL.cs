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
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.IO;

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
            
            foreach (BatchJobDTO job in batchJobs)
            {
                if (!DetermineTodayBatchJob(job)) continue;
                ProcessBatchJob(job);           
            }
        }

        public void ProcessBatchJob(BatchJobDTO job)
        {
            int rowCount = 0;
            try
            {
                if (job.JobName.Equals(Constant.SERVICER_DAILY_SUMMARY))
                    rowCount = GenerateServicerDailySummary(job);
                else if (job.JobName.Equals(Constant.FANNIE_MAE_WEEKLY_REPORT))
                    rowCount = GenerateFannieMaeWeeklyReport(job);
                else if (job.JobName.Equals(Constant.COUNSELOR_LIST_GENERATION))
                    rowCount = GenerateCounsorList(job);
                else if (job.JobName.Equals(Constant.MHA_ESCALALATION_IMPORT))
                    rowCount = ImportMHAEscalationData();
                else if (job.JobName.Equals(Constant.MHA_HELP_IMPORT))
                    rowCount = ImportMHAHelpData();
                else if (job.JobName.Equals(Constant.COUNSELING_SUMMARY_AUDIT_IMPORT))
                    rowCount = ImportAuditLog();
                else if (job.JobName.Equals(Constant.COMPLETED_COUNSELING_DETAIL_REPORT))
                    rowCount = SendCompletedCounselingDetailReportToPortal(job);
                else return;
                //    throw ExceptionProcessor.GetHpfExceptionForBatchJob(new Exception("Error: Invalid job name for [" + job.JobName + "]"), job.BatchJobId.ToString(), "ProcessBatchJobs");

                InsertBatchJobLog(job, rowCount, Status.SUCCESS);
                UpdateBatchJobStartAndLastRunDates(job);                
            }
            catch(Exception Ex)
            {
                //Log Error down the text file
                ExceptionProcessor.HandleException(Ex);
                //Send E-mail to support
                var hpfSupportEmail = HPFConfigurationSettings.HPF_SUPPORT_EMAIL;
                var mail = new HPFSendMail
                {
                    To = hpfSupportEmail,
                    Subject = "Batch Manager Error: found error in batch job id " + job.BatchJobId,
                    Body = "Messsage: " + Ex.Message + "\nTrace: " + Ex.StackTrace
                };
                mail.Send();
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

            if (!string.IsNullOrEmpty(job.OutputDestination))
                summary.SPFolderName = job.OutputDestination;
            else
                summary.SPFolderName = servicer.SPFolderName;

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
            if (batchJob.JobFrequency == JobFrequency.Monthly)
            {
                batchJob.JobStartDate = batchJob.JobStartDate.AddMonths(1);
                batchJob.LastJobEndDate = batchJob.LastJobEndDate.AddMonths(1);
            }
            else
            {
                batchJob.LastJobEndDate = batchJob.LastJobEndDate.AddDays((int)batchJob.JobFrequency);
                batchJob.JobStartDate = batchJob.JobStartDate.AddDays((int)batchJob.JobFrequency);
            }
                  
            batchJob.SetUpdateTrackingInformation("System");
            BatchJobDAO.Instance.UpdateBatchJobStartAndLastRunDates(batchJob);
        }

        private int GenerateCounsorList(BatchJobDTO batchJob)
        {
            try
            {
                HPFPortalCounselor hpfCounselor = new HPFPortalCounselor();
                hpfCounselor.SPFolderName = batchJob.OutputDestination;
                hpfCounselor.Counselors = BatchJobDAO.Instance.GenerateCounsorList(batchJob);
                HPFPortalGateway.GenerateCouncelorList(hpfCounselor);
                return hpfCounselor.Counselors.Count;
            }
            catch (Exception ex)
            {
                InsertBatchJobLog(batchJob, 0, Status.FAIL);
                throw ex;
            }
        }
        private ServicerDTO GetServer(int servicerId)
        {
            return LookupDataBL.Instance.GetServicers().GetServicerById(servicerId);
        }

        public int ImportMHAEscalationData()
        {
            MHAEscalationDTOCollecion mhaCol = HPFPortalGateway.GetMHAEscaltions();
            if (mhaCol.Count > 0)
                BatchJobDAO.Instance.ImportMHAEscalationData(mhaCol);

            return mhaCol.Count;
        }

        public int ImportMHAHelpData()
        {
            MHAHelpDTOCollection mhaCol = HPFPortalGateway.GetMHAHelps();
            if (mhaCol.Count > 0)
                BatchJobDAO.Instance.ImportMHAHelp(mhaCol);

            return mhaCol.Count;
        }

        public int ImportAuditLog()
        {
            CounselingSummaryAuditLogDTOCollection auditLogs = HPFPortalGateway.GetCounselingSummaryAuditLog();
            if (auditLogs.Count > 0)
                BatchJobDAO.Instance.ImportCounselingSummaryAuditLog(auditLogs);

            return auditLogs.Count;
        }

        public int SendCompletedCounselingDetailReportToPortal(BatchJobDTO batchjob)
        {            
            DateTime startDate = batchjob.LastJobEndDate.AddDays(1); //new DateTime(batchjob.JobStartDate.Year, batchjob.JobStartDate.Month, 1).AddMonths(-1);            
            DateTime endDate = new DateTime(batchjob.LastJobEndDate.Year, batchjob.LastJobEndDate.Month, 1).AddMonths(2).AddDays(-1);

            return GenerateCompletedCounselingDetailReport(startDate, endDate, batchjob.OutputDestination);
        }

        public int GenerateCompletedCounselingDetailReport(DateTime startDate, DateTime endDate, string spFolder)
        {
            string filename = Application.StartupPath + @"\Temp\Completed Counseling Detail " + startDate.ToString("MM_dd_yyyy") + " to " + endDate.ToString("MM_dd_yyyy") + ".xls";
            string template = Application.StartupPath + @"\Templates\Template.xls";

            if (File.Exists(template))
                File.Copy(template, filename, true);
            else if (File.Exists(filename))
                File.Delete(filename);

            int count = BatchJobDAO.Instance.GetCompletedCounselingDetailReportData(filename, startDate, endDate);

            if (!string.IsNullOrEmpty(spFolder))
            {
                HPFPortalCompletedCounselingDetail counselingDetail = new HPFPortalCompletedCounselingDetail();
                counselingDetail.SPFolderName = spFolder;
                counselingDetail.ReportFile = ReadFile(filename);
                counselingDetail.ReportFilename = string.Format("Counseling_from_{0}-{1}-{2}_to_{3}-{4}-{5}.xls",
                                                startDate.Month, startDate.Day, startDate.Year,
                                                endDate.Month, endDate.Day, endDate.Year);

                HPFPortalGateway.SendCompletedCounselingDetailReport(counselingDetail);
            }
            return count;
        }

        public static byte[] ReadFile(string filename)
        {
            int offset = 0;            
            FileStream stream = new FileStream(filename, FileMode.Open);
            BinaryReader reader = new BinaryReader(stream);
            
            byte[] data = new byte[stream.Length];
            int remaining = (int)stream.Length;

            while (remaining > 0)
            {
                int read = reader.Read(data, offset, remaining);
                if (read <= 0) 
                    throw new EndOfStreamException
                            (String.Format("End of stream reached with {0} bytes left to read", remaining));                     
                remaining -= read;
                offset += read;
            }
            reader.Close();
            return data;
        }

    }
}
