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
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting;


namespace HPF.FutureState.Web.PrintSummary
{
    public partial class PrintSummaryUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //LoadReport();
        }
        protected void LoadReport()
        {
            int caseid = Convert.ToInt32(Request.QueryString["CaseID"].ToString());
            ReportParameter reportParameter = new ReportParameter();
            reportParameter.Name = "pi_fc_id";
            reportParameter.Values[1] = caseid.ToString();
            ReportViewerPrintSummary.ServerReport.SetParameters(new ReportParameter[]{reportParameter});
            ReportViewerPrintSummary.ServerReport.ReportServerUrl = new Uri("http://HPF-01/REPORTSERVER");
            ReportViewerPrintSummary.ServerReport.ReportPath = @"HPF_Report/rpt_CounselingSummary.rdl";
            ReportViewerPrintSummary.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            //ReportViewerCredentials rvc = new  ReportViewerCredentials("TestUser", "TestPassword", "");
            //ReportViewerPrintSummary.ServerReport.ReportServerCredentials = rvc;
        }
    }
}