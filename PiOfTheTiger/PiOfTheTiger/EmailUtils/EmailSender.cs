using PiOTTDAL.Entities;
using PiOTTDAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
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

        /// <summary>
        /// Sends email using a SMTPClient
        /// </summary>
        /// <param name="email"></param>
        public void SendTheMail(MailMessage email)
        {
            SmtpClient smtpClient = InitializeSmtpClient();

            ServicePointManager.ServerCertificateValidationCallback =
            delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };

            smtpClient.Send(email);

            new SentEmailQuery().InsertSentEmail(email);
        }

        private SmtpClient InitializeSmtpClient()
        {
            SmtpClient smtpClient = new SmtpClient(emailSettings.SMTPServer);

            smtpClient.Port = emailSettings.SMTPPort;
            smtpClient.EnableSsl = emailSettings.SMTPSslRequired;

            smtpClient.Credentials = new NetworkCredential(emailSettings.SMTPUserName, emailSettings.SMTPPassword);
            return smtpClient;
        }
    }
}
