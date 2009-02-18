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
using System.Net.Mail;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.BusinessLogic;
using System.Globalization;
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.Utils;


namespace HPF.FutureState.Web.SummaryEmail
{
    public partial class SummaryEmailUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtTo.Focus();
            ForeclosureCaseDTO forclosureInfo = (ForeclosureCaseDTO)Session["foreclosureInfo"];
            int? fc_id = forclosureInfo.FcId;
            txtSubject.Text =EmailSummaryBL.Instance.CreateEmailSummarySubject(fc_id);
        }
        public string SendEmailWithAttachment(string sendTo, string subject, string body, int caseID)
        {
            try
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                bool result = true;
                String[] ALL_EMAILS = sendTo.Split(';');

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
                    EmailSummaryBL.Instance.SendEmailSummaryReport(sendTo, subject, body, caseID);
                }

            }
            catch (Exception ex)
            {

                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                return ex.Message;
            }
            return "successful";
        }
  

        protected void btnSend_Click(object sender, EventArgs e)
        {            
            string CaseID = Request.QueryString["CaseID"];
            string SendTo = txtTo.Text;
            string Subject = txtSubject.Text;
            string Body = txtBody.Text;
            lblMessgage.Text = SendEmailWithAttachment(SendTo, Subject, Body,Convert.ToInt32(CaseID));
            ActivityLogDTO activityLog = GetActivityLogInfo();
            activityLog.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
            ActivityLogBL.Instance.InsertActivityLog(activityLog);

        }
        protected ActivityLogDTO GetActivityLogInfo()
        {
            ActivityLogDTO activityLog = new ActivityLogDTO();
            ForeclosureCaseDTO forclosureInfo = (ForeclosureCaseDTO)Session["foreclosureInfo"];
            activityLog.FcId = forclosureInfo.FcId;
            activityLog.ActivityCd = "EMAIL";
            activityLog.ActivityDt = DateTime.Now;
            activityLog.ActivityNote = string.Concat(" Subject: ", txtSubject.Text, " To: ", txtTo.Text, " Body: ", txtBody.Text);
            return activityLog;
        }        
    }
}