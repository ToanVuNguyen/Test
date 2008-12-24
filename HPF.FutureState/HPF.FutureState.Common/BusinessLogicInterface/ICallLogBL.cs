using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

namespace HPF.FutureState.Common.BusinessLogicInterface
{
    public interface ICallLogBL
    {
        /// <summary>
        /// Insert a CallLog
        /// </summary>
        /// <param name="aCallLog"></param>
        /// <returns>callLogID in string format</returns>
        int InsertCallLog(CallLogDTO aCallLog);
        /// <summary>
       
        /// Get a CallLog by CallLogId
        /// </summary>
        /// <param name="callLogId">CallLogId</param>
        /// <returns></returns>
        CallLogDTO RetrieveCallLog(int callLogId);        
    }
}
