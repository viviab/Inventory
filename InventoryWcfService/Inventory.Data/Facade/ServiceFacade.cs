

using System;
using System.Collections.Generic;
using Inventory.Data.Interfaces;
using Inventory.Data.Model;
using Inventory.Data.Repository;

namespace Inventory.Data.Facade
{
    public class ServiceFacade : IServiceFacade
    {
        private readonly IInventoryRepository _repository;
        public ServiceFacade()
        {
         _repository = new InventoryRepository();

        }

        public Item AddItem(string label, DateTime expiration)
        {
            if (string.IsNullOrEmpty(label))
                throw new ArgumentException("label is null or empty");

            if (_repository.GetItem(label)!=null)
                throw new ArgumentException("there is an item with the same label");

            return _repository.Add(label, expiration);

        }

        public Item TakeItem(int id)
        {
            var item = GetItem(id);
            if (item == null)
                throw new ArgumentException("the item does not exist");

            _repository.RemoveItem(item.Id);
            return item;
        }

        public List<Item> GetListInventory()
        {
            return _repository.GetList();
        }

        public List<Item> GetListItemsExpired()
        {
            return _repository.GetListExpiredItems();
        }

        public Item GetItem(int id)
        {
            if (id<0)
                throw new ArgumentException("id item is invalid");

            return _repository.Get(id);
        }

        public Item GetItem(string label)
        {
            if (string.IsNullOrEmpty(label))
                throw new ArgumentException("label is null or empty");

            return _repository.GetItem(label);
        }
    }
}
