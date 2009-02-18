using System;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.BusinessLogic
{
    public class EmailSummaryBL
    {
        private const string HPF_ATTACHMENT_REPORT_FILE_NAME = "hpf_report.pdf";

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

        public void SendEmailSummaryReport(string sendTo, string subject, string body, int fc_id)
        {
            var hpfSendMail = GetHpfSendMail();
            var pdfSummaryReport = GetPdfSummaryReport(fc_id);
            //
            hpfSendMail.To = sendTo;
            hpfSendMail.Subject = subject;
            hpfSendMail.Body = body;
            hpfSendMail.AddAttachment(HPF_ATTACHMENT_REPORT_FILE_NAME, pdfSummaryReport);
            hpfSendMail.Send();
        }

        public void SendEmailSummaryReport(int? fc_id, string sendTo, string attachmentReportFileName)
        {
            var hpfSendMail = GetHpfSendMail();
            var pdfSummaryReport = GetPdfSummaryReport(fc_id);
            //
            hpfSendMail.To = sendTo;
            hpfSendMail.Subject = CreateEmailSummarySubject(Convert.ToInt32(fc_id));
            hpfSendMail.AddAttachment(attachmentReportFileName, pdfSummaryReport);
            hpfSendMail.Send();
        }

        private static byte[] GetPdfSummaryReport(int? fc_id)
        {
            return SummaryReportBL.Instance.GenerateSummaryReport(fc_id);
        }

        private static HPFSendMail GetHpfSendMail()
        {
            return new HPFSendMail();
        }

        public  string CreateEmailSummarySubject(int? fc_id)
        {
            var strSubject = "HPF Summary loan#";
            //get info from case loan to get: Loan Status and Loan Num
            var caseLoanDTOCol = CaseLoanBL.Instance.RetrieveCaseLoan(fc_id);
            //get foreclosurecase dto info
            var foreclosurecaseInfo = ForeclosureCaseBL.Instance.GetForeclosureCase(fc_id);
            
            strSubject += caseLoanDTOCol[0].AcctNum + "/" + foreclosurecaseInfo.PropZip;

            var loanDelinqStatus = caseLoanDTOCol[0].LoanDelinqStatusCd;
            if (foreclosurecaseInfo.ForSaleInd == "Y" || loanDelinqStatus == "120+")
                strSubject += " ,priority URGENT";
            return strSubject;

        }
    }
}
