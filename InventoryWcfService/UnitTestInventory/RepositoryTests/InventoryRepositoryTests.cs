using System;
using Inventory.Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestInventory.RepositoryTests
{
    [TestClass]
    public class InventoryRepositoryTests
    {

        private InventoryRepository _repositoryTest;

        [TestMethod]
        public void AddNewItem()
        {
            //ARRANGE:
            _repositoryTest = new InventoryRepository();
            const string label = "NewOne";
            var expires = DateTime.Today.AddDays(1);

            //ACT:
            var newitem =_repositoryTest.Add(label, expires);
            var item = _repositoryTest.Get(newitem.Id);

            //ASSERT:
            Assert.AreEqual(item.Label, label);
            Assert.AreEqual(item.Expiration, expires);
            Assert.IsTrue(item.Id > 0);
            Assert.AreEqual(_repositoryTest.GetItem(label).Id, 1);

            Assert.AreEqual(_repositoryTest.GetList().Count, 1);

        }

        [TestMethod]
        public void Add2ItemsReturns2Items()
        {
            //ITEM: 1
            //ARRANGE:
            _repositoryTest = new InventoryRepository();
            const string label = "NewItemOne";
            var expires = DateTime.Today.AddDays(1);

            //ACT: 
            var newitem1 = _repositoryTest.Add(label, expires);
            var item = _repositoryTest.Get(newitem1.Id);

            //ASSERT:
            Assert.AreEqual(item.Label, label);
            Assert.AreEqual(item.Expiration, expires);
            Assert.AreEqual(_repositoryTest.GetItem(label).Id, 1);
            Assert.AreEqual(_repositoryTest.GetList().Count, 1);

            //ITEM: 2
            //ACT:
            const string label2 = "NewItemTwo";
            var expires2 = DateTime.Today.AddDays(2);
            var item2 = _repositoryTest.Add(label2, expires2);

            //ASSERT:
            Assert.AreEqual(item2.Label, label2);
            Assert.AreEqual(item2.Expiration, expires2);
            Assert.AreEqual(_repositoryTest.GetItem(label2).Id, 2);
            Assert.AreEqual(_repositoryTest.GetList().Count, 2);

        }

        [TestMethod]
        public void NoItemReturnsZero()
        {
            //ARRANGE:
            _repositoryTest = new InventoryRepository();
            const string label = "NOEXISTS";

            //ASSERT:
            Assert.IsNull(_repositoryTest.GetItem(label));
            Assert.AreEqual(_repositoryTest.GetList().Count, 0);
        }

        [TestMethod]
        public void RemoveItemThenListIsEmpty()
        {

            //ARRANGE:
            _repositoryTest = new InventoryRepository();

            const string label = "NewItem";
            var expires = DateTime.Today.AddDays(1);


            var newitem1 = _repositoryTest.Add(label,expires);
            var item = _repositoryTest.Get(newitem1.Id);
            Assert.AreEqual(item.Label, label);
            Assert.AreEqual(item.Expiration, expires);
            Assert.AreEqual(_repositoryTest.GetItem(label).Id, 1);

            //ACT:
            _repositoryTest.RemoveItem(item.Id);

            //ASSERT:
            Assert.IsNull(_repositoryTest.GetItem(label));
            Assert.AreEqual(_repositoryTest.GetList().Count, 0);

        }


        [TestMethod]
        public void GetListEmptyReturnsEmpty()
        {

            //ARRANGE:
            _repositoryTest = new InventoryRepository();
      
            //ACT:
            var list = _repositoryTest.GetList();

            //ASSERT:
            Assert.AreEqual(list.Count, 0);

        }

        [TestMethod]
        public void ListExpiredItemsEmptyReturnsEmpty()
        {
            //ARRANGE:
            _repositoryTest = new InventoryRepository();

            //ACT: 
            var list =_repositoryTest.GetListExpiredItems();

            //ASSERT:
            Assert.AreEqual(list.Count, 0);
        }

        [TestMethod]
        public void ListExpiredItemsReturnAItem()
        {
            //ARRANGE:
            _repositoryTest = new InventoryRepository();
            const string label1 = "NewOne";
            var expires1 = DateTime.Today.AddDays(1);

            const string label2 = "NewTwo";
            var expires2 = DateTime.Today.AddDays(-1);

            _repositoryTest.Add(label1, expires1);
            _repositoryTest.Add(label2, expires2);

            //ACT: 
            var list = _repositoryTest.GetListExpiredItems();

            //ASSERT:
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Label, label2);
            Assert.AreEqual(list[0].Expiration, expires2);
        }


    }
}
