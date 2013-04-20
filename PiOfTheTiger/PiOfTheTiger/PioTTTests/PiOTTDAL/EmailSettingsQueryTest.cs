using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PiOTTDAL.Queries.Base;
using PiOTTDAL.Entities;
using System.Collections.Generic;
using PiOTTDAL.Queries;

namespace PioTTTests.PiOTTDAL
{
    [TestClass]
    public class EmailSettingsQueryTest
    {
        [TestMethod]
        public void GetEmailSettingsTest()
        {
            EmailSettingsQuery mailQuery = new EmailSettingsQuery();
            EmailSettings emailSettings = mailQuery.GetEmailSettings();

            Assert.AreEqual("PiOTT.Alerts@gmail.com", emailSettings.SMTPUserName);
        }
    }
}
