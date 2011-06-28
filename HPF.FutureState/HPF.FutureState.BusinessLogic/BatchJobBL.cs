﻿using System;
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
                else if (job.JobName.Equals(Constant.ATT_CALLING_RECORD_IMPORT))
                    rowCount = ImportATTCallingData(job);
                else if (job.JobName.Equals(Constant.SCAM_IMPORT))
                    rowCount = ImportScamData();
                else if (job.JobName.Equals(Constant.AD_HOC))
                    rowCount = AdHoc();
                else if (job.JobName.Equals(Constant.POST_MOD_INCLUSION_IMPORT))
                    rowCount = ImportPostModInclusionData(job);
                else
                    throw ExceptionProcessor.GetHpfExceptionForBatchJob(new Exception("Error: Invalid job name for [" + job.JobName + "]"), job.BatchJobId.ToString(), "ProcessBatchJobs");

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

        public int ImportScamData()
        {
            ScamDTOCollection scamCol = HPFPortalGateway.GetScams();
            if (scamCol.Count > 0)
                BatchJobDAO.Instance.ImportScam(scamCol);
            return scamCol.Count;
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

                if (batchJob.LastJobEndDate.AddDays(1).Month > batchJob.LastJobEndDate.Month)//last day is 29(Fed), 30, 31
                    batchJob.LastJobEndDate = new DateTime(batchJob.LastJobEndDate.Year, batchJob.LastJobEndDate.Month, 1).AddMonths(2).AddDays(-1); //get the next last day of the month
                else
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
        public int AdHoc()
        {
            BatchJobDAO.Instance.AdHoc();
            return 0;
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
            string sheetName = "Completed Counseling Detail1";
            string[] dataHeaders = new string[]{"HPF Case ID","Agency","Completed Date","Agency Case Num","Agency Client Num","Counselor First Name","Counselor Last Name",
                    "Program","Intake Date","Call ID","Case Source","Counseling Duration","Primary Default Reason",
                    "Secondary Default Reason","Borrower First Name","Borrower Last Name",
                    "Mothers Maiden Name","Borrower Last4 SSN","Borrower DOB", "Gender",
                    "Race","Hispanic Ind","Marital Status","Education Level Completed","Military Service",	"Borrower Disabled Ind","Primary Contact No",
                    "Secondary Contact No","Email Address","Co-borrower First Name","Co-borrower Last Name",
                    "Co-borrower Last4 SSN","Co-borrower DOB","Household","Occupants","Owner Occupied Ind",
                    "Income Earners","Household Gross Annual Income","Intake Credit Score","Property Address Line 1","Property Address Line 2",
                    "Property City","Property State","Property Zip","Servicer Consent Ind","Funding Consent Ind","Summary Sent Other Method",
                    "Summary Sent Other Date","Summary Sent Date","Servicer Name","Account Num",
                    "Mortgage Type","ARM Reset Ind","Interest Rate","Term Length","Current Loan Balance",
                    "Loan Delinquency Status","FC Notice Received Ind","HUD Termination Reason",
                    "HUD Termination Date","HUD Outcome","Outcome 1","Outcome 2","Outcome 3"};

            if (File.Exists(template))
                File.Copy(template, filename, true);
            else if (File.Exists(filename))
                File.Delete(filename);

            var dataRows = BatchJobDAO.Instance.GetCompletedCounselingDetailReportData(filename, startDate, endDate);
            ExcelFileWriter.PutToExcel(filename, sheetName, dataHeaders, dataRows);            

            if (!string.IsNullOrEmpty(spFolder))
            {
                HPFPortalCompletedCounselingDetail counselingDetail = new HPFPortalCompletedCounselingDetail();
                counselingDetail.SPFolderName = spFolder;
                counselingDetail.ReportFile = ReadFile(filename);
                counselingDetail.ReportFilename = Path.GetFileName(filename);

                HPFPortalGateway.SendCompletedCounselingDetailReport(counselingDetail);
            }
            return dataRows.Count;
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

        /// <summary>
        /// Import data into database from call data file located in folder
        /// If there is error import file, it will log error and continue import the next file.
        /// It does not stop when found error.
        /// If import file successfuly, the file will be moved to Processed Folder
        /// </summary>
        /// <param name="batchJob"></param>
        /// <returns></returns>
        public int ImportATTCallingData(BatchJobDTO batchJob)
        {
            string[] reportFiles = Directory.GetFiles(batchJob.OutputDestination + @"\Upload\");
            int recordCount = 0;
            //There are no rpt files in upload directory
            if (reportFiles.Length <= 0)
            {
                var hpfSupportEmail = HPFConfigurationSettings.HPF_SUPPORT_EMAIL;
                var mail = new HPFSendMail
                {
                    To = hpfSupportEmail,
                    Subject = "Batch Manager Warning- Import ATT report data",
                    Body = "Error import ATT report file \n" +
                            "Messsage: There are no rpt files in upload directory"
                };
                mail.Send();
                return 0;
            }
            foreach (string reportFile in reportFiles)
            {
                try
                {
                    recordCount += ImportATTCallingData(reportFile);
                    File.Move(reportFile, batchJob.OutputDestination + @"\Processed\" + Path.GetFileName(reportFile));
                }
                catch (Exception ex)
                {                     
                    ExceptionProcessor.HandleException(ex);

                    //Send E-mail to support
                    var hpfSupportEmail = HPFConfigurationSettings.HPF_SUPPORT_EMAIL;
                    var mail = new HPFSendMail
                    {
                        To = hpfSupportEmail,
                        Subject = "Batch Manager Error- Import ATT report data",
                        Body = "Error import ATT report file " + reportFile + "\n--" +
                                "Messsage: " + ex.Message + "\nTrace: " + ex.StackTrace
                    };
                    mail.Send();
                }
            }

            return recordCount;
        }

        /// <summary>
        /// Import data into database from calling data file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public int ImportATTCallingData(string filename)
        {
            int recordCount = 0;
            CallingDataHeaderDTO callHeader = null;                
            CallingDataDAO callDataDAO = CallingDataDAO.CreateInstance();
            try
            {
                int callHeaderId = 0;
                TextReader tr = new StreamReader(filename);
                string strLine = "";
                strLine = tr.ReadLine();                
                callDataDAO.Begin();
                while (strLine != null)
                {
                    if (callHeader == null)
                    {
                        callHeader = CallingDataBL.Instance.ReadHeaderData(strLine);
                        callHeader.SetInsertTrackingInformation("System");
                        strLine = strLine.Substring(CallingDataHeaderDTO.Length, strLine.Length - CallingDataHeaderDTO.Length);
                        callHeaderId = callDataDAO.InsertCallingHeader(callHeader);
                    }
                    CallingDataDetailDTO callDetail = CallingDataBL.Instance.ReadDetailData(strLine);
                    callDetail.CallingHeaderId = callHeaderId;
                    callDetail.SetInsertTrackingInformation("System");
                    callDataDAO.InsertCallingDetail(callDetail);
                    strLine = tr.ReadLine();
                    recordCount++;
                }
                tr.Close();
            }
            catch (Exception ex)
            {
                callDataDAO.Cancel();                
                throw ex;
            }
            finally
            {
                callDataDAO.Commit();
                //Send alert email to support when CallRecordCount field from the header not equal with numbers of detail records
                if (callHeader != null &&callHeader.CallRecordCount != recordCount)
                {
                    //Send E-mail to support
                    var hpfSupportEmail = HPFConfigurationSettings.HPF_SUPPORT_EMAIL;
                    var mail = new HPFSendMail
                    {
                        To = hpfSupportEmail,
                        Subject = "Batch Manager Warning- Import ATT report data",
                        Body = "Warning import ATT report file " + filename + "\n" +
                                "Messsage: CallRecordCount field from the header not equal with numbers of detail records."
                    };
                    mail.Send();
                }
            }
            return recordCount;
        }

        /// <summary>
        /// Import postModInclusion data from all servicers get from config file
        /// </summary>
        /// <param name="batchJob"></param>
        /// <returns></returns>
        public int ImportPostModInclusionData(BatchJobDTO batchJob)
        {
            int recordCount = 0;
            StringBuilder hpfAccessFolder;
            StringBuilder servicerAccessFolder;
            string listPostModInclusionErrorFile = "";
            PostModInclusionDAO postModInclusionDAO = PostModInclusionDAO.CreateInstance();
            try
            {
                string[] servicers = HPFConfigurationSettings.POST_MOD_INCLUSION_SERVICER_LIST.Split(',');
                if (servicers.Length > 0)
                {
                    //Get list of fannieMaeLoanNum from database
                    PostModInclusionBL.Instance.fannieMaeLoanNumExistedList = postModInclusionDAO.GetPostModInclusion();
                    //Get list of servicers
                    PostModInclusionBL.Instance.servicerCollection = ServicerBL.Instance.GetServicers();
                    foreach (string servicer in servicers)
                    {
                        hpfAccessFolder = new StringBuilder();
                        servicerAccessFolder = new StringBuilder();
                        hpfAccessFolder.AppendFormat(@"C:\HPF_Batch_Processed\FNMA_PostMod\{0}\", servicer);
                        servicerAccessFolder.AppendFormat(@"C:\HPF_FTP_Secure\{0}\FNMA_PostMod\", servicer);
                        recordCount += PostModInclusionBL.Instance.ImportPostModInclusionData(hpfAccessFolder.ToString(), servicerAccessFolder.ToString(), ref listPostModInclusionErrorFile);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(listPostModInclusionErrorFile))
                {
                    //Send E-mail to support
                    var hpfSupportEmail = HPFConfigurationSettings.HPF_SUPPORT_EMAIL;
                    var mail = new HPFSendMail
                    {
                        To = hpfSupportEmail,
                        Subject = "Batch Manager Error- Import post mod inclusion data",
                        Body = "Error import post mod inclusion files: " + listPostModInclusionErrorFile
                    };
                    mail.Send();
                }
            }
            return recordCount;
        }

        
    }
}
