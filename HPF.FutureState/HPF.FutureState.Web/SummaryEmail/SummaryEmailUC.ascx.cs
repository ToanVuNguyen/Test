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


namespace HPF.FutureState.Web.SummaryEmail
{
    public partial class SummaryEmailUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtTo.Focus();
            ForeclosureCaseDTO forclosureInfo = (ForeclosureCaseDTO)Session["foreclosureInfo"];
            txtSubject.Text = GenerateSubject(forclosureInfo);
        }
        public string SendEmailWithAttachment(string SendFrom, string SendTo, string Subject, string Body)
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
                    try
                    {
                        MailMessage em = new MailMessage(from, to, subject, body);
                        Attachment attach = new Attachment(@"D:\test.pdf");
                        em.Attachments.Add(attach);
                        em.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = ConfigurationManager.AppSettings["smtpServer"];
                        smtp.UseDefaultCredentials = true;
                        smtp.Port = 25;
                        smtp.Send(em);

                        return "Email was sent: " + to + ".";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                        ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            //string SendFrom = "HPF.DoNotReply@HopeNetAdmin.org";
            string SendFrom = "tnguyen243@csc.com";
            string SendTo = txtTo.Text;
            string Subject = txtSubject.Text + "$S$";
            string Body = txtBody.Text;
            lblMessgage.Text = SendEmailWithAttachment(SendFrom, SendTo, Subject, Body);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="foreclosurecaseInfo"></param>
        /// <returns></returns>
        protected string GenerateSubject(ForeclosureCaseDTO foreclosurecaseInfo)
        {
            string strSubject = "HPF Summary loan#";
            //get info from case loan to get: Loan Status and Loan Num
            CaseLoanDTOCollection caseLoanDTOCol = CaseLoanBL.Instance.RetrieveCaseLoan(foreclosurecaseInfo.FcId);
            //
            strSubject += caseLoanDTOCol[0].AcctNum + "/" + foreclosurecaseInfo.PropZip;

            string loanDelinqStatus = caseLoanDTOCol[0].LoanDelinqStatusCd;
            if (foreclosurecaseInfo.ForSaleInd == "Y" || loanDelinqStatus == "120+")
                strSubject += " ,priority URGENT";
            return strSubject;

        }
    }
}