using PiOTTDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailUtils
{
    public class EmailSender
    {
        private EmailSettings emailSettings;

        public EmailSender()
        {
            emailSettings = new EmailSettings();
        }

        public EmailSender(EmailSettings emailSettings)
        {
            this.emailSettings = emailSettings;
        }

        public void SendTheMail(MailMessage email)
        {
            SmtpClient smtpClient = InitializeSmtpClient();

            smtpClient.Send(email);
        }

        private SmtpClient InitializeSmtpClient()
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = emailSettings.SMTPServer;
            smtpClient.Port = emailSettings.SMTPPort;
            smtpClient.EnableSsl = emailSettings.SMTPSslRequired;
            smtpClient.Credentials = new NetworkCredential(emailSettings.SMTPUserName, emailSettings.SMTPPassword);
            return smtpClient;
        }
    }
}
