using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTDAL.Entities
{
    public class AddressBook
    {
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }
        
        public int IdAddressBook { get; set; }
        public string EmailAddress { get; set; }

        [Name]
        public string AddressName { get; set; }
    }
}
