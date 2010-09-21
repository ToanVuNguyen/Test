using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.Utils.Exceptions;
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
            WarningMessage = new ExceptionMessageCollection();
        }
        public ExceptionMessageCollection WarningMessage { get; private set; } 
        /// <summary>
        /// Generate summary report as pdf format
        /// </summary>
        /// <param name="fc_id"></param>
        /// <returns>PDF file buffer</returns>
        public byte[] GenerateSummaryReport(int? fc_id)
        {
            return GenerateSummaryReport(fc_id, ReportFormat.PDF);         
        }

        /// <summary>
        /// Generate summary report as pdf format
        /// </summary>
        /// <param name="fc_id"></param>
        /// <returns>PDF file buffer</returns>
        public byte[] GenerateSummaryReport(int? fc_id, ReportFormat format)
        {
            try
            {                
                var reportExport = new ReportingExporter
                {
                    ReportPath = HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_COUNSELINGSUMMARY_REPORT)
                };
                reportExport.SetReportParameter("pi_fc_id", fc_id.ToString());
                var report = reportExport.ExportTo(format);

                return report;
            }
            catch (Exception ex)
            {
                string functionTrace = "[Report]SummaryReportBL.GenerateSummaryReport";                
                throw ExceptionProcessor.GetHpfException(ex, fc_id.ToString(), functionTrace);
            }            
        }


        public byte[] GenerateCompletedCouncellingDetailReport(CompletedCounselingDetailReportCriteriaDTO criteria , ReportFormat format)
        {
            try
            {
                var reportExport = new ReportingExporter
                {
                    ReportPath = HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_COMPLETEDCOUNSELINGDETTAIL_REPORT)
                };
                reportExport.SetReportParameter("pi_agency_id", criteria.AgencyId.ToString());
                reportExport.SetReportParameter("pi_program_id", criteria.ProgramId.ToString());
                reportExport.SetReportParameter("pi_from_dt", criteria.FromDate.ToString());
                reportExport.SetReportParameter("pi_to_dt", criteria.ToDate.ToString());

                var report = reportExport.ExportTo(format);

                return report;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }               

        public void UpdateSummarySentDateTime(int? fcId)
        {
            try
            {
                var foreclosureCaseSetDAO = ForeclosureCaseSetDAO.CreateInstance();
                foreclosureCaseSetDAO.UpdateSendSummaryDate(fcId);                
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.GetHpfException(ex, fcId.ToString(), "[UpdateDB]foreclosureCaseSetDAO.UpdateSendSummaryDate in SummaryReportBL");
            }
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
            WarningMessage = new ExceptionMessageCollection();
            //<Prepare data>
            var foreclosureCase = ForeclosureCaseBL.Instance.GetForeclosureCase(fc_id);
            if (foreclosureCase == null)
            {
                Exception ex = new Exception("Database error: Invalid fc ID " + fc_id);
                throw ExceptionProcessor.GetHpfException(ex, fc_id.ToString(), "SummaryReportBL.SendCompletedCaseSummary");
            }
            if (foreclosureCase.CompletedDt == null)
            {
                WarningMessage.AddExceptionMessage(ErrorMessages.WARN1107, ErrorMessages.GetExceptionMessage(ErrorMessages.WARN1107));
                return;
            }
            var caseLoan = GetCaseLoans1St(fc_id);
            var primaryServicer = ServicerBL.Instance.GetServicer(caseLoan.ServicerId.Value);
            //var primaryServicer = servicers.GetServicerById(caseLoan.ServicerId);
            //</Prepare data>           
            
            //Send summary
            switch (primaryServicer.SummaryDeliveryMethod)
            {
                case Constant.SECURE_DELIVERY_METHOD_SECEMAIL:
                    SendSummaryMailToServicer(foreclosureCase, primaryServicer, caseLoan);
                    break;
                case Constant.SECURE_DELIVERY_METHOD_PORTAL:
                    SendSummaryToHPFPortal(foreclosureCase, primaryServicer, caseLoan);
                    break;
            }

            //Update Summary sent datetime if any
            if (primaryServicer.SummaryDeliveryMethod != Constant.SECURE_DELIVERY_METHOD_NOSEND)
            {
                UpdateSummarySentDateTime(fc_id);
            }
        }

        /// <summary>
        /// Send CounselingSummary Mail to Servicer
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
            catch(Exception ex)
            {                
                throw ExceptionProcessor.GetHpfException(ex, foreclosureCase.FcId.ToString(),"[Email]SummaryReportBL.SendSummaryMailToServicer");
            }            
        }

        /// <summary>
        /// Send CounselingSummary information to HPF Portal.
        /// </summary>
        /// <param name="foreclosureCase"></param>
        /// <param name="servicer"></param>
        /// <param name="caseLoan"></param>
        private void SendSummaryToHPFPortal(ForeclosureCaseDTO foreclosureCase, ServicerDTO servicer, CaseLoanDTO caseLoan)
        {

            var hpfSharepointSummary = new HPFPortalCounselingSummary
                                           {
                                               ReportFile = GenerateSummaryReport(foreclosureCase.FcId),
                                               LoanNumber = caseLoan.AcctNum,
                                               CompletedDate = foreclosureCase.CompletedDt,
                                               ForeclosureSaleDate = foreclosureCase.FcSaleDate,
                                               Servicer = servicer.ServicerName,
                                               Delinquency = caseLoan.LoanDelinqStatusCd,
                                               ReportFileName = BuildPdfAttachmentFileName(foreclosureCase, caseLoan),
                                               SPFolderName = servicer.SPFolderName
                                           };
            try
            {                
                HPFPortalGateway.SendSummary(hpfSharepointSummary);
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.GetHpfException(ex, foreclosureCase.FcId.ToString(), "[Sharepoint API]HPFPortalGateway.SendSummary in SummaryReportBL.SendSummaryToHPFPortal");
            }

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
            pdfFile.Append(RemoveSpecialChars(caseLoan.AcctNum));
            pdfFile.Append("_");
            pdfFile.Append(RemoveSpecialChars(foreclosureCase.BorrowerLname));
            pdfFile.Append("_");
            pdfFile.Append(foreclosureCase.BorrowerFname.Substring(0, 1));
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

        public static CaseLoanDTO GetCaseLoans1St(int? fc_id)
        {
            try
            {
                var caseLoans = CaseLoanBL.Instance.RetrieveCaseLoan(fc_id);
                return caseLoans.SingleOrDefault(i => i.Loan1st2nd.ToUpper() == Constant.LOAN_1ST);
            }
            catch(Exception ex)
            {                
                throw ExceptionProcessor.GetHpfException(ex, fc_id.ToString(), "SummaryReportBL.GetCaseLoans1St");
            }
        }

        static private string RemoveSpecialChars(string str)
        {
            string s = (string)str.Clone();
            Regex exp = new Regex(@"[^a-zA-Z0-9]");
            MatchCollection matches = exp.Matches(s);
            foreach (Match item in matches)
            {
                s = s.Replace(item.Value, string.Empty);
            }

            return s;
        }
    }
}
