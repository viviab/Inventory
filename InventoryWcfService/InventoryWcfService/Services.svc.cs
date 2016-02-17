using System;
using System.Collections.Generic;
using Inventory.Data.Facade;
using Inventory.Data.Interfaces;
using Inventory.Data.Model;
using InventoryWcfService.Event;
using InventoryWcfService.Interfaces;

namespace InventoryWcfService
{
     public class Services : IServices
    {

         private readonly IServiceFacade _serviceFacade;

        public Services()
        {
            _serviceFacade = new ServiceFacade();
           
        }

        public Item AddItem(string label, DateTime expiration)
        {
            return _serviceFacade.AddItem(label, expiration);
        }

         public void TakeItem(int id)
         {
             var item = _serviceFacade.TakeItem(id);
             ItemTakeOutEvent(item);
         }

         public List<Item> GetListItems()
         {
             CheckItemsExpiredEvent();
             return _serviceFacade.GetListInventory();
         }

         public Item GetItem(string label)
         {
             return _serviceFacade.GetItem(label);
         }

         private void CheckItemsExpiredEvent()
         {
             var list = _serviceFacade.GetListItemsExpired();        
             list.ForEach(e=> 
              EventManager.AddNotification(string.Format("the item '{0}' is out of date: '{1:d}'", e.Label, e.Expiration)));

         }

        private void ItemTakeOutEvent(Item item)
        {
            EventManager.AddNotification(string.Format("the item ('{0}','{1:d}') is taken out ", item.Label, item.Expiration));

        }
    }
}
