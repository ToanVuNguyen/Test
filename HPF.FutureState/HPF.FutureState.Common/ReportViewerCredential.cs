using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Net;

namespace HPF.FutureState.Common
{
    [Serializable]
   public class ReportViewerCredential : IReportServerCredentials
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
            this.Password = HPFConfigurationSettings.REPORTSERVER_PASSWORD;
            string username_domain = HPFConfigurationSettings.REPORTSERVER_LOGINNAME;
            var DomainUser = username_domain.Split('\\');
            if (username_domain.Contains(@"\"))
            {
                this.Domain = DomainUser[0];
                this.Username = DomainUser[1];//username_domain.Substring(username_domain.IndexOf(@"\") + 1, username_domain.Length - username_domain.IndexOf(@"\")-1);
            }
        }

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
    }
}
