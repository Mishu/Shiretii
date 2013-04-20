using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTDAL.Entities
{
    public class AddressBook
    {
        public int IdAddressBook { get; set; }
        public string EmailAddress { get; set; }

        [Name]
        public string AddressName { get; set; }
    }
}
