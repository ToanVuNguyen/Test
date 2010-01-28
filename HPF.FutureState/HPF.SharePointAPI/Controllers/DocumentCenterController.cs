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
        static private void WriteLog(string s)
        {
            System.IO.TextWriter tw = new System.IO.StreamWriter("LOG.txt", true);
            tw.WriteLine(s);
            tw.Close();
        }
        
        public static IList<MHAEscalationInfo> GetMHAEscalationList()
        {
            string trackingName = "";
            try
            {                
                SPUserToken token = GetUploadSPUserToken(DocumentCenter.Default.MHAEscalationLoginName);
                
                List<MHAEscalationInfo> mhaList = new List<MHAEscalationInfo>();                
                using (SPSite site = new SPSite(DocumentCenter.Default.SharePointSite, token))
                {                    
                    //SPWeb web = site.AllWebs[DocumentCenter.Default.DocumentCenterWeb];  
                    SPWeb web = site.OpenWeb();
                    web.AllowUnsafeUpdates = true;                 
                    SPList docLib = web.Lists[DocumentCenter.Default.MHAEscalationList];                    
                    SPListItemCollection listItems = docLib.Items;
                    
                    foreach (SPListItem item in listItems)
                    {
                        MHAEscalationInfo mhaInfo = new MHAEscalationInfo(); trackingName = MHAEscalation.Default.LoanNumber;
                        mhaInfo.LoanNumber = (string)item[MHAEscalation.Default.LoanNumber]; trackingName = MHAEscalation.Default.MMICaseId;
                        mhaInfo.MMICaseId = (string)item[MHAEscalation.Default.MMICaseId];trackingName = MHAEscalation.Default.FirstName;
                        mhaInfo.Firstname = (string)item[MHAEscalation.Default.FirstName];trackingName = MHAEscalation.Default.LastName;
                        mhaInfo.Lastname = (string)item[MHAEscalation.Default.LastName]; trackingName = MHAEscalation.Default.CounselorEmail;
                        mhaInfo.CounselorEmail = (string)item[MHAEscalation.Default.CounselorEmail]; trackingName = MHAEscalation.Default.CounselorName;
                        mhaInfo.CounselorName = (string)item[MHAEscalation.Default.CounselorName]; trackingName = MHAEscalation.Default.CounselorPhone;
                        mhaInfo.CounselorPhone = (string)item[MHAEscalation.Default.CounselorPhone]; trackingName = MHAEscalation.Default.CreatedDate;

                        mhaInfo.CreatedDate = DateTime.Now;

                        foreach (SPField field in item.Fields)
                        {
                            if (field.Title.Equals("Created") || field.Title.Equals(MHAEscalation.Default.CreatedDate))
                            {
                                mhaInfo.CreatedDate = (DateTime?)item[field.Title];
                                break;
                            }
                        }
                        trackingName = MHAEscalation.Default.CurrentOwnerOfIssue;
                        mhaInfo.CurrentOwnerOfIssue = (string)item[MHAEscalation.Default.CurrentOwnerOfIssue]; trackingName = MHAEscalation.Default.EscalatedToFannieMae;
                        mhaInfo.EscalatedToFannie = item[MHAEscalation.Default.EscalatedToFannieMae].ToString(); trackingName = MHAEscalation.Default.EscalatedToFreddie;
                        mhaInfo.EscalatedToFreddie = (string)item[MHAEscalation.Default.EscalatedToFreddie].ToString(); trackingName = MHAEscalation.Default.EscalatedToMMIMgmt;
                        mhaInfo.EscalatedToMMIMgmt = (string)item[MHAEscalation.Default.EscalatedToMMIMgmt].ToString(); trackingName = MHAEscalation.Default.Escalation;
                        mhaInfo.Escalation = (string)item[MHAEscalation.Default.Escalation]; trackingName = MHAEscalation.Default.EscalationTeamNotes;
                        mhaInfo.EscalationTeamNotes = (string)item[MHAEscalation.Default.EscalationTeamNotes]; trackingName = MHAEscalation.Default.FinalResolution;
                        mhaInfo.FinalResolution = (string)item[MHAEscalation.Default.FinalResolution]; trackingName = MHAEscalation.Default.FinalResolutionDate;
                        mhaInfo.FinalResolutionDate = (DateTime?)item[MHAEscalation.Default.FinalResolutionDate]; trackingName = MHAEscalation.Default.FinalResolutionNotes;
                        mhaInfo.FinalResolutionNotes = (string)item[MHAEscalation.Default.FinalResolutionNotes]; trackingName = MHAEscalation.Default.GSELookup;
                        mhaInfo.GSELookup = (string)item[MHAEscalation.Default.GSELookup]; trackingName = MHAEscalation.Default.GSENotes;
                        mhaInfo.GseNotes = (string)item[MHAEscalation.Default.GSENotes]; trackingName = MHAEscalation.Default.HPFNotes;
                        mhaInfo.HpfNotes = (string)item[MHAEscalation.Default.HPFNotes]; trackingName = MHAEscalation.Default.ResolvedBy;
                        mhaInfo.ResolvedBy = (string)item[MHAEscalation.Default.ResolvedBy]; trackingName = MHAEscalation.Default.Servicer;
                        mhaInfo.Servicer = (string)item[MHAEscalation.Default.Servicer]; trackingName = MHAEscalation.Default.City;
                        mhaInfo.City = (string)item[MHAEscalation.Default.City]; trackingName = MHAEscalation.Default.State;
                        mhaInfo.State = (string)item[MHAEscalation.Default.State]; trackingName = MHAEscalation.Default.Zip;
                        mhaInfo.Zip = (string)item[MHAEscalation.Default.Zip]; trackingName = MHAEscalation.Default.Address;
                        mhaInfo.Address = (string)item[MHAEscalation.Default.Address]; trackingName = MHAEscalation.Default.Email;
                        mhaInfo.BorrowerEmail = (string)item[MHAEscalation.Default.Email]; trackingName = MHAEscalation.Default.BestNumberToCall;
                        mhaInfo.BestNumberToCall = (string)item[MHAEscalation.Default.BestNumberToCall]; trackingName = MHAEscalation.Default.BestTimeToReach;
                        mhaInfo.BestTimetoReach = (string)item[MHAEscalation.Default.BestTimeToReach]; trackingName = MHAEscalation.Default.HandleTimeHrs;
                        mhaInfo.HandleTimeHrs = ConvertToInt(item[MHAEscalation.Default.HandleTimeHrs]); trackingName = MHAEscalation.Default.HandleTimeMins;
                        mhaInfo.HandleTimeMins = ConvertToInt(item[MHAEscalation.Default.HandleTimeMins]);
                        if (!string.IsNullOrEmpty(mhaInfo.Servicer))
                        {
                            int index = mhaInfo.Servicer.IndexOf(";#");
                            if (index > 0)
                                mhaInfo.Servicer = mhaInfo.Servicer.Substring(index + 2, mhaInfo.Servicer.Length - index - 2);
                        }
                        mhaList.Add(mhaInfo);
                    }
                }
                return mhaList;
            }
            catch (Exception ex)
            {
                WriteLog("----------------\n" + System.DateTime.Now.ToString() + "--" +  ex.Message + "\n" + ex.StackTrace + "--Current Error Field: " + trackingName);
                throw ex;
            }            
        }

        public static IList<MHAHelpInfo> GetMHAHelpList()
        {
            string trackingName = "";
            try
            {
                SPUserToken token = GetUploadSPUserToken(DocumentCenter.Default.MHAHelpLoginName);

                List<MHAHelpInfo> mhaList = new List<MHAHelpInfo>();
                using (SPSite site = new SPSite(DocumentCenter.Default.SharePointSite, token))
                {                    
                    SPWeb web = site.OpenWeb();
                    web.AllowUnsafeUpdates = true;
                    SPList docLib = web.Lists[DocumentCenter.Default.MHAHelpList];
                    SPListItemCollection listItems = docLib.Items;

                    foreach (SPListItem item in listItems)
                    {
                        MHAHelpInfo mhaInfo = new MHAHelpInfo(); trackingName = MHAHelp.Default.Address;
                        mhaInfo.Address = (string)item[MHAHelp.Default.Address]; trackingName = MHAHelp.Default.AllDocumentsSubmitted;
                        mhaInfo.AllDocumentsSubmitted = (string)item[MHAHelp.Default.AllDocumentsSubmitted]; trackingName = MHAHelp.Default.BestTimeToReach;
                        mhaInfo.BestTimeToReach = (string)item[MHAHelp.Default.BestTimeToReach]; trackingName = MHAHelp.Default.BorrowerInTrialMod;
                        mhaInfo.BorrowerInTrialMod = (string)item[MHAHelp.Default.BorrowerInTrialMod]; trackingName = MHAHelp.Default.CallSource;
                        mhaInfo.CallSource = (string)item[MHAHelp.Default.CallSource]; trackingName = MHAHelp.Default.City;
                        mhaInfo.City = (string)item[MHAHelp.Default.City]; trackingName = MHAHelp.Default.Comments;
                        mhaInfo.Comments = (string)item[MHAHelp.Default.Comments]; trackingName = MHAHelp.Default.CounselorEmail;
                        mhaInfo.CounselorEmail = (string)item[MHAHelp.Default.CounselorEmail]; trackingName = MHAHelp.Default.CounselorName;                        
                        mhaInfo.CounselorName = (string)item[MHAHelp.Default.CounselorName]; trackingName = MHAHelp.Default.CurrentOnPayments;
                        mhaInfo.CurrentOnPayments = (string)item[MHAHelp.Default.CurrentOnPayments]; trackingName = MHAHelp.Default.DocumentsSubmitted;
                        mhaInfo.DocumentsSubmitted = (string)item[MHAHelp.Default.DocumentsSubmitted]; trackingName = MHAHelp.Default.Email;
                        mhaInfo.Email = (string)item[MHAHelp.Default.Email]; trackingName = MHAHelp.Default.FinalResolutionNotes;
                        mhaInfo.FinalResolutionNotes = (string)item[MHAHelp.Default.FinalResolutionNotes]; trackingName = MHAHelp.Default.FirstName;
                        mhaInfo.FirstName = (string)item[MHAHelp.Default.FirstName]; trackingName = MHAHelp.Default.IfWageEarnerWereTwoPayStubsSentIn;
                        mhaInfo.IfWageEarnerWereTwoPayStubsSentIn = (string)item[MHAHelp.Default.IfWageEarnerWereTwoPayStubsSentIn]; trackingName = MHAHelp.Default.ItemCreatedDate;
                        mhaInfo.ItemCreatedDate = (DateTime?)item[MHAHelp.Default.ItemCreatedDate]; trackingName = MHAHelp.Default.ItemCreatedUser;
                        mhaInfo.ItemCreatedUser = (string)item[MHAHelp.Default.ItemCreatedUser]; trackingName = MHAHelp.Default.ItemModifiedDate;
                        mhaInfo.ItemModifiedDate = (DateTime?)item[MHAHelp.Default.ItemModifiedDate]; trackingName = MHAHelp.Default.ItemModifiedUser;
                        mhaInfo.ItemModifiedUser = (string)item[MHAHelp.Default.ItemModifiedUser]; trackingName = MHAHelp.Default.LastName;
                        mhaInfo.LastName = (string)item[MHAHelp.Default.LastName]; trackingName = MHAHelp.Default.LoanNumber;
                        mhaInfo.LoanNumber = (string)item[MHAHelp.Default.LoanNumber]; trackingName = MHAHelp.Default.MHAConversionCampaignFields;
                        mhaInfo.MHAConversionCampainFields = (string)item[MHAHelp.Default.MHAConversionCampaignFields]; trackingName = MHAHelp.Default.MHAHelpReason;
                        mhaInfo.MHAHelpReason = (string)item[MHAHelp.Default.MHAHelpReason]; trackingName = MHAHelp.Default.MHAHelpResolution;
                        mhaInfo.MHAHelpResolution = (string)item[MHAHelp.Default.MHAHelpResolution]; trackingName = MHAHelp.Default.MMICaseId;
                        mhaInfo.MMICaseId = (string)item[MHAHelp.Default.MMICaseId]; trackingName = MHAHelp.Default.Phone;
                        mhaInfo.Phone = (string)item[MHAHelp.Default.Phone]; trackingName = MHAHelp.Default.PrivacyConsent;
                        mhaInfo.PrivacyConsent = (string)item[MHAHelp.Default.PrivacyConsent]; trackingName = MHAHelp.Default.Servicer;
                        mhaInfo.Servicer = (string)item[MHAHelp.Default.Servicer]; trackingName = MHAHelp.Default.State;
                        mhaInfo.State = (string)item[MHAHelp.Default.State]; trackingName = MHAHelp.Default.TrialModStartedBeforeNov1;
                        mhaInfo.TrialModStartedBeforeNov1 = (string)item[MHAHelp.Default.TrialModStartedBeforeNov1]; trackingName = MHAHelp.Default.VoicemailDate;
                        mhaInfo.VoicemailDate = (DateTime?)item[MHAHelp.Default.VoicemailDate]; trackingName = MHAHelp.Default.WageEarner;
                        mhaInfo.WageEarner = (string)item[MHAHelp.Default.WageEarner]; trackingName = MHAHelp.Default.Zip;
                        mhaInfo.Zip = (string)item[MHAHelp.Default.Zip];trackingName = MHAHelp.Default.HandleTimeHrs;
                        mhaInfo.HandleTimeHrs = ConvertToInt(item[MHAHelp.Default.HandleTimeHrs]); trackingName = MHAHelp.Default.HandleTimeMins;
                        mhaInfo.HandleTimeMins = ConvertToInt(item[MHAHelp.Default.HandleTimeMins]);                        
                        if (!string.IsNullOrEmpty(mhaInfo.Servicer))
                        {
                            int index = mhaInfo.Servicer.IndexOf(";#");
                            if (index > 0)
                                mhaInfo.Servicer = mhaInfo.Servicer.Substring(index + 2, mhaInfo.Servicer.Length - index - 2);
                        }
                        if (!string.IsNullOrEmpty(mhaInfo.ItemCreatedUser))
                        {
                            int index = mhaInfo.ItemCreatedUser.IndexOf(";#");
                            if (index > 0)
                                mhaInfo.ItemCreatedUser = mhaInfo.ItemCreatedUser.Substring(index + 2, mhaInfo.ItemCreatedUser.Length - index - 2);
                        }
                        if (!string.IsNullOrEmpty(mhaInfo.ItemModifiedUser))
                        {
                            int index = mhaInfo.ItemModifiedUser.IndexOf(";#");
                            if (index > 0)
                                mhaInfo.ItemModifiedUser = mhaInfo.ItemModifiedUser.Substring(index + 2, mhaInfo.ItemModifiedUser.Length - index - 2);
                        }
                        mhaList.Add(mhaInfo);
                    }
                }
                return mhaList;
            }
            catch (Exception ex)
            {
                WriteLog("----------------\n" + System.DateTime.Now.ToString() + "--" + ex.Message + "\n" + ex.StackTrace + "--Current Error Field: " + trackingName);
                throw ex;
            }
        }
        #region "counseling List Generate"

        public static void GenerateWeeklyCounselorList(IList<CounselorInfo> counselorInfoList, string spFolderName)
        {
            SPUserToken token = GetUploadSPUserToken(DocumentCenter.Default.CounselorWeeklyLoginName);

            using (SPSite site = new SPSite(DocumentCenter.Default.SharePointSite, token))
            {                
                SPWeb web = site.OpenWeb();             
                web.AllowUnsafeUpdates = true;
                SPList docLib = web.Lists[DocumentCenter.Default.CounselorWeeklyList];                
                SPListItemCollection listItems = docLib.Items;
                
                for (int i = listItems.Count - 1; i >= 0; i--)
                    listItems.Delete(i);

                foreach (CounselorInfo counselor in counselorInfoList)
                {                    
                    SPListItem item = listItems.Add();
                    item[Counselor.Default.Title] = counselor.Title;
                    item[Counselor.Default.AgencyName] = counselor.AgencyName;
                    item[Counselor.Default.CounselorEmail] = counselor.CounselorEmail;
                    item[Counselor.Default.CounselorExt] = counselor.CounselorExt;
                    item[Counselor.Default.CounselorFirstName] = counselor.counselorFirstName;
                    item[Counselor.Default.CounselorLastName] = counselor.CounselorLastName;
                    item[Counselor.Default.CounselorPhone] = counselor.CounselorPhone;
                    item.Update();                 
                }
            }
        }
       
        #endregion
        
        #region "counseling Summary"
        public static IList<ResultInfo<FannieMaeInfo>> Upload(IList<FannieMaeInfo> fannieMaeInfoList, string spFolderName)
        {
            IList<ResultInfo<FannieMaeInfo>> results = UploadSPFiles(
                DocumentCenter.Default.FannieMaeLoginName,
                fannieMaeInfoList,
                DocumentCenter.Default.FannieMaeWeeklyReport,
                spFolderName,
                UpdateFannieMaeInfo);
            
            return results;
        }

        public static ResultInfo<FannieMaeInfo> Upload(FannieMaeInfo fannieMaeInfo, string spFolderName)
        {
            List<FannieMaeInfo> fannieMaeInfoList = new List<FannieMaeInfo>();
            fannieMaeInfoList.Add(fannieMaeInfo);
            IList<ResultInfo<FannieMaeInfo>> results = Upload(fannieMaeInfoList, spFolderName);
        
            return results[0];            
        }
        #endregion

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
        private static void UpdateFannieMaeInfo(SPListItem spItem, FannieMaeInfo fannieMaeSummary)
        {
            spItem[FannieMae.Default.StartDt] = fannieMaeSummary.StartDt;
            spItem[FannieMae.Default.EndDt] = fannieMaeSummary.EndDt;
            spItem[FannieMae.Default.FileName] = fannieMaeSummary.FileName;
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
            if (counselingSummary.LoanNumber != null)
                spItem[CounselingSummary.Default.LoanNumber] = counselingSummary.LoanNumber;
            if (counselingSummary.Servicer != null)
                spItem[CounselingSummary.Default.Servicer] = counselingSummary.Servicer;
            if(counselingSummary.ReviewStatus != null)
                spItem[CounselingSummary.Default.ReviewStatus] = counselingSummary.ReviewStatus;
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
                if(fileName.IndexOf("WeeklyCallReport") >= 0)                
                    return sourceWeb.GetFolder(docLibRootFolderUrl + "/");
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

        private static int? ConvertToInt(object obj)
        { 
            if(obj == null) return null;
            return int.Parse(obj.ToString());
        }
        #endregion
    }   
}
