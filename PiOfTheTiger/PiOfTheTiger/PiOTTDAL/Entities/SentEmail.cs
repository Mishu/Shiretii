using PiOTTDAL.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTDAL.Entities
{
    public class SentEmail
    {
        [ID]
        public int IdSentEmail { get; set; }
        public DateTime SentDate { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
