using System;
using System.Collections.Generic;
using System.ServiceModel;
using Inventory.Data.Model;

namespace InventoryWcfService.Interfaces
{
    [ServiceContract]
    public interface IServices
    {
        [OperationContract]
        Item AddItem(string label, DateTime expiration);

        [OperationContract]
        void TakeItem(int id);

        [OperationContract]
        List<Item> GetListItems();

        [OperationContract]
        Item GetItem(string label);

    }
}
