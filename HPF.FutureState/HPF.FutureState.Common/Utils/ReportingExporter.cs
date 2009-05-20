using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Net;
using Microsoft.Practices.EnterpriseLibrary.Logging;

using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.Common.Utils
{
    /// <summary>
    /// Export a report from Reporting service server.
    /// </summary>
    public class ReportingExporter
    {        
        /// <summary>
        /// Report path
        /// </summary>
        public string ReportPath { get; set; }

        /// <summary>
        /// Reporting server address
        /// </summary>
        private readonly string _ReportServer;

        private readonly Dictionary<string, string> _Parameter;

        public ReportingExporter()
        {
            _Parameter = new Dictionary<string, string>();
            _ReportServer = HPFConfigurationSettings.REPORTSERVER_URL;
        }

        public byte[] ExportTo(ReportFormat format)
        {
            Validate();
            return Export(format.ToString());
        } 

        /*
        /// <summary>
        /// Export report to Pdf format.
        /// </summary>
        /// <returns></returns>
        public byte[] ExportToPdf()
        {
            Validate();
            var format = "PDF";
            return Export(format);
        }        

        /// <summary>
        /// Export report to Excel format
        /// </summary>
        /// <returns></returns>
        public byte[] ExportToExcel()
        {
            Validate();
            var format = "EXCEL";
            return Export(format);
        }

        /// <summary>
        /// Export report to Excel format
        /// </summary>
        /// <returns></returns>
        public byte[] ExportToCSV()
        {
            Validate();
            var format = "CSV";
            return Export(format);
        }
        */

        private void Validate()
        {
            if (string.IsNullOrEmpty(ReportPath))
            {
                throw new Exception("ReportPath can not be empty.");
            }
        }

        private byte[] Export(string format)
        {
            var webClient = GetWebClient();
            var reportUrl = GetReportUrl(format);
            try
            {                
                var buffer = webClient.DownloadData(reportUrl);
                webClient.Dispose();
                return buffer;
            }
            catch(Exception ex)
            {                
                throw ExceptionProcessor.GetHpfException(ex, "", "ReportingExporter.Export");
            }
        }

        private static WebClient GetWebClient()
        {
            var configLoginName = HPFConfigurationSettings.REPORTSERVER_LOGINNAME;
            var loginNameDomain = configLoginName.Split('\\');
            var credentials = new NetworkCredential();
            if (loginNameDomain.Length > 1)
            {
                credentials.UserName = loginNameDomain[1];
                credentials.Domain = loginNameDomain[0];
            }
            else
                credentials.UserName = configLoginName;
            //
            credentials.Password = HPFConfigurationSettings.REPORTSERVER_PASSWORD;

            return new WebClient
                       {
                           Credentials = credentials
                       };
        }

        public void SetReportParameter(string name, string value)
        {
            if (!_Parameter.ContainsKey(name))
                _Parameter.Add(name, value);
            else
            {
                _Parameter[name] = value;
            }
        }

        private string GetReportUrl(string renderFormat)
        {                        
            var url = _ReportServer + "/?" + ReportPath + "&rs:Command=Render&rs:Format={0}{1}";
            //
            var paramString = GetParamString();
            return string.Format(url, renderFormat, paramString);
        }

        private string GetParamString()
        {
            var paramString = new StringBuilder();
            foreach (var param in _Parameter)
            {
                paramString.Append("&" + param.Key + "=" + param.Value);
            }
            return paramString.ToString();
        }
    }
}
