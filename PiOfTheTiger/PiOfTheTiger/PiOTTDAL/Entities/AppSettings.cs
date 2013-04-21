using PiOTTDAL.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTDAL.Entities
{
    public class AppSettings
    {
        [Name]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
