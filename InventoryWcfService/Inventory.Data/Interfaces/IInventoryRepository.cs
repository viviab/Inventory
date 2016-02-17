using System;
using System.Collections.Generic;
using Inventory.Data.Model;

namespace Inventory.Data.Interfaces
{
    public interface IInventoryRepository
    {
        Item Add(string label, DateTime expiration);
        Item Add(Item item);
        Item GetItem(string label);
        Item Get(int id);
        void RemoveItem(int id);
        List<Item> GetList();
        List<Item> GetListExpiredItems();
    }
}
