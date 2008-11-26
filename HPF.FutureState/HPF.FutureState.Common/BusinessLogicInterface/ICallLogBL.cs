using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.Common.BusinessLogicInterface
{
    public interface ICallLogBL
    {
        /// <summary>
        /// Insert a CallLog
        /// </summary>
        /// <param name="aCallLog"></param>
        void InsertCallLog(CallLogDTO aCallLog);
        /// <summary>
        /// Update a Callog
        /// </summary>
        /// <param name="aCallLog"></param>
        void UpdateCallLog(CallLogDTO aCallLog);
        /// <summary>
        /// Get a CallLog by CallLogId
        /// </summary>
        /// <param name="callLogId">CallLogId</param>
        /// <returns></returns>
        CallLogDTO GetCallLog(int callLogId);
        
    }
}
