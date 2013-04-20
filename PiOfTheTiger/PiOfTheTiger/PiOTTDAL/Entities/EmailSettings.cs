using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTDAL.Entities
{
    [Serializable]
    public class EmailSettings
    {
        public int IdEmailSettings { get; set; }
        public String EmailSettingsName { get; set; }
        public String SMTPServer { get; set; }
        public String SMTPUserName { get; set; }
        public String SMTPPassword { get; set; }
        public String SMTPSslRequired { get; set; }
    }
}
