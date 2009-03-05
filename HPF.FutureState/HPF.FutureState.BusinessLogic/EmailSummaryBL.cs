using System;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils.Exceptions;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;

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
            StringBuilder strSubject = new StringBuilder();
           
            //get info from case loan to get: Loan Status and Loan Num
            var caseLoanDTOCol = CaseLoanBL.Instance.RetrieveCaseLoan(fc_id);
            //get foreclosurecase dto info
            var foreclosurecaseInfo = ForeclosureCaseBL.Instance.GetForeclosureCase(fc_id);
            //
            strSubject.Append("HPF Summary loan#");
            strSubject.Append(caseLoanDTOCol[0].AcctNum);
            strSubject.Append("/");
            strSubject.Append(foreclosurecaseInfo.PropZip);

            var loanDelinqStatus = caseLoanDTOCol[0].LoanDelinqStatusCd;
            if (foreclosurecaseInfo.FcNoticeReceiveInd == "Y" || loanDelinqStatus == "120+")
                strSubject.Append(" ,priority URGENT");
            return strSubject.ToString();

        }

        public SendSummaryResponse ProcessWebServiceSendSummary(SendSummaryRequest sendSummary, int agencyID)
        {

            var response = new SendSummaryResponse();
            ExceptionMessageCollection errorList = new ExceptionMessageCollection();

            if (sendSummary.FCId == null)
            {
                errorList.AddExceptionMessage(ErrorMessages.ERR0805, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0805));

                response.Status = ResponseStatus.Fail;
                response.Messages = errorList;

                return response;
            }

            int fc_Id = sendSummary.FCId;

            ForeclosureCaseDTO foreclosureCase = ForeclosureCaseBL.Instance.GetForeclosureCase(fc_Id);

            errorList = ValidateSendSummaryRequest(foreclosureCase, sendSummary, agencyID, fc_Id);

            if (errorList.Count>0)
            {
                response.Status = ResponseStatus.Fail;
                response.Messages = errorList;

                return response;
            }

            string fileName;

            CaseLoanDTO caseLoan=CaseLoanBL.Instance.Retrieve1stCaseLoan(fc_Id);

            //<loan num>_<lname>_<1st initial>.pdf

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

        private ExceptionMessageCollection ValidateSendSummaryRequest(ForeclosureCaseDTO foreclosureCase, SendSummaryRequest sendSummary, int agencyID, int fcId)
        {
            // validate field length first
            var errorCollection = new ExceptionMessageCollection { HPFValidator.ValidateToGetExceptionMessage(sendSummary, Constant.RULESET_LENGTH) };

            if (sendSummary.EmailToAddress == null)
                errorCollection.AddExceptionMessage(ErrorMessages.ERR0800, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0800));
            else
            {
                if (sendSummary.EmailToAddress.Trim().Length == 0)
                    errorCollection.AddExceptionMessage(ErrorMessages.ERR0800, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0800));
            }

            if (sendSummary.SenderId == null)
                errorCollection.AddExceptionMessage(ErrorMessages.ERR0801, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0801));
            else
            {
                if (sendSummary.SenderId.Trim().Length == 0)
                    errorCollection.AddExceptionMessage(ErrorMessages.ERR0801, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0801));
            }

            if (foreclosureCase == null)
                errorCollection.AddExceptionMessage(ErrorMessages.ERR0802, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0802, fcId));

            else
            {
                if (foreclosureCase.AgencyId != agencyID)
                    errorCollection.AddExceptionMessage(ErrorMessages.ERR0803, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0803, fcId));

                if (foreclosureCase.CompletedDt == null)
                    errorCollection.AddExceptionMessage(ErrorMessages.ERR0804, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0804));
    
            }
            
            return errorCollection;
        }
    }
}
