using PiOTTDAL.Constants;
using PiOTTDAL.Entities;
using PiOTTDAL.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTDAL.Queries
{
    public class EmailSettingsQuery : QueryRunner
    {
        public EmailSettings GetEmailSettings()
        {
            Type nameAttribute = typeof(Name);

            EmailSettings emailSettings = new EmailSettings();
            emailSettings.SMTPPassword = GetByAttribute<AppSettings>(QueryConstants.AppSettingsKey_SMTPPassword, nameAttribute).Value;
            emailSettings.SMTPPort = Int32.Parse(GetByAttribute<AppSettings>(QueryConstants.AppSettingsKey_SMTPPort, nameAttribute).Value);
            emailSettings.SMTPServer = GetByAttribute<AppSettings>(QueryConstants.AppSettingsKey_SMTPServer, nameAttribute).Value;
            emailSettings.SMTPSslRequired = Boolean.Parse(GetByAttribute<AppSettings>(QueryConstants.AppSettingsKey_SMTPUseSSL, nameAttribute).Value);
            emailSettings.SMTPUserName = GetByAttribute<AppSettings>(QueryConstants.AppSettingsKey_SMTPUserName, nameAttribute).Value;

            return emailSettings;
        }
    }
}
