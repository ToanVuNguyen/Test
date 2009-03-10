using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;

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
        public byte[] InvoiceExcelReport(int invoiceId,string fundingSourceExportFormatCd)
        { 
            //Generate the excel file first, need more info about the pdf file


            //Create excel file
            string reportPath = string.Empty;
            switch (fundingSourceExportFormatCd)
            {
                case (Constant.REF_CODE_SET_BILLING_EXPORT_FIS_CODE):
                    {
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
    }
}
