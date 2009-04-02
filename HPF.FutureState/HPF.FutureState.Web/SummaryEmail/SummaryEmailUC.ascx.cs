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
using HPF.FutureState.Common;


namespace HPF.FutureState.Web.SummaryEmail
{
    public partial class SummaryEmailUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtTo.Focus();
            ForeclosureCaseDTO forclosureInfo = (ForeclosureCaseDTO)Session["foreclosureInfo"];
            if (!IsPostBack)
            {
                if (forclosureInfo != null)
                {
                    int? fc_id = forclosureInfo.FcId;
                    txtSubject.Text = EmailSummaryBL.Instance.CreateEmailSummarySubject(fc_id);
                }
            }
        }
        public bool CheckEmailAddress(string emailAddress)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            bool result = true;
            String[] ALL_EMAILS = emailAddress.Split(',');

            foreach (string emailaddress in ALL_EMAILS)
            {
                result = (regex.IsMatch(emailaddress));
                if (!result || String.IsNullOrEmpty(emailAddress))
                {
                    return result;
                }
            }
            return true;
        }
        public string SendEmailWithAttachment(string sendTo, string subject, string body, int caseID)
        {
            try
            {
                EmailSummaryBL.Instance.SendEmailSummaryReport(sendTo, subject, body, caseID);
                return "successful";
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                throw ex;
            }

        }

        private ExceptionMessage GetExceptionMessage(string exCode)
        {
            ExceptionMessage exMess = new ExceptionMessage();
            exMess.ErrorCode = exCode;
            exMess.Message = ErrorMessages.GetExceptionMessageCombined(exCode);
            return exMess;
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            bulMessage.Items.Clear();
            DataValidationException dataValidException = new DataValidationException();

            try
            {
                BuildSendMailInfo();
                //
                InsertActivityLogInfo();
                //
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script language='javascript'>window.close();</script>");
            }
            catch (DataValidationException ex)
            {
                foreach (var mes in ex.ExceptionMessages)
                    bulMessage.Items.Add(new ListItem(mes.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                return;
            }
            catch (Exception ex)
            {
                bulMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                return;
            }
        }

        private void InsertActivityLogInfo()
        {
            ActivityLogDTO activityLog = BuildActivityLogInfo();
            activityLog.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
            ActivityLogBL.Instance.InsertActivityLog(activityLog);
        }

        private void BuildSendMailInfo()
        {
            bulMessage.Items.Clear();
            DataValidationException ex = new DataValidationException();
            string CaseID = Request.QueryString["CaseID"];
            string SendTo = txtTo.Text;
            if (SendTo.Length > 255)
            {
                ExceptionMessage exMessage = GetExceptionMessage(ErrorMessages.ERR0852);//error code
                ex.ExceptionMessages.Add(exMessage);
            }
            if (!CheckEmailAddress(SendTo))
            {
                ExceptionMessage exMessage = GetExceptionMessage(ErrorMessages.ERR0850);//error code
                ex.ExceptionMessages.Add(exMessage);
            }
            string Subject = txtSubject.Text;
            if (Subject.Length > 255)
            {
                ExceptionMessage exMessage = GetExceptionMessage(ErrorMessages.ERR0853);//error code
                ex.ExceptionMessages.Add(exMessage);
            }
            if (String.IsNullOrEmpty(Subject))
            {
                ExceptionMessage exMessage = GetExceptionMessage(ErrorMessages.ERR0851);//error code
                ex.ExceptionMessages.Add(exMessage);
            }
            string Body = txtBody.Text;
            if (Body.Length > 2000)
            {
                ExceptionMessage exMessage = GetExceptionMessage(ErrorMessages.ERR0854);//error code
                ex.ExceptionMessages.Add(exMessage);
            }
            if (ex.ExceptionMessages.Count > 0)
                throw ex;
            bulMessage.Items.Add(new ListItem(SendEmailWithAttachment(SendTo, Subject, Body, Convert.ToInt32(CaseID))));
        }
        protected ActivityLogDTO BuildActivityLogInfo()
        {
            ActivityLogDTO activityLog = new ActivityLogDTO();
            ForeclosureCaseDTO forclosureInfo = (ForeclosureCaseDTO)Session["foreclosureInfo"];
            activityLog.FcId = forclosureInfo.FcId;
            activityLog.ActivityCd = "EMAIL";
            activityLog.ActivityDt = DateTime.Now;
            activityLog.ActivityNote = string.Concat(" To: ", txtTo.Text," From:",HPFWebSecurity.CurrentIdentity.LoginName, " Subject: ", txtSubject.Text, " Body: ", txtBody.Text);
            return activityLog;
        }
    }
}