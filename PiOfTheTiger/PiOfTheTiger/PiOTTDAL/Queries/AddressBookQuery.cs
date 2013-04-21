using PiOTTDAL.Entities;
using PiOTTDAL.Entities.Attributes;
using PiOTTDAL.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTDAL.Queries
{
    public class AddressBookQuery : QueryRunner
    {
        public List<AddressBook> GetAllAddressBook()
        {
            return GetAll<AddressBook>();
        }

        public AddressBook GetAddressBookByName(string name)
        {
            return GetByAttribute<AddressBook>(name, typeof(Name));
        }
    }
}
