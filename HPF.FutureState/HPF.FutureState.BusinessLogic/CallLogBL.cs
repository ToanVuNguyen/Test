using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;

namespace HPF.FutureState.BusinessLogic
{
    public class CallLogBL : BaseBusinessLogic, ICallLogBL
    {
        private static readonly CallLogBL instance = new CallLogBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CallLogBL Instance
        {
            get
            {
                return instance;
            }
        }
       
        protected CallLogBL()
        {
            
        }

        #region Implementation of ICallLogBL

        /// <summary>
        /// Insert a CallLog
        /// </summary>
        /// <param name="aCallLog"></param>
        public void InsertCallLog(CallLogDTO aCallLog)
        {

           CallLogDAO.Instance.InsertCallLog(aCallLog);
        }

        /// <summary>
        /// Update a Callog
        /// </summary>
        /// <param name="aCallLog"></param>
        public void UpdateCallLog(CallLogDTO aCallLog)
        {
            CallLogDAO.Instance.UpdateCallLog(aCallLog);
        }

        /// <summary>
        /// Get a CallLog by CallLogId
        /// </summary>
        /// <param name="callLogId">CallLogId</param>
        /// <returns></returns>
        public CallLogDTO GetCallLog(int callLogId)
        {
            return CallLogDAO.Instance.ReadCallLog(callLogId);
        }

        #endregion
    }
}
