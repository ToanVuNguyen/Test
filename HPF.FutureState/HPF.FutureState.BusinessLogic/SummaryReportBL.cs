using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.DataAccess;

namespace HPF.FutureState.BusinessLogic
{
    public class SummaryReportBL : BaseBusinessLogic
    {
        private static readonly SummaryReportBL instance = new SummaryReportBL();
        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static SummaryReportBL Instance
        {
            get { return instance; }
        }

        protected SummaryReportBL()
        {
            
        }

        /// <summary>
        /// Generate summary report as pdf format
        /// </summary>
        /// <param name="fc_id"></param>
        /// <returns>PDF file buffer</returns>
        public byte[] GenerateSummaryReport(int? fc_id)
        {
            var reportExport = new ReportingExporter();
            var attachContent = reportExport.ExportToPdf();
            reportExport.ReportPath = @"HPF_Report/rpt_CounselingSummary";
            reportExport.SetReportParameter("pi_fc_id", fc_id.ToString());
            return attachContent;
        }
        
        /// <summary>
        /// Get All servicer of a case
        /// </summary>
        /// <param name="fcId"></param>
        /// <returns></returns>
        public ServicerDTOCollection GetServicerbyFcId(int? fcId)
        {
            return ServicerDAO.Instance.GetServicersByFcId(fcId);
        }

        /// <summary>
        /// Process a completed case summary
        /// </summary>
        /// <param name="fc_id"></param>
        public void SendCompletedCaseSummary(int? fc_id)
        {
            
        }
    }
}
