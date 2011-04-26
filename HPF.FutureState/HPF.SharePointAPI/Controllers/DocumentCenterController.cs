using System;
using System.Collections.Generic;
using System.Text;
using HPF.SharePointAPI.BusinessEntity;
using Microsoft.SharePoint;
using System.Security;
using HPF.SharePointAPI.ContentTypes;
using System.Net.Mail;
using System.Configuration;
using Microsoft.SharePoint.Utilities;

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

        public static IList<CounselingSummaryAuditLogInfo> GetCounselingSummaryAuditLog()
        {             
            try
            {
                SPUserToken token = GetUploadSPUserToken(DocumentCenter.Default.DocumentCenterWeb, DocumentCenter.Default.CounselingSummaryAuditLogLoginName);

                List<CounselingSummaryAuditLogInfo> counseling = new List<CounselingSummaryAuditLogInfo>();
                using (SPSite site = new SPSite(DocumentCenter.Default.SharePointSite, token))
                {
                    SPWeb web = site.AllWebs[DocumentCenter.Default.DocumentCenterWeb];
                    //SPWeb web = site.OpenWeb();
                    web.AllowUnsafeUpdates = true;                 
                    SPList docLib = web.Lists[DocumentCenter.Default.CounselingSummaryAuditLogList];

                    SPQuery query = new SPQuery();
                    query.Query = "<Where><Neq><FieldRef Name='Completed'/><Value Type='Boolean'>1</Value></Neq></Where>";

                    SPListItemCollection listItems = docLib.GetItems(query);                    

                    foreach (SPListItem item in listItems)
                    {
                        byte[] buffer = item.File.OpenBinary();
                        UTF8Encoding enc = new UTF8Encoding();
                        string str = enc.GetString(buffer);
                        string[] rows = str.Split('\n');                        
                        for (int i = 1; i < rows.Length; i++)
                        {
                            string[] auditValues = rows[i].Split(',');
                            CounselingSummaryAuditLogInfo info = new CounselingSummaryAuditLogInfo();
                            if (auditValues.Length < 8) continue;
                            info.ArchiveName = auditValues[0];
                            info.UserId = auditValues[1];
                            info.OccurredDate = (string.IsNullOrEmpty(auditValues[2]) ? null : (DateTime?)DateTime.Parse(auditValues[2]));
                            info.CounselingSummaryFile = auditValues[3];
                            info.Servicer = auditValues[4];
                            info.LoanNumber = auditValues[5];
                            info.CompletedDate = (string.IsNullOrEmpty(auditValues[6]) ? null : (DateTime?)DateTime.Parse(auditValues[6]));
                            info.ItemCreatedDate = (string.IsNullOrEmpty(auditValues[7]) ? null : (DateTime?)DateTime.Parse(auditValues[7]));

                            counseling.Add(info);
                        }
                        item["Completed"] = true;
                        item.Update();
                    }
                }

                return counseling;
            }
            catch (Exception ex)
            {
                WriteLog("----------------\n" + System.DateTime.Now.ToString() + "--" + ex.Message + "\n" + ex.StackTrace + "--user:" + DocumentCenter.Default.CounselingSummaryAuditLogLoginName);
                throw ex;
            }            
        }
        public static IList<MHAEscalationInfo> GetMHAEscalationList()
        {
            string trackingName = "";
            try
            {
                SPUserToken token = GetUploadSPUserToken(DocumentCenter.Default.DocumentCenterWeb, DocumentCenter.Default.MHAEscalationLoginName);
                
                List<MHAEscalationInfo> mhaList = new List<MHAEscalationInfo>();                
                using (SPSite site = new SPSite(DocumentCenter.Default.SharePointSite, token))
                {                    
                    //SPWeb web = site.AllWebs[DocumentCenter.Default.DocumentCenterWeb];  
                    SPWeb web = site.OpenWeb();
                    web.AllowUnsafeUpdates = true;                 
                    SPList docLib = web.Lists[DocumentCenter.Default.MHAEscalationList];

                    //Set previous day to grab sharepoint items which have modified date in this range
                    int importDayRange = 4;
                    if (!string.IsNullOrEmpty(HPF_MHA_ESCALATION_IMPORT_DAY_RANGE))
                    {
                        int.TryParse(HPF_MHA_ESCALATION_IMPORT_DAY_RANGE, out importDayRange);
                    }
                    SPQuery query = new SPQuery();
                    query.Query = String.Format("<Where><Gt><FieldRef Name='Modified'/>" +
                                    "<Value Type='DateTime' StorageTZ='TRUE'>{0}</Value></Gt></Where>",
                                    SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.UtcNow.AddDays(0 - importDayRange)));
                    SPListItemCollection listItems = docLib.GetItems(query);
                    
                    foreach (SPListItem item in listItems)
                    {
                        MHAEscalationInfo mhaInfo = new MHAEscalationInfo();
                        trackingName = MHAEscalation.Default.ItemId;
                        mhaInfo.ItemId = ConvertToInt(item[MHAEscalation.Default.ItemId]); trackingName = MHAEscalation.Default.LoanNumber;
                        mhaInfo.LoanNumber = (string)item[MHAEscalation.Default.LoanNumber]; trackingName = MHAEscalation.Default.MMICaseId;
                        mhaInfo.MMICaseId = (string)item[MHAEscalation.Default.MMICaseId];trackingName = MHAEscalation.Default.FirstName;
                        mhaInfo.Firstname = (string)item[MHAEscalation.Default.FirstName];trackingName = MHAEscalation.Default.LastName;
                        mhaInfo.Lastname = (string)item[MHAEscalation.Default.LastName]; trackingName = MHAEscalation.Default.CounselorEmail;
                        mhaInfo.CounselorEmail = (string)item[MHAEscalation.Default.CounselorEmail]; trackingName = MHAEscalation.Default.CounselorName;
                        mhaInfo.CounselorName = (string)item[MHAEscalation.Default.CounselorName]; trackingName = MHAEscalation.Default.CounselorPhone;
                        mhaInfo.CounselorPhone = (string)item[MHAEscalation.Default.CounselorPhone]; trackingName = MHAEscalation.Default.ItemCreatedDate;                                                
                        mhaInfo.ItemCreatedDate = (DateTime?)item[MHAEscalation.Default.ItemCreatedDate]; trackingName = MHAEscalation.Default.CurrentOwnerOfIssue;
                        mhaInfo.ItemCreatedUser = (string)item[MHAEscalation.Default.ItemCreatedUser]; trackingName = MHAEscalation.Default.ItemModifiedDate;
                        mhaInfo.ItemModifiedDate = (DateTime?)item[MHAEscalation.Default.ItemModifiedDate]; trackingName = MHAEscalation.Default.ItemModifiedUser;
                        mhaInfo.ItemModifiedUser = (string)item[MHAEscalation.Default.ItemModifiedUser]; trackingName = MHAEscalation.Default.LastName;
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
                        mhaInfo.HandleTimeMins = ConvertToInt(item[MHAEscalation.Default.HandleTimeMins]); trackingName = MHAEscalation.Default.EscalatedToGSEDate;
                        mhaInfo.EscalatedToGSEDate = (DateTime?)item[MHAEscalation.Default.EscalatedToGSEDate]; trackingName = MHAEscalation.Default.EscalatedToMMIMgmtDate;
                        mhaInfo.EscalatedToMMIMgmtDate = (DateTime?)item[MHAEscalation.Default.EscalatedToMMIMgmtDate]; trackingName = MHAEscalation.Default.GSENotesCompletedDate;
                        mhaInfo.GSENotesCompletedDate = (DateTime?)item[MHAEscalation.Default.GSENotesCompletedDate]; trackingName = MHAEscalation.Default.Commitment;
                        bool? Ind = (bool?)item[MHAEscalation.Default.Commitment]; trackingName = MHAEscalation.Default.FollowupDateTime;
                        mhaInfo.Commitment = (Ind.HasValue && Ind.Value == true) ? "Y" : "N";
                        mhaInfo.FollowupDateTime = (DateTime?)item[MHAEscalation.Default.FollowupDateTime]; trackingName = MHAEscalation.Default.CommitmentClosed;
                        mhaInfo.CommitmentClosed = (DateTime?)item[MHAEscalation.Default.CommitmentClosed]; trackingName = MHAEscalation.Default.EscalatedToHSC;
                        Ind = (bool?)item[MHAEscalation.Default.EscalatedToHSC];
                        mhaInfo.EscalatedToHSC = (Ind.HasValue && Ind.Value == true) ? "Y" : "N";
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
                WriteLog("----------------\n" + System.DateTime.Now.ToString() + "--" +  ex.Message + "\n" + ex.StackTrace + "--Current Error Field: " + trackingName);
                throw ex;
            }            
        }

        public static IList<MHAHelpInfo> GetMHAHelpList()
        {
            string trackingName = "";
            try
            {
                SPUserToken token = GetUploadSPUserToken(DocumentCenter.Default.DocumentCenterWeb, DocumentCenter.Default.MHAHelpLoginName);

                List<MHAHelpInfo> mhaList = new List<MHAHelpInfo>();
                using (SPSite site = new SPSite(DocumentCenter.Default.SharePointSite, token))
                {
                    SPWeb web = site.OpenWeb();
                    web.AllowUnsafeUpdates = true;
                    SPList docLib = web.Lists[DocumentCenter.Default.MHAHelpList];

                    //Set previous day to grab sharepoint items which have modified date in this range
                    int importDayRange = 4;
                    if (!string.IsNullOrEmpty(HPF_MHA_HELP_IMPORT_DAY_RANGE))
                    {
                        int.TryParse(HPF_MHA_HELP_IMPORT_DAY_RANGE, out importDayRange);
                    }
                    SPQuery query = new SPQuery();
                    query.Query = String.Format("<Where><Gt><FieldRef Name='Modified'/>" +
                                    "<Value Type='DateTime' StorageTZ='TRUE'>{0}</Value></Gt></Where>",
                                    SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.UtcNow.AddDays(0 - importDayRange)));
                    SPListItemCollection listItems = docLib.GetItems(query);
                    foreach (SPListItem item in listItems)
                    {
                        MHAHelpInfo mhaInfo = new MHAHelpInfo();
                        trackingName = MHAHelp.Default.ItemId;
                        mhaInfo.ItemId = ConvertToInt(item[MHAHelp.Default.ItemId]); trackingName = MHAHelp.Default.Address;
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
                        mhaInfo.MHAHelpResolution = (string)item[MHAHelp.Default.MHAHelpResolution];

                        if (string.IsNullOrEmpty(mhaInfo.MHAHelpReason))
                        {
                            trackingName = MHAHelp.Default.MHAHelpReasonOld;
                            mhaInfo.MHAHelpReason = (string)item[MHAHelp.Default.MHAHelpReasonOld];
                        }
                        if (string.IsNullOrEmpty(mhaInfo.MHAHelpResolution))
                        {
                            trackingName = MHAHelp.Default.MHAHelpResolutionOld;
                            mhaInfo.MHAHelpResolution = (string)item[MHAHelp.Default.MHAHelpResolutionOld];
                        }
                        trackingName = MHAHelp.Default.MMICaseId;
                        mhaInfo.MMICaseId = (string)item[MHAHelp.Default.MMICaseId]; trackingName = MHAHelp.Default.Phone;
                        mhaInfo.Phone = (string)item[MHAHelp.Default.Phone]; trackingName = MHAHelp.Default.PrivacyConsent;
                        mhaInfo.PrivacyConsent = (string)item[MHAHelp.Default.PrivacyConsent]; trackingName = MHAHelp.Default.Servicer;
                        mhaInfo.Servicer = (string)item[MHAHelp.Default.Servicer]; trackingName = MHAHelp.Default.State;
                        mhaInfo.State = (string)item[MHAHelp.Default.State]; trackingName = MHAHelp.Default.TrialModStartedBeforeNov1;
                        mhaInfo.TrialModStartedBeforeNov1 = (string)item[MHAHelp.Default.TrialModStartedBeforeNov1]; trackingName = MHAHelp.Default.VoicemailDate;
                        mhaInfo.VoicemailDate = (DateTime?)item[MHAHelp.Default.VoicemailDate]; trackingName = MHAHelp.Default.WageEarner;
                        mhaInfo.WageEarner = (string)item[MHAHelp.Default.WageEarner]; trackingName = MHAHelp.Default.Zip;
                        mhaInfo.Zip = (string)item[MHAHelp.Default.Zip]; trackingName = MHAHelp.Default.HandleTimeHrs;
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

        public static IList<ScamInfo> GetScamList()
        {
            string trackingName = "";
            try
            {
                SPUserToken token = GetUploadSPUserToken(DocumentCenter.Default.DocumentCenterWeb, DocumentCenter.Default.ScamLoginName);

                List<ScamInfo> scamList = new List<ScamInfo>();
                using (SPSite site = new SPSite(DocumentCenter.Default.SharePointSite, token))
                {
                    SPWeb web = site.OpenWeb();
                    web.AllowUnsafeUpdates = true;
                    SPList docLib = web.Lists[DocumentCenter.Default.ScamList];

                    //Set previous day to grab sharepoint items which have modified date in this range
                    int importDayRange = 4;
                    if (!string.IsNullOrEmpty(HPF_SCAM_IMPORT_DAY_RANGE))
                    {
                        int.TryParse(HPF_SCAM_IMPORT_DAY_RANGE, out importDayRange);
                    }
                    SPQuery query = new SPQuery();
                    query.Query = String.Format("<Where><Gt><FieldRef Name='Modified'/>" +
                                    "<Value Type='DateTime' StorageTZ='TRUE'>{0}</Value></Gt></Where>",
                                    SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.UtcNow.AddDays(0 - importDayRange)));
                    SPListItemCollection listItems = docLib.GetItems(query);
                    foreach (SPListItem item in listItems)
                    {
                        ScamInfo scamInfo = new ScamInfo();
                        trackingName = Scam.Default.ItemId;
                        scamInfo.ItemId = ConvertToInt(item[Scam.Default.ItemId]); trackingName = Scam.Default.VoiceMailOnlyInd;
                        scamInfo.VoiceMailOnlyInd = (string)item[Scam.Default.VoiceMailOnlyInd];trackingName = Scam.Default.LoanModificationScamConsent;
                        scamInfo.LoanModificationScamConsent = (string)item[Scam.Default.LoanModificationScamConsent]; trackingName = Scam.Default.HotlineSource;
                        scamInfo.HotlineSource = (string)item[Scam.Default.HotlineSource];trackingName = Scam.Default.InformationSharingConsent;
                        scamInfo.InformationSharingConsent = (string)item[Scam.Default.InformationSharingConsent]; trackingName = Scam.Default.MortgageModificationOffer;
                        scamInfo.MortgageModificationOffer = (string)item[Scam.Default.MortgageModificationOffer]; trackingName = Scam.Default.ListOfWereYous;
                        scamInfo.ListOfWereYous = (string)item[Scam.Default.ListOfWereYous]; trackingName = Scam.Default.BorrowerFName;
                        scamInfo.BorrowerFName = (string)item[Scam.Default.BorrowerFName]; trackingName = Scam.Default.BorrowerLName;
                        scamInfo.BorrowerLName = (string)item[Scam.Default.BorrowerLName]; trackingName = Scam.Default.BorrowerPhone;
                        scamInfo.BorrowerPhone = (string)item[Scam.Default.BorrowerPhone]; trackingName = Scam.Default.BorrowerSecondPhone;
                        scamInfo.BorrowerSecondPhone = (string)item[Scam.Default.BorrowerSecondPhone]; trackingName = Scam.Default.Address1;
                        scamInfo.Address1 = (string)item[Scam.Default.Address1]; trackingName = Scam.Default.Address2;
                        scamInfo.Address2 = (string)item[Scam.Default.Address2]; trackingName = Scam.Default.City;
                        scamInfo.City = (string)item[Scam.Default.City]; trackingName = Scam.Default.State;
                        scamInfo.State = (string)item[Scam.Default.State]; trackingName = Scam.Default.Zip;
                        scamInfo.Zip = (string)item[Scam.Default.Zip]; trackingName = Scam.Default.BorrowerAgeRange;
                        scamInfo.BorrowerAgeRange = (string)item[Scam.Default.BorrowerAgeRange]; trackingName = Scam.Default.BorrowerEmail;
                        scamInfo.BorrowerEmail = (string)item[Scam.Default.BorrowerEmail]; trackingName = Scam.Default.BorrowerRace;
                        scamInfo.BorrowerRace = (string)item[Scam.Default.BorrowerRace]; trackingName = Scam.Default.ListOfServicesOffered;
                        scamInfo.ListOfServicesOffered = (string)item[Scam.Default.ListOfServicesOffered]; trackingName = Scam.Default.GuraranteedLoanModification;
                        bool? Ind = (bool?)item[Scam.Default.GuraranteedLoanModification];
                        scamInfo.GuraranteedLoanModification = (Ind.HasValue && Ind.Value == true ? "Y" : "N");trackingName = Scam.Default.FeePaid;
                        scamInfo.FeePaid = (string)item[Scam.Default.FeePaid]; trackingName = Scam.Default.TotalAmountPaid;
                        scamInfo.TotalAmountPaid = ConvertToDouble(item[Scam.Default.TotalAmountPaid]); trackingName = Scam.Default.PaymentType;
                        scamInfo.PaymentType = (string)item[Scam.Default.PaymentType];trackingName = Scam.Default.ContractServicesPerfomed;
                        scamInfo.ContractServicesPerfomed = (string)item[Scam.Default.ContractServicesPerfomed]; trackingName = Scam.Default.MainContact;
                        scamInfo.MainContact = (string)item[Scam.Default.MainContact]; trackingName = Scam.Default.ScamOrgName;
                        scamInfo.ScamOrgName = (string)item[Scam.Default.ScamOrgName]; trackingName = Scam.Default.ScamOrgAddress;
                        scamInfo.ScamOrgAddress = (string)item[Scam.Default.ScamOrgAddress]; trackingName = Scam.Default.ScamOrgCity;
                        scamInfo.ScamOrgCity = (string)item[Scam.Default.ScamOrgCity]; trackingName = Scam.Default.ScamOrgState;
                        scamInfo.ScamOrgState = (string)item[Scam.Default.ScamOrgState]; trackingName = Scam.Default.ScamOrgZip;
                        scamInfo.ScamOrgZip = (string)item[Scam.Default.ScamOrgZip]; trackingName = Scam.Default.ScamOrgPhone;
                        scamInfo.ScamOrgPhone = (string)item[Scam.Default.ScamOrgPhone]; trackingName = Scam.Default.ScamOrgFax;
                        scamInfo.ScamOrgFax = (string)item[Scam.Default.ScamOrgFax];trackingName = Scam.Default.ScamOrgURL;
                        scamInfo.ScamOrgURL = (string)item[Scam.Default.ScamOrgURL]; trackingName = Scam.Default.ScamOrgEmail;
                        scamInfo.ScamOrgEmail = (string)item[Scam.Default.ScamOrgEmail]; trackingName = Scam.Default.FindOutAboutDate;
                        scamInfo.FindOutAboutDate = (DateTime?)item[Scam.Default.FindOutAboutDate]; trackingName = Scam.Default.LastContactDate;
                        scamInfo.LastContactDate = (DateTime?)item[Scam.Default.LastContactDate]; trackingName = Scam.Default.Summary;
                        scamInfo.Summary = (string)item[Scam.Default.Summary]; trackingName = Scam.Default.ScamOrgStatus;
                        scamInfo.ScamOrgStatus = (string)item[Scam.Default.ScamOrgStatus]; trackingName = Scam.Default.CurrentLoanStatus;
                        scamInfo.CurrentLoanStatus = (string)item[Scam.Default.CurrentLoanStatus]; trackingName = Scam.Default.PriorLoanStatus;
                        scamInfo.PriorLoanStatus = (string)item[Scam.Default.PriorLoanStatus]; trackingName = Scam.Default.AgenciesContacted;
                        scamInfo.AgenciesContacted = (string)item[Scam.Default.AgenciesContacted]; trackingName = Scam.Default.OptionsOfferedByLender;
                        scamInfo.OptionsOfferedByLender = (string)item[Scam.Default.OptionsOfferedByLender]; trackingName = Scam.Default.MultiScammerCount;
                        scamInfo.MultiScammerCount = (string)item[Scam.Default.MultiScammerCount]; trackingName = Scam.Default.MultiScammerContactInfo;
                        scamInfo.MultiScammerContactInfo = (string)item[Scam.Default.MultiScammerContactInfo]; trackingName = Scam.Default.ScamOrgAddtlContact;
                        scamInfo.ScamOrgAddtlContact = (string)item[Scam.Default.ScamOrgAddtlContact]; trackingName = Scam.Default.ServicerContactSinceScamInd;
                        scamInfo.ServicerContactSinceScamInd = (string)item[Scam.Default.ServicerContactSinceScamInd]; trackingName = Scam.Default.GovernmentAffiliatedInd;
                        scamInfo.GovernmentAffiliatedInd = (string)item[Scam.Default.GovernmentAffiliatedInd]; trackingName = Scam.Default.ServicerAffiliatedInd;
                        scamInfo.ServicerAffiliatedInd = (string)item[Scam.Default.ServicerAffiliatedInd]; trackingName = Scam.Default.BorrowerReferredOthersInd;
                        scamInfo.BorrowerReferredOthersInd = (string)item[Scam.Default.BorrowerReferredOthersInd]; trackingName = Scam.Default.WillingToShareStoryInd;
                        scamInfo.WillingToShareStoryInd = (string)item[Scam.Default.WillingToShareStoryInd]; trackingName = Scam.Default.WillingToSendInfoInd;
                        scamInfo.WillingToSendInfoInd = (string)item[Scam.Default.WillingToSendInfoInd]; trackingName = Scam.Default.ReferredToCounselingInd;
                        scamInfo.ReferredToCounselingInd = (string)item[Scam.Default.ReferredToCounselingInd]; trackingName = Scam.Default.HpfMediaCandidateInd;
                        scamInfo.HpfMediaCandidateInd = (string)item[Scam.Default.HpfMediaCandidateInd]; trackingName = Scam.Default.HpfSuccessStoryInd;
                        scamInfo.HpfSuccessStoryInd = (string)item[Scam.Default.HpfSuccessStoryInd]; trackingName = Scam.Default.DeclinedCounselingInd;
                        scamInfo.DeclinedCounselingInd = (string)item[Scam.Default.DeclinedCounselingInd];trackingName = Scam.Default.CounselorName;
                        scamInfo.CounselorName = (string)item[Scam.Default.CounselorName]; trackingName = Scam.Default.CounselorEmail;
                        scamInfo.CounselorEmail = (string)item[Scam.Default.CounselorEmail]; trackingName = Scam.Default.Agency;
                        scamInfo.Agency = (string)item[Scam.Default.Agency]; trackingName = Scam.Default.AgencyCaseNum;
                        scamInfo.AgencyCaseNum = (string)item[Scam.Default.AgencyCaseNum]; trackingName = Scam.Default.LoanNumber;
                        scamInfo.LoanNumber = (string)item[Scam.Default.LoanNumber]; trackingName = Scam.Default.Servicer;
                        scamInfo.Servicer = (string)item[docLib.Fields[Scam.Default.Servicer].InternalName]; trackingName = Scam.Default.ItemCreatedDate;
                        scamInfo.ItemCreatedDate = (DateTime?)item[Scam.Default.ItemCreatedDate]; trackingName = Scam.Default.ItemCreatedUser;
                        scamInfo.ItemCreatedUser = (string)item[Scam.Default.ItemCreatedUser]; trackingName = Scam.Default.ItemModifiedDate;
                        scamInfo.ItemModifiedDate = (DateTime?)item[Scam.Default.ItemModifiedDate]; trackingName = Scam.Default.ItemModifiedUser;
                        scamInfo.ItemModifiedUser = (string)item[Scam.Default.ItemModifiedUser];

                        if (!string.IsNullOrEmpty(scamInfo.Servicer))
                        {
                            int index = scamInfo.Servicer.IndexOf(";#");
                            if (index > 0)
                                scamInfo.Servicer = scamInfo.Servicer.Substring(index + 2, scamInfo.Servicer.Length - index - 2);
                        }
                        if (!string.IsNullOrEmpty(scamInfo.ItemCreatedUser))
                        {
                            int index = scamInfo.ItemCreatedUser.IndexOf(";#");
                            if (index > 0)
                                scamInfo.ItemCreatedUser = scamInfo.ItemCreatedUser.Substring(index + 2, scamInfo.ItemCreatedUser.Length - index - 2);
                        }
                        if (!string.IsNullOrEmpty(scamInfo.ItemModifiedUser))
                        {
                            int index = scamInfo.ItemModifiedUser.IndexOf(";#");
                            if (index > 0)
                                scamInfo.ItemModifiedUser = scamInfo.ItemModifiedUser.Substring(index + 2, scamInfo.ItemModifiedUser.Length - index - 2);
                        }
                        scamList.Add(scamInfo);
                    }
                }
                return scamList;
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
            SPUserToken token = GetUploadSPUserToken(DocumentCenter.Default.DocumentCenterWeb, DocumentCenter.Default.CounselorWeeklyLoginName);

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
                DocumentCenter.Default.DocumentCenterWeb,
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
                DocumentCenter.Default.DocumentCenterWeb,
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

        #region "completed counseling detail"
        public static IList<ResultInfo<CompletedCounselingDetailInfo>> Upload(IList<CompletedCounselingDetailInfo> counselingList, string spFolderName)
        {
            IList<ResultInfo<CompletedCounselingDetailInfo>> results = UploadSPFiles(
                ReportCenter.Default.ReportCenterWeb,
                ReportCenter.Default.AutoGeneratedLoginName,
                counselingList,
                ReportCenter.Default.AutoGeneratedList,
                spFolderName,
                UpdateCompletedCounselingDetailInfo);

            return results;
        }

        public static ResultInfo<CompletedCounselingDetailInfo> Upload(CompletedCounselingDetailInfo counseling, string spFolderName)
        {
            List<CompletedCounselingDetailInfo> counselingList = new List<CompletedCounselingDetailInfo>();
            counselingList.Add(counseling);
            IList<ResultInfo<CompletedCounselingDetailInfo>> results = Upload(counselingList, spFolderName);

            return results[0];
        }
        #endregion

        #region "Invoice"
        public static IList<ResultInfo<InvoiceInfo>> Upload(IList<InvoiceInfo> invoiceList, string spFolderName)
        {
            IList<ResultInfo<InvoiceInfo>> results = UploadSPFiles(
                DocumentCenter.Default.DocumentCenterWeb,
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
                DocumentCenter.Default.DocumentCenterWeb,
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
        private static IList<ResultInfo<T>> UploadSPFiles<T>(string spWeb, string userName, IList<T> items, string listName, string spFolderName, UpdateSPListItem<T> action) where T:BaseObject
        {   
            List<ResultInfo<T>> results = new List<ResultInfo<T>>();

            SPUserToken token = GetUploadSPUserToken(spWeb, userName);

            using (SPSite site = new SPSite(DocumentCenter.Default.SharePointSite, token))
            {
                ResultInfo<T> resultInfo;
                string fileUrl;
                SPWeb web = site.AllWebs[spWeb];                
                
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

        private static void UpdateCompletedCounselingDetailInfo(SPListItem spItem, CompletedCounselingDetailInfo counseling)
        {
            //spItem["Name"] = counseling.Name;
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
        private static SPUserToken GetUploadSPUserToken(string spWeb, string userName)
        {
            SPUserToken token = null;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(DocumentCenter.Default.SharePointSite))
                {
                    SPWeb web = site.AllWebs[spWeb]; //DocumentCenter.Default.DocumentCenterWeb
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
        //Batch Manger- MHA HELP Import
        public static string HPF_MHA_HELP_IMPORT_DAY_RANGE
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_MHA_HELP_IMPORT_DAY_RANGE"];
            }
        }
        public static string HPF_MHA_ESCALATION_IMPORT_DAY_RANGE
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_MHA_ESCALATION_IMPORT_DAY_RANGE"];
            }
        }
        public static string HPF_SCAM_IMPORT_DAY_RANGE
        {
            get
            {
                return ConfigurationManager.AppSettings["HPF_SCAM_IMPORT_DAY_RANGE"];
            }
        }
        private static int? ConvertToInt(object obj)
        {
            //try
            //{                
            if (obj == null) return null;
            double value = double.Parse(obj.ToString());

            return (int)value;
            //}
            //catch
            //{
            //    return null;
            //}
        }
        private static double? ConvertToDouble(object obj)
        {
            double returnValue = 0;
            if (obj == null || !double.TryParse(obj.ToString(), out returnValue))
                return null;
            return returnValue;
        }
        #endregion
    }   
}
