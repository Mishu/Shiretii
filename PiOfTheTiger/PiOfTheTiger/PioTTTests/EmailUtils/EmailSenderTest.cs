using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Mail;
using PiOTTDAL.Entities;
using EmailUtils;

namespace PioTTTests.EmailUtils
{
    [TestClass]
    public class EmailSenderTest
    {
        private MailMessage MailMessageMock()
        {
            MailAddress fromAddress = new MailAddress("PiOTT.Alerts@gmail.com", "PiOfTheTiger");

            MailMessage mailMessage = new MailMessage()
            {
                IsBodyHtml = true,
                Subject = "PiOTT - Test Send Mail",
                Body = "<p>Acesta este un test!</p>",
                Sender = fromAddress,
                From = fromAddress
            };

            mailMessage.To.Add("mb.ghinea@gmail.com");
            mailMessage.To.Add("shaikhan2702@yahoo.com");
            mailMessage.To.Add("fantasycl2003@yahoo.com");
            mailMessage.CC.Add("PiOTT.Alerts@gmail.com");

            return mailMessage;
        }

        private MailMessage MailMessageMockWithAttachment()
        {
            MailMessage mailMessage = MailMessageMock();

            Attachment attachment = new Attachment(@"D:\Dropbox\Dropbox\Hackathon\Wallpaper.png");
            mailMessage.Attachments.Add(attachment);

            return mailMessage;
        }

        private EmailSettings EmailSettingsMock()
        {
            EmailSettings emailSettings = new EmailSettings()
            {
                SMTPPassword = "1234%asd",
                SMTPPort = 587,
                SMTPServer = "smtp.gmail.com",
                SMTPSslRequired = true,
                SMTPUserName = "PiOTT.Alerts@gmail.com"
            };

            return emailSettings;
        }

        [TestMethod]
        public void SendTheMailTest()
        {
            EmailSettings emailSettings = EmailSettingsMock();
            EmailSender emailSender = new EmailSender(emailSettings);

            MailMessage mailMessage = MailMessageMock();
            emailSender.SendTheMail(mailMessage);
        }

        [TestMethod]
        public void SendTheMailTestWithAttachment()
        {
            EmailSettings emailSettings = EmailSettingsMock();
            EmailSender emailSender = new EmailSender(emailSettings);

            MailMessage mailMessage = MailMessageMockWithAttachment();
            emailSender.SendTheMail(mailMessage);
        }
    }
}
