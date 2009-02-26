using System;
using System.Collections.Generic;
using System.Text;
using HPF.SharePointAPI.BusinessEntity;
using Microsoft.SharePoint;
using HPF.SharePointAPI.Enum;
using System.Security;
using HPF.SharePointAPI.ContentTypes;

namespace HPF.SharePointAPI.Controllers
{
    internal delegate void UpdateSPListItem<T>(SPListItem item, T obj) where T:BaseObject;
    public static class DocumentCenterController
    {        
        #region "counseling Summary"
        public static IList<ResultInfo<CounselingSummaryInfo>> Upload(IList<CounselingSummaryInfo> counselingSummaryList)
        {
            IList<ResultInfo<CounselingSummaryInfo>> results = UploadSPFiles(
                DocumentCenter.Default.CounselingSummaryLoginName,
                counselingSummaryList, 
                DocumentCenter.Default.CounselingSummary, 
                UpdateCounselingSummaryInfo);
            
            return results;
        }

        public static ResultInfo<CounselingSummaryInfo> Upload(CounselingSummaryInfo counselingSummary)
        {   
            List<CounselingSummaryInfo> counselingSummaryList = new List<CounselingSummaryInfo>();
            counselingSummaryList.Add(counselingSummary);
            IList<ResultInfo<CounselingSummaryInfo>> results = Upload(counselingSummaryList);
        
            return results[0];            
        }
        #endregion

        #region "Invoice"
        public static IList<ResultInfo<InvoiceInfo>> Upload(IList<InvoiceInfo> invoiceList)
        {
            IList<ResultInfo<InvoiceInfo>> results = UploadSPFiles(
                DocumentCenter.Default.InvoiceLoginName,
                invoiceList, 
                DocumentCenter.Default.Invoice, 
                UpdateInvoiceInfo);
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
            IList<ResultInfo<AccountPayableInfo>> results = UploadSPFiles(
                DocumentCenter.Default.AccountPayableLoginName,
                accountPayableList, 
                DocumentCenter.Default.AccountPayable, 
                UpdateAccountPayableInfo);
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
        /// <summary>
        /// Upload SPFile and call a delegate for updating SPItem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userName"></param>
        /// <param name="items"></param>
        /// <param name="listName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private static IList<ResultInfo<T>> UploadSPFiles<T>(string userName, IList<T> items, string listName, UpdateSPListItem<T> action) where T:BaseObject
        {
            List<ResultInfo<T>> results = new List<ResultInfo<T>>();

            SPUserToken token = GetUploadSPUserToken(userName);

            using (SPSite site = new SPSite(DocumentCenter.Default.SharePointSite, token))
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

        /// <summary>
        /// Update Counseling Summary SPListItem
        /// </summary>
        /// <param name="spItem"></param>
        /// <param name="counselingSummary"></param>
        private static void UpdateCounselingSummaryInfo(SPListItem spItem, CounselingSummaryInfo counselingSummary)
        {
            if (counselingSummary.CompletedDate != null)
            {
                spItem[CounselingSummary.Default.CompletedDate] = counselingSummary.CompletedDate;
            }
            spItem[CounselingSummary.Default.Delinquency] = counselingSummary.Delinquency;
            if (counselingSummary.ForeclosureSaleDate != null)
            {
                spItem[CounselingSummary.Default.ForeclosureSaleDate] = counselingSummary.ForeclosureSaleDate;
            }
            spItem[CounselingSummary.Default.LoanNumber] = counselingSummary.LoanNumber;
            spItem[CounselingSummary.Default.ReviewStatus] = counselingSummary.ReviewStatus == ReviewStatus.PendingReview ? "Pending Review" : "Reviewed";
            spItem[CounselingSummary.Default.Servicer] = counselingSummary.Servicer;
        }

        /// <summary>
        /// Update Invoice SPListItem
        /// </summary>
        /// <param name="spItem"></param>
        /// <param name="invoice"></param>
        private static void UpdateInvoiceInfo(SPListItem spItem, InvoiceInfo invoice)
        {
            if (invoice.Date != null)
            {
                spItem[Invoice.Default.Date] = invoice.Date;
            }
            spItem[Invoice.Default.FundingSource] = invoice.FundingSource;
            spItem[Invoice.Default.InvoiceNumber] = invoice.InvoiceNumber;
            spItem[Invoice.Default.Month] = invoice.Month;
            spItem[Invoice.Default.Year] = invoice.Year;
        }

        /// <summary>
        /// Update Account Payable SPListItem
        /// </summary>
        /// <param name="spItem"></param>
        /// <param name="accountPayable"></param>
        private static void UpdateAccountPayableInfo(SPListItem spItem, AccountPayableInfo accountPayable)
        {
            if (accountPayable.Date != null)
            {
                spItem[AccountPayable.Default.Date] = accountPayable.Date;
            }
            spItem[AccountPayable.Default.FundingSource] = accountPayable.FundingSource;
            spItem[AccountPayable.Default.InvoiceNumber] = accountPayable.InvoiceNumber;
            spItem[AccountPayable.Default.Month] = accountPayable.Month;
            spItem[AccountPayable.Default.Year] = accountPayable.Year;
        }

        /// <summary>
        /// Get SPUserToken for impersonating when upload file
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private static SPUserToken GetUploadSPUserToken(string userName)
        {
            SPUserToken token = null;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(DocumentCenter.Default.SharePointSite))
                {
                    SPWeb web = site.AllWebs[DocumentCenter.Default.DocumentCenterWeb];
                    SPUser user = web.AllUsers[userName];
                    token = user.UserToken;
                }
            });
            return token;
        }
        #endregion
    }
}
