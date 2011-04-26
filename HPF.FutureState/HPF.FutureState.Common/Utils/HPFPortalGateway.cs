using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.SharePointAPI.BusinessEntity;
using HPF.SharePointAPI.Controllers;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.Common.Utils
{
    public static class HPFPortalGateway
    {

        public static void SendFannieMaeReport(HPFPortalFannieMae fannieMae)
        {
            var fannieMaeInfo = new FannieMaeInfo
            {
                Name = fannieMae.ReportFileName,
                StartDt = fannieMae.StartDt,
                EndDt = fannieMae.EndDt,
                File = fannieMae.ReportFile,
                FileName = fannieMae.ReportFileName
                
            };

            var spFolderName = fannieMae.SPFolderName;
            var result = DocumentCenterController.Upload(fannieMaeInfo, spFolderName);
            if (!result.Successful)
                Logger.Write(result.Error.Message, "General");

        }

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
            
            var spFolderName = summary.SPFolderName;
            var result = DocumentCenterController.Upload(counselingSummaryInfo, spFolderName);
            if (!result.Successful)
                Logger.Write(result.Error.Message, "General");

        }
        public static void SendInvoiceReportFile(HPFPortalInvoice invoice)
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
                Name = summary.ReportFileName,
                File = summary.ReportFile,
                Date = summary.Date,
                AgencyName=summary.AgencyName,
                PayableNumber=summary.PayableNumber,
                PayableDate=summary.PayableDate,
                Month=DateTime.Now.Month.ToString(),
                Year=DateTime.Now.Year
            };

            //todo: please specify spFolderName
            string spFolderName = summary.SPFolderName;
            var result = DocumentCenterController.Upload(NewAgencyPayableInfo, spFolderName);
            if (!result.Successful)
                Logger.Write(result.Error.Message, "General");
        }

        public static void SendCompletedCounselingDetailReport(HPFPortalCompletedCounselingDetail counselingDetail)
        {
            var counselingInfo = new CompletedCounselingDetailInfo
            {
                Name = counselingDetail.ReportFilename,
                File = counselingDetail.ReportFile,
                FromDate = counselingDetail.FromDate,
                ToDate = counselingDetail.ToDate
            };

            string spFolderName = counselingDetail.SPFolderName;
            var result = DocumentCenterController.Upload(counselingInfo, spFolderName);
            if (!result.Successful)
                Logger.Write(result.Error.Message, "General");
        }
        public static void GenerateCouncelorList(HPFPortalCounselor hpfCounselor)
        {
            List<CounselorInfo> counselorList = new List<CounselorInfo>();
            foreach (CounselorDTO counsorlor in hpfCounselor.Counselors)
            {
                CounselorInfo info = new CounselorInfo();
                info.AgencyName = counsorlor.AgencyName;
                info.CounselorEmail = counsorlor.CounselorEmail;
                info.CounselorExt = counsorlor.CounselorExt;
                info.counselorFirstName = counsorlor.counselorFirstName;
                info.CounselorLastName = counsorlor.CounselorLastName;
                info.CounselorPhone = counsorlor.CounselorPhone;
                info.Title = counsorlor.counselorFirstName + " " + counsorlor.CounselorLastName;

                counselorList.Add(info);
            }

            DocumentCenterController.GenerateWeeklyCounselorList(counselorList, hpfCounselor.SPFolderName); 
        }

        public static MHAEscalationDTOCollecion GetMHAEscaltions()
        {
            var mhaInfos = DocumentCenterController.GetMHAEscalationList();
            MHAEscalationDTOCollecion result = new MHAEscalationDTOCollecion();

            foreach (MHAEscalationInfo mha in mhaInfos)
            {
                MHAEscalationDTO mhaDTO = new MHAEscalationDTO();
                mhaDTO.ItemId = mha.ItemId;
                mhaDTO.ItemCreatedDt = mha.ItemCreatedDate;
                mhaDTO.ItemCreatedUser = mha.ItemCreatedUser;
                mhaDTO.ItemModifiedDt = mha.ItemModifiedDate;
                mhaDTO.ItemModifiedUser = mha.ItemModifiedUser;
                mhaDTO.AcctNum = mha.LoanNumber;
                mhaDTO.AgencyCaseNum = mha.MMICaseId;
                mhaDTO.BorrowerFname = mha.Firstname;
                mhaDTO.BorrowerLname = mha.Lastname;
                mhaDTO.CounselorEmail = mha.CounselorEmail;
                mhaDTO.CounselorName = mha.CounselorName;
                mhaDTO.CounselorPhone = mha.CounselorPhone;                
                mhaDTO.CurrentOwnerOfIssue = mha.CurrentOwnerOfIssue;
                mhaDTO.EscalatedToFannie = mha.EscalatedToFannie;
                mhaDTO.EscalatedToFreddie = mha.EscalatedToFreddie;
                mhaDTO.EscalatedToMMIMgmt = mha.EscalatedToMMIMgmt;
                mhaDTO.Escalation = mha.Escalation;
                //mhaDTO.EscalationCd = mha.EscalationCd;
                mhaDTO.EscalationTeamNotes = mha.EscalationTeamNotes;
                //mhaDTO.FcId = mha.FcId;
                mhaDTO.FcId = null;
                mhaDTO.FinalResolution = mha.FinalResolution;
                mhaDTO.FinalResolutionDate = mha.FinalResolutionDate;
                mhaDTO.FinalResolutionNotes = mha.FinalResolutionNotes;
                mhaDTO.GseLookup = mha.GSELookup;
                mhaDTO.GseNotes = mha.GseNotes;
                mhaDTO.HpfNotes = mha.HpfNotes;
                mhaDTO.ResolvedBy = mha.ResolvedBy;
                mhaDTO.Servicer = mha.Servicer;
                //mhaDTO.ServicerId = mha.ServicerId;
                mhaDTO.Address = mha.Address;
                mhaDTO.City = mha.City;
                mhaDTO.State = mha.State;
                mhaDTO.Zip = mha.Zip;
                mhaDTO.BorrowerEmail = mha.BorrowerEmail;
                mhaDTO.BestNumber = mha.BestNumberToCall;
                mhaDTO.BestTime = mha.BestTimetoReach;
                mhaDTO.HandleTimeHrs = mha.HandleTimeHrs;
                mhaDTO.HandleTimeMins = mha.HandleTimeMins;
                mhaDTO.EscalatedToGseDt = mha.EscalatedToGSEDate;
                mhaDTO.GSENotesCompletedDt = mha.GSENotesCompletedDate;
                mhaDTO.EscalatedToMMIMgmtDt = mha.EscalatedToMMIMgmtDate;

                mhaDTO.CommitmentInd = mha.Commitment;
                mhaDTO.FollowupDt = mha.FollowupDateTime;
                mhaDTO.CommitmentClosedDt = mha.CommitmentClosed;
                mhaDTO.EscalatedToHscInd = mha.EscalatedToHSC;

                mhaDTO.SetInsertTrackingInformation("System");
                result.Add(mhaDTO);
            }

            return result;
        }

        public static MHAHelpDTOCollection GetMHAHelps()
        {
            var mhaInfos = DocumentCenterController.GetMHAHelpList();
            MHAHelpDTOCollection result = new MHAHelpDTOCollection();

            foreach (MHAHelpInfo mha in mhaInfos)
            {
                MHAHelpDTO mhaHelp = new MHAHelpDTO();
                mhaHelp.ItemId = mha.ItemId;
                mhaHelp.AcctNum = mha.LoanNumber;
                mhaHelp.AllDocumentsSubmitted = mha.AllDocumentsSubmitted;
                mhaHelp.BestTimeToReach = mha.BestTimeToReach;
                mhaHelp.BorrowerEmail = mha.Email;
                mhaHelp.BorrowerFName = mha.FirstName;
                mhaHelp.BorrowerInTrialMod = mha.BorrowerInTrialMod;
                mhaHelp.BorrowerLName = mha.LastName;
                mhaHelp.BorrowerPhone = mha.Phone;
                mhaHelp.CallSource = mha.CallSource;
                mhaHelp.City = mha.City;
                mhaHelp.Comments = mha.Comments;
                mhaHelp.CounselorEmail = mha.CounselorEmail;
                mhaHelp.CounselorName = mha.CounselorName;
                mhaHelp.CurrentOnPayments = mha.CurrentOnPayments;
                mhaHelp.DocumentsSubmitted = mha.DocumentsSubmitted;
                mhaHelp.FinalResolutionNotes = mha.FinalResolutionNotes;
                mhaHelp.IfWageEarnerWereTwoPayStubsSentIn = mha.IfWageEarnerWereTwoPayStubsSentIn;
                mhaHelp.ItemCreatedDt = mha.ItemCreatedDate;
                mhaHelp.ItemCreatedUser = mha.ItemCreatedUser;
                mhaHelp.ItemModifiedDt = mha.ItemModifiedDate;
                mhaHelp.ItemModifiedUser = mha.ItemModifiedUser;
                mhaHelp.MHAConversionCampainFields = mha.MHAConversionCampainFields;
                mhaHelp.MHAHelpReason = mha.MHAHelpReason;
                mhaHelp.MHAHelpResolution = mha.MHAHelpResolution;
                mhaHelp.MMICaseId = mha.MMICaseId;
                mhaHelp.PrivacyConsent = mha.PrivacyConsent;
                mhaHelp.Servicer = mha.Servicer;
                mhaHelp.State = mha.State;
                mhaHelp.TrialModStartedBeforeStept1 = mha.TrialModStartedBeforeNov1;
                mhaHelp.VoicemailDt = mha.VoicemailDate;
                mhaHelp.WageEarner = mha.WageEarner;
                mhaHelp.Zip = mha.Zip;
                mhaHelp.HandleTimeHrs = mha.HandleTimeHrs;
                mhaHelp.HandleTimeMins = mha.HandleTimeMins;
                mhaHelp.SetInsertTrackingInformation("System");
                result.Add(mhaHelp);
            }

            return result;
        }

        public static ScamDTOCollection GetScams()
        {
            var scamInfos = DocumentCenterController.GetScamList();
            ScamDTOCollection result = new ScamDTOCollection();

            foreach (ScamInfo scamInfo in scamInfos)
            {
                ScamDTO scam = new ScamDTO();
                scam.ItemId= scamInfo.ItemId;
                scam.VoiceMailOnlyInd = scamInfo.VoiceMailOnlyInd;
                scam.LoanModificationScamConsent = scamInfo.LoanModificationScamConsent;
                scam.HotlineSource = scamInfo.HotlineSource;
                scam.InformationSharingConsent = scamInfo.InformationSharingConsent;
                scam.MortgageModificationOffer = scamInfo.MortgageModificationOffer;
                scam.ListOfWereYous = scamInfo.ListOfWereYous;
                scam.BorrowerFName = scamInfo.BorrowerFName;
                scam.BorrowerLName = scamInfo.BorrowerLName;
                scam.BorrowerPhone = scamInfo.BorrowerPhone;
                scam.BorrowerSecondPhone = scamInfo.BorrowerSecondPhone;
                scam.Address1 = scamInfo.Address1;
                scam.Address2 = scamInfo.Address2;
                scam.City = scamInfo.City;
                scam.State = scamInfo.State;
                scam.Zip = scamInfo.Zip;
                scam.BorrowerAgeRange = scamInfo.BorrowerAgeRange;
                scam.BorrowerEmail = scamInfo.BorrowerEmail;
                scam.BorrowerRace = scamInfo.BorrowerRace;
                scam.ListOfServicesOffered = scamInfo.ListOfServicesOffered;
                scam.GuraranteedLoanModificationInd = scamInfo.GuraranteedLoanModification;
                scam.FeePaidInd = scamInfo.FeePaid;
                scam.TotalAmountPaid = scamInfo.TotalAmountPaid;
                scam.PaymentType = scamInfo.PaymentType;
                scam.ContractServicesPerfomed = scamInfo.ContractServicesPerfomed;
                scam.MainContact = scamInfo.MainContact;
                scam.ScamOrgName = scamInfo.ScamOrgName;
                scam.ScamOrgAddress = scamInfo.ScamOrgAddress;
                scam.ScamOrgCity = scamInfo.ScamOrgCity;
                scam.ScamOrgState = scamInfo.ScamOrgState;
                scam.ScamOrgZip = scamInfo.ScamOrgZip;
                scam.ScamOrgPhone = scamInfo.ScamOrgPhone;
                scam.ScamOrgFax = scamInfo.ScamOrgFax;
                scam.ScamOrgURL = scamInfo.ScamOrgURL;
                scam.ScamOrgEmail = scamInfo.ScamOrgEmail;
                scam.FindOutAboutDate = scamInfo.FindOutAboutDate;
                scam.LastContactDate = scamInfo.LastContactDate;
                scam.Summary = scamInfo.Summary;
                scam.ScamOrgStatus = scamInfo.ScamOrgStatus;
                scam.CurrentLoanStatus = scamInfo.CurrentLoanStatus;
                scam.PriorLoanStatus = scamInfo.PriorLoanStatus;
                scam.AgenciesContacted = scamInfo.AgenciesContacted;
                scam.OptionsOfferedByLender = scamInfo.OptionsOfferedByLender;
                scam.MultiScammerCount = scamInfo.MultiScammerCount;
                scam.MultiScammerContactInfo = scamInfo.MultiScammerContactInfo;
                scam.ScamOrgAddtlContact = scamInfo.ScamOrgAddtlContact;
                scam.ServicerContactSinceScamInd = scamInfo.ServicerContactSinceScamInd;
                scam.GovernmentAffiliatedInd = scamInfo.GovernmentAffiliatedInd;
                scam.ServicerAffiliatedInd = scamInfo.ServicerAffiliatedInd;
                scam.BorrowerReferredOthersInd = scamInfo.BorrowerReferredOthersInd;
                scam.WillingToShareStoryInd = scamInfo.WillingToShareStoryInd;
                scam.WillingToSendInfoInd = scamInfo.WillingToSendInfoInd;
                scam.ReferredToCounselingInd = scamInfo.ReferredToCounselingInd;
                scam.HpfMediaCandidateInd = scamInfo.HpfMediaCandidateInd;
                scam.HpfSuccessStoryInd = scamInfo.HpfSuccessStoryInd;
                scam.DeclinedCounselingInd = scamInfo.DeclinedCounselingInd;
                scam.CounselorName = scamInfo.CounselorName;
                scam.CounselorEmail = scamInfo.CounselorEmail;
                scam.Agency = scamInfo.Agency;
                scam.AgencyCaseNum = scamInfo.AgencyCaseNum;
                scam.LoanNumber = scamInfo.LoanNumber;
                scam.Servicer = scamInfo.Servicer;
                scam.ItemCreatedDt = scamInfo.ItemCreatedDate;
                scam.ItemCreatedUser = scamInfo.ItemCreatedUser;
                scam.ItemModifiedDt = scamInfo.ItemModifiedDate;
                scam.ItemModifiedUser = scamInfo.ItemModifiedUser;
                scam.SetInsertTrackingInformation("System");
                result.Add(scam);
            }
            return result;
        }

        public static CounselingSummaryAuditLogDTOCollection GetCounselingSummaryAuditLog()
        {
            var counselingAudit = DocumentCenterController.GetCounselingSummaryAuditLog();
            CounselingSummaryAuditLogDTOCollection result = new CounselingSummaryAuditLogDTOCollection();

            foreach (CounselingSummaryAuditLogInfo auditInfor in counselingAudit)
            {
                CounselingSummaryAuditLogDTO auditDTO = new CounselingSummaryAuditLogDTO();
                auditDTO.CompletedDate = auditInfor.CompletedDate;
                auditDTO.CounselingSummaryFile = auditInfor.CounselingSummaryFile;
                auditDTO.ItemCreatedDate = auditInfor.ItemCreatedDate;
                auditDTO.OccurredDate = auditInfor.OccurredDate;
                auditDTO.LoanNumber = auditInfor.LoanNumber;
                auditDTO.Servicer = auditInfor.Servicer;
                auditDTO.UserId = auditInfor.UserId;
                auditDTO.ArchiveName = auditInfor.ArchiveName;
                auditDTO.SetInsertTrackingInformation("System");
                result.Add(auditDTO);
            }
            return result;
        }
    }
}
