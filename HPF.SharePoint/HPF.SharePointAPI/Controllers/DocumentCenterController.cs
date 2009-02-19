using System;
using System.Collections.Generic;
using System.Text;
using HPF.SharePointAPI.BusinessEntity;
using Microsoft.SharePoint;
using HPF.SharePointAPI.Constants;

namespace HPF.SharePointAPI.Controllers
{
    public delegate void UpdateSPListItem<T>(SPListItem item, T obj) where T:BaseObject;
    public static class DocumentCenterController
    {        
        #region "Conseling Summary"
        public static IList<ResultInfo<ConselingSummaryInfo>> Upload(IList<ConselingSummaryInfo> conselingSummaryList)
        {
            IList<ResultInfo<ConselingSummaryInfo>> results = UploadSPFiles(conselingSummaryList, UpdateConselingSummaryInfo);
            
            return results;
        }

        public static ResultInfo<ConselingSummaryInfo> Upload(ConselingSummaryInfo conselingSummary)
        {
            List<ConselingSummaryInfo> conselingSummaryList = new List<ConselingSummaryInfo>();
            conselingSummaryList.Add(conselingSummary);
            IList<ResultInfo<ConselingSummaryInfo>> results = Upload(conselingSummaryList);
            return results[0];
        }
        #endregion

        #region "Invoice"
        public static IList<ResultInfo<InvoiceInfo>> Upload(IList<InvoiceInfo> invoiceList)
        {
            IList<ResultInfo<InvoiceInfo>> results = UploadSPFiles(invoiceList, UpdateInvoiceInfo);

            return results;
        }

        public static ResultInfo<InvoiceInfo> Upload(InvoiceInfo invoice)
        {
            List<InvoiceInfo> invoiceList = new List<InvoiceInfo>();
            invoiceList.Add(invoice);
            IList<ResultInfo<InvoiceInfo>> results = Upload(invoiceList);

            return results[0];
        }
        #endregion

        #region "Account Payable"
        public static IList<ResultInfo<AccountPayableInfo>> Upload(IList<AccountPayableInfo> accountPayableList)
        {
            IList<ResultInfo<AccountPayableInfo>> results = UploadSPFiles(accountPayableList, UpdateAccountPayableInfo);

            return results;
        }

        public static ResultInfo<AccountPayableInfo> Upload(AccountPayableInfo accountPayable)
        {
            List<AccountPayableInfo> accountPayableList = new List<AccountPayableInfo>();
            accountPayableList.Add(accountPayable);
            IList<ResultInfo<AccountPayableInfo>> results = Upload(accountPayableList);
            return results[0];
        }
        #endregion

        #region "Helper"
        private static IList<ResultInfo<T>> UploadSPFiles<T>(IList<T> items, UpdateSPListItem<T> action) where T:BaseObject
        {
            List<ResultInfo<T>> results = new List<ResultInfo<T>>();
            
            using (SPSite site = new SPSite(DocumentCenter.Default.DocumentCenterSite))
            {
                ResultInfo<T> resultInfo;
                string fileUrl;
                SPWeb web = site.AllWebs[DocumentCenter.Default.DocumentCenterWeb];
                web.AllowUnsafeUpdates = true;
                SPDocumentLibrary docLib = (SPDocumentLibrary)web.Lists[DocumentCenter.Default.ConselingSummary];
                SPFolder spFolder = docLib.RootFolder;
                SPFile spFile;
                foreach (T item in items)
                {
                    resultInfo = new ResultInfo<T>();
                    try
                    {
                        //add file                        
                        fileUrl = String.Format("{0}/{1}", spFolder.ServerRelativeUrl, item.Name);
                        spFile = spFolder.Files.Add(fileUrl, item.File);
                        spFile.Update();

                        action(spFile.Item, item);

                        spFile.Item.Update();

                        resultInfo.BizObject = item;
                    }
                    catch (Exception error)
                    {
                        resultInfo.Successful = false;
                        resultInfo.Error = error;
                    }
                    results.Add(resultInfo);
                }
            }
            return results;
        }

        private static void UpdateConselingSummaryInfo(SPListItem spItem, ConselingSummaryInfo conselingSummary)
        {            
            spItem[DocumentCenterContentType.ConselingSummary.CompletedDate] = conselingSummary.CompletedDate;
            spItem[DocumentCenterContentType.ConselingSummary.Delinquency] = conselingSummary.Delinquency;
            spItem[DocumentCenterContentType.ConselingSummary.ForeclosureSaleDate] = conselingSummary.ForeclosureSaleDate;
            spItem[DocumentCenterContentType.ConselingSummary.LoanNumber] = conselingSummary.LoanNumber;
            spItem[DocumentCenterContentType.ConselingSummary.ReviewStatus] = conselingSummary.ReviewStatus;
            spItem[DocumentCenterContentType.ConselingSummary.Servicer] = conselingSummary.Servicer;
        }

        private static void UpdateInvoiceInfo(SPListItem spItem, InvoiceInfo invoice)
        {            
            spItem[DocumentCenterContentType.Invoice.Date] = invoice.Date;
            spItem[DocumentCenterContentType.Invoice.FundingSource] = invoice.FundingSource;
            spItem[DocumentCenterContentType.Invoice.InvoiceNumber] = invoice.InvoiceNumber;
            spItem[DocumentCenterContentType.Invoice.Month] = invoice.Month;
            spItem[DocumentCenterContentType.Invoice.Year] = invoice.Year;
        }

        private static void UpdateAccountPayableInfo(SPListItem spItem, AccountPayableInfo accountPayable)
        {            
            spItem[DocumentCenterContentType.AccountPayable.Date] = accountPayable.Date;
            spItem[DocumentCenterContentType.AccountPayable.FundingSource] = accountPayable.FundingSource;
            spItem[DocumentCenterContentType.AccountPayable.InvoiceNumber] = accountPayable.InvoiceNumber;
            spItem[DocumentCenterContentType.AccountPayable.Month] = accountPayable.Month;
            spItem[DocumentCenterContentType.AccountPayable.Year] = accountPayable.Year;
        }
        #endregion
    }
}
