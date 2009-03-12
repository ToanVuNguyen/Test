using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace HPF.FutureState.BusinessLogic
{
    public class ReportBL
    {
        private static readonly ReportBL instance = new ReportBL();
        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static ReportBL Instance
        {
            get { return instance; }
        }

        protected ReportBL()
        {

        }
        /// <summary>
        /// Generate PDF Report
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public byte[] InvoicePDFReport(int invoiceId)
        {
            string reportPath = HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_INVOICE_EXPORT_INVOICE_SUMMARY_REPORT);
            var reportExport = new ReportingExporter { ReportPath = reportPath };
            reportExport.SetReportParameter("pi_invoice_id", invoiceId.ToString());
            var pdfReport = reportExport.ExportToPdf();
            return pdfReport;
        }
            
        public byte[] InvoiceExcelReport(int invoiceId, string fundingSourceExportFormatCd,bool getFISDetail)
        {
            //Generate the excel file first, need more info about the pdf file


            //Create excel file
            string reportPath = string.Empty;
            switch (fundingSourceExportFormatCd)
            {
                case (Constant.REF_CODE_SET_BILLING_EXPORT_FIS_CODE):
                    {
                        reportPath = HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_INVOICE_EXPORT_FIS_HEADER_REPORT);
                        // if isFIS
                        if (getFISDetail)
                            reportPath = HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_INVOICE_EXPORT_FIS_DETAIL_REPORT);
                        break;
                    }
                case (Constant.REF_CODE_SET_BILLING_EXPORT_HPFSTD_CODE):
                    {
                        reportPath = HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_INVOICE_EXPORT_HPFSTD_REPORT);
                        break;
                    }
                case (Constant.REF_CODE_SET_BILLING_EXPORT_ICLEAR_CODE):
                    {
                        reportPath = HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_INVOICE_EXPORT_ICLEAR_REPORT);
                        break;
                    }
                case (Constant.REF_CODE_SET_BILLING_EXPORT_HSBC_CODE):
                    {
                        reportPath = HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_INVOICE_EXPORT_HSBC_REPORT);
                        break;
                    }
                case (Constant.REF_CODE_SET_BILLING_EXPORT_NFMC_CODE):
                    {
                        reportPath = HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_INVOICE_EXPORT_NFMC_REPORT);
                        break;
                    }
            }
            if (reportPath == string.Empty)
                return null;
            var reportExport = new ReportingExporter { ReportPath = reportPath };
            reportExport.SetReportParameter("pi_invoice_id", invoiceId.ToString());
            var excelReport = reportExport.ExportToExcel();
            return excelReport;
        }
        private byte[] AgencyPayableReportPdf(int? agency_payable_id)
        {
            var reportExport = new ReportingExporter
            {
                ReportPath = HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_AGENCY_PAYABLE_REPORT)
            };
            reportExport.SetReportParameter("pi_agency_payable_id", agency_payable_id.ToString());
            var pdfReport = reportExport.ExportToPdf();
            return pdfReport;
        }
        private byte[] AgencyPayableReportXls(int? agency_payable_id)
        {
            var reportExport = new ReportingExporter
            {
                ReportPath = HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_AGENCY_PAYABLE_REPORT)
            };
            reportExport.SetReportParameter("pi_agency_payable_id", agency_payable_id.ToString());
            var xlsReport = reportExport.ExportToExcel();
            return xlsReport;
        }
        public void SendAgencyPayableToHPFPortal(AgencyPayableDTO agencyPayable)
        {
            try
            {
                var hpfSharepointSummaryPdf = new HPFPortalNewAgencyPayable
                {
                    ReportFile = AgencyPayableReportPdf(agencyPayable.AgencyPayableId),
                    ReportFileName = "AgencyPayablePDF.pdf",
                    AgencyName = agencyPayable.AgencyName.ToString(),
                    PayableNumber = agencyPayable.PayableNum.ToString(),
                    PayableDate=agencyPayable.PaymentDate,
                    Date = agencyPayable.CreateDate
                };
                var hpfSharepointSummaryXls = new HPFPortalNewAgencyPayable
                {
                    ReportFile = AgencyPayableReportPdf(agencyPayable.AgencyPayableId),
                    ReportFileName = "AgencyPayablePDF.xls",
                    AgencyName = agencyPayable.AgencyName.ToString(),
                    PayableNumber = agencyPayable.PayableNum.ToString(),
                    PayableDate = agencyPayable.PaymentDate,
                    Date = agencyPayable.CreateDate
                };
                HPFPortalGateway.SendSummaryNewAgencyPayable(hpfSharepointSummaryPdf);
                HPFPortalGateway.SendSummaryNewAgencyPayable(hpfSharepointSummaryXls);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
                Logger.Write(ex.StackTrace);
            }
        }

    }
}
