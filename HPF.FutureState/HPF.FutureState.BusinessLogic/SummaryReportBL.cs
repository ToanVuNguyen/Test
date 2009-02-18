using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.DataAccess;

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
            var reportExport = new ReportingExporter();
            reportExport.ReportPath = @"HPF_Report/rpt_CounselingSummary";
            reportExport.SetReportParameter("pi_fc_id", fc_id.ToString());
            var attachContent = reportExport.ExportToPdf();
            return attachContent;
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
            var caseLoans = GetCaseLoans(fc_id);
            var servicers = GetServicerbyFcId(fc_id);
            //</Prepare data>           

            //<Send mail>
            var secureMailServicers = servicers.ExtractServicerByDeliveryMethod(Constant.SECURE_DELIVERY_METHOD_SECEMAIL);
            SendSummaryMailToServicer(foreclosureCase, secureMailServicers, caseLoans);
            //</Send mail>

            //<Send to HPF Sharepoint Portal>
            var portalServicers = servicers.ExtractServicerByDeliveryMethod(Constant.SECURE_DELIVERY_METHOD_PORTAL);
            SendSummaryToHPFPortal(foreclosureCase, portalServicers, caseLoans);
            //</Send to HPF Sharepoint Portal>

            //Update Summary sent datetime if any
            if(secureMailServicers.Count > 0 || portalServicers.Count>0)
            {
                UpdateSummarySentDateTime(fc_id);
            }
        }        

        private static void SendSummaryMailToServicer(ForeclosureCaseDTO foreclosureCase, ServicerDTOCollection servicers, CaseLoanDTOCollection caseLoans)
        {
            foreach (var servicer in servicers)
            {                
                var caseLoan = caseLoans.GetCaseLoanByServicer(servicer.ServicerID);
                var attachmentFileName = BuildPdfAttachmentFileName(foreclosureCase, caseLoan);
                EmailSummaryBL.Instance.SendEmailSummaryReport(foreclosureCase.FcId, servicer.ContactEmail, attachmentFileName);
            }
        }

        private static void SendSummaryToHPFPortal(ForeclosureCaseDTO foreclosureCase, ServicerDTOCollection servicers, CaseLoanDTOCollection caseLoans)
        {
            //
        }
        
        private static string BuildPdfAttachmentFileName(ForeclosureCaseDTO foreclosureCase, CaseLoanDTO caseLoan)
        {            
            var pdfFile = new StringBuilder();            
            pdfFile.Append(caseLoan.AcctNum);
            pdfFile.Append("_");
            pdfFile.Append(caseLoan.ServicerName);
            pdfFile.Append("_");
            pdfFile.Append(caseLoan.Loan1st2nd);
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

        private static CaseLoanDTOCollection GetCaseLoans(int? fc_id)
        {
            return CaseLoanBL.Instance.RetrieveCaseLoan(fc_id);
        }

        private static ForeclosureCaseDTO GetForeclosureCase(int? fc_id)
        {
            return ForeclosureCaseBL.Instance.GetForeclosureCase(fc_id);
        }

    }
}
