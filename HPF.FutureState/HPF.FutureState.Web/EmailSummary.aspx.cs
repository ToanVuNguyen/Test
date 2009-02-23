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
using HPF.FutureState.Common;
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.Utils;


namespace HPF.FutureState.Web
{
    public partial class EmailSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string SendEmailWithAttachment(string SendFrom, string SendTo, string Subject, string Body,string CaseID)
        {
            try
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                string from = SendFrom;
                string to = SendTo; //Danh sách email được ngăn cách nhau bởi dấu ";"
                string subject = Subject;
                string body = Body;

                bool result = true;
                String[] ALL_EMAILS = to.Split(';');

                foreach (string emailaddress in ALL_EMAILS)
                {
                    result = regex.IsMatch(emailaddress);
                    if (result == false)
                    {
                        return "Email address is not wellform";
                    }
                }

                if (result == true)
                {
                    HPFSendMail hpfSendMail = new HPFSendMail();
                    ReportingExporter reportExport = new ReportingExporter();
                    hpfSendMail.To = SendTo;
                    hpfSendMail.Subject = Subject;
                    hpfSendMail.Body = Body;
                    reportExport.ReportPath = @"HPF_Report/rpt_CounselingSummary";
                    reportExport.SetReportParameter("pi_fc_id", CaseID);
                    byte[] attachContent = reportExport.ExportToPdf();
                    hpfSendMail.AddAttachment("hpf_report", attachContent);
                    hpfSendMail.Send();
                }
                
            }
            catch (Exception ex)
            {

                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                return ex.Message;
            }
            return "succesfull";
        }
    }
}
