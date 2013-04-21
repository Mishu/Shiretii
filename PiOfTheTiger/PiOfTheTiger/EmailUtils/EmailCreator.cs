using PiOTTDAL.Entities;
using PiOTTDAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailUtils
{
    public class EmailCreator
    {
        /// <summary>
        /// Sends email with picture attachment
        /// </summary>
        /// <param name="imagePath">Path to the image on the disk</param>
        public void SendMailForDifferentImages(string imagePath)
        {
            EmailSettings emailSettings = new EmailSettingsQuery().GetEmailSettings();

            MailAddress mailFrom = new MailAddress(emailSettings.SMTPUserName);

            MailMessage mailMessage = new MailMessage()
            {
                IsBodyHtml = true,
                Subject = "Motion CAPTURED",
                Body = "<h>Please check the attachments to see what happened!!!</h>",
                Sender = mailFrom,
                From = mailFrom
            };

            List<AddressBook> addresses = new AddressBookQuery().GetAllAddressBook();

            foreach (AddressBook address in addresses)
            {
                mailMessage.To.Add(address.EmailAddress);
            }

            mailMessage.CC.Add(mailFrom);

            mailMessage.Attachments.Add(new Attachment(imagePath));

            new EmailSender(emailSettings).SendTheMail(mailMessage);
        }
    }
}
