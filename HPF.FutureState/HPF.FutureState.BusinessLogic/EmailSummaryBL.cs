using System;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;

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

        public void SendEmailSummaryReport(string sendTo, string subject, string body, int fc_id, string fileName)
        {
            var hpfSendMail = GetHpfSendMail();
            var pdfSummaryReport = GetPdfSummaryReport(fc_id);
            //
            hpfSendMail.To = sendTo;
            hpfSendMail.Subject = subject;
            hpfSendMail.Body = body;
            hpfSendMail.AddAttachment(fileName, pdfSummaryReport);
            hpfSendMail.Send();
        }

        public void SendEmailSummaryReport(string sendTo, string subject, string body, int fc_id)
        {

            SendEmailSummaryReport(sendTo, subject, body, fc_id, HPF_ATTACHMENT_REPORT_FILE_NAME);

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

        public string CreateEmailSummarySubject(int? fc_id)
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

        public SendSummaryResponse ProcessWebServiceSendSummary(SendSummaryRequest sendSummary, int agencyID)
        {
            var response = new SendSummaryResponse();

            int fc_Id = sendSummary.FCId;

            ForeclosureCaseDTO foreclosureCase = ForeclosureCaseBL.Instance.GetForeclosureCase(fc_Id);

            ExceptionMessageCollection errorList = ValidateSendSummaryRequest(foreclosureCase, sendSummary, agencyID);

            if (errorList.Count>0)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages = errorList;

                return response;
            }

            string fileName;

            CaseLoanDTO caseLoan=CaseLoanBL.Instance.Retrieve1stCaseLoan(fc_Id);

            //loan num>_<lname>_<1st initial>.pdf

            fileName = caseLoan.AcctNum + "_" + foreclosureCase.BorrowerLname + "_" +
                       foreclosureCase.BorrowerFname.Substring(1, 1) + ".pdf";


            SendEmailSummaryReport(sendSummary.EmailToAddress,
                                    sendSummary.EmailSubject + Constant.HPF_SECURE_EMAIL, 
                                    sendSummary.EmailBody, 
                                    sendSummary.FCId, 
                                    fileName);

            //log to activity log
            ActivityLogDTO activityLog = ActivityLogBL.Instance.CreateSendSummaryWSActivityLog(sendSummary);
            activityLog.SetInsertTrackingInformation(sendSummary.SenderId.ToString());
            ActivityLogBL.Instance.InsertActivityLog(activityLog);

            response.Status = ResponseStatus.Success;

            return response;
        }

        private ExceptionMessageCollection ValidateSendSummaryRequest(ForeclosureCaseDTO foreclosureCase, SendSummaryRequest sendSummary, int agencyID)
        {
            ExceptionMessageCollection errorCollection = new ExceptionMessageCollection();

            if (sendSummary.EmailToAddress == null)
                errorCollection.AddExceptionMessage("ERR495", "The EmailToAddress cannot be null or empty.");
            else
            {
                if (sendSummary.EmailToAddress.Trim().Length == 0)
                    errorCollection.AddExceptionMessage("ERR495", "The EmailToAddress cannot be null or empty.");
            }

            if (sendSummary.SenderId == null)
                errorCollection.AddExceptionMessage("ERR496", "The SenderId cannot be null or empty.");
            else
            {
                if (sendSummary.SenderId.Trim().Length == 0)
                    errorCollection.AddExceptionMessage("ERR496", "The SenderId cannot be null or empty.");
            }

            if (foreclosureCase == null)
                errorCollection.AddExceptionMessage("ERR497", "The FCId supplied is invalid, no foreclosure case found.");

            else
            {
                if (foreclosureCase.AgencyId != agencyID)
                    errorCollection.AddExceptionMessage("ERR498", "The foreclosure case of FCId supplied does not belong to your agency.");

                if (foreclosureCase.CompletedDt == null)
                    errorCollection.AddExceptionMessage("ERR499", "Summary report cannot be sent while the case is not completed.");
    
            }
            
            return errorCollection;
        }
    }
}
