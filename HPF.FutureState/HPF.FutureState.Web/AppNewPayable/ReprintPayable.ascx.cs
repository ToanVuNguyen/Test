using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting;
using HPF.FutureState.Common;
using HPF.FutureState.Web.Security;
using System.Net;

namespace HPF.FutureState.Web.AppNewPayable
{
    public partial class ReprintPayable : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadReport();
        }
        protected void LoadReport()
        {
            int agencypayableid = Convert.ToInt32(Request.QueryString["agencyPayableId"].ToString());

            ReportViewerCredential rvc = new ReportViewerCredential();
            ReportViewerPrintSummary.ServerReport.ReportServerCredentials = rvc;
            ReportViewerPrintSummary.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            //
            SetReportServerUrl();
            //
            SetReportPath();
            //
            ReportParameter reportParameter = new ReportParameter("pi_agency_payable_id", agencypayableid.ToString());
            ReportViewerPrintSummary.ServerReport.SetParameters(new ReportParameter[] { reportParameter });
        }
        private void SetReportServerUrl()
        { 
            ReportViewerPrintSummary.ServerReport.ReportServerUrl = new Uri(HPFConfigurationSettings.REPORTSERVER_URL);
        }
        private void SetReportPath()
        {
            ReportViewerPrintSummary.ServerReport.ReportPath =
                HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_AGENCY_PAYABLE_REPORT);
        }
    }
}