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

namespace HPF.FutureState.Web.PrintSummary
{
    public partial class PrintSummaryUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadReport();
        }
        protected void LoadReport()
        {
            int caseid = Convert.ToInt32(Request.QueryString["CaseID"].ToString());
            
            ReportViewerCredential rvc = new ReportViewerCredential();
            ReportViewerPrintSummary.ServerReport.ReportServerCredentials = rvc;
            ReportViewerPrintSummary.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;

            ReportViewerPrintSummary.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["REPORTSERVER_URL"].ToString());
            ReportViewerPrintSummary.ServerReport.ReportPath = @"/HPF_Report/rpt_CounselingSummary";

            ReportParameter reportParameter = new ReportParameter("pi_fc_id", caseid.ToString());
            ReportViewerPrintSummary.ServerReport.SetParameters(new ReportParameter[] { reportParameter });
            


        }
    }
    [Serializable]
    class ReportViewerCredential : IReportServerCredentials
    {
        public string Username
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;

        }
        public string Domain
        {
            get;
            set;
        }

        
        public ReportViewerCredential()
        {
            this.Password = ConfigurationManager.AppSettings["REPORTSERVER_PASSWORD"].ToString();
            string username_domain = ConfigurationManager.AppSettings["REPORTSERVER_LOGINNAME"].ToString();
            if (username_domain.Contains(@"/"))
            {
                this.Domain = username_domain.Substring(0, username_domain.IndexOf(@"/"));
                this.Username = username_domain.Substring(username_domain.IndexOf(@"/") + 1, username_domain.Length - username_domain.IndexOf(@"/")-1);
            }
        }

        #region IReportServerCredentials Members

        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = this.Username;
            password = this.Password;
            authority = this.Domain;
            return false;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get { return (new NetworkCredential(this.Username, this.Password, this.Domain)); }
        }

        #endregion
    }



}