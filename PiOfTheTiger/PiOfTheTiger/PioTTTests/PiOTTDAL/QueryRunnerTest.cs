using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PiOTTDAL.Queryes;
using PiOTTDAL.Entities;
using System.Collections.Generic;

namespace PioTTTests.PiOTTDAL
{
    [TestClass]
    public class QueryRunnerTest
    {
        [TestMethod]
        public void TestExecuteScript()
        {
            string query = "select * from AddressBook";

            QueryRunner runner = new QueryRunner();
            List<AddressBook> addresses = runner.Execute<AddressBook>(query);

            Assert.AreEqual(3, addresses.Count);
        }

        [TestMethod]
        public void TestGetAllScript()
        {
            QueryRunner runner = new QueryRunner();
            List<AddressBook> addresses = runner.GetAll<AddressBook>();

            Assert.AreEqual(3, addresses.Count);
        }

        [TestMethod]
        public void TestGetByAttribute()
        {
            QueryRunner runner = new QueryRunner();
            List<AddressBook> addresses = runner.GetByAttribute<AddressBook>("", typeof(Name));

            Assert.AreEqual(3, addresses.Count);
        }
    }
}
