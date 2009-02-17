using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;

namespace HPF.FutureState.BusinessLogic
{
    public class SummaryReportBL : BaseBusinessLogic
    {
        /// <summary>
        /// Generate summary report as pdf format
        /// </summary>
        /// <param name="fc_id"></param>
        /// <returns>PDF file buffer</returns>
        public byte[] GenerateSummaryReport(int fc_id)
        {
            return null;
        }

        //Get All service if a case.

        public ServicerDTOCollection GetServicerbyFcId(int? fcId)
        {
            return ServicerDAO.Instance.GetServicersByFcId(fcId);
        }
    }
}
