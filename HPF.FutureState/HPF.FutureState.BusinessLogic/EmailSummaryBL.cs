using System;
using HPF.FutureState.Common.Utils;

namespace HPF.FutureState.BusinessLogic
{
    public class EmailSummaryBL
    {
        private static readonly EmailSummaryBL instance = new EmailSummaryBL();
        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static EmailSummaryBL Instance
        {
            get { return instance; }
        }

        protected EmailSummaryBL()
        {
            
        }

        public void SendEmailSummaryReport(string SendTo, string Subject, string Body, string CaseID)
        {
            var hpfSendMail = new HPFSendMail();
            var pdfSummaryReport = SummaryReportBL.Instance.GenerateSummaryReport(Convert.ToInt32(CaseID));
            //
            hpfSendMail.To = SendTo;
            hpfSendMail.Subject = Subject;
            hpfSendMail.Body = Body;
            hpfSendMail.AddAttachment("hpf_report.pdf", pdfSummaryReport);
            hpfSendMail.Send();
        }       
    }
}
