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
using HPF.FutureState.Common;
using HPF.FutureState.Web.Security;
using System.Net;

namespace HPF.FutureState.Web.PrintQCReport
{
    public partial class PrintQCReportUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            //[Nam Cao]: just enable export formats which are configured in web.config
            DisableUnwantedExportFormats();
        }

        protected void LoadReport()
        {
            string reportType = Request.QueryString["ReportType"].ToString();
            ReportViewerCredential rvc = new ReportViewerCredential();
            ReportViewerPrintSummary.ServerReport.ReportServerCredentials = rvc;
            ReportViewerPrintSummary.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            SetReportServerUrl();
            if (!string.IsNullOrEmpty(reportType))
                if (string.Compare(reportType, Constant.QC_AUDIT_CASE_REPORT_TYPE) == 0)
                    LoadAuditCaseReport();
                else
                    LoadMonthlyReport(reportType);
        }
        private void LoadAuditCaseReport()
        {
            int caseId = Convert.ToInt32(Request.QueryString["CaseID"].ToString()); 
            ReportViewerPrintSummary.ServerReport.ReportPath =
                HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_QC_AUDIT_CASE_REPORT);
            ReportParameter reportParameter = new ReportParameter("pi_fc_id", caseId.ToString());
            ReportViewerPrintSummary.ServerReport.SetParameters(new ReportParameter[] { reportParameter });
        }
        private void LoadMonthlyReport(string reportType)
        {
            string evalType = Request.QueryString["EvalType"].ToString();
            string agencyId = Request.QueryString["AgencyId"].ToString();
            string yearMonthFrom = Request.QueryString["From"].ToString();
            string yearMonthTo = Request.QueryString["To"].ToString();
            switch (reportType)
            {
                case Constant.QC_MONTHLY_SUMMARY_REPORT_TYPE:
                    ReportViewerPrintSummary.ServerReport.ReportPath =
                    HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_QC_MONTHLY_SUMMARY_REPORT);
                    break;
                case Constant.QC_MONTHLY_CALIBRATION_SUMMARY_REPORT_TYPE:
                    ReportViewerPrintSummary.ServerReport.ReportPath =
                HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_QC_MONTHLY_CALIBRATION_SUMMARY_REPORT);
                    break;
            }
            ReportParameter pEvalType = new ReportParameter("pi_eval_type", evalType);
            ReportParameter pAgencyId = new ReportParameter("pi_agency_id", agencyId);
            ReportParameter pYearMonthFrom = new ReportParameter("pi_from_eval_year_month", yearMonthFrom);
            ReportParameter pYearMonthTo = new ReportParameter("pi_to_eval_year_month", yearMonthTo);
            ReportViewerPrintSummary.ServerReport.SetParameters(new ReportParameter[] { pEvalType,pAgencyId, pYearMonthFrom, pYearMonthTo });
        }
        private void SetReportServerUrl()
        {
            ReportViewerPrintSummary.ServerReport.ReportServerUrl = new Uri(HPFConfigurationSettings.REPORTSERVER_URL);
        }

        
        #region "Disable Unwanted Export Formats"
        private void DisableUnwantedExportFormats()
        {
            if (!String.IsNullOrEmpty(HPFConfigurationSettings.HPF_EXPORT_FORMATS))
            {
                this.ReportViewerPrintSummary.ShowExportControls = true;

                DropDownList exportFormatCtl = FindExportFormatControl(ReportViewerPrintSummary.Controls);

                if (exportFormatCtl != null)
                {
                    exportFormatCtl.PreRender += delegate(object sender, EventArgs e)
                    {
                        string[] exportFormats = HPFConfigurationSettings.HPF_EXPORT_FORMATS.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        int index = 1;
                        ListItem item;
                        while (index < exportFormatCtl.Items.Count)
                        {
                            item = exportFormatCtl.Items[index];
                            if (!Array.Exists<string>(exportFormats,
                                delegate(string match) { return match.Trim().Equals(item.Value, StringComparison.OrdinalIgnoreCase); }))
                            {
                                exportFormatCtl.Items.Remove(item);
                            }
                            else
                            {
                                index++;
                            }
                        }
                    };
                }
            }
        }

        private DropDownList FindExportFormatControl(ControlCollection controls)
        {
            if (controls == null) return null;
            foreach (Control ctl in controls)
            {
                if ((ctl.GetType() == typeof(DropDownList)) &&
                    (((DropDownList)ctl).ToolTip == "Export Formats"))
                {
                    return ctl as DropDownList;
                }
                else
                {
                    Control child = FindExportFormatControl(ctl.Controls);
                    if (child != null)
                    {
                        return child as DropDownList;
                    }
                }
            }
            return null;
        }
        #endregion
    }
}