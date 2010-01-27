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
                mhaDTO.CreatedDt = mha.CreatedDate;
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
                mhaDTO.FinalResolution = mha.FinalResolution;
                mhaDTO.FinalResolutionDate = mha.FinalResolutionDate;
                mhaDTO.FinalResolutionNotes = mha.FinalResolutionNotes;
                mhaDTO.GseLookup = mha.GSELookup;
                mhaDTO.GseNotes = mha.GseNotes;
                mhaDTO.HpfNotes = mha.HpfNotes;
                //mhaDTO.ReplicateDt = mha.ReplicateDt;
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
                mhaHelp.TrialModStartedBeforeStept1 = mha.TrialModStartedBeforeStept1;
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
    }
}
