using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.Utils;



namespace HPF.FutureState.BusinessLogic
{
    public class EmailSummaryBL
    {
        private static readonly EmailSummaryBL instance = new EmailSummaryBL();
        public static EmailSummaryBL Instance
        {
            get { return instance; }
        }
        protected EmailSummaryBL()
        { }
        public void SendEmailWithAttachment(string SendTo, string Subject, string Body, string CaseID)
        {
            try
            {
                    HPFSendMail hpfSendMail = new HPFSendMail();
                    ReportingExporter reportExport = new ReportingExporter();
                    hpfSendMail.To = SendTo;
                    hpfSendMail.Subject = Subject;
                    hpfSendMail.Body = Body;
                    reportExport.ReportPath = @"HPF_Report/rpt_CounselingSummary";
                    reportExport.SetReportParameter("pi_fc_id", CaseID);
                    byte[] attachContent = reportExport.ExportToPdf();
                    hpfSendMail.AddAttachment("hpf_report.pdf", attachContent);
                    hpfSendMail.Send();
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
