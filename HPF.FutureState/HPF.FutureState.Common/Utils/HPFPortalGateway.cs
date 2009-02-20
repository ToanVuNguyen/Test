using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.SharePointAPI.BusinessEntity;
using HPF.SharePointAPI.Controllers;


namespace HPF.FutureState.Common.Utils
{
    public static class HPFPortalGateway
    {
        public static void SendSummary(HPFPortalConselingSummary summary)
        {
            var conselingSummaryInfo = new ConselingSummaryInfo
                                           {
                                               LoanNumber = summary.LoanNumber,
                                               CompletedDate = summary.CompletedDate,
                                               ForeclosureSaleDate = summary.ForeclosureSaleDate,
                                               File = summary.ReportFile,
                                               Delinquency = summary.Delinquency
                                           };
            DocumentCenterController.Upload(conselingSummaryInfo);
        }
    }
}
