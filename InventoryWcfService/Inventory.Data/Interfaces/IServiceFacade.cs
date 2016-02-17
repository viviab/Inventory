using System;
using System.Collections.Generic;
using Inventory.Data.Model;

namespace Inventory.Data.Interfaces
{
    public interface IServiceFacade
    {
        Item AddItem(string label, DateTime expiration);
        Item TakeItem(int id);
        List<Item> GetListInventory();
        List<Item> GetListItemsExpired();
        Item GetItem(int id);
        Item GetItem(string label);
    }
}