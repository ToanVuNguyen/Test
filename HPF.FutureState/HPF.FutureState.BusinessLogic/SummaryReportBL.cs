using System;
using System.Linq;
using System.Text;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace HPF.FutureState.BusinessLogic
{
    public class SummaryReportBL : BaseBusinessLogic
    {
        private static readonly SummaryReportBL instance = new SummaryReportBL();
        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static SummaryReportBL Instance
        {
            get { return instance; }
        }

        protected SummaryReportBL()
        {
            
        }

        /// <summary>
        /// Generate summary report as pdf format
        /// </summary>
        /// <param name="fc_id"></param>
        /// <returns>PDF file buffer</returns>
        public byte[] GenerateSummaryReport(int? fc_id)
        {            
            var reportExport = new ReportingExporter
                                   {
                                       ReportPath = HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_COUNSELINGSUMMARY_REPORT)
                                   };
            reportExport.SetReportParameter("pi_fc_id", fc_id.ToString());
            var pdfReport = reportExport.ExportToPdf();
            return pdfReport;            
        }
        
        /// <summary>
        /// Get All servicer of a case
        /// </summary>
        /// <param name="fcId"></param>
        /// <returns></returns>
        public ServicerDTOCollection GetServicerbyFcId(int? fcId)
        {
            return ServicerDAO.Instance.GetServicersByFcId(fcId);
        }        

        public void UpdateSummarySentDateTime(int? fcId)
        {
            var foreclosureCaseSetDAO = ForeclosureCaseSetDAO.CreateInstance();
            foreclosureCaseSetDAO.UpdateSendSummaryDate(fcId);
        }

        public DateTime? GetCurrentSummarySentDateTime(int? fcId)
        {
            return ForeclosureCaseSetDAO.CreateInstance().GetSummarySendDt(fcId);            
        }

        /// <summary>
        /// Process a completed case summary
        /// </summary>
        /// <param name="fc_id"></param>
        public void SendCompletedCaseSummary(int? fc_id)
        {
            //<Prepare data>
            var foreclosureCase = GetForeclosureCase(fc_id);
            var caseLoan = GetCaseLoans1St(fc_id);
            var servicers = GetServicerbyFcId(fc_id);
            var primaryServicer = servicers.GetServicerById(caseLoan.ServicerId);
            //</Prepare data>           
            //Send summary
            switch (primaryServicer.SecureDeliveryMethodCd)
            {
                case Constant.SECURE_DELIVERY_METHOD_SECEMAIL:
                    SendSummaryMailToServicer(foreclosureCase, primaryServicer, caseLoan);
                    break;
                case Constant.SECURE_DELIVERY_METHOD_PORTAL:
                    SendSummaryToHPFPortal(foreclosureCase, primaryServicer, caseLoan);
                    break;
            }            
            //Update Summary sent datetime if any
            if (primaryServicer.SecureDeliveryMethodCd != Constant.SECURE_DELIVERY_METHOD_NOSEND)
            {
                UpdateSummarySentDateTime(fc_id);
            }
        }

        /// <summary>
        /// Send ConselingSummary Mail to Servicer
        /// </summary>
        /// <param name="foreclosureCase"></param>
        /// <param name="servicer"></param>
        /// <param name="caseLoan"></param>
        private static void SendSummaryMailToServicer(ForeclosureCaseDTO foreclosureCase, ServicerDTO servicer, CaseLoanDTO caseLoan)
        {
            
            try
            {                
                var attachmentFileName = BuildPdfAttachmentFileName(foreclosureCase, caseLoan);
                EmailSummaryBL.Instance.SendEmailSummaryReport(foreclosureCase.FcId, servicer.ContactEmail,
                                                               attachmentFileName);                
            }
            catch(Exception Ex)
            {
                Logger.Write(Ex.Message, "General");
                Logger.Write(Ex.StackTrace, "General");
            }
        }
        /// <summary>
        /// Send ConselingSummary information to HPF Portal.
        /// </summary>
        /// <param name="foreclosureCase"></param>
        /// <param name="servicer"></param>
        /// <param name="caseLoan"></param>
        private void SendSummaryToHPFPortal(ForeclosureCaseDTO foreclosureCase, ServicerDTO servicer, CaseLoanDTO caseLoan)
        {            
            var hpfSharepointSummary = new HPFPortalConselingSummary
                                           {
                                               ReportFile = GenerateSummaryReport(foreclosureCase.FcId),
                                               LoanNumber = caseLoan.AcctNum,
                                               CompletedDate = foreclosureCase.CompletedDt,
                                               ForeclosureSaleDate = foreclosureCase.FcSaleDate,
                                               Servicer = servicer.ServicerName,
                                               Delinquency = caseLoan.LoanDelinqStatusCd,
                                               ReportFileName = BuildPdfAttachmentFileName(foreclosureCase, caseLoan)
                                           };            
            HPFPortalGateway.SendSummary(hpfSharepointSummary);            
        }

        /// <summary>
        /// Build Pdf file name to attach to the e-mail that will be sent to servicer.
        /// </summary>
        /// <param name="foreclosureCase"></param>
        /// <param name="caseLoan"></param>
        /// <returns></returns>
        private static string BuildPdfAttachmentFileName(ForeclosureCaseDTO foreclosureCase, CaseLoanDTO caseLoan)
        {            
            var pdfFile = new StringBuilder();            
            pdfFile.Append(caseLoan.AcctNum);
            pdfFile.Append("_");
            pdfFile.Append(foreclosureCase.BorrowerLname);
            pdfFile.Append("_");
            pdfFile.Append(foreclosureCase.BorrowerFname.Substring(1, 1));
            if (caseLoan.LoanDelinqStatusCd == "120+" || foreclosureCase.FcNoticeReceiveInd == "Y")
            {
                pdfFile.Append("_");
                pdfFile.Append("URGENT");
            }
            if (foreclosureCase.SummarySentDt!=null)
            {
                pdfFile.Append("_");
                pdfFile.Append("REVISED");
            }
            pdfFile.Append(".PDF");
            //
            return pdfFile.ToString();
        }

        private static CaseLoanDTO GetCaseLoans1St(int? fc_id)
        {
            var caseLoans = CaseLoanBL.Instance.RetrieveCaseLoan(fc_id);
            return caseLoans.SingleOrDefault(i => i.Loan1st2nd == "1st");
        }

        private static ForeclosureCaseDTO GetForeclosureCase(int? fc_id)
        {
            return ForeclosureCaseBL.Instance.GetForeclosureCase(fc_id);
        }

    }
}
