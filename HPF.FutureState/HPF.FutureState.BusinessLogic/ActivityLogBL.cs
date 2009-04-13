using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;
//using HPF.FutureState.Web.Security;

namespace HPF.FutureState.BusinessLogic
{
    public class ActivityLogBL : BaseBusinessLogic
    {
        private static readonly ActivityLogBL instance = new ActivityLogBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static ActivityLogBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected ActivityLogBL()
        {
        }
        public void InsertActivityLog(ActivityLogDTO activityLog)
        {
            ActivityLogDAO.Instance.InsertActivityLog(activityLog);
        }

        public ActivityLogDTOCollection GetActivityLog(int fcId)
        {
            return ActivityLogDAO.Instance.GetActivityLog(fcId);
        }

        public ActivityLogDTO CreateSendSummaryWSActivityLog(int fcId, string sender, string toEmail, string subject, string body)
        {
            ActivityLogDTO activityLog = new ActivityLogDTO();
            activityLog.FcId = fcId;
            activityLog.ActivityCd = "EMAIL";
            activityLog.ActivityDt = DateTime.Now;
            activityLog.ActivityNote = string.Concat(" To: ", toEmail, " From:", sender, " Subject: ", subject + Constant.HPF_SECURE_EMAIL, " Body: ", body);
                
            return activityLog;
        }  
    }
}