

using System;
using Inventory.Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestInventory.RepositoryTests
{
    [TestClass]
    public class LogRepositoryTests
    {

        [TestMethod]
        public void AddLogItem()
        {
            //ARRANGE:
            var message = string.Format("LOG MESSAGE TEST: {0:G}", DateTime.Now);
            var log = new LogRepository();

            //ACT:
            log.Add(message);
            var result = log.GetLog();

            //ASSERT:
            Assert.IsTrue(result.Contains(message));

        }

    }
}
