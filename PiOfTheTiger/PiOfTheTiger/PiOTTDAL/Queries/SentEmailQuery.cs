using PiOTTDAL.Entities;
using PiOTTDAL.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTDAL.Queries
{
    public class SentEmailQuery : QueryRunner
    {
        public List<SentEmail> GetAllSentEmail()
        {
            return GetAll<SentEmail>();
        }

        public void InsertSentEmail(MailMessage mail)
        {
            SentEmail sentMail = new SentEmail()
            {
                Body = mail.Body,
                Cc = mail.CC.ToString(),
                SentDate = DateTime.Now,
                Subject = mail.Subject,
                To = mail.To.ToString()
            };

            InsertEntity<SentEmail>(sentMail);
        }
    }
}
