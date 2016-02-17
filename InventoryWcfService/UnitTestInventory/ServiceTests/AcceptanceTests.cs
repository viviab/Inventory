
using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Data.Model;
using Inventory.Data.Repository;
using InventoryWcfService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestInventory.ServiceTests
{

    [TestClass]
    public class AcceptanceTests
    {

        [TestMethod]
        public void AddNewItemThenIsCreated()
        {
            //ARRANGE:
            var service = new Services();
            var label = "NewITemOne";
            var expires = DateTime.Today.AddDays(1);

            //ACT:
            var item = service.AddItem(label, expires);

            //ASSERT:
            Assert.AreEqual(item.Label, label);
            Assert.AreEqual(item.Expiration, expires);
        }


        [TestMethod]
        public void TakeOutAnItemThenIsOutRepo()
        {
            //ARRANGE:
            var service = new Services();
            var label = "NewItemOne";
            var expires = DateTime.Today.AddDays(1);
            var item = service.AddItem(label, expires);

            //ACT:
            service.TakeItem(item.Id);

            //ASSERT:
            Assert.AreEqual(service.GetListItems().Count, 0);


            var log = new LogRepository();
            var result = log.GetLog();
            Assert.IsTrue(result.Contains(string.Format("the item ('{0}','{1:d}') is taken out", label, expires)));

        }

        [TestMethod]
        public void GetListItemsThenIsShown()
        {
            //ARRANGE:
            var service = new Services();

            var label1 = "NewItemOne";
            var expires1 = DateTime.Today.AddDays(1);
            var label2 = "NewItemTwo";
            var expires2 = DateTime.Today.AddDays(1);

            var listarrange = new List<Item>();
            listarrange.Add(new Item() { Expiration = expires1, Label = label1 });
            listarrange.Add(new Item() { Expiration = expires2, Label = label2 });


           listarrange.ForEach(l => service.AddItem(l.Label, l.Expiration));

            //ASSERT:

            var listresult = service.GetListItems();
            Assert.AreEqual(listresult.Count, 2);

            listresult.ForEach(i =>
            {
                Assert.IsNotNull(listarrange.Any(a => a.Label == i.Label));
                Assert.IsNotNull(listarrange.Any(a => a.Expiration == i.Expiration));
            });

        }

        [TestMethod]
        public void LogItemOutOfDateThenIsInLog()
        {
            //ARRANGE:
            var service = new Services();

            var label1 = "NewItemOne";
            var expires1 = DateTime.Today.AddDays(1);
            var label2 = "NewItemTwo";
            var expires2 = DateTime.Today.AddDays(-1);

            var listarrange = new List<Item>();
            listarrange.Add(new Item() { Expiration = expires1, Label = label1 });
            listarrange.Add(new Item() { Expiration = expires2, Label = label2 });


            listarrange.ForEach(l => service.AddItem(l.Label, l.Expiration));

            //ASSERT:

            var listresult = service.GetListItems();
            Assert.AreEqual(listresult.Count, 2);

            var log = new LogRepository();
            var result = log.GetLog();
            Assert.IsTrue(result.Contains(string.Format("the item '{0}' is out of date: '{1:d}'", label2, expires2)));

        }

    }
}
