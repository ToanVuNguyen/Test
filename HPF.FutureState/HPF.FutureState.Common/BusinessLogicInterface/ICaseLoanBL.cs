using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

namespace HPF.FutureState.Common.BusinessLogicInterface
{
    public interface ICaseLoanBL
    {        
        /// Get CaseLoan by FcID
        /// </summary>
        /// <param name="callLogId">fcId</param>
        /// <returns></returns>
        CaseLoanDTOCollection RetrieveCaseLoan(int fcId);        
    }
}
