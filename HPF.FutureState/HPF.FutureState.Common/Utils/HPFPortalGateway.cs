using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.SharePointAPI.BusinessEntity;
using HPF.SharePointAPI.Controllers;
using Microsoft.Practices.EnterpriseLibrary.Logging;


namespace HPF.FutureState.Common.Utils
{
    public static class HPFPortalGateway
    {
        public static void SendSummary(HPFPortalCounselingSummary summary)
        {
            var counselingSummaryInfo = new CounselingSummaryInfo
                                            {
                                                LoanNumber = summary.LoanNumber,
                                                CompletedDate = summary.CompletedDate,
                                                ForeclosureSaleDate = summary.ForeclosureSaleDate,
                                                File = summary.ReportFile,
                                                Name = summary.ReportFileName,
                                                Servicer = summary.Servicer,
                                                Delinquency = summary.Delinquency
                                            };

            //todo: please specify spFolderName
            string spFolderName = "";
            var result = DocumentCenterController.Upload(counselingSummaryInfo, spFolderName);
            if (!result.Successful)
                Logger.Write(result.Error.Message, "General");

        }
        public static void SendInvoiceExcelFile(HPFPortalInvoice invoice)
        {
            var invoiceInfo = new   InvoiceInfo
                                    {
                                        Date = invoice.InvoiceDate,
                                        File = invoice.File,
                                        FundingSource = invoice.FundingSource,
                                        InvoiceNumber = invoice.InvoiceNumber,
                                        Month = string.Format("{0:MMM}",invoice.InvoiceDate),
                                        Name = invoice.FileName,
                                        Year = invoice.Year
                                    };
            string spFolderName = invoice.InvoiceFolderName;
            var result = DocumentCenterController.Upload(invoiceInfo, spFolderName);
            if (!result.Successful)
                Logger.Write(result.Error.Message, "General");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="summary"></param>
        public static void SendSummaryNewAgencyPayable(HPFPortalNewAgencyPayable summary)
        {
            var NewAgencyPayableInfo = new AgencyPayableInfo
            {
                Date = summary.Date,
                AgencyName=summary.AgencyName,
                PayableNumber=summary.PayableNumber,
                PayableDate=summary.PayableDate
            };

            //todo: please specify spFolderName
            string spFolderName = "";
            var result = DocumentCenterController.Upload(NewAgencyPayableInfo, spFolderName);
            if (!result.Successful)
                Logger.Write(result.Error.Message, "General");
        }
    }
}
