using System;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.DataTransferObjects;

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
        public  string GenerateSubject(int? fc_id)
        {
            string strSubject = "HPF Summary loan#";
            //get info from case loan to get: Loan Status and Loan Num
            CaseLoanDTOCollection caseLoanDTOCol = CaseLoanBL.Instance.RetrieveCaseLoan(fc_id);
            //get foreclosurecase dto info
            ForeclosureCaseDTO foreclosurecaseInfo = ForeclosureCaseBL.Instance.GetForeclosureCase(fc_id);
            
            strSubject += caseLoanDTOCol[0].AcctNum + "/" + foreclosurecaseInfo.PropZip;

            string loanDelinqStatus = caseLoanDTOCol[0].LoanDelinqStatusCd;
            if (foreclosurecaseInfo.ForSaleInd == "Y" || loanDelinqStatus == "120+")
                strSubject += " ,priority URGENT";
            return strSubject;

        }
    }
}
