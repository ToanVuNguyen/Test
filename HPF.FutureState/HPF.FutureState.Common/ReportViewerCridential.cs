using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace HPF.FutureState.Common
{
    class ReportViewerCridential
    {
        public string Username
        { get; set; }
        public string Password
        { get; set; }
        public string Domain
        { get; set; }

        public ReportViewerCridential()
        {

        }
        public ReportViewerCridential(string username, string password, string domain)
        {
            this.Username = username;
            this.Password = password;
            this.Domain = domain;

        }
    }
}
