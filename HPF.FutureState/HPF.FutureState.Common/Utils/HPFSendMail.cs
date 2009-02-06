using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

namespace HPF.FutureState.Common.Utils
{
    public class HPFSendMail
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        private bool isEncrypted = true;

        public bool IsEncrypted
        {
            get { return isEncrypted; }
            set { isEncrypted = value; }
        }

        private readonly Dictionary<string, byte[]> _attachments;

        private MailMessage _mailMessage;

        public HPFSendMail()
        {
            _attachments = new Dictionary<string, byte[]>();
        }

        public void Send()
        {
            _mailMessage = CreateMailMessage();
            AddAttachmentToMailMessage(_mailMessage);
            //
            var smtpClient = CreateSmtpClient();
            smtpClient.SendCompleted += smtpClient_SendCompleted;
            smtpClient.Send(_mailMessage);
        }

        private void smtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            _mailMessage.Dispose();
        }

        private void AddAttachmentToMailMessage(MailMessage mailMessage)
        {
            foreach (var attachment in _attachments)
            {
                var mem = new MemoryStream(attachment.Value, false);
                var att = new Attachment(mem, null, null) {Name = attachment.Key};
                mailMessage.Attachments.Add(att);
            }
        }

        private static SmtpClient CreateSmtpClient()
        {
            return new SmtpClient();
        }

        private MailMessage CreateMailMessage()
        {
            var mailMessage = new MailMessage();
            mailMessage.To.Add(To);
            if (!IsEncrypted)
                mailMessage.Subject = Subject;
            else
                mailMessage.Subject = Subject + "$S$";
            return mailMessage;
            //
        }

        public void AddAttachment(string filename, byte[] content)
        {
            _attachments.Add(filename, content);
        }

        public void RemoveAttachment(string filename)
        {
            _attachments.Remove(filename);
        }
    }
}
