using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PiOTTDAL.Queries.Base;
using PiOTTDAL.Entities;
using System.Collections.Generic;
using PiOTTDAL.Queries;

namespace PioTTTests.PiOTTDAL
{
    [TestClass]
    public class AddressBookQueryTest
    {
        [TestMethod]
        public void GetAllAddressBookTest()
        {
            AddressBookQuery addQuery = new AddressBookQuery();
            List<AddressBook> addresses = addQuery.GetAllAddressBook();

            Assert.AreEqual(3, addresses.Count);
        }

        [TestMethod]
        public void GetAddressBookByNameTest()
        {
            string name = "Robert";
            AddressBookQuery addQuery = new AddressBookQuery();
            AddressBook address = addQuery.GetAddressBookByName(name);

            Assert.AreEqual(name, address.AddressName);
        }
    }
}
