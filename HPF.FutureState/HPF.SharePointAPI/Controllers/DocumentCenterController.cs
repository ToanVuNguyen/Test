using System;
using System.Collections.Generic;
using System.Text;
using HPF.SharePointAPI.BusinessEntity;
using Microsoft.SharePoint;
using HPF.SharePointAPI.Constants;
using HPF.SharePointAPI.Enum;
using HPF.SharePointAPI.Utils;

namespace HPF.SharePointAPI.Controllers
{
    public delegate void UpdateSPListItem<T>(SPListItem item, T obj) where T:BaseObject;
    public static class DocumentCenterController
    {        
        #region "counseling Summary"
        public static IList<ResultInfo<CounselingSummaryInfo>> Upload(IList<CounselingSummaryInfo> counselingSummaryList)
        {
            IList<ResultInfo<CounselingSummaryInfo>> results = UploadSPFiles(counselingSummaryList, DocumentCenter.Default.CounselingSummary, UpdatecounselingSummaryInfo);
            
            return results;
        }

        public static ResultInfo<CounselingSummaryInfo> Upload(CounselingSummaryInfo counselingSummary)
        {
            WindowsImpersonation imp = WindowsImpersonation.ImpersonateAs(DocumentCenter.Default.CounselingSummaryUserName, DocumentCenter.Default.CounselingSummaryPassword);

            List<CounselingSummaryInfo> counselingSummaryList = new List<CounselingSummaryInfo>();
            counselingSummaryList.Add(counselingSummary);
            IList<ResultInfo<CounselingSummaryInfo>> results = Upload(counselingSummaryList);

            if (imp != null) { imp.Undo(); }

            return results[0];
        }
        #endregion

        #region "Invoice"
        public static IList<ResultInfo<InvoiceInfo>> Upload(IList<InvoiceInfo> invoiceList)
        {
            WindowsImpersonation imp = WindowsImpersonation.ImpersonateAs(DocumentCenter.Default.InvoiceUserName, DocumentCenter.Default.InvoicePassword);

            IList<ResultInfo<InvoiceInfo>> results = UploadSPFiles(invoiceList, DocumentCenter.Default.Invoice, UpdateInvoiceInfo);

            if (imp != null) { imp.Undo(); }

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
            WindowsImpersonation imp = WindowsImpersonation.ImpersonateAs(DocumentCenter.Default.AccountPayableUserName, DocumentCenter.Default.AccountPayablePassword);

            IList<ResultInfo<AccountPayableInfo>> results = UploadSPFiles(accountPayableList, DocumentCenter.Default.AccountPayable, UpdateAccountPayableInfo);

            if (imp != null) { imp.Undo(); }

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
        private static IList<ResultInfo<T>> UploadSPFiles<T>(IList<T> items, string listName, UpdateSPListItem<T> action) where T:BaseObject
        {
            List<ResultInfo<T>> results = new List<ResultInfo<T>>();
            
            using (SPSite site = new SPSite(DocumentCenter.Default.SharePointSite))
            {
                ResultInfo<T> resultInfo;
                string fileUrl;
                SPWeb web = site.AllWebs[DocumentCenter.Default.DocumentCenterWeb];
                web.AllowUnsafeUpdates = true;                
                SPDocumentLibrary docLib = (SPDocumentLibrary)web.Lists[listName];                
                SPFolder spFolder = docLib.RootFolder;
                SPFile spFile;
                foreach (T item in items)
                {
                    resultInfo = new ResultInfo<T>();
                    try
                    {
                        if (item.Name.Length == 0)
                        {
                            resultInfo.Successful = false;
                            resultInfo.Error = new ArgumentNullException("Name");
                            continue;
                        }
                        if (item.File.Length == 0)
                        {
                            resultInfo.Successful = false;
                            resultInfo.Error = new ArgumentNullException("File");
                            continue;
                        }
                        //add file                        
                        fileUrl = String.Format("{0}/{1}", spFolder.ServerRelativeUrl, item.Name);

                        spFile = web.GetFile(fileUrl);
                        if (spFile == null || !spFile.Exists)
                        {
                            spFile = spFolder.Files.Add(fileUrl, item.File);
                            spFile.Update();
                        }
                        else
                        {
                            if (spFile.CheckedOutBy != null)
                            {
                                if (spFile.CheckedOutBy.ID != web.CurrentUser.ID)
                                {
                                    resultInfo.Successful = false;
                                    resultInfo.Error = new SPException("File Already Checked Out Error"); ;
                                    continue;
                                }
                            }
                            else
                            {
                                if (docLib.ForceCheckout) { spFile.CheckOut(); }
                                spFile.SaveBinary(item.File, true);
                            }
                        }

                        action(spFile.Item, item);

                        spFile.Item.Update();
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

        private static void UpdatecounselingSummaryInfo(SPListItem spItem, CounselingSummaryInfo counselingSummary)
        {
            if (counselingSummary.CompletedDate != null)
            {
                spItem[DocumentCenterContentType.CounselingSummary.CompletedDate] = counselingSummary.CompletedDate;
            }
            spItem[DocumentCenterContentType.CounselingSummary.Delinquency] = counselingSummary.Delinquency;
            if (counselingSummary.ForeclosureSaleDate != null)
            {
                spItem[DocumentCenterContentType.CounselingSummary.ForeclosureSaleDate] = counselingSummary.ForeclosureSaleDate;
            }
            spItem[DocumentCenterContentType.CounselingSummary.LoanNumber] = counselingSummary.LoanNumber;
            spItem[DocumentCenterContentType.CounselingSummary.ReviewStatus] = counselingSummary.ReviewStatus == ReviewStatus.PendingReview ? "Pending Review" : "Reviewed";
            spItem[DocumentCenterContentType.CounselingSummary.Servicer] = counselingSummary.Servicer;
        }

        private static void UpdateInvoiceInfo(SPListItem spItem, InvoiceInfo invoice)
        {
            if (invoice.Date != null)
            {
                spItem[DocumentCenterContentType.Invoice.Date] = invoice.Date;
            }
            spItem[DocumentCenterContentType.Invoice.FundingSource] = invoice.FundingSource;
            spItem[DocumentCenterContentType.Invoice.InvoiceNumber] = invoice.InvoiceNumber;
            spItem[DocumentCenterContentType.Invoice.Month] = invoice.Month;
            spItem[DocumentCenterContentType.Invoice.Year] = invoice.Year;
        }

        private static void UpdateAccountPayableInfo(SPListItem spItem, AccountPayableInfo accountPayable)
        {
            if (accountPayable.Date != null)
            {
                spItem[DocumentCenterContentType.AccountPayable.Date] = accountPayable.Date;
            }            
            spItem[DocumentCenterContentType.AccountPayable.FundingSource] = accountPayable.FundingSource;
            spItem[DocumentCenterContentType.AccountPayable.InvoiceNumber] = accountPayable.InvoiceNumber;
            spItem[DocumentCenterContentType.AccountPayable.Month] = accountPayable.Month;
            spItem[DocumentCenterContentType.AccountPayable.Year] = accountPayable.Year;
        }
        #endregion
    }
}
