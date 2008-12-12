using System;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CallLogDTO : BaseDTO
    {        
        public int CallId { get; set; }

        [StringRequiredValidator(Ruleset = "Default",MessageTemplate = "External call number is required.")]
        public string ExtCallNumber { get; set; }

        public string AgencyId { get; set; }

        [InTakeDateValidator(60, MessageTemplate = "StartDate problem", Ruleset = "Default")]
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public string DNIS { get; set; }

        public string CallCenter { get; set; }

        public string CallCenterCD { get; set; }

        public string CallResource { get; set; }

        public string ReasonToCall { get; set; }

        public string AccountNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CounselPastYRInd { get; set; }

        public string MtgProbInd { get; set; }

        public string PastDueInd { get; set; }

        public string PastDueSoonInd { get; set; }

        public int PastDueMonths { get; set; }

        public int ServicerId { get; set; }

        public string OtherServicerName { get; set; }

        public string PropZip { get; set; }

        public int PrevCounselorId { get; set; }

        public int PrevAgencyId { get; set; }

        public string SelectedAgencyId { get; set; }       

        public string ScreenRout { get; set; }

        public int FinalDispo { get; set; }

        public string TransNumber { get; set; }

        public string OutOfNetworkReferralTBD { get; set; }

        public CallLogDTO(CallLogWSDTO callLog)
        {
            if (callLog.CallId != null)
            {
                callLog.CallId = callLog.CallId.Replace("HPF_", "");
                int id = 0;
                int.TryParse(callLog.CallId, out id);
                this.CallId = id;
            }
            this.ExtCallNumber = callLog.ExtCallNumber;
            this.AgencyId = callLog.AgencyId;
            this.StartDate = callLog.StartDate;
            this.EndDate = callLog.EndDate;
            this.DNIS = callLog.DNIS;
            this.CallCenter = callLog.CallCenter;
            this.CallCenterCD = callLog.CallCenterCD;
            this.CallResource = callLog.CallResource;
            this.ReasonToCall = callLog.ReasonToCall;
            this.AccountNumber = callLog.AccountNumber;
            this.FirstName = callLog.FirstName;
            this.LastName = callLog.LastName;
            this.CounselPastYRInd = callLog.CounselPastYRInd;
            this.MtgProbInd = callLog.MtgProbInd;
            this.PastDueInd = callLog.PastDueInd;
            this.PastDueSoonInd = callLog.PastDueSoonInd;
            this.PastDueMonths = callLog.PastDueMonths;
            this.ServicerId = callLog.ServicerId;
            this.OtherServicerName = callLog.OtherServicerName;
            this.PropZip = callLog.PropZip;
            this.PrevCounselorId = callLog.PrevCounselorId;
            this.PrevAgencyId = callLog.PrevAgencyId;
            this.SelectedAgencyId = callLog.SelectedAgencyId;
            this.ScreenRout = callLog.ScreenRout;
            this.FinalDispo = callLog.FinalDispo;
            this.TransNumber = callLog.TransNumber;
            this.OutOfNetworkReferralTBD = callLog.OutOfNetworkReferralTBD;
            this.CreateDate = callLog.CreateDate;
            this.CreateUserId = callLog.CreateUserId;
            this.CreateAppName = callLog.CreateAppName;
            this.ChangeLastDate = callLog.ChangeLastDate;
            this.ChangeLastUserId = callLog.ChangeLastUserId;
            this.ChangeLastAppName = callLog.ChangeLastAppName;
        }

        public CallLogDTO() { }
    }
}
