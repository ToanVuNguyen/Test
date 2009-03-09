using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.BusinessLogic
{
    class ResendToServicerBL
    {
          private static readonly ResendToServicerBL instance = new ResendToServicerBL();
        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static ResendToServicerBL Instance
        {
            get { return instance; }
        }

        protected ResendToServicerBL()
        {
            
        }
        private void SendCompletedCaseToQueueIfAny(ForeclosureCaseSetDTO fCaseSetFromAgency)
        {
            int? fcId = fCaseSetFromAgency.ForeclosureCase.FcId;
            try
            {
                var queue = new HPFSummaryQueue();
                queue.SendACompletedCaseToQueue(fcId);
            }
            catch
            {
                var QUEUE_ERROR_MESSAGE = "Fail to push completed case to Queue : " + fcId;
                //Log
                Logger.Write(QUEUE_ERROR_MESSAGE, Constant.DB_LOG_CATEGORY);
                //Send E-mail to support
                var hpfSupportEmail = HPFConfigurationSettings.HPF_SUPPORT_EMAIL;
                var mail = new HPFSendMail
                {
                    To = hpfSupportEmail,
                    Subject = QUEUE_ERROR_MESSAGE
                };
                mail.Send();
                //
            }
        }

    }
}
