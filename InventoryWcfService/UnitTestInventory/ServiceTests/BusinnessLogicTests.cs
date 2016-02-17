using System;
using InventoryWcfService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestInventory.ServiceTests
{
    [TestClass]
    public class BusinnessLogicTests
    {
        [TestMethod]
        public void SameItemLabelThrowsException()
        {

            //ARRANGE:
            var service = new Services();
            const string label = "NewOne";
            var expires = DateTime.Today.AddDays(1);
            var item = service.AddItem(label, expires);

            //ACT:
            //ACT & ASSERT:
            try
            {
                var item2 = service.AddItem(label, expires);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message, "there is an item with the same label");
            }
            catch (Exception)
            {
                Assert.Fail();
            }


        }

        [TestMethod]
        public void InvalidLabelAddItemThrowsException()
        {
            //ARRANGE:
            var service = new Services();
            const string label = "";
            var expires = DateTime.Today.AddDays(1);

            //ACT & ASSERT:
            try
            {
                var item = service.AddItem(label, expires);

                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message, "label is null or empty");
            }
            catch (Exception)
            {
                Assert.Fail();
            }           
        }

        [TestMethod]
        public void InvalidLabelGetItemThrowsException()
        {
            //ARRANGE:
            var service = new Services();
            const string label = "";

            //ACT & ASSERT:
            try
            {
                var item = service.GetItem(label);

                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message, "label is null or empty");
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void TakeOutInvalidItemThrowsException()
        {
            //ARRANGE:
            var service = new Services();
            const int id = 999999999;

            //ACT & ASSERT:
            try
            {
                service.TakeItem(id);

                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message, "the item does not exist");
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TakeOutInvalidIdThrowsException()
        {
            //ARRANGE:
            var service = new Services();
            const int id = -99;

            //ACT & ASSERT:
            try
            {
                service.TakeItem(id);

                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message, "id item is invalid");
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }


    }
}
