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

using System.Reflection;

namespace HPF.FutureState.Web.PrintSummary
{
    public partial class PrintSummaryUC : System.Web.UI.UserControl
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
            int caseid = Convert.ToInt32(Request.QueryString["CaseID"].ToString());

            ReportViewerCredential rvc = new ReportViewerCredential();
            ReportViewerPrintSummary.ServerReport.ReportServerCredentials = rvc;
            ReportViewerPrintSummary.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            //
            SetReportServerUrl();
            //
            SetReportPath();
            //
            ReportParameter reportParameter = new ReportParameter("pi_fc_id", caseid.ToString());
            ReportViewerPrintSummary.ServerReport.SetParameters(new ReportParameter[] {reportParameter});

        }

        private void SetReportServerUrl()
        {
            ReportViewerPrintSummary.ServerReport.ReportServerUrl = new Uri(HPFConfigurationSettings.REPORTSERVER_URL);
        }

        private void SetReportPath()
        {
            ReportViewerPrintSummary.ServerReport.ReportPath =
                HPFConfigurationSettings.MapReportPath(HPFConfigurationSettings.HPF_COUNSELINGSUMMARY_REPORT);
        }

        #region "Disable Unwanted Export Formats"
        private void DisableUnwantedExportFormats()
        {
            if (!String.IsNullOrEmpty(HPFConfigurationSettings.HPF_EXPORT_FORMATS))
            {
                DropDownList exportFormatCtl = FindExportFormatControl(ReportViewerPrintSummary.Controls);
                if (exportFormatCtl != null)
                {
                    exportFormatCtl.PreRender += delegate(object sender, EventArgs e)
                    {
                        string[] exportFormats = HPFConfigurationSettings.HPF_EXPORT_FORMATS.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        int index = 1;
                        ListItem item;
                        while(index < exportFormatCtl.Items.Count)
                        {
                            item = exportFormatCtl.Items[index];
                            if (!Array.Exists<string>(exportFormats,
                                delegate(string match) { return match.Equals(item.Value, StringComparison.OrdinalIgnoreCase); }))
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
            else
            {
                this.ReportViewerPrintSummary.ShowExportControls = false;
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