using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Data.Interfaces;
using Inventory.Data.Model;

namespace Inventory.Data.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private Dictionary<int, Item> _repository;
        private int _index;

        public InventoryRepository()
        {
            _repository = new Dictionary<int, Item>();
            _index = 0;
        }

        public Item Add(string label, DateTime expiration)
        {

            var item = new Item() { Label = label, Expiration = expiration };
            return Add(item);
        }

        public Item Add(Item item)
        {
            item.Id = ++ _index;
            _repository.Add(_index, item);
            return item;
        }

        public Item GetItem(string label)
        {
            return _repository.FirstOrDefault(r => r.Value.Label == label).Value;
        }
        public Item Get(int id)
        {
            return _repository.FirstOrDefault(r => r.Key == id).Value;
        }

        public void RemoveItem(int id)
        {
            _repository.Remove(id);
        }

        public List<Item> GetList()
        {
            return _repository.Values.ToList();
        }

        public List<Item> GetListExpiredItems()
        {
            return _repository.Where(r => r.Value.Expiration < DateTime.Today).Select(s=> s.Value).ToList();
        } 
    }
}