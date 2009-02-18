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

        public ActivityLogDTO CreateSendSummaryWSActivityLog(SendSummaryRequest sendRequest)
        {
            ActivityLogDTO activityLog = new ActivityLogDTO();
            activityLog.FcId = sendRequest.FCId;
            activityLog.ActivityCd = "EMAIL";
            activityLog.ActivityDt = DateTime.Now;
            activityLog.ActivityNote = string.Concat(" Subject: ", sendRequest.EmailSubject + Constant.HPF_SECURE_EMAIL,
                " To: ", sendRequest.EmailToAddress, " Body: ", sendRequest.EmailBody);
            return activityLog;
        }  
    }
}