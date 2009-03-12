using System;
using System.Collections.Generic;
using System.Text;
using HPF.SharePointAPI.BusinessEntity;
using Microsoft.SharePoint;
using System.Security;
using HPF.SharePointAPI.ContentTypes;
using System.Net.Mail;
using System.Configuration;

namespace HPF.SharePointAPI.Controllers
{
    internal delegate void UpdateSPListItem<T>(SPListItem item, T obj) where T:BaseObject;
    public static class DocumentCenterController
    {        
        #region "counseling Summary"
        public static IList<ResultInfo<CounselingSummaryInfo>> Upload(IList<CounselingSummaryInfo> counselingSummaryList, string spFolderName)
        {
            IList<ResultInfo<CounselingSummaryInfo>> results = UploadSPFiles(
                DocumentCenter.Default.CounselingSummaryLoginName,
                counselingSummaryList, 
                DocumentCenter.Default.CounselingSummary,
                spFolderName,
                UpdateCounselingSummaryInfo);
            
            return results;
        }

        public static ResultInfo<CounselingSummaryInfo> Upload(CounselingSummaryInfo counselingSummary, string spFolderName)
        {   
            List<CounselingSummaryInfo> counselingSummaryList = new List<CounselingSummaryInfo>();
            counselingSummaryList.Add(counselingSummary);
            IList<ResultInfo<CounselingSummaryInfo>> results = Upload(counselingSummaryList, spFolderName);
        
            return results[0];            
        }
        #endregion

        #region "Invoice"
        public static IList<ResultInfo<InvoiceInfo>> Upload(IList<InvoiceInfo> invoiceList, string spFolderName)
        {
            IList<ResultInfo<InvoiceInfo>> results = UploadSPFiles(
                DocumentCenter.Default.InvoiceLoginName,
                invoiceList, 
                DocumentCenter.Default.Invoice, 
                spFolderName,
                UpdateInvoiceInfo);
            return results;
        }

        public static ResultInfo<InvoiceInfo> Upload(InvoiceInfo invoice, string spFolderName)
        {
            List<InvoiceInfo> invoiceList = new List<InvoiceInfo>();
            invoiceList.Add(invoice);
            IList<ResultInfo<InvoiceInfo>> results = Upload(invoiceList, spFolderName);

            return results[0];
        }
        #endregion
        #region "Account Payable"
        public static IList<ResultInfo<AgencyPayableInfo>> Upload(IList<AgencyPayableInfo> accountPayableList, string spFolderName)
        {
            IList<ResultInfo<AgencyPayableInfo>> results = UploadSPFiles(
                DocumentCenter.Default.AgencyPayableLoginName,
                accountPayableList, 
                DocumentCenter.Default.AgencyPayable, 
                spFolderName,
                UpdateAgencyPayableInfo);
            return results;
        }

        public static ResultInfo<AgencyPayableInfo> Upload(AgencyPayableInfo accountPayable, string spFolderName)
        {
            List<AgencyPayableInfo> accountPayableList = new List<AgencyPayableInfo>();
            accountPayableList.Add(accountPayable);
            IList<ResultInfo<AgencyPayableInfo>> results = Upload(accountPayableList, spFolderName);
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
        private static IList<ResultInfo<T>> UploadSPFiles<T>(string userName, IList<T> items, string listName, string spFolderName, UpdateSPListItem<T> action) where T:BaseObject
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

                //get SPFolder       
                StringBuilder fileName = new StringBuilder();
                foreach (BaseObject file in items)
                {
                    fileName.Append(file.Name + Environment.NewLine);
                }
                SPFolder spFolder = GetSPFolder(web, 
                    docLib.RootFolder.ServerRelativeUrl, 
                    spFolderName, fileName.ToString());

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
        private static void UpdateAgencyPayableInfo(SPListItem spItem, AgencyPayableInfo agencyPayable)
        {
            if (agencyPayable.Date != null)
            {
                spItem[AgencyPayable.Default.Date] = agencyPayable.Date;
            }
            spItem[AgencyPayable.Default.AgencyName] = agencyPayable.AgencyName;
            spItem[AgencyPayable.Default.PayableNumber] = agencyPayable.PayableNumber;
            spItem[AgencyPayable.Default.Month] = agencyPayable.Month;
            spItem[AgencyPayable.Default.Year] = agencyPayable.Year;
            if (agencyPayable.PayableDate != null)
            {
                spItem[AgencyPayable.Default.PayableDate] = agencyPayable.PayableDate;
            }
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

        /// <summary>
        /// Ensure SharePoint folder if exists
        /// </summary>
        /// <param name="sourceWeb"></param>
        /// <param name="docUrl"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        private static SPFolder EnsureSPFolder(SPWeb sourceWeb, string folderPath)
        {
            SPFolder folder = sourceWeb.GetFolder(folderPath);

            if (folder.Exists) { return folder; }
            string tmpPath = "";
            string[] folders = folderPath.Split(new Char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < folders.Length; i++)
            {
                tmpPath += folders[i] + "/";

                if (!sourceWeb.GetFolder(tmpPath).Exists)
                {
                    folder = sourceWeb.Folders.Add(tmpPath);
                }
            }
            return folder;
        }

        private static SPFolder GetSPFolder(SPWeb sourceWeb, string docLibRootFolderUrl, string folderName, string fileName)
        {            
            if (String.IsNullOrEmpty(folderName))
            {
                //notify support team with empty folder name
                //Email Body: Error when upload report file {0} to empty folder. It was moved to {1}
                string body = String.Format(DocumentCenter.Default.ErrorBodyEmptySPFolderName,
                    fileName, DocumentCenter.Default.ErrorFolderName);
                SendMail(HPF_SUPPORT_EMAIL, DocumentCenter.Default.ErrorSubject, body);

                return sourceWeb.GetFolder(docLibRootFolderUrl + "/" +
                    DocumentCenter.Default.ErrorFolderName);
            }
            else
            {
                string folderPath = docLibRootFolderUrl + "/" + folderName;
                SPFolder folder = sourceWeb.GetFolder(folderPath);
                if (!folder.Exists)
                {
                    //notify support team with folder name not exists
                    //Email Body: Error when upload report file {0} to {1} folder. The {1} folder does not exist. It was moved to {2}
                    string body = String.Format(DocumentCenter.Default.ErrorBodyDoesNotExistSPFolder,
                        fileName, folderPath, DocumentCenter.Default.ErrorFolderName);
                    SendMail(HPF_SUPPORT_EMAIL, DocumentCenter.Default.ErrorSubject, body);

                    return sourceWeb.GetFolder(docLibRootFolderUrl + "/" +
                        DocumentCenter.Default.ErrorFolderName);
                }
                else
                {
                    return folder;
                }
            }
        }

        private static void SendMail(string to, string subject, string body)
        {
            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.To.Add(new MailAddress(to));
                    message.Subject = subject;
                    message.Body = body;
                    SmtpClient sender = new SmtpClient();
                    sender.Send(message);
                }
            }
            catch { }
        }

        public static string HPF_SUPPORT_EMAIL
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_SUPPORT_EMAIL"];
            }

        }
        #endregion
    }
}
